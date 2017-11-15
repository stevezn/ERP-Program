Imports System.Windows.Forms
Imports System.Drawing
Imports System.Globalization
'Imports System.Web.Script.Serialization
Imports DB
Imports ERPModules
Imports System.Net
Imports System.Threading

Public Class OrderControl : Inherits ERPModule
    Public Class loadThread
        Private dbcon As DBConn
        Private mainThd As Object
        Private mainForm As Object
        Private month As Date = Nothing

        Private cust_code As String
        Private filter As String

        Private Delegate Sub progBarNotify()
        Private Delegate Sub progBarUpdate(dt As DataTable, events As Boolean)

        Private mNotifyStart As progBarNotify
        Private mNotifyRun As progBarNotify
        Private mNotifyEnd As progBarNotify

        Private modNotifyEnd As progBarNotify
        Private modNotifyLoad As progBarUpdate

        Public Sub New(ByRef MainWindow As Object, ByRef main As Object, ByRef db As DBConn)
            mainForm = MainWindow
            dbcon = db
            mainThd = main
            month = Nothing
            filter = ""

            mNotifyStart = AddressOf MainWindow.progBarNotifyStart
            mNotifyRun = AddressOf MainWindow.progBarNotifyRun
            mNotifyEnd = AddressOf MainWindow.progBarNotifyEnd

            modNotifyEnd = AddressOf mainThd.progBarNotifyEnd
            modNotifyLoad = AddressOf mainThd.progBarNotifyUpdate
        End Sub

        Public Sub New(ByRef MainWindow As Object, ByRef main As Object, ByRef db As DBConn, filter As String)
            mainForm = MainWindow
            dbcon = db
            mainThd = main
            month = Nothing
            Me.filter = filter

            mNotifyStart = AddressOf MainWindow.progBarNotifyStart
            mNotifyRun = AddressOf MainWindow.progBarNotifyRun
            mNotifyEnd = AddressOf MainWindow.progBarNotifyEnd

            modNotifyEnd = AddressOf mainThd.progBarNotifyEnd
            modNotifyLoad = AddressOf mainThd.progBarNotifyUpdate
        End Sub

        Public Sub New(ByRef MainWindow As Object, ByRef main As Object, ByRef db As DBConn, month As Date)
            mainForm = MainWindow
            dbcon = db
            mainThd = main
            Me.month = month
            filter = "month"

            mNotifyStart = AddressOf MainWindow.progBarNotifyStart
            mNotifyRun = AddressOf MainWindow.progBarNotifyRun
            mNotifyEnd = AddressOf MainWindow.progBarNotifyEnd

            modNotifyEnd = AddressOf mainThd.progBarNotifyEnd
            modNotifyLoad = AddressOf mainThd.progBarNotifyUpdate
        End Sub

        Public Sub New(ByRef MainWindow As Object, ByRef main As Object, ByRef db As DBConn, cust_code As String, filter As String)
            mainForm = MainWindow
            dbcon = db
            mainThd = main
            Me.cust_code = cust_code
            Me.filter = filter

            mNotifyStart = AddressOf MainWindow.progBarNotifyStart
            mNotifyRun = AddressOf MainWindow.progBarNotifyRun
            mNotifyEnd = AddressOf MainWindow.progBarNotifyEnd

            modNotifyEnd = AddressOf mainThd.progBarNotifyEnd
            modNotifyLoad = AddressOf mainThd.progBarNotifyUpdate
        End Sub

        Public Sub loadUnbindItems()
            If dbcon.state() = ConnectionState.Open Then
                'While dbcon.isReading
                '    Thread.Sleep(200)
                'End While

                Dim ctr As Integer = 0
                Dim rdr As Object

                Dim cmdRdr As Object = dbcon.SQLReader("select i.*, isnull(ic.min_order, 2000) [min_order], isnull(ic.price, 0) [price], isnull(ic.currency_code, 'IDR') [currency_code] 
                        from view_items i left join item_customers ic on i.company_code = ic.company_code and i.item_code = ic.item_code and ic.del = 0
                        where i.company_code = @cc and ic.customer_code is null and i.sellable = 1")
                dbcon.addParameter("@cc", mainThd.corp, cmdRdr)

                rdr = dbcon.beginRead(cmdRdr)
                mainThd.itemUnbindBuffer.Clear()

                mainForm.Invoke(mNotifyStart)

                While dbcon.doRead(rdr)
                    mainThd.itemUnbindBuffer.Add(dbcon.getRow(rdr))
                    If ctr = 25 Then
                        mainForm.Invoke(mNotifyRun)
                        ctr = 0
                    End If
                    ctr += 1
                End While

                dbcon.endRead(rdr)

                mainForm.Invoke(mNotifyEnd)
            Else
                MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LUI-01)")
            End If
        End Sub

        Public Sub loadItems()
            If dbcon.state() = ConnectionState.Open Then
                'While dbcon.isReading
                '    Thread.Sleep(200)
                'End While

                Dim ctr As Integer = 0
                Dim rdr As Object

                Dim cmdRdr As Object = dbcon.SQLReader("select i.*, isnull(ic.min_order, 2000) [min_order], isnull(ic.price, 0) [price], isnull(ic.currency_code, 'IDR') [currency_code] 
                        from view_items i left join item_customers ic on i.company_code = ic.company_code and i.item_code = ic.item_code and ic.del = 0
                        where i.company_code = @cc and ic.customer_code = @cust and i.sellable = 1" + If(filter <> "", " and i.item_name like @filter", ""))
                dbcon.addParameter("@cc", mainThd.corp, cmdRdr)
                dbcon.addParameter("@cust", cust_code, cmdRdr)
                If filter <> "" Then
                    dbcon.addParameter("@filter", "%" + filter + "%", cmdRdr)
                End If

                rdr = dbcon.beginRead(cmdRdr)
                mainThd.itemBuffer.Clear()
                mainThd.itemBuffer.AddRange(mainThd.itemUnbindBuffer)
                mainThd.itemSearch.Clear()

                mainForm.Invoke(mNotifyStart)

                While dbcon.doRead(rdr)
                    mainThd.itemBuffer.Add(dbcon.getRow(rdr))
                    If ctr = 25 Then
                        mainForm.Invoke(mNotifyRun)
                        ctr = 0
                    End If
                    ctr += 1
                End While

                dbcon.endRead(rdr)

                mainForm.Invoke(mNotifyEnd)

                mainThd.itemSearch.AddRange(mainThd.itemBuffer)
                mainThd.Invoke(modNotifyEnd)
            Else
                MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LI-01)")
            End If
        End Sub

        Public Sub loadSO()
            If dbcon.state() = ConnectionState.Open Then
                'While dbcon.isReading
                '    Thread.Sleep(200)
                'End While

                Dim ctr As Integer = 0
                Dim rdr As Object
                Dim cmdRdr As Object

                Try
                    If filter <> "" Then
                        cmdRdr = dbcon.SQLReader("select so.* from view_sales_orders so left join business_areas ba on ba.business_code = @bc
                            where so.company_code = @cc and (so.business_code = @bc or ba.is_company_wide = 1) and left(replace(convert(varchar, so.so_date, 111), '/', '-'), 7) = @mth")
                        dbcon.addParameter("@cc", mainThd.corp, cmdRdr)
                        dbcon.addParameter("@bc", mainThd.area, cmdRdr)
                        dbcon.addParameter("@mth", month.ToString("yyyy-MM", CultureInfo.InvariantCulture), cmdRdr)
                    Else
                        cmdRdr = dbcon.SQLReader("select * from view_sales_orders where company_code = @cc")
                        dbcon.addParameter("@cc", mainThd.corp, cmdRdr)
                    End If

                    rdr = dbcon.beginRead(cmdRdr)

                    mainThd.SOBuffer.Clear()
                    mainThd.SOSearch.Clear()

                    mainForm.Invoke(mNotifyStart)

                    Dim r As New Dictionary(Of String, Object)

                    Try
                        While dbcon.doRead(rdr)
                            r = dbcon.getRow(rdr)
                            mainThd.SOBuffer.Add(r)
                            If ctr = 25 Then
                                mainForm.Invoke(mNotifyRun)
                                ctr = 0
                            End If
                            ctr += 1
                        End While
                    Catch ex2 As Exception
                        MsgBox(ex2.Message + "." + vbCrLf + "(OC-LSO-03)")
                    End Try

                    dbcon.endRead(rdr)

                    mainForm.Invoke(mNotifyEnd)

                    mainThd.SOSearch.AddRange(mainThd.SOBuffer)
                    mainThd.Invoke(modNotifyEnd)
                Catch ex As Exception
                    MsgBox(ex.Message + "." + vbCrLf + "(OC-LSO-02)")
                End Try
            Else
                MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LSO-01)")
            End If
        End Sub

        Public Sub loadSOTypes()
            Dim dt As New DataTable

            If dbcon.state() = ConnectionState.Open Then
                If filter = "" Then
                    dbcon.SQL("select description, code, lead_time, lead_time_unit from sales_order_types where company_code = @cc")
                    dbcon.addParameter("@cc", mainThd.corp)
                Else
                    dbcon.SQL("select sot.description, sot.code, sot.lead_time, sot.lead_time_unit from sales_order_types sot 
                            join customer_type_so_types ctsot on sot.company_code = ctsot.company_code and sot.code = ctsot.so_type where sot.company_code = @cc and ctsot.customer_type = @ct")
                    dbcon.addParameter("@cc", mainThd.corp)
                    dbcon.addParameter("@ct", filter)
                End If
                dbcon.fillTable(dt)

                Dim args(1) As Object
                args(0) = dt
                args(1) = False
                mainThd.Invoke(modNotifyLoad, args)
            Else
                MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LSOT-01)")
            End If
        End Sub

    End Class

    Public Sub progBarNotifyEnd()
        lstSO.VirtualListSize = SOSearch.Count()
        lstSO.SelectedIndices.Clear()

        lstItemSearch.VirtualListSize = itemSearch.Count()
        lstItemSearch.SelectedIndices.Clear()
    End Sub

    Public Sub progBarNotifyUpdate(dt As DataTable, events As Boolean)
        Dim tmp As Boolean = doEvent
        Dim bs As BindingSource

        doEvent = events
        bs = New BindingSource(dt, Nothing)

        cbSOType.DataSource = bs
        cbSOType.DisplayMember = "code"
        cbSOType.ValueMember = "code"
        doEvent = tmp
    End Sub

    Public SOBuffer As New List(Of Dictionary(Of String, Object))
    Public SOSearch As New List(Of Dictionary(Of String, Object))

    Private SOItemBuffer As New List(Of Dictionary(Of String, Object))

    Private custBuffer As New List(Of Dictionary(Of String, Object))
    Private custSearch As New List(Of Dictionary(Of String, Object))

    Private expdBuffer As New List(Of Dictionary(Of String, Object))
    Private expdSearch As New List(Of Dictionary(Of String, Object))

    Public itemBuffer As New List(Of Dictionary(Of String, Object))
    Public itemUnbindBuffer As New List(Of Dictionary(Of String, Object))
    Public itemSearch As New List(Of Dictionary(Of String, Object))

    Private lotBuffer As New List(Of Dictionary(Of String, Object))

    Public injectAscend As Boolean = False
    Public ascendConStr As String
    Private ascendCon As DBConn

    Private _cust_code As String = ""
    Private _expd_code As String = ""
    Private _expd_ascend_id As Integer = 0
    Private _credit_limit As Decimal = 0D
    Private _on_credit As New Dictionary(Of String, Object)
    Private _current_credit As Decimal = 0D
    Private _max_lot As Decimal = 0D
    Private _used_lot As Decimal = 0D
    Private _tot_qty As Decimal = 0D

    Private needApproval As Boolean = False
    Private approveMails As New List(Of Dictionary(Of String, Object))

    Private doEvent As Boolean = False

    Private mailComparer As Comparison(Of Dictionary(Of String, Object)) = Function(a, b)
                                                                               If a.ContainsKey("case_order") And b.ContainsKey("case_order") And a.ContainsKey("pic_order") And b.ContainsKey("pic_order") Then
                                                                                   Return (a("case_order") * 10 + a("pic_order")).CompareTo(b("case_order") * 10 + b("pic_order"))
                                                                               Else
                                                                                   Return 0
                                                                               End If
                                                                           End Function

    Private Class Tax
        Public tax As Decimal = 0D
        Public total As Decimal = 0D
        Public net As Decimal = 0D

        Public Sub calc(exclusive As Boolean, tax_val As Decimal)
            If exclusive Then
                tax = tax_val / 100 * total
                net = total + tax
            Else
                net = total
                total = net / (tax_val + 100) * 100
                tax = net - total
            End If
        End Sub
    End Class

    Private Class Numbering
        Public last_number As Integer
        Public last_date As Date
        Public num As String
        Public type As String
        Public subtype As String

        Public Sub nextNumber(pattern As String, reset_mode As Integer)
            If (reset_mode = 1 And last_date <> Today) Or
              (reset_mode = 2 And Month(last_date) <> Month(Today)) Or
              (reset_mode = 3 And Year(last_date) <> Year(Today)) Then
                last_number = 1
            Else
                last_number += 1
            End If
            last_date = Today

            num = pattern
            num = Replace(num, "{type}", type)
            num = Replace(num, "{subtype}", subtype)
            num = Replace(num, "{yyyymmdd}", Year(last_date).ToString() + Strings.Right("0" + Month(last_date).ToString(), 2) + Strings.Right("0" + DateAndTime.Day(last_date).ToString(), 2))
            num = Replace(num, "{yymmdd}", Strings.Right(Year(last_date), 2) + Strings.Right("0" + Month(last_date).ToString(), 2) + Strings.Right("0" + DateAndTime.Day(last_date).ToString(), 2))
            num = Replace(num, "{yyyymm}", Year(last_date).ToString() + Strings.Right("0" + Month(last_date).ToString(), 2))
            num = Replace(num, "{yymm}", Strings.Right(Year(last_date), 2) + Strings.Right("0" + Month(last_date).ToString(), 2))
            num = Replace(num, "{num:5}", Strings.Right("0000" + last_number.ToString(), 5))
            num = Replace(num, "{num:4}", Strings.Right("000" + last_number.ToString(), 4))
            num = Replace(num, "{num:3}", Strings.Right("00" + last_number.ToString(), 3))
            num = Replace(num, "{num}", Strings.Right("00" + last_number.ToString(), 3))
        End Sub
    End Class

    Public Overrides Sub Print()
        MsgBox("print")
    End Sub

    Public Overrides Sub Save()
        If _cust_code = "" Then
            MsgBox("Please specify the customer first." + vbCrLf + "(OC-SV-01)")
            Exit Sub
        End If

        If cbSOType.SelectedIndex = -1 Then
            MsgBox("Please select Sales Order Type first." + vbCrLf + "(OC-SV-02)")
            Exit Sub
        End If

        If Not isPrived("OrderControl", If(updateData, 5, 3)) Then
            MsgBox("You are not allowed to " + If(updateData, "edit", "create") + " sales order." + vbCrLf + "(OC-SV-03)")
            Exit Sub
        End If

        If needApproval Then
            Dim result As Integer = MessageBox.Show("This order is violating the company rules, you will need approval to process this order." +
                                                    If(tbNote.Text = "", vbCrLf + "You should consider adding some notes.", ""), "Save anyway?", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then
                Exit Sub
            End If
        End If

        If saveSO(updateData) Then
            CloseForm()
            asyncLoadSO()
            _parent.refreshMenuState(toolButton)
        End If
    End Sub

    Public Sub Reload()
        asyncLoadSO()
    End Sub

    Public Overloads Sub Dispose()
        Try
            toolMenu.Clear()
            toolButton.Clear()
            SOBuffer.Clear()
            SOSearch.Clear()

            SOItemBuffer.Clear()

            custBuffer.Clear()
            custSearch.Clear()

            expdBuffer.Clear()
            expdSearch.Clear()

            itemBuffer.Clear()
            itemSearch.Clear()

            lotBuffer.Clear()

            If Not IsNothing(ascendCon) Then
                ascendCon.Dispose()
            End If

            MyBase.Dispose()
        Catch
        End Try
    End Sub

    Private Function getUsedLot(Optional exclude As String = "") As List(Of Dictionary(Of String, Object))
        Dim sql As String
        Dim rdr As Object
        Dim cmdRdr As Object

        getUsedLot = New List(Of Dictionary(Of String, Object))
        If dbCon.state() = ConnectionState.Open Then
            'While dbCon.isReading
            '    Thread.Sleep(200)
            'End While

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
                ascendCon.addParameter("@d1", DateValue(Now), cmdRdr)
                ascendCon.addParameter("@d2", DateValue(Now.AddMonths(6)), cmdRdr)
                ascendCon.addParameter("@exclude_so", exclude, cmdRdr)

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
                ascendCon.addParameter("@exclude_so", exclude)
                sql = dbCon.scalar()
            End If

            cmdRdr = dbCon.SQLReader(sql)
            dbCon.addParameter("@ld1", DateValue(Now), cmdRdr)
            dbCon.addParameter("@ld2", DateValue(Now.AddMonths(6)), cmdRdr)

            lotBuffer.Clear()

            rdr = dbCon.beginRead(cmdRdr)
            While dbCon.doRead(rdr)
                lotBuffer.Add(dbCon.getRow(rdr))
            End While
            dbCon.endRead(rdr)

            If lstLot.VirtualListSize = lotBuffer.Count Then
                lstLot.Invalidate()
            Else
                lstLot.VirtualListSize = lotBuffer.Count
            End If
        Else
            MsgBox("There is something wrong with the database connection." + vbCrLf + "(OC-GUL-01)")
        End If
    End Function

    Private Function getLotInfo(d As Date) As Dictionary(Of String, Object)
        Return (From l In lotBuffer Where l("lot_date") = d Select l).FirstOrDefault
    End Function

    Private Function getOnCredit(cust_code As String, Optional exclude_so As String = "", Optional useAscend As Boolean = False) As Dictionary(Of String, Object)
        Dim err As New Dictionary(Of String, Object)
        Dim rdr As Object
        Dim cmdRdr As Object

        err.Add("customer_code", "")
        err.Add("on_credit", 0)
        err.Add("payable", 0)
        err.Add("paid", 0)
        err.Add("balance", 0)
        err.Add("credit_balance", 0)

        _parent.spbUpdate.Visible = True
        _parent.spbUpdate.Value = 0
        _parent.spbUpdate.Style = ProgressBarStyle.Marquee
        _parent.spbUpdate.Text = "Loading Credits"

        _parent.spbUpdate.Value = (_parent.spbUpdate.Value + 1) Mod 100

        If useAscend Then
            If IsNothing(ascendCon) Then
                ascendCon = New DBConn
                ascendCon.init("MSSQL", ascendConStr)
                ascendCon.open()
            End If

            _parent.spbUpdate.Value = (_parent.spbUpdate.Value + 1) Mod 100

            If ascendCon.state = ConnectionState.Open Then
                dbCon.SQL("select query from sqls where [transaction] = 'ascend_oncredit'")
                Dim sql As String = dbCon.scalar()

                cmdRdr = ascendCon.SQLReader(sql)
                ascendCon.addParameter("@cust", cust_code, cmdRdr)
                ascendCon.addParameter("@exclude_so", exclude_so, cmdRdr)
                ascendCon.addParameter("@exclude_tax", 1, cmdRdr)

                rdr = ascendCon.beginRead(cmdRdr)
                _parent.spbUpdate.Value = (_parent.spbUpdate.Value + 1) Mod 100
                If ascendCon.doRead(rdr) Then
                    Dim r As Dictionary(Of String, Object) = ascendCon.getRow(rdr)
                    ascendCon.endRead(rdr)
                    _parent.spbUpdate.Visible = False
                    Return r
                Else
                    ascendCon.endRead(rdr)
                    _parent.spbUpdate.Visible = False
                    Return err
                End If
            Else
                _parent.spbUpdate.Visible = False
                MsgBox("There is something wrong with the database connection." + vbCrLf + "(OC-GOC-01)")
                Return err
            End If
        Else
            _parent.spbUpdate.Value = (_parent.spbUpdate.Value + 1) Mod 100

            If dbCon.state() = ConnectionState.Open Then
                _parent.spbUpdate.Visible = False
                Return err
            Else
                _parent.spbUpdate.Visible = False
                MsgBox("There is something wrong with the database connection." + vbCrLf + "(OC-GOC-02)")
                Return err
            End If
        End If
    End Function

    Private Function injectAscendSO() As String
        Dim sql As String

        If IsNothing(ascendCon) Then
            ascendCon = New DBConn
            ascendCon.init("MSSQL", ascendConStr)
            ascendCon.open()
        End If

        If ascendCon.state = ConnectionState.Open Then
            'get document number
            ascendCon.SQL("USP_Common_Generate_Number", CommandType.StoredProcedure)
            ascendCon.addParameter("@DocumentType", "BS.SalesOrder")
            ascendCon.addParameter("@TableName", "AR_SalesOrders")
            ascendCon.addParameter("@FieldCounter", "SOCounter")
            ascendCon.addParameter("@FieldDate", "SODate")
            ascendCon.addParameter("@Date", Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
            ascendCon.addOutputParam("@NewNumber", 300, SqlDbType.VarChar)
            ascendCon.addOutputParam("@NewCounter", 18, SqlDbType.BigInt)
            ascendCon.addParameter("@ExtraWhereClause", "SOType = '" + cbSOType.SelectedValue + "'")
            ascendCon.addParameter("@Tag1", "$SOT")
            ascendCon.addParameter("@Tag1Value", cbSOType.SelectedValue)
            ascendCon.execute()

            Dim SONum As String = ascendCon.getParamOutput("@NewNumber").ToString
            Dim SOCtr As Integer = ascendCon.getParamOutput("@NewCounter")

            'Dim item As ListViewItem
            Dim cust As Dictionary(Of String, Object)
            'item = lstCustSearch.Items(lstCustSearch.SelectedIndices(0))
            cust = (From c In custSearch Where c("customer_code") = _cust_code Select c).FirstOrDefault()

            Dim t As Date = Now
            Dim rng As New Random
            Dim taxamt As Decimal = 0
            Dim tot As Decimal = 0
            Dim net As Decimal = 0
            Dim tqty As Decimal = 0
            Dim remark As String = ""

            Dim minprc As Decimal = Decimal.MaxValue

            For Each row In SOItemBuffer
                net += row("linetotal")
                tot += row("linebeforetax")
                tqty += row("quantity")
                If row("sales_price") < minprc Then
                    minprc = row("sales_price")
                End If
            Next
            taxamt = net - tot

            dbCon.SQL("select query from sqls where [transaction] = 'ascend_inject_so'")
            sql = dbCon.scalar()

            ascendCon.SQL(sql)
            ascendCon.addParameter("@sot", cbSOType.SelectedValue)
            ascendCon.addParameter("@num", SONum)
            ascendCon.addParameter("@soctr", SOCtr)
            ascendCon.addParameter("@custid", cust("ascend_id"))
            ascendCon.addParameter("@sodate", DateValue(t))
            ascendCon.addParameter("@expdd", DateValue(dpExpect.Value))
            ascendCon.addParameter("@curid", cbCurrency.SelectedItem.Item("ascend_id"))
            ascendCon.addParameter("@salesid", cust("ascend_sp_id"))
            ascendCon.addParameter("@top", cust("top"))
            ascendCon.addParameter("@tax", If(cust("tax") = "nt", "", "10%"))
            ascendCon.addParameter("@taxamt", taxamt)
            ascendCon.addParameter("@taxinc", rbTaxInclusive.Checked)
            ascendCon.addParameter("@totcharge", 0)
            ascendCon.addParameter("@olpin", rng.Next(0, 9999).ToString("0000"))
            ascendCon.addParameter("@nettot", net)
            ascendCon.addParameter("@intrem", tbRemark.Text + vbCrLf + vbCrLf + tbNote.Text)
            ascendCon.addParameter("@etd", DateValue(dpEstimate.Value))
            ascendCon.addParameter("@eta", DateValue(dpExpect.Value))
            ascendCon.addParameter("@prcrate", Decimal.Parse(tbRate.Text))
            ascendCon.addParameter("@shipid", _expd_ascend_id)      'to update
            ascendCon.addParameter("@dest", tbDestination.Text)     'to update
            ascendCon.addParameter("@custpo", tbCustPO.Text)
            ascendCon.addParameter("@custpodate", If(String.IsNullOrEmpty(tbCustPO.Text), DBNull.Value, dpCustPO.Value))
            ascendCon.addParameter("@approved", If(needApproval, 0, 1))
            ascendCon.addParameter("@mingrossprc", minprc)
            ascendCon.addParameter("@grossprcvar", 0)
            ascendCon.addParameter("@minnetprc", minprc)
            ascendCon.addParameter("@netprcvar", 0)
            ascendCon.addParameter("@discvar", 1)
            ascendCon.addParameter("@createby", user)
            ascendCon.addParameter("@createdate", t)
            ascendCon.addParameter("@modby", user)
            ascendCon.addParameter("@moddate", t)
            ascendCon.addParameter("@rem", "")
            ascendCon.execute()

            'get insert id
            ascendCon.SQL("select SOID from AR_SalesOrders where SONumber = @num")
            ascendCon.addParameter("@num", SONum)
            Dim soid As Integer = ascendCon.scalar()
            Dim i = 1
            Dim irem As String = ""

            dbCon.SQL("select query from sqls where [transaction] = 'ascend_inject_so_detail'")
            sql = dbCon.scalar()

            For Each row In SOItemBuffer
                ascendCon.SQL(sql)
                ascendCon.addParameter("@soid", soid)
                ascendCon.addParameter("@itemid", row("ascend_id"))
                ascendCon.addParameter("@qty", row("quantity"))
                ascendCon.addParameter("@uomlvl", 1)
                ascendCon.addParameter("@uprc", row("sales_price"))
                tot = row("sales_price") * row("quantity")
                If rbTaxInclusive.Checked Then
                    ascendCon.addParameter("@taxincprc", tot)
                    ascendCon.addParameter("@lntot", tot / 1.1)
                    taxamt = tot - tot / 1.1
                Else
                    ascendCon.addParameter("@taxincprc", tot * 1.1)
                    ascendCon.addParameter("@lntot", tot)
                    taxamt = tot * 0.1
                End If
                If row("min_order") > row("quantity") Then
                    irem = "Order quantity is below minimum order (" + String.Format(numformat, row("quantity")) + " < " + String.Format(numformat, row("min_order")) + ")"
                Else
                    irem = ""
                End If
                ascendCon.addParameter("@rem", irem)
                ascendCon.addParameter("@createby", user)
                ascendCon.addParameter("@createdate", t)
                ascendCon.addParameter("@modby", user)
                ascendCon.addParameter("@didx", i)
                ascendCon.addParameter("@moddate", t)
                ascendCon.addParameter("@taxamt", taxamt)
                ascendCon.execute()

                i += 1
            Next

            ascendCon.SQL("USP_AR_SalesOrders_CalculateVariances", CommandType.StoredProcedure)
            ascendCon.addParameter("@SOID", soid)
            ascendCon.execute()

            Return SONum
        Else
            Return ""
        End If
    End Function

    Private Sub updateAscendSO(SONum As String)
        Dim sql As String

        If IsNothing(ascendCon) Then
            ascendCon = New DBConn
            ascendCon.init("MSSQL", ascendConStr)
            ascendCon.open()
        End If

        If ascendCon.state = ConnectionState.Open Then
            'get insert id
            ascendCon.SQL("select SOID from AR_SalesOrders where SONumber = @num")
            ascendCon.addParameter("@num", SONum)
            Dim soid As Integer = ascendCon.scalar()

            'Dim item As ListViewItem
            Dim cust As Dictionary(Of String, Object)
            'item = lstCustSearch.Items(lstCustSearch.SelectedIndices(0))
            cust = (From c In custSearch Where c("customer_code") = _cust_code Select c).FirstOrDefault()

            Dim t As Date = Now
            Dim rng As New Random
            Dim taxamt As Decimal = 0
            Dim tot As Decimal = 0
            Dim net As Decimal = 0
            Dim tqty As Decimal = 0
            Dim remark As String = ""

            Dim minprc As Decimal = Decimal.MaxValue

            For Each row In SOItemBuffer
                net += row("linetotal")
                tot += row("linebeforetax")
                tqty += row("quantity")
                If row("sales_price") < minprc Then
                    minprc = row("sales_price")
                End If
            Next
            taxamt = net - tot

            dbCon.SQL("select query from sqls where [transaction] = 'ascend_update_so'")
            sql = dbCon.scalar()

            ascendCon.SQL(sql)
            ascendCon.addParameter("@custid", cust("ascend_id"))
            ascendCon.addParameter("@expdd", DateValue(dpExpect.Value))
            ascendCon.addParameter("@curid", cbCurrency.SelectedItem.Item("ascend_id"))
            ascendCon.addParameter("@salesid", cust("ascend_sp_id"))
            ascendCon.addParameter("@top", cust("top"))
            ascendCon.addParameter("@tax", If(cust("tax") = "nt", "", "10%"))
            ascendCon.addParameter("@taxamt", taxamt)
            ascendCon.addParameter("@taxinc", rbTaxInclusive.Checked)
            ascendCon.addParameter("@totcharge", 0)
            ascendCon.addParameter("@nettot", net)
            ascendCon.addParameter("@intrem", tbRemark.Text + vbCrLf + vbCrLf + tbNote.Text)
            ascendCon.addParameter("@etd", DateValue(dpEstimate.Value))
            ascendCon.addParameter("@eta", DateValue(dpExpect.Value))
            ascendCon.addParameter("@prcrate", Decimal.Parse(tbRate.Text))
            ascendCon.addParameter("@shipid", _expd_ascend_id)      'to update
            ascendCon.addParameter("@dest", tbDestination.Text)     'to update
            ascendCon.addParameter("@custpo", tbCustPO.Text)
            ascendCon.addParameter("@custpodate", If(String.IsNullOrEmpty(tbCustPO.Text), DBNull.Value, dpCustPO.Value))
            ascendCon.addParameter("@approved", If(needApproval, 0, 1))
            ascendCon.addParameter("@mingrossprc", minprc)
            ascendCon.addParameter("@grossprcvar", 0)
            ascendCon.addParameter("@minnetprc", minprc)
            ascendCon.addParameter("@netprcvar", 0)
            ascendCon.addParameter("@discvar", 1)
            ascendCon.addParameter("@modby", user)
            ascendCon.addParameter("@moddate", t)
            ascendCon.addParameter("@rem", "")

            ascendCon.addParameter("@sonum", SONum)
            ascendCon.addParameter("@soid", soid)
            ascendCon.execute()


            dbCon.SQL("select query from sqls where [transaction] = 'ascend_clear_so_detail'")
            sql = dbCon.scalar()

            ascendCon.SQL(sql)
            ascendCon.addParameter("@soid", soid)
            ascendCon.execute()

            Dim i = 1
            Dim irem As String = ""

            dbCon.SQL("select query from sqls where [transaction] = 'ascend_inject_so_detail'")
            sql = dbCon.scalar()

            For Each row In SOItemBuffer
                ascendCon.SQL(sql)
                ascendCon.addParameter("@soid", soid)
                ascendCon.addParameter("@itemid", row("ascend_id"))
                ascendCon.addParameter("@qty", row("quantity"))
                ascendCon.addParameter("@uomlvl", 1)
                ascendCon.addParameter("@uprc", row("sales_price"))
                tot = row("sales_price") * row("quantity")
                If rbTaxInclusive.Checked Then
                    ascendCon.addParameter("@taxincprc", tot)
                    ascendCon.addParameter("@lntot", tot / 1.1)
                    taxamt = tot - tot / 1.1
                Else
                    ascendCon.addParameter("@taxincprc", tot * 1.1)
                    ascendCon.addParameter("@lntot", tot)
                    taxamt = tot * 0.1
                End If
                If row("min_order") > row("quantity") Then
                    irem = "Order quantity is below minimum order (" + String.Format(numformat, row("quantity")) + " < " + String.Format(numformat, row("min_order")) + ")"
                Else
                    irem = ""
                End If
                ascendCon.addParameter("@rem", irem)
                ascendCon.addParameter("@createby", user)
                ascendCon.addParameter("@createdate", t)
                ascendCon.addParameter("@modby", user)
                ascendCon.addParameter("@didx", i)
                ascendCon.addParameter("@moddate", t)
                ascendCon.addParameter("@taxamt", taxamt)
                ascendCon.execute()

                i += 1
            Next

            ascendCon.SQL("USP_AR_SalesOrders_CalculateVariances", CommandType.StoredProcedure)
            ascendCon.addParameter("@SOID", soid)
            ascendCon.execute()

        End If
    End Sub

    Private Function saveSO(update As Boolean) As Boolean
        Dim num As New Numbering
        Dim row As Dictionary(Of String, Object)
        Dim r As Dictionary(Of String, Object)
        Dim cust As Dictionary(Of String, Object)
        'Dim item As ListViewItem
        Dim t As Date = Now

        Dim number As String

        'item = lstCustSearch.Items(lstCustSearch.SelectedIndices(0))
        cust = (From c In custSearch Where c("customer_code") = _cust_code Select c).FirstOrDefault()

        If dbCon.state() = ConnectionState.Open Then
            Dim tot As Decimal = 0D
            Dim net As Decimal = 0D
            Dim taxamt As Decimal = 0D
            Dim tqty As Decimal = 0D
            Dim remark As String = ""
            Dim irem As String = ""
            Dim rdr As Object
            Dim cmdRdr As Object

            For Each row In SOItemBuffer
                net += row("linetotal")
                tot += row("linebeforetax")
                tqty += row("quantity")
            Next
            taxamt = net - tot

            If update Then
                'update SO
                If injectAscend Then
                    updateAscendSO(tbSONum.Text)
                End If
                dbCon.SQL("update sales_orders set customer_code = @cust, customer_po = @custpo, customer_po_date = @custpodate, sales_person_code = @sales, " +
                            "tax_inclusive = @taxinc, tax_rate = @taxrate, tax_amount = @taxamt, total_amount = @totamt, net_amount = @netamt, " +
                            "currency_code = @curr, exchange_rate = @exrate, est_delivery_date = @estd, est_arrival_date = @expd, " +
                            "updated_at = @updateat, updated_by = @updateby, min_approval = @minapv, approval_concern = @apvconcern, " +
                            "approvals = @apvs, note = @note, expedition = @exped, destination = @dest where company_code = @cc and business_code = @bc and so_type = @type and so_number = @sonum")
                dbCon.addParameter("@cust", cust("customer_code"))
                dbCon.addParameter("@custpo", tbCustPO.Text)
                dbCon.addParameter("@custpodate", DateValue(dpCustPO.Value))
                dbCon.addParameter("@sales", cust("sales_person_code"))
                dbCon.addParameter("@taxinc", If(rbTaxInclusive.Checked, 1, 0))
                dbCon.addParameter("@taxrate", If(cust("tax") = "nt", 0, 10))
                dbCon.addParameter("@taxamt", taxamt)
                dbCon.addParameter("@totamt", tot)
                dbCon.addParameter("@netamt", net)
                dbCon.addParameter("@curr", cbCurrency.SelectedItem.Item("currency_code"))
                dbCon.addParameter("@exrate", Decimal.Parse(tbRate.Text))
                dbCon.addParameter("@estd", DateValue(dpEstimate.Value))
                dbCon.addParameter("@expd", DateValue(dpExpect.Value))
                dbCon.addParameter("@updateat", t)
                dbCon.addParameter("@updateby", user)
                dbCon.addParameter("@minapv", If(needApproval, approveMails.Count, 0))
                dbCon.addParameter("@apvconcern", tbRemark.Text)
                If approveMails.Count = 0 Then
                    dbCon.addParameter("@apvs", "{}")
                Else
                    Dim apvs As New Dictionary(Of String, Object)

                    If approveMails.Count > 1 Then
                        approveMails.Sort(mailComparer)
                    End If

                    apvs.Add("emails", approveMails)
                    dbCon.addParameter("@apvs", json.Serialize(apvs))
                End If
                dbCon.addParameter("@note", tbNote.Text)
                dbCon.addParameter("@exped", _expd_code)
                dbCon.addParameter("@dest", tbDestination.Text)

                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@bc", area)
                dbCon.addParameter("@type", cbSOType.SelectedValue)
                dbCon.addParameter("@sonum", tbSONum.Text)
                dbCon.execute()

                dbCon.SQL("delete from sales_order_details where company_code = @cc and business_code = @bc and so_number = @sonum")
                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@bc", area)
                dbCon.addParameter("@type", cbSOType.SelectedValue)
                dbCon.addParameter("@sonum", tbSONum.Text)
                dbCon.execute()

                For Each soitem In SOItemBuffer
                    dbCon.SQL("insert into sales_order_details(company_code, business_code, so_number, item_code, price, quantity, total, tax_amount, note)
                            values(@cc, @bc, @num, @item, @prc, @qty, @tot, @taxamt, @note)")
                    dbCon.addParameter("@cc", corp)
                    dbCon.addParameter("@bc", area)
                    dbCon.addParameter("@num", tbSONum.Text)
                    dbCon.addParameter("@item", soitem("item_code"))
                    dbCon.addParameter("@prc", soitem("sales_price"))
                    dbCon.addParameter("@qty", soitem("quantity"))
                    dbCon.addParameter("@tot", soitem("linetotal"))
                    dbCon.addParameter("@taxamt", soitem("linetotal") - soitem("linebeforetax"))
                    If soitem("min_order") > soitem("quantity") Then
                        irem = "Order quantity is below minimum order (" + String.Format(numformat, soitem("quantity")) + " < " + String.Format(numformat, soitem("min_order")) + ")"
                    Else
                        irem = ""
                    End If
                    dbCon.addParameter("@note", irem)
                    dbCon.execute()
                Next
            Else
                If Not injectAscend Then
                    cmdRdr = dbCon.SQLReader("select n.format, n.reset_mode from numbering n join numbering_sequence ns on n.company_code = ns.company_code and n.module = ns.module and 
                            where company_code = @cc and module = @m")
                    dbCon.addParameter("@cc", corp, cmdRdr)
                    dbCon.addParameter("@m", "SO", cmdRdr)
                    rdr = dbCon.beginRead(cmdRdr)
                    Try
                        If dbCon.doRead(rdr) Then
                            r = dbCon.getRow(rdr)
                        Else
                            r = New Dictionary(Of String, Object)
                        End If
                        dbCon.endRead(rdr)
                    Catch ex As Exception
                        dbCon.endRead(rdr)
                        MsgBox(ex.Message)
                        Return False
                        Exit Function
                    End Try

                    num.last_number = r("last_number")
                    num.last_date = r("last_date")
                    num.type = cbSOType.SelectedValue
                    num.subtype = cust("customer_type")
                    num.nextNumber(r("format"), r("reset_mode"))

                    number = num.num
                Else
                    number = injectAscendSO()
                End If

                dbCon.SQL("insert into sales_orders(company_code, business_code, so_number, so_type, so_date, customer_code, customer_po, customer_po_date, sales_person_code, tax_inclusive, tax_rate,
                            tax_amount, total_amount, net_amount, currency_code, exchange_rate, est_delivery_date, est_arrival_date, created_at, created_by, min_approval, approval_concern, approvals, note,
                            expedition, destination)
                            values(@cc, @bc, @num, @type, @date, @cust, @custpo, @custpodate, @sales, @taxinc, @taxrate,
                            @taxamt, @totamt, @netamt, @curr, @exrate, @estd, @expd, @createat, @createby, @minapv, @apvconcern, @apvs, @note,
                            @exped, @dest)")
                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@bc", area)
                dbCon.addParameter("@num", number)
                dbCon.addParameter("@type", cbSOType.SelectedValue)
                dbCon.addParameter("@date", DateValue(t))
                dbCon.addParameter("@cust", cust("customer_code"))
                dbCon.addParameter("@custpo", tbCustPO.Text)
                dbCon.addParameter("@custpodate", DateValue(dpCustPO.Value))
                dbCon.addParameter("@sales", cust("sales_person_code"))
                dbCon.addParameter("@taxinc", If(rbTaxInclusive.Checked, 1, 0))
                dbCon.addParameter("@taxrate", If(cust("tax") = "nt", 0, 10))
                dbCon.addParameter("@taxamt", taxamt)
                dbCon.addParameter("@totamt", tot)
                dbCon.addParameter("@netamt", net)
                dbCon.addParameter("@curr", cbCurrency.SelectedItem.Item("currency_code"))
                dbCon.addParameter("@exrate", Decimal.Parse(tbRate.Text))
                dbCon.addParameter("@estd", DateValue(dpEstimate.Value))
                dbCon.addParameter("@expd", DateValue(dpExpect.Value))
                dbCon.addParameter("@createat", t)
                dbCon.addParameter("@createby", user)
                dbCon.addParameter("@minapv", If(needApproval, approveMails.Count, 0))
                dbCon.addParameter("@apvconcern", tbRemark.Text)
                If approveMails.Count = 0 Then
                    dbCon.addParameter("@apvs", "{}")
                Else
                    Dim apvs As New Dictionary(Of String, Object)

                    If approveMails.Count > 1 Then
                        approveMails.Sort(mailComparer)
                    End If

                    apvs.Add("emails", approveMails)
                    dbCon.addParameter("@apvs", json.Serialize(apvs))
                End If
                dbCon.addParameter("@note", tbNote.Text)
                dbCon.addParameter("@exped", _expd_code)
                dbCon.addParameter("@dest", tbDestination.Text)
                dbCon.execute()

                For Each soitem In SOItemBuffer
                    dbCon.SQL("insert into sales_order_details(company_code, business_code, so_number, item_code, price, quantity, total, tax_amount, note)
                            values(@cc, @bc, @num, @item, @prc, @qty, @tot, @taxamt, @note)")
                    dbCon.addParameter("@cc", corp)
                    dbCon.addParameter("@bc", area)
                    dbCon.addParameter("@num", number)
                    dbCon.addParameter("@item", soitem("item_code"))
                    dbCon.addParameter("@prc", soitem("sales_price"))
                    dbCon.addParameter("@qty", soitem("quantity"))
                    dbCon.addParameter("@tot", soitem("linetotal"))
                    dbCon.addParameter("@taxamt", soitem("linetotal") - soitem("linebeforetax"))
                    If soitem("min_order") > soitem("quantity") Then
                        irem = "Order quantity is below minimum order (" + String.Format(numformat, soitem("quantity")) + " < " + String.Format(numformat, soitem("min_order")) + ")"
                    Else
                        irem = ""
                    End If
                    dbCon.addParameter("@note", irem)
                    dbCon.execute()
                Next
            End If
            If approveMails.Count > 0 Then
                dbCon.SQL("select url from web_links where link_type = @lt and company_code = @cc")
                dbCon.addParameter("@lt", "so.approval")
                dbCon.addParameter("@cc", corp)

                Dim url As String = dbCon.scalar()

                Dim req As HttpWebRequest = WebRequest.Create(url)
                Dim response As HttpWebResponse = req.GetResponse()
            End If
            Return True
        Else
            MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-SVSO-01)")
            Return False
        End If
    End Function

    Private Sub loadCurrencies()
        Dim dt As New DataTable
        Dim bs As BindingSource

        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select currency, currency_code, convertion_rate, ascend_id from currencies")
            dbCon.fillTable(dt)

            bs = New BindingSource(dt, Nothing)
            cbCurrency.DataSource = bs
            cbCurrency.DisplayMember = "currency_code"
            cbCurrency.ValueMember = "currency_code"
        Else
            MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LC-01)")
        End If
    End Sub

    Private Sub asyncLoadSOTypes(cust_type As String)
        Dim loadThd As New loadThread(_parent, Me, dbCon, cust_type)
        Dim thd As New Thread(AddressOf loadThd.loadSOTypes)
        thd.IsBackground = True
        thd.Start()
    End Sub

    'Private Sub loadSOTypes(Optional cust_type As String = "")
    '    Dim dt As New DataTable
    '    Dim bs As BindingSource

    '    If dbCon.state() = ConnectionState.Open Then
    '        If cust_type = "" Then
    '            dbCon.SQL("select description, code, lead_time, lead_time_unit from sales_order_types where company_code = @cc")
    '            dbCon.addParameter("@cc", corp)
    '        Else
    '            dbCon.SQL("select sot.description, sot.code, sot.lead_time, sot.lead_time_unit from sales_order_types sot 
    '                        join customer_type_so_types ctsot on sot.company_code = ctsot.company_code and sot.code = ctsot.so_type where sot.company_code = @cc and ctsot.customer_type = @ct")
    '            dbCon.addParameter("@cc", corp)
    '            dbCon.addParameter("@ct", cust_type)
    '        End If
    '        dbCon.fillTable(dt)

    '        bs = New BindingSource(dt, Nothing)
    '        cbSOType.DataSource = bs
    '        cbSOType.DisplayMember = "code"
    '        cbSOType.ValueMember = "code"
    '        Else
    '            MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LSOT-01)")
    '    End If
    'End Sub

    Private Sub loadCusts()
        If dbCon.state() = ConnectionState.Open Then
            Dim rdr As Object

            Dim cmdRdr As Object = dbCon.SQLReader("select c.*, sp.sales_person, sp.ascend_id [ascend_sp_id], t.tax_value
                        from customers c 
                        join sales_persons sp on c.sales_person_code = sp.sales_person_code and c.company_code = sp.company_code 
                        join taxes t on c.tax = t.tax_code
                        where c.company_code = @cc and c.del = 0")
            dbCon.addParameter("@cc", corp, cmdRdr)

            rdr = dbCon.beginRead(cmdRdr)
            custBuffer.Clear()
            custSearch.Clear()

            While dbCon.doRead(rdr)
                custBuffer.Add(dbCon.getRow(rdr))
            End While
            dbCon.endRead(rdr)
            custSearch.AddRange(custBuffer)
            lstCustSearch.VirtualListSize = custSearch.Count()
        Else
            MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LC-01)")
        End If
    End Sub

    Private Sub lstCustSearch_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstCustSearch.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = custSearch(e.ItemIndex)
        e.Item = New ListViewItem(row("customer_code").ToString)
        e.Item.SubItems.Add(row("customer_name"))
        e.Item.SubItems.Add(row("customer_type"))
        e.Item.SubItems.Add(row("mailing_city"))
    End Sub

    Private Function searchCust(lst As List(Of Dictionary(Of String, Object)), val As String) As IQueryable(Of Dictionary(Of String, Object))
        searchCust = lst.AsQueryable

        Dim filter As Expressions.Expression(Of Func(Of Dictionary(Of String, Object), Boolean)) =
            Function(i As Dictionary(Of String, Object)) i.Where(Function(x) x.Value.ToString().IndexOf(val, 0, StringComparison.CurrentCultureIgnoreCase) > -1).Count > 0

        If Not String.IsNullOrEmpty(val) Then searchCust = Queryable.Where(searchCust, filter)
    End Function

    Private Sub loadExpeditions()
        If dbCon.state() = ConnectionState.Open Then
            Dim rdr As Object

            Dim cmdRdr As Object = dbCon.SQLReader("select v.*
                        from vendors v 
                        where v.company_code = @cc and v.del = 0 and v.vendor_type = 'EXPD'")
            dbCon.addParameter("@cc", corp, cmdRdr)

            rdr = dbCon.beginRead(cmdRdr)
            expdBuffer.Clear()
            expdSearch.Clear()

            While dbCon.doRead(rdr)
                expdBuffer.Add(dbCon.getRow(rdr))
            End While
            dbCon.endRead(rdr)
            expdSearch.AddRange(expdBuffer)
            lstExped.VirtualListSize = expdSearch.Count()
        Else
            MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LE-01)")
        End If
    End Sub

    Private Sub lstExped_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstExped.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = expdSearch(e.ItemIndex)
        e.Item = New ListViewItem(row("vendor_code").ToString)
        e.Item.SubItems.Add(row("vendor_name"))
        e.Item.SubItems.Add(row("mailing_city"))
    End Sub

    Private Function searchExpd(lst As List(Of Dictionary(Of String, Object)), val As String) As IQueryable(Of Dictionary(Of String, Object))
        searchExpd = lst.AsQueryable

        Dim filter As Expressions.Expression(Of Func(Of Dictionary(Of String, Object), Boolean)) =
            Function(i As Dictionary(Of String, Object)) i.Where(Function(x) x.Value.ToString().IndexOf(val, 0, StringComparison.CurrentCultureIgnoreCase) > -1).Count > 0

        If Not String.IsNullOrEmpty(val) Then searchExpd = Queryable.Where(searchExpd, filter)
    End Function

    Private Sub asyncLoadUnbindItems()
        Dim loadThd As New loadThread(_parent, Me, dbCon)
        Dim thd As New Thread(AddressOf loadThd.loadUnbindItems)
        thd.IsBackground = True
        thd.Start()
    End Sub

    'Private Sub loadUnbindItems()
    '    If dbCon.state() = ConnectionState.Open Then
    '        Dim ctr As Integer = 0
    '        dbCon.SQL("select i.*, isnull(ic.min_order, 2000) [min_order], isnull(ic.price, 0) [price], isnull(ic.currency_code, 'IDR') [currency_code] 
    '                    from view_items i left join item_customers ic on i.company_code = ic.company_code and i.item_code = ic.item_code and ic.del = 0
    '                    where i.company_code = @cc and ic.customer_code is null and i.sellable = 1")
    '        dbCon.addParameter("@cc", corp)

    '        dbCon.beginRead()
    '        itemUnbindBuffer.Clear()

    '        _parent.spbUpdate.Visible = True
    '        _parent.spbUpdate.Value = 0
    '        _parent.spbUpdate.Style = ProgressBarStyle.Marquee
    '        _parent.spbUpdate.Text = "Loading Items"

    '        While dbCon.doRead()
    '            itemUnbindBuffer.Add(dbCon.getRow)
    '            If ctr = 25 Then
    '                _parent.spbUpdate.Value = (_parent.spbUpdate.Value + 1) Mod 100
    '                ctr = 0
    '            End If
    '            ctr += 1
    '        End While

    '        dbCon.endRead()

    '        _parent.spbUpdate.Text = "Update Available"
    '        _parent.spbUpdate.Style = ProgressBarStyle.Continuous
    '        _parent.spbUpdate.Visible = False

    '    Else
    '        MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LUI-01)")
    '    End If
    'End Sub

    Private Sub asyncLoadItems(cust_code As String, Optional filter As String = "")
        Dim loadThd As New loadThread(_parent, Me, dbCon, cust_code, filter)
        Dim thd As New Thread(AddressOf loadThd.loadItems)
        thd.IsBackground = True
        thd.Start()
    End Sub

    'Private Sub loadItems(cust_code As String, Optional filter As String = "")
    '    If dbCon.state() = ConnectionState.Open Then
    '        Dim ctr As Integer = 0
    '        dbCon.SQL("select i.*, isnull(ic.min_order, 2000) [min_order], isnull(ic.price, 0) [price], isnull(ic.currency_code, 'IDR') [currency_code] 
    '                    from view_items i left join item_customers ic on i.company_code = ic.company_code and i.item_code = ic.item_code and ic.del = 0
    '                    where i.company_code = @cc and ic.customer_code = @cust and i.sellable = 1" + If(filter <> "", " and i.item_name like @filter", ""))
    '        dbCon.addParameter("@cc", corp)
    '        dbCon.addParameter("@cust", cust_code)
    '        If filter <> "" Then
    '            dbCon.addParameter("@filter", "%" + filter + "%")
    '        End If

    '        dbCon.beginRead()
    '        itemBuffer.Clear()
    '        itemBuffer.AddRange(itemUnbindBuffer)
    '        itemSearch.Clear()

    '        _parent.spbUpdate.Visible = True
    '        _parent.spbUpdate.Value = 0
    '        _parent.spbUpdate.Style = ProgressBarStyle.Marquee
    '        _parent.spbUpdate.Text = "Loading Items"

    '        While dbCon.doRead()
    '            itemBuffer.Add(dbCon.getRow)
    '            If ctr = 25 Then
    '                _parent.spbUpdate.Value = (_parent.spbUpdate.Value + 1) Mod 100
    '                ctr = 0
    '            End If
    '            ctr += 1
    '        End While

    '        dbCon.endRead()

    '        _parent.spbUpdate.Text = "Update Available"
    '        _parent.spbUpdate.Style = ProgressBarStyle.Continuous
    '        _parent.spbUpdate.Visible = False

    '        itemSearch.AddRange(itemBuffer)
    '        lstItemSearch.VirtualListSize = itemSearch.Count()
    '    Else
    '        MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LI-01)")
    '    End If
    'End Sub

    Private Sub lstItemSearch_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstItemSearch.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = itemSearch(e.ItemIndex)
        e.Item = New ListViewItem(row("item_code").ToString)
        e.Item.SubItems.Add(row("item_name"))
    End Sub

    Private Function searchItem(lst As List(Of Dictionary(Of String, Object)), val As String) As IQueryable(Of Dictionary(Of String, Object))
        searchItem = lst.AsQueryable

        Dim filter As Expressions.Expression(Of Func(Of Dictionary(Of String, Object), Boolean)) =
            Function(i As Dictionary(Of String, Object)) i.Where(Function(x) x.Value.ToString().IndexOf(val, 0, StringComparison.CurrentCultureIgnoreCase) > -1).Count > 0

        If Not String.IsNullOrEmpty(val) Then searchItem = Queryable.Where(searchItem, filter)
    End Function

    Private Sub loadSODetail(so_num As String)
        If dbCon.state() = ConnectionState.Open Then
            Dim rdr As Object

            Dim cmdRdr As Object = dbCon.SQLReader("select * from view_sales_order_details where company_code = @cc and so_number = @num")
            dbCon.addParameter("@cc", corp, cmdRdr)
            dbCon.addParameter("@num", so_num, cmdRdr)

            rdr = dbCon.beginRead(cmdRdr)
            SOItemBuffer.Clear()

            While dbCon.doRead(rdr)
                SOItemBuffer.Add(dbCon.getRow(rdr))
            End While

            dbCon.endRead(rdr)

            lstSOItem.VirtualListSize = SOItemBuffer.Count()
        Else
            MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LSOD-01)")
        End If
    End Sub

    Private Sub asyncLoadSO()
        Dim loadThd As loadThread
        If chkMonthFilter.Checked Then
            loadThd = New loadThread(_parent, Me, dbCon, dpDateFilter.Value)
        Else
            loadThd = New loadThread(_parent, Me, dbCon)
        End If
        Dim thd As New Thread(AddressOf loadThd.loadSO)
        thd.IsBackground = True
        thd.Start()
    End Sub

    'Private Sub loadSO()
    '    If dbCon.state() = ConnectionState.Open Then
    '        Dim ctr As Integer = 0
    '        Try
    '            If chkMonthFilter.Checked Then
    '                dbCon.SQL("select so.* from view_sales_orders so left join business_areas ba on ba.business_code = @bc
    '                        where so.company_code = @cc and (so.business_code = @bc or ba.is_company_wide = 1) and left(replace(convert(varchar, so.so_date, 111), '/', '-'), 7) = @mth")
    '                dbCon.addParameter("@cc", corp)
    '                dbCon.addParameter("@bc", area)
    '                dbCon.addParameter("@mth", dpDateFilter.Value.ToString("yyyy-MM", CultureInfo.InvariantCulture))
    '            Else
    '                dbCon.SQL("select * from view_sales_orders where company_code = @cc")
    '                dbCon.addParameter("@cc", corp)
    '            End If

    '            dbCon.beginRead()
    '            SOBuffer.Clear()
    '            SOSearch.Clear()

    '            _parent.spbUpdate.Visible = True
    '            _parent.spbUpdate.Value = 0
    '            _parent.spbUpdate.Style = ProgressBarStyle.Marquee
    '            _parent.spbUpdate.Text = "Loading Sales Orders"

    '            Dim r As New Dictionary(Of String, Object)

    '            While dbCon.doRead()
    '                r = dbCon.getRow
    '                SOBuffer.Add(r)
    '                If ctr = 25 Then
    '                    _parent.spbUpdate.Value = (_parent.spbUpdate.Value + 1) Mod 100
    '                    ctr = 0
    '                End If
    '                ctr += 1
    '            End While

    '            dbCon.endRead()

    '            _parent.spbUpdate.Text = "Update Available"
    '            _parent.spbUpdate.Style = ProgressBarStyle.Continuous
    '            _parent.spbUpdate.Visible = False

    '            SOSearch.AddRange(SOBuffer)
    '            lstSO.VirtualListSize = SOSearch.Count()
    '        Catch ex As Exception
    '            Try
    '                dbCon.endRead()
    '            Catch ex2 As Exception
    '            End Try
    '            MsgBox(ex.Message + "." + vbCrLf + "(OC-LSO-02)")
    '        End Try
    '    Else
    '        MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LSO-01)")
    '    End If
    'End Sub

    Private Sub lstSO_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstSO.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)
        Dim approvals As Dictionary(Of String, Object)

        row = SOSearch(e.ItemIndex)
        e.Item = New ListViewItem(row("so_number").ToString)

        If row("min_approval") = row("approve_count") Then
            e.Item.ImageIndex = 0
        ElseIf row("min_approval") > row("approve_count") Then
            e.Item.ImageIndex = 1
        ElseIf row("void") Then
            e.Item.ImageIndex = 2
        End If

        e.Item.SubItems.Add(row("so_date"))
        e.Item.SubItems.Add(row("customer_name"))
        e.Item.SubItems.Add(row("sales_person"))
        e.Item.SubItems.Add(String.Format(numformat, row("net_amount")))
        e.Item.SubItems.Add(row("est_delivery_date"))
        e.Item.SubItems.Add(row("created_at"))
        e.Item.SubItems.Add(row("created_by"))
        If row("void") Then
            e.Item.SubItems.Add(If(row("min_approval") = 0, "Void", If(row("min_approval") = row("approve_count"), "Void", "Rejected")))
        Else
            e.Item.SubItems.Add(If(row("min_approval") = 0, "Approved", If(row("min_approval") = row("approve_count"), "Approved", "Pending Approval")))
        End If

        approvals = json.Deserialize(Of Dictionary(Of String, Object))(row("approvals"))
        If approvals.ContainsKey("approve_at") Then
            If approvals("approve_at").Count > 0 Then
                e.Item.SubItems.Add(approvals("approve_at")(approvals("approve_at").Count - 1))
            Else
                e.Item.SubItems.Add("")
            End If
        Else
            e.Item.SubItems.Add("")
        End If

        e.Item.SubItems.Add(If(row("closed_at"), ""))
        e.Item.SubItems.Add(If(row("void_at"), ""))

        'row.Clear()
        approvals.Clear()
    End Sub

    Private Function searchSO(lst As List(Of Dictionary(Of String, Object)), val As String) As IQueryable(Of Dictionary(Of String, Object))
        searchSO = lst.AsQueryable

        Dim filter As Expressions.Expression(Of Func(Of Dictionary(Of String, Object), Boolean)) =
            Function(i As Dictionary(Of String, Object)) i.Where(Function(x) x.Value.ToString().IndexOf(val, 0, StringComparison.CurrentCultureIgnoreCase) > -1).Count > 0

        If Not String.IsNullOrEmpty(val) Then searchSO = Queryable.Where(searchSO, filter)
    End Function

    Private Sub lstSOItem_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstSOItem.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = SOItemBuffer(e.ItemIndex)
        e.Item = New ListViewItem(row("item_code").ToString)
        e.Item.SubItems.Add(row("item_name"))
        e.Item.SubItems.Add(String.Format(numformat, row("base_price")))
        e.Item.SubItems.Add(String.Format(numformat, row("sales_price")))
        e.Item.SubItems.Add(String.Format(numformat, row("quantity")))
        e.Item.SubItems.Add(row("uom"))
        e.Item.SubItems.Add(String.Format(numformat, row("linetotal")))
        e.Item.SubItems.Add(String.Format(numformat, row("linebeforetax")))
        e.Item.SubItems.Add(row("note"))

        If row("min_order") > row("quantity") Or (_current_credit + _on_credit("credit_balance") > _credit_limit And _credit_limit > 0) Or _used_lot + _tot_qty > _max_lot Then
            e.Item.ForeColor = Color.Red
        End If
    End Sub

    Private Sub calcItems()
        'Dim t As New Tax
        'Dim c As ListViewItem
        'Dim cust As Dictionary(Of String, Object)
        'c = lstCustSearch.Items(lstCustSearch.SelectedIndices(0))
        'cust = custSearch(c.Index)
        'For Each item In SOItemBuffer
        '    t.total = item("quantity") * item("sales_price")
        '    t.calc(rbTaxExclusive.Checked, cust("tax_value"))
        '    item("linetotal") = t.net
        '    item("linebeforetax") = t.total
        'Next

        'lstSOItem.Invalidate()
    End Sub

    Private Sub calcSO()
        Dim total As Decimal = 0D
        Dim tax As Decimal = 0D
        Dim net As Decimal = 0D
        Dim tqty As Decimal = 0D
        Dim rdr As Object
        Dim cmdRdr As Object

        tbRemark.Text = ""

        needApproval = False
        approveMails.Clear()

        For Each item In SOItemBuffer
            net += item("linetotal")
            total += item("linebeforetax")
            tqty += item("quantity")

            If item("min_order") > item("quantity") Then
                needApproval = True
                tbRemark.Text = tbRemark.Text + If(String.IsNullOrEmpty(tbRemark.Text), "", vbCrLf) + "Order quantity for item " + item("item_code") + " is below minimum order quantity (" + String.Format(numformat, item("quantity")) + " < " + String.Format(numformat, item("min_order")) + ")"

                cmdRdr = dbCon.SQLReader("select case_code, pic_order, case_order, pic_email, pic_name from approval_pic where case_code = @case and company_code = @cc order by pic_order")
                dbCon.addParameter("@cc", corp, cmdRdr)
                dbCon.addParameter("@case", "MIN_ORDER", cmdRdr)

                rdr = dbCon.beginRead(cmdRdr)
                While dbCon.doRead(rdr)
                    approveMails.Add(dbCon.getRow(rdr))
                End While
                dbCon.endRead(rdr)
            End If
        Next
        tax = net - total
        _current_credit = net
        _tot_qty = tqty

        If _credit_limit < _on_credit("credit_balance") + net And _credit_limit > 0 Then
            needApproval = True
            tbRemark.Text = tbRemark.Text + If(String.IsNullOrEmpty(tbRemark.Text), "", vbCrLf) + "Total credit is larger than credit limit (" + String.Format(numformat, _on_credit("credit_balance") + net) + " > " + String.Format(numformat, _credit_limit) + ")"

            cmdRdr = dbCon.SQLReader("select case_code, pic_order, case_order, pic_email, pic_name from approval_pic where case_code = @case and company_code = @cc order by pic_order")
            dbCon.addParameter("@cc", corp, cmdRdr)
            dbCon.addParameter("@case", "CRD_LIMIT", cmdRdr)

            rdr = dbCon.beginRead(cmdRdr)
            While dbCon.doRead(rdr)
                approveMails.Add(dbCon.getRow(rdr))
            End While
            dbCon.endRead(rdr)
        End If

        If _max_lot < _used_lot + tqty Then
            needApproval = True
            tbRemark.Text = tbRemark.Text + If(String.IsNullOrEmpty(tbRemark.Text), "", vbCrLf) + "Total order lot for " + dpEstimate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " is exceeded (" + String.Format(numformat, _used_lot + tqty) + " > " + String.Format(numformat, _max_lot) + ")"

            cmdRdr = dbCon.SQLReader("select case_code, pic_order, case_order, pic_email, pic_name from approval_pic where case_code = @case and company_code = @cc order by pic_order")
            dbCon.addParameter("@cc", corp, cmdRdr)
            dbCon.addParameter("@case", "MAX_LOT", cmdRdr)

            rdr = dbCon.beginRead(cmdRdr)
            While dbCon.doRead(rdr)
                approveMails.Add(dbCon.getRow(rdr))
            End While
            dbCon.endRead(rdr)
        End If

        lbTaxAmt.Text = String.Format(numformat, tax)
        lbTotalAmt.Text = String.Format(numformat, total)
        lbTotalWithTax.Text = String.Format(numformat, net)

        lstSOItem.Invalidate()
    End Sub

    Private Sub OpenForm()
        SplitContainer1.Panel1Collapsed = True
        SplitContainer1.Panel2Collapsed = False

        'itemLoadTick.Enabled = True

        toolButton("Save").enabled = True
        toolButton("Edit").enabled = False
        toolButton("Delete").enabled = False
        toolButton("Print").enabled = False
    End Sub

    Private Sub CloseForm()
        SplitContainer1.Panel1Collapsed = False
        SplitContainer1.Panel2Collapsed = True
        toolButton("Save").enabled = False
        If lstSO.SelectedIndices.Count = 1 Then
            toolButton("Edit").enabled = True
        End If
        If lstSO.SelectedIndices.Count = 1 Then
            toolButton("Delete").enabled = True
        End If
        If lstSO.SelectedIndices.Count = 1 Then
            toolButton("Print").enabled = True
        End If
    End Sub

    Private Sub ClearForm()
        doEvent = False

        needApproval = False
        updateData = False

        cbSOType.SelectedValue = ""
        tbSONum.Text = "NEW"
        dpSO.Value = Now
        dpEstimate.Value = Now.AddMonths(1)
        dpExpect.Value = Now.AddMonths(1)
        tbCust.Text = ""
        tbSales.Text = ""
        tbCustPO.Text = ""
        cbCurrency.SelectedValue = "IDR"

        _cust_code = ""
        _credit_limit = 0D
        _on_credit("customer_code") = ""
        _on_credit("on_credit") = 0D
        _on_credit("payable") = 0D
        _on_credit("paid") = 0D
        _on_credit("balance") = 0D
        _on_credit("credit_balance") = 0D

        lbCreditLimit.Text = "0"
        lbOnCredit.Text = "0 (payable: 0)"

        custSearch.Clear()
        custSearch.AddRange(custBuffer)
        lstCustSearch.VirtualListSize = custSearch.Count
        lstCustSearch.SelectedIndices.Clear()

        lbTOP.Text = "-"
        rbTaxExclusive.Checked = True
        taxPanel.Enabled = False

        lstSOItem.VirtualListSize = 0
        SOItemBuffer.Clear()

        calcSO()

        lstItemSearch.VirtualListSize = 0
        itemBuffer.Clear()

        tbItemSearch.Text = ""
        tbQuantity.Value = 0

        doEvent = True
    End Sub

    Public Sub NewForm()
        Dim lot As Dictionary(Of String, Object)

        tbSONum.Enabled = True
        OpenForm()
        ClearForm()
        getUsedLot()

        lot = getLotInfo(DateValue(dpEstimate.Value))

        If Not IsNothing(lot) Then
            _max_lot = lot("max_order")
            _used_lot = lot("existing_order")
        Else
            _max_lot = 0D
            _used_lot = 0D
        End If
        lbMaxLot.Text = String.Format(numformat, _max_lot)
        lbUsedLot.Text = String.Format(numformat, _used_lot)
        lbLotAvail.Text = String.Format(numformat, _max_lot - _used_lot)

        _parent.refreshMenuState(toolButton)
    End Sub

    Public Sub EditForm()
        Dim item As ListViewItem
        Dim row As Dictionary(Of String, Object)
        Dim cust As Dictionary(Of String, Object)

        If lstSO.SelectedIndices.Count = 1 Then
            updateData = True

            doEvent = False

            item = lstSO.Items(lstSO.SelectedIndices(0))
            row = SOSearch(item.Index)

            cbSOType.SelectedValue = row("so_type")
            tbSONum.Text = row("so_number")
            dpSO.Value = row("so_date")
            dpEstimate.Value = row("est_delivery_date")
            dpExpect.Value = row("est_arrival_date")
            tbCust.Text = row("customer_name")
            tbSales.Text = row("sales_person")
            tbCustPO.Text = row("customer_po")
            If Not String.IsNullOrEmpty(row("customer_po")) Then
                dpCustPO.Value = row("customer_po_date")
            Else
                dpCustPO.Enabled = False
            End If
            cbCurrency.SelectedValue = row("currency_code")

            cust = (From cb In custBuffer Where cb("customer_code") = row("customer_code") Select cb).FirstOrDefault()
            _cust_code = row("customer_code")
            _credit_limit = cust("credit_limit")
            lbCreditLimit.Text = String.Format(numformat, _credit_limit)

            custSearch.Clear()
            custSearch.AddRange(custBuffer)
            lstCustSearch.VirtualListSize = custSearch.Count
            lstCustSearch.SelectedIndices.Clear()

            lbTOP.Text = cust("top")
            If row("tax_inclusive") Then
                rbTaxInclusive.Checked = True
            Else
                rbTaxExclusive.Checked = True
            End If
            taxPanel.Enabled = If(row("tax_rate") > 0, True, False)

            lstSOItem.VirtualListSize = 0
            SOItemBuffer.Clear()

            itemLoadTick.Enabled = True

            tbItemSearch.Text = ""
            tbQuantity.Value = 0

            tbSONum.Enabled = False
            OpenForm()

            doEvent = True
            _parent.refreshMenuState(toolButton)
        Else
            MsgBox("Please select the item to modify." + vbCrLf + "(OC-EF-01)")
        End If
    End Sub

    Private Sub OrderControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim btn As ERPModules.buttonState
        'Dim lot As Dictionary(Of String, Object)

        _parent = FindForm()

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = True
        toolButton.Add("New", btn)

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = False
        toolButton.Add("Edit", btn)

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = False
        toolButton.Add("Save", btn)

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = False
        toolButton.Add("Delete", btn)

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = False
        toolButton.Add("Print", btn)

        lstItemSearch.VirtualMode = True
        lstCustSearch.VirtualMode = True
        lstSOItem.VirtualMode = True
        lstSO.VirtualMode = True
        lstExped.VirtualMode = True
        lstLot.VirtualMode = True

        tbQuantity.Maximum = Decimal.MaxValue

        'getUsedLot()
        'lot = getLotInfo(DateValue(dpEstimate.Value))

        'If Not IsNothing(lot) Then
        '    _max_lot = lot("max_order")
        '    _used_lot = lot("existing_order")
        'Else
        '    _max_lot = 0D
        '    _used_lot = 0D
        'End If
        'lbMaxLot.Text = String.Format(numformat, _max_lot)
        'lbUsedLot.Text = String.Format(numformat, _used_lot)
        'lbLotAvail.Text = String.Format(numformat, _max_lot - _used_lot)

        SplitContainer1.Panel1Collapsed = False
        SplitContainer1.Panel2Collapsed = True

        loadCusts()
        loadExpeditions()
        loadCurrencies()
        asyncLoadSOTypes("")
        asyncLoadUnbindItems()
        asyncLoadSO()

        doEvent = True
    End Sub

    Private Sub tbItemSearch_TextChanged(sender As Object, e As EventArgs) Handles tbItemSearch.TextChanged, tbItemSearch.GotFocus
        Dim row As Dictionary(Of String, Object)

        If Not doEvent Then Exit Sub

        If _cust_code = "" Then
            doEvent = False
            tbItemSearch.Text = ""
            tbCust.Focus()
            MsgBox("Please select Customer first." + vbCrLf + "(OC-ISTC-01)")
            doEvent = True
            Exit Sub
        Else
            row = (From r In custSearch Where r("customer_code") = _cust_code Select r).FirstOrDefault
            If UCase(row("customer_name")) <> tbCust.Text Then
                doEvent = False
                tbItemSearch.Text = ""
                tbCust.Focus()
                MsgBox("Please select Customer first." + vbCrLf + "(OC-ISTC-02)")
                doEvent = True
                Exit Sub
            End If
        End If

        Dim tmp As IQueryable(Of Dictionary(Of String, Object))
        tmp = searchItem(itemBuffer, tbItemSearch.Text)
        If tmp IsNot Nothing Then
            itemSearch = tmp.ToList()
        Else
            itemSearch.Clear()
        End If

        lstItemSearch.VirtualListSize = itemSearch.Count

        'If lstItemSearch.VirtualListSize = 0 And pnlItem.Visible Then
        '    itemSearchTick.Enabled = False
        '    itemSearchTick.Enabled = True
        'End If

        pnlItem.Location = New Point(tcItems.Location.X + tcItems.TabPages(0).Location.X + tbItemSearch.Location.X,
                                             tcItems.Location.Y + tcItems.TabPages(0).Location.Y + tbItemSearch.Location.Y - pnlItem.Height + 1)

        pnlItem.Visible = True
    End Sub

    Private Sub tbItemSearch_LostFocus(sender As Object, e As EventArgs) Handles tbItemSearch.LostFocus
        If Not lstItemSearch.Focused Then
            pnlItem.Visible = False
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearForm()
        CloseForm()
        _parent.refreshMenuState(toolButton)
    End Sub

    Private Sub lstSO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSO.SelectedIndexChanged
        If lstSO.SelectedIndices.Count = 1 Then
            toolButton("Edit").enabled = True
            toolButton("Delete").enabled = True
            toolButton("Print").enabled = True
            _parent.refreshMenuState(toolButton)
        ElseIf toolButton("Edit").enabled Then
            toolButton("Edit").enabled = False
            toolButton("Delete").enabled = False
            toolButton("Print").enabled = False
            _parent.refreshMenuState(toolButton)
        End If
    End Sub

    Private Sub cbDateFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkMonthFilter.CheckedChanged
        dpDateFilter.Enabled = chkMonthFilter.Checked
    End Sub

    Private Sub cbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCurrency.SelectedIndexChanged
        tbRate.Text = String.Format(numformat, cbCurrency.SelectedItem.Item("convertion_rate"))
    End Sub

    Private Sub tbCust_TextChanged(sender As Object, e As EventArgs) Handles tbCust.TextChanged, tbCust.GotFocus
        If Not doEvent Then Exit Sub

        Dim tmp As IQueryable(Of Dictionary(Of String, Object))
        tmp = searchCust(custBuffer, tbCust.Text)
        If tmp IsNot Nothing Then
            custSearch = tmp.ToList()
        Else
            custSearch.Clear()
        End If
        lstCustSearch.VirtualListSize = custSearch.Count

        pnlCust.Location = New Point(tbCust.Location.X, tbCust.Location.Y + tbCust.Height - 1)

        pnlCust.Visible = True
    End Sub

    Private Sub tbSearchSO_TextChanged(sender As Object, e As EventArgs) Handles tbSearchSO.TextChanged
        If Not doEvent Then Exit Sub

        Dim tmp As IQueryable(Of Dictionary(Of String, Object))
        tmp = searchSO(SOBuffer, tbSearchSO.Text)
        If tmp IsNot Nothing Then
            SOSearch = tmp.ToList()
        Else
            SOSearch.Clear()
        End If
        lstSO.VirtualListSize = SOSearch.Count
    End Sub

    Private Sub tbCust_LostFocus(sender As Object, e As EventArgs) Handles tbCust.LostFocus
        If Not lstCustSearch.Focused Then
            pnlCust.Visible = False
        End If
    End Sub

    Private Sub lstCustSearch_LostFocus(sender As Object, e As EventArgs) Handles lstCustSearch.LostFocus
        pnlCust.Visible = False
    End Sub

    Private Sub lstCustSearch_DoubleClick(sender As Object, e As EventArgs) Handles lstCustSearch.DoubleClick
        Dim item As ListViewItem
        Dim row As Dictionary(Of String, Object)

        If lstCustSearch.SelectedIndices.Count = 1 Then
            doEvent = False

            item = lstCustSearch.Items(lstCustSearch.SelectedIndices(0))
            row = custSearch(item.Index)

            tbCust.Text = row("customer_name")
            tbCust.Focus()

            asyncLoadItems(row("customer_code"))
            asyncLoadSOTypes(row("customer_type"))
            'pnlCust.Visible = False

            tbSales.Text = row("sales_person")
            lbTOP.Text = row("top")
            If row("tax") <> "nt" Then
                taxPanel.Enabled = True
            Else
                taxPanel.Enabled = False
            End If

            _credit_limit = row("credit_limit")
            lbCreditLimit.Text = String.Format(numformat, _credit_limit)
            _on_credit = getOnCredit(row("customer_code"), "", injectAscend)
            lbOnCredit.Text = String.Format(numformat, _on_credit("credit_balance")) + " (payable: " + String.Format(numformat, _on_credit("balance")) + ")"

            _cust_code = row("customer_code")
            'Select Case row("customer_type")
            '    Case ""
            'End Select

            SOItemBuffer.Clear()
            lstSOItem.VirtualListSize = SOItemBuffer.Count

            calcSO()

            doEvent = True
            dpEstimate.Focus()
            dpEstimate_ValueChanged(sender, e)
        Else
            MsgBox("Nothing selected." + vbCrLf + "(OC-CSDC-01)")
        End If
    End Sub

    Private Sub tbExped_LostFocus(sender As Object, e As EventArgs) Handles tbExped.LostFocus
        If Not lstExped.Focused Then
            pnlExped.Visible = False
        End If
    End Sub

    Private Sub lstExped_LostFocus(sender As Object, e As EventArgs) Handles lstExped.LostFocus
        pnlExped.Visible = False
    End Sub

    Private Sub lstExped_DoubleClick(sender As Object, e As EventArgs) Handles lstExped.DoubleClick
        Dim item As ListViewItem
        Dim row As Dictionary(Of String, Object)

        If lstExped.SelectedIndices.Count = 1 Then
            item = lstExped.Items(lstExped.SelectedIndices(0))
            row = expdSearch(item.Index)

            doEvent = False
            tbExped.Text = row("vendor_name")

            _expd_code = row("vendor_code")
            _expd_ascend_id = row("ascend_id")

            tbExped.Focus()
            doEvent = True
        Else
            MsgBox("Nothing selected." + vbCrLf + "(OC-LEDC-01)")
        End If
    End Sub

    Private Sub lstItemSearch_DoubleClick(sender As Object, e As EventArgs) Handles lstItemSearch.DoubleClick
        Dim item As ListViewItem
        Dim row As Dictionary(Of String, Object)

        If lstItemSearch.SelectedIndices.Count = 1 Then
            item = lstItemSearch.Items(lstItemSearch.SelectedIndices(0))
            row = itemSearch(item.Index)

            'pnlItem.Visible = False
            doEvent = False
            tbItemSearch.Text = row("item_name")
            tbQuantity.Value = row("min_order")
            tbItemSearch.Focus()
            doEvent = True
        Else
            MsgBox("Nothing selected." + vbCrLf + "(OC-ISDC-01)")
        End If
    End Sub

    Private Sub lstItemSearch_LostFocus(sender As Object, e As EventArgs) Handles lstItemSearch.LostFocus
        pnlItem.Visible = False
    End Sub

    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        Dim item As ListViewItem
        Dim row As Dictionary(Of String, Object)
        'Dim c As ListViewItem
        Dim cust As Dictionary(Of String, Object)

        If lstItemSearch.SelectedIndices.Count <> 1 Then
            MsgBox("Nothing to add." + vbCrLf + "(OC-AIC-01)" + sender.ToString)
            Exit Sub
        ElseIf lstItemSearch.SelectedIndices.Count = 0 Then
            MsgBox("Nothing to add." + vbCrLf + "(OC-AIC-02)" + sender.ToString)
            Exit Sub
        End If

        item = lstItemSearch.Items(lstItemSearch.SelectedIndices(0))
        row = (From i In itemSearch(item.Index) Select i).ToDictionary(Function(i) i.Key, Function(i) i.Value)

        If UCase(row("item_name")) <> tbItemSearch.Text Then
            MsgBox("Please select Item first." + vbCrLf + "(OC-AIC-03)")
            Exit Sub
        End If

        If tbQuantity.Value = 0 Then
            MsgBox("Quantity cannot be zero." + vbCrLf + "(OC-AIC-04)")
            Exit Sub
        End If

        'c = lstCustSearch.Items(lstCustSearch.SelectedIndices(0))
        cust = (From c In custSearch Where c("customer_code") = _cust_code Select c).FirstOrDefault()

        row("quantity") = tbQuantity.Value
        Dim t As New Tax

        If row("price") = 0 Then
            Dim sp As New SpecialPrice()
            sp.tbPrice.Value = row("price")
            sp.lbBasePrice.Text = String.Format(numformat, row("price"))
            If sp.ShowDialog() = DialogResult.OK Then
                row("price") = sp.tbPrice.Value

                If row("price") = 0 Then
                    Dim result As Integer = MessageBox.Show("The price is 0", "Proceed anyway?", MessageBoxButtons.YesNo)
                    If result = DialogResult.No Then
                        Exit Sub
                    End If
                Else
                    dbCon.SQL("update item_customers set price = @p where company_code = @cc and item_code = @ic and customer_code = @cust")
                    dbCon.addParameter("@p", row("price"))
                    dbCon.addParameter("@cc", corp)
                    dbCon.addParameter("@ic", row("item_code"))
                    dbCon.addParameter("@cust", _cust_code)
                    dbCon.execute()
                End If
            Else
                Exit Sub
            End If
            sp.Dispose()
        End If

        t.total = tbQuantity.Value * row("price")
        t.calc(rbTaxExclusive.Checked, cust("tax_value"))
        row.Add("linetotal", t.net)
        row.Add("linebeforetax", t.total)
        row.Add("base_price", row("price"))
        row.Add("sales_price", row("price"))
        row.Add("note", If(row("quantity") < row("min_order"), "This item is below minimum order (" + String.Format(numformat, row("quantity")) + " < " + String.Format(numformat, row("min_order")) + ")", ""))

        SOItemBuffer.Add(row)
        lstSOItem.VirtualListSize = SOItemBuffer.Count

        doEvent = False
        tbItemSearch.Text = ""
        tbQuantity.Value = 0
        doEvent = True

        calcSO()
    End Sub

    Private Sub btnDelItem_Click(sender As Object, e As EventArgs) Handles btnDelItem.Click
        If lstSOItem.SelectedIndices.Count <> 1 Then
            MsgBox("Nothing to delete." + vbCrLf + "(OC-DI-01)")
            Exit Sub
        End If
        SOItemBuffer.RemoveAt(lstSOItem.SelectedIndices(0))
        lstSOItem.VirtualListSize = SOItemBuffer.Count
    End Sub

    Private Sub rbTax_CheckedChanged(sender As Object, e As EventArgs) Handles rbTaxInclusive.CheckedChanged, rbTaxExclusive.CheckedChanged
        If Not doEvent Then Exit Sub
        calcSO()
        calcItems()
    End Sub

    Private Sub tbCustPO_TextChanged(sender As Object, e As EventArgs) Handles tbCustPO.TextChanged
        If Not String.IsNullOrEmpty(tbCustPO.Text) Then
            dpCustPO.Enabled = True
        End If
    End Sub

    Private Sub lstSO_DoubleClick(sender As Object, e As EventArgs) Handles lstSO.DoubleClick
        EditForm()
    End Sub

    Private Sub dpDateFilter_ValueChanged(sender As Object, e As EventArgs) Handles dpDateFilter.ValueChanged, chkMonthFilter.CheckedChanged
        If Not doEvent Then Exit Sub

        asyncLoadSO()
    End Sub

    Private Sub llbLotAvail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbLotAvail.LinkClicked
        pnlLot.Location = New Point(llbLotAvail.Location.X, llbLotAvail.Location.Y - pnlLot.Height + 1)

        pnlLot.Visible = True
    End Sub

    Private Sub LinkLabel1_LostFocus(sender As Object, e As EventArgs) Handles llbLotAvail.LostFocus
        If Not lstLot.Focused Then
            pnlLot.Visible = False
        End If
    End Sub

    Private Sub lstLot_LostFocus(sender As Object, e As EventArgs) Handles lstLot.LostFocus
        pnlLot.Visible = False
    End Sub

    Private Sub lstSOItem_KeyUp(sender As Object, e As KeyEventArgs) Handles lstSOItem.KeyUp
        If e.KeyCode = Keys.Delete Then
            btnDelItem.PerformClick()
        End If
    End Sub

    Private Sub tbExped_TextChanged(sender As Object, e As EventArgs) Handles tbExped.TextChanged, tbExped.GotFocus
        If Not doEvent Then Exit Sub

        Dim tmp As IQueryable(Of Dictionary(Of String, Object))
        tmp = searchExpd(expdBuffer, tbExped.Text)
        If tmp IsNot Nothing Then
            expdSearch = tmp.ToList()
        Else
            expdSearch.Clear()
        End If
        lstExped.VirtualListSize = expdSearch.Count

        pnlExped.Location = New Point(tbExped.Location.X, tbExped.Location.Y + tbExped.Height - 1)

        pnlExped.Visible = True
    End Sub

    Private Sub itemLoadTick_Tick(sender As Object, e As EventArgs) Handles itemLoadTick.Tick
        _on_credit = getOnCredit(_cust_code, tbSONum.Text, injectAscend)
        asyncLoadItems(_cust_code)
        lbOnCredit.Text = String.Format(numformat, _on_credit("credit_balance")) + " (payable: " + String.Format(numformat, _on_credit("balance")) + ")"
        getUsedLot(tbSONum.Text)
        Dim lot As Dictionary(Of String, Object) = getLotInfo(DateValue(dpEstimate.Value))

        If Not IsNothing(lot) Then
            _max_lot = lot("max_order")
            _used_lot = lot("existing_order")
        Else
            _max_lot = 0D
            _used_lot = 0D
        End If

        loadSODetail(tbSONum.Text)
        calcSO()

        lbMaxLot.Text = String.Format(numformat, _max_lot)
        lbUsedLot.Text = String.Format(numformat, _used_lot)
        lbLotAvail.Text = String.Format(numformat, _max_lot - _used_lot)
        itemLoadTick.Enabled = False
    End Sub

    Private Sub lstLot_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstLot.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = lotBuffer(e.ItemIndex)
        e.Item = New ListViewItem(DirectCast(row("lot_date"), Date))
        e.Item.SubItems.Add(String.Format(numformat, row("max_order")))
        e.Item.SubItems.Add(String.Format(numformat, row("existing_order")))
        e.Item.SubItems.Add(String.Format(numformat, row("max_order") - row("existing_order")))
    End Sub

    Private Sub dpEstimate_ValueChanged(sender As Object, e As EventArgs) Handles dpEstimate.ValueChanged
        If Not doEvent Then Exit Sub

        'If _cust_code = "" Then
        '    MsgBox("Please select customer first." + vbCrLf + "(OC-DEC-02)")
        '    Exit Sub
        'End If

        Dim ds As DataRowView = cbSOType.SelectedItem
        Dim lead_date As Date

        If IsNothing(ds) Then Exit Sub

        Select Case ds.Item("lead_time_unit")
            Case "day"
                lead_date = dpSO.Value.AddDays(ds.Item("lead_time"))
            Case "month"
                lead_date = dpSO.Value.AddMonths(ds.Item("lead_time"))
        End Select

        If DateValue(lead_date) > DateValue(dpEstimate.Value) Then
            doEvent = False
            dpEstimate.Value = DateValue(lead_date)
            doEvent = True
            'kilang padi 8 hari
            MsgBox("Estimated delivery date must be more than " + ds.Item("lead_time").ToString + " " + ds.Item("lead_time_unit") + If(ds.Item("lead_time") > 1, "s", "") + "." + vbCrLf + "(OC-DEC-01)")
        End If
        If DateValue(dpExpect.Value) < DateValue(dpEstimate.Value) Then
            doEvent = False
            dpExpect.Value = DateValue(dpEstimate.Value)
            doEvent = True
        End If

        Dim lot As Dictionary(Of String, Object) = getLotInfo(DateValue(dpEstimate.Value))

        If Not IsNothing(lot) Then
            _max_lot = lot("max_order")
            _used_lot = lot("existing_order")
        Else
            _max_lot = 0D
            _used_lot = 0D
        End If

        calcSO()

        lbMaxLot.Text = String.Format(numformat, _max_lot)
        lbUsedLot.Text = String.Format(numformat, _used_lot)
        lbLotAvail.Text = String.Format(numformat, _max_lot - _used_lot)
    End Sub

    Private Sub lstLot_DoubleClick(sender As Object, e As EventArgs) Handles lstLot.DoubleClick
        Dim item As ListViewItem
        Dim row As Dictionary(Of String, Object)

        If lstLot.SelectedIndices.Count = 1 Then
            item = lstLot.Items(lstLot.SelectedIndices(0))
            row = lotBuffer(item.Index)

            dpEstimate.Value = Date.Parse(row("lot_date"))

            pnlLot.Visible = False
        End If
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        btnDelItem.PerformClick()
    End Sub

    Private Sub SpecialPriceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpecialPriceToolStripMenuItem.Click
        If lstSOItem.SelectedIndices.Count = 1 Then
            Dim item As ListViewItem = lstSOItem.Items(lstSOItem.SelectedIndices(0))
            Dim row As Dictionary(Of String, Object) = SOItemBuffer(item.Index)
            Dim cust As Dictionary(Of String, Object) = (From c In custBuffer Where c("customer_code") = _cust_code Select c).FirstOrDefault()
            Dim sp As New SpecialPrice()
            Dim t As New Tax

            sp.tbPrice.Maximum = Decimal.MaxValue
            sp.tbPrice.Value = row("sales_price")
            sp.lbBasePrice.Text = String.Format(numformat, row("base_price"))
            If sp.ShowDialog() = DialogResult.OK Then
                row("sales_price") = sp.tbPrice.Value

                t.total = row("quantity") * row("sales_price")
                t.calc(rbTaxExclusive.Checked, cust("tax_value"))
                row("linetotal") = t.net
                row("linebeforetax") = t.total

                lstSOItem.Invalidate()
                calcSO()
            End If
            sp.Dispose()
        Else
            MsgBox("No item selected." + vbCrLf + "(OC-SPMC-01)")
        End If
    End Sub

    Private Sub lstSOItem_DoubleClick(sender As Object, e As EventArgs) Handles lstSOItem.DoubleClick
        SpecialPriceToolStripMenuItem.PerformClick()
    End Sub

    Private Sub dpSO_ValueChanged(sender As Object, e As EventArgs) Handles dpSO.ValueChanged
        If Not doEvent Then Exit Sub

        If DateValue(dpSO.Value) < DateValue(Now) Then
            doEvent = False
            dpSO.Value = Now

            MsgBox("Cannot create back date order." + vbCrLf + "(OC-DSO-01)")
            doEvent = True
        End If
    End Sub

    Private Sub dpExpect_ValueChanged(sender As Object, e As EventArgs) Handles dpExpect.ValueChanged
        If Not doEvent Then Exit Sub

        If DateValue(dpExpect.Value) < DateValue(dpEstimate.Value) Then
            doEvent = False
            dpExpect.Value = dpEstimate.Value

            MsgBox("Estimated arrival date must be later than estimated delivery date." + vbCrLf + "(OC-DEXC-01)")
            doEvent = True
        End If
    End Sub

    Private Sub tbCust_KeyUp(sender As Object, e As KeyEventArgs) Handles tbCust.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            lstCustSearch.SelectedIndices.Add(0)
            lstCustSearch.Focus()
        End If
    End Sub

    Private Sub tbItemSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles tbItemSearch.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            lstItemSearch.SelectedIndices.Add(0)
            lstItemSearch.Focus()
            'ElseIf e.KeyCode = Keys.Enter Then
            '    btnAddItem.PerformClick()
        End If
    End Sub

    Private Sub tbExped_KeyUp(sender As Object, e As KeyEventArgs) Handles tbExped.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            lstExped.SelectedIndices.Add(0)
            lstExped.Focus()
        End If
    End Sub

    Private Sub lstCustSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles lstCustSearch.KeyUp
        If e.KeyCode = Keys.Enter Then
            lstCustSearch_DoubleClick(sender, e)
        End If
    End Sub

    Private Sub lstItemSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles lstItemSearch.KeyUp
        If e.KeyCode = Keys.Enter Then
            lstItemSearch_DoubleClick(sender, e)
        End If
    End Sub

    Private Sub lstExped_KeyUp(sender As Object, e As KeyEventArgs) Handles lstExped.KeyUp
        If e.KeyCode = Keys.Enter Then
            lstExped_DoubleClick(sender, e)
        End If
    End Sub

    Private Sub tbQuantity_KeyUp(sender As Object, e As KeyEventArgs) Handles tbQuantity.KeyUp
        'If e.KeyCode = Keys.Enter Then
        '    btnAddItem.PerformClick()
        'End If
    End Sub

    Private Sub itemSearchTick_Tick(sender As Object, e As EventArgs) Handles itemSearchTick.Tick
        'loadItems(_cust_code, tbItemSearch.Text)
        'itemSearchTick.Enabled = False
    End Sub

    Private Sub cbSOType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSOType.SelectedIndexChanged
        If Not doEvent Then Exit Sub

        dpEstimate_ValueChanged(sender, e)
    End Sub
End Class
