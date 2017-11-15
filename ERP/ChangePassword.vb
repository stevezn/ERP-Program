Imports System.Data.SQLite

Public Class ChangePassword

    Public user As String
    Public localCon As SQLiteConnection

    Private Sub btLogin_Click(sender As Object, e As EventArgs) Handles btLogin.Click
        If txtPwd.Text = txtRetype.Text Then

            Dim cmd As SQLiteCommand = localCon.CreateCommand()

            cmd.CommandText = "SELECT `password` FROM `users` WHERE `username` = ?"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("username", user)
            Dim pwd As String = cmd.ExecuteScalar()
            Dim auth As Boolean = False

            Dim c As New Crypto.Crypto
            If txtOldPwd.Text = pwd Then
                auth = True
            Else
                Try
                    If c.Verify(txtOldPwd.Text, pwd) Then
                        auth = True
                    End If
                Catch ex As Exception
                    auth = False
                End Try
            End If
            If auth Then
                DialogResult = DialogResult.OK
                Close()
            Else
                MsgBox("Wrong old password")
            End If
        Else
            MsgBox("You have mistyped your new password")
        End If
    End Sub
End Class