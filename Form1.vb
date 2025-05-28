Imports System.Data.OleDb
Imports System.Linq.Expressions
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.Logging

Imports Microsoft.Office.Interop
Imports Excel = Microsoft.Office.Interop.Excel



Public Class Form1
    Dim c As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=..\..\gedt.accdb;")
    Dim editType As Form2.TypeElement
    Public Function GetClassRooms()
        Try
            Connexion()
            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter("SELECT id, class_room FROM CLASS", c)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex, MsgBoxStyle.Critical)
            Return Nothing
        Finally
            c.Close()
        End Try

    End Function
    Sub Connexion()
        Try
            AddWeeks(c)
            c.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub RoundButtonCorners(btn As Button, radius As Integer)
        ' Configuration du bouton pour un meilleur rendu
        btn.FlatAppearance.BorderSize = 2 ' Important pour voir la bordure
        btn.FlatAppearance.BorderColor = Color.Gray ' Couleur de la bordure

        Dim path As New Drawing2D.GraphicsPath()
        ' Crée un rectangle avec des coins arrondis
        path.AddArc(0, 0, radius, radius, 180, 90)
        path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
        path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
        path.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
        path.CloseFigure()

        btn.Region = New Region(path)

        ' Optionnel: Redessiner manuellement la bordure pour un meilleur résultat
        AddHandler btn.Paint, Sub(sender As Object, e As PaintEventArgs)
                                  Using pen As New Pen(btn.FlatAppearance.BorderColor, btn.FlatAppearance.BorderSize)
                                      e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                                      e.Graphics.DrawPath(pen, path)
                                  End Using
                              End Sub
    End Sub
    Sub DrawPanel(panel As Panel)
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Panel Then
                ctrl.Visible = False
            End If
        Next
        panel.Visible = True
    End Sub
    Public Sub HideChildPanelsExcept(parent As Control, exceptionPanel As Panel)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is Panel Then
                If Not ctrl.Equals(exceptionPanel) Then
                    ctrl.Visible = False
                Else
                    ctrl.Visible = True
                End If
                HideChildPanelsExcept(ctrl, exceptionPanel)
            End If
        Next
    End Sub



    Function ConflitHoraire() As Boolean
        Connexion()

        Dim profIdCmd As New OleDbCommand("SELECT professor_id FROM SUBJECT WHERE id = ?", c)
        profIdCmd.Parameters.AddWithValue("?", cbSubjects.SelectedValue)
        Dim professorId As Integer = CInt(profIdCmd.ExecuteScalar())

        MsgBox($"ProfesseurID : {professorId}")

        Dim cmd As New OleDbCommand($"
        SELECT COUNT(*) FROM emploiDuTemps
        WHERE day_id = ?
        AND week_id = ?
        AND creneau_id = ?
        AND (class_id = ? OR subject_id IN (
            SELECT id FROM SUBJECT WHERE professor_id = {professorId}
        ))", c)

        cmd.Parameters.AddWithValue("?", cbDays.SelectedValue)       ' 1. day_id
        cmd.Parameters.AddWithValue("?", cbWeeks.SelectedValue)      ' 2. week_id
        cmd.Parameters.AddWithValue("?", cbCreneaux.SelectedValue)   ' 3. creneau_id
        cmd.Parameters.AddWithValue("?", cbClasses.SelectedValue)    ' 4. class_id

        'Dim debugSql As String = "Paramètres passés à la requête :" & vbCrLf
        'For i As Integer = 0 To cmd.Parameters.Count - 1
        '    debugSql &= "Paramètre " & (i + 1) & " : " & cmd.Parameters(i).Value.ToString() & vbCrLf
        'Next
        'MsgBox(debugSql, MsgBoxStyle.Information, "Debug SQL")

        Dim count As Integer = CInt(cmd.ExecuteScalar())
        MsgBox("Conflits détectés : " & count)

        c.Close()

        Return count > 0
    End Function

    Function AddSubject()
        Connexion()
        If c.State <> ConnectionState.Open Then
            c.Open()
        End If

        Dim sql As New OleDb.OleDbCommand("INSERT INTO SUBJECT (subject_name, subject_hours_left, subject_total_hours, professor_id)
                                           VALUES(@name, @leftHours, @totalHours, @teacher)",
                                          c)
        sql.Parameters.AddWithValue("@name", subjectNameField.Text.Trim())
        sql.Parameters.AddWithValue("@leftHours", nuTotalHours.Value)
        sql.Parameters.AddWithValue("@totalHours", nuTotalHours.Value)
        sql.Parameters.AddWithValue("@teacher", cbTeacher.SelectedValue)
        Dim result As Integer = sql.ExecuteNonQuery()
        AddSubjectPannel.Visible = False
        Return result > 0
    End Function
    Function AddClass()
        Connexion()
        If c.State <> ConnectionState.Open Then
            c.Open()
        End If
        Dim sql As New OleDb.OleDbCommand("INSERT INTO CLASS (class_name, class_room)
                                           VALUES(@name, @classRoom)",
                                          c)
        sql.Parameters.AddWithValue("@name", classNameAddField.Text.Trim())
        sql.Parameters.AddWithValue("@classRoom", classRoomAddField.Value)
        Dim result As Integer = sql.ExecuteNonQuery()
        AddClassPannel.Visible = False
        Return result > 0
    End Function

    Function AddProfessor()
        Dim numero As Integer
        Dim texte As String = profPhoneAddField.Text.Trim()

        If texte.Length <> 10 OrElse Not Integer.TryParse(texte, numero) Then
            Throw New Exception("Numéro invalide. Entrez un numéro à 10 chiffres.")
        End If
        Connexion()
        If c.State <> ConnectionState.Open Then
            c.Open()
        End If
        Dim sql As New OleDb.OleDbCommand("INSERT INTO PROFESSOR (prof_name, phone)
                                           VALUES(@name, @phone)",
                                          c)
        sql.Parameters.AddWithValue("@name", profNameAddField.Text.Trim())
        sql.Parameters.AddWithValue("@phone", profPhoneAddField.Text.Trim())
        Dim result As Integer = sql.ExecuteNonQuery()
        AddProfPanel.Visible = False
        Return result > 0
    End Function
    Public Sub LoadComboBoxes()
        If c.State <> ConnectionState.Open Then
            c.Open()
        End If
        Dim dtClasses As New DataTable
        Dim da As New OleDbDataAdapter("SELECT id, class_name FROM CLASS", c)
        da.Fill(dtClasses)
        cbClasses.DataSource = dtClasses
        cbClasses.DisplayMember = "class_name"
        cbClasses.ValueMember = "id"

        cbClassSelector.DataSource = dtClasses
        cbClassSelector.DisplayMember = "class_name"
        cbClassSelector.ValueMember = "id"



        Dim dtSubjects As New DataTable
        da = New OleDbDataAdapter("SELECT id, subject_name FROM SUBJECT", c)
        da.Fill(dtSubjects)
        cbSubjects.DataSource = dtSubjects
        cbSubjects.DisplayMember = "subject_name"
        cbSubjects.ValueMember = "id"


        Dim dtWeeks As New DataTable
        da = New OleDbDataAdapter("SELECT id, week_no FROM WEEK", c)
        da.Fill(dtWeeks)
        cbWeeks.DataSource = dtWeeks
        cbWeeks.DisplayMember = "week_no"
        cbWeeks.ValueMember = "id"

        cbWeekSelector.DataSource = dtWeeks
        cbWeekSelector.DisplayMember = "week_no"
        cbWeekSelector.ValueMember = "id"


        Dim dtDays As New DataTable
        da = New OleDbDataAdapter("SELECT id, day_label FROM DAYS", c)
        da.Fill(dtDays)
        cbDays.DataSource = dtDays
        cbDays.DisplayMember = "day_label"
        cbDays.ValueMember = "id"

        Dim dtCreneaux As New DataTable
        da = New OleDbDataAdapter("SELECT id, label FROM CRENEAU", c)
        da.Fill(dtCreneaux)
        cbCreneaux.DataSource = dtCreneaux
        cbCreneaux.DisplayMember = "label"
        cbCreneaux.ValueMember = "id"

        c.Close()
    End Sub

    Sub AddWeeks(c As OleDbConnection)
        If c.State <> ConnectionState.Open Then
            c.Open()
        End If

        Dim currentCount As Integer
        Dim countCommand As New OleDbCommand("SELECT COUNT(*) FROM WEEK", c)

        currentCount = Convert.ToInt32(countCommand.ExecuteScalar())
        If currentCount < 52 Then
            For i As Integer = currentCount + 1 To 52
                Dim insertCommand As New OleDbCommand("INSERT INTO WEEK (week_no) VALUES (@weekNo)", c)
                insertCommand.Parameters.AddWithValue("@weekNo", i)
                insertCommand.ExecuteNonQuery()
            Next i
            MessageBox.Show("Semaines ajoutées jusqu'à 52")

        End If
        c.Close()
    End Sub
    Private Sub login_button_Click(sender As Object, e As EventArgs) Handles login_button.Click
        Try
            Connexion()

            Dim sql As New OleDb.OleDbCommand("SELECT * FROM USERS WHERE Trim(username) = @username AND password = @password", c)
            sql.Parameters.AddWithValue("@username", username_field.Text)
            sql.Parameters.AddWithValue("@password", Inscription.HashSHA256(password_field.Text))
            Dim log As String = "Requête : " & sql.CommandText & vbCrLf
            'For Each p As OleDbParameter In sql.Parameters
            'log &= $"{p.ParameterName} = {p.Value}" & vbCrLf
            'Next
            'MsgBox(log)
            Dim reader As OleDbDataReader = sql.ExecuteReader()

            If reader.HasRows Then
                MsgBox("Connexion réussie !")
                LoadComboBoxes()
                DrawPanel(HomePanel)
            Else
                While reader.Read()
                    MsgBox("Utilisateur trouvé : " & reader("username").ToString() & reader("password").ToString())
                End While
                MsgBox("Nom d'utilisateur ou mot de passe incorrect.")
            End If

            reader.Close()
        Catch ex As Exception
            MsgBox("Erreur : " & ex.Message)
        Finally
            c.Close()
        End Try
    End Sub
    Private Sub ChargerEmploiDuTemps()
        Dim requete As String = "
        SELECT emploiDuTemps.N° As ID,
            WEEK.week_no AS [No Week],
            DAYS.day_label AS Jour ,
            CRENEAU.label AS Créneau,
            CLASS.class_name AS Classe,
            SUBJECT.subject_name AS Matière,
            PROFESSOR.prof_name AS Professeur
        FROM ((((((emploiDuTemps
        INNER JOIN WEEK ON emploiDuTemps.week_id = WEEK.id)
        INNER JOIN DAYS ON emploiDuTemps.day_id = DAYS.id)
        INNER JOIN CRENEAU ON emploiDuTemps.creneau_id = CRENEAU.id)
        INNER JOIN CLASS ON emploiDuTemps.class_id = CLASS.id)
        INNER JOIN SUBJECT ON emploiDuTemps.subject_id = SUBJECT.id)
        INNER JOIN PROFESSOR ON SUBJECT.professor_id = PROFESSOR.id )       
        ORDER BY WEEK.week_no, DAYS.day_order, CRENEAU.creneau_order
    "

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter(requete, c)
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Sub LoadItemsDataGridViews()
        Connexion()
        If c.State <> ConnectionState.Open Then
            c.Open()
        End If
        Dim dtClasses = New DataTable
        Dim da As New OleDbDataAdapter("SELECT  id As [ID], class_name AS Nom, class_room AS [SALLE] FROM CLASS", c)
        da.Fill(dtClasses)
        dgvClasses.DataSource = dtClasses

        Dim dtSubjects = New DataTable
        Dim requete As String = "
            SELECT SUBJECT.id As [ID], subject_name AS Nom, subject_hours_left AS [Heures restantes], PROFESSOR.prof_name AS Enseignant 
            FROM (SUBJECT
            INNER JOIN PROFESSOR ON SUBJECT.professor_id = PROFESSOR.id)                                              
"
        da = New OleDbDataAdapter(requete, c)
        da.Fill(dtSubjects)
        dgvSubjects.DataSource = dtSubjects

        Dim dtProfs = New DataTable
        da = New OleDbDataAdapter("SELECT  id As [ID], prof_name AS Nom, phone AS [Téléphone] FROM PROFESSOR", c)
        da.Fill(dtProfs)
        dgvProfs.DataSource = dtProfs
        c.Close()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.Visible = True
        Panel2.Visible = True
        RoundButtonCorners(login_button, 25)
        SetPlaceholder(username_field, "Nom d'utilisateur")
        SetPlaceholder(password_field, "Mot de passe", True)
        DrawPanel(Panel1)
        Panel2.Visible = True

    End Sub


    Sub LoadEDTInGrid(tlp As TableLayoutPanel, idClasse As Integer, semaineId As Integer)
        Connexion()

        Dim jours As New List(Of String)
        Dim creneaux As New List(Of String)

        Dim cmdJours As New OleDbCommand("SELECT id, day_label FROM DAYS ORDER BY day_order", c)
        Dim rJours = cmdJours.ExecuteReader()
        While rJours.Read()
            jours.Add(rJours("day_label").ToString())
        End While
        rJours.Close()

        Dim cmdCreneaux As New OleDbCommand("SELECT id, label FROM CRENEAU ORDER BY creneau_order", c)
        Dim rCreneaux = cmdCreneaux.ExecuteReader()
        While rCreneaux.Read()
            creneaux.Add(rCreneaux("label").ToString())
        End While
        rCreneaux.Close()


        tlp.Controls.Clear()
        tlp.ColumnCount = jours.Count + 1
        tlp.RowCount = creneaux.Count + 1

        tlp.ColumnStyles.Clear()
        tlp.RowStyles.Clear()
        tlp.AutoSize = True

        For i = 0 To tlp.ColumnCount - 1
            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 80 / tlp.ColumnCount))
        Next

        For i = 0 To tlp.RowCount - 1
            tlp.RowStyles.Add(New RowStyle(SizeType.Absolute, 64))
        Next

        For col = 1 To jours.Count
            Dim lbl As New Label With {
            .Text = jours(col - 1),
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .BackColor = Color.LightGray,
            .BorderStyle = BorderStyle.FixedSingle
        }
            tlp.Controls.Add(lbl, col, 0)
        Next

        For row = 1 To creneaux.Count
            Dim lbl As New Label With {
            .Text = creneaux(row - 1),
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .BackColor = Color.LightGray,
            .BorderStyle = BorderStyle.FixedSingle
        }
            tlp.Controls.Add(lbl, 0, row)
        Next

        Dim da As New OleDbDataAdapter("
        SELECT DAYS.day_label, CRENEAU.label, SUBJECT.subject_name, PROFESSOR.prof_name
        FROM(((( emploiDuTemps
        INNER JOIN DAYS ON emploiDuTemps.day_id = DAYS.id)
        INNER JOIN CRENEAU ON emploiDuTemps.creneau_id = CRENEAU.id)
        INNER JOIN SUBJECT ON emploiDuTemps.subject_id = SUBJECT.id)
        INNER JOIN PROFESSOR ON SUBJECT.professor_id = PROFESSOR.id)
        WHERE emploiDuTemps.class_id = ? AND emploiDuTemps.week_id = ?
        ORDER BY DAYS.day_order, CRENEAU.creneau_order", c)

        da.SelectCommand.Parameters.AddWithValue("?", idClasse)
        da.SelectCommand.Parameters.AddWithValue("?", semaineId)

        Dim dt As New DataTable
        da.Fill(dt)

        For Each row As DataRow In dt.Rows
            Dim jour = row("day_label").ToString()
            Dim creneau = row("label").ToString()
            Dim matiere = row("subject_name").ToString()
            Dim prof = row("prof_name").ToString()

            Dim rowIndex = creneaux.IndexOf(creneau) + 1
            Dim colIndex = jours.IndexOf(jour) + 1

            Dim lbl As New Label With {
            .Text = matiere & vbCrLf & prof,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.White
        }

            tlp.Controls.Add(lbl, colIndex, rowIndex)
        Next

        c.Close()
    End Sub


    Public Sub SetPlaceholder(txtBox As TextBox, placeholderText As String, Optional secret As Boolean = False)

        txtBox.Tag = placeholderText
        txtBox.Text = placeholderText
        txtBox.ForeColor = Color.Gray

        AddHandler txtBox.GotFocus, Sub(sender, args)
                                        Dim box = DirectCast(sender, TextBox)
                                        If box.Text = box.Tag.ToString() Then
                                            If secret = True Then
                                                box.UseSystemPasswordChar = True
                                            End If
                                            box.Text = ""
                                            box.ForeColor = Color.Black
                                        End If
                                    End Sub

        AddHandler txtBox.LostFocus, Sub(sender, args)
                                         Dim box = DirectCast(sender, TextBox)
                                         If String.IsNullOrWhiteSpace(box.Text) Then
                                             box.UseSystemPasswordChar = False
                                             box.Text = box.Tag.ToString()
                                             box.ForeColor = Color.Gray
                                         End If
                                     End Sub
    End Sub


    Private Sub AddEDTButton_Click(sender As Object, e As EventArgs) Handles addEDTButton.Click
        LoadComboBoxes()
        DrawPanel(AddEDTPannel)

    End Sub

    Private Sub EditEDTButton_Click(sender As Object, e As EventArgs) Handles editEDTButton.Click
        ChargerEmploiDuTemps()
        DrawPanel(EditOrDeleteEDTPanel)
    End Sub

    Private Sub DeleteEDTButton_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ListEDTButton_Click(sender As Object, e As EventArgs) Handles listEDTButton.Click
        HideChildPanelsExcept(edtPannel, EDTOptionsSelectorPanel)

        DrawPanel(edtPannel)
        ChargerEmploiDuTemps()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Connexion()
            If ConflitHoraire() Then
                MsgBox("Conflit détecté ! Ce créneau est déjà occupé.")
                c.Close()
                Exit Sub
            End If
            If c.State <> ConnectionState.Open Then
                c.Open()
            End If
            Dim sql As New OleDb.OleDbCommand("
            INSERT INTO emploiDuTemps (week_id, day_id, creneau_id, class_id, subject_id)
            VALUES (?, ?, ?, ?, ?)", c)

            sql.Parameters.AddWithValue("?", cbWeeks.SelectedValue)
            sql.Parameters.AddWithValue("?", cbDays.SelectedValue)
            sql.Parameters.AddWithValue("?", cbCreneaux.SelectedValue)
            sql.Parameters.AddWithValue("?", cbClasses.SelectedValue)
            sql.Parameters.AddWithValue("?", cbSubjects.SelectedValue)
            'Dim log As String = "Requête : " & sql.CommandText & vbCrLf

            'For Each p As OleDbParameter In sql.Parameters
            'log &= $"{p.ParameterName} = {p.Value}" & vbCrLf
            'Next
            'MsgBox(log)


            Dim result As Integer = sql.ExecuteNonQuery()
            MsgBox("Insertion réussie")
        Catch ex As Exception
            MsgBox("Erreur : " & ex.Message)
        Finally
            If c.State = ConnectionState.Open Then
                c.Close()
            End If
        End Try
    End Sub


    Private Sub Back_From_AddEDT_Click(sender As Object, e As EventArgs) Handles back_From_AddEDT.Click
        DrawPanel(HomePanel)
    End Sub

    Private Sub BackFromShowEDT_Click(sender As Object, e As EventArgs) Handles backFromShowEDT.Click
        DrawPanel(HomePanel)
    End Sub

    Private Sub ItemToAddConfirmButton_Click(sender As Object, e As EventArgs) Handles ItemToAddConfirmButton.Click
        confirmAddItemButton.Enabled = True
        Select Case cbItemToAdd.SelectedItem.ToString()
            Case "MATIÈRE"
                Connexion()
                If c.State <> ConnectionState.Open Then
                    c.Open()
                End If
                Dim dt As New DataTable
                Dim da As New OleDbDataAdapter("SELECT id, prof_name FROM PROFESSOR", c)
                MsgBox("Nombre de lignes : " & dt.Rows.Count.ToString())

                da.Fill(dt)
                cbTeacher.DataSource = dt
                cbTeacher.DisplayMember = "prof_name"
                cbTeacher.ValueMember = "id"
                c.Close()
                HideChildPanelsExcept(AddItemPannel, AddSubjectPannel)

            Case "PROFESSEUR"
                HideChildPanelsExcept(AddItemPannel, AddProfPanel)
            Case "CLASSE"
                HideChildPanelsExcept(AddItemPannel, AddClassPannel)
        End Select
    End Sub

    Private Sub ConfirmAddItemButton_Click(sender As Object, e As EventArgs) Handles confirmAddItemButton.Click
        Dim done As Boolean = False
        Select Case cbItemToAdd.SelectedItem.ToString()
            Case "MATIÈRE"
                If AddSubject() Then
                    done = True
                End If
            Case "PROFESSEUR"
                If AddProfessor() Then
                    done = True
                End If
            Case "CLASSE"
                If AddClass() Then
                    done = True
                End If
        End Select
        If done Then
            MsgBox("Création réussie !")
        Else
            MsgBox("Erreur lors de la créarion", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub AddSubjectButton_Click(sender As Object, e As EventArgs) Handles AddSubjectButton.Click
        DrawPanel(AddItemPannel)
    End Sub

    Private Sub classRoomAddField_ValueChanged(sender As Object, e As EventArgs) Handles classRoomAddField.ValueChanged
        Dim valueExists As Boolean = False
        Dim classrooms As DataTable = GetClassRooms()

        For Each row As DataRow In classrooms.Rows
            If row(0).ToString() = classRoomAddField.Value.ToString() Then
                valueExists = True
                Exit For
            End If
        Next

        If classRoomAddField.Value = 0 OrElse valueExists Then
            confirmAddItemButton.Enabled = False
        Else
            confirmAddItemButton.Enabled = True
        End If
    End Sub

    Private Sub BackFromAddItem_Click(sender As Object, e As EventArgs) Handles BackFromAddItem.Click
        DrawPanel(HomePanel)
    End Sub

    Private Sub BackFromShowItems_Click(sender As Object, e As EventArgs) Handles BackFromShowItems.Click
        DrawPanel(HomePanel)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LoadItemsDataGridViews()
        DrawPanel(ShowItemsPanel)
    End Sub

    Private Sub selectEDTOptionsButton_Click(sender As Object, e As EventArgs) Handles selectEDTOptionsButton.Click
        LoadEDTInGrid(TableLayoutPanel1, cbClassSelector.SelectedValue, cbWeekSelector.SelectedValue)
        HideChildPanelsExcept(edtPannel, weekEDTSubPanel)
        TableLayoutPanel1.Visible = True
    End Sub

    Private Sub dgvSubjects_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSubjects.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = dgvSubjects.Rows(e.RowIndex)

            Dim subjectName As String = selectedRow.Cells("Nom").Value.ToString()
            Dim remainingHours As String = selectedRow.Cells("Heures restantes").Value.ToString()
            Dim teacher As String = selectedRow.Cells("Enseignant").Value.ToString()

            'MessageBox.Show($"Matière: {subjectName}" & vbCrLf &
            '              $"Heures restantes: {remainingHours}" & vbCrLf &
            '             $"Enseignant: {teacher}", "Détails de la matière")


        End If
    End Sub
    Sub EditClass(id As Integer, name As String, room As Integer)
        Try
            Connexion()
            If c.State <> ConnectionState.Open Then
                c.Open()
            End If
            Dim sql As New OleDb.OleDbCommand("UPDATE CLASS SET class_name = @name, class_room = @room WHERE id = @id", c)
            sql.Parameters.AddWithValue("@name", name)
            sql.Parameters.AddWithValue("@room", room)
            sql.Parameters.AddWithValue("@id", id)

            Dim result As Integer = sql.ExecuteNonQuery()
            If result > 0 Then
                MsgBox("Classe mise à jour avec succès.")
            Else
                MsgBox("Erreur lors de la mise à jour de la classe.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub EditSubject(id As Integer, name As String, hoursLeft As String, teacher As String)
        Try
            Connexion()
            If c.State <> ConnectionState.Open Then
                c.Open()
            End If
            Dim sql As New OleDb.OleDbCommand("UPDATE SUBJECT SET subject_name = @name, subject_hours_left = @hoursLeft, professor_id = @teacher WHERE id = @id", c)
            sql.Parameters.AddWithValue("@name", name)
            sql.Parameters.AddWithValue("@hoursLeft", hoursLeft)
            sql.Parameters.AddWithValue("@teacher", teacher)
            sql.Parameters.AddWithValue("@id", id)

            Dim result As Integer = sql.ExecuteNonQuery()
            If result > 0 Then
                MsgBox("Matière mise à jour avec succès.")
            Else
                MsgBox("Erreur lors de la mise à jour de la matière.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub EditProfessor(id As Integer, name As String, phone As String)
        Try
            Connexion()
            If c.State <> ConnectionState.Open Then
                c.Open()
            End If
            Dim sql As New OleDb.OleDbCommand("UPDATE PROFESSOR SET prof_name = @name, phone = @phone WHERE id = @id", c)
            sql.Parameters.AddWithValue("@name", name)
            sql.Parameters.AddWithValue("@phone", phone)
            sql.Parameters.AddWithValue("@id", id)

            Dim result As Integer = sql.ExecuteNonQuery()
            If result > 0 Then
                MsgBox("Professeur mis à jour avec succès.")
            Else
                MsgBox("Erreur lors de la mise à jour du professeur.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub DeleteClass(id As Integer)
        Try
            Connexion()
            If c.State <> ConnectionState.Open Then
                c.Open()
            End If
            Dim sql As New OleDb.OleDbCommand("DELETE FROM CLASS WHERE id = @id", c)
            sql.Parameters.AddWithValue("@id", id)

            Dim result As Integer = sql.ExecuteNonQuery()
            If result > 0 Then
                MsgBox("Classe supprimée avec succès.")
            Else
                MsgBox("Erreur lors de la suppression de la classe.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox($"Erreur : {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub
    Sub DeleteSubject(id As Integer)
        Try
            Connexion()
            If c.State <> ConnectionState.Open Then
                c.Open()
            End If
            Dim sql As New OleDb.OleDbCommand("DELETE FROM SUUJECT WHERE id = @id", c)
            sql.Parameters.AddWithValue("@id", id)

            Dim result As Integer = sql.ExecuteNonQuery()
            If result > 0 Then
                MsgBox("Discipline supprimée avec succès.")
            Else
                MsgBox("Erreur lors de la suppression de la discipline.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox($"Erreur : {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub

    Sub DeleteProfessor(id As Integer)
        Try
            Connexion()
            If c.State <> ConnectionState.Open Then
                c.Open()
            End If
            Dim sql As New OleDb.OleDbCommand("DELETE FROM PROFESSOR WHERE id = @id", c)
            sql.Parameters.AddWithValue("@id", id)

            Dim result As Integer = sql.ExecuteNonQuery()
            If result > 0 Then
                MsgBox("Professeur supprimée avec succès.")
            Else
                MsgBox("Erreur lors de la suppression du professeur.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox($"Erreur : {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim hitTestInfo = DataGridView1.HitTest(e.X, e.Y)
            If hitTestInfo.RowIndex >= 0 Then
                DataGridView1.ClearSelection()
                DataGridView1.Rows(hitTestInfo.RowIndex).Selected = True
                ContextMenuStrip1.Show(DataGridView1, e.Location)
                editType = Form2.TypeElement.EDT
            End If
        End If
    End Sub
    Private Sub dgvSubjects_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvSubjects.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim hitTestInfo = dgvSubjects.HitTest(e.X, e.Y)
            If hitTestInfo.RowIndex >= 0 Then
                dgvSubjects.ClearSelection()
                dgvSubjects.Rows(hitTestInfo.RowIndex).Selected = True
                ContextMenuStrip1.Show(dgvSubjects, e.Location)
                editType = Form2.TypeElement.Subject
            End If
        End If
    End Sub
    Private Sub dgvClass_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvClasses.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim hitTestInfo = dgvClasses.HitTest(e.X, e.Y)
            If hitTestInfo.RowIndex >= 0 Then
                dgvClasses.ClearSelection()
                dgvClasses.Rows(hitTestInfo.RowIndex).Selected = True
                ContextMenuStrip1.Show(dgvClasses, e.Location)
                editType = Form2.TypeElement.Class
            End If
        End If
    End Sub

    Private Sub dgvProfs_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvProfs.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim hitTestInfo = dgvProfs.HitTest(e.X, e.Y)
            If hitTestInfo.RowIndex >= 0 Then
                dgvProfs.ClearSelection()
                dgvProfs.Rows(hitTestInfo.RowIndex).Selected = True
                ContextMenuStrip1.Show(dgvProfs, e.Location)
                editType = Form2.TypeElement.Professor
            End If
        End If
    End Sub

    Function FindDayByLabel(label As String)
        Connexion()
        Dim cmd As New OleDbCommand("SELECT id FROM DAYS WHERE day_label = ?", c)
        cmd.Parameters.AddWithValue("?", label)
        Dim reader As OleDbDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            MsgBox($"ID du jour '{label}': {reader("id")}")
            Return reader("id")
        End If
        reader.Close()
        c.Close()
        Return Nothing
    End Function
    Function FindCreneauByLabel(label As String)
        Connexion()
        Dim cmd As New OleDbCommand("SELECT id FROM CRENEAU WHERE label = ?", c)
        cmd.Parameters.AddWithValue("?", label)
        Dim reader As OleDbDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            MsgBox($"ID du créneau '{label}': {reader("id")}")
            Return reader("id")
        End If
        reader.Close()
        c.Close()
        Return Nothing
    End Function
    Function FindClassByLabel(label As String)
        Connexion()
        Dim cmd As New OleDbCommand("SELECT id FROM CLASS WHERE class_name = ?", c)
        cmd.Parameters.AddWithValue("?", label)
        Dim reader As OleDbDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            MsgBox($"ID de la classe '{label}': {reader("id")}")
            Return reader("id")
        End If
        reader.Close()
        c.Close()
        Return Nothing
    End Function

    Function FindSubjectByLabel(label As String)
        Connexion()
        Dim cmd As New OleDbCommand("SELECT id FROM SUBJECT WHERE subject_name = ?", c)
        cmd.Parameters.AddWithValue("?", label)
        Dim reader As OleDbDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            MsgBox($"ID de la matière '{label}': {reader("id")}")
            Return reader("id")
        End If
        reader.Close()
        c.Close()
        Return Nothing
    End Function
    Function FindWeekByNo(weekNo As String)
        Connexion()
        Dim cmd As New OleDbCommand("SELECT id FROM WEEK WHERE week_no = ?", c)
        cmd.Parameters.AddWithValue("?", weekNo)
        Dim reader As OleDbDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            MsgBox($"ID de la semaine '{weekNo}': {reader("id")}")
            Return reader("id")
        End If
        reader.Close()
        c.Close()
        Return Nothing
    End Function

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditerToolStripMenuItem.Click
        If dgvSubjects.SelectedRows.Count = 0 And dgvProfs.SelectedRows.Count = 0 And dgvClasses.SelectedRows.Count Then Exit Sub

        Try
            Select Case editType
                Case Form2.TypeElement.EDT
                    LoadComboBoxes()
                    Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
                    If selectedRow.Cells("No Week").Value Is Nothing OrElse
                   selectedRow.Cells("Jour").Value Is Nothing OrElse
                   selectedRow.Cells("Créneau").Value Is Nothing OrElse
                   selectedRow.Cells("Classe").Value Is Nothing OrElse
                   selectedRow.Cells("Matière").Value Is Nothing Then
                        MessageBox.Show("Données incomplètes pour cet emploi du temps.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                    Using editForm As New Form2()
                        editForm.type = Form2.TypeElement.EDT
                        editForm.WeekID = CInt(FindWeekByNo(selectedRow.Cells("No Week").Value))
                        editForm.CreneauID = CInt(FindCreneauByLabel(selectedRow.Cells("Créneau").Value))
                        editForm.ClassID = CInt(FindClassByLabel(selectedRow.Cells("Classe").Value))
                        editForm.DayID = CInt(FindDayByLabel(selectedRow.Cells("Jour").Value))
                        editForm.SubjectId = CInt(FindSubjectByLabel(selectedRow.Cells("Matière").Value))
                        If editForm.ShowDialog() = DialogResult.OK Then
                            LoadItemsDataGridViews()
                        End If
                    End Using
                    Using editForm As New Form2()
                        editForm.type = Form2.TypeElement.EDT
                        If editForm.ShowDialog() = DialogResult.OK Then
                            LoadItemsDataGridViews()
                        End If
                    End Using
                Case Form2.TypeElement.Class
                    MsgBox("On va modifier une classe")
                    Dim selectedRow As DataGridViewRow = dgvClasses.SelectedRows(0)
                    If selectedRow.Cells("ID").Value Is Nothing OrElse
                   selectedRow.Cells("Nom").Value Is Nothing OrElse
                   selectedRow.Cells("SALLE").Value Is Nothing Then
                        MessageBox.Show("Données incomplètes pour cette matière.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                    Using editForm As New Form2()
                        editForm.type = Form2.TypeElement.Class
                        editForm.ClassID = CInt(selectedRow.Cells("ID").Value)
                        editForm.txtClassName.Text = selectedRow.Cells("Nom").Value.ToString()
                        editForm.classRoomAddField.Value = CInt(selectedRow.Cells("SALLE").Value)
                        If editForm.ShowDialog() = DialogResult.OK Then
                            EditClass(editForm.ClassID,
                                  editForm.txtClassName.Text,
                                  editForm.classRoomAddField.Value)
                            LoadItemsDataGridViews()
                        End If
                    End Using
                Case Form2.TypeElement.Subject
                    Dim selectedRow As DataGridViewRow = dgvSubjects.SelectedRows(0)

                    If selectedRow.Cells("id").Value Is Nothing OrElse
                   selectedRow.Cells("Nom").Value Is Nothing OrElse
                   selectedRow.Cells("Heures restantes").Value Is Nothing Then
                        MessageBox.Show("Données incomplètes pour cette matière.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If

                    Using editForm As New Form2()

                        editForm.type = Form2.TypeElement.Subject
                        editForm.SubjectId = CInt(selectedRow.Cells("id").Value)
                        editForm.SubjectName = selectedRow.Cells("Nom").Value.ToString()
                        editForm.HoursLeft = CInt(selectedRow.Cells("Heures restantes").Value)

                        Dim currentProfId As Integer = GetCurrentProfessorId(editForm.SubjectId)
                        editForm.ProfessorId = currentProfId

                        If editForm.ShowDialog() = DialogResult.OK Then
                            EditSubject(editForm.SubjectId,
                                  editForm.SubjectName,
                                  editForm.HoursLeft.ToString(),
                                  editForm.ProfessorId.ToString())
                            LoadItemsDataGridViews()
                        End If
                    End Using

                Case Form2.TypeElement.Professor
                    Dim selectedRow As DataGridViewRow = dgvProfs.SelectedRows(0)
                    If selectedRow.Cells("ID").Value Is Nothing OrElse
                   selectedRow.Cells("Nom").Value Is Nothing OrElse
                   selectedRow.Cells("Téléphone").Value Is Nothing Then
                        MessageBox.Show("Données incomplètes pour cette matière.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                    Using editForm As New Form2()
                        editForm.type = Form2.TypeElement.Professor
                        editForm.ProfID = CInt(selectedRow.Cells("ID").Value)
                        editForm.txtProfName.Text = selectedRow.Cells("Nom").Value.ToString()
                        editForm.txtProfPhone.Text = selectedRow.Cells("Téléphone").Value.ToString()
                        If editForm.ShowDialog() = DialogResult.OK Then
                            EditProfessor(editForm.ProfID,
                                  editForm.txtProfName.Text,
                                  editForm.txtProfPhone.Text)
                            LoadItemsDataGridViews()
                        End If
                    End Using

            End Select


        Catch ex As Exception
            MessageBox.Show($"Erreur lors de la modification : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetCurrentProfessorId(subjectId As Integer) As Integer
        Connexion()
        Try
            Using cmd As New OleDbCommand("SELECT professor_id FROM SUBJECT WHERE id = ?", c)
                cmd.Parameters.AddWithValue("?", subjectId)
                Dim result = cmd.ExecuteScalar()
                Return If(result IsNot Nothing AndAlso Not IsDBNull(result), CInt(result), -1)
            End Using
        Catch ex As Exception
            Return -1
        Finally
            If c.State = ConnectionState.Open Then c.Close()
        End Try
    End Function
    Sub DeleteEdt(id As Integer)
        Try
            Connexion()
            If c.State <> ConnectionState.Open Then
                c.Open()
            End If

            MsgBox($"Emploi du temps à supprimer, ID = {id}")
            Dim sql As New OleDb.OleDbCommand("DELETE FROM emploiDuTemps WHERE [N°] = ?", c)
            sql.Parameters.AddWithValue("?", id)

            Dim result As Integer = sql.ExecuteNonQuery()
            If result > 0 Then
                MsgBox("Emploi du temps supprimé avec succès.")
            Else
                MsgBox("Aucun emploi du temps trouvé avec cet ID.", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox($"Erreur : {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupprimerToolStripMenuItem.Click
        Select Case editType
            Case Form2.TypeElement.EDT
                If DataGridView1.SelectedRows.Count > 0 Then
                    Dim result As DialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cet emploi du temps?",
                                                   "Confirmation",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning)

                    If result = DialogResult.Yes Then
                        Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
                        Dim emploiDuTempsId As Integer = CInt(selectedRow.Cells("ID").Value)

                        DeleteEdt(emploiDuTempsId)
                        ChargerEmploiDuTemps()
                    End If
                End If
            Case Form2.TypeElement.Class
                If dgvClasses.SelectedRows.Count > 0 Then
                    Dim result As DialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cette classe?",
                                                   "Confirmation",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning)

                    If result = DialogResult.Yes Then
                        Dim selectedRow As DataGridViewRow = dgvClasses.SelectedRows(0)
                        Dim classId As Integer = CInt(selectedRow.Cells("ID").Value)

                        DeleteClass(classId)
                        LoadItemsDataGridViews()
                    End If
                End If

            Case Form2.TypeElement.Professor
                If dgvProfs.SelectedRows.Count > 0 Then
                    Dim result As DialogResult = MessageBox.Show("Voulez-vous vraiment supprimer ce professeur?",
                                                   "Confirmation",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning)

                    If result = DialogResult.Yes Then
                        Dim selectedRow As DataGridViewRow = dgvProfs.SelectedRows(0)
                        Dim professorId As Integer = CInt(selectedRow.Cells("ID").Value)

                        DeleteProfessor(professorId)
                        LoadItemsDataGridViews()
                    End If
                End If

            Case Form2.TypeElement.Subject

                If dgvSubjects.SelectedRows.Count > 0 Then
                    Dim result As DialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cette matière?",
                                                           "Confirmation",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Warning)

                    If result = DialogResult.Yes Then
                        Dim selectedRow As DataGridViewRow = dgvSubjects.SelectedRows(0)
                        Dim subjectId As Integer = CInt(selectedRow.Cells("id").Value)

                        DeleteSubject(subjectId)
                        LoadItemsDataGridViews()
                    End If
                End If
        End Select
    End Sub

    Private Sub dgvClasses_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClasses.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = dgvClasses.Rows(e.RowIndex)
        End If
    End Sub

    Private Sub BackFromEditOrDeleteEDTPanel_Click(sender As Object, e As EventArgs) Handles BackFromEditOrDeleteEDTPanel.Click
        DrawPanel(HomePanel)
    End Sub

    Private Sub cbItemToAdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbItemToAdd.SelectedIndexChanged
        ItemToAddConfirmButton.Enabled = True
    End Sub

    Private Sub ExportToExcel()
        Dim xlApp As New Excel.Application
        Dim xlWorkbook As Excel.Workbook = xlApp.Workbooks.Add()
        Dim xlWorksheet As Excel.Worksheet = xlWorkbook.Sheets(1)

        Try
            xlApp.Visible = True ' mettre à True si vous voulez voir Excel s'ouvrir

            For col = 0 To DataGridView1.Columns.Count - 1
                xlWorksheet.Cells(1, col + 1).Value = DataGridView1.Columns(col).HeaderText
            Next

            For row = 0 To DataGridView1.Rows.Count - 1
                For col = 0 To DataGridView1.Columns.Count - 1
                    If DataGridView1.Rows(row).Cells(col).Value IsNot Nothing Then
                        xlWorksheet.Cells(row + 2, col + 1).Value = DataGridView1.Rows(row).Cells(col).Value.ToString()
                    End If
                Next
            Next

            Dim saveDialog As New SaveFileDialog
            saveDialog.Filter = "Fichiers Excel (*.xlsx)|*.xlsx"
            saveDialog.FileName = "emploi_du_temps.xlsx"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                xlWorkbook.SaveAs(saveDialog.FileName)
                MessageBox.Show("Exportation réussie !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur lors de l'exportation : " & ex.Message)
        Finally
            xlWorkbook.Close(False)
            xlApp.Quit()

            Marshal.ReleaseComObject(xlWorksheet)
            Marshal.ReleaseComObject(xlWorkbook)
            Marshal.ReleaseComObject(xlApp)
        End Try
    End Sub
    Sub ExportEDTToExcel(tlp As TableLayoutPanel)
        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add()
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Sheets(1)

        xlApp.Visible = True

        For row = 0 To tlp.RowCount - 1
            For col = 0 To tlp.ColumnCount - 1

                Dim ctrl As Control = tlp.GetControlFromPosition(col, row)
                If ctrl IsNot Nothing AndAlso TypeOf ctrl Is Label Then
                    Dim lbl As Label = CType(ctrl, Label)
                    Dim texte As String = lbl.Text

                    xlWorkSheet.Cells(row + 1, col + 1).Value = texte

                    xlWorkSheet.Cells(row + 1, col + 1).WrapText = True
                    xlWorkSheet.Cells(row + 1, col + 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(row + 1, col + 1).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Cells(row + 1, col + 1).Borders.Weight = Excel.XlBorderWeight.xlThin
                End If
            Next
        Next

        xlWorkSheet.Columns.AutoFit()

        MsgBox("Emploi du temps exporté vers Excel avec succès.", MsgBoxStyle.Information)
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        ExportEDTToExcel(TableLayoutPanel1)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Using signUp As New Inscription()
            If signUp.ShowDialog() = DialogResult.OK Then
                MsgBox("Inscription réussie ! Vous pouvez maintenant vous connecter.", MsgBoxStyle.Information)
            End If
        End Using
    End Sub
End Class

