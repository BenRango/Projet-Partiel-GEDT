Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Text

Public Class Inscription
    Dim c As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=..\..\gedt.accdb;")
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
    Sub SignUp()
        If tbFirstNameField.Text = "" Or tbLastNameField.Text = "" Or tbUsernameField.Text = "" Or tbPasswordField.Text = "" Then
            MessageBox.Show("Veuillez remplir tous les champs.")
            Return
        End If
        c.Open()
        Dim sql As String = "INSERT INTO USERS (user_first_name, user_last_name, username, [password], role) " &
                    "VALUES (@firstName, @lastName, @username, @password, @role)"
        Dim cmd As New OleDbCommand(sql, c)

        cmd.Parameters.AddWithValue("@firstName", tbFirstNameField.Text)
        cmd.Parameters.AddWithValue("@lastName", tbLastNameField.Text)
        cmd.Parameters.AddWithValue("@username", tbUsernameField.Text)
        cmd.Parameters.AddWithValue("@password", HashSHA256(tbPasswordField.Text))
        cmd.Parameters.AddWithValue("@role", cbRoleSelector.Text) ' SelectedItem.ToString()

        Dim result As Integer = cmd.ExecuteNonQuery()

        If result > 0 Then
            MessageBox.Show("Inscription réussie !")
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("Erreur lors de l'inscription.")
        End If
        c.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SignUp()
    End Sub

End Class