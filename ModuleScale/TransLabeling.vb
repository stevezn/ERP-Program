Imports DB
Imports System.IO
Imports ERPModules
Imports System.Threading
Imports System.Data.SQLite
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports RawPrinter
Imports System.Drawing.Printing
Imports System.Globalization

Public Class TransLabeling : Inherits ERPModule

    Public Class loadThread
        Private dbcon As DBConn
        Private mainThd As Object
        Private mainForm As Object
        Private month As Date = Nothing

        Private cust_code As String
        Private filter As String

        Private target As Object

        Private Delegate Sub progBarNotify(max As Integer, prog As Integer, txt As String)
        Private Delegate Sub progBarUpdate(dt As DataTable, target As Object, events As Boolean)

        Private pNotify As progBarNotify

        Private modNotifyEnd As progBarNotify
        Private modNotifyLoad As progBarUpdate

        Public Sub New(ByRef MainWindow As Object, ByRef main As Object, ByRef db As DBConn)
            mainForm = MainWindow
            dbcon = db
            mainThd = main

            pNotify = AddressOf MainWindow.progBarNotify

            modNotifyEnd = AddressOf main.progBarNotifyEnd
            modNotifyLoad = AddressOf main.progBarNotifyUpdate
        End Sub

        Public Sub New(ByRef MainWindow As Object, ByRef main As Object, ByRef db As DBConn, ByRef target As Object)
            mainForm = MainWindow
            dbcon = db
            mainThd = main
            Me.target = target

            pNotify = AddressOf MainWindow.progBarNotify

            modNotifyEnd = AddressOf main.progBarNotifyEnd
            modNotifyLoad = AddressOf main.progBarNotifyUpdate
        End Sub

        Public Sub loadAssemblyLines()
            Dim dt As New DataTable
            Dim cmd As Object

            If dbcon.state() = ConnectionState.Open Then
                If dbcon.getServerType() = "MSSQL" Then
                    cmd = New SqlCommand()
                Else
                    cmd = New MySqlCommand()
                End If
                dbcon.SQL("select line_code [code], line_name [name], ascend_id from assembly_lines where company_code = @cc", cmd)
                dbcon.addParameter("@cc", mainThd.corp, cmd)
                Try
                    dbcon.fillTable(dt, cmd)

                    Dim args(2) As Object
                    args(0) = dt
                    args(1) = target
                    args(2) = False
                    mainThd.Invoke(modNotifyLoad, args)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(OC-LSOT-01)")
            End If
        End Sub

        Public Sub loadShifts()
            Dim dt As New DataTable
            Dim cmd As Object

            If dbcon.state() = ConnectionState.Open Then
                If dbcon.getServerType() = "MSSQL" Then
                    cmd = New SqlCommand()
                Else
                    cmd = New MySqlCommand()
                End If
                dbcon.SQL("select shift_code [code], shift_name [name], shift_in, shift_out from shifts where company_code = @cc", cmd)
                dbcon.addParameter("@cc", mainThd.corp, cmd)
                Try
                    dbcon.fillTable(dt, cmd)

                    Dim args(2) As Object
                    args(0) = dt
                    args(1) = target
                    args(2) = False
                    mainThd.Invoke(modNotifyLoad, args)
                Catch ex As Exception
                    'MsgBox(ex.Message)
                End Try
            Else
                MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(TL-LS-01)")
            End If
        End Sub

    End Class

    Public Class syncThread
        Private rcon As DBConn
        Private mcon As DBConn
        Private lcon As SQLiteConnection
        Private mainForm As Object
        Private station As String
        Private corp As String

        Dim thdId As Integer

        Dim numformat As String = "{0:#,0.##}"

        Private Delegate Sub progBarNotify(max As Integer, prog As Integer, txt As String)
        Private pNotify As progBarNotify

        Public Sub New(ByRef MainWindow As Object, mainDb As DBConn, ByRef localDb As SQLiteConnection, corp As String, station As String)
            mcon = mainDb
            lcon = localDb
            mainForm = MainWindow
            Me.corp = corp
            Me.station = station

            If mainForm.syncing.Count = 0 Then
                thdId = 0
            Else
                thdId = (Enumerable.Max(mainForm.syncing) + 1) Mod Integer.MaxValue
            End If
            mainForm.syncing.Add(thdId)

            pNotify = AddressOf MainWindow.progBarNotify
        End Sub

        Public Sub Dispose()
            mainForm.syncing.Remove(thdId)
        End Sub

        Public Sub sync(table As String)
            Dim cmd As SQLiteCommand
            Dim cmdStr As String = ""
            Dim str As String, tmp As String
            Dim ds As New DataSet
            Dim rdr As Object
            Dim row As Dictionary(Of String, Object) = Nothing
            Dim args(2) As Object

            While mainForm.syncing(0) <> thdId
                Thread.Sleep(100)
                'wait for other sync process
            End While

            If mcon.state <> ConnectionState.Open Then
                mainForm.syncing.Remove(thdId)
                Exit Sub
            End If

            cmd = lcon.CreateCommand()

            Dim cmdRdr = mcon.SQLReader("select sql_list, sql_insert, sql_update, db_host, db_name, db_user, db_pass from syncs where company_code = @cc and table_name = @tb")
            mcon.addParameter("@cc", corp, cmdRdr)
            mcon.addParameter("@tb", table, cmdRdr)
            Try
                rdr = mcon.beginRead(cmdRdr)
                If mcon.doRead(rdr) Then
                    row = mcon.getRow(rdr)
                End If
                mcon.endRead(rdr)
            Catch ex As Exception
                mainForm.syncing.Remove(thdId)
                Exit Sub
            End Try

            If Not IsNothing(row) Then
                rcon = New DBConn()
                rcon.init("MSSQL", "Server=" + row("db_host") + ";Database=" + row("db_name") + ";User Id=" +
                                                        row("db_user") + ";Password=" + row("db_pass") + ";")
                Try
                    rcon.open()

                    If rcon.state = ConnectionState.Open Then
                        rcon.SQL(row("sql_list"))
                        rcon.addParameter("@cc", corp)
                        rcon.addParameter("@sc", station)
                        rcon.fillData(ds)

                        If ds.Tables.Count = 1 Then
                            args(0) = ds.Tables(0).Rows.Count
                            args(1) = 0

                            Select Case table
                                Case "employees"
                                    args(2) = "Syncing Employees"
                                    cmdStr = "REPLACE INTO employees(company_code, employee_code, employee_id, full_name, workgroup_id, workgroup, department_id, department, status) values(?, ?, ?, ?, ?, ?, ?, ?, ?)"
                                Case "customers"
                                    args(2) = "Syncing Customers"
                                    cmdStr = "REPLACE INTO customers(company_code, customer_code, customer_id, customer_name, status) values(?, ?, ?, ?, ?)"
                                Case "items"
                                    args(2) = "Syncing Items"
                                    cmdStr = "REPLACE INTO items(company_code, item_code, item_id, item_name, line, warna, denier, anyaman, lebar, panjang, status) values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
                                Case "sales_orders"
                                    args(2) = "Syncing Sales Orders"
                                    cmdStr = "REPLACE INTO sales_orders() values()"
                                Case "bobbins"
                                    args(2) = "Syncing Bobbins"
                                    cmdStr = "REPLACE INTO bobbins(company_code, bobbin_code, bobbin, weight) values(?, ?, ?, ?)"
                                Case "shifts"
                                    args(2) = "Syncing Shifts"
                                    cmdStr = "REPLACE INTO shifts(company_code, shift_code, shift_name, shift_in, shift_out) values(?, ?, ?, ?, ?)"
                                Case "transfer_details"
                                    args(2) = "Syncing Transfers"
                                    cmdStr = "REPLACE INTO transfer_details(company_code, area_code, station_code, doc_number, date, shift, from_line, to_line, operator, `type`, src_doc_number, synced, void, void_by) 
                                                values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 1, ?, ?)"
                            End Select

                            mainForm.Invoke(pNotify, args)
                            For Each r In ds.Tables(0).Rows
                                While mainForm.syncing(0) <> thdId
                                    Thread.Sleep(100)
                                    'wait for other sync process
                                End While

                                rcon.clearParameter()
                                If (IsDBNull(r.Item("last_sync")) And r.Item("status") = 1) Or Not IsDBNull(r.Item("last_sync")) Then
                                    'insert row
                                    cmd.CommandText = cmdStr
                                    cmd.Parameters.AddWithValue("company_name", corp)

                                    Select Case table
                                        Case "employees"
                                            cmd.Parameters.AddWithValue("employee_code", r.Item("employeecode"))
                                            cmd.Parameters.AddWithValue("employee_id", r.Item("employeeid"))
                                            cmd.Parameters.AddWithValue("full_name", r.Item("fullname"))
                                            cmd.Parameters.AddWithValue("workgroup_id", r.Item("workgroupid"))
                                            cmd.Parameters.AddWithValue("workgroup", r.Item("workgroupname"))
                                            cmd.Parameters.AddWithValue("department_id", r.Item("departmentid"))
                                            cmd.Parameters.AddWithValue("department", r.Item("departmentname"))
                                        Case "customers"
                                            cmd.Parameters.AddWithValue("customer_code", r.Item("customercode"))
                                            cmd.Parameters.AddWithValue("customer_id", r.Item("customerid"))
                                            cmd.Parameters.AddWithValue("customer_name", r.Item("customername"))
                                        Case "items"
                                            cmd.Parameters.AddWithValue("item_code", r.Item("itemcode"))
                                            cmd.Parameters.AddWithValue("item_id", r.Item("itemid"))
                                            str = r.Item("itemname") + If(Not IsDBNull(r.Item("customername")), " | " + r.Item("customername"), "")
                                            str = str + If(Not IsDBNull(r.Item("kapasitas")), " | Uk: " + String.Format(numformat, r.Item("klebar")) + "x" + String.Format(numformat, r.Item("kpanjang")), "")
                                            str = str + If(Not IsDBNull(r.Item("denier")), " | Den: " + String.Format(numformat, r.Item("denier")), "")
                                            str = str + If(If(IsDBNull(r.Item("inner")), False, r.Item("inner")), " | Inn: " + String.Format(numformat, r.Item("ilebar")) + "x" + String.Format(numformat, r.Item("ipanjang")), "")
                                            tmp = str.Replace("  ", " ")
                                            While tmp <> str
                                                str = tmp
                                                tmp = str.Replace("  ", " ")
                                            End While
                                            cmd.Parameters.AddWithValue("item_name", str)
                                            cmd.Parameters.AddWithValue("line", r.item("line"))
                                            cmd.Parameters.AddWithValue("warna", r.item("warna"))
                                            cmd.Parameters.AddWithValue("denier", r.item("denier"))
                                            cmd.Parameters.AddWithValue("anyaman", r.item("anyaman"))
                                            cmd.Parameters.AddWithValue("lebar", r.item("klebar"))
                                            cmd.Parameters.AddWithValue("panjang", r.item("kpanjang"))
                                        Case "sales_orders"
                                        Case "bobbins"
                                            cmd.Parameters.AddWithValue("bobbin_code", r.Item("bobbin_code"))
                                            cmd.Parameters.AddWithValue("bobbin", r.Item("bobbin"))
                                            cmd.Parameters.AddWithValue("weight", r.Item("weight"))
                                        Case "shifts"
                                            cmd.Parameters.AddWithValue("shift_code", r.Item("shift_code"))
                                            cmd.Parameters.AddWithValue("shift_name", r.Item("shift_name"))
                                            cmd.Parameters.AddWithValue("shift_in", r.Item("shift_in"))
                                            cmd.Parameters.AddWithValue("shift_out", r.Item("shift_out"))
                                        Case "transfer_details"
                                            cmd.Parameters.AddWithValue("area_code", r.Item("area_code"))
                                            cmd.Parameters.AddWithValue("station_code", r.Item("station_code"))
                                            cmd.Parameters.AddWithValue("doc_number", r.Item("doc_number"))
                                            cmd.Parameters.AddWithValue("date", r.Item("date"))
                                            cmd.Parameters.AddWithValue("shift", r.Item("shift"))
                                            cmd.Parameters.AddWithValue("from_line", r.Item("from_line"))
                                            cmd.Parameters.AddWithValue("to_line", r.Item("to_line"))
                                            cmd.Parameters.AddWithValue("operator", r.Item("operator"))
                                            cmd.Parameters.AddWithValue("type", r.Item("type"))
                                            cmd.Parameters.AddWithValue("src_doc_number", r.Item("src_doc_number"))
                                            cmd.Parameters.AddWithValue("void", r.Item("void"))
                                            cmd.Parameters.AddWithValue("void_by", r.Item("void_by"))

                                            Dim rdb As New DBConn()
                                            rdb.init("MSSQL", "Server=" + row("db_host") + ";Database=" + row("db_name") + ";User Id=" +
                                                        row("db_user") + ";Password=" + row("db_pass") + ";")
                                            rdb.SQL("SELECT * FROM transfer_detail_items WHERE trans_doc_number = @tdn")
                                            rdb.addParameter("@tdn", r.Item("doc_number"))
                                            Dim ds2 As New DataSet
                                            rdb.fillData(ds2)

                                            Dim cmd2 As SQLiteCommand
                                            cmd2 = lcon.CreateCommand()
                                            cmd2.CommandText = "DELETE FROM transfer_detail_items WHERE trans_doc_number = ?"
                                            cmd2.Parameters.AddWithValue("trans_doc_number", r.Item("doc_number"))
                                            cmd2.ExecuteNonQuery()

                                            For Each t2 As DataTable In ds2.Tables
                                                For Each r2 As DataRow In t2.Rows

                                                    cmd2.CommandText = "INSERT INTO transfer_detail_items(trans_doc_number, anyaman, denier, lebar, panjang, warna, cetakan, opp, qty, item_code, item_name, uom, netto, src_item_number) 
                                                values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

                                                    cmd2.Parameters.Clear()
                                                    cmd2.Parameters.AddWithValue("trans_doc_number", r.Item("doc_number"))
                                                    cmd2.Parameters.AddWithValue("anyaman", r2.Item("anyaman"))
                                                    cmd2.Parameters.AddWithValue("denier", r2.Item("denier"))
                                                    cmd2.Parameters.AddWithValue("lebar", r2.Item("lebar"))
                                                    cmd2.Parameters.AddWithValue("panjang", r2.Item("panjang"))
                                                    cmd2.Parameters.AddWithValue("warna", r2.Item("warna"))
                                                    cmd2.Parameters.AddWithValue("cetakan", r2.Item("cetakan"))
                                                    cmd2.Parameters.AddWithValue("opp", r2.Item("opp"))
                                                    cmd2.Parameters.AddWithValue("qty", r2.Item("qty"))
                                                    cmd2.Parameters.AddWithValue("item_code", r2.Item("item_code"))
                                                    cmd2.Parameters.AddWithValue("item_name", r2.Item("item_name"))
                                                    cmd2.Parameters.AddWithValue("uom", r2.Item("uom"))
                                                    cmd2.Parameters.AddWithValue("netto", r2.Item("netto"))
                                                    cmd2.Parameters.AddWithValue("src_item_number", r2.Item("src_item_number"))
                                                    cmd2.ExecuteNonQuery()
                                                Next
                                            Next
                                            cmd2.Dispose()
                                            ds2.Dispose()
                                            rdb.close()
                                            rdb.Dispose()
                                    End Select

                                    cmd.Parameters.AddWithValue("status", r.Item("status"))
                                    cmd.ExecuteNonQuery()

                                    If Not IsDBNull(r.Item("last_sync")) Then
                                        rcon.SQL(row("sql_update"))
                                    Else
                                        rcon.SQL(row("sql_insert"))
                                    End If
                                    rcon.addParameter("@cc", corp)

                                    Select Case table
                                        Case "employees"
                                            rcon.addParameter("@ec", r.Item("employeecode"))
                                            rcon.addParameter("@ei", r.Item("employeeid"))
                                        Case "customers"
                                            rcon.addParameter("@cust", r.Item("customercode"))
                                            rcon.addParameter("@custid", r.Item("customerid"))
                                        Case "items"
                                            rcon.addParameter("@ic", r.Item("itemcode"))
                                            rcon.addParameter("@ii", r.Item("itemid"))
                                        Case "sales_orders"
                                        Case "bobbins"
                                            rcon.addParameter("@bc", r.Item("bobbin_code"))
                                        Case "shifts"
                                            rcon.addParameter("@shc", r.Item("shift_code"))
                                        Case "transfer_details"
                                            rcon.addParameter("@ac", r.Item("area_code"))
                                            rcon.addParameter("@rsc", r.Item("station_code"))
                                            rcon.addParameter("@dn", r.Item("doc_number"))
                                    End Select

                                    rcon.addParameter("@sc", station)
                                    rcon.addParameter("@ls", Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                    rcon.execute()
                                End If

                                args(1) = args(1) + 1
                                mainForm.Invoke(pNotify, args)
                                If args(1) Mod 100 = 0 And mainForm.syncing.Count > 1 Then
                                    mainForm.syncing.Remove(thdId)
                                    Thread.Sleep(100)
                                    mainForm.syncing.Add(thdId)
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    mainForm.syncing.Remove(thdId)
                    Exit Sub
                End Try

                rcon.close()
            End If

            ds.Dispose()

            mainForm.syncing.Remove(thdId)
            cmd.Dispose()
        End Sub
    End Class

    Public Sub progBarNotifyEnd()
    End Sub

    Public Sub progBarNotifyUpdate(dt As DataTable, target As Object, events As Boolean)
        target.DataSource = dt
        target.DisplayMember = "name"
        target.ValueMember = "code"
    End Sub

    'Private Sub asyncLoadShifts()
    '    Dim loadThd2 As loadThread
    '    loadThd2 = New loadThread(_parent, Me, dbCon, cbShifts)
    '    Dim thd As New Thread(AddressOf loadThd2.loadShifts)
    '    thd.IsBackground = True
    '    thd.Start()
    'End Sub

    Private Sub loadShifts()
        If localCon.State = ConnectionState.Open Then
            Dim cmd As SQLiteCommand = localCon.CreateCommand()
            cmd.CommandText = "select * from shifts order by shift_code"
            Dim adp As New SQLiteDataAdapter(cmd)
            Dim dt As New DataTable
            adp.Fill(dt)
            cbShifts.DataSource = dt
            cbShifts.DisplayMember = "shift_name"
            cbShifts.ValueMember = "shift_code"
        End If
    End Sub

    Private buffer As String = ""
    Private continuous As Boolean = True
    Private dataLength As Integer
    Private readStart As Integer
    Private readEnd As Integer
    Private unitRatio As Decimal
    Private precision As Integer
    Private startChar As Char
    Private endChar As Char
    Private active_scale As String = ""
    Private barcode_printer As String = ""

    Public localCon As SQLiteConnection

    Private textFocus As TextBox
    Private textCode As TextBox
    Private datacol As Integer = 0
    Private textcol As Integer = 0

    Public injectAscend As Boolean = False
    Public ascendConStr As String
    Private ascendCon As DBConn

    Dim scaleObj As ScaleClass

    Public Shadows canOffline As Boolean = True

    Private papersize As PaperSize

    Private searchFrom As Date = Today
    Private searchTo As Date = Today
    Private searchData As New List(Of ListViewItem)

    Private filterData As New List(Of ListViewItem)

    Private srcDoc As String = ""

    Private Sub clearForm()
        'Dim _now As Date = Now

        tbFromDocNum.Text = ""
        tbAnyaman.Text = ""
        tbDenier.Text = ""
        tbLebar.Text = ""
        tbPanjang.Text = ""
        tbWarna.Text = ""
        tbCetakan.Text = ""
        tbJumlah.Text = ""
        cbUOM.SelectedIndex = -1
        tbNetto.Text = ""

        'toolButton("Print").enabled = False
        '_parent.refreshMenuState(toolButton)
    End Sub

    Private Sub openData()
        Dim conStr As String
        Dim table As String
        Dim cmd As SQLiteCommand
        Try
            If Not File.Exists("localDB.sqlite") Then
                SQLiteConnection.CreateFile("localDB.sqlite")
            End If

            conStr = "Data Source=localDB.sqlite;Version=3;New=False;Compress=True;"
            localCon = New SQLiteConnection(conStr)
            localCon.Open()

            cmd = localCon.CreateCommand()

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='used_devices'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `used_devices` (
	                                    `company_code`	TEXT,
	                                    `device`	TEXT,
	                                    `table_name`	TEXT,
	                                    `device_id`	INTEGER,
	                                    PRIMARY KEY(device,table_name)
                                    )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='scales'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `scales` (
	                                 `id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	                                 `company_code`	TEXT,
	                                 `code`	TEXT,
	                                 `name`	TEXT,
	                                 `port`	TEXT,
	                                 `config`	TEXT,
	                                 `continuous`	INTEGER,
	                                 `datalength`	INTEGER,
	                                 `startchar`	TEXT,
	                                 `endchar`	TEXT,
	                                 `readstart`	INTEGER,
	                                 `readend`	INTEGER,
	                                 `precision`	INTEGER,
	                                 `unitratio`	REAL,
                                     `last_update` TEXT
                                )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='printers'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `printers` (
	                                 `id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	                                 `company_code`	TEXT,
	                                 `code`	TEXT,
	                                 `name`	TEXT,
                                     `last_update` TEXT
                                )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='customers'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `customers` (
                                     `company_code`	TEXT,
                                     `customer_code`	TEXT,
                                     `customer_id`	INTEGER,
                                     `customer_name`	TEXT,
                                     `status`	INTEGER,
                                     PRIMARY KEY(company_code,customer_code)
                                )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='employees'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `employees` (
	                                `company_code`	TEXT,
	                                `employee_code`	TEXT,
	                                `employee_id`	INTEGER,
	                                `full_name`	TEXT,
	                                `workgroup_id`	INTEGER,
	                                `workgroup`	TEXT,
	                                `department_id`	INTEGER,
	                                `department`	TEXT,
	                                `status`	INTEGER,
	                                PRIMARY KEY(company_code,employee_code)
                                )"
                cmd.ExecuteNonQuery()
            End If

            'cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='item_weight'"
            'table = cmd.ExecuteScalar()
            'If table = "" Then
            '    cmd.CommandText = "CREATE TABLE `item_weight` (
            '                     `company_code`	TEXT,
            '                     `weight_number`	TEXT,
            '                     `assembly_line`	TEXT,
            '                     `so_number`	TEXT,
            '                     `customer_code`	TEXT,
            '                     `customer_name`	TEXT,
            '                     `labor_code`	TEXT,
            '                     `labor_name`	TEXT,
            '                     `shift`	TEXT,
            '                     `team`	TEXT,
            '                     `item_code`	TEXT,
            '                     `item_name`	TEXT,
            '                     `machine_code`	TEXT,
            '                     `machine_name`	TEXT,
            '                     `weight`	NUMERIC,
            '                     `synced`	INTEGER DEFAULT 0,
            '                     PRIMARY KEY(company_code,weight_number)
            '                    )"
            '    cmd.ExecuteNonQuery()
            'End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='sales_orders'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `sales_orders` (
	                                `company_code`	TEXT,
	                                `so_number`	TEXT,
	                                `so_id`	INTEGER,
	                                `so_date`	TEXT,
	                                `customer_po`	TEXT,
	                                `status`	INTEGER,
	                                PRIMARY KEY(company_code,so_number)
                                )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='items'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `items` (
	                                `company_code`	TEXT,
	                                `item_code`	TEXT,
	                                `item_id`	INTEGER,
	                                `item_name`	INTEGER,
	                                `status`	INTEGER,
	                                `line`	TEXT,
	                                `warna`	TEXT,
	                                `denier`	NUMERIC,
	                                `anyaman`	NUMERIC,
	                                `lebar`	NUMERIC,
	                                `panjang`	NUMERIC,
	                                PRIMARY KEY(company_code,item_code)
                                )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='shifts'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `shifts` (
	                                `company_code`	TEXT,
	                                `shift_code`	TEXT,
	                                `shift_name`	TEXT,
	                                `shift_in`	TEXT,
	                                `shift_out`	TEXT,
	                                PRIMARY KEY(company_code,shift_code)
                                )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='transfer_details'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `transfer_details` (
	                                `company_code`	TEXT,
	                                `area_code`	TEXT,
	                                `station_code`	TEXT,
	                                `doc_number`	TEXT,
	                                `date`	TEXT,
	                                `shift`	TEXT,
	                                `from_line`	TEXT,
	                                `to_line`	TEXT,
	                                `operator`	TEXT,
	                                `type`	TEXT,
	                                `src_doc_number`	TEXT,
	                                `synced`	INTEGER NOT NULL DEFAULT 0,
                                    `void`  INTEGER NOT NULL DEFAULT 0,
                                    `void_by` TEXT,
	                                PRIMARY KEY(company_code,area_code,station_code,doc_number)
                                )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='transfer_detail_items'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `transfer_detail_items` (
	                                `trans_doc_number`	TEXT,
	                                `anyaman`	NUMERIC,
	                                `denier`	NUMERIC,
	                                `lebar`	NUMERIC,
	                                `panjang`	NUMERIC,
	                                `warna`	TEXT,
	                                `cetakan`	TEXT,
	                                `opp`	NUMERIC,
	                                `qty`	NUMERIC,
	                                `item_code`	TEXT,
	                                `item_name`	TEXT,
                                    `uom`   TEXT,
                                    `netto` NUMERIC,
                                    `src_item_number`   TEXT
                                )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='bobbins'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `bobbins` (
	                                `company_code`	TEXT,
	                                `bobbin_code`	TEXT,
	                                `bobbin`	TEXT,
	                                `weight`	NUMERIC,
	                                PRIMARY KEY(company_code,bobbin_code)
                                )"
                cmd.ExecuteNonQuery()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub closeData()
        Try
            If localCon.State = ConnectionState.Open Then
                localCon.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub syncData()
        If Not IsNothing(dbCon) Then
            sync("bobbins", lblStationEX.Text)
            sync("shifts", lblStationEX.Text)
            sync("transfer_details", lblStationEX.Text)
            sync("customers", lblStationEX.Text)
            sync("employees", lblStationEX.Text)
            sync("items", lblStationEX.Text)
        End If

        loadShifts()
    End Sub

    Private Function checkPrinter(ByRef printer As String) As Boolean
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As DataSet

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand()

            cmd.CommandText = "SELECT * FROM `printers` where company_code = ?"
            cmd.Parameters.AddWithValue("company_code", corp)
            adp = New SQLiteDataAdapter(cmd)
            ds = New DataSet
            adp.Fill(ds)

            adp.Dispose()
            cmd.Dispose()

            If ds.Tables(0).Rows.Count = 1 Then
                Dim ps As New PrinterSettings
                ps.PrinterName = ds.Tables(0).Rows(0).Item("name")
                If ps.IsValid() Then
                    printer = ds.Tables(0).Rows(0).Item("name")
                End If

                ds.Dispose()
                If printer <> "" Then
                    Return True
                Else
                    Return False
                End If
            Else
                ds.Dispose()
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Function checkScale(ByRef scale As DataRow) As Boolean
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As DataSet

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand()

            cmd.CommandText = "SELECT s.* FROM `scales` s join `used_devices` ud on s.id = ud.device_id and s.company_code = ud.company_code and ud.table_name = ? where s.company_code = ?"
            cmd.Parameters.AddWithValue("table_name", "scales")
            cmd.Parameters.AddWithValue("company_code", corp)
            adp = New SQLiteDataAdapter(cmd)
            ds = New DataSet
            adp.Fill(ds)

            adp.Dispose()
            cmd.Dispose()

            If ds.Tables(0).Rows.Count = 1 Then
                scale = ds.Tables(0).Rows(0)
                ds.Dispose()
                Return True
            Else
                ds.Dispose()
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub init()
        Dim scale As DataRow = Nothing
        Dim tool As ToolStripItem
        Dim btn As ERPModules.buttonState

        _parent = FindForm()
        DirectCast(_parent, Form).WindowState = FormWindowState.Maximized

        tool = New ToolStripMenuItem()
        tool.Text = "Get Data From Server"

        AddHandler tool.Click, AddressOf syncData

        toolMenu.Add(tool.Text, tool)

        tool = New ToolStripSeparator()
        toolMenu.Add("separator", tool)

        tool = New ToolStripMenuItem()
        tool.Text = "Rekap Data"

        AddHandler tool.Click, AddressOf rekapData

        toolMenu.Add(tool.Text, tool)

        btn = New ERPModules.buttonState
        btn.enabled = False
        btn.visible = True
        toolButton.Add("Print", btn)

        lblOperator.Text = user

        openData()


        If File.Exists("station.txt") Then
            lblStationEX.Text = UCase(File.ReadAllText("station.txt"))
            syncData()
        Else
            lblStationEX.Text = "UNKNOWN STATION"
            loadShifts()
        End If

        loadData()

        Dim pd As New PrintDocument

        For Each ps As PaperSize In pd.PrinterSettings.PaperSizes
            If ps.PaperName = "FAKTUR" Then
                Me.papersize = ps
            End If
        Next

        If Me.papersize Is Nothing Then
            Try
                Printer.AddCustomPaperSizeToDefaultPrinter("FAKTUR", 215, 140)
                For Each ps As PaperSize In pd.PrinterSettings.PaperSizes
                    If ps.PaperName = "FAKTUR" Then
                        Me.papersize = ps
                        Exit For
                    End If
                Next
            Catch ex As Exception
            End Try
        End If

        syncTick.Enabled = True
    End Sub

    Public Overloads Sub Dispose()
        Try
            closeData()
            toolMenu.Clear()

            MyBase.Dispose()
        Catch
        End Try
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
    End Sub

    Private Sub dateTick_Tick(sender As Object, e As EventArgs) Handles dateTick.Tick
        Dim d As DateTime = Now
        lblDate.Text = d.ToShortDateString() + " " + d.ToShortTimeString
    End Sub

    Private Function getNextNumber(line_code As String, station_code As String) As String
        Dim station As String = station_code.Substring(station_code.IndexOf("-") + 1)
        Dim cmd As SQLiteCommand
        cmd = localCon.CreateCommand()
        cmd.CommandText = "SELECT ifnull(substr(MAX(substr(`doc_number`, -11)), -4), 0)+1 FROM transfer_details WHERE `date` like ? AND company_code = ? and doc_number like ? and station_code = ?"
        cmd.Parameters.AddWithValue("date", Now.ToString("yyyy-MM") + "%")
        cmd.Parameters.AddWithValue("company_code", corp)
        cmd.Parameters.AddWithValue("line_code", line_code + "%")
        cmd.Parameters.AddWithValue("station_code", station_code)
        Dim num As Integer = cmd.ExecuteScalar()

        Return line_code + "-" + station + "-" + Now.ToString("yyyyMM") + "-" + Strings.Right("0000" + num.ToString(), 4)
    End Function

    Private Sub ScaleLabeling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        init()
    End Sub

    Private Sub Find_Click(sender As Object, e As EventArgs) Handles btnItemFind.Click
        Dim x As Integer, y As Integer, h As Integer

        x = DirectCast(sender, Button).Left
        y = DirectCast(sender, Button).Top
        h = DirectCast(sender, Button).Height

        Select Case DirectCast(sender, Button).Name
            Case "btnSONumFind"
            '    textFocus = tbSONum
            Case "btnCustFind"
            '    textFocus = tbCust
            'Case "btnLaborFind"
            '    textFocus = tbLabor
            Case "btnItemFind"
                textFocus = tbItem
        End Select

        lstFind.Left = x - 176 + gbLabel.Left
        lstFind.Top = y + h + gbLabel.Top

        lstFind.Visible = True
        textFocus.Focus()
    End Sub

    Private Sub sync(table As String, station_code As String)
        Dim syncThd As New syncThread(_parent, dbCon, localCon, corp, station_code)
        Dim thd As Thread = Nothing
        Select Case table
            Case "employees", "customers", "items", "shifts", "bobbins", "transfer_details"
                thd = New Thread(Sub() syncThd.sync(table))
                thd.Start()
                'Case "sales_orders"
                '    thd = New Thread(AddressOf syncThd.syncSO)
                '    thd.Start()
            Case Else
                syncThd.Dispose()
        End Select
    End Sub

    Private Sub find(key As String, table As String, targetCode As TextBox, targetName As TextBox)
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As DataSet
        'Dim row As Dictionary(Of String, Object) = Nothing

        cmd = localCon.CreateCommand()

        Select Case table
            Case "employees"
                cmd.CommandText = "SELECT * FROM `" + table + "` where company_code = ? and (employee_code like ? or employee_name like ?) LIMIT 0, 10"
            Case "customers"
                cmd.CommandText = "SELECT * FROM `" + table + "` where company_code = ? and (customer_code like ? or customer_name like ?) LIMIT 0, 10"
        End Select

        cmd.Parameters.AddWithValue("company_code", corp)

        Select Case table
            Case "employees"
                cmd.Parameters.AddWithValue("employee_code", "%" + key + "%")
                cmd.Parameters.AddWithValue("employee_name", "%" + key + "%")
            Case "customers"
                cmd.Parameters.AddWithValue("customer_code", "%" + key + "%")
                cmd.Parameters.AddWithValue("customer_name", "%" + key + "%")
        End Select

        adp = New SQLiteDataAdapter(cmd)
        ds = New DataSet
        adp.Fill(ds)

        textFocus = targetName
        textCode = targetCode

    End Sub

    Private Sub tbSONum_TextChanged(sender As Object, e As EventArgs)
        If lstFind.View = True Then
            'find
        End If
    End Sub

    Private Sub lblStation_Click(sender As Object, e As EventArgs) Handles lblStationEX.Click
        Dim station As New Station()

        If lblStationEX.Text <> "UNKNOWN" Then
            station.tbStation.Text = lblStationEX.Text
        End If
        If station.ShowDialog = DialogResult.OK Then
            lblStationEX.Text = station.tbStation.Text
            If File.Exists("station.txt") Then
                File.Delete("station.txt")
            End If
            File.WriteAllText("station.txt", station.tbStation.Text)

            If MessageBox.Show("You have changed the Station Name, would you like to resync data from server?", "Sync Data", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                syncData()
            End If
        End If

        station.Dispose()
    End Sub

    Private Sub addList()

    End Sub

    Private Function lineCode(line As String) As String
        Select Case line
            Case "Extruder"
                Return "EXT"
            Case "Circular Loom"
                Return "CL"
            Case "Cutting Sewing"
                Return "CS"
            Case "Insert Inner"
                Return "INR"
            Case "Printing"
                Return "PRT"
            Case "OPP"
                Return "OPP"
            Case "Packing"
                Return "PCK"
            Case "Jahit"
                Return "JHT"
                'Added by #Steve
            Case "Cutting Inner"
                Return "CIN"
                'end here
        End Select
        Return ""
    End Function

    Private Function itemName(anyaman As Decimal, denier As Decimal, lebar As Decimal, panjang As Decimal) As String
        Dim res As String = ""
        If lebar <> 0 Then
            res = lebar.ToString()
            If panjang <> 0 Then
                res += "x" + panjang.ToString()
            End If
        End If
        If anyaman <> 0 Then
            res += If(res <> "", " ", "") + anyaman.ToString()
            If denier <> 0 Then
                res += "/"
            End If
        End If
        If denier <> 0 Then
            res += If(res = "", "D ", "") + denier.ToString()
        End If
        Return res
    End Function

    Private Function itemType(cetakan As String, warna As String, opp As Decimal) As String
        Dim res As String = ""
        If cetakan <> "" And cetakan IsNot Nothing Then
            res = "CETAKAN " + cetakan
        End If
        If warna <> "" And warna IsNot Nothing Then
            res = If(res <> "", " ", "") + warna
        End If
        If opp <> 0 Then
            res = If(res <> "", " ", "") + opp.ToString() + " MICRON"
        End If
        Return res
    End Function

    Private Sub printLabel()
        Dim cmd As SQLiteCommand
        Dim docnum As String = ""

        If localCon.State = ConnectionState.Open Then
            Dim data As New Dictionary(Of String, Object)
            Dim line As String = lineCode(cbFromLine.Text) + "-" + lineCode(cbToLine.Text)

            docnum = getNextNumber(line, lblStationEX.Text)

            cmd = localCon.CreateCommand()
            cmd.CommandText = "INSERT INTO transfer_details(company_code, area_code, station_code, 
                                                                doc_number, date, shift, from_line, to_line, operator, type, src_doc_number) 
                                VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("area_code", area)
            cmd.Parameters.AddWithValue("station_code", lblStationEX.Text)
            cmd.Parameters.AddWithValue("doc_number", docnum)
            cmd.Parameters.AddWithValue("date", Now.ToString("yyyy-MM-dd HH:mm:ss"))
            cmd.Parameters.AddWithValue("shift", cbShifts.Text)
            cmd.Parameters.AddWithValue("from_line", lineCode(cbFromLine.Text))
            cmd.Parameters.AddWithValue("to_line", lineCode(cbToLine.Text))
            cmd.Parameters.AddWithValue("operator", lblOperator.Text)
            cmd.Parameters.AddWithValue("type", If(rbIn.Checked, "IN", "OUT"))
            cmd.Parameters.AddWithValue("src_doc_number", If(rbIn.Checked, srcDoc, ""))

            Try
                cmd.ExecuteNonQuery()

                For Each item As ListViewItem In lstItems.Items
                    cmd.CommandText = "INSERT INTO transfer_detail_items(trans_doc_number, anyaman, denier, lebar, panjang, 
                                                                        warna, cetakan, opp, qty, item_code, item_name, uom, netto, src_item_number) 
                                        VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("trans_doc_number", docnum)
                    cmd.Parameters.AddWithValue("anyaman", item.SubItems(0).Text)
                    cmd.Parameters.AddWithValue("denier", item.SubItems(1).Text)
                    cmd.Parameters.AddWithValue("lebar", item.SubItems(2).Text)
                    cmd.Parameters.AddWithValue("panjang", item.SubItems(3).Text)
                    cmd.Parameters.AddWithValue("warna", item.SubItems(4).Text)
                    cmd.Parameters.AddWithValue("cetakan", item.SubItems(5).Text)
                    cmd.Parameters.AddWithValue("opp", item.SubItems(6).Text)
                    cmd.Parameters.AddWithValue("qty", item.SubItems(7).Text)
                    cmd.Parameters.AddWithValue("item_code", "")
                    cmd.Parameters.AddWithValue("item_name", "")
                    cmd.Parameters.AddWithValue("uom", item.SubItems(8).Text)
                    cmd.Parameters.AddWithValue("netto", item.SubItems(9).Text)
                    cmd.Parameters.AddWithValue("src_item_number", item.SubItems(10).Text)
                    cmd.ExecuteNonQuery()
                Next

                Dim repdata As New List(Of Dictionary(Of String, Object))
                Dim dt As Date = Now
                Dim r As Dictionary(Of String, Object)
                Dim i As Integer = 1

                For Each item As ListViewItem In lstItems.Items
                    r = New Dictionary(Of String, Object)
                    r.Add("doc_number", docnum)
                    r.Add("src_doc_number", srcDoc)
                    r.Add("tanggal", dt)
                    r.Add("shift", cbShifts.Text)
                    r.Add("from_line", lineCode(cbFromLine.Text))
                    r.Add("to_line", lineCode(cbToLine.Text))
                    r.Add("operator", lblOperator.Text)
                    r.Add("type", If(rbIn.Checked, "PENERIMAAN", "PENGELUARAN"))
                    r.Add("item", "'" + itemName(Decimal.Parse("0" + item.SubItems(0).Text), Decimal.Parse("0" + item.SubItems(1).Text), Decimal.Parse("0" + item.SubItems(2).Text), Decimal.Parse("0" + item.SubItems(3).Text)))
                    r.Add("jenis", itemType(Trim(item.SubItems(5).Text), item.SubItems(4).Text, Decimal.Parse("0" + item.SubItems(6).Text)))
                    r.Add("qty", Decimal.Parse("0" + item.SubItems(7).Text))
                    r.Add("uom", item.SubItems(8).Text)
                    r.Add("netto", Decimal.Parse("0" + item.SubItems(9).Text))
                    r.Add("src_item_number", item.SubItems(10).Text)
                    r.Add("no", i)
                    i += 1

                    repdata.Add(r)
                Next

                _parent.dicPrint = repdata
                _parent.reportPrint = LCase(corp) + "-transfer-label.xlsx"
                _parent.papersize = papersize
                _parent.doPrint()
            Catch ex As Exception
                MsgBox(ex.Message + vbCrLf + "(TL-PL-01)")
            End Try

            cmd.Dispose()

            loadData()

            srcDoc = ""
        End If
    End Sub

    Public Sub rekapData()
        Dim prd As New Periode

        prd.rbPeriode.Visible = False
        prd.rb1day.Visible = False
        prd.rb1day.Checked = True
        If prd.ShowDialog() = DialogResult.OK Then
            Dim cmd As SQLiteCommand = localCon.CreateCommand()
            cmd.CommandText = "select t.item_code, t.item_name, t.anyaman, t.denier, t.lebar, t.panjang, t.warna, t.cetakan, t.opp, tt.doc_number,
                                case when shift = 'Shift 3' then date(date, '-1 day') else date(date) end tanggal, 
                                shift, from_line, to_line, qty total, type, netto
                            from transfer_detail_items t
                            join transfer_details tt on t.trans_doc_number = tt.doc_number
                            left join items i on t.item_code = i.item_code and tt.company_code = i.company_code
                            where case when shift = 'Shift 3' then date(date, '-1 day') else date(date) end = ? and void = 0"
            cmd.Parameters.AddWithValue("date", prd.dpAwal.Value.ToString("yyyy-MM-dd"))
            Dim adp As New SQLiteDataAdapter(cmd)
            Dim ds As New DataSet

            adp.Fill(ds)

            _parent.dsPrint = ds
            _parent.papersize = Nothing
            _parent.reportPrint = LCase(corp) + "-transfer-summary.xlsx"
            _parent.doPreview()
        End If

        prd.Dispose()
    End Sub

    Private Sub addItem()
        If (tbAnyaman.Text <> "" Or tbAnyaman.Enabled = False) And (tbDenier.Text <> "" Or tbDenier.Enabled = False) And
            (tbLebar.Text <> "" Or tbLebar.Enabled = False) And (tbPanjang.Text <> "" Or tbPanjang.Enabled = False) And
            (tbWarna.Text <> "" Or tbWarna.Enabled = False) And
            tbJumlah.Text <> "" And
            cbUOM.SelectedIndex <> -1 Then
            Dim item As ListViewItem

            item = lstItems.Items.Add(If(tbAnyaman.Enabled, tbAnyaman.Text, ""))
            item.SubItems.Add(tbDenier.Text)
            item.SubItems.Add(If(tbLebar.Enabled, tbLebar.Text, ""))
            item.SubItems.Add(If(tbPanjang.Enabled, tbPanjang.Text, ""))
            item.SubItems.Add(tbWarna.Text)
            item.SubItems.Add(If(tbCetakan.Enabled, tbCetakan.Text, ""))
            item.SubItems.Add(If(tbOPP.Enabled, tbOPP.Text, ""))
            item.SubItems.Add(tbJumlah.Text)
            item.SubItems.Add(cbUOM.Text)
            item.SubItems.Add(If(tbNetto.Enabled, tbNetto.Text, ""))
            item.SubItems.Add("")

            clearForm()
        Else
            MsgBox("Isi data terlebih dahulu")
        End If
    End Sub

    Private Sub delItem()
        lstItems.SelectedItems(0).Remove()
    End Sub

    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        addItem()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim t As Date = Now
        Dim sh As String = ""
        Select Case TimeValue(t)
            Case TimeValue(Convert.ToDateTime("08:00:00")) To TimeValue(Convert.ToDateTime("15:59:59"))
                sh = "Shift 1"
            Case TimeValue(Convert.ToDateTime("16:00:00")) To TimeValue(Convert.ToDateTime("23:59:59"))
                sh = "Shift 2"
            Case TimeValue(Convert.ToDateTime("00:00:00")) To TimeValue(Convert.ToDateTime("07:59:59"))
                sh = "Shift 3"
        End Select

        If sh <> cbShifts.Text Then
            If MessageBox.Show("Pilihan shift tidak sesuai dengan jam, pilih cancel untuk mengubah shift, pilih ok untuk menyimpan data.", "Konfirmasi Shift", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        If Not rbIn.Checked And Not rbOut.Checked Then
            MsgBox("Pilih dulu In atau Out nya")
            Exit Sub
        End If
        If cbFromLine.SelectedIndex < 0 Or cbToLine.SelectedIndex < 0 Then
            MsgBox("Pilih dulu Dari dan Ke nya")
            Exit Sub
        End If
        If lstItems.Items.Count = 0 Then
            MsgBox("Data transfer belum di-input")
            Exit Sub
        End If
        printLabel()
        clearForm()
        lstItems.Items.Clear()
        rbIn.Checked = False
        rbOut.Checked = False
        tbFromDocNum.Enabled = False
    End Sub

    Private Sub btnDelItem_Click(sender As Object, e As EventArgs) Handles btnDelItem.Click
        delItem()
    End Sub

    Public Sub loadData(Optional d1 As Date = Nothing, Optional d2 As Date = Nothing)
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As New DataSet
        Dim item As ListViewItem

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand
            If d1 = Date.MinValue And d2 = Date.MinValue Then
                cmd.CommandText = "SELECT * FROM transfer_details WHERE date(replace(`date`, '.', ':')) = date(?) AND company_code = ? and void = 0 order by `date` desc"
                cmd.Parameters.AddWithValue("date", Now)
                cmd.Parameters.AddWithValue("company_code", corp)
            ElseIf d2 = Date.MinValue Then
                cmd.CommandText = "SELECT * FROM transfer_details WHERE date(replace(`date`, '.', ':')) = date(?) AND company_code = ? and void = 0 order by `date` desc"
                cmd.Parameters.AddWithValue("date", d1)
                cmd.Parameters.AddWithValue("company_code", corp)
            Else
                cmd.CommandText = "SELECT * FROM transfer_details WHERE company_code = ? AND date(replace(`date`, '.', ':')) BETWEEN date(?) AND date(?) and void = 0 order by `date` desc"
                cmd.Parameters.AddWithValue("company_code", corp)
                cmd.Parameters.AddWithValue("date1", d1)
                cmd.Parameters.AddWithValue("date2", d2)
            End If

            adp = New SQLiteDataAdapter(cmd)
            adp.Fill(ds)

            lstData.Items.Clear()

            searchData.Clear()

            For Each d In ds.Tables(0).Rows
                item = lstData.Items.Add(d.Item("doc_number"))
                item.SubItems.Add(d.Item("date"))
                item.SubItems.Add(d.Item("from_line"))
                item.SubItems.Add(d.Item("to_line"))
                item.SubItems.Add(d.Item("operator"))
                item.SubItems.Add(d.Item("type"))

                searchData.Add(item)
            Next

            ds.Dispose()
            cmd.Dispose()
        End If
    End Sub

    Private Sub btnTanggal_Click(sender As Object, e As EventArgs) Handles btnTanggal.Click
        Dim prd As New Periode

        prd.dpAwal.Value = searchFrom
        prd.dpAkhir.Value = searchTo

        If prd.ShowDialog() = DialogResult.OK Then
            If prd.rb1day.Checked Then
                loadData(prd.dpAwal.Value)
                searchFrom = prd.dpAwal.Value
                searchTo = prd.dpAwal.Value
            Else
                loadData(prd.dpAwal.Value, prd.dpAkhir.Value)
                searchFrom = prd.dpAwal.Value
                searchTo = prd.dpAkhir.Value
            End If
        End If

        prd.Dispose()
    End Sub

    Private Sub rbInOut_CheckedChanged(sender As Object, e As EventArgs) Handles rbIn.CheckedChanged, rbOut.CheckedChanged
        tbFromDocNum.Enabled = True
        'If rbIn.Checked Then
        '    tbFromDocNum.Enabled = True
        'Else
        '    tbFromDocNum.Enabled = False
        'End If
    End Sub

    Private Sub searchDocNum(docnum As String, Optional mode As String = "TRF")
        Select Case mode
            Case "TRF"
                Dim cmd As SQLiteCommand = localCon.CreateCommand()
                cmd.CommandText = "SELECT ti.*, t.type, t.void FROM transfer_details t 
                            JOIN transfer_detail_items ti ON t.doc_number = ti.trans_doc_number and t.company_code = ?
                            WHERE t.doc_number = ?"
                cmd.Parameters.AddWithValue("company_code", corp)
                cmd.Parameters.AddWithValue("doc_number", docnum)

                Dim adp As New SQLiteDataAdapter(cmd)
                Dim ds As New DataSet
                adp.Fill(ds)

                Dim item As ListViewItem

                If ds.Tables.Count = 0 Then
                    MsgBox("Dokumen tidak ditemukan")
                    Exit Sub
                End If
                If ds.Tables(0).Rows.Count = 0 Then
                    MsgBox("Dokumen tidak ditemukan")
                    Exit Sub
                End If
                If ds.Tables(0).Rows(0).Item("void") = 1 Then
                    MsgBox("Dokumen yang sudah di void tidak dapat diterima")
                    Exit Sub
                End If
                If ds.Tables(0).Rows(0).Item("type") = "IN" Then
                    MsgBox("Dokumen harus berupa bukti pengeluaran")
                    Exit Sub
                End If
                If cbFromLine.SelectedIndex >= 0 Then
                    Dim lc As String = lineCode(cbFromLine.Text)
                    If docnum.IndexOf(lc) <> 0 Then
                        MsgBox("Asal dokumen tidak sesuai")
                        Exit Sub
                    End If
                End If
                If cbToLine.SelectedIndex >= 0 Then
                    Dim lc As String = lineCode(cbToLine.Text)
                    If docnum.IndexOf(lc) <= 0 Then
                        MsgBox("Tujuan dokumen tidak sesuai")
                        Exit Sub
                    End If
                End If
                If cbFromLine.SelectedIndex = -1 And cbToLine.SelectedIndex = -1 Then
                    Dim s As Integer = docnum.IndexOf("-")
                    Dim f As String = Strings.Left(docnum, s)
                    Dim t As String = Mid(docnum, s + 2, docnum.IndexOf("-", s + 1) - s - 1)
                    Select Case f
                        Case "EXT"
                            cbFromLine.SelectedIndex = 0
                        Case "CL"
                            cbFromLine.SelectedIndex = 1
                        Case "CS"
                            cbFromLine.SelectedIndex = 2
                        Case "INR"
                            cbFromLine.SelectedIndex = 3
                        Case "PRT"
                            cbFromLine.SelectedIndex = 4
                        Case "OPP"
                            cbFromLine.SelectedIndex = 5
                        Case "RTR"
                            cbFromLine.SelectedIndex = 4
                        Case "JHT"
                            cbFromLine.SelectedIndex = 6
                    End Select
                    Select Case t
                        Case "CL"
                            cbToLine.SelectedIndex = 0
                        Case "CS"
                            cbToLine.SelectedIndex = 1
                        Case "INR"
                            cbToLine.SelectedIndex = 2
                        Case "PRT"
                            cbToLine.SelectedIndex = 3
                        Case "OPP"
                            cbToLine.SelectedIndex = 4
                        Case "RTR"
                            cbToLine.SelectedIndex = 3
                        Case "PCK"
                            cbToLine.SelectedIndex = 5
                        Case "JHT"
                            cbToLine.SelectedIndex = 6
                    End Select
                End If

                For Each t As DataTable In ds.Tables
                    For Each r As DataRow In t.Rows
                        item = lstItems.Items.Add(If(If(IsDBNull(r.Item("anyaman")), 0, r.Item("anyaman")) = 0, "", r.Item("anyaman")))
                        item.SubItems.Add(If(If(IsDBNull(r.Item("denier")), 0, r.Item("denier")) = 0, "", r.Item("denier")))
                        item.SubItems.Add(If(If(IsDBNull(r.Item("lebar")), 0, r.Item("lebar")) = 0, "", r.Item("lebar")))
                        item.SubItems.Add(If(If(IsDBNull(r.Item("panjang")), 0, r.Item("panjang")) = 0, "", r.Item("panjang")))
                        item.SubItems.Add(If(IsDBNull(r.Item("warna")), "", r.Item("warna")))
                        item.SubItems.Add(If(IsDBNull(r.Item("cetakan")), "", r.Item("cetakan")))
                        item.SubItems.Add(If(If(IsDBNull(r.Item("opp")), 0, r.Item("opp")) = 0, "", r.Item("opp")))
                        item.SubItems.Add(r.Item("qty"))
                        item.SubItems.Add(If(IsDBNull(r.Item("uom")), "", r.Item("uom")))
                        item.SubItems.Add(If(If(IsDBNull(r.Item("netto")), 0, r.Item("netto")) = 0, "", r.Item("netto")))
                        item.SubItems.Add(If(IsDBNull(r.Item("src_item_number")), "", r.Item("src_item_number")))
                    Next
                Next
                srcDoc = tbFromDocNum.Text
            Case "ITM"
                Dim cmd As SQLiteCommand = localCon.CreateCommand()
                cmd.CommandText = "SELECT p.* FROM production_details p
                            WHERE p.company_code = ? AND p.doc_number = ?"
                cmd.Parameters.AddWithValue("company_code", corp)
                cmd.Parameters.AddWithValue("doc_number", docnum)

                Dim adp As New SQLiteDataAdapter(cmd)
                Dim ds As New DataSet
                adp.Fill(ds)

                Dim item As ListViewItem

                If ds.Tables.Count = 0 Then
                    MsgBox("Dokumen tidak ditemukan")
                    Exit Sub
                End If
                If ds.Tables(0).Rows.Count = 0 Then
                    MsgBox("Dokumen tidak ditemukan")
                    Exit Sub
                End If
                If ds.Tables(0).Rows(0).Item("void") = 1 Then
                    MsgBox("Dokumen yang sudah di void tidak dapat diterima")
                    Exit Sub
                End If
                If cbFromLine.SelectedIndex >= 0 Then
                    Dim lc As String = lineCode(cbFromLine.Text)
                    If docnum.IndexOf(lc) <> 0 Then
                        MsgBox("Asal dokumen tidak sesuai")
                        Exit Sub
                    End If
                End If

                For Each t As DataTable In ds.Tables
                    For Each r As DataRow In t.Rows
                        item = lstItems.Items.Add(If(If(IsDBNull(r.Item("anyaman")), 0, r.Item("anyaman")) = 0, "", r.Item("anyaman")))
                        item.SubItems.Add(If(If(IsDBNull(r.Item("denier")), 0, r.Item("denier")) = 0, "", r.Item("denier")))
                        item.SubItems.Add(If(If(IsDBNull(r.Item("lebar")), 0, r.Item("lebar")) = 0, "", r.Item("lebar")))
                        Select Case r.Item("line_code")
                            Case "CL"
                                item.SubItems.Add("")
                            Case Else
                                item.SubItems.Add(If(If(IsDBNull(r.Item("panjang")), 0, r.Item("panjang")) = 0, "", r.Item("panjang")))
                        End Select
                        item.SubItems.Add(If(IsDBNull(r.Item("warna")), "", r.Item("warna")))
                        item.SubItems.Add(If(IsDBNull(r.Item("cetakan")), "", r.Item("cetakan")))
                        item.SubItems.Add(If(If(IsDBNull(r.Item("opp")), 0, r.Item("opp")) = 0, "", r.Item("opp")))
                        Select Case r.Item("line_code")
                            Case "CL"
                                item.SubItems.Add(r.Item("panjang"))
                            Case Else
                                item.SubItems.Add(r.Item("qty"))
                        End Select
                        Select Case r.Item("line_code")
                            Case "EXT", "OPP"
                                item.SubItems.Add("KG")
                            Case "CL", "RTR"
                                item.SubItems.Add("MTR")
                            Case "CS", "INR", "PRT", "JHT"
                                item.SubItems.Add("LBR")
                            Case Else
                                item.SubItems.Add("")
                        End Select

                        Dim jsondata As Dictionary(Of String, Object) = json.Deserialize(Of Dictionary(Of String, Object))(r.Item("data"))
                        If jsondata.ContainsKey("netto") Then
                            item.SubItems.Add(If(If(IsDBNull(jsondata("netto")), 0, jsondata("netto")) = 0, "", jsondata("netto")))
                        Else
                            item.SubItems.Add("")
                        End If
                        item.SubItems.Add(r.Item("doc_number"))
                    Next
                Next
        End Select
        clearForm()
    End Sub

    Private Sub tbFromDocNum_KeyUp(sender As Object, e As KeyEventArgs) Handles tbFromDocNum.KeyUp
        If e.KeyCode = 13 And rbIn.Checked Then
            lstItems.Items.Clear()
            searchDocNum(tbFromDocNum.Text)
        ElseIf e.KeyCode = 13 And rbOut.Checked Then
            searchDocNum(tbFromDocNum.Text, "ITM")
        End If
    End Sub

    Private Sub tmReprint_Click(sender As Object, e As EventArgs) Handles tmReprint.Click
        If lstData.SelectedItems.Count <> 1 Then
            MsgBox("Pilih dulu dokumen yang akan di print ulang")
            Exit Sub
        End If

        Dim repdata As New List(Of Dictionary(Of String, Object))
        Dim r As Dictionary(Of String, Object)
        Dim i As Integer = 1
        Dim item As ListViewItem = lstData.SelectedItems(0)

        Dim cmd As SQLiteCommand = localCon.CreateCommand()
        cmd.CommandText = "SELECT ti.*, t.`date`, t.shift, t.from_line, t.to_line, t.operator, t.`type`, t.src_doc_number FROM transfer_detail_items ti JOIN transfer_details t ON ti.trans_doc_number = t.doc_number WHERE trans_doc_number = ?"
        cmd.Parameters.AddWithValue("doc_number", item.Text)
        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        adp.Fill(ds)

        For Each t As DataTable In ds.Tables
            For Each dr As DataRow In t.Rows
                r = New Dictionary(Of String, Object)
                r.Add("doc_number", item.Text)
                r.Add("tanggal", dr.Item("date"))
                r.Add("src_doc_number", dr.Item("src_doc_number"))
                r.Add("shift", dr.Item("shift"))
                r.Add("from_line", dr.Item("from_line"))
                r.Add("to_line", dr.Item("to_line"))
                r.Add("operator", dr.Item("operator"))
                r.Add("type", If(dr.Item("type") = "IN", "PENERIMAAN", "PENGELUARAN"))
                r.Add("item", "'" & itemName(dr.Item("anyaman"), dr.Item("denier"), dr.Item("lebar"), dr.Item("panjang")))
                r.Add("jenis", itemType(dr.Item("cetakan"), dr.Item("warna"), dr.Item("opp")))
                r.Add("qty", dr.Item("qty"))
                r.Add("uom", dr.Item("uom"))
                r.Add("netto", dr.Item("netto"))
                r.Add("src_item_number", dr.Item("src_item_number"))
                r.Add("no", i)

                repdata.Add(r)
                i += 1
            Next
        Next

        _parent.dicPrint = repdata
        _parent.reportPrint = LCase(corp) + "-transfer-label.xlsx"
        _parent.papersize = papersize
        _parent.doPrint()
    End Sub

    Private Sub syncTick_Tick(sender As Object, e As EventArgs) Handles syncTick.Tick
        'If dbCon Is Nothing Then Exit Sub
        If dbCon.state <> ConnectionState.Open Then Exit Sub
        sync("transfer_details", lblStationEX.Text)
    End Sub

    Private Sub btnDenier_Click(sender As Object, e As EventArgs) Handles btnDenier.Click
        If Not tbDenier.Enabled Then
            Exit Sub
        End If

        searchProp("denier")

        'Dim tb As TextBox = Nothing
        'Dim cmd As SQLiteCommand = localCon.CreateCommand()

        'cmd.CommandText = "SELECT DISTINCT denier FROM items WHERE company_code = ? AND line like ? AND denier IS NOT NULL ORDER BY denier"
        'cmd.Parameters.AddWithValue("company_code", corp)
        'Select Case cbFromLine.Text
        '    Case "Extruder"
        '        cmd.Parameters.AddWithValue("line", "%EXT%")
        '    Case "Circular Loom"
        '        cmd.Parameters.AddWithValue("line", "%CL%")
        '    Case "Roll To Roll"
        '        cmd.Parameters.AddWithValue("line", "%RTR%")
        '    Case "Cutting Sewing"
        '        cmd.Parameters.AddWithValue("line", "%CS%")
        '    Case "Insert Inner"
        '        cmd.Parameters.AddWithValue("line", "%INR%")
        '    Case "Printing"
        '        cmd.Parameters.AddWithValue("line", "%PRT%")
        '    Case "OPP"
        '        cmd.Parameters.AddWithValue("line", "%OPP%")
        '    Case "Packing"
        '        cmd.Parameters.AddWithValue("line", "%PCK%")
        '    Case Else
        '        cmd.Parameters.AddWithValue("line", "%")
        'End Select

        'Dim adp As New SQLiteDataAdapter(cmd)
        'Dim ds As New DataSet
        'Dim item As ListViewItem

        'adp.Fill(ds)

        'lstFind.Items.Clear()

        'If ds.Tables.Count = 1 Then
        '    For Each row In ds.Tables(0).Rows
        '        item = lstFind.Items.Add(row.Item("denier"))
        '    Next
        'End If

        'ds.Dispose()
        'adp.Dispose()
        'cmd.Dispose()

        'tb = tbDenier

        'If tb IsNot Nothing Then
        '    lstFind.Left = tb.Left + gbLabel.Left
        '    lstFind.Top = tb.Top + gbLabel.Top + tb.Height

        '    lstFind.Visible = True

        '    textFocus = tb
        '    textCode = Nothing
        '    textCol = 0
        '    textFocus.Focus()
        'End If
    End Sub

    Private Sub btnAnyaman_Click(sender As Object, e As EventArgs) Handles btnAnyaman.Click
        If Not tbAnyaman.Enabled Then
            Exit Sub
        End If

        searchProp("anyaman")

        'Dim tb As TextBox = Nothing
        'Dim cmd As SQLiteCommand = localCon.CreateCommand()

        'cmd.CommandText = "SELECT DISTINCT anyaman FROM items WHERE company_code = ? AND line like ? AND anyaman IS NOT NULL AND anyaman <> 0 ORDER BY anyaman"
        'cmd.Parameters.AddWithValue("company_code", corp)
        'Select Case cbFromLine.Text
        '    Case "Extruder"
        '        cmd.Parameters.AddWithValue("line", "%EXT%")
        '    Case "Circular Loom"
        '        cmd.Parameters.AddWithValue("line", "%CL%")
        '    Case "Roll To Roll"
        '        cmd.Parameters.AddWithValue("line", "%RTR%")
        '    Case "Cutting Sewing"
        '        cmd.Parameters.AddWithValue("line", "%CS%")
        '    Case "Insert Inner"
        '        cmd.Parameters.AddWithValue("line", "%INR%")
        '    Case "Printing"
        '        cmd.Parameters.AddWithValue("line", "%PRT%")
        '    Case "OPP"
        '        cmd.Parameters.AddWithValue("line", "%OPP%")
        '    Case "Packing"
        '        cmd.Parameters.AddWithValue("line", "%PCK%")
        '    Case Else
        '        cmd.Parameters.AddWithValue("line", "%")
        'End Select

        'Dim adp As New SQLiteDataAdapter(cmd)
        'Dim ds As New DataSet
        'Dim item As ListViewItem

        'adp.Fill(ds)

        'lstFind.Items.Clear()

        'If ds.Tables.Count = 1 Then
        '    For Each row In ds.Tables(0).Rows
        '        item = lstFind.Items.Add(row.Item("Anyaman"))
        '    Next
        'End If

        'ds.Dispose()
        'adp.Dispose()
        'cmd.Dispose()

        'tb = tbAnyaman

        'If tb IsNot Nothing Then
        '    lstFind.Left = tb.Left + gbLabel.Left
        '    lstFind.Top = tb.Top + gbLabel.Top + tb.Height

        '    lstFind.Visible = True

        '    textFocus = tb
        '    textCode = Nothing
        '    textcol = 0
        '    textFocus.Focus()
        'End If
    End Sub

    Private Sub lstFind_DoubleClick(sender As Object, e As EventArgs) Handles lstFind.DoubleClick
        If lstFind.SelectedItems.Count = 1 And (textFocus IsNot Nothing Or textCode IsNot Nothing) Then
            Dim item As ListViewItem = lstFind.SelectedItems(0)
            If textCode IsNot Nothing Then
                textFocus.Text = item.SubItems(textcol).Text
                textCode.Text = item.SubItems(datacol).Text
            ElseIf textcol > 0 Then
                textFocus.Text = item.SubItems(textcol).Text
            Else
                textFocus.Text = item.Text
            End If

            lstFind.Visible = False
        End If
    End Sub

    Private Sub lstFind_LostFocus(sender As Object, e As EventArgs) Handles lstFind.LostFocus, tbItem.LostFocus, tbAnyaman.LostFocus,
            tbDenier.LostFocus, tbLebar.LostFocus
        If Not IsNothing(textFocus) Then
            If Not textFocus.Focused And Not lstFind.Focused Then
                lstFind.Visible = False
            End If
        Else
            lstFind.Visible = False
        End If
    End Sub

    Private Function validFromTo(dari As String, ke As String) As Boolean
        If (dari = "Extruder" And ke = "Circular Loom") Or
                (dari = "Circular Loom" And ke = "Extruder") Or         'retur
                (dari = "Circular Loom" And ke = "Cutting Sewing") Or
                (dari = "Circular Loom" And ke = "Printing") Or
                (dari = "Cutting Sewing" And ke = "Circular Loom") Or   'retur
                (dari = "Cutting Sewing" And ke = "Insert Inner") Or
                (dari = "Cutting Sewing" And ke = "Printing") Or
                (dari = "Cutting Sewing" And ke = "OPP") Or
                (dari = "Cutting Sewing" And ke = "Packing") Or
                (dari = "Insert Inner" And ke = "Cutting Sewing") Or    'retur
                (dari = "Insert Inner" And ke = "Packing") Or
                (dari = "Printing" And ke = "Circular Loom") Or         'retur
                (dari = "Printing" And ke = "Cutting Sewing") Or        'retur
                (dari = "Printing" And ke = "Packing") Or
                (dari = "Printing" And ke = "Insert Inner") Or
                (dari = "Printing" And ke = "Jahit") Or
                (dari = "OPP" And ke = "Cutting Sewing") Or             'retur
                (dari = "OPP" And ke = "Packing") Or
                (dari = "Packing" And ke = "Cutting Sewing") Or         'retur
                (dari = "Packing" And ke = "Insert Inner") Or           'retur
                (dari = "Packing" And ke = "Printing") Or               'retur
                (dari = "Packing" And ke = "OPP") Then                  'retur
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub cbFromLine_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFromLine.SelectedIndexChanged
        If Not validFromTo(cbFromLine.Text, cbToLine.Text) And cbToLine.Text <> "" Then
            'MsgBox("Dari " + cbFromLine.Text + " ke " + cbToLine.Text + " tidak diperbolehkan")
            cbToLine.SelectedIndex = -1
            'Exit Sub
        End If
        Select Case cbFromLine.Text
            Case "Extruder"
                tbDenier.Enabled = True
                tbWarna.Enabled = True
                tbAnyaman.Enabled = False
                tbLebar.Enabled = False
                tbPanjang.Enabled = False
                tbCetakan.Enabled = False
                tbOPP.Enabled = False
            Case "Circular Loom"
                tbDenier.Enabled = True
                tbWarna.Enabled = True
                tbAnyaman.Enabled = True
                tbPanjang.Enabled = False
                tbLebar.Enabled = True
                tbCetakan.Enabled = False
                tbOPP.Enabled = False
            Case Else
                tbDenier.Enabled = True
                tbWarna.Enabled = True
                tbAnyaman.Enabled = True
                tbPanjang.Enabled = True
                tbLebar.Enabled = True
                tbCetakan.Enabled = True
                tbOPP.Enabled = True
        End Select
    End Sub

    Private Sub cbToLine_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbToLine.SelectedIndexChanged
        If Not validFromTo(cbFromLine.Text, cbToLine.Text) And cbFromLine.Text <> "" And cbToLine.Text <> "" Then
            MsgBox("Dari " + cbFromLine.Text + " ke " + cbToLine.Text + " tidak diperbolehkan")
            cbToLine.SelectedIndex = -1
        End If
    End Sub

    Private Sub cbShifts_GotFocus(sender As Object, e As EventArgs) Handles cbShifts.GotFocus
        If cbShifts.Items.Count = 0 Then
            loadShifts()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lstItems.Items.Clear()
    End Sub

    Private Function search(lst As List(Of ListViewItem), Key As String) As IQueryable(Of ListViewItem)
        search = lst.AsQueryable

        Dim filter As Expression(Of Func(Of ListViewItem, Boolean)) =
            Function(i As ListViewItem) i.Text.IndexOf(Key, StringComparison.InvariantCultureIgnoreCase) >= 0 Or i.SubItems(4).Text.IndexOf(Key, StringComparison.InvariantCultureIgnoreCase) >= 0

        If Not String.IsNullOrEmpty(Key) Then search = Queryable.Where(search, filter)
    End Function

    Private Sub tbCari_TextChanged(sender As Object, e As EventArgs) Handles tbCari.TextChanged
        lstData.Items.Clear()
        For Each item In search(searchData, tbCari.Text)
            lstData.Items.Add(item)
        Next
    End Sub

    Private Sub lstItems_KeyUp(sender As Object, e As KeyEventArgs) Handles lstItems.KeyUp
        If e.KeyCode = Keys.Delete Then
            btnDelItem.PerformClick()
        End If
    End Sub

    Private Sub lstItems_DoubleClick(sender As Object, e As EventArgs) Handles lstItems.DoubleClick
        If lstItems.SelectedItems.Count = 1 Then
            Dim item = lstItems.SelectedItems(0)

            If item.SubItems(9).Text <> "" Then
                Dim jml As New Quantity
                jml.tbJumlah.Maximum = Decimal.Parse(item.SubItems(7).Text)
                jml.tbJumlah.Value = jml.tbJumlah.Maximum
                jml.lblUOM.Text = item.SubItems(8).Text

                If jml.ShowDialog() = DialogResult.OK Then
                    item.SubItems(7).Text = jml.tbJumlah.Value
                End If
            End If
        End If
    End Sub

    Private Sub VoidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VoidToolStripMenuItem.Click
        If lstData.SelectedItems.Count <> 1 Then
            MsgBox("Pilih dulu dokumen yang akan di void")
            Exit Sub
        End If

        Dim item As ListViewItem = lstData.SelectedItems(0)

        If MessageBox.Show("Apakah yakin akan membatalkan transfer no " & item.Text & "?", "Pembatalan Transfer", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Dim cmd As SQLiteCommand = localCon.CreateCommand()
            cmd.CommandText = "UPDATE transfer_details SET void = 1, synced = 0 WHERE doc_number = ? AND company_code = ?"
            cmd.Parameters.AddWithValue("doc_number", item.Text)
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.ExecuteNonQuery()

            dbCon.SQL("UPDATE transfer_details SET void = 1, void_by = @vb WHERE doc_number = @dn AND company_code = @cc")
            dbCon.addParameter("@vb", user)
            dbCon.addParameter("@dn", item.Text)
            dbCon.addParameter("@cc", corp)
            dbCon.execute()

            dbCon.SQL("DELETE FROM transfer_details_sync WHERE doc_number = @dn AND company_code = @cc")
            dbCon.addParameter("@dn", item.Text)
            dbCon.addParameter("@cc", corp)
            dbCon.execute()
        End If
    End Sub

    Private Sub cbUOM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbUOM.SelectedIndexChanged
        If sender.Text = "KG" Then
            lblNetto.Visible = False
            tbNetto.Visible = False
        Else
            lblNetto.Visible = True
            tbNetto.Visible = True
        End If
    End Sub

    Private Sub searchProp(prop As String)
        Dim tb As TextBox = Nothing
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        cmd.CommandText = "SELECT DISTINCT " & prop & " FROM items WHERE company_code = ? AND line like ? AND " & prop & " IS NOT NULL ORDER BY " & prop
        cmd.Parameters.AddWithValue("company_code", corp)
        Select Case cbFromLine.Text
            Case "Extruder"
                cmd.Parameters.AddWithValue("line", "%EXT%")
            Case "Circular Loom"
                cmd.Parameters.AddWithValue("line", "%CL%")
            Case "Roll To Roll"
                cmd.Parameters.AddWithValue("line", "%RTR%")
            Case "Cutting Sewing"
                cmd.Parameters.AddWithValue("line", "%CS%")
            Case "Insert Inner"
                cmd.Parameters.AddWithValue("line", "%INR%")
            Case "Printing"
                cmd.Parameters.AddWithValue("line", "%PRT%")
            Case "OPP"
                cmd.Parameters.AddWithValue("line", "%OPP%")
            Case "Packing"
                cmd.Parameters.AddWithValue("line", "%PCK%")
            Case Else
                cmd.Parameters.AddWithValue("line", "%")
        End Select

        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        lstFind.Items.Clear()

        If ds.Tables.Count = 1 Then
            For Each row In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item(prop))
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        Select Case prop
            Case "anyaman"
                tb = tbAnyaman
            Case "denier"
                tb = tbDenier
            Case "lebar"
                tb = tbLebar
            Case "panjang"
                tb = tbPanjang
        End Select

        findCol1.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(prop)

        If tb IsNot Nothing Then
            lstFind.Left = tb.Left + gbLabel.Left
            lstFind.Top = tb.Top + gbLabel.Top + tb.Height

            lstFind.Visible = True

            textFocus = tb
            textCode = Nothing
            textcol = 0
            textFocus.Focus()
        End If
    End Sub

    Private Sub btnLebar_Click(sender As Object, e As EventArgs) Handles btnLebar.Click
        If Not tbLebar.Enabled Then
            Exit Sub
        End If

        searchProp("lebar")
    End Sub

    Private Sub btnPanjang_Click(sender As Object, e As EventArgs) Handles btnPanjang.Click
        If Not tbPanjang.Enabled Then
            Exit Sub
        End If

        searchProp("panjang")
    End Sub

    Private Sub gbLabel_Enter(sender As Object, e As EventArgs) Handles gbLabel.Enter

    End Sub
End Class

