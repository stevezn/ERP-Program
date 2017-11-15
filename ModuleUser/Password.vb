Imports System.Windows.Forms

Public Class Password
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If tbNewPasswd.Text = tbCfmPasswd.Text Then
            DialogResult = DialogResult.OK
        Else
            MsgBox("Password does not match")
        End If
    End Sub
End Class