<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        estado_print = New TextBox()
        Label10 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.FlatStyle = FlatStyle.System
        Label1.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        Label1.Location = New Point(714, 65)
        Label1.Name = "Label1"
        Label1.Size = New Size(331, 25)
        Label1.TabIndex = 0
        Label1.Text = "Universidad Tecnologica de Panamá"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        Label2.Location = New Point(674, 102)
        Label2.Name = "Label2"
        Label2.Size = New Size(411, 21)
        Label2.TabIndex = 1
        Label2.Text = "Facultad de Ingeniería de Sistemas Computacionales"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        Label3.Location = New Point(786, 239)
        Label3.Name = "Label3"
        Label3.Size = New Size(187, 21)
        Label3.TabIndex = 2
        Label3.Text = "Estudiante: Edgar Mora"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        Label8.Location = New Point(727, 134)
        Label8.Name = "Label8"
        Label8.Size = New Size(304, 21)
        Label8.TabIndex = 7
        Label8.Text = "Licenciatura en Desarrollo de Software"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        Label9.Location = New Point(772, 170)
        Label9.Name = "Label9"
        Label9.Size = New Size(214, 21)
        Label9.TabIndex = 8
        Label9.Text = "Desarrollo de Software VIII"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        Label4.Location = New Point(787, 204)
        Label4.Name = "Label4"
        Label4.Size = New Size(184, 21)
        Label4.TabIndex = 9
        Label4.Text = "Profesor: Ricardo Chan"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        Label5.Location = New Point(806, 276)
        Label5.Name = "Label5"
        Label5.Size = New Size(146, 21)
        Label5.TabIndex = 10
        Label5.Text = "Cedula: 8-955-756"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        Label6.Location = New Point(817, 312)
        Label6.Name = "Label6"
        Label6.Size = New Size(124, 21)
        Label6.TabIndex = 11
        Label6.Text = "Grupo: 1LS-232"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        Label7.Location = New Point(805, 345)
        Label7.Name = "Label7"
        Label7.Size = New Size(148, 21)
        Label7.TabIndex = 12
        Label7.Text = "Fecha: 20/10/2024"
        ' 
        ' estado_print
        ' 
        estado_print.Location = New Point(494, 357)
        estado_print.Name = "estado_print"
        estado_print.Size = New Size(100, 23)
        estado_print.TabIndex = 13
        estado_print.TabStop = False
        estado_print.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(524, 383)
        Label10.Name = "Label10"
        Label10.Size = New Size(42, 15)
        Label10.TabIndex = 14
        Label10.Text = "Estado"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1109, 616)
        Controls.Add(Label10)
        Controls.Add(estado_print)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents estado_print As TextBox
    Friend WithEvents Label10 As Label

End Class
