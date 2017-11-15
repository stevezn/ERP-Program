Imports System.Data.SQLite
Imports System.Windows.Forms

Public Class Login
    Public forManual As Boolean = False
    Public uc As Object

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmd As SQLiteCommand

        Dim obj As Object
        If Not IsNothing(uc) Then
            obj = uc
        Else
            obj = Owner
        End If

        If obj.localCon.State = ConnectionState.Open Then
            cmd = obj.localCon.CreateCommand
            If forManual Then
                cmd.CommandText = "SELECT COUNT(username) FROM `users` WHERE `username` = ? AND `password` = ? AND `super` = 1"
            Else
                cmd.CommandText = "SELECT COUNT(username) FROM `users` WHERE `username` = ? AND `password` = ?"
            End If
            cmd.Parameters.AddWithValue("username", UCase(TextBox1.Text))
            cmd.Parameters.AddWithValue("password", TextBox2.Text)
            Dim found = cmd.ExecuteScalar()

            If CInt(found) = 1 Then
                DialogResult = DialogResult.OK
            Else
                If forManual Then
                    MsgBox("Password salah, atau User tidak bisa input manual")
                Else
                    MsgBox("Password salah")
                End If
            End If
        End If
    End Sub

    Private Sub Login_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        TextBox1.Focus()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class