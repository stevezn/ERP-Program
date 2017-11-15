Imports System.Reflection
Imports System.Data.SQLite
Imports System.IO
Imports System.ComponentModel
Imports System.Net.NetworkInformation
Imports System.Net.FtpClient
Imports System.Text
Imports System.Web.Script.Serialization
Imports System.Threading
Imports System.Drawing.Printing
Imports DB

Public Class TabbedParent

    Class netChecker
        Private dbcon As DBConn
        Private mform As Object

        Private Delegate Sub netNotify(state As Integer)

        Private pNotify As netNotify

        Public Sub New(ByRef MainWindow As Object, ByRef db As DBConn)
            mform = MainWindow
            dbcon = db

            pNotify = AddressOf MainWindow.netNotify
        End Sub

        Private Function netLatency(host As String) As Integer
            Dim ping As New Ping()
            Dim rep As PingReply = Nothing
            Try
                rep = ping.Send(host, 2000)
                Select Case rep.Status
                    Case IPStatus.Success
                        Return rep.RoundtripTime
                    Case IPStatus.TimedOut, IPStatus.TimeExceeded
                        Return -1
                    Case IPStatus.DestinationHostUnreachable, IPStatus.DestinationNetworkUnreachable, IPStatus.DestinationPortUnreachable
                        Return -2
                End Select
            Catch ex As Exception
            End Try
            Return -3
        End Function

        Public Sub netCheck()
            On Error Resume Next

            Dim args(0) As Object

            If mform.netChecking Then
                args(0) = -1
                mform.Invoke(pNotify, args)
                Exit Sub
            End If

            args(0) = -2
            mform.Invoke(pNotify, args)

            Dim latency As Integer = netLatency(dbcon.host)

            If dbcon.state() = ConnectionState.Open Then
                If latency < 0 Then
                    args(0) = -1
                    mform.Invoke(pNotify, args)
                    Exit Sub
                Else
                    args(0) = ConnectionState.Open
                    mform.Invoke(pNotify, args)
                    Exit Sub
                End If
            End If

            If dbcon.state() = ConnectionState.Closed Then
                If latency > 0 Then
                    args(0) = ConnectionState.Connecting
                    mform.Invoke(pNotify, args)
                    Exit Sub
                Else
                    args(0) = ConnectionState.Broken
                    mform.Invoke(pNotify, args)
                    Exit Sub
                End If
            End If
        End Sub

    End Class

    Public Class syncThread
        Private mcon As DBConn
        Private lcon As SQLiteConnection
        Private mainForm As Object
        Private station As String
        Private corp As String

        Dim thdId As Integer

        Dim numformat As String = "{0:#,0.##}"

        Private Delegate Sub progBarNotify(max As Integer, prog As Integer, txt As String)
        Private pNotify As progBarNotify

        Public Sub New(ByRef MainWindow As Object, mainDb As DBConn, ByRef localDb As SQLiteConnection, corp As String, Optional station As String = "")
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
            Dim ds As New DataSet
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

            If mcon.state = ConnectionState.Open Then

                Select Case table
                    Case "companies"
                        mcon.SQL("select company_code, company_name from companies")
                    Case "business_areas"
                        mcon.SQL("select company_code, business_code [area_code], business_area [area_name], is_company_wide [company_wide] from business_areas")
                    Case "users"
                        mcon.SQL("select username, name, password, role, case when del = 0 then 1 else 0 end [status] from users")
                    Case "menus"
                        mcon.SQL("select m.id, m.name, m.parent_id, md.dll, m.form from menus m left join modules md on m.module_id = md.id")
                    Case "privileges"
                        mcon.SQL("select id, role, username, menu_id, company_code, business_code [area_code], access_level from [privileges]")
                End Select

                mcon.fillData(ds)

                If ds.Tables.Count = 1 Then
                    args(0) = ds.Tables(0).Rows.Count
                    args(1) = 0

                    Select Case table
                        Case "companies"
                            args(2) = "Syncing Companies"
                            cmdStr = "REPLACE INTO companies VALUES(?, ?)"
                        Case "business_areas"
                            args(2) = "Syncing Business Areas"
                            cmdStr = "REPLACE INTO business_areas VALUES(?, ?, ?, ?)"
                        Case "users"
                            args(2) = "Syncing Users"
                            cmdStr = "REPLACE INTO users(username, name, password, role, super, status) VALUES(?, ?, ?, ?, ?, ?)"
                        Case "menus"
                            args(2) = "Syncing Menus"
                            cmdStr = "REPLACE INTO menus VALUES(?, ?, ?, ?, ?)"
                        Case "privileges"
                            args(2) = "Syncing Privileges"
                            cmdStr = "REPLACE INTO privileges VALUES(?, ?, ?, ?, ?, ?, ?)"
                    End Select


                    mainForm.Invoke(pNotify, args)
                    For Each r In ds.Tables(0).Rows
                        While mainForm.syncing(0) <> thdId
                            Thread.Sleep(100)
                            'wait for other sync process
                        End While

                        'insert row
                        cmd.CommandText = cmdStr

                        Select Case table
                            Case "companies"
                                cmd.Parameters.AddWithValue("company_code", r.Item("company_code"))
                                cmd.Parameters.AddWithValue("company_name", r.Item("company_name"))
                            Case "business_areas"
                                cmd.Parameters.AddWithValue("company_code", r.Item("company_code"))
                                cmd.Parameters.AddWithValue("area_code", r.Item("area_code"))
                                cmd.Parameters.AddWithValue("area_name", r.Item("area_name"))
                                cmd.Parameters.AddWithValue("company_wide", r.Item("company_wide"))
                            Case "users"
                                cmd.Parameters.AddWithValue("username", r.Item("username"))
                                cmd.Parameters.AddWithValue("name", r.Item("name"))
                                cmd.Parameters.AddWithValue("password", r.Item("password"))
                                cmd.Parameters.AddWithValue("role", r.Item("role"))
                                cmd.Parameters.AddWithValue("super", 0)
                                cmd.Parameters.AddWithValue("status", r.Item("status"))
                            Case "menus"
                                cmd.Parameters.AddWithValue("id", r.Item("id"))
                                cmd.Parameters.AddWithValue("name", r.Item("name"))
                                cmd.Parameters.AddWithValue("parent_id", r.Item("parent_id"))
                                cmd.Parameters.AddWithValue("dll", r.Item("dll"))
                                cmd.Parameters.AddWithValue("form", r.Item("form"))
                            Case "privileges"
                                cmd.Parameters.AddWithValue("id", r.Item("id"))
                                cmd.Parameters.AddWithValue("role", r.Item("role"))
                                cmd.Parameters.AddWithValue("username", r.Item("username"))
                                cmd.Parameters.AddWithValue("menu_id", r.Item("menu_id"))
                                cmd.Parameters.AddWithValue("company_code", r.Item("company_code"))
                                cmd.Parameters.AddWithValue("area_code", r.Item("area_code"))
                                cmd.Parameters.AddWithValue("access_level", r.Item("access_level"))
                        End Select

                        cmd.ExecuteNonQuery()

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

            ds.Dispose()

            mainForm.syncing.Remove(thdId)
            cmd.Dispose()
        End Sub

        Private Sub pushTransferItems(doc_number As String)
            Dim cmd3 As SQLiteCommand = lcon.CreateCommand()
            cmd3.CommandText = "SELECT * FROM transfer_detail_items WHERE trans_doc_number = ?"
            cmd3.Parameters.AddWithValue("trans_doc_number", doc_number)
            Dim adp2 As New SQLiteDataAdapter(cmd3)
            Dim ds2 As New DataSet
            adp2.Fill(ds2)

            For Each t2 As DataTable In ds2.Tables
                For Each r2 As DataRow In t2.Rows
                    mcon.SQL("INSERT INTO 
                                    transfer_detail_items(trans_doc_number, anyaman, denier, lebar, panjang, warna, cetakan, opp,
                                        qty, item_code, item_name, uom, netto, src_item_number)
                                    VALUES(@tdn, @a, @d, @l, @p, @w, @c, @opp, @qty, @ic, @in, @uom, @netto, @src)")
                    mcon.addParameter("@tdn", r2.Item("trans_doc_number"))
                    mcon.addParameter("@a", r2.Item("anyaman"))
                    mcon.addParameter("@d", r2.Item("denier"))
                    mcon.addParameter("@l", r2.Item("lebar"))
                    mcon.addParameter("@p", r2.Item("panjang"))
                    mcon.addParameter("@w", r2.Item("warna"))
                    mcon.addParameter("@c", r2.Item("cetakan"))
                    mcon.addParameter("@opp", r2.Item("opp"))
                    mcon.addParameter("@qty", r2.Item("qty"))
                    mcon.addParameter("@ic", r2.Item("item_code"))
                    mcon.addParameter("@in", r2.Item("item_name"))
                    mcon.addParameter("@uom", r2.Item("uom"))
                    mcon.addParameter("@netto", Decimal.Parse("0" & r2.Item("netto")))
                    mcon.addParameter("@src", r2.Item("src_item_number"))
                    Try
                        mcon.execute()
                    Catch ex3 As Exception
                        Debug.WriteLine(ex3.Message)
                    End Try
                Next
            Next

            ds2.Dispose()
            adp2.Dispose()
            cmd3.Dispose()
        End Sub

        Public Sub update(table As String)
            If mcon.state <> ConnectionState.Open Then
                mainForm.syncing.Remove(thdId)
                Exit Sub
            End If

            If station = "" Or station = "UNKNOWN STATION" Then
                mainForm.syncing.Remove(thdId)
                Exit Sub
            End If

            While mainForm.syncing(0) <> thdId
                Thread.Sleep(100)
                'wait for other sync process
            End While

            Dim args(2) As Object
            Dim cmd As SQLiteCommand = lcon.CreateCommand()
            Dim cmd2 As SQLiteCommand = lcon.CreateCommand()
            cmd.CommandText = "select * from " + table + " where synced = 0"
            Dim adp As New SQLiteDataAdapter(cmd)
            Dim ds As New DataSet
            adp.Fill(ds)

            For Each t As DataTable In ds.Tables
                args(0) = t.Rows.Count
                args(1) = 0

                Select Case table
                    Case "production_details"
                        args(2) = "Uploading Productions"
                    Case "transfer_details"
                        args(2) = "Uploading Transfers"
                End Select

                mainForm.Invoke(pNotify, args)
                For Each r As DataRow In t.Rows
                    While mainForm.syncing(0) <> thdId
                        Thread.Sleep(100)
                        'wait for other sync process
                    End While

                    Try
                        Select Case table
                            Case "production_details"
                                mcon.SQL("INSERT INTO 
                                            production_details(company_code, area_code, station_code, doc_number, date, shift, data, item_code, line_code,
                                                qty, creator, pic1, pic1code, pic2, pic2code, anyaman, denier, lebar, panjang, cetakan, warna, opp, void, void_by) 
                                            VALUES(@cc, @ac, @sc, @dn, @date, @s, @data, @ic, @lc, @qty, @c, @pic1, @pic1c, @pic2, @pic2c, @a, @d, @l, @p, @ctk, @w, @opp, @v, @vb)")
                                mcon.addParameter("@cc", r.Item("company_code"))
                                mcon.addParameter("@ac", r.Item("area_code"))
                                mcon.addParameter("@sc", r.Item("station_code"))
                                mcon.addParameter("@dn", r.Item("doc_number"))
                                mcon.addParameter("@date", Strings.Replace(r.Item("date"), ".", ":"))
                                mcon.addParameter("@s", r.Item("shift"))
                                mcon.addParameter("@data", r.Item("data"))
                                mcon.addParameter("@ic", r.Item("item_code"))
                                mcon.addParameter("@lc", r.Item("line_code"))
                                mcon.addParameter("@qty", r.Item("qty"))
                                mcon.addParameter("@c", r.Item("creator"))
                                mcon.addParameter("@pic1", r.Item("pic1"))
                                mcon.addParameter("@pic1c", r.Item("pic1code"))
                                mcon.addParameter("@pic2", r.Item("pic2"))
                                mcon.addParameter("@pic2c", r.Item("pic2code"))
                                mcon.addParameter("@a", r.Item("anyaman"))
                                mcon.addParameter("@d", r.Item("denier"))
                                mcon.addParameter("@l", r.Item("lebar"))
                                mcon.addParameter("@p", r.Item("panjang"))
                                mcon.addParameter("@ctk", r.Item("cetakan"))
                                mcon.addParameter("@w", r.Item("warna"))
                                mcon.addParameter("@opp", r.Item("opp"))
                                mcon.addParameter("@v", r.Item("void"))
                                mcon.addParameter("@vb", r.Item("void_by"))
                                mcon.execute()
                            Case "transfer_details"
                                mcon.SQL("INSERT INTO 
                                            transfer_details(company_code, area_code, station_code, doc_number, date, shift, from_line, to_line,
                                                operator, type, src_doc_number, void, void_by)
                                            VALUES(@cc, @ac, @sc, @dn, @date, @s, @fl, @tl, @o, @t, @src, @v, @vb)")
                                mcon.addParameter("@cc", r.Item("company_code"))
                                mcon.addParameter("@ac", r.Item("area_code"))
                                mcon.addParameter("@sc", r.Item("station_code"))
                                mcon.addParameter("@dn", r.Item("doc_number"))
                                mcon.addParameter("@date", Strings.Replace(r.Item("date"), ".", ":"))
                                mcon.addParameter("@s", r.Item("shift"))
                                mcon.addParameter("@fl", r.Item("from_line"))
                                mcon.addParameter("@tl", r.Item("to_line"))
                                mcon.addParameter("@o", r.Item("operator"))
                                mcon.addParameter("@t", r.Item("type"))
                                mcon.addParameter("@src", r.Item("src_doc_number"))
                                mcon.addParameter("@v", r.Item("void"))
                                mcon.addParameter("@vb", r.Item("void_by"))
                                mcon.execute()

                                pushTransferItems(r.Item("doc_number"))
                        End Select

                        cmd2.CommandText = "UPDATE " + table + " SET synced = 1 WHERE company_code = ? AND area_code = ? AND
                                            station_code = ? AND doc_number = ?"
                        cmd2.Parameters.AddWithValue("company_code", r.Item("company_code"))
                        cmd2.Parameters.AddWithValue("area_code", r.Item("area_code"))
                        cmd2.Parameters.AddWithValue("station_code", r.Item("station_code"))
                        cmd2.Parameters.AddWithValue("doc_number", r.Item("doc_number"))
                        cmd2.ExecuteNonQuery()
                    Catch ex As Exception
                        Debug.WriteLine(ex.Message)

                        If ex.Message.IndexOf("duplicate") > 0 Then
                            Try            'for when header n detail tables does not sync properly
                                If table = "transfer_details" Then
                                    pushTransferItems(r.Item("doc_number"))
                                End If
                            Catch ex2 As Exception

                            End Try        'for when header n detail tables does not sync properly

                            cmd2.CommandText = "UPDATE " + table + " SET synced = 1 WHERE company_code = ? AND area_code = ? AND
                                            station_code = ? AND doc_number = ?"
                            cmd2.Parameters.AddWithValue("company_code", r.Item("company_code"))
                            cmd2.Parameters.AddWithValue("area_code", r.Item("area_code"))
                            cmd2.Parameters.AddWithValue("station_code", r.Item("station_code"))
                            cmd2.Parameters.AddWithValue("doc_number", r.Item("doc_number"))
                            cmd2.ExecuteNonQuery()
                        End If
                    End Try

                    args(1) = args(1) + 1
                    mainForm.Invoke(pNotify, args)
                    If args(1) Mod 100 = 0 And mainForm.syncing.Count > 1 Then
                        mainForm.syncing.Remove(thdId)
                        Thread.Sleep(100)
                        mainForm.syncing.Add(thdId)
                    End If
                Next
            Next
            mainForm.syncing.Remove(thdId)
            cmd.Dispose()
            cmd2.Dispose()
        End Sub
    End Class

    Private Sub copyToLocal(table As String)
        Dim syncThd As New syncThread(Me, dbCon, localCon, corp)
        Dim thd As Thread = Nothing
        Select Case table
            Case "companies", "business_areas", "users", "menus", "privileges"
                thd = New Thread(Sub() syncThd.sync(table))
                thd.Start()
            Case Else
                syncThd.Dispose()
        End Select

    End Sub

    Public Sub netCheck()
        Dim nc As New netChecker(Me, dbCon)
        Dim thd As New Thread(AddressOf nc.netCheck)
        thd.IsBackground = True
        thd.Start()
    End Sub

    Public Sub netNotify(state As Integer)
        Select Case state
            Case -2, -3
                netChecking = True
            Case -1
                pingLostCount += 1
                netCheckInterval = 1
                netChecking = False
            Case ConnectionState.Open
                pingLostCount = 0
                netCheckInterval = 1
                netChecking = False
            Case ConnectionState.Connecting
                reconnectCount += 1
                netCheckInterval = 1
                netChecking = False
            Case ConnectionState.Broken
                reconnectCount = 0
                netCheckInterval = 60
                netChecking = False
        End Select

        If pingLostCount >= 20 Then
            dbCon.close()
            pingLostCount = 0
        End If

        If reconnectCount >= 20 Then
            dbCon.open()
            reconnectCount = 0
        End If
    End Sub

    Private Modules As New Dictionary(Of String, List(Of Object))
    Private toUpdate As New Dictionary(Of String, String)
    Private menus As New Dictionary(Of Integer, TreeNode)

    Private doClose As Boolean = True

    Public dsPrint As DataSet
    Public dicPrint As List(Of Dictionary(Of String, Object))
    Public reportPrint As String = ""
    Public papersize As PaperSize

    Public privs As New Dictionary(Of String, Integer)

    Public dbCon As DBConn
    Public user As String
    Public role As String
    Public corp As String
    Public area As String

    Public injectAscend As Boolean = False
    Public ascendConStr As String = ""

    'Public errorStack As New List(Of String)

    Public configCon As SQLiteConnection
    Public localCon As SQLiteConnection

    Public ftp As FtpClient
    Private updateRequireRestart As Boolean = False

    Private progCtr As Integer = 0
    Public syncing As New List(Of Integer)

    Private pingLostCount As Integer = 0
    Private reconnectCount As Integer = 0
    Private netCheckInterval As Integer = 1
    Public netChecking As Boolean = False

    Public copy_profiles As Boolean = False

    Private tickCount As Integer = 0

    Public Sub progBarNotify(max As Integer, prog As Integer, txt As String)
        If (prog = 0 And (max > 0 Or max = -1)) Or (prog > 0 And spbUpdate.Visible = False And (max > 0 Or max = -1)) Or (spbUpdate.Maximum <> max And spbUpdate.Visible = True And (max > 0 Or max = -1)) Then
            spbUpdate.Text = txt
            spbUpdate.Visible = True
            slblSync.Text = txt
            slblSync.Visible = True
            If max = -1 Then
                spbUpdate.Maximum = Integer.MaxValue
                spbUpdate.Style = ProgressBarStyle.Marquee
            Else
                spbUpdate.Maximum = max
                spbUpdate.Style = ProgressBarStyle.Continuous
            End If
            spbUpdate.Value = prog
        ElseIf prog = max Then
            spbUpdate.Maximum = Integer.MaxValue
            spbUpdate.Style = ProgressBarStyle.Continuous
            spbUpdate.Visible = False
            slblSync.Text = ""
            slblSync.Visible = False
        Else
            If Not spbUpdate.Visible Then
                spbUpdate.Visible = True
            End If
            spbUpdate.Value = prog
        End If
    End Sub


    Public Sub progBarNotifyStart()
        progCtr = 0
        spbUpdate.Visible = True
        spbUpdate.Value = 0
        spbUpdate.Style = ProgressBarStyle.Marquee
        spbUpdate.Text = "Loading Items"
    End Sub

    Public Sub progBarNotifyRun()
        spbUpdate.Value = (spbUpdate.Value + 1) Mod 100
    End Sub

    Public Sub progBarNotifyEnd()
        spbUpdate.Text = "Update Available"
        spbUpdate.Style = ProgressBarStyle.Continuous
        spbUpdate.Visible = False
    End Sub

    Private Function ftpreadfile(filename As String) As Byte()
        Dim data As Byte(), buffer(10240) As Byte
        Dim fs As FtpDataStream
        Dim fsize As Long, read As Long, readcumul As Long

        fs = ftp.OpenRead(filename)
        fsize = ftp.GetFileSize(filename)
        ReDim data(fsize - 1)
        readcumul = 0
        Do
            read = fs.Read(buffer, 0, 10240)
            Array.Copy(buffer, 0, data, readcumul, read)
            readcumul += read
        Loop While read > 0
        fs.Close()

        Return data
    End Function

    Private Function netLatency(host As String) As Integer
        Dim ping As New Ping()
        Dim rep As PingReply = Nothing
        Try
            rep = ping.Send(host, 2000)
            Select Case rep.Status
                Case IPStatus.Success
                    Return rep.RoundtripTime
                Case IPStatus.TimedOut, IPStatus.TimeExceeded
                    Return -1
                Case IPStatus.DestinationHostUnreachable, IPStatus.DestinationNetworkUnreachable, IPStatus.DestinationPortUnreachable
                    Return -2
            End Select
        Catch ex As Exception
        End Try
        Return -3
    End Function

    'Public Sub showErrors()
    '    If errorStack.Count > 0 Then
    '        Dim str As String = ""
    '        For Each Er In errorStack
    '            str = str + If(str <> "", vbCrLf, "") + Er
    '        Next
    '        MsgBox(str)
    '        errorStack.Clear()
    '    End If
    'End Sub

    Private Sub LoadModule(dll As String, form As String, title As String)
        Dim oAssembly As Assembly
        Dim tp As TabPage
        Dim oControl As Object
        Dim l As List(Of Object)

        Dim b As Byte()

        Try
            If Not Modules.ContainsKey(form) Then
                If Not File.Exists("modules\" & dll & ".dll") Then
                    MsgBox("Module dll file is missing." + vbCrLf + "(TP-LM-01)")
                    Exit Sub
                End If

                b = File.ReadAllBytes("modules\" & dll & ".dll")
                oAssembly = Assembly.Load(b)

                oControl = oAssembly.CreateInstance(dll & "." & form)

                If Not oControl.canOffline Then
                    If dbCon.state() <> ConnectionState.Open Then
                        oControl.Dispose()
                        MsgBox("Network is not stable, it is better to wait until stabilized")
                        Exit Sub
                    End If
                End If

                oControl.dbCon = dbCon
                oControl.user = user
                oControl.role = role
                oControl.corp = corp
                oControl.area = area
                oControl.setPrivs(privs)

                If injectAscend Then
                    Try
                        oControl.injectAscend = True
                        oControl.ascendConStr = ascendConStr
                    Catch ex As Exception
                    End Try
                End If

                tcMain.TabPages.Add(title)
                tp = tcMain.TabPages(tcMain.TabPages.Count - 1)
                tp.Name = "dll" & dll & "_" & "form" & form
                tp.Controls.Add(oControl)
                DirectCast(oControl, UserControl).Dock = DockStyle.Fill

                l = New List(Of Object)
                l.Add(oControl)
                l.Add(tp)

                Modules.Add(form, l)
            Else
                oControl = Modules(form)(0)
                tp = Modules(form)(1)
            End If

            tcMain.SelectedTab = tp

            loadModuleMenu(oControl)
        Catch ex As Exception
            MsgBox(ex.Message + "." + vbCrLf + "(TP-LM-02)")
        End Try
    End Sub

    Private Sub unloadModule(form As String, doMenuUnload As Boolean)
        If IsNothing(form) Then Exit Sub
        If Modules.ContainsKey(form) Then
            If doMenuUnload Then unloadModuleMenu()
            Modules(form)(0).Dispose()
            Modules.Remove(form)
        End If
    End Sub

    Public Sub refreshMenuState(t As Object)
        If t.ContainsKey("New") Then
            tsbNew.Visible = t("New").visible
            tsbNew.Enabled = t("New").enabled
        Else
            tsbNew.Visible = False
            tsbNew.Enabled = False
        End If

        If t.ContainsKey("Edit") Then
            tsbEdit.Visible = t("Edit").visible
            tsbEdit.Enabled = t("Edit").enabled
        Else
            tsbEdit.Visible = False
            tsbEdit.Enabled = False
        End If

        If t.ContainsKey("Delete") Then
            tsbDelete.Visible = t("Delete").visible
            tsbDelete.Enabled = t("Delete").enabled
        Else
            tsbDelete.Visible = False
            tsbDelete.Enabled = False
        End If

        If t.ContainsKey("Save") Then
            tsbSave.Visible = t("Save").visible
            tsbSave.Enabled = t("Save").enabled
        Else
            tsbSave.Visible = False
            tsbSave.Enabled = False
        End If

        If t.ContainsKey("Print") Then
            tsbPrint.Visible = t("Print").visible
            tsbPrint.Enabled = t("Print").enabled
        Else
            tsbPrint.Visible = False
            tsbPrint.Enabled = False
        End If
    End Sub

    Private Sub loadModuleMenu(m As Object)
        miMenu.DropDownItems.Clear()
        For Each tm In m.toolMenu
            miMenu.DropDownItems.Add(tm.value)
        Next

        If miMenu.DropDownItems.Count = 0 Then
            miMenu.Visible = False
        Else
            miMenu.Visible = True
        End If

        refreshMenuState(m.toolButton)
    End Sub

    Private Sub unloadModuleMenu()
        miMenu.DropDownItems.Clear()
        miMenu.Visible = False

        tsbNew.Visible = False
        tsbNew.Enabled = False

        tsbEdit.Visible = False
        tsbEdit.Enabled = False

        tsbDelete.Visible = False
        tsbDelete.Enabled = False

        tsbSave.Visible = False
        tsbSave.Enabled = False

        tsbPrint.Visible = False
        tsbPrint.Enabled = False

    End Sub

    Private Sub ModuleClick(Sender As Object, e As EventArgs)
        Dim dllform() As String
        dllform = Split(Sender.Name, "_")

        LoadModule(Mid(dllform(0), 4), Mid(dllform(1), 5), Sender.Text)
    End Sub

    Private Sub tvMenu_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvMenu.NodeMouseClick
        Dim dllform() As String
        dllform = Split(e.Node.Name, "_")
        If dllform.Count = 2 Then
            LoadModule(Mid(dllform(0), 4), Mid(dllform(1), 5), e.Node.Text)
        End If
    End Sub

    Private Sub loadMenu(Optional local As Boolean = False)
        Dim ds As New DataSet
        Dim tn As TreeNode

        If local Then
            If localCon.State = ConnectionState.Open Then
                Dim cmd As SQLiteCommand = localCon.CreateCommand
                cmd.CommandText = "SELECT m.id, m.`name` `label`, max(p.access_level) access_level, parent_id, dll, m.form 
                        FROM privileges p JOIN menus m ON p.menu_id = m.id
                        WHERE p.company_code = ? AND (p.role = ? OR p.username = ?) 
                        GROUP BY m.id, m.`name`, parent_id, dll, m.form
                        ORDER BY CASE WHEN parent_id IS NULL then 0 else 1 END, m.`name`"

                cmd.Parameters.AddWithValue("company_code", corp)
                cmd.Parameters.AddWithValue("role", role)
                cmd.Parameters.AddWithValue("username", user)

                Dim adp As New SQLiteDataAdapter(cmd)
                adp.Fill(ds)
                adp.Dispose()
                cmd.Dispose()
            End If
        Else
            dbCon.SQL("SELECT m.id, m.[name] [label], max(p.access_level) access_level, parent_id, dll, m.form 
                        FROM privileges p JOIN menus m ON p.menu_id = m.id LEFT JOIN modules md ON m.module_id = md.id
                        WHERE p.company_code = @c AND (p.role = @r OR p.username = @u) 
                        GROUP BY m.id, m.[name], module_id, parent_id, dll, m.form
                        ORDER BY CASE WHEN parent_id IS NULL then 0 else 1 END, m.[name]")

            dbCon.addParameter("@c", corp)
            dbCon.addParameter("@r", role)
            dbCon.addParameter("@u", user)

            ds = dbCon.getData()
        End If

        If ds.Tables.Count = 1 Then
            For Each d In ds.Tables(0).Rows
                If Not IsDBNull(d.Item("parent_id")) Then
                    If menus.ContainsKey(d.Item("parent_id")) Then
                        tn = menus(d.Item("parent_id")).Nodes.Add(d.Item("label"))
                    Else
                        Continue For
                    End If
                Else
                    tn = tvMenu.Nodes.Add(d.Item("label"))
                End If

                If Not IsDBNull(d.Item("dll")) Then
                    privs.Add(d.Item("form"), d.Item("access_level"))
                    tn.Name = "dll" & Trim(d.Item("dll")) & "_" & "form" & Trim(d.Item("form"))
                End If

                menus.Add(d.Item("id"), tn)
            Next
        End If

        tvMenu.ExpandAll()

        ds.Dispose()
    End Sub

    Private Sub unloadMenu()
        privs.Clear()
        ModuleMenu.DropDownItems.Clear()
        tvMenu.Nodes.Clear()
        menus.Clear()

        For Each page In tcMain.TabPages
            Dim b As Button = tcMain.CloseButtonOfTabPage(page)
            b.PerformClick()
        Next

        For Each m In menus

        Next
    End Sub

    Public Sub doPrint()
        If File.Exists(Directory.GetCurrentDirectory + "\reports\" + reportPrint) Then
            Dim b As Byte()
            Dim oAssembly As Assembly
            Dim oObject As Object = Nothing

            b = File.ReadAllBytes("modules\ExcelReportModule.dll")
            oAssembly = Assembly.Load(b)

            Try
                oObject = oAssembly.CreateInstance("ExcelReportModule.ReportMain")
                If IsNothing(dsPrint) And Not IsNothing(dicPrint) Then
                    oObject.repdata = dicPrint
                Else
                    oObject.ds = dsPrint
                End If
                If papersize IsNot Nothing Then
                    oObject.papersize = papersize
                End If
                oObject.openTemplate(Directory.GetCurrentDirectory + "\reports\" + reportPrint)
                If oObject.templateExists() Then
                    oObject.generateReport()
                    oObject.print()
                Else
                    MsgBox("Template file does not have template definitions." + vbCrLf + "(TP-DP-01)")
                End If
                reportPrint = ""
                oObject.Dispose()
            Catch ex As Exception
                reportPrint = ""
                If Not IsNothing(oObject) Then oObject.Dispose()
                MsgBox(ex.Message + vbCrLf + "(TP-DP-02)")
            End Try
        Else
            reportPrint = ""
            MsgBox("Report file is missing." + vbCrLf + "(TP-DP-03)")
        End If
    End Sub

    Public Sub doPreview()
        If File.Exists(Directory.GetCurrentDirectory + "\reports\" + reportPrint) Then
            Dim b As Byte()
            Dim oAssembly As Assembly
            Dim oObject As Object = Nothing

            b = File.ReadAllBytes("modules\ExcelReportModule.dll")
            oAssembly = Assembly.Load(b)

            Try
                oObject = oAssembly.CreateInstance("ExcelReportModule.ReportMain")
                If IsNothing(dsPrint) And Not IsNothing(dicPrint) Then
                    oObject.repdata = dicPrint
                Else
                    oObject.ds = dsPrint
                End If
                If papersize IsNot Nothing Then
                    oObject.papersize = papersize
                End If
                oObject.openTemplate(Directory.GetCurrentDirectory + "\reports\" + reportPrint)
                If oObject.templateExists() Then
                    oObject.generateReport()
                    oObject.preview()
                Else
                    MsgBox("Template file does not have template definitions." + vbCrLf + "(TP-DPV-01)")
                End If
                reportPrint = ""
                oObject.SaveReport()
                oObject.Dispose()
            Catch ex As Exception

                reportPrint = ""
                If Not IsNothing(oObject) Then oObject.Dispose()
                MsgBox(ex.Message + "." + vbCrLf + "(TP-DPV-02)")
            End Try
        Else
            reportPrint = ""
            MsgBox("Report file is missing." + vbCrLf + "(TP-DPV-03)")
        End If
    End Sub

    Public Sub doSaveReport()
        If File.Exists(Directory.GetCurrentDirectory + "\reports\" + reportPrint) Then
            Dim b As Byte()
            Dim oAssembly As Assembly
            Dim oObject As Object = Nothing
            Dim fname As String

            b = File.ReadAllBytes("modules\ExcelReportModule.dll")
            oAssembly = Assembly.Load(b)

            Try
                oObject = oAssembly.CreateInstance("ExcelReportModule.ReportMain")
                If IsNothing(dsPrint) And Not IsNothing(dicPrint) Then
                    oObject.repdata = dicPrint
                Else
                    oObject.ds = dsPrint
                End If
                If papersize IsNot Nothing Then
                    oObject.papersize = papersize
                End If
                oObject.openTemplate(Directory.GetCurrentDirectory + "\reports\" + reportPrint)
                If oObject.templateExists() Then
                    oObject.generateReport()
                Else
                    MsgBox("Template file does not have template definitions." + vbCrLf + "(TP-DPV-01)")
                End If
                reportPrint = ""
                fname = oObject.SaveReport()
                oObject.Dispose()
                If fname <> "" Then
                    Process.Start(fname)
                End If
            Catch ex As Exception
                reportPrint = ""
                If Not IsNothing(oObject) Then oObject.Dispose()
                MsgBox(ex.Message + "." + vbCrLf + "(TP-DPV-02)")
            End Try
        Else
            reportPrint = ""
            MsgBox("Report file is missing." + vbCrLf + "(TP-DPV-03)")
        End If
    End Sub

    Private Sub ChildNew()
        Dim c As Object

        c = (From m In Modules Where m.Value(1) Is tcMain.SelectedTab Select m.Value(0)).FirstOrDefault
        Try
            c.NewForm()
        Catch ex As Exception
            MsgBox(ex.Message + "." + vbCrLf + "(TP-CN-01)")
        End Try
    End Sub

    Private Sub ChildReload()
        Dim c As Object

        c = (From m In Modules Where m.Value(1) Is tcMain.SelectedTab Select m.Value(0)).FirstOrDefault
        Try
            c.Reload()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ChildEdit()
        Dim c As Object

        c = (From m In Modules Where m.Value(1) Is tcMain.SelectedTab Select m.Value(0)).FirstOrDefault
        Try
            c.EditForm()
        Catch ex As Exception
            MsgBox(ex.Message + "." + vbCrLf + "(TP-CE-01)")
        End Try
    End Sub

    Private Sub ChildSave()
        Dim c As Object

        c = (From m In Modules Where m.Value(1) Is tcMain.SelectedTab Select m.Value(0)).FirstOrDefault
        Try
            c.Save()
        Catch ex As Exception
            MsgBox(ex.Message + "." + vbCrLf + "(TP-CS-01)")
        End Try
    End Sub

    Private Sub ChildDelete()
        Dim c As Object

        c = (From m In Modules Where m.Value(1) Is tcMain.SelectedTab Select m.Value(0)).FirstOrDefault
        Try
            c.Delete()
        Catch ex As Exception
            MsgBox(ex.Message + "." + vbCrLf + "(TP-CD-01)")
        End Try
    End Sub

    Private Sub ChildPrint()
        Dim c As Object

        If Not File.Exists("modules\ExcelReportModule.dll") Then
            MsgBox("Excel report module dll file is missing." + vbCrLf + "(TP-CP-01)")
            Exit Sub
        End If

        c = (From m In Modules Where m.Value(1) Is tcMain.SelectedTab Select m.Value(0)).FirstOrDefault

        If Not IsNothing(c) Then
            Try
                c.Print()
            Catch ex As Exception
                reportPrint = ""
                MsgBox(ex.Message)
            End Try
        Else
            reportPrint = ""
            MsgBox("No module opened." + vbCrLf + "(TP-CP-02)")
        End If
    End Sub

    Private Function ftpToUpdate(ftpdir As String, localdir As String, updateType As String) As Boolean
        ftpToUpdate = False
        If ftp.DirectoryExists(ftpdir) Then
            Dim dll() As FtpListItem = ftp.GetListing(ftpdir)
            For Each d As FtpListItem In dll
                'Dim m = File.GetLastWriteTime("modules\" + d.Name)
                If File.Exists(localdir + "\" + d.Name) Then
                    Dim attr As FileAttributes = File.GetAttributes(localdir + "\" + d.Name)
                    If attr And FileAttributes.ReparsePoint Then
                        'Debug.WriteLine("local file " + d.Name + " is symbolic link")
                    Else
                        Dim info As FileInfo = My.Computer.FileSystem.GetFileInfo(localdir + "\" + d.Name)

                        'If d.Modified > info.LastWriteTime Or d.Size <> info.Length Then
                        If d.Modified > info.LastWriteTime Then
                            toUpdate.Add(d.Name, updateType)
                            ftpToUpdate = True
                        End If
                        'Debug.WriteLine("lm:" + info.LastWriteTime + ", ls:" + info.Length.ToString())
                        'Debug.WriteLine("f:" + d.FullName + ", n:" + d.Name + ", m:" + d.Modified + ", s:" + d.Size.ToString())
                    End If
                Else
                    toUpdate.Add(d.Name, updateType)
                    ftpToUpdate = True
                End If
            Next
        End If
    End Function

    Private Function CheckUpdate(Optional closeConn As Boolean = True) As Integer
        Dim json As New JavaScriptSerializer
        Dim updates As List(Of Dictionary(Of String, Object))
        Dim localfiles As List(Of Dictionary(Of String, Object))
        Dim data As Byte()
        Dim str As String
        Dim r As Dictionary(Of String, Object)

        updateRequireRestart = False

        If File.Exists("updates\update.json") Then
            data = File.ReadAllBytes("updates\update.json")
            str = Encoding.ASCII.GetString(data)
            If str = "" Then str = "{}"
            localfiles = json.Deserialize(Of List(Of Dictionary(Of String, Object)))(str)
        Else
            localfiles = New List(Of Dictionary(Of String, Object))
        End If

        Try
            If Not ftp.IsConnected Then
                ftp.Connect()
            End If

            toUpdate.Clear()

            If ftp.DirectoryExists("repo") Then

                ftpToUpdate("repo/dll", "modules", "dll")
                ftpToUpdate("repo/report", "reports", "report")
                ftpToUpdate("repo/label", "labels", "label")
                If ftpToUpdate("repo/app", ".", "app") Then
                    updateRequireRestart = True
                End If

                If Not ftp.FileExists("repo/update.json") Then
                    updates = New List(Of Dictionary(Of String, Object))
                Else
                    data = ftpreadfile("repo/update.json")
                    str = Encoding.ASCII.GetString(data)
                    Try
                        updates = json.Deserialize(Of List(Of Dictionary(Of String, Object)))(str)
                    Catch ex As Exception
                        updates = New List(Of Dictionary(Of String, Object))
                    End Try
                End If

                For Each u In updates
                    r = (From lf In localfiles Where lf("file") = u("file") And lf("type") = u("type") Select lf).FirstOrDefault
                    'If IsNothing(r) And u("type") <> "app" And u("type") <> "database" Then
                    '    toUpdate.Add(u("file"), u("type"))
                    'ElseIf u("type") <> "app" And u("type") <> "database" Then
                    '    If (r("version") < u("version")) Then
                    '        toUpdate.Add(u("file"), u("type"))
                    '    ElseIf u("type") = "dll" And Not File.Exists("modules\" + u("file")) Then
                    '        toUpdate.Add(u("file"), u("type"))
                    '    ElseIf u("type") = "report" And Not File.Exists("reports\" + u("file")) Then
                    '        toUpdate.Add(u("file"), u("type"))
                    '    ElseIf u("type") = "label" And Not File.Exists("labels\" + u("file")) Then
                    '        toUpdate.Add(u("file"), u("type"))
                    '    End If
                    'ElseIf u("type") = "database" Then
                    If u("type") = "database" Then
                        Dim update As Boolean = False
                        If r Is Nothing Then
                            update = True
                        ElseIf (r("version") < u("version")) Then
                            update = True
                        End If
                        If update Then
                            Try
                                Dim cmd As SQLiteCommand = localCon.CreateCommand()
                                cmd.CommandText = u("sql")
                                cmd.ExecuteNonQuery()
                                cmd.Dispose()
                            Catch ex As Exception
                            End Try
                        End If
                        'Else
                        '    updateRequireRestart = True
                    End If
                Next
            End If

            If closeConn Then ftp.Disconnect()
        Catch ex As Exception
        End Try

        Return toUpdate.Count
    End Function

    Private Sub GetUpdate()
        Dim data As Byte()
        Dim src As String
        Dim dst As String
        Dim item As ListViewItem

        If updateRequireRestart Then
            doClose = True
            Application.Restart()
            Exit Sub
        End If

        Dim pf As New UpdateForm

        For Each u In toUpdate
            item = pf.ListView1.Items.Add(u.Key)
            item.SubItems.Add(u.Value)
            item.SubItems.Add("Pending")
            item.BackColor = Color.Tomato
            item.ForeColor = Color.White
        Next
        pf.ListView1.Columns(2).Width = -2

        pf.ProgressBar1.Maximum = toUpdate.Count
        pf.ProgressBar1.Value = 0
        pf.Show()

        spbUpdate.Visible = True
        spbUpdate.Value = 0
        spbUpdate.Maximum = toUpdate.Count

        Try
            'If toUpdate.Count = 0 Then
            '    CheckUpdate(False)
            'Else
            If Not ftp.IsConnected Then
                ftp.Connect()
            End If

            Dim i As Integer = 0
            If ftp.DirectoryExists("repo") Then
                For Each u In toUpdate
                    If u.Value = "dll" Then
                        src = "repo/dll/" + u.Key
                        dst = "modules\" + u.Key
                    ElseIf u.Value = "label" Then
                        src = "repo/label/" + u.Key
                        dst = "labels\" + u.Key
                    Else
                        src = "repo/report/" + u.Key
                        dst = "reports\" + u.Key
                    End If
                    If ftp.FileExists(src) Then
                        data = ftpreadfile(src)
                        File.WriteAllBytes("updates\" + u.Key, data)
                        Try
                            File.Delete(dst)
                            File.Move("updates\" + u.Key, dst)
                        Catch ex As Exception
                        End Try
                        spbUpdate.Value += 1

                        pf.ListView1.Items(i).SubItems(2).Text = "Done"
                        pf.ListView1.Items(i).BackColor = Color.ForestGreen
                        If i < pf.ListView1.Items.Count - 1 Then
                            pf.ListView1.Items(i + 1).EnsureVisible()
                        End If
                        i += 1
                        pf.ProgressBar1.Value += 1
                    End If
                Next
            End If

            data = ftpreadfile("repo/update.json")
            File.WriteAllBytes("updates\update.json", data)

            ftp.Disconnect()
        Catch ex As Exception
        End Try

        pf.Close()

        spbUpdate.Visible = False
    End Sub

    Private Sub tcMain_CloseButtonClick(sender As Object, e As CancelEventArgs) Handles tcMain.CloseButtonClick
        Dim btn As Button = DirectCast(sender, Button)
        Dim tp As TabPage = tcMain.GetTabPage(btn)
        Dim doMenuUnload As Boolean = False
        Dim form As String = (From m In Modules Where m.Value(1) Is tp Select m.Key).FirstOrDefault
        doMenuUnload = If(tp Is tcMain.SelectedTab, True, False)
        unloadModule(form, doMenuUnload)
    End Sub

    Private Sub TabbedParent_Load(sender As Object, e As EventArgs) Handles Me.Load
        KeyPreview = True

        Dim ver As String = Assembly.GetExecutingAssembly().GetName().Version.ToString()
        slblVersion.Text = "Version: " + ver
    End Sub

    Private Sub TabbedParent_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        netTick.Enabled = False
        Try
            If ftp.IsConnected Then
                ftp.Disconnect()
            End If
        Catch ex As Exception
        End Try
        If doClose Then
            End
        End If
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        ChildSave()
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles tsbPrint.Click
        ChildPrint()
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        ChildNew()
    End Sub

    Private Sub miChangeCorp_Click(sender As Object, e As EventArgs) Handles miChangeCorp.Click
        Dim ok As Boolean = False
        If tcMain.TabCount > 0 Then
            Dim result As Integer = MessageBox.Show("There are open tabs, do you want to close it? Any unsaved data will be lost if you proceed", "Are you sure?", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                ok = True
            End If
        Else
            ok = True
        End If
        If ok Then
            If CompanyForm.ShowDialog() = DialogResult.OK Then
                Dim ctmp As String()
                ctmp = CompanyForm.ComboBox1.Text.Split("/")
                corp = Trim(ctmp(0))
                area = Trim(ctmp(1))

                slblCorp.Text = corp
                unloadMenu()
                loadMenu()
            End If
        End If
    End Sub

    Private Sub slblCorp_Click(sender As Object, e As EventArgs) Handles slblCorp.Click
        miChangeCorp.PerformClick()
    End Sub

    Private Sub miLogout_Click(sender As Object, e As EventArgs) Handles miLogout.Click
        Dim ok As Boolean = False
        If tcMain.TabCount > 0 Then
            Dim result As Integer = MessageBox.Show("There are open tabs, do you want to close it? Any unsaved data will be lost if you proceed", "Are you sure?", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                ok = True
            End If
        Else
            ok = True
        End If
        If ok Then
            doClose = False
            Owner.Show()
            netTick.Enabled = False
            CompanyForm.Button2.Visible = False
            CompanyForm.Button2.Enabled = False
            Close()
        End If
    End Sub

    Private Sub CheckForUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdateToolStripMenuItem.Click
        If CheckUpdate() > 0 Then
            GetUpdate()
            'MsgBox("Updates available")
            spbUpdate.Visible = False
            slblUpdate.Visible = False
            MsgBox("Update Finish." + vbCrLf + "(TP-CFUC-01)")
        Else
            MsgBox("No update available." + vbCrLf + "(TP-CFUC-02)")
        End If
    End Sub

    Private Sub tcMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcMain.SelectedIndexChanged
    End Sub

    Private Sub conStateChange(sender As Object, args As StateChangeEventArgs)
        Select Case args.CurrentState
            Case ConnectionState.Broken
                slblNetCheck.Text = "Connection: Broken"
            Case ConnectionState.Closed
                slblNetCheck.Text = "Connection: Closed"
            Case ConnectionState.Connecting
                slblNetCheck.Text = "Connection: Connecting"
            Case Else
                slblNetCheck.Text = "Connection: Open"
        End Select
    End Sub

    Private Sub uploadData(table As String, station As String)
        Dim thdObj As New syncThread(Me, dbCon, localCon, corp, station)
        Dim thd As New Thread(Sub() thdObj.update(table))
        thd.Start()
    End Sub

    Private Sub netcheck_Tick(sender As Object, e As EventArgs) Handles netTick.Tick
        netCheck()

        If File.Exists("station.txt") Then
            Dim station As String = UCase(File.ReadAllText("station.txt"))
            If dbCon.state = ConnectionState.Open Then
                If tickCount >= 120 Then
                    tickCount = 0
                    uploadData("production_details", station)
                    uploadData("transfer_details", station)
                Else
                    tickCount += 1
                End If
            End If
        End If
    End Sub

    Private Sub tcMain_Selected(sender As Object, e As TabControlEventArgs) Handles tcMain.Selected
        Dim c As Object
        Dim tn As TreeNode = Nothing

        c = (From m In Modules Where m.Value(1) Is e.TabPage Select m.Value(0)).FirstOrDefault
        Try
            tn = (From m In menus Where m.Value.Name = e.TabPage.Name Select m.Value).FirstOrDefault
        Catch ex As Exception
        End Try

        If Not IsNothing(c) Then
            loadModuleMenu(c)
        Else
            unloadModuleMenu()
        End If

        If Not IsNothing(tn) Then
            tvMenu.SelectedNode = tn
        End If
    End Sub

    Private Sub tcMain_Deselected(sender As Object, e As TabControlEventArgs) Handles tcMain.Deselected
        tvMenu.SelectedNode = Nothing
    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        ChildEdit()
    End Sub

    Private Sub TabbedParent_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F5 Then
            ChildReload()
        End If
    End Sub

    Private Sub tsbDelete_Click(sender As Object, e As EventArgs) Handles tsbDelete.Click
        ChildDelete()
    End Sub

    Private Sub ToolStripDropDownButton1_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton1.Click
        SplitContainer1.Panel1Collapsed = Not SplitContainer1.Panel1Collapsed
        If SplitContainer1.Panel1Collapsed Then
            sender.Text = "»"
        Else
            sender.Text = "«"
        End If
    End Sub

    Private Sub thdTick_Tick(sender As Object, e As EventArgs) Handles thdTick.Tick
        Dim str As String = ""

        For Each thdId In syncing
            str = str + If(str = "", "", ", ") + thdId.ToString
        Next

        slblThreads.Text = String.Format("{0:#,0.##}", syncing.Count) + " [" + str + "]"
    End Sub

    Private Sub TabbedParent_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        netTick.Enabled = True

        dbCon.setOnStateChange(New StateChangeEventHandler(AddressOf conStateChange))

        If dbCon.state <> ConnectionState.Open And Not IsNothing(localCon) Then
            slblNetCheck.Text = "OFFLINE"
            loadMenu(True)
        Else
            loadMenu()

            If CheckUpdate() > 0 Then
                slblUpdate.Visible = True
            End If

            slblNetCheck.Text = "Connection: Open"

            If copy_profiles Then
                Dim cmd As SQLiteCommand = localCon.CreateCommand()
                Dim table As String

                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='companies'"
                table = cmd.ExecuteScalar()

                If table = "" Then
                    cmd.CommandText = "CREATE TABLE `companies` (
	                                        `company_code`	TEXT NOT NULL,
	                                        `company_name`	TEXT NOT NULL,
	                                        PRIMARY KEY(company_code)
                                        )"
                    cmd.ExecuteNonQuery()
                End If

                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='business_areas'"
                table = cmd.ExecuteScalar()

                If table = "" Then
                    cmd.CommandText = "CREATE TABLE `business_areas` (
	                                        `company_code`	TEXT NOT NULL,
	                                        `area_code`	TEXT NOT NULL,
	                                        `area_name`	TEXT NOT NULL,
	                                        `company_wide`	INTEGER NOT NULL DEFAULT 0,
	                                        PRIMARY KEY(company_code,area_code)
                                        )"
                    cmd.ExecuteNonQuery()
                End If

                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='menus'"
                table = cmd.ExecuteScalar()

                If table = "" Then
                    cmd.CommandText = "CREATE TABLE `menus` (
	                                        `id`	INTEGER,
	                                        `name`	TEXT,
	                                        `parent_id`	INTEGER,
	                                        `dll`	TEXT,
	                                        `form`	TEXT,
	                                        PRIMARY KEY(id)
                                        )"
                    cmd.ExecuteNonQuery()
                End If

                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='privileges'"
                table = cmd.ExecuteScalar()

                If table = "" Then
                    cmd.CommandText = "CREATE TABLE `privileges` (
	                                        `id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	                                        `role`	TEXT,
	                                        `username`	TEXT,
	                                        `menu_id`	INTEGER,
	                                        `company_code`	TEXT,
	                                        `area_code`	TEXT,
	                                        `access_level`	INTEGER
                                        )"
                    cmd.ExecuteNonQuery()
                End If

                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='users'"
                table = cmd.ExecuteScalar()

                If table = "" Then
                    cmd.CommandText = "CREATE TABLE `users` (
	                                    `username`	TEXT NOT NULL,
	                                    `password`	TEXT NOT NULL,
	                                    `super`	INTEGER NOT NULL DEFAULT 0,
	                                    `name`	TEXT NOT NULL,
	                                    `role`	TEXT NOT NULL,
	                                    `status`	INTEGER NOT NULL DEFAULT 1,
	                                    PRIMARY KEY(username)
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

                copyToLocal("companies")
                copyToLocal("business_areas")
                copyToLocal("users")
                copyToLocal("menus")
                copyToLocal("privileges")
            End If

            'dbCon.close()
            'dbCon.open()
        End If
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        ChangePassword.localCon = localCon
        ChangePassword.user = user

        If ChangePassword.ShowDialog() = DialogResult.OK Then
            Dim c As New Crypto.Crypto
            Dim cmd As SQLiteCommand = localCon.CreateCommand()
            Dim pwd As String = c.HashPassword(ChangePassword.txtPwd.Text)

            cmd.CommandText = "UPDATE users SET password = ? WHERE username = ?"
            cmd.Parameters.AddWithValue("password", pwd)
            cmd.Parameters.AddWithValue("username", user)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

            dbCon.SQL("UPDATE users SET password = @pwd WHERE username = @un")
            dbCon.addParameter("@pwd", pwd)
            dbCon.addParameter("@un", user)
            dbCon.execute()

            MsgBox("Password changed successfully")
        End If
    End Sub

    Private Sub MenuStrip_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip.ItemClicked

    End Sub
End Class