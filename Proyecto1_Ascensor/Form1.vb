Public Class Form1
    Private btn_Subir(11) As Button ' Cambiar el tamaño del arreglo a 11
    Private btn_Bajar(11) As Button ' Cambiar el tamaño del arreglo a 11
    Private btn_ascensor As Button ' boton del ascensor

    ' RadioButtons para los Pisos
    Private grupoPisos As GroupBox
    Private WithEvents Pares As RadioButton
    Private WithEvents Impares As RadioButton
    Private WithEvents Todos As RadioButton

    ' RadioButtons para el Ascensor
    Private grupoAscensor As GroupBox
    Private WithEvents Encendido As RadioButton
    Private WithEvents Apagado As RadioButton
    Private WithEvents Mantenimiento As RadioButton

    Private posicion_inicial As Integer = 540 ' Defino la posición inicial que es PB en el edificio
    Private estado As String = "Detenido" ' Defino el estado del ascensor
    Private destino As Integer = 0 ' Piso destino
    Private WithEvents timer As Timer ' Temporizador para el movimiento del ascensor
    Private stopTimer As Timer ' Temporizador para la duración de la parada
    Private solicitudSubir(11) As Integer ' Arreglo para guardar solicitudes "Subir"
    Private solicitudBajar(11) As Integer ' Arreglo para guardar solicitudes "Bajar"
    Private slt_subir As Integer ' Contador para solicitudes de subir
    Private slt_bajar As Integer ' Contador para solicitudes de bajar
    Private procesando As Boolean = False ' Indica si el ascensor está procesando una llamada actualmente
    Private procesandoSubir As Boolean = True ' Indica si se están procesando llamadas de "Subir"

    Private Function Crear_Botones(text As Object, location As Point) As Button
        Dim boton As New Button ' Creo un objeto tipo Button
        If text.Equals("Subir") Or text.Equals("Bajar") Then
            boton.Size = New Size(50, 30)
        Else
            boton.Size = New Size(35, 30) ' Cambio el tamaño para el botón Ascensor
        End If
        boton.Location = location
        boton.Text = text.ToString()
        AddHandler boton.Click, AddressOf Click_Bajar_o_Subir ' Asignar el evento de clic a los botones de subir y bajar
        Return boton
    End Function

    Private Function Crear_Lables(text As Object, location As Point) As Label
        Dim label As New Label ' Creamos un objeto tipo Label
        label.Location = location
        label.Text = text.ToString()
        label.Size = New Size(20, 20)
        Return label
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btn_ascensor = Crear_Botones("0", New Point(180, posicion_inicial - 40)) ' Defino el botón para ascensor
        Controls.Add(btn_ascensor)
        Actualizar_Estado_Ascensor()
        For i = 0 To 11
            Dim lbl_piso1 As Label = Crear_Lables(i, New Point(150, 505 - 40 * i)) ' Labels para subir
            Dim btn_boton1 As Button = Crear_Botones("Subir", New Point(90, 500 - 40 * i)) ' Botones para subir
            Dim lbl_piso2 As Label = Crear_Lables(i + 1, New Point(230, 465 - 40 * i)) ' Labels para bajar
            Dim btn_boton2 As Button = Crear_Botones("Bajar", New Point(265, 460 - 40 * i)) ' Botones para bajar
            Controls.Add(lbl_piso1)
            Controls.Add(lbl_piso2)
            Controls.Add(btn_boton1)
            Controls.Add(btn_boton2)
            btn_Subir(i) = btn_boton1 ' Guardamos los botones de subir
            btn_Bajar(i) = btn_boton2 ' Guardamos los botones de bajar
        Next

        ' Inicializar el temporizador
        timer = New Timer()
        timer.Interval = 25 ' Intervalo de movimiento del ascensor
        estado = "Detenido " ' Estado inicial
    End Sub

    Private Sub Gestionar_Pisos(sender As Object, e As EventArgs) Handles MyBase.Load
        grupoPisos = New GroupBox() With {.Text = "Gestión de Pisos", .Location = New Point(400, 460), .Size = New Size(130, 120)}
        ' Inicializar RadioButtons para pisos
        Pares = New RadioButton() With {.Text = "Pares", .Location = New Point(10, 20)}
        Impares = New RadioButton() With {.Text = "Impares", .Location = New Point(10, 50)}
        Todos = New RadioButton() With {.Text = "Todos", .Location = New Point(10, 80)}
        grupoPisos.Controls.Add(Pares)
        grupoPisos.Controls.Add(Impares)
        grupoPisos.Controls.Add(Todos)
        Controls.Add(grupoPisos)
        Todos.Checked = True ' Establecer el estado inicial
    End Sub

    Private Sub Gestionar_Ascensor(sender As Object, e As EventArgs) Handles MyBase.Load
        grupoAscensor = New GroupBox() With {.Text = "Estado del Ascensor", .Location = New Point(555, 460), .Size = New Size(175, 120)}
        ' Inicializar RadioButtons para estado del ascensor
        Encendido = New RadioButton() With {.Text = "Encendido", .Location = New Point(10, 20), .Size = New Size(150, 25)}
        Apagado = New RadioButton() With {.Text = "Apagado", .Location = New Point(10, 50), .Size = New Size(150, 25)}
        Mantenimiento = New RadioButton() With {.Text = "Mantenimiento", .Location = New Point(10, 80), .Size = New Size(150, 25)}
        grupoAscensor.Controls.Add(Encendido)
        grupoAscensor.Controls.Add(Apagado)
        grupoAscensor.Controls.Add(Mantenimiento)
        Controls.Add(grupoAscensor)
        Encendido.Checked = True ' Establecer el estado inicial
    End Sub

    Private Sub MoverAscensor() Handles timer.Tick
        btn_ascensor.BackColor = Color.Green ' El ascensor empieza a moverse y se setea en verde

        If btn_ascensor.Location.Y > destino Then
            btn_ascensor.Location = New Point(btn_ascensor.Location.X, btn_ascensor.Location.Y - 1) ' Moviendo hacia arriba
        ElseIf btn_ascensor.Location.Y < destino Then
            btn_ascensor.Location = New Point(btn_ascensor.Location.X, btn_ascensor.Location.Y + 1) ' Moviendo hacia abajo
        End If

        Mostrar_Piso_Actual(btn_ascensor.Location.Y) ' Mostramos el piso actual en el ascensor

        If btn_ascensor.Location.Y = destino Then ' Verificar si el ascensor ha alcanzado su destino
            Select Case estado ' Resetear dependiendo del estado 
                Case "Subiendo"
                    Resetear_Color_Subir(destino) ' Resetea el botón de subir correspondiente al destino
                Case "Bajando"
                    Resetear_Color_Bajar(destino) ' Resetea el botón de bajar correspondiente al destino
            End Select

            ' Una vez llegado al destino detiene el ascensor
            Detener_Ascensor() 'llama a subrutina para detener el timer y actualizar el estado del ascensor
        End If
    End Sub

    Private Sub Detener_Ascensor()
        timer.Stop() ' Detengo el temporizador al llegar al destino

        estado = "Detenido" ' Cambio el estado a "Detenido"
        btn_ascensor.Text = ((posicion_inicial - btn_ascensor.Location.Y) \ 40) - 1 ' Actualizar el texto del botón con el piso actual
        btn_ascensor.BackColor = Color.Yellow ' El ascensor se detiene setea en amarillo

        Actualizar_Estado_Ascensor() ' Actualiza el estado del ascensor en el TextBox

        ' Iniciar el temporizador de parada
        stopTimer = New Timer()
        stopTimer.Interval = 3000 ' 3 segundos
        AddHandler stopTimer.Tick, AddressOf Parada_Piso
        stopTimer.Start() ' Iniciar el temporizador
    End Sub

    Private Sub Parada_Piso(sender As Object, e As EventArgs)
        stopTimer.Stop() ' Detener el temporizador de parada
        RemoveHandler stopTimer.Tick, AddressOf Parada_Piso ' Limpiar el evento

        ' Procesar la siguiente llamada si hay alguna
        If slt_subir > 0 Or slt_bajar > 0 Then
            Procesar_Siguiente_Llamada() ' Procesar la siguiente llamada en el arreglo
        Else
            procesando = False ' Marcar que no hay más llamadas
        End If
    End Sub

    Private Sub Click_Bajar_o_Subir(sender As Object, e As EventArgs)
        Dim btn_Click As Button = CType(sender, Button) ' Convierte el objeto que desencadenó el evento en un botón.
        Dim btn_index As Integer
        ' Verifica si el botón pertenece al array de botones de "Subir".
        btn_index = Array.IndexOf(btn_Subir, btn_Click)
        If btn_index >= 0 Then
            btn_Subir(btn_index).BackColor = Color.Cyan
            Llamar_Ascensor(btn_index, "Subir") ' Llama a la función con el índice correspondiente a "Subir".
            Exit Sub
        End If
        ' Verifica si el botón pertenece al array de botones de "Bajar".
        btn_index = Array.IndexOf(btn_Bajar, btn_Click)
        If btn_index >= 0 Then
            btn_Bajar(btn_index).BackColor = Color.Cyan
            Llamar_Ascensor(btn_index, "Bajar") ' Llama a la función con el índice correspondiente a "Bajar".
        End If
    End Sub

    Private Sub Llamar_Ascensor(index As Integer, estado As String) ' Método para gestionar la llamada del ascensor desde un piso específico, diferenciando si es "Subir" o "Bajar"
        If estado.Equals("Bajar") Then ' Si es "Bajar".

            If Botones_Impares(index) Or Botones_Pares(index) Then ' Verifica si el piso donde se hace la solicitud no está permitido para bajar.
                MessageBox.Show("El ascensor no se detiene en este piso según la configuración actual.")
                Return ' Si el piso no está permitido, no agrega la solicitud
            End If

            If slt_bajar < solicitudBajar.Length Then ' Agrega la solicitud de bajar al array de solicitudes de bajar si hay espacio disponible
                solicitudBajar(slt_bajar) = index
                slt_bajar += 1 ' Incrementa el contador de solicitudes de bajar
                Ordenar_Llamadas_Bajar() ' Ordena las solicitudes de bajar
            Else
                MessageBox.Show("No se pueden agregar más solicitudes de bajar.") ' Si no hay espacio para la solicitud, muestra un mensaje de advertencia
            End If

        ElseIf estado.Equals("Subir") Then ' Si es "Subir".

            If Botones_Impares(index + 1) Or Botones_Pares(index + 1) Then ' Verifica si el piso donde se hace la solicitud no está permitido para bajar.
                MessageBox.Show("El ascensor no se detiene en este piso según la configuración actual.")
                Return ' Si el piso no está permitido, no agrega la solicitud
            End If

            If slt_subir < solicitudSubir.Length Then ' Agrega la solicitud de bajar al array de solicitudes de bajar si hay espacio disponible
                solicitudSubir(slt_subir) = index
                slt_subir += 1 ' Incrementa el contador de solicitudes de Subir
                Ordenar_Llamadas_Subir() ' Ordena las solicitudes de Subir
            Else
                MessageBox.Show("No se pueden agregar más solicitudes de subir.") ' Si no hay espacio para la solicitud, muestra un mensaje de advertencia
            End If

        End If

        If procesando = False Then ' Si no se está procesando ninguna solicitud, empieza a procesar la siguiente llamada
            procesando = True
            Procesar_Siguiente_Llamada() ' Procesa la siguiente solicitud de ascensor
        End If
    End Sub

    Private Sub Procesar_Siguiente_Llamada() ' Método para procesar la siguiente llamada en la lista
        If slt_subir = 0 AndAlso slt_bajar = 0 Then
            procesando = False ' Finaliza el procesamiento si no hay más llamadas
            Return
        End If

        ' Verifica si el ascensor puede moverse
        If Not Estado_Ascensor() Then
            MessageBox.Show($"El ascensor no puede moverse, está en estado: {If(Apagado.Checked, "Apagado", "Mantenimiento")}") ' Muestra un mensaje del porque no se puede mover
            btn_ascensor.BackColor = Color.Red ' Cambiar a rojo
            procesando = False
            Return
        End If

        procesando = True ' Marca el inicio del procesamiento de una nueva llamada

        ' Cambia al tipo opuesto de llamada si el tipo actual se ha agotado
        If procesandoSubir AndAlso slt_subir = 0 Then
            procesandoSubir = False ' Cambia a "Bajar" si no hay más solicitudes de "Subir"
        ElseIf Not procesandoSubir AndAlso slt_bajar = 0 Then
            procesandoSubir = True ' Cambia a "Subir" si no hay más solicitudes de "Bajar"
        End If

        ' Determina el índice de la próxima llamada a procesar
        Dim sigIndice As Integer
        If procesandoSubir AndAlso slt_subir > 0 Then
            sigIndice = solicitudSubir(0)  ' Si esta procesando solicitudes de "Subir", obtiene el índice de la siguiente solicitud
            destino = 540 - ((sigIndice + 1) * 40) ' Calculo la posición "Y" del piso 
            estado = "Subiendo" ' Establezco el estado del ascensor en Subiendo
        ElseIf Not procesandoSubir AndAlso slt_bajar > 0 Then
            sigIndice = solicitudBajar(0) ' Si esta procesando solicitudes de "Bajar", obtiene el índice de la siguiente solicitud
            destino = 460 - ((sigIndice) * 40) ' Calculo la posición "Y" del piso
            estado = "Bajando" ' Establezco el estado del ascensor en Bajando
        Else
            Return ' Si no hay más llamadas, sale del método
        End If

        ' Actualiza el estado del ascensor en el TextBox
        Actualizar_Estado_Ascensor()

        ' Inicia el timer para mover el ascensor
        timer.Start()

        ' Elimina la llamada procesada del arreglo correspondiente
        If procesandoSubir Then
            For i As Integer = 1 To slt_subir - 1  ' Desplaza las solicitudes de "Subir" para eliminar la primera llamada procesada
                solicitudSubir(i - 1) = solicitudSubir(i)
            Next
            slt_subir -= 1 ' Decrementa el contador de solicitudes de "Subir"
        Else
            For i As Integer = 1 To slt_bajar - 1 ' Desplaza las solicitudes de "Bajar" para eliminar la primera llamada procesada
                solicitudBajar(i - 1) = solicitudBajar(i)
            Next
            slt_bajar -= 1 ' Decrementa el contador de solicitudes de "Bajar"
        End If
    End Sub

    Private Sub Ordenar_Llamadas_Subir() ' Ordenar los indices "Subir" 
        Array.Sort(solicitudSubir, 0, slt_subir) ' Ordena los elementos del arreglo en orden ascendente
    End Sub

    Private Sub Ordenar_Llamadas_Bajar() ' Ordenar los indices "Bajar" 
        Array.Sort(solicitudBajar, 0, slt_bajar) ' Ordena los elementos del arreglo en orden ascendente
        Array.Reverse(solicitudBajar, 0, slt_bajar) ' Invierte para que queden en orden descendente
    End Sub

    Private Sub Resetear_Color_Subir(destino As Integer) ' Resetea el color los botones designados a subir una vez que el ascensor llega a su destino
        Dim indice As Integer = (540 - destino) \ 40 - 1 ' Calcular el índice basado en el destino

        ' Verifica que el índice esté dentro del rango válido
        If indice >= 0 AndAlso indice < btn_Subir.Length Then
            btn_Subir(indice).BackColor = SystemColors.Control ' Restaurar el color original
        End If
    End Sub

    Private Sub Resetear_Color_Bajar(destino As Integer) ' Resetea el color los botones designados a subir una vez que el ascensor llega a su destino
        Dim indice As Integer = (460 - destino) \ 40 ' Calcular el índice basado en el destino

        ' Verifica que el índice esté dentro del rango válido
        If indice >= 0 AndAlso indice < btn_Bajar.Length Then
            btn_Bajar(indice).BackColor = SystemColors.Control ' Restaurar el color original
        End If
    End Sub

    Private Sub Mostrar_Piso_Actual(localizacion As Integer)
        Dim piso_actual As Integer = (posicion_inicial - localizacion) \ 40 ' Calculo la distancia entre pisos, con la localizacion
        btn_ascensor.Text = (piso_actual - 1).ToString() ' Muestra el piso actual en el boton
    End Sub

    Private Sub Encendido_Checked(sender As Object, e As EventArgs) Handles Encendido.CheckedChanged
        If Encendido.Checked And Estado_Ascensor() Then
            btn_ascensor.BackColor = SystemColors.Control ' Cambiar a verde
        End If
    End Sub

    Private Sub Apagado_Checked(sender As Object, e As EventArgs) Handles Apagado.CheckedChanged
        If Apagado.Checked And Not Estado_Ascensor() Then
            btn_ascensor.BackColor = Color.Red ' Cambiar a rojo
        End If
    End Sub

    Private Sub Mantenimiento_Checked(sender As Object, e As EventArgs) Handles Mantenimiento.CheckedChanged
        If Mantenimiento.Checked And Not Estado_Ascensor() Then
            btn_ascensor.BackColor = Color.Red ' Cambiar a rojo
        End If
    End Sub

    Private Function Estado_Ascensor() As Boolean
        If Apagado.Checked Then
            estado = "Apagado" ' Actualiza el estado
            Actualizar_Estado_Ascensor() ' Actualiza el TextBox
            Return False ' No se puede mover si está apagado
        ElseIf Mantenimiento.Checked Then
            estado = "Mantenimiento" ' Actualiza el estado
            Actualizar_Estado_Ascensor() ' Actualiza el TextBox
            Return False ' No se puede mover si está en mantenimiento
        End If
        estado = "Detenido"
        Actualizar_Estado_Ascensor()
        Return True ' El ascensor puede moverse si está detenido
    End Function

    Private Function Botones_Impares(index As Integer) As Boolean
        Return Impares.Checked AndAlso index Mod 2 <> 0 ' Si el boton impares esta seleccionado y el piso es un numero impar, envia True
    End Function

    Private Function Botones_Pares(index As Integer) As Boolean
        Return Pares.Checked AndAlso index Mod 2 = 0 ' Si el boton impares esta seleccionado y el piso es un numero par, envia True
    End Function

    Private Sub Actualizar_Estado_Ascensor()
        estado_print.Text = estado ' Asumiendo que 'estado' es una variable que contiene el estado del ascensor
    End Sub

End Class