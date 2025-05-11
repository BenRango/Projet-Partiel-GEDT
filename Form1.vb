Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Text

Public Class Form1
    Dim c As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\Élie N'srango\OneDrive\Documentos\gedt.accdb;")
    Sub Connexion()
        Try
            c.Open()
            MsgBox("Connection effectuée")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub RoundButtonCorners(btn As Button, radius As Integer)
        ' Configuration du bouton pour un meilleur rendu
        btn.FlatStyle = FlatStyle.Flat
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
    Public Function HashSHA256(input As String) As String
        Using sha256 As SHA256 = sha256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(input)
            Dim hashBytes As Byte() = sha256.ComputeHash(bytes)
            Dim sb As New StringBuilder()
            For Each b As Byte In hashBytes
                sb.Append(b.ToString("x2")) ' Convertir en hexadécimal
            Next
            Return sb.ToString()
        End Using
    End Function
    Private Sub login_button_Click(sender As Object, e As EventArgs) Handles login_button.Click
        Try
            Connexion()

            Dim sql As New OleDb.OleDbCommand("SELECT * FROM USERS WHERE Trim(username) = @username AND password = @password", c)
            sql.Parameters.AddWithValue("@username", username_field.Text)
            sql.Parameters.AddWithValue("@password", password_field.Text)
            Dim log As String = "Requête : " & sql.CommandText & vbCrLf
            For Each p As OleDbParameter In sql.Parameters
                log &= $"{p.ParameterName} = {p.Value}" & vbCrLf
            Next
            MsgBox(log) ' ou Debug.WriteLine(log)
            Dim reader As OleDbDataReader = sql.ExecuteReader()

            If reader.HasRows Then
                MsgBox("Connexion réussie !")
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
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RoundButtonCorners(login_button, 25)

        ' Ou pour arrondir tous les boutons automatiquement :
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Button Then
                RoundButtonCorners(DirectCast(ctrl, Button), 15)
            End If
        Next
    End Sub


End Class
