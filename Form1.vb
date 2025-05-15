Imports System.Data.OleDb
Imports System.Linq.Expressions
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.Logging

Public Class Form1
    Dim c As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\Élie N'srango\OneDrive\Documentos\gedt.accdb;")
    Function GetClassRooms()
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
    Sub HideChildPanelsExcept(parent As Control, exceptionPanel As Panel)
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

    Public Function HashSHA256(input As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(input)
            Dim hashBytes As Byte() = sha256.ComputeHash(bytes)
            Dim sb As New StringBuilder()
            For Each b As Byte In hashBytes
                sb.Append(b.ToString("x2")) ' Convertir en hexadécimal
            Next
            Return sb.ToString()
        End Using
    End Function
    Function ConflitHoraire() As Boolean
        Connexion()
        Dim cmd As New OleDbCommand("
        SELECT COUNT(*) FROM emploiDuTemps
        WHERE day_id = ?
        AND week_id = ?
        AND creneau_id = ?
        AND (class_id = ? OR subject_id IN (
            SELECT id FROM SUBJECT WHERE professor_id = ?))", c)

        cmd.Parameters.AddWithValue("?", cbDays.SelectedValue)
        cmd.Parameters.AddWithValue("?", cbWeeks.SelectedValue)
        cmd.Parameters.AddWithValue("?", cbCreneaux.SelectedValue)
        cmd.Parameters.AddWithValue("?", cbClasses.SelectedValue)
        cmd.Parameters.AddWithValue("?", cbSubjects.SelectedValue)

        Dim count As Integer = cmd.ExecuteScalar()
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
        sql.Parameters.AddWithValue("@teacher", cbTeacher.SelectedIndex)
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
    Sub LoadComboBoxes()
        If c.State <> ConnectionState.Open Then
            c.Open()
        End If
        Dim dtClasses As New DataTable
        Dim da As New OleDbDataAdapter("SELECT id, class_name FROM CLASS", c)
        da.Fill(dtClasses)
        cbClasses.DataSource = dtClasses
        cbClasses.DisplayMember = "class_name"
        cbClasses.ValueMember = "id"

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
            sql.Parameters.AddWithValue("@password", password_field.Text)
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
        SELECT 
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
        '        WHERE WEEK.week_no = @week_no

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter(requete, c)
        da.Fill(dt)
        DataGridView1.DataSource = dt
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

        ' Préparer le tableau
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
            tlp.RowStyles.Add(New RowStyle(SizeType.Absolute, 64)) ' hauteur fixe
        Next

        ' En-têtes
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

        ' Charger le contenu de l'emploi du temps
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

        ' Placer les données dans la grille
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
    End Sub

    Private Sub DeleteEDTButton_Click(sender As Object, e As EventArgs) Handles deleteEDTButton.Click

    End Sub

    Private Sub ListEDTButton_Click(sender As Object, e As EventArgs) Handles listEDTButton.Click
        LoadEDTInGrid(TableLayoutPanel1, 3, 2)
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
End Class
