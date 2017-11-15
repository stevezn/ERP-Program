Imports RawPrinter
Imports System.Drawing.Printing

Public Class Form1

    Dim barcode_printer As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim label As String

        If barcode_printer = "" Then
            Dim pd As New PrintDialog()
            pd.PrinterSettings = New PrinterSettings()
            pd.UseEXDialog = True
            If (pd.ShowDialog() = DialogResult.OK) Then
                barcode_printer = pd.PrinterSettings.PrinterName
            Else
                Exit Sub
            End If
        End If

        Dim ctr As Integer = 0
        Dim num As String

        For ctr = 0 To labelCount.Value - 1

            label = txtTemplate.Text

            num = (ctr * stepA.Value + startA.Value).ToString()
            label = label.Replace("[A]", txtA.Text + num.PadLeft(numDigit.Value, "0"))

            num = (ctr * stepB.Value + startB.Value).ToString()
            label = label.Replace("[B]", txtB.Text + num.PadLeft(numDigit.Value, "0"))

            num = (ctr * stepC.Value + startC.Value).ToString()
            label = label.Replace("[C]", txtC.Text + num.PadLeft(numDigit.Value, "0"))

            num = (ctr * stepD.Value + startD.Value).ToString()
            label = label.Replace("[D]", txtD.Text + num.PadLeft(numDigit.Value, "0"))

            Printer.SendStringToPrinter(barcode_printer, label)
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
