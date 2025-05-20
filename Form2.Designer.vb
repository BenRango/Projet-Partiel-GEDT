<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.txtSubjectName = New System.Windows.Forms.TextBox()
        Me.numHoursLeft = New System.Windows.Forms.NumericUpDown()
        Me.cbProfessor = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.EditSubjectPanel = New System.Windows.Forms.Panel()
        Me.EditClassPanel = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.classRoomAddField = New System.Windows.Forms.NumericUpDown()
        Me.txtClassName = New System.Windows.Forms.TextBox()
        Me.EditProfPanel = New System.Windows.Forms.Panel()
        Me.txtProfPhone = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtProfName = New System.Windows.Forms.TextBox()
        Me.EditEDTPanel = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbClasses = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cbCreneaux = New System.Windows.Forms.ComboBox()
        Me.cbSubjects = New System.Windows.Forms.ComboBox()
        Me.cbDays = New System.Windows.Forms.ComboBox()
        Me.cbWeeks = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        CType(Me.numHoursLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.EditSubjectPanel.SuspendLayout()
        Me.EditClassPanel.SuspendLayout()
        CType(Me.classRoomAddField, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.EditProfPanel.SuspendLayout()
        Me.EditEDTPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSubjectName
        '
        Me.txtSubjectName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubjectName.Location = New System.Drawing.Point(20, 126)
        Me.txtSubjectName.Name = "txtSubjectName"
        Me.txtSubjectName.Size = New System.Drawing.Size(133, 26)
        Me.txtSubjectName.TabIndex = 0
        '
        'numHoursLeft
        '
        Me.numHoursLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.numHoursLeft.Location = New System.Drawing.Point(194, 126)
        Me.numHoursLeft.Name = "numHoursLeft"
        Me.numHoursLeft.Size = New System.Drawing.Size(163, 26)
        Me.numHoursLeft.TabIndex = 1
        '
        'cbProfessor
        '
        Me.cbProfessor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.cbProfessor.FormattingEnabled = True
        Me.cbProfessor.Location = New System.Drawing.Point(420, 124)
        Me.cbProfessor.Name = "cbProfessor"
        Me.cbProfessor.Size = New System.Drawing.Size(121, 28)
        Me.cbProfessor.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnSave.Location = New System.Drawing.Point(418, 204)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 29)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Enregistrer"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnCancel.Location = New System.Drawing.Point(151, 204)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 29)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Annuler"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Nom de la matière"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(191, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(169, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Nombre d'heures restantes"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(417, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 16)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Enseignant"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(162, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(240, 25)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Modifier La Discipline"
        '
        'EditSubjectPanel
        '
        Me.EditSubjectPanel.Controls.Add(Me.Label4)
        Me.EditSubjectPanel.Controls.Add(Me.Label3)
        Me.EditSubjectPanel.Controls.Add(Me.Label2)
        Me.EditSubjectPanel.Controls.Add(Me.Label1)
        Me.EditSubjectPanel.Controls.Add(Me.cbProfessor)
        Me.EditSubjectPanel.Controls.Add(Me.numHoursLeft)
        Me.EditSubjectPanel.Controls.Add(Me.txtSubjectName)
        Me.EditSubjectPanel.Location = New System.Drawing.Point(54, 12)
        Me.EditSubjectPanel.Name = "EditSubjectPanel"
        Me.EditSubjectPanel.Size = New System.Drawing.Size(579, 187)
        Me.EditSubjectPanel.TabIndex = 9
        '
        'EditClassPanel
        '
        Me.EditClassPanel.Controls.Add(Me.Label5)
        Me.EditClassPanel.Controls.Add(Me.Label7)
        Me.EditClassPanel.Controls.Add(Me.Label8)
        Me.EditClassPanel.Controls.Add(Me.classRoomAddField)
        Me.EditClassPanel.Controls.Add(Me.txtClassName)
        Me.EditClassPanel.Location = New System.Drawing.Point(54, 12)
        Me.EditClassPanel.Name = "EditClassPanel"
        Me.EditClassPanel.Size = New System.Drawing.Size(579, 187)
        Me.EditClassPanel.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(162, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(209, 25)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Modifier La Classe"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(361, 97)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 16)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Salle de classe"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(65, 97)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 16)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Nom de la classe"
        '
        'classRoomAddField
        '
        Me.classRoomAddField.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.classRoomAddField.Location = New System.Drawing.Point(364, 123)
        Me.classRoomAddField.Name = "classRoomAddField"
        Me.classRoomAddField.Size = New System.Drawing.Size(163, 26)
        Me.classRoomAddField.TabIndex = 1
        '
        'txtClassName
        '
        Me.txtClassName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassName.Location = New System.Drawing.Point(63, 123)
        Me.txtClassName.Name = "txtClassName"
        Me.txtClassName.Size = New System.Drawing.Size(133, 26)
        Me.txtClassName.TabIndex = 0
        '
        'EditProfPanel
        '
        Me.EditProfPanel.Controls.Add(Me.txtProfPhone)
        Me.EditProfPanel.Controls.Add(Me.Label6)
        Me.EditProfPanel.Controls.Add(Me.Label9)
        Me.EditProfPanel.Controls.Add(Me.Label10)
        Me.EditProfPanel.Controls.Add(Me.txtProfName)
        Me.EditProfPanel.Location = New System.Drawing.Point(54, 12)
        Me.EditProfPanel.Name = "EditProfPanel"
        Me.EditProfPanel.Size = New System.Drawing.Size(579, 187)
        Me.EditProfPanel.TabIndex = 11
        '
        'txtProfPhone
        '
        Me.txtProfPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.txtProfPhone.Location = New System.Drawing.Point(363, 125)
        Me.txtProfPhone.Name = "txtProfPhone"
        Me.txtProfPhone.Size = New System.Drawing.Size(141, 26)
        Me.txtProfPhone.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(162, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(244, 25)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Modifier le Professeur"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(361, 97)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(143, 16)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Numéro de Téléphone"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(65, 97)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(121, 16)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Nom du professeur"
        '
        'txtProfName
        '
        Me.txtProfName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProfName.Location = New System.Drawing.Point(68, 125)
        Me.txtProfName.Name = "txtProfName"
        Me.txtProfName.Size = New System.Drawing.Size(133, 26)
        Me.txtProfName.TabIndex = 0
        '
        'EditEDTPanel
        '
        Me.EditEDTPanel.Controls.Add(Me.cbClasses)
        Me.EditEDTPanel.Controls.Add(Me.Label14)
        Me.EditEDTPanel.Controls.Add(Me.Label15)
        Me.EditEDTPanel.Controls.Add(Me.cbCreneaux)
        Me.EditEDTPanel.Controls.Add(Me.cbSubjects)
        Me.EditEDTPanel.Controls.Add(Me.cbDays)
        Me.EditEDTPanel.Controls.Add(Me.cbWeeks)
        Me.EditEDTPanel.Controls.Add(Me.Label16)
        Me.EditEDTPanel.Controls.Add(Me.Label17)
        Me.EditEDTPanel.Controls.Add(Me.Label18)
        Me.EditEDTPanel.Controls.Add(Me.Label11)
        Me.EditEDTPanel.Location = New System.Drawing.Point(28, 11)
        Me.EditEDTPanel.Name = "EditEDTPanel"
        Me.EditEDTPanel.Size = New System.Drawing.Size(629, 187)
        Me.EditEDTPanel.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(180, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(294, 25)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Modifier L'emploi du temps"
        '
        'cbClasses
        '
        Me.cbClasses.FormattingEnabled = True
        Me.cbClasses.Location = New System.Drawing.Point(11, 109)
        Me.cbClasses.Name = "cbClasses"
        Me.cbClasses.Size = New System.Drawing.Size(97, 21)
        Me.cbClasses.TabIndex = 24
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(32, 89)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(38, 13)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Classe"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(540, 88)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 13)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "Créneau"
        '
        'cbCreneaux
        '
        Me.cbCreneaux.FormattingEnabled = True
        Me.cbCreneaux.Location = New System.Drawing.Point(520, 109)
        Me.cbCreneaux.Name = "cbCreneaux"
        Me.cbCreneaux.Size = New System.Drawing.Size(97, 21)
        Me.cbCreneaux.TabIndex = 21
        '
        'cbSubjects
        '
        Me.cbSubjects.FormattingEnabled = True
        Me.cbSubjects.Location = New System.Drawing.Point(377, 109)
        Me.cbSubjects.Name = "cbSubjects"
        Me.cbSubjects.Size = New System.Drawing.Size(97, 21)
        Me.cbSubjects.TabIndex = 20
        '
        'cbDays
        '
        Me.cbDays.FormattingEnabled = True
        Me.cbDays.Location = New System.Drawing.Point(261, 109)
        Me.cbDays.Name = "cbDays"
        Me.cbDays.Size = New System.Drawing.Size(74, 21)
        Me.cbDays.TabIndex = 19
        '
        'cbWeeks
        '
        Me.cbWeeks.FormattingEnabled = True
        Me.cbWeeks.Location = New System.Drawing.Point(128, 109)
        Me.cbWeeks.Name = "cbWeeks"
        Me.cbWeeks.Size = New System.Drawing.Size(97, 21)
        Me.cbWeeks.TabIndex = 18
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(393, 89)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(52, 13)
        Me.Label16.TabIndex = 17
        Me.Label16.Text = "Discipline"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(284, 88)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(27, 13)
        Me.Label17.TabIndex = 16
        Me.Label17.Text = "Jour"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(149, 89)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(48, 13)
        Me.Label18.TabIndex = 15
        Me.Label18.Text = "Semaine"
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(669, 261)
        Me.Controls.Add(Me.EditEDTPanel)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.EditProfPanel)
        Me.Controls.Add(Me.EditClassPanel)
        Me.Controls.Add(Me.EditSubjectPanel)
        Me.Name = "Form2"
        Me.Text = "Form2"
        CType(Me.numHoursLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.EditSubjectPanel.ResumeLayout(False)
        Me.EditSubjectPanel.PerformLayout()
        Me.EditClassPanel.ResumeLayout(False)
        Me.EditClassPanel.PerformLayout()
        CType(Me.classRoomAddField, System.ComponentModel.ISupportInitialize).EndInit()
        Me.EditProfPanel.ResumeLayout(False)
        Me.EditProfPanel.PerformLayout()
        Me.EditEDTPanel.ResumeLayout(False)
        Me.EditEDTPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents txtSubjectName As TextBox
    Friend WithEvents numHoursLeft As NumericUpDown
    Friend WithEvents cbProfessor As ComboBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents EditSubjectPanel As Panel
    Friend WithEvents EditClassPanel As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents classRoomAddField As NumericUpDown
    Friend WithEvents txtClassName As TextBox
    Friend WithEvents EditProfPanel As Panel
    Friend WithEvents txtProfPhone As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtProfName As TextBox
    Friend WithEvents EditEDTPanel As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents cbClasses As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cbCreneaux As ComboBox
    Friend WithEvents cbSubjects As ComboBox
    Friend WithEvents cbDays As ComboBox
    Friend WithEvents cbWeeks As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
End Class
