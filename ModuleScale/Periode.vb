Imports System.Windows.Forms

Public Class Periode
    Private Sub rb1day_CheckedChanged(sender As Object, e As EventArgs) Handles rb1day.CheckedChanged
        If rb1day.Checked Then
            Label1.Text = "Tanggal"
            Label2.Visible = False
            dpAkhir.Visible = False
        Else
            Label1.Text = "Awal"
            Label2.Visible = True
            dpAkhir.Visible = True
        End If
    End Sub

    Private Sub rbPeriode_CheckedChanged(sender As Object, e As EventArgs) Handles rbPeriode.CheckedChanged
        If rb1day.Checked Then
            Label1.Text = "Tanggal"
            Label2.Visible = False
            dpAkhir.Visible = False
        Else
            Label1.Text = "Awal"
            Label2.Visible = True
            dpAkhir.Visible = True
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If cbShift.Visible And cbShift.SelectedIndex < 0 Then
            DialogResult = DialogResult.None
        End If
        If cbLine.Visible And cbLine.SelectedIndex < 0 Then
            DialogResult = DialogResult.None
        End If
    End Sub

    Private Sub Periode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class