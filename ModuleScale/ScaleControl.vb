Imports ERPModules
'Imports DB
Imports System.Data.SQLite
Imports System.IO
Imports System.Windows.Forms
'Imports ModuleScale

Public Class ScaleControl : Inherits ERPModule
    Private buffer As String = ""
    Private continuous As Boolean = True
    Private dataLength As Integer
    Private readStart As Integer
    Private readEnd As Integer
    Private precision As Integer
    Private unitRatio As Decimal
    Private startChar As Char
    Private endChar As Char
    Private active_scale As String = ""

    Private ticketList As New List(Of ListViewItem)

    Public localCon As SQLiteConnection

    Dim scaleObj As ScaleClass

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

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='weight'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `weight` (
	                                 `id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	                                 `company_code`	TEXT,
	                                 `scale_code`	TEXT,
	                                 `operator`	TEXT,
	                                 `date`	TEXT,
	                                 `time_in`	TEXT,
	                                 `time_out`	TEXT,
	                                 `ticket_no`	TEXT,
	                                 `vehicle_no`	TEXT,
	                                 `item_name`	TEXT,
	                                 `supplier`	TEXT,
	                                 `customer`	TEXT,
	                                 `driver`	TEXT,
	                                 `container_no`	TEXT,
	                                 `delivery_no`	TEXT,
	                                 `note`	TEXT,
	                                 `bruto`	REAL,
	                                 `tare`	REAL,
	                                 `netto`	REAL
                                )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='users'"
            table = cmd.ExecuteScalar()
            If table = "" Then
                cmd.CommandText = "CREATE TABLE `users` (
	                                `username`	TEXT,
	                                `name`	TEXT,
	                                `password`	TEXT,
	                                `role`	TEXT,
	                                `super`	INTEGER,
	                                PRIMARY KEY(username)
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

    Private Function useScale(scale As ListViewItem, port As String) As Boolean
        Dim cmd As SQLiteCommand

        If localCon.State = ConnectionState.Open Then
            active_scale = scale.Text

            cmd = localCon.CreateCommand()
            cmd.CommandText = "REPLACE INTO `used_devices`(`company_code`, `device`, `table_name`, `device_id`) values(?, ?, ?, ?)"
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("device", "scale")
            cmd.Parameters.AddWithValue("table_name", "scales")
            cmd.Parameters.AddWithValue("device_id", Integer.Parse(scale.SubItems(11).Text))
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
            'cmd.Parameters.AddWithValue("precision", Integer.Parse(scale.SubItems(9).Text))
            'cmd.Parameters.AddWithValue("unitratio", Decimal.Parse(scale.SubItems(10).Text))
            'cmd.Parameters.AddWithValue("lastupdate", Now)
            'cmd.ExecuteNonQuery()
            'cmd.Dispose()

            Return True
        Else
            Return False
        End If
    End Function

    Private Function getNextNumber() As Integer
        Dim cmd As SQLiteCommand
        cmd = localCon.CreateCommand()
        cmd.CommandText = "SELECT ifnull(MAX(`num`), 0)+1 FROM weight WHERE date(`date`) like ? AND company_code = ?"
        cmd.Parameters.AddWithValue("date", Now.ToString("yyyy-MM") + "%")
        cmd.Parameters.AddWithValue("company_code", corp)
        Return cmd.ExecuteScalar()
    End Function

    Public Sub saveTicket()
        Dim cmd As SQLiteCommand
        Dim item As ListViewItem
        Dim _now As Date = Now

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand
            cmd.CommandText = "INSERT INTO weight(
                                    company_code, scale_code, operator, `date`, time_in, time_out, ticket_no, vehicle_no, item_name, supplier, 
                                    customer, driver, container_no, delivery_no, note, bruto, tare, netto, num
                                ) VALUES(?, ?, ?, date(?), time(?), time(?), ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("scale_code", active_scale)
            cmd.Parameters.AddWithValue("operator", user)
            cmd.Parameters.AddWithValue("date", _now)
            cmd.Parameters.AddWithValue("time_in", _now)
            cmd.Parameters.AddWithValue("time_out", _now)
            cmd.Parameters.AddWithValue("ticket_no", tbNoTiket.Text)
            cmd.Parameters.AddWithValue("vehicle_no", tbNopol.Text)
            cmd.Parameters.AddWithValue("item_name", tbBarang.Text)
            cmd.Parameters.AddWithValue("supplier", tbSupplier.Text)
            cmd.Parameters.AddWithValue("customer", tbCustomer.Text)
            cmd.Parameters.AddWithValue("driver", tbSupir.Text)
            cmd.Parameters.AddWithValue("container_no", tbNoContainer.Text)
            cmd.Parameters.AddWithValue("delivery_no", tbNoDo.Text)
            cmd.Parameters.AddWithValue("note", tbKeterangan.Text)
            cmd.Parameters.AddWithValue("bruto", tbBruto.Text)
            cmd.Parameters.AddWithValue("tare", tbTare.Text)
            cmd.Parameters.AddWithValue("netto", tbNetto.Text)
            cmd.Parameters.AddWithValue("num", getNextNumber())
            cmd.ExecuteNonQuery()

            cmd.Dispose()

            item = lstTiket.Items.Add(tbNoTiket.Text)
            item.SubItems.Add(_now.ToString("yyyy-MM-dd"))
            item.SubItems.Add(tbNopol.Text)
            item.SubItems.Add(tbBarang.Text)
            item.SubItems.Add(tbSupplier.Text)
            item.SubItems.Add(tbCustomer.Text)
            item.SubItems.Add(tbBruto.Text)
            item.SubItems.Add(tbTare.Text)
            item.SubItems.Add(tbNetto.Text)
            item.SubItems.Add(_now.ToString("HH:mm:ss"))
            item.SubItems.Add(_now.ToString("HH:mm:ss"))
            item.SubItems.Add(tbSupir.Text)
            item.SubItems.Add(tbNoContainer.Text)
            item.SubItems.Add(tbNoDo.Text)
            item.SubItems.Add(tbKeterangan.Text)

            ticketList.Add(item)
        End If
    End Sub

    Public Sub updateTicket()
        Dim cmd As SQLiteCommand
        Dim item As ListViewItem
        Dim _now As Date = Now

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand
            cmd.CommandText = "UPDATE weight SET
                                    vehicle_no = ?, time_out = time(?), item_name = ?, driver = ?, container_no = ?, note = ?,
                                    bruto = ?, tare = ?, netto = ?
                                WHERE ticket_no = ? AND company_code = ? AND scale_code = ?"
            cmd.Parameters.AddWithValue("vehicle_no", tbNopol.Text)
            cmd.Parameters.AddWithValue("time_out", _now)
            cmd.Parameters.AddWithValue("item_name", tbBarang.Text)
            cmd.Parameters.AddWithValue("driver", tbSupir.Text)
            cmd.Parameters.AddWithValue("container_no", tbNoContainer.Text)
            cmd.Parameters.AddWithValue("note", tbKeterangan.Text)
            cmd.Parameters.AddWithValue("bruto", tbBruto.Text)
            cmd.Parameters.AddWithValue("tare", tbTare.Text)
            cmd.Parameters.AddWithValue("netto", tbNetto.Text)
            cmd.Parameters.AddWithValue("ticket_no", tbNoTiket.Text)
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("scale_code", active_scale)
            cmd.ExecuteNonQuery()

            cmd.Dispose()

            item = lstTiket.SelectedItems.Item(0)

            item.SubItems(10).Text = _now.ToString("HH:mm:ss")
            item.SubItems(2).Text = tbNopol.Text
            item.SubItems(3).Text = tbBarang.Text
            item.SubItems(11).Text = tbSupir.Text
            item.SubItems(12).Text = tbNoContainer.Text
            item.SubItems(14).Text = tbKeterangan.Text
            item.SubItems(6).Text = tbBruto.Text
            item.SubItems(7).Text = tbTare.Text
            item.SubItems(8).Text = tbNetto.Text
        End If
    End Sub

    Public Sub loadTicket(f As Date, t As Date)
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As New DataSet
        Dim item As ListViewItem

        ticketList.Clear()

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand
            cmd.CommandText = "SELECT * FROM weight WHERE `date` BETWEEN date(?) AND date(?) AND company_code = ? AND scale_code = ?"
            cmd.Parameters.AddWithValue("date1", f.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("date2", t.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("scale_code", active_scale)

            adp = New SQLiteDataAdapter(cmd)
            adp.Fill(ds)

            lstTiket.Items.Clear()

            For Each d In ds.Tables(0).Rows
                item = lstTiket.Items.Add(d.Item("ticket_no"))
                item.SubItems.Add(d.Item("date"))
                item.SubItems.Add(d.Item("vehicle_no"))
                item.SubItems.Add(d.Item("item_name"))
                item.SubItems.Add(d.Item("supplier"))
                item.SubItems.Add(d.Item("customer"))
                item.SubItems.Add(d.Item("bruto"))
                item.SubItems.Add(d.Item("tare"))
                item.SubItems.Add(d.Item("netto"))
                item.SubItems.Add(d.Item("time_in"))
                item.SubItems.Add(d.Item("time_out"))
                item.SubItems.Add(d.Item("driver"))
                item.SubItems.Add(d.Item("container_no"))
                item.SubItems.Add(d.Item("delivery_no"))
                item.SubItems.Add(d.Item("note"))

                ticketList.Add(item)
            Next

            ds.Dispose()
            cmd.Dispose()
        End If
    End Sub

    Public Sub loadTicket(dt As Date)
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As New DataSet
        Dim item As ListViewItem

        ticketList.Clear()

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand
            cmd.CommandText = "SELECT * FROM weight WHERE `date` = date(?) AND company_code = ? AND scale_code = ?"
            cmd.Parameters.AddWithValue("date", dt.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("scale_code", active_scale)

            adp = New SQLiteDataAdapter(cmd)
            adp.Fill(ds)

            lstTiket.Items.Clear()

            For Each d In ds.Tables(0).Rows
                item = lstTiket.Items.Add(d.Item("ticket_no"))
                item.SubItems.Add(d.Item("date"))
                item.SubItems.Add(d.Item("vehicle_no"))
                item.SubItems.Add(d.Item("item_name"))
                item.SubItems.Add(d.Item("supplier"))
                item.SubItems.Add(d.Item("customer"))
                item.SubItems.Add(d.Item("bruto"))
                item.SubItems.Add(d.Item("tare"))
                item.SubItems.Add(d.Item("netto"))
                item.SubItems.Add(d.Item("time_in"))
                item.SubItems.Add(d.Item("time_out"))
                item.SubItems.Add(d.Item("driver"))
                item.SubItems.Add(d.Item("container_no"))
                item.SubItems.Add(d.Item("delivery_no"))
                item.SubItems.Add(d.Item("note"))

                ticketList.Add(item)
            Next

            ds.Dispose()
            cmd.Dispose()
        End If
    End Sub

    Public Sub loadTicket()
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As New DataSet
        Dim item As ListViewItem

        ticketList.Clear()

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand
            cmd.CommandText = "SELECT * FROM weight WHERE `date` = date(?) AND company_code = ? AND scale_code = ?"
            cmd.Parameters.AddWithValue("date", Now)
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("scale_code", active_scale)

            adp = New SQLiteDataAdapter(cmd)
            adp.Fill(ds)

            lstTiket.Items.Clear()

            For Each d In ds.Tables(0).Rows
                item = lstTiket.Items.Add(d.Item("ticket_no"))
                item.SubItems.Add(d.Item("date"))
                item.SubItems.Add(d.Item("vehicle_no"))
                item.SubItems.Add(d.Item("item_name"))
                item.SubItems.Add(d.Item("supplier"))
                item.SubItems.Add(d.Item("customer"))
                item.SubItems.Add(d.Item("bruto"))
                item.SubItems.Add(d.Item("tare"))
                item.SubItems.Add(d.Item("netto"))
                item.SubItems.Add(d.Item("time_in"))
                item.SubItems.Add(d.Item("time_out"))
                item.SubItems.Add(d.Item("driver"))
                item.SubItems.Add(d.Item("container_no"))
                item.SubItems.Add(d.Item("delivery_no"))
                item.SubItems.Add(d.Item("note"))

                ticketList.Add(item)
            Next

            ds.Dispose()
            cmd.Dispose()
        End If
    End Sub

    Private Function ticketExists(company_code As String, scale_code As String, ticket_no As String) As Boolean
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As New DataSet

        If localCon.State = ConnectionState.Open Then
            cmd = localCon.CreateCommand
            cmd.CommandText = "SELECT * FROM weight WHERE ticket_no = ? AND company_code = ? AND scale_code = ?"
            cmd.Parameters.AddWithValue("ticket_no", ticket_no)
            cmd.Parameters.AddWithValue("company_code", corp)
            cmd.Parameters.AddWithValue("scale_code", active_scale)

            adp = New SQLiteDataAdapter(cmd)
            adp.Fill(ds)

            If ds.Tables(0).Rows.Count = 1 Then
                ds.Dispose()
                cmd.Dispose()

                Return True
            Else
                ds.Dispose()
                cmd.Dispose()

                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Function searchTicket(lst As List(Of ListViewItem), Key As String) As IQueryable(Of ListViewItem)
        searchTicket = lst.AsQueryable

        Dim filter As Expression(Of Func(Of ListViewItem, Boolean)) =
            Function(i As ListViewItem) i.Text.IndexOf(Key, StringComparison.InvariantCultureIgnoreCase) >= 0 Or i.SubItems(2).Text.IndexOf(Key, StringComparison.InvariantCultureIgnoreCase) >= 0

        If Not String.IsNullOrEmpty(Key) Then searchTicket = Queryable.Where(searchTicket, filter)
    End Function

    Private Sub clearForm()
        Dim _now As Date = Now

        tbBruto.Text = "0"
        tbTare.Text = "0"
        tbNetto.Text = "0"

        tbNoTiket.Text = getNextNumber().ToString().PadLeft(3, "0") + "/" + corp + "/" + Now.ToString("MM\/yy")
        tbNoTiket.ReadOnly = False

        tbBarang.Text = ""
        tbSupplier.Text = ""
        tbCustomer.Text = ""
        tbSupir.Text = ""
        tbNopol.Text = ""
        tbNoContainer.Text = ""
        tbNoDo.Text = ""
        tbKeterangan.Text = ""

        dpTanggal.Value = _now
        dpMasuk.Value = _now
        dpKeluar.Value = _now

        tbBruto.ReadOnly = True
        tbTare.ReadOnly = True
        'tbNetto.ReadOnly = True

        btnCetak.Enabled = False
        toolButton("Print").enabled = False
        _parent.refreshMenuState(toolButton)
    End Sub

    Public Overrides Sub Print()
        Dim cmd As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim report As String = ""
        Dim toprint As String = ""
        Dim ds As New DataSet

        If Not btnCetak.Enabled Then
            MsgBox("Nothing to print")
            Exit Sub
        End If
        toprint = tbNoTiket.Text

        If dbCon.state() = ConnectionState.Open Then
            Try
                Dim tmpds As New DataSet
                dbCon.SQL("SELECT report FROM reports WHERE company_code = @cc AND report_name = @rn")
                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@rn", "scale")
                dbCon.fillData(tmpds)
                If tmpds.Tables(0).Rows.Count = 1 Then
                    report = tmpds.Tables(0).Rows(0).Item("report")
                Else
                    report = corp + "-scale.xlsx"
                End If
            Catch
                report = corp + "-scale.xlsx"
            End Try
        Else
            report = corp + "-scale.xlsx"
        End If

        cmd = localCon.CreateCommand
        cmd.CommandText = "SELECT * FROM weight WHERE ticket_no = ?"
        cmd.Parameters.AddWithValue("ticket_no", toprint)
        adp = New SQLiteDataAdapter(cmd)

        adp.Fill(ds)

        Try
            DirectCast(FindForm(), Object).dsPrint = ds
            DirectCast(FindForm(), Object).reportPrint = report
            DirectCast(FindForm(), Object).doPrint()

            btnCetak.Enabled = False
            toolButton("Print").enabled = False
            _parent.refreshMenuState(toolButton)
            tbNoTiket.ReadOnly = False
            clearForm()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub Save()
        btnSimpan.PerformClick()
    End Sub

    Public Overloads Sub Dispose()
        Try
            closeData()
            ticketList.Clear()
            toolMenu.Clear()
            sPort.Close()

            MyBase.Dispose()
        Catch
        End Try
    End Sub

    Private Sub rekapTimbangan(sender As Object, e As EventArgs)
        Dim prd As New Periode

        If prd.ShowDialog() = DialogResult.OK Then
            Dim cmd As SQLiteCommand
            Dim adp As SQLiteDataAdapter
            Dim ds As New DataSet
            Dim report As String

            report = corp + "-scale-report.xlsx"

            cmd = localCon.CreateCommand()
            If prd.rbPeriode.Checked Then
                cmd.CommandText = "SELECT * FROM weight WHERE `date` BETWEEN date(?) AND date(?)"
                cmd.Parameters.AddWithValue("date1", prd.dpAwal.Value)
                cmd.Parameters.AddWithValue("date2", prd.dpAkhir.Value)
            Else
                cmd.CommandText = "SELECT * FROM weight WHERE `date` = date(?)"
                cmd.Parameters.AddWithValue("date", prd.dpAwal.Value)
            End If
            adp = New SQLiteDataAdapter(cmd)

            adp.Fill(ds)

            Try
                DirectCast(FindForm(), Object).dsPrint = ds
                DirectCast(FindForm(), Object).reportPrint = report
                DirectCast(FindForm(), Object).doPreview()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub init()
        Dim scale As DataRow = Nothing
        Dim tool As ToolStripItem
        Dim btn As ERPModules.buttonState

        _parent = FindForm()
        DirectCast(_parent, Form).WindowState = FormWindowState.Maximized

        tool = New ToolStripMenuItem()
        tool.Text = "Rekap Timbangan"

        AddHandler tool.Click, AddressOf rekapTimbangan

        toolMenu.Add(tool.Text, tool)

        btn = New ERPModules.buttonState
        btn.enabled = False
        btn.visible = True
        toolButton.Add("Print", btn)

        openData()

        tbNoTiket.Text = getNextNumber().ToString().PadLeft(3, "0") + "/" + corp + "/" + Now.ToString("MM\/yy")

        If checkScale(scale) Then
            active_scale = scale.Item("code")
            loadTicket()

            Dim conf() As String
            conf = scale.Item("config").Split(";")
            sPort.BaudRate = Integer.Parse(conf(0))
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
            sPort.DataBits = Integer.Parse(conf(2))
            sPort.StopBits = Integer.Parse(conf(3))
            sPort.PortName = scale.Item("port")

            continuous = CBool(Integer.Parse(scale.Item("continuous")))
            dataLength = Integer.Parse(scale.Item("datalength"))

            Dim x As Integer = Integer.Parse(scale.Item("startchar"))
            If x > 0 Then
                startChar = Chr(x)
            Else
                startChar = Nothing
            End If
            x = Integer.Parse(scale.Item("endchar"))
            If x > 0 Then
                endChar = Chr(x)
            Else
                endChar = Nothing
            End If

            readStart = Integer.Parse(scale.Item("readstart"))
            readEnd = Integer.Parse(scale.Item("readend"))
            precision = Integer.Parse(scale.Item("precision"))
            unitRatio = Decimal.Parse(scale.Item("unitratio"))

            scaleObj = New ScaleClass(dataLength, startChar, endChar, readStart, readEnd, unitRatio)

            Try
                sPort.Open()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            If sPort.IsOpen Then
                tick.Enabled = True
                lblCom.Text = active_scale
            Else
                lblCom.Text = "COM Error"
            End If
        Else
            lblCom.Text = "No COM defined"
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        init()
    End Sub

    Private Sub tick_Tick(sender As Object, e As EventArgs) Handles tick.Tick
        Dim data As String

        If sPort.IsOpen Then
            scaleObj.appendData(sPort.ReadExisting())
        End If

        data = scaleObj.getValue()

        If Not IsNothing(data) Then
            lblWeight.Text = data
        Else
            lblWeight.Text = "-"
        End If
    End Sub

    Private Sub lblCom_Click(sender As Object, e As EventArgs) Handles lblCom.Click
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
            loadTicket()

            Dim conf() As String
            conf = scale.SubItems(2).Text.Split(";")
            sPort.BaudRate = Integer.Parse(conf(0))
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
            sPort.DataBits = Integer.Parse(conf(2))
            sPort.StopBits = Integer.Parse(conf(3))
            continuous = CBool(Integer.Parse(scale.SubItems(3).Text))
            dataLength = Integer.Parse(scale.SubItems(4).Text)

            Dim x As Integer = Integer.Parse(scale.SubItems(5).Text)
            If x > 0 Then
                startChar = Chr(x)
            Else
                startChar = Nothing
            End If
            x = Integer.Parse(scale.SubItems(6).Text)
            If x > 0 Then
                endChar = Chr(x)
            Else
                endChar = Nothing
            End If
            readStart = Integer.Parse(scale.SubItems(7).Text)
            readEnd = Integer.Parse(scale.SubItems(8).Text)
            unitRatio = Decimal.Parse(scale.SubItems(9).Text)

            scaleObj = New ScaleClass(dataLength, startChar, endChar, readStart, readEnd, unitRatio)

            Try
                sPort.Open()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            If sPort.IsOpen Then
                tick.Enabled = True
                lblCom.Text = active_scale
            Else
                lblCom.Text = "COM Error"
            End If
        End If

        fSetting.Dispose()
    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        Dim prevData As Decimal
        Dim weight As Decimal

        If tbBruto.Text = "" Then
            tbBruto.Text = 0
        End If
        prevData = Decimal.Parse(tbBruto.Text)
        weight = Decimal.Parse(lblWeight.Text)
        If prevData > 0 Then
            If weight > prevData Then
                tbBruto.Text = weight
                tbTare.Text = prevData
                tbNetto.Text = weight - prevData
            Else
                tbTare.Text = weight
                tbBruto.Text = prevData
                tbNetto.Text = prevData - weight
            End If

            dpKeluar.Value = Now
        Else
            tbBruto.Text = weight
            tbTare.Text = 0
            tbNetto.Text = weight

            dpTanggal.Value = Now
            dpMasuk.Value = Now
            dpKeluar.Value = Now
        End If
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If tbNoTiket.Text = "" Then
            MsgBox("Nomor tiket harus diisi")
            Exit Sub
        End If
        'If tbBarang.Text = "" Then
        '    MsgBox("Nama barang harus diisi")
        '    Exit Sub
        'End If
        If tbSupplier.Text = "" Then
            MsgBox("Nama supplier harus diisi")
            Exit Sub
        End If
        If tbCustomer.Text = "" Then
            MsgBox("Nama customer harus diisi")
            Exit Sub
        End If
        If tbSupir.Text = "" Then
            MsgBox("Nama supir harus diisi")
            Exit Sub
        End If
        If tbNopol.Text = "" Then
            MsgBox("Nomor polisi harus diisi")
            Exit Sub
        End If

        If ticketExists(corp, active_scale, tbNoTiket.Text) Then
            updateTicket()
            tbNoTiket.ReadOnly = False
        Else
            saveTicket()
        End If

        Dim result As Integer = MessageBox.Show("Cetak Nota?", "Cetak", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Try
                btnCetak.Enabled = True
                toolButton("Print").enabled = True
                _parent.refreshMenuState(toolButton)
                Print()
            Catch ex As Exception
                MsgBox(ex.Message)
                clearForm()
            End Try
        End If
        clearForm()
    End Sub

    Private Sub lstTiket_DoubleClick(sender As Object, e As EventArgs) Handles lstTiket.DoubleClick
        Dim item As ListViewItem
        Dim _now As Date = Now

        If lstTiket.SelectedItems.Count = 1 Then
            item = lstTiket.SelectedItems.Item(0)

            tbNoTiket.Text = item.Text
            tbNoTiket.ReadOnly = True
            btnCetak.Enabled = True
            toolButton("Print").enabled = True
            _parent.refreshMenuState(toolButton)

            dpTanggal.Value = item.SubItems(1).Text
            tbNopol.Text = item.SubItems(2).Text
            tbBarang.Text = item.SubItems(3).Text
            tbSupplier.Text = item.SubItems(4).Text
            tbCustomer.Text = item.SubItems(5).Text
            tbBruto.Text = item.SubItems(6).Text
            tbTare.Text = item.SubItems(7).Text
            tbNetto.Text = item.SubItems(8).Text
            dpMasuk.Value = _now.ToString("yyyy-MM-dd") + " " + item.SubItems(9).Text
            dpKeluar.Value = _now.ToString("yyyy-MM-dd") + " " + item.SubItems(10).Text
            tbSupir.Text = item.SubItems(11).Text
            tbNoContainer.Text = item.SubItems(12).Text
            tbNoDo.Text = item.SubItems(13).Text
            tbKeterangan.Text = item.SubItems(14).Text
        End If
    End Sub

    Private Sub tbCari_TextChanged(sender As Object, e As EventArgs) Handles tbCari.TextChanged
        lstTiket.Items.Clear()
        For Each item In searchTicket(ticketList, tbCari.Text)
            lstTiket.Items.Add(item)
        Next
    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        Print()
    End Sub

    Private Sub btnKosong_Click(sender As Object, e As EventArgs) Handles btnKosong.Click
        clearForm()
    End Sub

    Private Sub btnTanggal_Click(sender As Object, e As EventArgs) Handles btnTanggal.Click
        Dim prd As New Periode

        If prd.ShowDialog() = DialogResult.OK Then
            If prd.rb1day.Checked Then
                loadTicket(prd.dpAwal.Value)
            Else
                loadTicket(prd.dpAwal.Value, prd.dpAkhir.Value)
            End If
        End If
        clearForm()

        prd.Dispose()
    End Sub

    Private Sub tbBruto_DoubleClick(sender As Object, e As EventArgs) Handles tbBruto.DoubleClick
        Dim lgn As New Login

        lgn.TextBox1.Text = ""
        lgn.TextBox2.Text = ""

        lgn.forManual = True
        lgn.uc = Me
        If lgn.ShowDialog() = DialogResult.OK Then
            tbBruto.ReadOnly = False
            tbTare.ReadOnly = False
            'tbNetto.ReadOnly = False
        End If

        lgn.Dispose()
    End Sub

    Private Sub tbBruto_TextChanged(sender As Object, e As EventArgs) Handles tbBruto.TextChanged, tbTare.TextChanged
        Try
            tbNetto.Text = Math.Round(Decimal.Parse(tbBruto.Text) - Decimal.Parse(tbTare.Text), 2)
        Catch
        End Try
    End Sub

    Private Sub ScaleControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
