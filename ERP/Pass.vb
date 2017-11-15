Public Class Pass
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        author()
    End Sub

    Public Sub author()
        Try
            If TextBox1.Text = "R@s3ng4n" Then
                MainForm.btnCreateCon.Enabled = True
                MainForm.btnModifCon.Enabled = True
                MainForm.btnDeleteCon.Enabled = True
                MainForm.Button2.Visible = False
                Close()
            Else
                MainForm.btnCreateCon.Enabled = False
                MainForm.btnModifCon.Enabled = False
                MainForm.btnDeleteCon.Enabled = False
                MainForm.Button2.Visible = True
                Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            author()
        End If
    End Sub
End Class