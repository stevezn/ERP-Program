Imports System.Windows.Forms
Imports DB
Imports ERPModules

Public Class OrderLot : Inherits ERPModule

    Private lotBuffer As New List(Of Dictionary(Of String, Object))

    Public injectAscend As Boolean = False
    Public ascendConStr As String
    Private ascendCon As DBConn

    Private doEvent As Boolean = False

    Public Overrides Sub Print()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub Save()
        If tbMaxOrder.Value = 0 Then
            MsgBox("Please input the maximum order value." + vbCrLf + "(OL-S-01)")
            Exit Sub
        End If
        saveLot()
        clearForm()
        loadLots()
    End Sub

    Public Overloads Sub Dispose()
        Try
            toolMenu.Clear()
            toolButton.Clear()
            lotBuffer.Clear()

            If Not IsNothing(ascendCon) Then
                ascendCon.Dispose()
            End If

            MyBase.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Reload()
        loadLots()
    End Sub

    Private Sub clearForm()
        tbMaxOrder.Value = 0
    End Sub

    Private Function getUsedLot() As List(Of Dictionary(Of String, Object))
        Dim sql As String
        Dim rdr As Object
        Dim cmdRdr As Object

        getUsedLot = New List(Of Dictionary(Of String, Object))

        If injectAscend Then
            If IsNothing(ascendCon) Then
                ascendCon = New DBConn
                ascendCon.init("MSSQL", ascendConStr)
                ascendCon.open()
            End If

            dbCon.SQL("select query from sqls where [transaction] = 'ascend_so_lot_usage'")
            sql = dbCon.scalar()

            dbCon.SQL("select query from sqls where [transaction] = 'ascend_insert_tmp_lot'")
            Dim sql2 As String = dbCon.scalar()

            dbCon.SQL("truncate table ascend_existing_lot")
            dbCon.execute()

            cmdRdr = ascendCon.SQLReader(sql)
            ascendCon.addParameter("@d1", DateValue(dpFilterFrom.Value), cmdRdr)
            ascendCon.addParameter("@d2", DateValue(dpFilterTo.Value), cmdRdr)
            ascendCon.addParameter("@exclude_so", "", cmdRdr)

            Dim row As Dictionary(Of String, Object)

            rdr = ascendCon.beginRead(cmdRdr)
            While ascendCon.doRead(rdr)
                row = ascendCon.getRow(rdr)
                dbCon.SQL(sql2)
                dbCon.addParameter("@ld", row("lot_date"))
                dbCon.addParameter("@el", row("qty"))
                dbCon.execute()
            End While
            ascendCon.endRead(rdr)

            dbCon.SQL("select query from sqls where [transaction] = 'ascend_lot_summary'")
            sql = dbCon.scalar()
        Else
            dbCon.SQL("select query from sqls where [transaction] = 'so_lot_summary'")
            ascendCon.addParameter("@exclude_so", "")
            sql = dbCon.scalar()
        End If

        cmdRdr = dbCon.SQLReader(sql)
        dbCon.addParameter("@ld1", DateValue(dpFilterFrom.Value), cmdRdr)
        dbCon.addParameter("@ld2", DateValue(dpFilterTo.Value), cmdRdr)

        lotBuffer.Clear()

        rdr = dbCon.beginRead(cmdRdr)
        While dbCon.doRead(rdr)
            lotBuffer.Add(dbCon.getRow(rdr))
        End While
        dbCon.endRead(rdr)
    End Function

    Private Sub saveLot()
        If dbCon.state() = ConnectionState.Open Then
            Try
                Dim d As Integer = Math.Abs(DateValue(dpLot.Value).Subtract(DateValue(dpLot2.Value)).Days)
                For i = 0 To d
                    dbCon.SQL("insert into sales_order_lots(company_code, lot_date, max_order) values(@cc, @ld, @mo)")
                    dbCon.addParameter("@cc", corp)
                    dbCon.addParameter("@ld", DateValue(dpLot.Value.AddDays(i)))
                    dbCon.addParameter("@mo", tbMaxOrder.Value)
                    dbCon.execute()
                Next
            Catch ex As Exception
                MsgBox(ex.Message + vbCrLf + "(OL-SL-02)")
            End Try
        Else
            MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OL-SL-01)")
        End If
    End Sub

    Private Sub updateLot()
        If dbCon.state() = ConnectionState.Open Then
            Try
                Dim d As Integer = Math.Abs(DateValue(dpLot.Value).Subtract(DateValue(dpLot2.Value)).Days)
                For i = 0 To d
                    dbCon.SQL("update sales_order_lots set max_order = @mo where company_code = @cc and lot_date = @ld")
                    dbCon.addParameter("@mo", tbMaxOrder.Value)
                    dbCon.addParameter("@cc", corp)
                    dbCon.addParameter("@ld", DateValue(dpLot.Value.AddDays(i)))
                    dbCon.execute()
                Next
            Catch ex As Exception
                MsgBox(ex.Message + vbCrLf + "(OL-UL-02)")
            End Try
        Else
            MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OL-UL-01)")
        End If
    End Sub

    Private Sub loadLots()
        If dbCon.state() = ConnectionState.Open Then
            getUsedLot()

            If lstLot.VirtualListSize = lotBuffer.Count Then
                lstLot.Invalidate()
            Else
                lstLot.VirtualListSize = lotBuffer.Count
            End If
        Else
            MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OL-LL-01)")
        End If
    End Sub

    Private Sub OrderLot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim btn As ERPModules.buttonState

        _parent = FindForm()

        btn = New ERPModules.buttonState()
        btn.visible = False
        btn.enabled = False
        toolButton.Add("New", btn)

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = False
        toolButton.Add("Edit", btn)

        btn = New ERPModules.buttonState()
        btn.visible = False
        btn.enabled = False
        toolButton.Add("Save", btn)

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = False
        toolButton.Add("Delete", btn)

        btn = New ERPModules.buttonState()
        btn.visible = False
        btn.enabled = False
        toolButton.Add("Print", btn)

        dpFilterTo.Value = Now.AddMonths(1)

        'lbUOM.Text = "Lembar"

        tbMaxOrder.Maximum = Decimal.MaxValue

        lstLot.VirtualMode = True

        loadLots()
    End Sub

    Private Sub lstLot_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstLot.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = lotBuffer(e.ItemIndex)
        e.Item = New ListViewItem(DirectCast(row("lot_date"), Date))
        e.Item.SubItems.Add(String.Format(numformat, row("max_order")))
        e.Item.SubItems.Add(String.Format(numformat, If(row("existing_order"), 0)))
        e.Item.SubItems.Add(String.Format(numformat, row("max_order") - row("existing_order")))
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Save()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        loadLots()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        updateLot()
        loadLots()
    End Sub

    Private Sub lstLot_DoubleClick(sender As Object, e As EventArgs) Handles lstLot.DoubleClick
        If lstLot.SelectedIndices.Count = 1 Then
            Dim item As ListViewItem = lstLot.Items(lstLot.SelectedIndices(0))
            Dim lot As Dictionary(Of String, Object) = lotBuffer(item.Index)
            dpLot.Value = lot("lot_date")
            dpLot2.Value = lot("lot_date")
        End If
    End Sub
End Class
