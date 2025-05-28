<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inscription
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tbLastNameField = New System.Windows.Forms.TextBox()
        Me.tbFirstNameField = New System.Windows.Forms.TextBox()
        Me.tbUsernameField = New System.Windows.Forms.TextBox()
        Me.tbPasswordField = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbRoleSelector = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tbLastNameField
        '
        Me.tbLastNameField.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLastNameField.Location = New System.Drawing.Point(415, 78)
        Me.tbLastNameField.Name = "tbLastNameField"
        Me.tbLastNameField.Size = New System.Drawing.Size(244, 29)
        Me.tbLastNameField.TabIndex = 0
        '
        'tbFirstNameField
        '
        Me.tbFirstNameField.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFirstNameField.Location = New System.Drawing.Point(415, 133)
        Me.tbFirstNameField.Name = "tbFirstNameField"
        Me.tbFirstNameField.Size = New System.Drawing.Size(244, 29)
        Me.tbFirstNameField.TabIndex = 1
        '
        'tbUsernameField
        '
        Me.tbUsernameField.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbUsernameField.Location = New System.Drawing.Point(415, 191)
        Me.tbUsernameField.Name = "tbUsernameField"
        Me.tbUsernameField.Size = New System.Drawing.Size(244, 29)
        Me.tbUsernameField.TabIndex = 2
        '
        'tbPasswordField
        '
        Me.tbPasswordField.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPasswordField.Location = New System.Drawing.Point(415, 250)
        Me.tbPasswordField.Name = "tbPasswordField"
        Me.tbPasswordField.Size = New System.Drawing.Size(244, 29)
        Me.tbPasswordField.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label1.Location = New System.Drawing.Point(140, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 21)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Nom"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label2.Location = New System.Drawing.Point(140, 258)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 21)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Mot de passe"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label3.Location = New System.Drawing.Point(140, 199)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 21)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Nom d'utilisateur"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label4.Location = New System.Drawing.Point(140, 141)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 21)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Prénom"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(349, 376)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(136, 41)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "S'inscrire"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DodgerBlue
        Me.Label5.Location = New System.Drawing.Point(343, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 32)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Inscription"
        '
        'cbRoleSelector
        '
        Me.cbRoleSelector.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cbRoleSelector.FormattingEnabled = True
        Me.cbRoleSelector.Items.AddRange(New Object() {"ADMINISTRATEUR", "PROFESSEUR", "ÉLÈVE"})
        Me.cbRoleSelector.Location = New System.Drawing.Point(415, 308)
        Me.cbRoleSelector.Name = "cbRoleSelector"
        Me.cbRoleSelector.Size = New System.Drawing.Size(244, 29)
        Me.cbRoleSelector.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label6.Location = New System.Drawing.Point(140, 316)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 21)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "ROLE"
        '
        'Inscription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cbRoleSelector)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbPasswordField)
        Me.Controls.Add(Me.tbUsernameField)
        Me.Controls.Add(Me.tbFirstNameField)
        Me.Controls.Add(Me.tbLastNameField)
        Me.Name = "Inscription"
        Me.Text = "Inscription"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbLastNameField As TextBox
    Friend WithEvents tbFirstNameField As TextBox
    Friend WithEvents tbUsernameField As TextBox
    Friend WithEvents tbPasswordField As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents cbRoleSelector As ComboBox
    Friend WithEvents Label6 As Label
End Class
