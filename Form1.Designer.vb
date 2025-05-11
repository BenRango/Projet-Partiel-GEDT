<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.username_field = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.password_field = New System.Windows.Forms.TextBox()
        Me.login_button = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'username_field
        '
        Me.username_field.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.25!)
        Me.username_field.Location = New System.Drawing.Point(106, 142)
        Me.username_field.Name = "username_field"
        Me.username_field.Size = New System.Drawing.Size(169, 31)
        Me.username_field.TabIndex = 0
        Me.username_field.Text = "Nom d'utilisateur"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.DodgerBlue
        Me.Label1.Location = New System.Drawing.Point(103, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(172, 29)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Se Connecter"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!)
        Me.Label2.Location = New System.Drawing.Point(15, 313)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(133, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Pas encore inscrit ?"
        '
        'password_field
        '
        Me.password_field.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.25!)
        Me.password_field.Location = New System.Drawing.Point(103, 236)
        Me.password_field.Name = "password_field"
        Me.password_field.Size = New System.Drawing.Size(169, 31)
        Me.password_field.TabIndex = 3
        Me.password_field.Text = "Mot de passe"
        Me.password_field.UseSystemPasswordChar = True
        '
        'login_button
        '
        Me.login_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.login_button.Location = New System.Drawing.Point(108, 353)
        Me.login_button.Name = "login_button"
        Me.login_button.Size = New System.Drawing.Size(148, 36)
        Me.login_button.TabIndex = 4
        Me.login_button.Text = "Se connecter"
        Me.login_button.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.password_field)
        Me.Panel1.Controls.Add(Me.login_button)
        Me.Panel1.Controls.Add(Me.username_field)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(405, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(383, 426)
        Me.Panel1.TabIndex = 5
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightSeaGreen
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(2, 12)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(397, 426)
        Me.Panel2.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 19.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(1, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(396, 32)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "GESTIONNAIRE D'EMPLOI"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 18.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(125, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(158, 31)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "DU TEMPS"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents username_field As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents password_field As TextBox
    Friend WithEvents login_button As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
