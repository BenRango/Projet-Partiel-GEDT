Imports System.Data.OleDb

Public Class Form2
    ' Contrôles publics pour accéder aux valeurs depuis Form1
    Public Enum TypeElement
        [Class]
        Subject
        Professor
        EDT
    End Enum
    Public Property type As TypeElement
    Public Property SubjectId As Integer

    Public Property DayID As Integer

    Public Property WeekID As Integer

    Public Property CreneauID As Integer
    Public Property ProfID As Integer
    Public Property ClassID As Integer
    Public Property SubjectName As String
    Public Property HoursLeft As Integer
    Public Property ProfessorId As Integer
    Dim c As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\Élie N'srango\OneDrive\Documentos\gedt.accdb;")

    Public Sub LoadComboBoxes()
        Try
            If c.State <> ConnectionState.Open Then c.Open()

            Dim dtClasses As New DataTable
            Dim da As New OleDbDataAdapter("SELECT id, class_name FROM CLASS", c)
            da.Fill(dtClasses)
            If dtClasses.Columns.Contains("class_name") Then
                cbClasses.DataSource = dtClasses
                cbClasses.DisplayMember = "class_name"
                cbClasses.ValueMember = "id"
            Else
                MessageBox.Show("Colonne 'class_name' introuvable dans CLASS")
            End If

            Dim dtSubjects As New DataTable
            da = New OleDbDataAdapter("SELECT id, subject_name FROM SUBJECT", c)
            da.Fill(dtSubjects)
            If dtSubjects.Columns.Contains("subject_name") Then
                cbSubjects.DataSource = dtSubjects
                cbSubjects.DisplayMember = "subject_name"
                cbSubjects.ValueMember = "id"
                cbSubjects.SelectedValue = SubjectId
            Else
                MessageBox.Show("Colonne 'subject_name' introuvable dans SUBJECT")
            End If

            Dim dtWeeks As New DataTable
            da = New OleDbDataAdapter("SELECT id, week_no FROM WEEK", c)
            da.Fill(dtWeeks)
            cbWeeks.DataSource = dtWeeks
            cbWeeks.DisplayMember = "week_no"
            cbWeeks.ValueMember = "id"
            cbWeeks.SelectedValue = WeekID
            MsgBox($"WeekID = {WeekID}")

            Dim dtDays As New DataTable
            da = New OleDbDataAdapter("SELECT id, day_label FROM DAYS", c)
            da.Fill(dtDays)
            cbDays.DataSource = dtDays
            cbDays.DisplayMember = "day_label"
            cbDays.ValueMember = "id"
            cbDays.SelectedValue = DayID

            Dim dtCreneaux As New DataTable
            da = New OleDbDataAdapter("SELECT id, label FROM CRENEAU", c)
            da.Fill(dtCreneaux)
            cbCreneaux.DataSource = dtCreneaux
            cbCreneaux.DisplayMember = "label"
            cbCreneaux.ValueMember = "id"
            cbCreneaux.SelectedValue = CreneauID

        Catch ex As Exception
            MessageBox.Show("Erreur : " & ex.Message)
        Finally
            c.Close()
        End Try
    End Sub

    Private Sub classRoomAddField_ValueChanged(sender As Object, e As EventArgs) Handles classRoomAddField.ValueChanged
        Dim valueExists As Boolean = False
        Dim classrooms As DataTable = Form1.GetClassRooms()

        For Each row As DataRow In classrooms.Rows
            If row(0).ToString() = classRoomAddField.Value.ToString() Then
                valueExists = True
                Exit For
            End If
        Next

        If classRoomAddField.Value = 0 OrElse valueExists Then
            btnSave.Enabled = False
        Else
            btnSave.Enabled = True
        End If
    End Sub
    Private Sub EditSubjectForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If type = TypeElement.Subject Then
            Form1.HideChildPanelsExcept(Me, EditSubjectPanel)
            txtSubjectName.Text = SubjectName
            numHoursLeft.Value = HoursLeft

            ' Charger la liste des professeurs
            LoadProfessors()
        ElseIf type = TypeElement.Professor Then
            Form1.HideChildPanelsExcept(Me, EditProfPanel)
        ElseIf type = TypeElement.Class Then
            Form1.HideChildPanelsExcept(Me, EditClassPanel)
        ElseIf type = TypeElement.EDT Then
            Form1.HideChildPanelsExcept(Me, EditEDTPanel)
            LoadComboBoxes()

        End If
        ' Initialiser les champs avec les valeurs actuelles


        ' Sélectionner le professeur actuel
        cbProfessor.SelectedValue = ProfessorId
    End Sub

    Private Sub LoadProfessors()
        Try
            c.Open()
            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter("SELECT id, prof_name FROM PROFESSOR", c)
            da.Fill(dt)

            cbProfessor.DataSource = dt
            cbProfessor.DisplayMember = "prof_name"
            cbProfessor.ValueMember = "id"
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des professeurs: " & ex.Message)
        Finally
            c.Close()
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Select Case type
            Case TypeElement.EDT

            Case TypeElement.Subject
                ' Valider les entrées
                If String.IsNullOrWhiteSpace(txtSubjectName.Text) Then
                    MessageBox.Show("Veuillez entrer un nom de matière")
                    Return
                End If

                ' Mettre à jour les propriétés
                SubjectName = txtSubjectName.Text
                HoursLeft = CInt(numHoursLeft.Value)
                ProfessorId = CInt(cbProfessor.SelectedValue)
            Case TypeElement.Class
                If String.IsNullOrWhiteSpace(txtClassName.Text) Then
                    MessageBox.Show("Veuillez entrer un nom de classe")
                    Return
                End If
            Case TypeElement.Professor
                If String.IsNullOrWhiteSpace(txtProfName.Text) Or String.IsNullOrWhiteSpace(txtProfPhone.Text) Then
                    MessageBox.Show("Veuillez entrer un nom de Professeur")
                    Return
                End If
        End Select


        ' Fermer le formulaire avec DialogResult.OK
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class