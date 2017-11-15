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
Imports System.ComponentModel
Imports System.Reflection
Imports ERP

Public Class ScaleLabeling : Inherits ERPModule

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
                MsgBox("Connection error, please check your network connection and try again." + vbCrLf + "(SL-LS-01)")
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
                                Case "production_details"
                                    args(2) = "Syncing Productions"
                                    cmdStr = "REPLACE INTO production_details(company_code, area_code, station_code, doc_number, date, shift, data, item_code, line_code,
                                                    qty, creator, pic1, pic1code, pic2, pic2code, synced, anyaman, denier, lebar, panjang, cetakan, warna, opp, synced, void, void_by) 
                                                values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 1, ?, ?)"
                                Case "machines"
                                    args(2) = "Syncing Machines"
                                    cmdStr = "REPLACE INTO machines(company_code, area_code, line_code, machine_code) values(?, ?, ?, ?)"
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
                                        Case "production_details"
                                            cmd.Parameters.AddWithValue("area_code", r.Item("area_code"))
                                            cmd.Parameters.AddWithValue("station_code", r.Item("station_code"))
                                            cmd.Parameters.AddWithValue("doc_number", r.Item("doc_number"))
                                            cmd.Parameters.AddWithValue("date", r.Item("date"))
                                            cmd.Parameters.AddWithValue("shift", r.Item("shift"))
                                            cmd.Parameters.AddWithValue("data", r.Item("data"))
                                            cmd.Parameters.AddWithValue("item_code", r.Item("item_code"))
                                            cmd.Parameters.AddWithValue("line_code", r.Item("line_code"))
                                            cmd.Parameters.AddWithValue("qty", r.Item("qty"))
                                            cmd.Parameters.AddWithValue("creator", r.Item("creator"))
                                            cmd.Parameters.AddWithValue("pic1", r.Item("pic1"))
                                            cmd.Parameters.AddWithValue("pic1code", r.Item("pic1code"))
                                            cmd.Parameters.AddWithValue("pic2", r.Item("pic2"))
                                            cmd.Parameters.AddWithValue("pic2code", r.Item("pic2code"))
                                            cmd.Parameters.AddWithValue("synced", 1)
                                            cmd.Parameters.AddWithValue("anyaman", r.Item("anyaman"))
                                            cmd.Parameters.AddWithValue("denier", r.Item("denier"))
                                            cmd.Parameters.AddWithValue("lebar", r.Item("lebar"))
                                            cmd.Parameters.AddWithValue("panjang", r.Item("panjang"))
                                            cmd.Parameters.AddWithValue("cetakan", r.Item("cetakan"))
                                            cmd.Parameters.AddWithValue("warna", r.Item("warna"))
                                            cmd.Parameters.AddWithValue("opp", r.Item("opp"))
                                            cmd.Parameters.AddWithValue("void", r.Item("void"))
                                            cmd.Parameters.AddWithValue("void_by", r.Item("void_by"))
                                        Case "machines"
                                            cmd.Parameters.AddWithValue("area_code", r.Item("area_code"))
                                            cmd.Parameters.AddWithValue("line_code", r.Item("line_code"))
                                            cmd.Parameters.AddWithValue("machine_code", r.Item("machine_code"))
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
                                        Case "production_details"
                                            rcon.addParameter("@ac", r.Item("area_code"))
                                            rcon.addParameter("@rsc", r.Item("station_code"))
                                            rcon.addParameter("@dn", r.Item("doc_number"))
                                        Case "machines"
                                            rcon.addParameter("@ac", r.Item("area_code"))
                                            rcon.addParameter("@lc", r.Item("line_code"))
                                            rcon.addParameter("@mc", r.Item("machine_code"))
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
    Private dataCol As Integer = 0
    Private textCol As Integer = 0

    Public injectAscend As Boolean = False
    Public ascendConStr As String
    Private ascendCon As DBConn

    Private doEvents As Boolean = False

    Dim scaleObj As ScaleClass

    Public Shadows canOffline As Boolean = True

    Private papersize As PaperSize = Nothing

    Private txtValidate As String = ""

    Private searchFrom As Date = Today
    Private searchTo As Date = Today
    Private searchData As New List(Of ListViewItem)

    Private filterData As New List(Of ListViewItem)

    Private Sub clearForm(Optional parent As ControlCollection = Nothing)
        'On Error Resume Next

        If IsNothing(parent) Then
            parent = Controls
        End If

        For Each c In parent
            If TypeName(c) = "TextBox" Then
                c.Text = ""
            ElseIf TypeName(c) = "CheckBox" Then
                c.Checked = False
            ElseIf c.HasChildren Then
                clearForm(c.Controls)
            End If
        Next

        tbTungkul.Text = 2.5
        tbTungkulRTR.Text = 2.5

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

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='production_details'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `production_details` (
	                                `company_code`	TEXT,
	                                `area_code`	TEXT,
	                                `station_code`	TEXT,
	                                `doc_number`	TEXT,
	                                `date`	TEXT,
	                                `shift`	TEXT,
	                                `data`	TEXT,
	                                `item_code`	TEXT,
	                                `line_code`	TEXT,
	                                `qty`	NUMERIC,
	                                `creator`	TEXT,
	                                `pic1`	TEXT,
	                                `pic1code`	TEXT,
	                                `pic2`	TEXT,
	                                `pic2code`	TEXT,
	                                `synced`	INTEGER NOT NULL DEFAULT 0,
	                                `anyaman`	NUMERIC,
	                                `denier`	NUMERIC,
	                                `lebar`	NUMERIC,
	                                `panjang`	NUMERIC,
	                                `cetakan`	TEXT,
	                                `warna`	TEXT,
	                                `opp`	NUMERIC,
                                    `void`  INTEGER NOT NULL DEFAULT 0,
                                    `void_by` TEXT,
	                                PRIMARY KEY(company_code,area_code,station_code,doc_number)
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

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='machines'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `machines` (
	                                `company_code`	TEXT,
	                                `area_code`	TEXT,
	                                `line_code`	TEXT,
	                                `machine_code`	TEXT,
	                                PRIMARY KEY(company_code,area_code,line_code,machine_code)
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
            sync("machines", lblStationEX.Text)
            sync("bobbins", lblStationEX.Text)
            sync("shifts", lblStationEX.Text)
            sync("production_details", lblStationEX.Text)
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

    Private Function usePrinter(printer As String) As Boolean
        Dim cmd As SQLiteCommand

        If localCon.State = ConnectionState.Open Then
            barcode_printer = printer
            lblPrinterEX.Text = barcode_printer

            cmd = localCon.CreateCommand()
            cmd.CommandText = "REPLACE INTO `printers`(
                                    `id`, `company_code`, `name`, `last_update`
                                ) values(?, ?, ?, ?)"
            cmd.Parameters.AddWithValue("id", 1)
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("name", printer)
            cmd.Parameters.AddWithValue("lastupdate", Now)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

            Return True
        Else
            Return False
        End If

    End Function

    Private Function useScale(scale As ListViewItem, port As String) As Boolean
        Dim cmd As SQLiteCommand

        If localCon.State = ConnectionState.Open Then
            active_scale = scale.Text

            cmd = localCon.CreateCommand()
            cmd.CommandText = "REPLACE INTO `used_devices`(`company_code`, `device`, `table_name`, `device_id`) values(?, ?, ?, ?)"
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("device", "scale")
            cmd.Parameters.AddWithValue("table_name", "scales")
            cmd.Parameters.AddWithValue("device_id", Integer.Parse("0" + scale.SubItems(11).Text))
            cmd.ExecuteNonQuery()
            cmd.Dispose()

            'cmd = localCon.CreateCommand()
            'cmd.CommandText = "REPLACE INTO `scales`(
            '                        `id`, `company_code`, `code`, `name`, `port`, `config`, `continuous`, 
            '                        `datalength`, `startchar`, `endchar`, `readstart`, `readend`, 
            '                        `precision`, `unitratio`, `last_update`
            '                    ) values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            'cmd.Parameters.AddWithValue("id", 1)
            'cmd.Parameters.AddWithValue("company_code", corp)
            'cmd.Parameters.AddWithValue("code", scale.Text)
            'cmd.Parameters.AddWithValue("name", scale.SubItems(1).Text)
            'cmd.Parameters.AddWithValue("port", port)
            'cmd.Parameters.AddWithValue("config", scale.SubItems(2).Text)
            'cmd.Parameters.AddWithValue("continuous", scale.SubItems(3).Text)
            'cmd.Parameters.AddWithValue("datalength", scale.SubItems(4).Text)
            'cmd.Parameters.AddWithValue("startchar", scale.SubItems(5).Text)
            'cmd.Parameters.AddWithValue("endchar", scale.SubItems(6).Text)
            'cmd.Parameters.AddWithValue("readstart", scale.SubItems(7).Text)
            'cmd.Parameters.AddWithValue("readend", scale.SubItems(8).Text)
            'cmd.Parameters.AddWithValue("precision", Integer.parse("0"+scale.SubItems(9).Text))
            'cmd.Parameters.AddWithValue("unitratio", Decimal.parse("0"+scale.SubItems(10).Text))
            'cmd.Parameters.AddWithValue("lastupdate", Now)
            'cmd.ExecuteNonQuery()
            'cmd.Dispose()

            Return True
        Else
            Return False
        End If

    End Function

    Public Sub loadData(line As String, Optional d1 As Date = Nothing, Optional d2 As Date = Nothing)
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As New DataSet
        Dim item As ListViewItem
        Dim jsondata As Dictionary(Of String, Object)

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand
            If d1 = Date.MinValue And d2 = Date.MinValue Then
                cmd.CommandText = "SELECT * FROM production_details WHERE date(replace(`date`, '.', ':')) = date(?) AND company_code = ? AND line_code = ? and void = 0 order by `date` desc"
                cmd.Parameters.AddWithValue("date", Now)
                cmd.Parameters.AddWithValue("company_code", corp)
                cmd.Parameters.AddWithValue("line_code", line)
            ElseIf d2 = Date.MinValue Then
                cmd.CommandText = "SELECT * FROM production_details WHERE date(replace(`date`, '.', ':')) = date(?) AND company_code = ? AND line_code = ? and void = 0 order by `date` desc"
                cmd.Parameters.AddWithValue("date", d1)
                cmd.Parameters.AddWithValue("company_code", corp)
                cmd.Parameters.AddWithValue("line_code", line)
            Else
                cmd.CommandText = "SELECT * FROM production_details WHERE company_code = ? AND line_code = ? AND date(replace(`date`, '.', ':')) BETWEEN date(?) AND date(?) and void = 0 order by `date` desc"
                cmd.Parameters.AddWithValue("company_code", corp)
                cmd.Parameters.AddWithValue("line_code", line)
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
                item.SubItems.Add(d.Item("line_code"))
                item.SubItems.Add(If(IsDBNull(d.Item("anyaman")), "", d.Item("anyaman")))
                item.SubItems.Add(If(IsDBNull(d.Item("denier")), "", d.Item("denier")))
                item.SubItems.Add(If(IsDBNull(d.Item("lebar")), "", d.Item("lebar")))
                item.SubItems.Add(If(IsDBNull(d.Item("panjang")), "", d.Item("panjang")))
                item.SubItems.Add(If(IsDBNull(d.Item("cetakan")), "", d.Item("cetakan")))
                item.SubItems.Add(If(IsDBNull(d.Item("warna")), "", d.Item("warna")))
                item.SubItems.Add(If(IsDBNull(d.Item("opp")), "", d.Item("opp")))
                item.SubItems.Add(d.Item("qty"))

                jsondata = json.Deserialize(Of Dictionary(Of String, Object))(d.Item("data"))
                If jsondata.ContainsKey("urutan") Then
                    item.SubItems.Add(jsondata("urutan"))
                ElseIf jsondata.ContainsKey("panen") Then
                    item.SubItems.Add(jsondata("panen"))
                Else
                    item.SubItems.Add("")
                End If
                If jsondata.ContainsKey("mesin") Then
                    item.SubItems.Add(jsondata("mesin"))
                Else
                    item.SubItems.Add("")
                End If
                item.SubItems.Add(d.Item("pic1"))

                If jsondata.ContainsKey("berat") Then
                    item.SubItems.Add(jsondata("berat"))
                Else
                    item.SubItems.Add("")
                End If

                searchData.Add(item)
            Next

            ds.Dispose()
            cmd.Dispose()
        End If
    End Sub

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

        If checkPrinter(barcode_printer) Then
            lblPrinterEX.Text = barcode_printer
        Else
            barcode_printer = ""
            lblPrinterEX.Text = "No Barcode Printer Specified"
        End If

        If checkScale(scale) Then
            active_scale = scale.Item("code")

            Dim conf() As String
            conf = scale.Item("config").Split(";")
            sPort.BaudRate = Integer.Parse("0" + conf(0))
            Select Case conf(1)
                Case "N"
                    sPort.Parity = Ports.Parity.None
                Case "O"
                    sPort.Parity = Ports.Parity.Odd
                Case "E"
                    sPort.Parity = Ports.Parity.Even
                Case "M"
                    sPort.Parity = Ports.Parity.Mark
                Case "S"
                    sPort.Parity = Ports.Parity.Space
            End Select
            sPort.DataBits = Integer.Parse("0" + conf(2))
            sPort.StopBits = Integer.Parse("0" + conf(3))
            sPort.PortName = scale.Item("port")

            continuous = CBool(Integer.Parse("0" + scale.Item("continuous")))
            dataLength = Integer.Parse("0" + scale.Item("datalength"))

            Dim x As Integer = Integer.Parse("0" + scale.Item("startchar"))
            If x > 0 Then
                startChar = Chr(x)
            Else
                startChar = Nothing
            End If
            x = Integer.Parse("0" + scale.Item("endchar"))
            If x > 0 Then
                endChar = Chr(x)
            Else
                endChar = Nothing
            End If

            readStart = Integer.Parse("0" + scale.Item("readstart"))
            readEnd = Integer.Parse("0" + scale.Item("readend"))
            precision = Integer.Parse("0" + scale.Item("precision"))
            unitRatio = Decimal.Parse("0" + scale.Item("unitratio"))

            scaleObj = New ScaleClass(dataLength, startChar, endChar, readStart, readEnd, unitRatio, precision)

            Try
                sPort.Open()
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try

            lblComEX.AutoSize = True
            lblPrinterEX.AutoSize = True
            lblComCL.AutoSize = True
            lblPrinterCL.AutoSize = True
            lblComCS.AutoSize = True
            lblPrinterCS.AutoSize = True
            lblComII.AutoSize = True
            lblPrinterII.AutoSize = True
            lblComPRT.AutoSize = True
            lblPrinterPRT.AutoSize = True
            lblComOPP.AutoSize = True
            lblPrinterOPP.AutoSize = True
            lblComPCK.AutoSize = True
            lblPrinterPCK.AutoSize = True
            lblComRTR.AutoSize = True
            lblPrinterRTR.AutoSize = True
            lblComJHT.AutoSize = True
            lblPrinterJHT.AutoSize = True
            'Added by #Steve
            lblComCI.AutoSize = True
            lblPrinterCI.AutoSize = True

            If sPort.IsOpen Then
                tick.Enabled = True
                lblComEX.Text = active_scale
                lblComCL.Text = active_scale
                lblComCS.Text = active_scale
                lblComII.Text = active_scale
                lblComPRT.Text = active_scale
                lblComOPP.Text = active_scale
                lblComPCK.Text = active_scale
                lblComRTR.Text = active_scale
                lblComJHT.Text = active_scale

                'Added by Steve
                lblComCI.Text = active_scale
            Else
                lblComEX.Text = "COM Error"
                lblComCL.Text = "COM Error"
                lblComCS.Text = "COM Error"
                lblComII.Text = "COM Error"
                lblComPRT.Text = "COM Error"
                lblComOPP.Text = "COM Error"
                lblComPCK.Text = "COM Error"
                lblComRTR.Text = "COM Error"
                lblComJHT.Text = "COM Error"

                'Added by #Steve
                lblComCI.Text = "COM Error"
            End If
        Else
            lblComEX.Text = "No COM defined"
            lblComCL.Text = "No COM defined"
            lblComCS.Text = "No COM defined"
            lblComII.Text = "No COM defined"
            lblComPRT.Text = "No COM defined"
            lblComOPP.Text = "No COM defined"
            lblComPCK.Text = "No COM defined"
            lblComRTR.Text = "No COM defined"
            lblComJHT.Text = "No COM defined"

            'Added by #Steve
            lblComCI.Text = "No COM defined"
        End If

        If File.Exists("station.txt") Then
            lblStationEX.Text = UCase(File.ReadAllText("station.txt"))
            lblStationCL.Text = UCase(File.ReadAllText("station.txt"))
            lblStationCS.Text = UCase(File.ReadAllText("station.txt"))
            lblStationII.Text = UCase(File.ReadAllText("station.txt"))
            lblStationPRT.Text = UCase(File.ReadAllText("station.txt"))
            lblStationOPP.Text = UCase(File.ReadAllText("station.txt"))
            lblStationPCK.Text = UCase(File.ReadAllText("station.txt"))
            lblStationRTR.Text = UCase(File.ReadAllText("station.txt"))
            lblStationJHT.Text = UCase(File.ReadAllText("station.txt"))
            'Added by #Steve
            lblStationCI.Text = UCase(File.ReadAllText("station.txt"))
            syncData()
        Else
            lblStationEX.Text = "UNKNOWN STATION"
            lblStationCL.Text = "UNKNOWN STATION"
            lblStationCS.Text = "UNKNOWN STATION"
            lblStationII.Text = "UNKNOWN STATION"
            lblStationPRT.Text = "UNKNOWN STATION"
            lblStationOPP.Text = "UNKNOWN STATION"
            lblStationPCK.Text = "UNKNOWN STATION"
            lblStationRTR.Text = "UNKNOWN STATION"
            lblStationJHT.Text = "UNKNOWN STATION"
            'Added by #Steve
            lblStationCI.Text = "UNKNOWN STATION"
            loadShifts()
        End If

        loadData("EXT")
        doEvents = True

        tbTungkul.Text = 2.5
        tbTungkulRTR.Text = 2.5

        Dim pd As New PrintDocument

        For Each ps As PaperSize In pd.PrinterSettings.PaperSizes
            If ps.PaperName = "FAKTUR" Then
                papersize = ps
            End If
        Next

        If papersize Is Nothing Then
            Try
                Printer.AddCustomPaperSizeToDefaultPrinter("FAKTUR", 215, 140)
                For Each ps As PaperSize In pd.PrinterSettings.PaperSizes
                    If ps.PaperName = "FAKTUR" Then
                        papersize = ps
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
            sPort.Close()

            MyBase.Dispose()
        Catch
        End Try
    End Sub

    Private Sub tick_Tick(sender As Object, e As EventArgs) Handles tick.Tick
        Dim data As String
        Dim lbl As Label

        Select Case tcLine.SelectedIndex
            Case 0
                lbl = lblWeightEX
            Case 1
                lbl = lblWeightCL
            Case 2
                lbl = lblWeightCS
            Case 9
                lbl = Label147
            Case 6
                lbl = lblWeightPCK
            Case 3
                lbl = lblWeightII
            Case Else
                lbl = lblWeightEX
        End Select

        If sPort.IsOpen Then
            scaleObj.appendData(sPort.ReadExisting())
            data = scaleObj.getValue()

            If Not IsNothing(data) Then
                lbl.Text = data
            Else
                lbl.Text = "-"
            End If
        Else
            lbl.Text = "-"
        End If
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
        Dim line As String
        Dim cmd As SQLiteCommand
        cmd = localCon.CreateCommand()
        cmd.CommandText = "SELECT ifnull(ABS(substr(MAX(substr(`doc_number`, -12)), -5)), 0)+1 FROM production_details WHERE `date` like ? AND company_code = ? and line_code = ? and station_code = ?"
        cmd.Parameters.AddWithValue("date", Now.ToString("yyyy-MM") + "%")
        cmd.Parameters.AddWithValue("company_code", corp)
        cmd.Parameters.AddWithValue("line_code", line_code)
        cmd.Parameters.AddWithValue("station_code", station_code)
        Dim num As Integer = cmd.ExecuteScalar()

        Select Case line_code
            Case "CL"
                line = "CRA"
            Case "CS"
                line = "CKA"
            Case "OPP"
                line = "POA"
            Case "PCK"
                line = "PCA"
                'Added by #Steve
            Case "CIN"
                line = "CIN"
                'end here
            Case Else
                line = line_code
        End Select
        Return line + "-" + station + "-" + Now.ToString("yyyyMM") + "-" + Strings.Right("00000" + num.ToString(), 5)
    End Function

    Private Sub btnPrintEX_Click(sender As Object, e As EventArgs) Handles btnPrintEX.Click
        'Dim label As String

        printLabel("EXT")
        'clearForm()

        'If barcode_printer = "" Then
        '    MsgBox("Please select barcode printer first")
        '    Exit Sub
        'End If

        'label = File.ReadAllText("labels/weight.txt")

        'label = label.Replace("[barcode]", "123456789")
        'label = label.Replace("[date]", Today.ToString("d MMM yyyy"))
        'label = label.Replace("[corp1]", "PT. MAKMUR BINTANG")
        'label = label.Replace("[corp2]", "PLASTINDO")
        ''label = label.Replace("[cust]", tbCust.Text)
        'label = label.Replace("[item]", tbItemName.Text)
        ''label = label.Replace("[sonum]", tbSONum.Text)

        'Printer.SendStringToPrinter(barcode_printer, label)
    End Sub

    Private Sub lblCom_Click(sender As Object, e As EventArgs) Handles lblComEX.Click, lblComCL.Click, lblComCS.Click, lblComCI.Click, lblComPCK.Click
        If role = "user" Then
            MsgBox("Access denied")
            Exit Sub
        End If

        Dim fSetting As New ScaleSettings
        Dim scale As ListViewItem

        'fSetting.Owner = Parent.Parent.Parent.Parent.Parent
        fSetting.active_scale = active_scale
        fSetting.uc = Me
        If fSetting.ShowDialog() = DialogResult.OK Then
            tick.Enabled = False
            buffer = ""
            sPort.Close()

            sPort.PortName = fSetting.cbPortList.Text

            scale = fSetting.ListView1.SelectedItems.Item(0)
            useScale(scale, fSetting.cbPortList.Text)

            Dim conf() As String
            conf = scale.SubItems(2).Text.Split(";")
            sPort.BaudRate = Integer.Parse("0" + conf(0))
            Select Case conf(1)
                Case "N"
                    sPort.Parity = Ports.Parity.None
                Case "O"
                    sPort.Parity = Ports.Parity.Odd
                Case "E"
                    sPort.Parity = Ports.Parity.Even
                Case "M"
                    sPort.Parity = Ports.Parity.Mark
                Case "S"
                    sPort.Parity = Ports.Parity.Space
            End Select
            sPort.DataBits = Integer.Parse("0" + conf(2))
            sPort.StopBits = Integer.Parse("0" + conf(3))
            continuous = CBool(Integer.Parse("0" + scale.SubItems(3).Text))
            dataLength = Integer.Parse("0" + scale.SubItems(4).Text)

            Dim x As Integer = Integer.Parse("0" + scale.SubItems(5).Text)
            If x > 0 Then
                startChar = Chr(x)
            Else
                startChar = Nothing
            End If
            x = Integer.Parse("0" + scale.SubItems(6).Text)
            If x > 0 Then
                endChar = Chr(x)
            Else
                endChar = Nothing
            End If
            readStart = Integer.Parse("0" + scale.SubItems(7).Text)
            readEnd = Integer.Parse("0" + scale.SubItems(8).Text)
            precision = Integer.Parse("0" + scale.SubItems(9).Text)
            unitRatio = Decimal.Parse("0" + scale.SubItems(10).Text)

            scaleObj = New ScaleClass(dataLength, startChar, endChar, readStart, readEnd, unitRatio, precision)

            Try
                sPort.Open()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            If sPort.IsOpen Then
                tick.Enabled = True
                lblComEX.Text = active_scale
                lblComCL.Text = active_scale
                lblComCS.Text = active_scale
                lblComII.Text = active_scale

                'added by #Steve
                lblComPCK.Text = active_scale
                lblComCI.Text = active_scale
            Else
                lblComEX.Text = "COM Error"
                lblComCL.Text = "COM Error"
                lblComCS.Text = "COM Error"
                lblComII.Text = "COM Error"
                lblComPCK.Text = "COM Error"
                lblComCI.Text = "COM Error"
            End If
        End If

        fSetting.Dispose()
    End Sub

    Private Sub lblPrinter_Click(sender As Object, e As EventArgs) Handles lblPrinterEX.Click
        Dim pd As New PrintDialog()
        pd.PrinterSettings = New PrinterSettings()
        pd.UseEXDialog = True
        If (pd.ShowDialog() = DialogResult.OK) Then
            usePrinter(pd.PrinterSettings.PrinterName)
        End If
    End Sub

    Private Sub ScaleLabeling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        init()
    End Sub

    'Private Sub Find_Click(sender As Object, e As EventArgs) Handles btnItemFind.Click
    '    Dim x As Integer, y As Integer, h As Integer

    '    x = DirectCast(sender, Button).Left
    '    y = DirectCast(sender, Button).Top
    '    h = DirectCast(sender, Button).Height

    '    Select Case DirectCast(sender, Button).Name
    '        Case "btnSONumFind"
    '        '    textFocus = tbSONum
    '        Case "btnCustFind"
    '        '    textFocus = tbCust
    '        'Case "btnLaborFind"
    '        '    textFocus = tbLabor
    '        Case "btnItemFind"
    '            textFocus = tbItem
    '    End Select

    '    lstFind.Left = x - 176 + gbLabel.Left
    '    lstFind.Top = y + h + gbLabel.Top

    '    lstFind.Visible = True
    '    textFocus.Focus()
    'End Sub

    Private Sub lstFind_LostFocus(sender As Object, e As EventArgs) Handles lstFind.LostFocus, tbItem.LostFocus, tbAnyamanCL.LostFocus,
            tbAnyamanCS.LostFocus, tbAnyamanII.LostFocus, tbAnyamanOPP.LostFocus, tbAnyamanPCK.LostFocus, tbAnyamanPRT.LostFocus, btnAnyamanRTR.LostFocus,
            tbDenierCL.LostFocus, tbDenierCS.LostFocus, tbDenierEX.LostFocus, tbDenierII.LostFocus, tbDenierOPP.LostFocus, tbDenierPCK.LostFocus,
            tbDenierPRT.LostFocus, tbDenierRTR.LostFocus, tbLebarCL.LostFocus, tbLebarCS.LostFocus, tbLebarII.LostFocus, tbLebarOPP.LostFocus, tbLebarPCK.LostFocus,
            tbLebarPRT.LostFocus, tbLebarRTR.LostFocus, tbBobbinType.LostFocus, tbPIC1CL.LostFocus, tbPIC1CS.LostFocus, tbPIC1EX.LostFocus, tbPIC1II.LostFocus,
            tbPIC1OPP.LostFocus, tbPIC1PCK.LostFocus, tbPIC1PRT.LostFocus, tbPIC1RTR.LostFocus, tbPIC2CL.LostFocus, tbPIC2CS.LostFocus, tbPIC2EX.LostFocus,
            tbPIC2II.LostFocus, tbPIC2OPP.LostFocus, tbPIC2PCK.LostFocus, tbPIC2PRT.LostFocus, tbPIC2RTR.LostFocus, tbMesinCL.LostFocus,
            tbMesinCS.LostFocus, tbMesinEX.LostFocus, tbMesinPCK.LostFocus, tbMesinPRT.LostFocus, tbMesinRTR.LostFocus, tbCustomerCS.LostFocus, tbCustomerII.LostFocus,
            tbCustomerJHT.LostFocus, tbCustomerOPP.LostFocus, tbCustomerPCK.LostFocus, tbCustomerPRT.LostFocus, tbCustomerRTR.LostFocus,            'add by #Steve
            TextBox3.LostFocus, TextBox5.LostFocus, TextBox6.LostFocus, TextBox7.LostFocus, TextBox9.LostFocus, TextBox10.LostFocus
        If Not IsNothing(textFocus) Then
            If Not textFocus.Focused And Not lstFind.Focused Then
                lstFind.Visible = False
            End If
        Else
            lstFind.Visible = False
        End If
    End Sub

    Private Sub sync(table As String, station_code As String)
        Dim syncThd As New syncThread(_parent, dbCon, localCon, corp, station_code)
        Dim thd As Thread = Nothing
        Select Case table
            Case "employees", "customers", "items", "shifts", "bobbins", "machines", "production_details"
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

    Private Sub lblStation_Click(sender As Object, e As EventArgs) Handles lblStationEX.Click, lblStationCL.Click, lblStationCS.Click
        Dim station As New Station()

        If lblStationEX.Text <> "UNKNOWN" Then
            station.tbStation.Text = lblStationEX.Text
        End If
        If station.ShowDialog = DialogResult.OK Then
            lblStationEX.Text = station.tbStation.Text
            lblStationCL.Text = station.tbStation.Text
            lblStationCS.Text = station.tbStation.Text
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

    Private Sub btnTroli_Click(sender As Object, e As EventArgs) Handles btnTroli.Click, tbTroli.Click
        If lblWeightEX.Text = "-" Then Exit Sub
        tbTroli.Text = lblWeightEX.Text
    End Sub

    Private Sub btnBrutoEX_Click(sender As Object, e As EventArgs) Handles btnBrutoEX.Click, tbBrutoEX.Click
        If lblWeightEX.Text = "-" Then Exit Sub
        tbBrutoEX.Text = lblWeightEX.Text
    End Sub

    'Private Sub btnNettoEX_Click(sender As Object, e As EventArgs)
    '    tbNettoEX.Text = lblWeightEX.Text
    'End Sub

    'Private Sub tbWeightEX_TextChanged(sender As Object, e As EventArgs) Handles tbBrutoEX.TextChanged, tbTroli.TextChanged, tbBobbinWeight.TextChanged, tbBobbin.TextChanged
    '    Dim v As Decimal

    '    v = Decimal.Parse("0" + tbBrutoEX.Text) - Decimal.Parse("0" + tbTroli.Text) - (Decimal.Parse("0" + tbBobbinWeight.Text) * Decimal.Parse("0" + tbBobbin.Text))

    '    tbNettoEX.Text = String.Format(numformat, v)
    'End Sub

    Private Sub calcWeight(line As String)
        Dim v As Decimal
        Select Case line
            Case "EXT"
                v = tbBrutoEX.Text - tbTroli.Text - tbBobbinWeight.Text * tbBobbin.Text
                tbNettoEX.Text = String.Format(numformat, v)
            Case "CL"
                v = Decimal.Parse("0" + tbBrutoCL.Text) - Decimal.Parse("0" + tbTungkul.Text)
                tbNettoCL.Text = String.Format(numformat, v)
                Dim pjg = Decimal.Parse("0" + tbPanjangCL.Text)
                If (pjg <> 0) Then
                    lblGrammatur.Text = (v * 1000 / pjg)
                Else
                    lblGrammatur.Text = "-"
                End If
            Case "RTR"
                v = Decimal.Parse("0" + tbBrutoRTR.Text) - Decimal.Parse("0" + tbTungkulRTR.Text)
                tbNettoRTR.Text = String.Format(numformat, v)
        End Select

        'Select Case line
        '    Case "EXT"
        '        v = Decimal.Parse("0" + tbBrutoEX.Text) - Decimal.Parse("0" + tbTroli.Text) - (Decimal.Parse("0" + tbBobbinWeight.Text) * Decimal.Parse("0" + tbBobbin.Text))
        '        tbNettoEX.Text = String.Format(numformat, v)
        '    Case "CL"
        '        v = Decimal.Parse("0" + tbBrutoCL.Text) - Decimal.Parse("0" + tbTungkul.Text)
        '        tbNettoCL.Text = String.Format(numformat, v)
        '        Dim pjg = Decimal.Parse("0" + tbPanjangCL.Text)
        '        If (pjg <> 0) Then
        '            lblGrammatur.Text = String.Format(numformat, v * 1000 / pjg)
        '        Else
        '            lblGrammatur.Text = "-"
        '        End If
        '    Case "RTR"
        '        v = Decimal.Parse("0" + tbBrutoRTR.Text) - Decimal.Parse("0" + tbTungkulRTR.Text)
        '        tbNettoRTR.Text = String.Format(numformat, v)
        'End Select
    End Sub

    'Private Sub addListItem()

    'End Sub

    Private Sub printLabel(line As String)
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

        Dim cmd As SQLiteCommand
        Dim docnum As String = ""
        Dim cansave As Boolean = True

        textFocus = Nothing
        textCode = Nothing

        Select Case line
            Case "EXT"
                If tbDenierEX.Text = "" Or tbWarnaEX.Text = "" Or tbPanenEX.Text = "" Or tbBobbinType.Text = "" Or tbBobbin.Text = "" Or tbMesinEX.Text = "" Or
                    tbTroli.Text = "" Or tbBrutoEX.Text = "" Or tbNettoEX.Text = "" Or tbPIC1EX.Text = "" Then
                    cansave = False
                End If
            Case "CL", "CRA"
                If tbDenierCL.Text = "" Or tbWarnaCL.Text = "" Or tbAnyamanCL.Text = "" Or tbLebarCL.Text = "" Or tbPanjangCL.Text = "" Or tbMesinCL.Text = "" Or
                    tbTungkul.Text = "" Or tbBrutoCL.Text = "" Or tbNettoCL.Text = "" Or tbPemotongCL.Text = "" Or tbPIC1CL.Text = "" Then
                    cansave = False
                End If
            Case "RTR"
                If tbDenierRTR.Text = "" Or tbWarnaRTR.Text = "" Or tbAnyamanRTR.Text = "" Or tbLebarRTR.Text = "" Or tbPanjangRTR.Text = "" Or tbCetakKeRTR.Text = "" Or
                    tbTungkul.Text = "" Or tbBrutoRTR.Text = "" Or tbNettoRTR.Text = "" Or tbCetakanRTR.Text = "" Or tbPIC1RTR.Text = "" Then
                    cansave = False
                End If
            Case "CS", "CKA"
                If tbDenierCS.Text = "" Or tbJenisCS.Text = "" Or tbAnyamanCS.Text = "" Or tbLebarCS.Text = "" Or tbPanjangCS.Text = "" Or tbMesinCS.Text = "" Or
                    tbJumlahCS.Text = "" Or tbUrutanCS.Text = "" Or tbPIC1CS.Text = "" Then
                    cansave = False
                End If
            Case "INR"
                If tbDenierII.Text = "" Or tbPaletII.Text = "" Or tbAnyamanII.Text = "" Or tbLebarII.Text = "" Or tbPanjangII.Text = "" Or tbCetakanII.Text = "" Or
                    tbJumlahII.Text = "" Or tbPIC1II.Text = "" Then
                    cansave = False
                End If
            Case "PRT"
                If tbDenierPRT.Text = "" Or tbCetakanPRT.Text = "" Or tbAnyamanPRT.Text = "" Or tbLebarPRT.Text = "" Or tbPanjangPRT.Text = "" Or tbMesinPRT.Text = "" Or
                    tbJumlahPRT.Text = "" Or cbLanjutPRT.Text = "" Or tbPaletPRT.Text = "" Or tbPIC1PRT.Text = "" Then
                    cansave = False
                End If
            Case "OPP"
                If tbDenierOPP.Text = "" Or tbCetakanOPP.Text = "" Or tbAnyamanOPP.Text = "" Or tbLebarOPP.Text = "" Or tbPanjangOPP.Text = "" Or tbBrutoOPP.Text = "" Or
                    tbNettoOPP.Text = "" Or tbOPP.Text = "" Or tbPIC1OPP.Text = "" Then
                    cansave = False
                End If
                'Added by #Steve
            Case "CIN"
                If TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Or TextBox9.Text = "" Or
                    TextBox10.Text = "" Then
                    cansave = False
                End If
            Case "PCK"
                If tbLebarPCK.Text = "" Or tbPanjangPCK.Text = "" Or tbAnyamanPCK.Text = "" Or tbDenierPCK.Text = "" Or tbCetakanPCK.Text = "" Or tbJumlahPCK.Text = "" Or
                      tbMesinPCK.Text = "" Or tbBeratPCK.Text = "" Or tbBeratPCK.Text = "-" Or tbBeratPCK.Text = "0" Or tbBeratPCK.Text = "0.0" OrElse tbBeratPCK.Text = "0,0" Then
                    cansave = False
                End If

        End Select

        If Not cansave Then
            MsgBox("Data belum lengkap")
            Exit Sub
        End If

        If localCon.State = ConnectionState.Open Then
            Dim data As New Dictionary(Of String, Object)
            Dim dt As DateTime = Now

            cmd = localCon.CreateCommand()
            cmd.CommandText = "INSERT INTO production_details(company_code, area_code, station_code, 
                                                                doc_number, date, shift, data, anyaman, denier, 
                                                                lebar, panjang, cetakan, opp, warna, line_code, 
                                                                item_code, qty, creator, pic1, pic2, pic1code, pic2code) 
                                VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("area_code", area)
            cmd.Parameters.AddWithValue("station_code", lblStationEX.Text)

            Try
                Select Case line
                    Case "EXT"
                        data.Add("denier", Decimal.Parse("0" + tbDenierEX.Text))
                        data.Add("warna", tbWarnaEX.Text)
                        data.Add("panen", Decimal.Parse("0" + tbPanenEX.Text))
                        data.Add("jml_bobbin", Decimal.Parse("0" + tbBobbin.Text))
                        data.Add("bobbin", tbBobbinType.Text)
                        data.Add("berat_bobbin", Decimal.Parse("0" + tbBobbinWeight.Text))
                        data.Add("mesin", tbMesinEX.Text)
                        data.Add("berat_troli", Decimal.Parse("0" + tbTroli.Text))
                        data.Add("bruto", Decimal.Parse("0" + tbBrutoEX.Text))
                        data.Add("netto", Decimal.Parse("0" + tbNettoEX.Text))
                        data.Add("team", tbTeamEX.Text)

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Nothing)
                        cmd.Parameters.AddWithValue("denier", Decimal.Parse("0" + tbDenierEX.Text))
                        cmd.Parameters.AddWithValue("lebar", Nothing)
                        cmd.Parameters.AddWithValue("panjang", Nothing)
                        cmd.Parameters.AddWithValue("cetakan", Nothing)
                        cmd.Parameters.AddWithValue("opp", Nothing)
                        cmd.Parameters.AddWithValue("warna", tbWarnaEX.Text)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", tbNettoEX.Text)
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", tbPIC1EX.Text)
                        cmd.Parameters.AddWithValue("pic2", tbPIC2EX.Text)
                        cmd.Parameters.AddWithValue("pic1code", tbPIC1CodeEX.Text)
                        cmd.Parameters.AddWithValue("pic2code", tbPIC2CodeEX.Text)
                    Case "CL", "CRA"
                        data.Add("anyaman", Decimal.Parse("0" + tbAnyamanCL.Text))
                        data.Add("denier", Decimal.Parse("0" + tbDenierCL.Text))
                        data.Add("lebar", Decimal.Parse("0" + tbLebarCL.Text))
                        data.Add("panjang", Decimal.Parse("0" + tbPanjangCL.Text))
                        data.Add("warna", tbWarnaCL.Text)
                        data.Add("mesin", tbMesinCL.Text)
                        data.Add("berat_tungkul", Decimal.Parse("0" + tbTungkul.Text))
                        data.Add("bruto", Decimal.Parse("0" + tbBrutoCL.Text))
                        data.Add("netto", Decimal.Parse("0" + tbNettoCL.Text))
                        data.Add("meter_mesin", Decimal.Parse("0" + tbMeterMesinCL.Text))
                        data.Add("bs_roll", Decimal.Parse("0" + tbBSRollCL.Text))
                        data.Add("pemotong", tbPemotongCL.Text)

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Decimal.Parse("0" + tbAnyamanCL.Text))
                        cmd.Parameters.AddWithValue("denier", Decimal.Parse("0" + tbDenierCL.Text))
                        cmd.Parameters.AddWithValue("lebar", Decimal.Parse("0" + tbLebarCL.Text))
                        cmd.Parameters.AddWithValue("panjang", Decimal.Parse("0" + tbPanjangCL.Text))
                        cmd.Parameters.AddWithValue("cetakan", Nothing)
                        cmd.Parameters.AddWithValue("opp", Nothing)
                        cmd.Parameters.AddWithValue("warna", tbWarnaCL.Text)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", Decimal.Parse("0" + tbNettoCL.Text))
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", tbPIC1CL.Text)
                        cmd.Parameters.AddWithValue("pic2", tbPIC2CL.Text)
                        cmd.Parameters.AddWithValue("pic1code", tbPIC1CodeCL.Text)
                        cmd.Parameters.AddWithValue("pic2code", tbPIC2CodeCL.Text)
                    Case "RTR"
                        data.Add("anyaman", Decimal.Parse("0" + tbAnyamanRTR.Text))
                        data.Add("denier", Decimal.Parse("0" + tbDenierRTR.Text))
                        data.Add("lebar", Decimal.Parse("0" + tbLebarRTR.Text))
                        data.Add("panjang", Decimal.Parse("0" + tbPanjangRTR.Text))
                        data.Add("warna", tbWarnaRTR.Text)
                        data.Add("mesin", tbMesinRTR.Text)
                        data.Add("berat_tungkul", Decimal.Parse("0" + tbTungkulRTR.Text))
                        data.Add("bruto", Decimal.Parse("0" + tbBrutoRTR.Text))
                        data.Add("netto", Decimal.Parse("0" + tbNettoRTR.Text))
                        data.Add("cust_po", tbCustPORTR.Text)
                        data.Add("customer", tbCustomerRTR.Text)
                        data.Add("customer_code", tbCustCodeRTR.Text)
                        data.Add("cetakan_ke", tbCetakKeRTR.Text)

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Decimal.Parse("0" + tbAnyamanRTR.Text))
                        cmd.Parameters.AddWithValue("denier", Decimal.Parse("0" + tbDenierRTR.Text))
                        cmd.Parameters.AddWithValue("lebar", Decimal.Parse("0" + tbLebarRTR.Text))
                        cmd.Parameters.AddWithValue("panjang", Decimal.Parse("0" + tbPanjangRTR.Text))
                        cmd.Parameters.AddWithValue("cetakan", tbCetakanRTR.Text)
                        cmd.Parameters.AddWithValue("opp", Nothing)
                        cmd.Parameters.AddWithValue("warna", tbWarnaRTR.Text)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", Decimal.Parse("0" + tbNettoRTR.Text))
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", tbPIC1RTR.Text)
                        cmd.Parameters.AddWithValue("pic2", tbPIC2RTR.Text)
                        cmd.Parameters.AddWithValue("pic1code", tbPIC1CodeRTR.Text)
                        cmd.Parameters.AddWithValue("pic2code", tbPIC2CodeRTR.Text)
                    Case "CS", "CKA"
                        data.Add("anyaman", Decimal.Parse("0" + tbAnyamanCS.Text))
                        data.Add("denier", Decimal.Parse("0" + tbDenierCS.Text))
                        data.Add("lebar", Decimal.Parse("0" + tbLebarCS.Text))
                        data.Add("panjang", Decimal.Parse("0" + tbPanjangCS.Text))
                        data.Add("jumlah", Decimal.Parse("0" + tbJumlahCS.Text))
                        data.Add("jenis", tbJenisCS.Text)
                        data.Add("mesin", tbMesinCS.Text)
                        data.Add("is_bs", cbBSCS.Checked)
                        data.Add("urutan", tbUrutanCS.Text)
                        data.Add("pemotong", tbPIC1CS.Text)
                        data.Add("team", tbTeamCS.Text)
                        data.Add("customer", tbCustomerCS.Text)
                        data.Add("customer_code", tbCustCodeCS.Text)

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Decimal.Parse("0" + tbAnyamanCS.Text))
                        cmd.Parameters.AddWithValue("denier", Decimal.Parse("0" + tbDenierCS.Text))
                        cmd.Parameters.AddWithValue("lebar", Decimal.Parse("0" + tbLebarCS.Text))
                        cmd.Parameters.AddWithValue("panjang", Decimal.Parse("0" + tbPanjangCS.Text))
                        cmd.Parameters.AddWithValue("cetakan", Nothing)
                        cmd.Parameters.AddWithValue("opp", Nothing)
                        cmd.Parameters.AddWithValue("warna", tbJenisCS.Text)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", Decimal.Parse("0" + tbJumlahCS.Text))
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", tbPIC1CS.Text)
                        cmd.Parameters.AddWithValue("pic2", tbPIC2CS.Text)
                        cmd.Parameters.AddWithValue("pic1code", tbPIC1CodeCS.Text)
                        cmd.Parameters.AddWithValue("pic2code", tbPIC2CodeCS.Text)
                    Case "INR"
                        data.Add("shift", cbShifts.Text)
                        data.Add("anyaman", Decimal.Parse("0" + tbAnyamanII.Text))
                        data.Add("denier", Decimal.Parse("0" + tbDenierII.Text))
                        data.Add("lebar", Decimal.Parse("0" + tbLebarII.Text))
                        data.Add("panjang", Decimal.Parse("0" + tbPanjangII.Text))
                        data.Add("jumlah", Decimal.Parse("0" + tbJumlahII.Text))
                        data.Add("cetakan", tbCetakanII.Text)
                        data.Add("so_num", tbSOII.Text)
                        data.Add("palet", Integer.Parse("0" + tbPaletII.Text))
                        data.Add("operator", tbPIC1II.Text)
                        data.Add("penerima", tbPIC2II.Text)
                        data.Add("team", tbTeamII.Text)
                        data.Add("customer", tbCustomerII.Text)
                        data.Add("customer_code", tbCustCodeII.Text)

                        'Added by #Steve
                        data.Add("berat", Decimal.Parse("0" + tbBeratINR.Text))

                        If CheckBox3.Checked = True Then
                            data.Add("lebarinner", Decimal.Parse("0" + tbLebarINR.Text))
                            data.Add("panjanginner", Decimal.Parse("0" + tbPanjangINR.Text))
                            data.Add("tebalinner", Decimal.Parse("0" + tbTebalINR.Text))
                        End If
                        'end here

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Decimal.Parse("0" + tbAnyamanII.Text))
                        cmd.Parameters.AddWithValue("denier", Decimal.Parse("0" + tbDenierII.Text))
                        cmd.Parameters.AddWithValue("lebar", Decimal.Parse("0" + tbLebarII.Text))
                        cmd.Parameters.AddWithValue("panjang", Decimal.Parse("0" + tbPanjangII.Text))
                        cmd.Parameters.AddWithValue("cetakan", tbCetakanII.Text)
                        cmd.Parameters.AddWithValue("opp", Nothing)
                        cmd.Parameters.AddWithValue("warna", Nothing)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", Decimal.Parse("0" + tbJumlahII.Text))
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", tbPIC1II.Text)
                        cmd.Parameters.AddWithValue("pic2", tbPIC2II.Text)
                        cmd.Parameters.AddWithValue("pic1code", tbPIC1CodeII.Text)
                        cmd.Parameters.AddWithValue("pic2code", tbPIC2CodeII.Text)
                    Case "JHT"
                        data.Add("shift", cbShifts.Text)
                        data.Add("anyaman", Decimal.Parse("0" + tbAnyamanJHT.Text))
                        data.Add("denier", Decimal.Parse("0" + tbDenierJHT.Text))
                        data.Add("lebar", Decimal.Parse("0" + tbLebarJHT.Text))
                        data.Add("panjang", Decimal.Parse("0" + tbPanjangJHT.Text))
                        data.Add("jumlah", Decimal.Parse("0" + tbJumlahJHT.Text))
                        data.Add("cetakan", tbCetakanJHT.Text)
                        data.Add("so_num", tbSOJHT.Text)
                        data.Add("palet", Integer.Parse("0" + tbPaletJHT.Text))
                        data.Add("operator", tbPIC1JHT.Text)
                        data.Add("penerima", tbPIC2JHT.Text)
                        data.Add("team", tbTeamJHT.Text)
                        data.Add("customer", tbCustomerJHT.Text)
                        data.Add("customer_code", tbCustCodeJHT.Text)

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Decimal.Parse("0" + tbAnyamanJHT.Text))
                        cmd.Parameters.AddWithValue("denier", Decimal.Parse("0" + tbDenierJHT.Text))
                        cmd.Parameters.AddWithValue("lebar", Decimal.Parse("0" + tbLebarJHT.Text))
                        cmd.Parameters.AddWithValue("panjang", Decimal.Parse("0" + tbPanjangJHT.Text))
                        cmd.Parameters.AddWithValue("cetakan", tbCetakanJHT.Text)
                        cmd.Parameters.AddWithValue("opp", Nothing)
                        cmd.Parameters.AddWithValue("warna", Nothing)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", Decimal.Parse("0" + tbJumlahJHT.Text))
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", tbPIC1JHT.Text)
                        cmd.Parameters.AddWithValue("pic2", tbPIC2JHT.Text)
                        cmd.Parameters.AddWithValue("pic1code", tbPIC1CodeJHT.Text)
                        cmd.Parameters.AddWithValue("pic2code", tbPIC2CodeJHT.Text)
                    Case "PRT"
                        data.Add("anyaman", Decimal.Parse("0" + tbAnyamanPRT.Text))
                        data.Add("denier", Decimal.Parse("0" + tbDenierPRT.Text))
                        data.Add("lebar", Decimal.Parse("0" + tbLebarPRT.Text))
                        data.Add("panjang", Decimal.Parse("0" + tbPanjangPRT.Text))
                        data.Add("cetakan", tbCetakanPRT.Text)
                        data.Add("jumlah", Decimal.Parse("0" + tbJumlahPRT.Text))
                        data.Add("bruto", Decimal.Parse("0" + tbBeratPRT.Text))
                        data.Add("lanjut", cbLanjutPRT.Text)
                        data.Add("pekerja", tbPIC1PRT.Text)
                        data.Add("penerima", tbPIC2PRT.Text)
                        data.Add("team", tbTeamPRT.Text)
                        data.Add("mesin", tbMesinPRT.Text)
                        data.Add("no_palet", tbPaletPRT.Text)
                        data.Add("cust_po", tbCustPOPRT.Text)
                        data.Add("customer", tbCustomerPRT.Text)
                        data.Add("customer_code", tbCustCodePRT.Text)
                        data.Add("so_number", tbSOPRT.Text)

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Decimal.Parse("0" + tbAnyamanPRT.Text))
                        cmd.Parameters.AddWithValue("denier", Decimal.Parse("0" + tbDenierPRT.Text))
                        cmd.Parameters.AddWithValue("lebar", Decimal.Parse("0" + tbLebarPRT.Text))
                        cmd.Parameters.AddWithValue("panjang", Decimal.Parse("0" + tbPanjangPRT.Text))
                        cmd.Parameters.AddWithValue("cetakan", tbCetakanPRT.Text)
                        cmd.Parameters.AddWithValue("opp", Nothing)
                        cmd.Parameters.AddWithValue("warna", Nothing)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", Decimal.Parse("0" + tbJumlahPRT.Text))
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", tbPIC1PRT.Text)
                        cmd.Parameters.AddWithValue("pic2", tbPIC2PRT.Text)
                        cmd.Parameters.AddWithValue("pic1code", tbPIC1CodePRT.Text)
                        cmd.Parameters.AddWithValue("pic2code", tbPIC2CodePRT.Text)
                    Case "OPP"
                        data.Add("anyaman", Decimal.Parse("0" + tbAnyamanOPP.Text))
                        data.Add("denier", Decimal.Parse("0" + tbDenierOPP.Text))
                        data.Add("lebar", Decimal.Parse("0" + tbLebarOPP.Text))
                        data.Add("panjang", Decimal.Parse("0" + tbPanjangOPP.Text))
                        data.Add("cetakan", tbCetakanOPP.Text)
                        data.Add("bruto", Decimal.Parse("0" + tbBrutoOPP.Text))
                        data.Add("netto", Decimal.Parse("0" + tbNettoOPP.Text))
                        data.Add("pekerja", tbPIC1OPP.Text)
                        data.Add("penerima", tbPIC2OPP.Text)
                        data.Add("team", tbTeamOPP.Text)
                        data.Add("cust_po", tbCustPOOPP.Text)
                        data.Add("customer", tbCustomerOPP.Text)
                        data.Add("customer_code", tbCustCodeOPP.Text)
                        data.Add("so_number", tbSOOPP.Text)

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Decimal.Parse("0" + tbAnyamanOPP.Text))
                        cmd.Parameters.AddWithValue("denier", Decimal.Parse("0" + tbDenierOPP.Text))
                        cmd.Parameters.AddWithValue("lebar", Decimal.Parse("0" + tbLebarOPP.Text))
                        cmd.Parameters.AddWithValue("panjang", Decimal.Parse("0" + tbPanjangOPP.Text))
                        cmd.Parameters.AddWithValue("cetakan", tbCetakanOPP.Text)
                        cmd.Parameters.AddWithValue("opp", Decimal.Parse("0" + tbOPP.Text))
                        cmd.Parameters.AddWithValue("warna", Nothing)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", Decimal.Parse("0" + tbNettoOPP.Text))
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", tbPIC1OPP.Text)
                        cmd.Parameters.AddWithValue("pic2", tbPIC2OPP.Text)
                        cmd.Parameters.AddWithValue("pic1code", tbPIC1CodeOPP.Text)
                        cmd.Parameters.AddWithValue("pic2code", tbPIC2CodeOPP.Text)
                    Case "PCK", "PCA"
                        data.Add("anyaman", Decimal.Parse("0" + tbAnyamanPCK.Text))
                        data.Add("denier", Decimal.Parse("0" + tbDenierPCK.Text))
                        data.Add("lebar", Decimal.Parse("0" + tbLebarPCK.Text))
                        data.Add("panjang", Decimal.Parse("0" + tbPanjangPCK.Text))
                        data.Add("cetakan", tbCetakanPCK.Text)
                        data.Add("jumlah", Decimal.Parse("0" + tbJumlahPCK.Text))
                        data.Add("ball", Decimal.Parse("0" + tbBallPCK.Text))

                        If ComboPCK.Text = "10 Kg" Then
                            data.Add("berat", ((Decimal.Parse("0" + tbBeratPCK.Text - 1 / 2) * 1000) + 21.89) / 1000)
                        ElseIf ComboPCK.Text = "15 Kg" Then
                            data.Add("berat", ((Decimal.Parse("0" + tbBeratPCK.Text - 1 / 2) * 1000) + 25.6) / 1000)
                        ElseIf ComboPCK.Text = "20 Kg" Then
                            data.Add("berat", ((Decimal.Parse("0" + tbBeratPCK.Text - 1 / 2) * 1000) + 30.4) / 1000)
                        ElseIf ComboPCK.Text = "30 Kg" Then
                            data.Add("berat", ((Decimal.Parse("0" + tbBeratPCK.Text - 1 / 2) * 1000) + 40.08) / 1000)
                        ElseIf ComboPCK.Text = "LAO YING" Then
                            data.Add("berat", ((Decimal.Parse("0" + tbBeratPCK.Text - 1 / 2) * 1000) + 53.31) / 1000)
                        Else
                            data.Add("berat", Decimal.Parse("0" + tbBeratPCK.Text - 1 / 2))
                        End If

                        data.Add("mesin", Decimal.Parse("0" + tbMesinPCK.Text))
                        data.Add("pekerja", tbPIC1PCK.Text)
                        data.Add("penerima", tbPIC2PCK.Text)
                        data.Add("team", tbTeamPCK.Text)
                        data.Add("customer", tbCustomerPCK.Text)
                        data.Add("customer_code", tbCustCodePCK.Text)
                        data.Add("so_number", tbSOPCK.Text)

                        ''Added by #Steve          

                        If CheckBox1.Checked = True Then
                            data.Add("lebarinner", Decimal.Parse("0" + tbLebar.Text))
                            data.Add("panjanginner", Decimal.Parse("0" + tbPanjang.Text))
                            data.Add("tebalinner", Decimal.Parse("0" + tbTebal.Text))
                        End If
                        'end here

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Decimal.Parse("0" + tbAnyamanPCK.Text))
                        cmd.Parameters.AddWithValue("denier", Decimal.Parse("0" + tbDenierPCK.Text))
                        cmd.Parameters.AddWithValue("lebar", Decimal.Parse("0" + tbLebarPCK.Text))
                        cmd.Parameters.AddWithValue("panjang", Decimal.Parse("0" + tbPanjangPCK.Text))
                        cmd.Parameters.AddWithValue("cetakan", tbCetakanPCK.Text)
                        cmd.Parameters.AddWithValue("opp", Nothing)
                        cmd.Parameters.AddWithValue("warna", Nothing)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", Decimal.Parse("0" + tbJumlahPCK.Text))
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", tbPIC1PCK.Text)
                        cmd.Parameters.AddWithValue("pic2", tbPIC2PCK.Text)
                        cmd.Parameters.AddWithValue("pic1code", tbPIC1CodePCK.Text)
                        cmd.Parameters.AddWithValue("pic2code", tbPIC2CodePCK.Text)

                        'Added by #Steve
                    Case "CIN"
                        data.Add("shift", cbShifts.Text)
                        data.Add("anyaman", TextBox9.Text)
                        data.Add("panjang", Decimal.Parse("0" + TextBox7.Text))
                        data.Add("lebar", Decimal.Parse("0" + TextBox3.Text))
                        data.Add("jumlah", Decimal.Parse("0" + TextBox4.Text))
                        data.Add("berat", Decimal.Parse("0" + TextBox12.Text))
                        data.Add("jenis", TextBox8.Text)
                        data.Add("mesin", TextBox10.Text)
                        data.Add("customer", TextBox6.Text)
                        data.Add("pemotong", TextBox5.Text)

                        docnum = getNextNumber(line, lblStationEX.Text)
                        cmd.Parameters.AddWithValue("doc_number", docnum)
                        cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd HH:mm:ss"))
                        cmd.Parameters.AddWithValue("shift", cbShifts.Text)
                        cmd.Parameters.AddWithValue("data", json.Serialize(data))
                        cmd.Parameters.AddWithValue("anyaman", Decimal.Parse("0" + TextBox9.Text))
                        cmd.Parameters.AddWithValue("denier", Nothing)
                        cmd.Parameters.AddWithValue("lebar", Decimal.Parse("0" + TextBox3.Text))
                        cmd.Parameters.AddWithValue("panjang", Decimal.Parse("0" + TextBox7.Text))
                        cmd.Parameters.AddWithValue("cetakan", Nothing)
                        cmd.Parameters.AddWithValue("opp", Nothing)
                        cmd.Parameters.AddWithValue("warna", Nothing)
                        cmd.Parameters.AddWithValue("line_code", line)
                        cmd.Parameters.AddWithValue("item_code", tbItem.Text)
                        cmd.Parameters.AddWithValue("qty", Decimal.Parse("0" + TextBox4.Text))
                        cmd.Parameters.AddWithValue("creator", user)
                        cmd.Parameters.AddWithValue("pic1", TextBox5.Text)
                        cmd.Parameters.AddWithValue("pic2", "")
                        cmd.Parameters.AddWithValue("pic1code", "")
                        cmd.Parameters.AddWithValue("pic2code", "")
                End Select

                cmd.ExecuteNonQuery()

                Dim repdata As New List(Of Dictionary(Of String, Object))
                Dim r As New Dictionary(Of String, Object)

                r.Add("doc_number", docnum)
                r.Add("tanggal", dt)
                r.Add("shift", cbShifts.Text)
                Select Case line
                    Case "EXT"
                        r.Add("denier", Decimal.Parse("0" + tbDenierEX.Text))
                        r.Add("warna", tbWarnaEX.Text)
                        r.Add("panen", Decimal.Parse("0" + tbPanenEX.Text))
                        r.Add("jml_bobbin", Decimal.Parse("0" + tbBobbin.Text))
                        r.Add("bobbin", tbBobbinType.Text)
                        r.Add("berat_bobbin", Decimal.Parse("0" + tbBobbinWeight.Text))
                        r.Add("mesin", tbMesinEX.Text)
                        r.Add("berat_troli", Decimal.Parse("0" + tbTroli.Text))
                        r.Add("bruto", Decimal.Parse("0" + tbBrutoEX.Text))
                        r.Add("netto", Decimal.Parse("0" + tbNettoEX.Text))
                        r.Add("penimbang", tbPIC1EX.Text)
                    Case "CL", "CRA"
                        r.Add("anyaman", Decimal.Parse("0" + tbAnyamanCL.Text))
                        r.Add("denier", Decimal.Parse("0" + tbDenierCL.Text))
                        r.Add("lebar", Decimal.Parse("0" + tbLebarCL.Text))
                        r.Add("panjang", Decimal.Parse("0" + tbPanjangCL.Text))
                        r.Add("warna", tbWarnaCL.Text)
                        r.Add("mesin", tbMesinCL.Text)
                        r.Add("berat_tungkul", Decimal.Parse("0" + tbTungkul.Text))
                        r.Add("bruto", Decimal.Parse("0" + tbBrutoCL.Text))
                        r.Add("netto", Decimal.Parse("0" + tbNettoCL.Text))
                        r.Add("meter_mesin", Decimal.Parse("0" + tbMeterMesinCL.Text))
                        r.Add("bs_roll", Decimal.Parse("0" + tbBSRollCL.Text))
                        r.Add("pemotong", tbPemotongCL.Text)
                        r.Add("penimbang", tbPIC1CL.Text)
                        r.Add("penerima", tbPIC2CL.Text)

                        'Dim berat As Double = Decimal.Parse("0", +tbNettoCL.Text)
                        'Dim jlh As Double = Decimal.Parse("0", +tbPanjangCL.Text)

                        Dim lebarcl As Decimal = Decimal.Parse("0" + tbLebarCL.Text)
                        Dim anyamancl As Decimal = Decimal.Parse("0" + tbAnyamanCL.Text)
                        Dim deniercl As Decimal = Decimal.Parse("0" + tbDenierCL.Text)

                        Dim stdberat As Double = ((lebarcl * 2) * 100 * (anyamancl + anyamancl) * (deniercl / 1000)) / 2286
                        Dim aktualberat As Double = (tbNettoCL.Text / tbPanjangCL.Text) * 1000
                        Dim selisih As Double = aktualberat - stdberat
                        Dim ptg As Double = selisih / stdberat * 100
                        If ptg > 0 Then
                            r.Add("status", "Status : BERAT" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        ElseIf ptg < -8 Then
                            r.Add("status", "Status : RINGAN" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        Else
                            r.Add("status", "Status : STANDAR" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        End If
                    '    'Added by #Steve

                    Case "RTR"
                        r.Add("anyaman", Decimal.Parse("0" + tbAnyamanRTR.Text))
                        r.Add("denier", Decimal.Parse("0" + tbDenierRTR.Text))
                        r.Add("lebar", Decimal.Parse("0" + tbLebarRTR.Text))
                        r.Add("panjang", Decimal.Parse("0" + tbPanjangRTR.Text))
                        r.Add("warna", tbWarnaRTR.Text)
                        r.Add("cetakan", tbCetakanRTR.Text)
                        r.Add("mesin", tbMesinRTR.Text)
                        r.Add("berat_tungkul", Decimal.Parse("0" + tbTungkulRTR.Text))
                        r.Add("bruto", Decimal.Parse("0" + tbBrutoRTR.Text))
                        r.Add("netto", Decimal.Parse("0" + tbNettoRTR.Text))
                        r.Add("cust_po", tbCustPORTR.Text)
                        r.Add("customer", tbCustomerRTR.Text)
                        r.Add("customer_code", tbCustCodeRTR.Text)
                        r.Add("penimbang", tbPIC1RTR.Text)
                        r.Add("penerima", tbPIC2RTR.Text)
                        r.Add("cetakan_ke", tbCetakKeRTR.Text)
                    Case "CS", "CKA"
                        r.Add("anyaman", Decimal.Parse("0" + tbAnyamanCS.Text))
                        r.Add("denier", Decimal.Parse("0" + tbDenierCS.Text))
                        r.Add("lebar", Decimal.Parse("0" + tbLebarCS.Text))
                        r.Add("panjang", Decimal.Parse("0" + tbPanjangCS.Text))
                        r.Add("jumlah", tbJumlahCS.Text)
                        r.Add("jenis", tbJenisCS.Text)
                        r.Add("mesin", tbMesinCS.Text)
                        r.Add("urutan", tbUrutanCS.Text)
                        r.Add("is_bs", If(cbBSCS.Checked, 1, 0))
                        r.Add("pemotong", tbPIC1CS.Text)
                        r.Add("penerima", tbPIC2CS.Text)
                        r.Add("team", tbTeamCS.Text)
                        r.Add("customer", tbCustomerCS.Text)
                        r.Add("customer_code", tbCustCodeCS.Text)
                    Case "INR"
                        r.Add("anyaman", Decimal.Parse("0" + tbAnyamanII.Text))
                        r.Add("denier", Decimal.Parse("0" + tbDenierII.Text))
                        r.Add("lebar", Decimal.Parse("0" + tbLebarII.Text))
                        r.Add("panjang", Decimal.Parse("0" + tbPanjangII.Text))
                        r.Add("jumlah", tbJumlahII.Text)
                        r.Add("cetakan", tbCetakanII.Text)
                        r.Add("so_num", tbSOII.Text)
                        r.Add("palet", Integer.Parse("0" + tbPaletII.Text))
                        r.Add("operator", tbPIC1II.Text)
                        r.Add("penerima", tbPIC2II.Text)
                        r.Add("team", tbTeamII.Text)
                        r.Add("customer", tbCustomerII.Text)
                        r.Add("customer_code", tbCustCodeII.Text)
                        'Added by #Steve                    
                        r.Add("berat", Decimal.Parse("0" + tbBeratINR.Text))

                        Dim stdinner As Double
                        Dim lebarinr As Decimal = Decimal.Parse("0" + tbLebarII.Text)
                        Dim panjanginr As Decimal = Decimal.Parse("0" + tbPanjangII.Text)
                        Dim denierInr As Decimal = Decimal.Parse("0" + tbDenierII.Text)
                        Dim anyamanInr As Decimal = Decimal.Parse("0" + tbAnyamanII.Text)

                        Dim stdberat As Double = (((lebarinr * 2)) * ((panjanginr + 4)) * (anyamanInr + anyamanInr) * (denierInr / 1000) / 2286)
                        stdinner = Decimal.Parse("0" + tbLebarINR.Text) * (Decimal.Parse("0" + tbPanjangINR.Text) + 1) * Decimal.Parse("0" + tbTebalINR.Text) * 0.2 * 0.91416

                        Dim aktualberat As Double = (Decimal.Parse("0" + tbBeratINR.Text) / Decimal.Parse("0" + tbJumlahII.Text) * 1000)
                        Dim selisih As Double = aktualberat - (stdberat + stdinner)
                        Dim total As Double = stdberat + stdinner
                        Dim ptg As Double = selisih / total * 100

                        If ptg > 1 Then
                            r.Add("status", "Status : BERAT" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        ElseIf ptg < -4 Then
                            r.Add("status", "Status : RINGAN" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        Else
                            r.Add("status", "Status : STANDAR" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        End If

                        r.Add("persen", selisih)

                        If CheckBox3.Checked = False Then
                            r.Add("inner", "-")
                        Else
                            r.Add("inner", tbLebarINR.Text & "x" & tbPanjangINR.Text & "x" & tbTebalINR.Text)
                        End If

                        r.Add("stdinner", stdinner)
                        'End here

                    Case "PRT"
                        r.Add("anyaman", Decimal.Parse("0" + tbAnyamanPRT.Text))
                        r.Add("denier", Decimal.Parse("0" + tbDenierPRT.Text))
                        r.Add("lebar", Decimal.Parse("0" + tbLebarPRT.Text))
                        r.Add("panjang", Decimal.Parse("0" + tbPanjangPRT.Text))
                        r.Add("cetakan", tbCetakanPRT.Text)
                        r.Add("jumlah", Decimal.Parse("0" + tbJumlahPRT.Text))
                        r.Add("bruto", Decimal.Parse("0" + tbBeratPRT.Text))
                        r.Add("lanjut", cbLanjutPRT.Text)
                        r.Add("pekerja", tbPIC1PRT.Text)
                        r.Add("penerima", tbPIC2PRT.Text)
                        r.Add("team", tbTeamPRT.Text)
                        r.Add("no_palet", tbPaletPRT.Text)
                        r.Add("mesin", tbMesinPRT.Text)
                        r.Add("cust_po", tbCustPOPRT.Text)
                        r.Add("customer", tbCustomerPRT.Text)
                        r.Add("customer_code", tbCustCodePRT.Text)
                        r.Add("so_number", tbSOPRT.Text)
                    Case "OPP"
                        r.Add("anyaman", Decimal.Parse("0" + tbAnyamanOPP.Text))
                        r.Add("denier", Decimal.Parse("0" + tbDenierOPP.Text))
                        r.Add("lebar", Decimal.Parse("0" + tbLebarOPP.Text))
                        r.Add("panjang", Decimal.Parse("0" + tbPanjangOPP.Text))
                        r.Add("cetakan", tbCetakanOPP.Text)
                        r.Add("bruto", Decimal.Parse("0" + tbBrutoOPP.Text))
                        r.Add("netto", Decimal.Parse("0" + tbNettoOPP.Text))
                        r.Add("pekerja", tbPIC1OPP.Text)
                        r.Add("penerima", tbPIC2OPP.Text)
                        r.Add("team", tbTeamOPP.Text)
                        r.Add("opp", Decimal.Parse("0" + tbOPP.Text))
                        r.Add("cust_po", tbCustPOOPP.Text)
                        r.Add("customer", tbCustomerOPP.Text)
                        r.Add("customer_code", tbCustCodeOPP.Text)
                        r.Add("so_number", tbSOOPP.Text)
                    Case "PCK", "PCA"
                        r.Add("anyaman", Decimal.Parse("0" + tbAnyamanPCK.Text))
                        r.Add("denier", Decimal.Parse("0" + tbDenierPCK.Text))
                        r.Add("lebar", Decimal.Parse("0" + tbLebarPCK.Text))
                        r.Add("panjang", Decimal.Parse("0" + tbPanjangPCK.Text))
                        r.Add("cetakan", tbCetakanPCK.Text)
                        r.Add("jumlah", Decimal.Parse("0" + tbJumlahPCK.Text) & " Lembar")
                        r.Add("ball", Decimal.Parse("0" + tbBallPCK.Text))
                        'r.Add("berat", Decimal.Parse("0" + tbBeratPCK.Text))

                        Dim berat As Decimal = Decimal.Parse("0" + tbBeratPCK.Text)

                        If ComboPCK.Text = "10 Kg" Then
                            berat = (((Decimal.Parse("0" + tbBeratPCK.Text) * 1000) + 21.89) / 1000)
                        ElseIf ComboPCK.Text = "15 Kg" Then
                            berat = (((Decimal.Parse("0" + tbBeratPCK.Text) * 1000) + 25.6) / 1000)
                        ElseIf ComboPCK.Text = "20 Kg" Then
                            berat = (((Decimal.Parse("0" + tbBeratPCK.Text) * 1000) + 30.4) / 1000)
                        ElseIf ComboPCK.Text = "30 Kg" Then
                            berat = (((Decimal.Parse("0" + tbBeratPCK.Text) * 1000) + 40.08) / 1000)
                        ElseIf ComboPCK.Text = "LAO YING" Then
                            berat = (((Decimal.Parse("0" + tbBeratPCK.Text) * 1000) + 53.31) / 1000)
                        Else
                            berat = (Decimal.Parse("0" + tbBeratPCK.Text))
                        End If

                        'Dim berat As Decimal = Decimal.Parse("0" + tbBeratPCK.Text)
                        Dim jlh As Integer = Decimal.Parse("0" + tbJumlahPCK.Text)
                        berat = FormatNumber((berat - 0.5), 2)
                        Dim beratlbr As Integer = (berat / jlh) * 1000
                        r.Add("berat", berat & " Kg")
                        r.Add("beratlbr", beratlbr & " gram")

                        r.Add("mesin", tbMesinPCK.Text)
                        r.Add("pekerja", tbPIC1PCK.Text)
                        r.Add("penerima", tbPIC2PCK.Text)
                        r.Add("team", tbTeamPCK.Text)
                        r.Add("customer", tbCustomerPCK.Text)
                        r.Add("customer_code", tbCustCodePCK.Text)
                        r.Add("so_number", tbSOPCK.Text)

                        Dim stdinner As Double
                        Dim stdberat As Double = (((Decimal.Parse("0" + tbLebarPCK.Text) * 2)) * ((Decimal.Parse("0" + tbPanjangPCK.Text) + 4)) * (Decimal.Parse("0" + tbAnyamanPCK.Text) + Decimal.Parse("0" + tbAnyamanPCK.Text)) * (Decimal.Parse("0" + tbDenierPCK.Text) / 1000) / 2286)
                        stdinner = Decimal.Parse("0" + tbLebar.Text) * (Decimal.Parse("0" + tbPanjang.Text) + 1) * Decimal.Parse("0" + tbTebal.Text) * 0.2 * 0.91416

                        Dim aktualberat As Double = ((berat / jlh) * 1000)
                        Dim selisih As Double = aktualberat - (stdberat + stdinner)
                        Dim total As Double = stdberat + stdinner
                        Dim ptg As Double = selisih / total * 100
                        If ptg > 1 Then
                            r.Add("status", "Status : BERAT" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        ElseIf ptg < -4 Then
                            r.Add("status", "Status : RINGAN" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        Else
                            r.Add("status", "Status : STANDAR" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        End If

                        'If tbLebar.Text = "" OrElse tbPanjang.Text = "" OrElse tbTebal.Text Then
                        If CheckBox1.Checked = False Then
                            r.Add("inner", "-")
                        Else
                            r.Add("inner", tbLebar.Text & "x" & tbPanjang.Text & "x" & tbTebal.Text)
                        End If

                        'End If

                        'Added by #Steve
                    Case "CIN"
                        r.Add("anyaman", Decimal.Parse("0" + TextBox9.Text))
                        r.Add("panjang", Decimal.Parse("0" + TextBox7.Text))
                        r.Add("lebar", Decimal.Parse("0" + TextBox3.Text))
                        r.Add("jumlah", Decimal.Parse("0" + TextBox4.Text) & "Lbr")
                        r.Add("mesin", TextBox10.Text)
                        r.Add("customer", TextBox6.Text)
                        r.Add("pemotong", TextBox5.Text)
                        'Added by #Steve'
                        Dim berat As Decimal = Decimal.Parse("0" + TextBox12.Text)
                        Dim jlh As Integer = Decimal.Parse("0" + TextBox4.Text)
                        berat = FormatNumber((berat), 2)
                        Dim beratlbr As Integer = (berat / jlh) * 1000
                        r.Add("berat", berat & " Kg")
                        r.Add("beratlbr", beratlbr & " gram")
                        r.Add("jenis", TextBox8.Text)
                        Dim stdberat As Double = Decimal.Parse("0" + TextBox3.Text) * (Decimal.Parse("0" + TextBox7.Text) + 1) * Decimal.Parse("0" + TextBox9.Text) * 0.2 * 0.91416
                        Dim aktualberat As Double = ((berat / Decimal.Parse("0" + TextBox4.Text)) * 1000)
                        Dim selisih As Double = aktualberat - stdberat
                        Dim ptg As Double = selisih / stdberat * 100
                        If ptg > 0 Then
                            r.Add("status", "Status : BERAT" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        ElseIf ptg < -6 Then
                            r.Add("status", "Status : RINGAN" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        Else
                            r.Add("status", "Status : STANDAR" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        End If
                        'end bere                   
                End Select

                repdata.Add(r)

                _parent.dicPrint = repdata

                Select Case line
                    Case "EXT"
                        _parent.reportPrint = LCase(corp) + "-extruder-label.xlsx"
                        clearForm()
                    Case "CL", "CRA"
                        _parent.reportPrint = LCase(corp) + "-loom-label.xlsx"
                        clearForm()
                    Case "RTR"
                        _parent.reportPrint = LCase(corp) + "-roll2roll-label.xlsx"
                        clearForm()
                    Case "CS", "CKA"
                        _parent.reportPrint = LCase(corp) + "-cutting-label.xlsx"
                        clearForm()
                    Case "INR"
                        _parent.reportPrint = LCase(corp) + "-insertinner-label.xlsx"
                        clearForm()
                    Case "JHT"
                        _parent.reportPrint = LCase(corp) + "-jahit-label.xlsx"
                        clearForm()
                    Case "PRT"
                        _parent.reportPrint = LCase(corp) + "-printing-label.xlsx"
                        clearForm()
                    Case "OPP"
                        _parent.reportPrint = LCase(corp) + "-opp-label.xlsx"
                        clearForm()
                    Case "PCK", "PCA"
                        _parent.reportPrint = LCase(corp) + "-packing-label.xlsx"
                        'Added by #Steve
                    Case "CIN"
                        _parent.reportprint = LCase(corp) + "-cuttinginner-label.xlsx"
                        clearForm()
                        'End here

                End Select

                _parent.papersize = papersize
                '_parent.papersize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperEnvelope11
                _parent.doPrint()
            Catch ex As Exception
                MsgBox(ex.Message + vbCrLf + "(SL-PL-01)")
            End Try

            cmd.Dispose()

            'clearForm()
            loadData(line)
        End If
    End Sub

    Private Sub btnPrintCL_Click(sender As Object, e As EventArgs) Handles btnPrintCL.Click
        printLabel("CL")
    End Sub

    Private Sub btnTungkul_Click(sender As Object, e As EventArgs) Handles btnTungkul.Click, tbTungkul.Click
        If lblWeightCL.Text = "-" Then Exit Sub
        tbTungkul.Text = lblWeightCL.Text
    End Sub

    Private Sub btnPrintCS_Click(sender As Object, e As EventArgs) Handles btnPrintCS.Click
        printLabel("CS")
    End Sub

    Private Sub btnPrintII_Click(sender As Object, e As EventArgs) Handles btnPrintII.Click
        If lblWeightII.Text = "-" Then Exit Sub
        tbBeratINR.Text = lblWeightII.Text
        printLabel("INR")
    End Sub

    Private Sub btnPrintPRT_Click(sender As Object, e As EventArgs) Handles btnPrintPRT.Click
        printLabel("PRT")
    End Sub

    Private Sub btnPrintOPP_Click(sender As Object, e As EventArgs) Handles btnPrintOPP.Click
        printLabel("OPP")
    End Sub

    Private Sub btnPrintPCK_Click(sender As Object, e As EventArgs) Handles btnPrintPCK.Click
        If tcPackMode.SelectedIndex = 0 Then
            If ChkOPP.Checked = True And ComboPCK.Text = "" Then
                MsgBox("Data OPP masih kosong, jika memang kosong checkBox OPP jangan dicentang")
                Exit Sub
                If CheckBox1.Checked = True And tbPanjang.Text = "" And tbLebar.Text = "" Then
                    If MessageBox.Show("Apakah ukuran inner memang kosong? jika kosong jangan centang checkBox ukuran inner", "Question", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        printLabel("PCK")
                    Else
                        Exit Sub
                        If MessageBox.Show("Bersihkan Kolom ?", "Clear Data", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            clearForm()
                        End If
                    End If
                    Exit Sub
                End If
            End If

            Dim subString As String = Microsoft.VisualBasic.Left(tbTebal.Text, 3)
            Dim xstring As String = Microsoft.VisualBasic.Right(tbTebal.Text, 2)
            If subString <> "0.0" Then
                tbTebal.Text = "0.0" & xstring.Replace(".", "")
            End If

            printLabel("PCK")
            If MessageBox.Show("Bersihkan Kolom ?", "Clear Data", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                clearForm()
            End If
        End If
    End Sub

    Private Function lineCode(line As String) As String
        Select Case LCase(line)
            Case "extruder"
                Return "EXT"
            Case "circular loom"
                Return "CL"
            Case "cutting sewing"
                Return "CS"
            Case "insert inner"
                Return "INR"
            Case "printing"
                Return "PRT"
            Case "opp"
                Return "OPP"
            Case "packing"
                Return "PCK"
            Case "roll to roll"
                Return "RTR"
            Case "jahit"
                Return "JHT"
            Case "cutting inner"
                Return "CIN"
        End Select
        Return ""
    End Function

    Public Sub rekapData()
        Dim prd As New Periode
        Dim line As String

        prd.rbPeriode.Visible = False
        prd.rb1day.Visible = False
        prd.rb1day.Checked = True
        prd.lblLine.Visible = True
        prd.cbLine.Visible = True
        prd.lblShift.Visible = True
        prd.cbShift.Visible = True

        Dim cmd As SQLiteCommand = localCon.CreateCommand()
        'cmd.CommandText = "select * from shifts"
        Dim adp As SQLiteDataAdapter
        Dim ds As DataSet

        'adp.Fill(ds)

        prd.cbShift.DataSource = cbShifts.DataSource
        prd.cbShift.DisplayMember = "shift_name"
        prd.cbShift.ValueMember = "shift_code"

        'ds.Dispose()
        'adp.Dispose()
        'cmd.Dispose()

        If prd.ShowDialog() = DialogResult.OK Then
            cmd = localCon.CreateCommand()
            'cmd.CommandText = "select p.item_code, item_name, p.anyaman, p.denier, p.lebar, p.panjang, p.warna, p.cetakan, p.opp,
            '                    case when shift = 'Shift 3' then date(date, '-1 day') else date(date) end tanggal, 
            '                    shift, line_code, sum(qty) total 
            '                from production_details p
            '                left join items i on p.item_code = i.item_code and p.company_code = i.company_code
            '                where case when shift = 'Shift 3' then date(date, '-1 day') else date(date) end = ?
            '                group by p.item_code, item_name, p.anyaman, p.denier, p.lebar, p.panjang, p.warna, p.cetakan, p.opp,
            '                    case when shift = 'Shift 3' then date(date, '-1 day') else date(date) end,
            '                    shift, line_code"
            cmd.CommandText = "select p.pic2, p.item_code, i.item_name, p.anyaman, p.denier, p.lebar, p.panjang, p.warna, p.cetakan, p.opp, p.doc_number,
                                case when p.shift = 'Shift 3' then date(replace(p.`date`, '.', ':'), '-1 day') else date(replace(p.`date`, '.', ':')) end tanggal, 
                                p.shift, p.line_code, p.qty total, p.data, p.pic1, p.pic1code, p.date
                            from production_details p
                            left join items i on p.item_code = i.item_code and p.company_code = i.company_code
                            where void = 0 and case when shift = 'Shift 3' then date(replace(`date`, '.', ':'), '-1 day') else date(replace(`date`, '.', ':')) end = ? and void = 0 and line_code = ? 
                            and shift = ?"

            line = lineCode(prd.cbLine.Text)

            cmd.Parameters.AddWithValue("date", prd.dpAwal.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("line_code", line)
            cmd.Parameters.AddWithValue("shift", prd.cbShift.Text)
            adp = New SQLiteDataAdapter(cmd)
            ds = New DataSet

            adp.Fill(ds)

            Dim d As New List(Of Dictionary(Of String, Object))
            Dim r As Dictionary(Of String, Object)

            For Each t As DataTable In ds.Tables
                For Each row As DataRow In t.Rows
                    r = New Dictionary(Of String, Object)
                    'r.Add("company_code", row.Item("company_code"))
                    'r.Add("area_code", row.Item("area_code"))
                    'r.Add("station_code", row.Item("station_code"))
                    r.Add("doc_number", row.Item("doc_number"))
                    r.Add("tanggal", row.Item("tanggal"))
                    r.Add("shift", row.Item("shift"))
                    r.Add("item_code", row.Item("item_code"))
                    r.Add("item_name", row.Item("item_name"))
                    r.Add("line_code", row.Item("line_code"))
                    r.Add("total", row.Item("total"))
                    'r.Add("creator", row.Item("creator"))
                    r.Add("operator", row.Item("pic1"))
                    r.Add("operator_code", row.Item("pic1code"))
                    r.Add("pic2", row.Item("pic2"))
                    'r.Add("pic2code", row.Item("pic2code"))
                    r.Add("anyaman", row.Item("anyaman"))
                    r.Add("denier", row.Item("denier"))
                    r.Add("lebar", row.Item("lebar"))
                    r.Add("panjang", row.Item("panjang"))
                    r.Add("cetakan", row.Item("cetakan"))
                    r.Add("warna", row.Item("warna"))
                    r.Add("opp", row.Item("opp"))


                    Dim jsondata As Dictionary(Of String, Object)
                    jsondata = json.Deserialize(Of Dictionary(Of String, Object))(row.Item("data"))
                    Dim netto As Decimal

                    'Added by #Steve
                    If jsondata.ContainsKey("netto") Then
                        r.Add("netto", jsondata("netto"))
                    Else
                        r.Add("netto", "0")
                    End If
                    'end here

                    If jsondata.ContainsKey("berat") Then
                        r.Add("berat", jsondata("berat"))
                        netto = jsondata("berat")
                    Else
                        r.Add("berat", "0")
                        netto = 0
                    End If

                    If jsondata.ContainsKey("customer") Then
                        r.Add("customer", jsondata("customer"))
                    Else
                        r.Add("customer", "")
                    End If
                    Select Case line
                        Case "EXT"
                            r.Add("jml_bobbin", jsondata("jml_bobbin"))
                            r.Add("panen", jsondata("panen"))
                            If jsondata.ContainsKey("bobbin") Then
                                r.Add("bobbin", jsondata("bobbin"))
                            Else
                                r.Add("bobbin", "")
                            End If

                        Case "CL"
                            'r.Add("netto", jsondata("netto"))
                            r("totals") = jsondata("panjang")
                    End Select

                    Dim j As DateTime = row.Item("date")
                    r.Add("jam", j.ToString("HH:mm:ss"))

                    If jsondata.ContainsKey("mesin") Then
                        r.Add("mesin", jsondata("mesin"))
                    Else
                        r.Add("mesin", "")
                    End If

                    'Added By #Steve
                    Select Case line
                        Case "PCK"
                            Dim stdinner As Double

                            Dim stdberat As Double = (((row.Item("lebar") * 2)) * ((row.Item("panjang") + 4)) * (row.Item("anyaman") + row.Item("anyaman")) * (row.Item("denier") / 1000) / 2286)
                            If jsondata.ContainsKey("lebarinner") And jsondata.ContainsKey("panjanginner") And jsondata.ContainsKey("tebalinner") Then
                                stdinner = jsondata("lebarinner") * (jsondata("panjanginner") + 1) * jsondata("tebalinner") * 0.2 * 0.91416
                            Else
                                stdinner = 0
                            End If
                            'Dim aktualberat As Double = (((netto - 1 / 2) / row.Item("total")) * 1000)
                            Dim aktualberat As Double = (((netto) / row.Item("total")) * 1000)
                            Dim selisih As Double = aktualberat - stdberat
                            Dim ptg As Double = selisih / stdberat * 100
                            If ptg > 1 Then
                                r.Add("stats", "Berat")
                            ElseIf ptg < -4 Then
                                r.Add("stats", "Ringan")
                            Else
                                r.Add("stats", "Standar")
                            End If
                            r.Add("stdberat", Math.Round((stdberat), 2))
                            r.Add("aktualberat", aktualberat)
                            r.Add("selisih", selisih)
                            r.Add("persenselisih", ptg & " %")
                            r.Add("ukurankarung", row.Item("lebar") & " x " & row.Item("panjang") & " " & row.Item("anyaman") & "/" & row.Item("denier"))
                            r.Add("stdinner", stdinner)
                            If jsondata.ContainsKey("lebarinner") And jsondata.ContainsKey("panjanginner") And jsondata.ContainsKey("tebalinner") Then
                                r.Add("inner", jsondata("lebarinner") & " x " & jsondata("panjanginner") & " x " & jsondata("tebalinner"))
                            Else
                                r.Add("inner", 0)
                            End If
                        Case "CIN"
                            r.Add("ukurankarung", row.Item("lebar") & " x " & row.Item("panjang") & " x " & row.Item("anyaman"))
                            If jsondata.ContainsKey("jenis") Then
                                r.Add("jenis", jsondata("jenis"))
                            Else
                                r.Add("jenis", "--")
                            End If

                            If jsondata.ContainsKey("pemotong") Then
                                r.Add("potong", jsondata("pemotong"))
                            Else
                                r.Add("potong", "--")
                            End If

                            Dim stdberat As Double = row.Item("lebar") * (row.Item("panjang") + 1) * row.Item("anyaman") * 0.2 * 0.91416
                            Dim aktualberat As Double = ((netto / row.Item("total")) * 1000)
                            Dim selisihberat As Double = aktualberat - stdberat
                            Dim ptg As Double = selisihberat / stdberat * 100
                            If ptg > 0 Then
                                r.Add("stats", "Berat")
                            ElseIf ptg < -6 Then
                                r.Add("stats", "Ringan")
                            Else
                                r.Add("stats", "Standar")
                            End If

                            r.Add("stdberat", Math.Round((stdberat), 2))
                            r.Add("aktualberat", aktualberat)
                            r.Add("selisih", selisihberat)
                            r.Add("persenselisih", ptg & " %")

                            'Newly Added
                            'r.Add("lebar", row.Item("lebar"))
                            'r.Add("panjang", row.Item("panjang"))
                            'r.Add("anyaman", row.Item("anyaman"))


                        Case "INR"
                            Dim stdinner As Double
                            Dim stdberat As Double = (((row.Item("lebar") * 2)) * ((row.Item("panjang") + 4)) * (row.Item("anyaman") + row.Item("anyaman")) * (row.Item("denier") / 1000) / 2286)
                            If jsondata.ContainsKey("lebarinner") And jsondata.ContainsKey("panjanginner") And jsondata.ContainsKey("tebalinner") Then
                                stdinner = jsondata("lebarinner") * (jsondata("panjanginner") + 1) * jsondata("tebalinner") * 0.2 * 0.91416
                            Else
                                stdinner = 0
                            End If
                            Dim aktualberat As Double
                            If jsondata.ContainsKey("berat") Then
                                aktualberat = (jsondata("berat") / row.Item("total")) * 1000
                            Else
                                aktualberat = 0
                            End If
                            'Dim aktualberat As Double = ((netto / row.Item("total")) * 1000)
                            Dim selisih As Double = aktualberat - stdberat
                            Dim ptg As Double = selisih / stdberat * 100
                            If ptg > 1 Then
                                r.Add("stats", "Berat")
                            ElseIf ptg < -4 Then
                                r.Add("stats", "Ringan")
                            Else
                                r.Add("stats", "Standar")
                            End If
                            r.Add("stdberat", Math.Round((stdberat), 2))
                            r.Add("aktualberat", aktualberat)
                            r.Add("selisih", selisih)
                            r.Add("persenselisih", ptg & " %")
                            r.Add("ukurankarung", row.Item("lebar") & " x " & row.Item("panjang") & " " & row.Item("anyaman") & "/" & row.Item("denier"))
                            r.Add("stdinner", stdinner)
                            If jsondata.ContainsKey("lebarinner") And jsondata.ContainsKey("panjanginner") And jsondata.ContainsKey("tebalinner") Then
                                r.Add("inner", jsondata("lebarinner") & " x " & jsondata("panjanginner") & " x " & jsondata("tebalinner"))
                            Else
                                r.Add("inner", 0)
                            End If

                        Case "RTR"
                            If jsondata.ContainsKey("netto") Then
                                r.Add("brtnetto", jsondata("netto"))
                            Else
                                r.Add("brtnetto", "0")
                            End If
                    End Select
                    'End here 

                    d.Add(r)
                Next
            Next

            If d.Count = 0 Then
                MsgBox("Tidak ada data yang bisa di rekap")
                prd.Dispose()
                Exit Sub
            End If

            Dim item As ListViewItem
            Dim rkp As New Rekap_Data

            '_parent.dsPrint = ds
            _parent.dicPrint = d
            _parent.papersize = Nothing
            Select Case line
                Case "EXT", "CL", "CS", "OPP", "PRT", "RTR", "JHT", "CIN", "PCK", "INR"
                    If File.Exists("reports\" + LCase(corp) + "-production-" & LCase(line) & "-summary.xlsx") Then
                        _parent.reportPrint = LCase(corp) + "-production-" & LCase(line) & "-summary.xlsx"
                    Else
                        _parent.reportPrint = LCase(corp) + "-production-summary.xlsx"
                    End If
                    'Case "PCK", "INR"
                    '    For Each a In d
                    '        item = rkp.ListView1.Items.Add(a.Item("doc_number"))
                    '        item.SubItems.Add(a.Item("operator"))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("customer")), "", a.Item("customer")))
                    '        item.SubItems.Add(a.Item("cetakan"))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("ukurankarung")), "", a.Item("ukurankarung")))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("inner")), "", a.Item("inner")))
                    '        item.SubItems.Add(a.Item("total"))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("berat")), "", a.Item("berat")))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("stdberat")), "", a.Item("stdberat")))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("stdinner")), "", a.Item("stdinner")))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("aktualberat")), "", a.Item("aktualberat")))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("selisih")), "", a.Item("selisih")))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("persenselisih")), "", a.Item("persenselisih")))
                    '        item.SubItems.Add(If(IsDBNull(a.Item("stats")), "", a.Item("stats")))
                    '    Next
                    '    rkp.Show()
                Case Else
                    _parent.reportPrint = LCase(corp) + "-production-summary.xlsx"
            End Select
            _parent.doSaveReport()
        End If
        prd.Dispose()
    End Sub

    Private Sub btnBobbin_Click(sender As Object, e As EventArgs) Handles btnBobbin.Click
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        cmd.CommandText = "SELECT * FROM bobbins WHERE company_code = ?"
        cmd.Parameters.AddWithValue("company_code", corp)
        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        lstFind.Items.Clear()
        lstFind.Columns(0).Text = "Bobbin"
        lstFind.Columns(1).Text = "Bobbin Code"
        lstFind.Columns(2).Text = "Weight"

        If ds.Tables.Count = 1 Then
            For Each row In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item("bobbin"))
                item.SubItems.Add(row.Item("bobbin_code"))
                item.SubItems.Add(row.Item("weight"))
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        lstFind.Left = tbBobbinType.Left + tcLine.Left + gbLabel.Left + tcLine.Margin.Left
        lstFind.Top = tbBobbinType.Top + tcLine.Top + tcLine.Height - tcLine.TabPages(0).Height - tcLine.Margin.Bottom + gbLabel.Top + tbBobbinType.Height

        lstFind.Visible = True

        textFocus = tbBobbinType
        textCode = tbBobbinWeight
        dataCol = 2
        textFocus.Focus()
    End Sub

    Private Sub btnDenier_Click(sender As Object, e As EventArgs) Handles btnDenierEX.Click, btnDenierCL.Click, btnDenierCS.Click,
            btnDenierII.Click, btnDenierPRT.Click, btnDenierOPP.Click, btnDenierPCK.Click, btnDenierRTR.Click, btnDenierJHT.Click
        Dim tb As TextBox = Nothing
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        cmd.CommandText = "SELECT DISTINCT denier FROM items WHERE company_code = ? AND line like ? AND denier IS NOT NULL ORDER BY denier"
        cmd.Parameters.AddWithValue("company_code", corp)
        Select Case DirectCast(sender, Button).Name
            Case "btnDenierEX"
                cmd.Parameters.AddWithValue("line", "%EXT%")
            Case "btnDenierCL", "btnDenierRTR"
                cmd.Parameters.AddWithValue("line", "%CL%")
            Case "btnDenierCS"
                cmd.Parameters.AddWithValue("line", "%CS%")
            Case "btnDenierPRT"
                cmd.Parameters.AddWithValue("line", "%PRT%")
            Case "btnDenierOPP"
                cmd.Parameters.AddWithValue("line", "%OPP%")
            Case "btnDenierPCK"
                cmd.Parameters.AddWithValue("line", "%PCK%")
            Case Else
                cmd.Parameters.AddWithValue("line", "%CL%")
        End Select

        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        lstFind.Items.Clear()
        lstFind.Columns(0).Text = "Denier"
        lstFind.Columns(1).Text = ""
        lstFind.Columns(2).Text = ""

        If ds.Tables.Count = 1 Then
            For Each row In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item("denier"))
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        Select Case DirectCast(sender, Button).Name
            Case "btnDenierEX"
                tb = tbDenierEX
            Case "btnDenierCL"
                tb = tbDenierCL
            Case "btnDenierRTR"
                tb = tbDenierRTR
            Case "btnDenierCS"
                tb = tbDenierCS
            Case "btnDenierII"
                tb = tbDenierII
            Case "btnDenierJHT"
                tb = tbDenierJHT
            Case "btnDenierPRT"
                tb = tbDenierPRT
            Case "btnDenierOPP"
                tb = tbDenierOPP
            Case "btnDenierPCK"
                tb = tbDenierPCK
        End Select

        If tb IsNot Nothing Then
            lstFind.Left = tb.Left + tcLine.Left + gbLabel.Left + tcLine.Margin.Left
            lstFind.Top = tb.Top + tcLine.Top + tcLine.Height - tcLine.TabPages(0).Height - tcLine.Margin.Bottom + gbLabel.Top + tb.Height

            lstFind.Visible = True

            textFocus = tb
            textCode = Nothing
            textCol = 0
            textFocus.Focus()
        End If
    End Sub

    Private Sub btnAnyaman_Click(sender As Object, e As EventArgs) Handles btnAnyamanCL.Click, btnAnyamanCS.Click,
            btnAnyamanII.Click, btnAnyamanPRT.Click, btnAnyamanOPP.Click, btnAnyamanPCK.Click, btnAnyamanRTR.Click, btnAnyamanJHT.Click, Button5.Click
        Dim tb As TextBox = Nothing
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        cmd.CommandText = "SELECT DISTINCT anyaman FROM items WHERE company_code = ? AND line like ? AND anyaman IS NOT NULL AND anyaman <> 0 ORDER BY anyaman"
        cmd.Parameters.AddWithValue("company_code", corp)
        Select Case DirectCast(sender, Button).Name
            Case "btnAnyamanCL", "btnAnyamanRTR"
                cmd.Parameters.AddWithValue("line", "%CL%")
            Case "btnAnyamanCS"
                cmd.Parameters.AddWithValue("line", "%CS%")
            Case "btnAnyamanPRT"
                cmd.Parameters.AddWithValue("line", "%PRT%")
            Case "btnAnyamanOPP"
                cmd.Parameters.AddWithValue("line", "%OPP%")
            Case "btnAnyamanPCK"
                cmd.Parameters.AddWithValue("line", "%PCK%")
                'Added by #Steve
                'Case "Button5"
                '    cmd.Parameters.AddWithValue("line", "%CIN%")
            Case Else
                cmd.Parameters.AddWithValue("line", "%CL%")
        End Select

        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        lstFind.Items.Clear()
        lstFind.Columns(0).Text = "Anyaman"
        lstFind.Columns(1).Text = ""
        lstFind.Columns(2).Text = ""

        If ds.Tables.Count = 1 Then
            For Each row In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item("Anyaman"))
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        Select Case DirectCast(sender, Button).Name
            Case "btnAnyamanCL"
                tb = tbAnyamanCL
            Case "btnAnyamanRTR"
                tb = tbAnyamanRTR
            Case "btnAnyamanCS"
                tb = tbAnyamanCS
            Case "btnAnyamanII"
                tb = tbAnyamanII
            Case "btnAnyamanJHT"
                tb = tbAnyamanJHT
            Case "btnAnyamanPRT"
                tb = tbAnyamanPRT
            Case "btnAnyamanOPP"
                tb = tbAnyamanOPP
            Case "btnAnyamanPCK"
                tb = tbAnyamanPCK
                'Added by #Steve
            Case "Button5"
                tb = TextBox9
                'End here
        End Select

        If tb IsNot Nothing Then
            lstFind.Left = tb.Left + tcLine.Left + gbLabel.Left + tcLine.Margin.Left
            lstFind.Top = tb.Top + tcLine.Top + tcLine.Height - tcLine.TabPages(0).Height - tcLine.Margin.Bottom + gbLabel.Top + tb.Height

            lstFind.Visible = True

            textFocus = tb
            textCode = Nothing
            textCol = 0
            textFocus.Focus()
        End If
    End Sub

    Private Sub btnLebar_Click(sender As Object, e As EventArgs) Handles btnLebarCL.Click, btnLebarCS.Click,
            btnLebarII.Click, btnLebarPRT.Click, btnLebarOPP.Click, btnLebarPCK.Click, btnLebarRTR.Click, btnLebarJHT.Click, Button1.Click
        Dim tb As TextBox = Nothing
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        cmd.CommandText = "SELECT DISTINCT lebar FROM items WHERE company_code = ? AND line like ? AND lebar IS NOT NULL AND Lebar <> 0 ORDER BY lebar"
        cmd.Parameters.AddWithValue("company_code", corp)
        Select Case DirectCast(sender, Button).Name
            Case "btnLebarCL", "btnLebarRTR"
                cmd.Parameters.AddWithValue("line", "%CL%")
            Case "btnLebarCS"
                cmd.Parameters.AddWithValue("line", "%CS%")
            Case "btnLebarPRT"
                cmd.Parameters.AddWithValue("line", "%PRT%")
            Case "btnLebarOPP"
                cmd.Parameters.AddWithValue("line", "%OPP%")
            Case "btnLebarPCK"
                cmd.Parameters.AddWithValue("line", "%PCK%")
                'Added by#Steve
                'Case "Button1"
                '    cmd.Parameters.AddWithValue("line", "%CIN%")
            Case Else
                cmd.Parameters.AddWithValue("line", "%CL%")
        End Select

        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        lstFind.Items.Clear()
        lstFind.Columns(0).Text = "Lebar"
        lstFind.Columns(1).Text = ""
        lstFind.Columns(2).Text = ""

        If ds.Tables.Count = 1 Then
            For Each row In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item("Lebar"))
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        Select Case DirectCast(sender, Button).Name
            Case "btnLebarCL"
                tb = tbLebarCL
            Case "btnLebarRTR"
                tb = tbLebarRTR
            Case "btnLebarCS"
                tb = tbLebarCS
            Case "btnLebarII"
                tb = tbLebarII
            Case "btnLebarJHT"
                tb = tbLebarJHT
            Case "btnLebarPRT"
                tb = tbLebarPRT
            Case "btnLebarOPP"
                tb = tbLebarOPP
            Case "btnLebarPCK"
                tb = tbLebarPCK
                'Added by Steve
            Case "Button1"
                tb = TextBox3
        End Select

        If tb IsNot Nothing Then
            lstFind.Left = tb.Left + tcLine.Left + gbLabel.Left + tcLine.Margin.Left
            lstFind.Top = tb.Top + tcLine.Top + tcLine.Height - tcLine.TabPages(0).Height - tcLine.Margin.Bottom + gbLabel.Top + tb.Height

            lstFind.Visible = True

            textFocus = tb
            textCode = Nothing
            textCol = 0
            textFocus.Focus()
        End If
    End Sub

    Private Sub btnPanjang_Click(sender As Object, e As EventArgs) Handles btnPanjangCS.Click,
            btnPanjangII.Click, btnPanjangPRT.Click, btnPanjangOPP.Click, btnPanjangPCK.Click, btnPanjangJHT.Click, Button4.Click
        Dim tb As TextBox = Nothing
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        cmd.CommandText = "SELECT DISTINCT panjang FROM items WHERE company_code = ? AND line like ? AND Panjang IS NOT NULL AND panjang <> 0 ORDER BY panjang"
        cmd.Parameters.AddWithValue("company_code", corp)
        Select Case DirectCast(sender, Button).Name
            Case "btnPanjangCS"
                cmd.Parameters.AddWithValue("line", "%CS%")
            Case "btnPanjangPRT"
                cmd.Parameters.AddWithValue("line", "%PRT%")
            Case "btnPanjangOPP"
                cmd.Parameters.AddWithValue("line", "%OPP%")
            Case "btnPanjangPCK"
                cmd.Parameters.AddWithValue("line", "%PCK%")
                'Added by #Steve
                'Case "Button4"
                '    cmd.Parameters.AddWithValue("line", "%CIN%")
            Case Else
                cmd.Parameters.AddWithValue("line", "%CL%")
        End Select

        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        lstFind.Items.Clear()
        lstFind.Columns(0).Text = "Panjang"
        lstFind.Columns(1).Text = ""
        lstFind.Columns(2).Text = ""

        If ds.Tables.Count = 1 Then
            For Each row In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item("Panjang"))
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        Select Case DirectCast(sender, Button).Name
            Case "btnPanjangCL"
                tb = tbPanjangCL
            Case "btnPanjangRTR"
                tb = tbPanjangRTR
            Case "btnPanjangCS"
                tb = tbPanjangCS
            Case "btnPanjangII"
                tb = tbPanjangII
            Case "btnPanjangJHT"
                tb = tbPanjangJHT
            Case "btnPanjangPRT"
                tb = tbPanjangPRT
            Case "btnPanjangOPP"
                tb = tbPanjangOPP
            Case "btnPanjangPCK"
                tb = tbPanjangPCK
            Case "Button4"
                tb = TextBox7
        End Select

        If tb IsNot Nothing Then
            lstFind.Left = tb.Left + tcLine.Left + gbLabel.Left + tcLine.Margin.Left
            lstFind.Top = tb.Top + tcLine.Top + tcLine.Height - tcLine.TabPages(0).Height - tcLine.Margin.Bottom + gbLabel.Top + tb.Height

            lstFind.Visible = True

            textFocus = tb
            textCode = Nothing
            textCol = 0
            textFocus.Focus()
        End If
    End Sub

    Private Sub btnPIC_Click(sender As Object, e As EventArgs) Handles btnPIC1EX.Click, btnPIC2EX.Click, btnPIC1CL.Click, btnPIC2CL.Click,
            btnPIC1CS.Click, btnPIC2CS.Click, btnPIC1II.Click, btnPIC2II.Click, btnPIC1PRT.Click, btnPIC2PRT.Click, btnPIC1OPP.Click, btnPIC2OPP.Click,
            btnPIC1PCK.Click, btnPIC2PCK.Click, btnPemotongCL.Click, btnPIC1RTR.Click, btnPIC2RTR.Click, btnPIC1JHT.Click, btnPIC2JHT.Click, Button2.Click
        Dim tb As TextBox = Nothing
        Dim tc As TextBox = Nothing
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        cmd.CommandText = "SELECT DISTINCT employee_code, full_name FROM employees WHERE company_code = ?"
        cmd.Parameters.AddWithValue("company_code", corp)

        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        filterData.Clear()

        lstFind.Items.Clear()
        lstFind.Columns(0).Text = "Employee Code"
        lstFind.Columns(1).Text = "Employee Name"
        lstFind.Columns(2).Text = ""

        If ds.Tables.Count = 1 Then
            For Each row In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item("employee_code"))
                item.SubItems.Add(row.Item("full_name"))
                filterData.Add(item)
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        Select Case DirectCast(sender, Button).Name
            Case "btnPIC1EX"
                tc = tbPIC1CodeEX
                tb = tbPIC1EX
            Case "btnPIC2EX"
                tc = tbPIC2CodeEX
                tb = tbPIC2EX
            Case "btnPIC1CL"
                tc = tbPIC1CodeCL
                tb = tbPIC1CL
            Case "btnPIC2CL"
                tc = tbPIC2CodeCL
                tb = tbPIC2CL
            Case "btnPIC1RTR"
                tc = tbPIC1CodeRTR
                tb = tbPIC1RTR
            Case "btnPIC2RTR"
                tc = tbPIC2CodeRTR
                tb = tbPIC2RTR
            Case "btnPIC1CS"
                tc = tbPIC1CodeCS
                tb = tbPIC1CS
            Case "btnPIC2CS"
                tc = tbPIC2CodeCS
                tb = tbPIC2CS
            Case "btnPIC1II"
                tc = tbPIC1CodeII
                tb = tbPIC1II
            Case "btnPIC2II"
                tc = tbPIC2CodeII
                tb = tbPIC2II
            Case "btnPIC1JHT"
                tc = tbPIC1CodeJHT
                tb = tbPIC1JHT
            Case "btnPIC2JHT"
                tc = tbPIC2CodeJHT
                tb = tbPIC2JHT
            Case "btnPIC1PRT"
                tc = tbPIC1CodePRT
                tb = tbPIC1PRT
            Case "btnPIC2PRT"
                tc = tbPIC2CodePRT
                tb = tbPIC2PRT
            Case "btnPIC1OPP"
                tc = tbPIC1CodeOPP
                tb = tbPIC1OPP
            Case "btnPIC2OPP"
                tc = tbPIC2CodeOPP
                tb = tbPIC2OPP
            Case "btnPIC1PCK"
                tc = tbPIC1CodePCK
                tb = tbPIC1PCK
            Case "btnPIC2PCK"
                tc = tbPIC2CodePCK
                tb = tbPIC2PCK
            Case "btnPemotongCL"
                tc = Nothing
                tb = tbPemotongCL
            Case "Button2"
                tc = TextBox13
                tb = TextBox5
        End Select

        If tb IsNot Nothing Then
            lstFind.Left = tb.Left + tcLine.Left + gbLabel.Left + tcLine.Margin.Left
            lstFind.Top = tb.Top + tcLine.Top + tcLine.Height - tcLine.TabPages(0).Height - tcLine.Margin.Bottom + gbLabel.Top + tb.Height

            lstFind.Visible = True
            textFocus = tb
            textCode = tc
            dataCol = 0
            textCol = 1
            textFocus.Focus()
        End If
    End Sub

    Private Sub tcLine_IndexChanged(sender As Object, e As EventArgs) Handles tcLine.SelectedIndexChanged
        If Not doEvents Then Exit Sub
        Select Case tcLine.SelectedIndex
            Case 0
                loadData("EXT", searchFrom, searchTo)
            Case 1
                loadData("CL", searchFrom, searchTo)
            Case 2
                loadData("CS", searchFrom, searchTo)
            Case 3
                loadData("INR", searchFrom, searchTo)
            Case 4
                loadData("PRT", searchFrom, searchTo)
            Case 5
                loadData("OPP", searchFrom, searchTo)
            Case 6
                loadData("PCK", searchFrom, searchTo)
            Case 7
                loadData("RTR", searchFrom, searchTo)
            Case 8
                loadData("JHT", searchFrom, searchTo)

                'Added by #Steve
            Case 9
                loadData("CIN", searchFrom, searchTo)
                'End here            
        End Select
    End Sub

    Private Sub btnTanggal_Click(sender As Object, e As EventArgs) Handles btnTanggal.Click
        Dim prd As New Periode
        Dim line As String

        Select Case tcLine.SelectedIndex
            Case 1
                line = "CL"
            Case 2
                line = "CS"
            Case 3
                line = "INR"
            Case 4
                line = "PRT"
            Case 5
                line = "OPP"
            Case 6
                line = "PCK"
            Case 7
                line = "RTR"
            Case 8
                line = "JHT"
                'Added by #Steve
            Case 9
                line = "CIN"
                'end here

            Case Else
                line = "EXT"

        End Select

        prd.dpAwal.Value = searchFrom
        prd.dpAkhir.Value = searchTo

        If prd.ShowDialog() = DialogResult.OK Then
            If prd.rb1day.Checked Then
                loadData(line, prd.dpAwal.Value)
                searchFrom = prd.dpAwal.Value
                searchTo = prd.dpAwal.Value
            Else
                loadData(line, prd.dpAwal.Value, prd.dpAkhir.Value)
                searchFrom = prd.dpAwal.Value
                searchTo = prd.dpAkhir.Value
            End If
        End If

        prd.Dispose()
    End Sub

    Private Sub lstFind_DoubleClick(sender As Object, e As EventArgs) Handles lstFind.DoubleClick
        If lstFind.SelectedItems.Count = 1 And (textFocus IsNot Nothing Or textCode IsNot Nothing) Then
            Dim item As ListViewItem = lstFind.SelectedItems(0)
            If textCode IsNot Nothing Then
                textFocus.Text = item.SubItems(textCol).Text
                textCode.Text = item.SubItems(dataCol).Text
            ElseIf textCol > 0 Then
                textFocus.Text = item.SubItems(textCol).Text
            Else
                textFocus.Text = item.Text
            End If
            textFocus.Focus()
            lstFind.Visible = False
        End If
    End Sub

    'Private Sub tbTungkul_TextChanged(sender As Object, e As EventArgs) Handles tbTungkul.TextChanged, tbBrutoCL.TextChanged
    '    Dim v As Decimal

    '    v = Decimal.Parse("0" + tbBrutoCL.Text) - Decimal.Parse("0" + tbTungkul.Text)

    '    tbNettoCL.Text = String.Format(numformat, v)
    'End Sub

    Private Sub btnBerat_Click(sender As Object, e As EventArgs) Handles btnBerat.Click
        If lblWeightPCK.Text = "-" Then Exit Sub
        tbBeratPCK.Text = lblWeightPCK.Text
    End Sub

    Private Sub btnBrutoOPP_Click(sender As Object, e As EventArgs) Handles btnBrutoOPP.Click, tbBrutoOPP.Click
        If lblWeightOPP.Text = "-" Then Exit Sub
        tbBrutoOPP.Text = lblWeightOPP.Text
    End Sub

    Private Sub btnNettoOPP_Click(sender As Object, e As EventArgs) Handles btnNettoOPP.Click, tbNettoOPP.Click
        'If lblWeightOPP.Text = "-" Then Exit Sub
        'tbNettoOPP.Text = lblWeightOPP.Text
    End Sub

    Private Sub btnBrutoCL_Click(sender As Object, e As EventArgs) Handles btnBrutoCL.Click, tbBrutoCL.Click
        If lblWeightCL.Text = "-" Then Exit Sub
        tbBrutoCL.Text = lblWeightCL.Text
    End Sub

    Private Sub btnNettoCL_Click(sender As Object, e As EventArgs)
        If lblWeightCL.Text = "-" Then Exit Sub
        tbNettoCL.Text = lblWeightCL.Text
    End Sub

    Private Sub tmReprint_Click(sender As Object, e As EventArgs) Handles tmReprint.Click
        On Error Resume Next

        If lstData.SelectedItems.Count <> 1 Then
            MsgBox("Pilih dulu dokumen yang akan di print ulang")
            Exit Sub
        End If

        Dim item As ListViewItem = lstData.SelectedItems(0)
        Dim cmd As SQLiteCommand = localCon.CreateCommand()
        cmd.CommandText = "SELECT * FROM production_details WHERE doc_number = ? AND company_code = ?"
        cmd.Parameters.AddWithValue("doc_number", item.Text)
        cmd.Parameters.AddWithValue("company_code", corp)
        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        adp.Fill(ds)

        Dim line As String = Strings.Left(item.Text, item.Text.IndexOf("-"))

        Dim repdata As New List(Of Dictionary(Of String, Object))
        Dim r As Dictionary(Of String, Object)
        Dim jsondata As Dictionary(Of String, Object)

        For Each t As DataTable In ds.Tables
            For Each dr As DataRow In t.Rows
                r = New Dictionary(Of String, Object)
                jsondata = json.Deserialize(Of Dictionary(Of String, Object))(dr.Item("data"))
                r.Add("doc_number", item.Text)
                r.Add("tanggal", dr.Item("date"))
                r.Add("shift", cbShifts.Text)

                Select Case line
                    Case "EXT"
                        r.Add("denier", dr.Item("denier"))
                        r.Add("warna", dr.Item("warna"))
                        r.Add("panen", jsondata("panen"))
                        r.Add("jml_bobbin", jsondata("jml_bobbin"))
                        r.Add("bobbin", If(jsondata.ContainsKey("bobbin"), jsondata("bobbin"), ""))
                        r.Add("berat_bobbin", If(jsondata.ContainsKey("berat_bobbin"), jsondata("berat_bobbin"), ""))
                        r.Add("mesin", jsondata("mesin"))
                        r.Add("berat_troli", jsondata("berat_troli"))
                        r.Add("bruto", jsondata("bruto"))
                        r.Add("netto", jsondata("netto"))
                        r.Add("penimbang", dr.Item("pic1"))
                    Case "CL", "CRA"
                        r.Add("anyaman", dr.Item("anyaman"))
                        r.Add("denier", dr.Item("denier"))
                        r.Add("lebar", dr.Item("lebar"))
                        r.Add("panjang", dr.Item("panjang"))
                        r.Add("warna", dr.Item("warna"))
                        r.Add("mesin", jsondata("mesin"))
                        r.Add("berat_tungkul", jsondata("berat_tungkul"))
                        r.Add("bruto", jsondata("bruto"))
                        r.Add("netto", jsondata("netto"))
                        r.Add("meter_mesin", If(jsondata.ContainsKey("meter_mesin"), jsondata("meter_mesin"), 0))
                        r.Add("bs_roll", If(jsondata.ContainsKey("bs_roll"), jsondata("bs_roll"), 0))
                        r.Add("pemotong", jsondata("pemotong"))
                        r.Add("penimbang", dr.Item("pic1"))
                        r.Add("penerima", dr.Item("pic2"))

                        Dim berat As Double = jsondata("netto")
                        Dim jlh As Double = jsondata("panjang")

                        Dim stdberat As Double = ((dr.Item("lebar") * 2) * 100 * (dr.Item("anyaman") + dr.Item("anyaman")) * (dr.Item("denier") / 1000)) / 2286
                        Dim aktualberat As Double = (berat / jlh) * 1000
                        Dim selisih As Double = aktualberat - stdberat
                        Dim ptg As Double = selisih / stdberat * 100
                        If ptg > 0 Then
                            r.Add("status", "Status : BERAT" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        ElseIf ptg < -8 Then
                            r.Add("status", "Status : RINGAN" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        Else
                            r.Add("status", "Status : STANDAR" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        End If

                    Case "RTR"
                        r.Add("anyaman", dr.Item("anyaman"))
                        r.Add("denier", dr.Item("denier"))
                        r.Add("lebar", dr.Item("lebar"))
                        r.Add("panjang", dr.Item("panjang"))
                        r.Add("warna", dr.Item("warna"))
                        r.Add("cetakan", dr.Item("cetakan"))
                        r.Add("mesin", jsondata("mesin"))
                        r.Add("berat_tungkul", jsondata("berat_tungkul"))
                        r.Add("bruto", jsondata("bruto"))
                        r.Add("netto", jsondata("netto"))
                        r.Add("cust_po", jsondata("cust_po"))
                        r.Add("customer", jsondata("customer"))
                        r.Add("customer_code", jsondata("customer_code"))
                        r.Add("penimbang", dr.Item("pic1"))
                        r.Add("penerima", dr.Item("pic2"))
                        r.Add("cetakan_ke", jsondata("cetakan_ke"))
                    Case "CS", "CKA"
                        r.Add("anyaman", dr.Item("anyaman"))
                        r.Add("denier", dr.Item("denier"))
                        r.Add("lebar", dr.Item("lebar"))
                        r.Add("panjang", dr.Item("panjang"))
                        r.Add("jumlah", jsondata("jumlah"))
                        r.Add("jenis", jsondata("jenis"))
                        r.Add("mesin", jsondata("mesin"))
                        r.Add("urutan", jsondata("urutan"))
                        r.Add("is_bs", If(jsondata.ContainsKey("is_bs"), If(jsondata("is_bs"), 1, 0), 0))
                        r.Add("pemotong", jsondata("pemotong"))
                        r.Add("penerima", dr.Item("pic2"))
                        r.Add("team", jsondata("team"))
                        r.Add("customer", jsondata("customer"))
                        r.Add("customer_code", jsondata("customer_code"))
                    Case "INR"
                        r.Add("anyaman", dr.Item("anyaman"))
                        r.Add("denier", dr.Item("denier"))
                        r.Add("lebar", dr.Item("lebar"))
                        r.Add("panjang", dr.Item("panjang"))
                        r.Add("jumlah", jsondata("jumlah"))
                        r.Add("cetakan", dr.Item("cetakan"))
                        r.Add("so_num", jsondata("so_num"))
                        r.Add("palet", jsondata("palet"))
                        r.Add("operator", jsondata("operator"))
                        r.Add("penerima", jsondata("penerima"))
                        r.Add("team", jsondata("team"))
                        r.Add("customer", jsondata("customer"))
                        r.Add("customer_code", jsondata("customer_code"))

                        ''Added by #Steve
                        Dim stdinner As Double
                        Dim stdberat As Double = (((dr.Item("lebar") * 2)) * ((dr.Item("panjang") + 4)) * (dr.Item("anyaman") + dr.Item("anyaman")) * (dr.Item("denier") / 1000) / 2286)
                        If jsondata.ContainsKey("lebarinner") And jsondata.ContainsKey("panjanginner") And jsondata.ContainsKey("tebalinner") Then
                            stdinner = jsondata("lebarinner") * (jsondata("panjanginner") + 1) * jsondata("tebalinner") * 0.2 * 0.91416
                        Else
                            stdinner = 0
                        End If

                        Dim aktualberat As Double = (jsondata("berat") / jsondata("jumlah")) * 1000
                        Dim selisih As Double = aktualberat - (stdberat + stdinner)
                        Dim total As Double = stdberat + stdinner
                        Dim ptg As Double = selisih / total * 100

                        If ptg > 1 Then
                            r.Add("status", "Status : BERAT" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        ElseIf ptg < -4 Then
                            r.Add("status", "Status : RINGAN" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        Else
                            r.Add("status", "Status : STANDAR" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        End If

                        r.Add("persen", selisih)
                        '                        r.Add("inner", jsondata("lebarinner") & "x" & jsondata("panjanginner") & "x" & jsondata("tebalinner"))
                        If jsondata.ContainsKey("lebarinner") And jsondata.ContainsKey("panjanginner") And jsondata.ContainsKey("tebalinner") Then
                            r.Add("inner", jsondata("lebarinner") & "x" & jsondata("panjanginner") & "x" & jsondata("tebalinner"))
                        Else
                            r.Add("inner", "-")
                        End If

                        r.Add("stdinner", stdinner)
                        'end here

                    Case "JHT"
                        r.Add("anyaman", dr.Item("anyaman"))
                        r.Add("denier", dr.Item("denier"))
                        r.Add("lebar", dr.Item("lebar"))
                        r.Add("panjang", dr.Item("panjang"))
                        r.Add("jumlah", jsondata("jumlah"))
                        r.Add("cetakan", dr.Item("cetakan"))
                        r.Add("so_num", jsondata("so_num"))
                        r.Add("palet", jsondata("palet"))
                        r.Add("operator", jsondata("operator"))
                        r.Add("penerima", jsondata("penerima"))
                        r.Add("team", jsondata("team"))
                        r.Add("customer", jsondata("customer"))
                        r.Add("customer_code", jsondata("customer_code"))
                    Case "PRT"
                        r.Add("anyaman", dr.Item("anyaman"))
                        r.Add("denier", dr.Item("denier"))
                        r.Add("lebar", dr.Item("lebar"))
                        r.Add("panjang", dr.Item("panjang"))
                        r.Add("cetakan", dr.Item("cetakan"))
                        r.Add("jumlah", jsondata("jumlah"))
                        r.Add("berat", jsondata("berat"))
                        r.Add("lanjut", jsondata("lanjut"))
                        r.Add("pekerja", jsondata("pekerja"))
                        r.Add("penerima", jsondata("penerima"))
                        r.Add("team", jsondata("team"))
                        r.Add("mesin", jsondata("mesin"))
                        r.Add("no_palet", jsondata("no_palet"))
                        r.Add("cust_po", jsondata("cust_po"))
                        r.Add("customer", jsondata("customer"))
                        r.Add("customer_code", jsondata("customer_code"))
                        r.Add("so_number", jsondata("so_number"))
                    Case "OPP"
                        r.Add("anyaman", dr.Item("anyaman"))
                        r.Add("denier", dr.Item("denier"))
                        r.Add("lebar", dr.Item("lebar"))
                        r.Add("panjang", dr.Item("panjang"))
                        r.Add("cetakan", dr.Item("cetakan"))
                        r.Add("bruto", jsondata("bruto"))
                        r.Add("netto", jsondata("netto"))
                        r.Add("pekerja", jsondata("pekerja"))
                        r.Add("penerima", jsondata("penerima"))
                        r.Add("team", jsondata("team"))
                        r.Add("opp", dr.Item("opp"))
                        r.Add("cust_po", jsondata("cust_po"))
                        r.Add("customer", jsondata("customer"))
                        r.Add("customer_code", jsondata("customer_code"))
                        r.Add("so_number", jsondata("so_number"))

                    Case "PCK", "PCA"
                        r.Add("anyaman", dr.Item("anyaman"))
                        r.Add("denier", dr.Item("denier"))
                        r.Add("lebar", dr.Item("lebar"))
                        r.Add("panjang", dr.Item("panjang"))
                        r.Add("cetakan", dr.Item("cetakan"))
                        r.Add("jumlah", jsondata("jumlah") & " Lembar")
                        r.Add("ball", jsondata("ball"))
                        'r.Add("berat", jsondata("berat"))

                        'Added By #Steve
                        Dim berat As Double = jsondata("berat")
                        Dim jlh As Double = jsondata("jumlah")
                        berat = FormatNumber((berat - 0.5), 2)
                        Dim beratlbr As Integer = (berat / jlh) * 1000
                        r.Add("berat", berat & " Kg")
                        r.Add("beratlbr", beratlbr & " gram")
                        'End Here                    
                        r.Add("mesin", jsondata("mesin"))
                        r.Add("pekerja", jsondata("pekerja"))
                        r.Add("penerima", jsondata("penerima"))
                        r.Add("team", jsondata("team"))
                        r.Add("customer", jsondata("customer"))
                        r.Add("customer_code", jsondata("customer_code"))
                        r.Add("so_number", jsondata("so_number"))

                        Dim stdinner As Double
                        Dim stdberat As Double = (((dr.Item("lebar") * 2)) * ((dr.Item("panjang") + 4)) * (dr.Item("anyaman") + dr.Item("anyaman")) * (dr.Item("denier") / 1000) / 2286)
                        If jsondata.ContainsKey("lebarinner") And jsondata.ContainsKey("panjanginner") And jsondata.ContainsKey("tebalinner") Then
                            stdinner = jsondata("lebarinner") * (jsondata("panjanginner") + 1) * jsondata("tebalinner") * 0.2 * 0.91416
                        Else
                            stdinner = 0
                        End If

                        Dim aktualberat As Double = (berat / jsondata("jumlah")) * 1000
                        Dim selisih As Double = aktualberat - (stdberat + stdinner)
                        Dim total As Double = stdberat + stdinner
                        Dim ptg As Double = selisih / total * 100

                        If ptg > 1 Then
                            r.Add("status", "Status : BERAT" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        ElseIf ptg < -4 Then
                            r.Add("status", "Status : RINGAN" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        Else
                            r.Add("status", "Status : STANDAR" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        End If
                        r.Add("persen", selisih)

                        If jsondata.ContainsKey("lebarinner") And jsondata.ContainsKey("panjanginner") And jsondata.ContainsKey("tebalinner") Then
                            r.Add("inner", jsondata("lebarinner") & "x" & jsondata("panjanginner") & "x" & jsondata("tebalinner"))
                        Else
                            r.Add("inner", "-")
                        End If

                        r.Add("stdinner", stdinner)
                        'Added by #Steve
                    Case "CIN"
                        r.Add("anyaman", dr.Item("anyaman"))
                        r.Add("denier", dr.Item("denier"))
                        r.Add("lebar", dr.Item("lebar"))
                        r.Add("panjang", dr.Item("panjang"))
                        r.Add("jumlah", jsondata("jumlah") & " Lembar")
                        r.Add("mesin", jsondata("mesin"))
                        r.Add("pemotong", jsondata("pemotong"))
                        Dim berat As Decimal = jsondata("berat")
                        Dim jlh As Integer = jsondata("jumlah")
                        berat = FormatNumber((berat), 2)
                        Dim beratlbr As Integer = (berat / jlh) * 1000
                        r.Add("berat", berat & " kg")
                        r.Add("beratlbr", beratlbr & " gram")
                        r.Add("customer", jsondata("customer"))
                        r.Add("jenis", jsondata("jenis"))

                        Dim stdberat As Double = dr.Item("lebar") * (dr.Item("panjang") + 1) * dr.Item("anyaman") * 0.2 * 0.91416
                        Dim aktualberat As Double = ((berat / jlh) * 1000)
                        Dim selisih As Double = aktualberat - stdberat
                        Dim ptg As Double = selisih / stdberat * 100
                        If ptg > 0 Then
                            r.Add("status", "Status : BERAT" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        ElseIf ptg < -6 Then
                            r.Add("status", "Status : RINGAN" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        Else
                            r.Add("status", "Status : STANDAR" & vbCrLf & "Persentase : " & Math.Round(ptg, 2) & " %")
                        End If

                        'end bere

                End Select

                repdata.Add(r)

                _parent.dicPrint = repdata

                Select Case line
                    Case "EXT"
                        _parent.reportPrint = LCase(corp) + "-extruder-label.xlsx"
                    Case "CL", "CRA"
                        _parent.reportPrint = LCase(corp) + "-loom-label.xlsx"
                    Case "RTR"
                        _parent.reportPrint = LCase(corp) + "-roll2roll-label.xlsx"
                    Case "CS", "CKA"
                        _parent.reportPrint = LCase(corp) + "-cutting-label.xlsx"
                    Case "INR"
                        _parent.reportPrint = LCase(corp) + "-insertinner-label.xlsx"
                    Case "JHT"
                        _parent.reportPrint = LCase(corp) + "-jahit-label.xlsx"
                    Case "PRT"
                        _parent.reportPrint = LCase(corp) + "-printing-label.xlsx"
                    Case "OPP"
                        _parent.reportPrint = LCase(corp) + "-opp-label.xlsx"
                    Case "PCK", "PCA"
                        _parent.reportPrint = LCase(corp) + "-packing-label.xlsx"
                        'Added by #Steve

                    Case "CIN"
                        _parent.reportprint = LCase(corp) + "-cuttinginner-label.xlsx"
                End Select

                _parent.papersize = papersize
                '_parent.papersize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperEnvelope11
                _parent.doPrint()
            Next
        Next
    End Sub

    Private Sub btnPicker(sender As Object, e As EventArgs)
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        Select Case DirectCast(sender, TextBox).Name
            Case "btnDenierEX"
                cmd.CommandText = "SELECT * FROM bobbins WHERE company_code = ?"
                cmd.Parameters.AddWithValue("company_code", corp)
        End Select

        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        lstFind.Items.Clear()

        If ds.Tables.Count = 1 Then
            For Each row As DataRow In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item("bobbin"))
                For i = 1 To row.ItemArray.Count - 1
                    item.SubItems.Add(row.Item("bobbin_code"))
                    item.SubItems.Add(row.Item("weight"))
                Next
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        lstFind.Left = tbBobbinType.Left + tcLine.Left + gbLabel.Left + tcLine.Margin.Left
        lstFind.Top = tbBobbinType.Top + tcLine.Top + tcLine.Height - tcLine.TabPages(0).Height - tcLine.Margin.Bottom + gbLabel.Top + tbBobbinType.Height

        lstFind.Visible = True

        textFocus = tbBobbinType
        textCode = tbBobbinWeight
        dataCol = 2
        textFocus.Focus()
    End Sub

    Private Sub syncTick_Tick(sender As Object, e As EventArgs) Handles syncTick.Tick
        'If dbCon Is Nothing Then Exit Sub
        If dbCon.state <> ConnectionState.Open Then Exit Sub
        sync("production_details", lblStationEX.Text)
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim item As ListViewItem
        If lstData.SelectedItems.Count = 1 Then
            item = lstData.SelectedItems(0)
            Dim cmd As SQLiteCommand = localCon.CreateCommand()
            cmd.CommandText = "SELECT * FROM production_details WHERE doc_number = ?"
            cmd.Parameters.AddWithValue("doc_number", item.Text)
            Dim adp As New SQLiteDataAdapter(cmd)
            Dim ds As New DataSet
        End If
    End Sub

    Private Sub btnPrintRTR_Click(sender As Object, e As EventArgs) Handles btnPrintRTR.Click
        printLabel("RTR")
        'clearForm()
    End Sub

    Private Sub btnItemFind_Click(sender As Object, e As EventArgs) Handles btnItemFind.Click
        clearForm()
    End Sub

    Private Sub numChange(sender As Object, e As EventArgs) Handles tbBobbin.TextChanged, tbBobbinWeight.TextChanged, tbBrutoEX.TextChanged, tbTroli.TextChanged,
            tbTungkul.TextChanged, tbBrutoCL.TextChanged, tbTungkulRTR.TextChanged, tbBrutoRTR.TextChanged
        Select Case sender.Name
            Case "tbBobbinWeight"
                calcWeight("EXT")
            Case "tbBobbin", "tbBrutoEX", "tbTroli", "tbTungkul", "tbBrutoCL", "tbTungkulRTR", "tbBrutoRTR"
                Dim t As Type = GetType(Control)
                Dim mi As MethodInfo = t.GetMethod("OnValidating", BindingFlags.Instance Or BindingFlags.NonPublic)
                Dim ce As CancelEventArgs = New CancelEventArgs()
                mi.Invoke(sender, {ce})
        End Select
    End Sub

    Private Sub numOriText(sender As Object, e As KeyPressEventArgs) Handles tbBobbin.KeyPress, tbBrutoEX.KeyPress, tbTroli.KeyPress,
            tbTungkul.KeyPress, tbBrutoCL.KeyPress, tbTungkulRTR.KeyPress, tbBrutoRTR.KeyPress
        txtValidate = sender.Text
    End Sub

    Private Sub numValidate(sender As Object, e As CancelEventArgs) Handles tbBobbin.Validating, tbBrutoEX.Validating, tbTroli.Validating,
            tbTungkul.Validating, tbBrutoCL.Validating, tbTungkulRTR.Validating, tbBrutoRTR.Validating
        Dim dec As Decimal
        If Not Decimal.TryParse("0" + sender.Text, dec) Then
            e.Cancel = True
            sender.Text = txtValidate
        Else
            Select Case sender.Name
                Case "tbBobbin", "tbBrutoEX", "tbTroli"
                    calcWeight("EXT")
                Case "tbTungkul", "tbBrutoCL"
                    calcWeight("CL")
                Case "tbTungkulRTR", "tbBrutoRTR"
                    calcWeight("RTR")
            End Select
        End If
    End Sub

    Private Sub btnMesinEX_Click(sender As Object, e As EventArgs) Handles btnMesinEX.Click, btnMesinCL.Click, btnMesinCS.Click, btnMesinPCK.Click, btnMesinRTR.Click, btnMesinPRT.Click, Button6.Click
        Dim tb As TextBox = Nothing
        Dim line As String = ""
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        Select Case sender.Name
            Case "btnMesinEX"
                line = "EXT"
            Case "btnMesinCL"
                line = "CL"
            Case "btnMesinCS"
                line = "CS"
            Case "btnMesinPCK"
                line = "PCK"
            Case "btnMesinRTR"
                line = "RTR"
            Case "btnMesinPRT"
                line = "PRT"
            Case "Button6"
                line = "CIN"
        End Select


        cmd.CommandText = "SELECT machine_code FROM machines WHERE company_code = ? AND line_code = ? ORDER BY machine_code"
        cmd.Parameters.AddWithValue("company_code", corp)
        cmd.Parameters.AddWithValue("line_code", line)

        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        lstFind.Items.Clear()
        lstFind.Columns(0).Text = "Machine"
        lstFind.Columns(1).Text = ""
        lstFind.Columns(2).Text = ""

        If ds.Tables.Count = 1 Then
            For Each row In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item("machine_code"))
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        Select Case DirectCast(sender, Button).Name
            Case "btnMesinEX"
                tb = tbMesinEX
            Case "btnMesinCL"
                tb = tbMesinCL
            Case "btnMesinRTR"
                tb = tbMesinRTR
            Case "btnMesinCS"
                tb = tbMesinCS
            Case "btnMesinPRT"
                tb = tbMesinPRT
            Case "btnMesinPCK"
                tb = tbMesinPCK
                'Added by 'Steve
            Case "Button6"
                tb = TextBox10
                'End here            
        End Select

        If tb IsNot Nothing Then
            lstFind.Left = tb.Left + tcLine.Left + gbLabel.Left + tcLine.Margin.Left
            lstFind.Top = tb.Top + tcLine.Top + tcLine.Height - tcLine.TabPages(0).Height - tcLine.Margin.Bottom + gbLabel.Top + tb.Height

            lstFind.Visible = True

            textFocus = tb
            textCode = Nothing
            textCol = 0
            textFocus.Focus()
        End If
    End Sub

    Private Sub cbShifts_GotFocus(sender As Object, e As EventArgs) Handles cbShifts.GotFocus
        If cbShifts.Items.Count = 0 Then
            loadShifts()
        End If
    End Sub

    Private Function search(lst As List(Of ListViewItem), Key As String) As IQueryable(Of ListViewItem)
        search = lst.AsQueryable

        Dim filter As Expression(Of Func(Of ListViewItem, Boolean)) =
            Function(i As ListViewItem) i.Text.IndexOf(Key, StringComparison.InvariantCultureIgnoreCase) >= 0 Or i.SubItems(7).Text.IndexOf(Key, StringComparison.InvariantCultureIgnoreCase) >= 0 Or i.SubItems(8).Text.IndexOf(Key, StringComparison.InvariantCultureIgnoreCase) >= 0

        If Not String.IsNullOrEmpty(Key) Then search = Queryable.Where(search, filter)
    End Function

    Private Sub tbCari_TextChanged(sender As Object, e As EventArgs) Handles tbCari.TextChanged
        lstData.Items.Clear()
        For Each item In search(searchData, tbCari.Text)
            lstData.Items.Add(item)
        Next
    End Sub

    Private Function filter(lst As List(Of ListViewItem), Key As String) As IQueryable(Of ListViewItem)
        filter = lst.AsQueryable

        Dim filterExp As Expression(Of Func(Of ListViewItem, Boolean)) =
            Function(i As ListViewItem) i.Text.IndexOf(Key, StringComparison.InvariantCultureIgnoreCase) >= 0 Or i.SubItems(1).Text.IndexOf(Key, StringComparison.InvariantCultureIgnoreCase) >= 0

        If Not String.IsNullOrEmpty(Key) Then filter = Queryable.Where(filter, filterExp)
    End Function

    Private Sub tbPIC_TextChanged(sender As Object, e As EventArgs) Handles tbPIC1EX.TextChanged, tbPIC2EX.TextChanged,
            tbPIC1CL.TextChanged, tbPIC2CL.TextChanged, tbPemotongCL.TextChanged,
            tbPIC1CS.TextChanged, tbPIC2CS.TextChanged,
            tbPIC1PRT.TextChanged, tbPIC2PRT.TextChanged,
            tbPIC1RTR.TextChanged, tbPIC2RTR.TextChanged,
            tbPIC1OPP.TextChanged, tbPIC2OPP.TextChanged,
            tbPIC1PCK.TextChanged, tbPIC2PCK.TextChanged,
            tbPIC1JHT.TextChanged, tbPIC2JHT.TextChanged,
            tbCustomerCS.TextChanged, tbCustomerII.TextChanged,
            tbCustomerJHT.TextChanged, tbCustomerOPP.TextChanged,
            tbCustomerPCK.TextChanged, tbCustomerPRT.TextChanged,
            tbCustomerRTR.TextChanged, TextBox5.TextChanged
        If Not lstFind.Visible Then
            Select Case sender.Name
                Case "tbPIC1EX"
                    btnPIC1EX.PerformClick()
                Case "tbPIC2EX"
                    btnPIC2EX.PerformClick()
                Case "tbPIC1CL"
                    btnPIC1CL.PerformClick()
                Case "tbPIC2CL"
                    btnPIC2CL.PerformClick()
                Case "tbPIC1CS"
                    btnPIC1CS.PerformClick()
                Case "tbPIC2CS"
                    btnPIC2CS.PerformClick()
                Case "tbPIC1PRT"
                    btnPIC1PRT.PerformClick()
                Case "tbPIC2PRT"
                    btnPIC2PRT.PerformClick()
                Case "tbPIC1RTR"
                    btnPIC1RTR.PerformClick()
                Case "tbPIC2RTR"
                    btnPIC2RTR.PerformClick()
                Case "tbPIC1OPP"
                    btnPIC1OPP.PerformClick()
                Case "tbPIC2OPP"
                    btnPIC2OPP.PerformClick()
                Case "tbPIC1PCK"
                    btnPIC1PCK.PerformClick()
                Case "tbPIC2PCK"
                    btnPIC2PCK.PerformClick()
                Case "tbPIC1JHT"
                    btnPIC1JHT.PerformClick()
                Case "tbPIC2JHT"
                    btnPIC2JHT.PerformClick()
                Case "tbPemotongCL"
                    btnPemotongCL.PerformClick()
                Case "tbCustomerCS"
                    btnCustCS.PerformClick()
                Case "tbCustomerII"
                    btnCustII.PerformClick()
                Case "tbCustomerJHT"
                    btnCustJHT.PerformClick()
                Case "tbCustomerOPP"
                    btnCustOPP.PerformClick()
                Case "tbCustomerPCK"
                    btnCustPCK.PerformClick()
                Case "tbCustomerPRT"
                    btnCustPRT.PerformClick()
                Case "tbCustomerRTR"
                    btnCustRTR.PerformClick()
                    'Added by #Steve
                Case "TextBox5"
                    Button2.PerformClick()
            End Select
        End If
        lstFind.Items.Clear()
        For Each item In filter(filterData, sender.Text)
            lstFind.Items.Add(item)
        Next
    End Sub

    Private Sub btnTungkulRTR_Click(sender As Object, e As EventArgs) Handles btnTungkulRTR.Click
        If lblWeightRTR.Text = "-" Then Exit Sub
        tbTungkulRTR.Text = lblWeightRTR.Text
    End Sub

    Private Sub btnBrutoRTR_Click(sender As Object, e As EventArgs) Handles btnBrutoRTR.Click
        If lblWeightRTR.Text = "-" Then Exit Sub
        tbBrutoRTR.Text = lblWeightRTR.Text
    End Sub

    Private Sub btnPrintJHT_Click(sender As Object, e As EventArgs) Handles btnPrintJHT.Click
        printLabel("JHT")
        'clearForm()
    End Sub

    Private Sub tbPIC_KeyUp(sender As Object, e As KeyEventArgs) Handles tbPIC1EX.KeyUp, tbPIC2EX.KeyUp,
            tbPIC1CL.KeyUp, tbPIC2CL.KeyUp, tbPemotongCL.KeyUp,
            tbPIC1CS.KeyUp, tbPIC2CS.KeyUp,
            tbPIC1PRT.KeyUp, tbPIC2PRT.KeyUp,
            tbPIC1RTR.KeyUp, tbPIC2RTR.KeyUp,
            tbPIC1OPP.KeyUp, tbPIC2OPP.KeyUp,
            tbPIC1PCK.KeyUp, tbPIC2PCK.KeyUp,
            tbPIC1JHT.KeyUp, tbPIC2JHT.KeyUp
        If e.KeyCode = Keys.Down Then
            lstFind.Focus()
            If lstFind.Items.Count > 0 Then
                lstFind.Items(0).Selected = True
            End If
        End If
    End Sub

    Private Sub lstFind_KeyUp(sender As Object, e As KeyEventArgs) Handles lstFind.KeyUp
        If e.KeyCode = Keys.Enter Then
            If lstFind.SelectedItems.Count = 1 And (textFocus IsNot Nothing Or textCode IsNot Nothing) Then
                Dim item As ListViewItem = lstFind.SelectedItems(0)
                If textCode IsNot Nothing Then
                    textFocus.Text = item.SubItems(textCol).Text
                    textCode.Text = item.SubItems(dataCol).Text
                ElseIf textCol > 0 Then
                    textFocus.Text = item.SubItems(textCol).Text
                Else
                    textFocus.Text = item.Text
                End If
                textFocus.Focus()
                lstFind.Visible = False
            End If
        End If
    End Sub

    Private Sub btnCustCS_Click(sender As Object, e As EventArgs) Handles btnCustCS.Click, btnCustII.Click, btnCustJHT.Click, btnCustOPP.Click, btnCustPCK.Click,
            btnCustPRT.Click, btnCustRTR.Click, Button3.Click
        Dim tb As TextBox = Nothing
        Dim tc As TextBox = Nothing
        Dim cmd As SQLiteCommand = localCon.CreateCommand()

        cmd.CommandText = "SELECT DISTINCT customer_code, customer_name FROM customers WHERE company_code = ?"
        cmd.Parameters.AddWithValue("company_code", corp)

        Dim adp As New SQLiteDataAdapter(cmd)
        Dim ds As New DataSet
        Dim item As ListViewItem

        adp.Fill(ds)

        filterData.Clear()

        lstFind.Items.Clear()
        lstFind.Columns(0).Text = "Customer Code"
        lstFind.Columns(1).Text = "Customer Name"
        lstFind.Columns(2).Text = ""

        If ds.Tables.Count = 1 Then
            For Each row In ds.Tables(0).Rows
                item = lstFind.Items.Add(row.Item("customer_code"))
                item.SubItems.Add(row.Item("customer_name"))
                filterData.Add(item)
            Next
        End If

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()

        Select Case DirectCast(sender, Button).Name
            Case "btnCustCS"
                tc = tbCustCodeCS
                tb = tbCustomerCS
            Case "btnCustII"
                tc = tbCustCodeII
                tb = tbCustomerII
            Case "btnCustJHT"
                tc = tbCustCodeJHT
                tb = tbCustomerJHT
            Case "btnCustOPP"
                tc = tbCustCodeOPP
                tb = tbCustomerOPP
            Case "btnCustPCK"
                tc = tbCustCodePCK
                tb = tbCustomerPCK
            Case "btnCustPRT"
                tc = tbCustCodePRT
                tb = tbCustomerPRT
            Case "btnCustRTR"
                tc = tbCustCodeRTR
                tb = tbCustomerRTR
            Case "Button3"
                tc = TextBox11
                tb = TextBox6
        End Select

        If tb IsNot Nothing Then
            lstFind.Left = tb.Left + tcLine.Left + gbLabel.Left + tcLine.Margin.Left
            lstFind.Top = tb.Top + tcLine.Top + tcLine.Height - tcLine.TabPages(0).Height - tcLine.Margin.Bottom + gbLabel.Top + tb.Height

            lstFind.Visible = True
            textFocus = tb
            textCode = tc
            dataCol = 0
            textCol = 1
            textFocus.Focus()
        End If
    End Sub

    Private Sub cbLanjutPRT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLanjutPRT.SelectedIndexChanged

    End Sub

    Private Sub VoidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VoidToolStripMenuItem.Click
        'If role = "user" Then
        '    MsgBox("Access Denied")
        '    Exit Sub
        'End If
        If lstData.SelectedItems.Count <> 1 Then
            MsgBox("Pilih dulu dokumen yang akan di void")
            Exit Sub
        End If
        Dim item As ListViewItem = lstData.SelectedItems(0)

        If MessageBox.Show("Apakah yakin akan membatalkan label " & item.Text & "?", "Pembatalan Label", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Dim cmd As SQLiteCommand = localCon.CreateCommand()
            cmd.CommandText = "UPDATE production_details SET void = 1, synced = 0 WHERE doc_number = ? AND company_code = ?"
            cmd.Parameters.AddWithValue("doc_number", item.Text)
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.ExecuteNonQuery()

            dbCon.SQL("UPDATE production_details SET void = 1, void_by = @vb WHERE doc_number = @dn AND company_code = @cc")
            dbCon.addParameter("@vb", user)
            dbCon.addParameter("@dn", item.Text)
            dbCon.addParameter("@cc", corp)
            dbCon.execute()

            dbCon.SQL("DELETE FROM production_details_sync WHERE doc_number = @dn AND company_code = @cc")
            dbCon.addParameter("@dn", item.Text)
            dbCon.addParameter("@cc", corp)
            dbCon.execute()
        End If
    End Sub

    Private Sub btnBeratPRT_Click(sender As Object, e As EventArgs) Handles btnBeratPRT.Click, tbBeratPRT.Click

    End Sub

    Private Sub gbLabel_Enter(sender As Object, e As EventArgs) Handles gbLabel.Enter

    End Sub

    Private Sub btnCustPOPCK_Click(sender As Object, e As EventArgs) Handles btnCustPOPCK.Click

    End Sub

    Private Sub tbBeratPCK_TextChanged(sender As Object, e As EventArgs) Handles tbBeratPCK.TextChanged
        Try
            If tbBeratPCK.Text = "" Then
                TextBox2.Text = 0
            Else
                Dim berat As Double
                berat = Convert.ToDouble(tbBeratPCK.Text) - 1 / 2
                TextBox2.Text = Math.Round(berat, 2)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If Label147.Text = "-" Then Exit Sub
        TextBox12.Text = Label147.Text
        printLabel("CIN")
        'clearForm()
    End Sub

    Private Sub lblComPCK_Click(sender As Object, e As EventArgs) Handles lblComPCK.Click

    End Sub

    Private Sub Label131_Click(sender As Object, e As EventArgs) Handles Label131.Click

    End Sub

    Private Sub lblStationII_Click(sender As Object, e As EventArgs) Handles lblStationII.Click

    End Sub

    Private Sub lblStationCI_Click(sender As Object, e As EventArgs) Handles lblStationCI.Click
        Dim station As New Station()

        If lblStationEX.Text <> "UNKNOWN" Then
            station.tbStation.Text = lblStationEX.Text
        End If
        If station.ShowDialog = DialogResult.OK Then
            lblStationEX.Text = station.tbStation.Text
            lblStationCL.Text = station.tbStation.Text
            lblStationCS.Text = station.tbStation.Text
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

    Private Sub tbCustPOPCK_TextChanged(sender As Object, e As EventArgs) Handles tbCustPOPCK.TextChanged

    End Sub

    Private Sub tbLebarPCK_TextChanged(sender As Object, e As EventArgs) Handles tbLebarPCK.TextChanged

    End Sub

    Private Sub lblStationPCK_Click(sender As Object, e As EventArgs) Handles lblStationPCK.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            tbLebar.Enabled = True
            tbPanjang.Enabled = True
            tbTebal.Enabled = True
        ElseIf CheckBox1.Checked = False Then
            tbLebar.Enabled = False
            tbPanjang.Enabled = False
            tbTebal.Enabled = False
            tbLebar.Text = ""
            tbPanjang.Text = ""
            tbTebal.Text = ""
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            tbLebarINR.Enabled = True
            tbPanjangINR.Enabled = True
            tbTebalINR.Enabled = True
        ElseIf CheckBox3.Checked = False Then
            tbLebarINR.Enabled = False
            tbPanjangINR.Enabled = False
            tbTebalINR.Enabled = False
            tbLebarINR.Text = ""
            tbPanjangINR.Text = ""
            tbTebalINR.Text = ""
        End If
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress

    End Sub

    Dim charactersAllowed As String = ",1234567890"

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        'Dim theText As String = TextBox9.Text
        'Dim Letter As String = "."
        'For x As Integer = 0 To TextBox9.Text.Length - 1
        '    Letter = TextBox9.Text.Substring(x, 1)
        '    If charactersAllowed.Contains(Letter) = False Then
        '        theText = theText.Replace(Letter, ",")
        '    End If
        'Next
        'TextBox9.Text = theText
        'TextBox9.Select(TextBox9.Text.Length, 0)
    End Sub

    Private Sub btnWarnaEX_Click(sender As Object, e As EventArgs) Handles btnWarnaEX.Click

    End Sub

    Private Sub tbBrutoOPP_TextChanged(sender As Object, e As EventArgs) Handles tbBrutoOPP.TextChanged
        Try
            tbNettoOPP.Text = tbBrutoOPP.Text - 2.5
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub tbTebal_TextChanged(sender As Object, e As EventArgs) Handles tbTebal.TextChanged

    End Sub

    Private Sub tbTebal_Leave(sender As Object, e As EventArgs) Handles tbTebal.Leave
        Try
            Dim subString As String = Microsoft.VisualBasic.Left(tbTebal.Text, 3)
            Dim xstring As String = Microsoft.VisualBasic.Right(tbTebal.Text, 2)
            If subString <> "0.0" Then
                tbTebal.Text = "0.0" & xstring.Replace(".", "")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ChkOPP_CheckedChanged(sender As Object, e As EventArgs) Handles ChkOPP.CheckedChanged
        If ChkOPP.Checked = True Then
            ComboPCK.Enabled = True
        ElseIf ChkOPP.Checked = False Then
            ComboPCK.Enabled = False
            ComboPCK.Text = ""
        End If
    End Sub
End Class