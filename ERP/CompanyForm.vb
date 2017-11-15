Public Class CompanyForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex >= 0 Then
            DialogResult = DialogResult.OK
        End If
    End Sub
End Class