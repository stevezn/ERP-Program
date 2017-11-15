Imports Microsoft.Office.Interop.Excel
Imports System.IO
Imports System.Drawing.Printing
Imports RawPrinter

Public Class ReportMain : Implements IDisposable

    Private xl As New Application
    Private books As Workbooks = xl.Workbooks
    Private book As Workbook
    Private sheet As Worksheet
    Private tplsheet As Worksheet

    Public papersize As PaperSize
    Public ds As DataSet
    Public repdata As List(Of Dictionary(Of String, Object))

    Public Sub openTemplate(fname As String)
        If File.Exists(fname) Then
            books.Open(fname)
            If books.Count > 0 Then
                book = books(1)
            Else
                book = books.Add
            End If
            findLayout()
        Else
            Throw New FileNotFoundException("File was not found")
        End If
    End Sub

    Public Sub findLayout()
        For Each ws As Worksheet In book.Worksheets
            If ws.Name = "Layout" Then
                sheet = ws
                Exit Sub
            End If
        Next
        sheet = book.Sheets(1)
    End Sub

    Public Function templateExists() As Boolean
        templateExists = False
        For Each ws As Worksheet In book.Worksheets
            If ws.Name = "Template" Then
                tplsheet = ws
                templateExists = True
                Exit Function
            End If
        Next
    End Function

    Public Function getLastRow(sh As Worksheet) As Integer
        Return sh.UsedRange.Row + sh.UsedRange.Rows.Count - 1
    End Function

    Public Function xlColName(colnum As Integer) As String
        Dim modulo As Integer

        xlColName = ""
        While colnum > 0
            modulo = (colnum - 1) Mod 26
            xlColName = Chr(modulo + 65) + xlColName
            colnum = CInt((colnum - modulo) / 26)
        End While
    End Function

    Public Function xlColNum(colname As String) As Integer
        Dim i As Integer = 0

        xlColNum = 0
        colname = UCase(colname)

        While colname.Length > 0
            xlColNum = xlColNum * (i * 26) + (Asc(colname(0)) - 64)
            i = i + 1
            colname = Mid(colname, 2)
        End While
    End Function

    Public Function generateReport() As Boolean
        Dim rowStart As Integer
        Dim rowEnd As Integer
        Dim field As String
        Dim col As String
        Dim row As Integer
        Dim numcol As Integer
        Dim dtype As String
        Dim repeat As New Dictionary(Of Integer, Dictionary(Of String, String))
        Dim header(2) As Integer
        Dim subtotal As Boolean = False
        Dim groupcol As Integer
        Dim sumcols As New List(Of Integer)
        Dim sortcols As New List(Of String)
        Dim fitcols As New List(Of Integer)
        Dim leftmostcol As Integer = 9999
        Dim rightmostcol As Integer = 0
        Dim topmostrow As Long = 999999
        Dim bottommostrow As Long = 0
        Dim l As Dictionary(Of String, String)

        generateReport = False

        rowStart = tplsheet.UsedRange.Row
        rowEnd = getLastRow(tplsheet)
        header(0) = 0
        header(1) = 0

        For i = rowStart To rowEnd
            field = tplsheet.Cells(i, 1).Value
            dtype = tplsheet.Cells(i, 2).Value
            col = tplsheet.Cells(i, 3).Value
            numcol = xlColNum(col)
            row = tplsheet.Cells(i, 4).Value
            Select Case dtype
                Case "repeat"
                    If rightmostcol < numcol Then rightmostcol = numcol
                    If leftmostcol > numcol Then leftmostcol = numcol
                    If topmostrow > row Then topmostrow = row
                    If repeat.ContainsKey(row) Then
                        l = repeat(row)
                        l.Add(col, field)
                    Else
                        l = New Dictionary(Of String, String)
                        l.Add(col, field)
                        repeat.Add(row, l)
                    End If
                Case "single"
                    If IsNothing(ds) And Not IsNothing(repdata) Then
                        If repdata.Count > 0 Then
                            If repdata(0).ContainsKey(field) Then
                                sheet.Cells(row, col).Value = repdata(0)(field)
                            Else
                                sheet.Cells(row, col).Value = ""
                            End If
                        End If
                    Else
                        If ds.Tables(0).Rows.Count > 0 Then
                            sheet.Cells(row, col).Value = ds.Tables(0).Rows(0).Item(field)
                        End If
                    End If
                Case "keyword"
                    Select Case field
                        Case "today"
                            sheet.Cells(row, col).Value = "=TODAY()"
                        Case "now"
                            sheet.Cells(row, col).Value = "=NOW()"
                    End Select
                Case "header"
                    Select Case field
                        Case "start"
                            header(0) = row
                        Case "end"
                            header(1) = row
                    End Select
                Case "subtotal"
                    subtotal = True
                Case "groupcol"
                    groupcol = col
                Case "sumcols"
                    sumcols.Add(col)
                Case "sortcols"
                    sortcols.Add(col)
                Case "autofit"
                    fitcols.Add(col)
            End Select
        Next

        If header(0) > 0 Or header(1) > 0 Then
            If header(0) = 0 Then header(0) = 1
            If header(1) = 0 Then header(1) = header(0)
            sheet.PageSetup.PrintTitleRows = "$" + header(0).ToString() + ":$" + header(1).ToString()
        End If

        Dim j = 0
        For Each c In repeat
            sheet.Rows(c.Key).Value = ""
            If bottommostrow < c.Key Then bottommostrow = c.Key
            If IsNothing(ds) And Not IsNothing(repdata) Then
                For Each drow In repdata
                    For Each d In c.Value
                        If d.Value.IndexOf("=") = 0 Then
                            Dim formula As String = d.Value.Replace("[row]", (c.Key + j).ToString())
                            sheet.Cells(c.Key + j, d.Key).value = formula
                        ElseIf IsDBNull(drow(d.Value)) And subtotal Then
                            sheet.Cells(c.Key + j, d.Key).value = "'-"
                        Else
                            sheet.Cells(c.Key + j, d.Key).value = drow(d.Value)
                        End If
                    Next
                    bottommostrow = c.Key + j
                    j = j + 1
                    sheet.Cells(c.Key + j, 1).EntireRow.Insert()
                Next
            Else
                For Each drow In ds.Tables(0).Rows
                    For Each d In c.Value
                        If IsDBNull(drow.Item(d.Value)) And subtotal Then
                            sheet.Cells(c.Key + j, d.Key).value = "'-"
                        Else
                            sheet.Cells(c.Key + j, d.Key).value = drow.Item(d.Value)
                        End If
                    Next
                    bottommostrow = c.Key + j
                    j = j + 1
                    sheet.Cells(c.Key + j, 1).EntireRow.Insert()
                Next
            End If
        Next

        Dim r As String = xlColName(leftmostcol) + (topmostrow - 1).ToString() + ":" + xlColName(rightmostcol) + bottommostrow.ToString()
        Dim fromBottom As Integer = getLastRow(sheet) - bottommostrow

        If subtotal And groupcol > 0 And sumcols.Count > 0 Then
            Dim rSort As String

            sheet.Activate()
            sheet.AutoFilterMode = False

            Try
                sheet.Range(r).AutoFilter(If(groupcol = 0, 1, groupcol))
            Catch ex As Exception
                sheet.Cells(leftmostcol + (topmostrow - 1).ToString()).Autofilter()
            End Try

            If bottommostrow - topmostrow > 0 Then
                With sheet.AutoFilter.Sort
                    .SortFields.Clear()
                    For Each c In sortcols
                        rSort = c + (topmostrow - 1).ToString() + ":" + c + bottommostrow.ToString()
                        .SortFields.Add(Key:=sheet.Range(rSort), SortOn:=XlSortOn.xlSortOnValues, Order:=XlSortOrder.xlAscending, DataOption:=XlSortDataOption.xlSortNormal)
                    Next
                    .Header = XlYesNoGuess.xlYes
                    .MatchCase = False
                    .Orientation = Constants.xlTopToBottom
                    .SortMethod = XlSortMethod.xlPinYin
                    .Apply()
                End With

                sheet.Range(r).Subtotal(GroupBy:=groupcol, Function:=XlConsolidationFunction.xlSum, TotalList:=sumcols.ToArray(),
                        Replace:=True, PageBreaks:=False, SummaryBelowData:=True)

                bottommostrow = getLastRow(sheet) - fromBottom
                With sheet
                    For k = topmostrow To bottommostrow
                        Dim dump As String = .Cells(k, groupcol).Value2
                        If Right(.Cells(k, groupcol).Value2, 5) = "Total" Then
                            .Rows(k).RowHeight = .Rows(k).RowHeight * 2
                            .Rows(k).VerticalAlignment = XlVAlign.xlVAlignTop
                            .Rows(k).Font.Bold = True
                            With .Range(xlColName(leftmostcol) + k.ToString + ":" + xlColName(rightmostcol) + k.ToString).Borders(XlBordersIndex.xlEdgeTop)
                                .LineStyle = XlLineStyle.xlContinuous
                                .ColorIndex = 0
                                .TintAndShade = 0
                                .Weight = XlBorderWeight.xlThin
                            End With
                            If groupcol > 4 Or .Columns(groupcol).Hidden = True Then
                                .Cells(k, 1).Value = .Cells(k, groupcol).Value2
                            End If
                        End If
                    Next

                End With
            End If
        End If

        For Each c In fitcols
            sheet.Columns(c).AutoFit()
        Next

        r = "$" + xlColName(sheet.UsedRange.Column) + sheet.UsedRange.Row.ToString() + ":$" + xlColName(sheet.UsedRange.Column + sheet.UsedRange.Columns.Count - 1) + getLastRow(sheet).ToString

        sheet.PageSetup.PrintArea = r
        sheet.PageSetup.FitToPagesWide = 1

        generateReport = True
    End Function

    Public Sub print()
        If Me.papersize IsNot Nothing Then
            Dim pd As New PrintDocument
            Try
                pd.DefaultPageSettings.PaperSize = Me.papersize
                sheet.PageSetup.PaperSize = Me.papersize.RawKind
            Catch ex As Exception
                Printer.AddCustomPaperSizeToDefaultPrinter("FAKTUR", 215, 140)
                For Each ps As PaperSize In pd.PrinterSettings.PaperSizes
                    If ps.PaperName = "FAKTUR" Then
                        Me.papersize = ps
                        pd.DefaultPageSettings.PaperSize = Me.papersize
                        sheet.PageSetup.PaperSize = Me.papersize.RawKind
                        Exit For
                    End If
                Next
            End Try
        End If
        sheet.PrintOutEx()
    End Sub

    Public Sub preview()
        xl.Visible = True
        If papersize IsNot Nothing Then
            Dim pd As New PrintDocument
            pd.DefaultPageSettings.PaperSize = papersize
            sheet.PageSetup.PaperSize = papersize.RawKind
        End If
        sheet.PrintPreview()
        xl.Visible = False
    End Sub

    Public Function SaveReport() As String
        Dim savedlg As New SaveFileDialog
        savedlg.Filter = "Excel file|*.xlsx"
        If savedlg.ShowDialog = DialogResult.OK Then
            If File.Exists(savedlg.FileName) Then
                File.Delete(savedlg.FileName)
            End If
            While book.Worksheets.Count > 1
                If book.Worksheets(1).Name = sheet.Name Then
                    book.Worksheets(book.Worksheets.Count).Delete()
                ElseIf book.Worksheets(book.Worksheets.Count).Name = sheet.Name Then
                    book.Worksheets(1).Delete()
                End If
            End While
            book.SaveAs(savedlg.FileName)
            Return savedlg.FileName
        End If
        Return ""
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                book.Close(False, Reflection.Missing.Value, Reflection.Missing.Value)
                xl.Quit()
            End If

        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
#End Region

End Class
