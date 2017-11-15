Imports System.Data.SQLite
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Net.FtpClient
Imports System.Text
Imports System.Web.Script.Serialization
Imports DB
Imports System.ComponentModel
Imports System.Reflection

Public Class MainForm

    Private configCon As SQLiteConnection
    Private dbCon As DBConn

    Public user As String
    Public role As String

    Private toUpdate As New Dictionary(Of String, String)
    Private ftp As FtpClient

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

        'updateRequireRestart = False

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
                    'updateRequireRestart = True
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

                Dim lcon As New SQLiteConnection("Data Source=localDB.sqlite;Version=3;New=False;Compress=True;")

                For Each u In updates
                    r = (From lf In localfiles Where lf("file") = u("file") And lf("type") = u("type") Select lf).FirstOrDefault
                    'If IsNothing(r) And u("type") <> "database" Then
                    '    toUpdate.Add(u("file"), u("type"))
                    'ElseIf u("type") <> "database" Then
                    '    If (r("version") < u("version")) Then
                    '        toUpdate.Add(u("file"), u("type"))
                    '    ElseIf u("type") = "dll" And Not File.Exists("modules\" + u("file")) Then
                    '        toUpdate.Add(u("file"), u("type"))
                    '    ElseIf u("type") = "report" And Not File.Exists("reports\" + u("file")) Then
                    '        toUpdate.Add(u("file"), u("type"))
                    '    ElseIf u("type") = "label" And Not File.Exists("labels\" + u("file")) Then
                    '        toUpdate.Add(u("file"), u("type"))
                    '    End If
                    'Else
                    If u("type") = "database" Then
                        Dim update As Boolean = False
                        If r Is Nothing Then
                            update = True
                        ElseIf (r("version") < u("version")) Then
                            update = True
                        End If
                        If update Then
                            Try
                                lcon.Open()
                                Dim cmd As SQLiteCommand = lcon.CreateCommand()
                                cmd.CommandText = u("sql")
                                cmd.ExecuteNonQuery()
                                cmd.Dispose()
                                lcon.Close()
                            Catch ex As Exception
                                lcon.Close()
                            End Try
                        End If
                    End If
                Next
            End If

            If closeConn Then ftp.Disconnect()

            'Dim dll As String() = Directory.GetFiles(".", "*.dll")

            'For Each f As String In dll
            '    f = f.Remove(0, 2)
            '    If File.Exists("modules\" + f) Then
            '        If File.GetLastWriteTime("modules\" + f) > File.GetLastWriteTime(f) Then
            '            File.Copy("modules\" + f, f, True)
            '        End If
            '    End If
            'Next
        Catch ex As Exception
        End Try

        If AppDomain.CurrentDomain.FriendlyName.IndexOf("vshost") > 0 Then
            Return 0
        Else
            Return toUpdate.Count
        End If
    End Function

    Private Sub GetUpdate()
        Dim data As Byte()
        Dim src As String
        Dim dst As String
        Dim item As ListViewItem

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
                    ElseIf u.Value = "report" Then
                        src = "repo/report/" + u.Key
                        dst = "reports\" + u.Key
                    ElseIf u.Value = "label" Then
                        src = "repo/label/" + u.Key
                        dst = "labels\" + u.Key
                    Else
                        src = "repo/app/" + u.Key
                        If Strings.Right(u.Key, 3) = "exe" Then
                            dst = "new-" + u.Key
                        Else
                            dst = u.Key
                        End If
                    End If
                    If ftp.FileExists(src) Then
                        data = ftpreadfile(src)
                        File.WriteAllBytes("updates\" + u.Key, data)
                        Try
                            File.Delete(dst)
                            File.Move("updates\" + u.Key, dst)
                        Catch ex As Exception
                        End Try

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

        'Dim dll As String() = Directory.GetFiles(".", "*.dll")

        'For Each f As String In dll
        '    f = f.Remove(0, 2)
        '    If File.Exists("modules\" + f) Then
        '        If File.GetLastWriteTime("modules\" + f) > File.GetLastWriteTime(f) Then
        '            File.Copy("modules\" + f, f, True)
        '        End If
        '    End If
        'Next

        If File.Exists("new-" + AppDomain.CurrentDomain.FriendlyName) Then
            Process.Start("new-" + AppDomain.CurrentDomain.FriendlyName)
            End
        End If
    End Sub

    Private Sub btnCreateCon_Click(sender As Object, e As EventArgs) Handles btnCreateCon.Click
        If Not Panel1.Visible Then
            txtName.Text = ""
            cbServer.Text = ""
            txtHost.Text = ""
            txtDb.Text = ""
            txtUser.Text = ""
            txtPwd.Text = ""
            chkAscendLogin.Checked = False
            chkAscendLogin.Enabled = False
            chkInjectAscend.Checked = False
            chkInjectAscend.Enabled = False
            tbAscendHost.Text = ""
            tbAscendDB.Text = ""
            tbAscendDBData.Text = ""
            tbAscendDBUser.Text = ""
            tbAscendDBPwd.Text = ""
            chkCopyProfiles.Checked = False

            Panel1.Visible = True
            btnLogin.Text = "Cancel"

            btnModifCon.Enabled = False
            btnDeleteCon.Enabled = False
            Exit Sub
        End If

        Dim command As SQLiteCommand

        Dim item As ListViewItem

        If txtName.Text = "" Or cbServer.Text = "" Or
            txtDb.Text = "" Or txtHost.Text = "" Or
            txtPwd.Text = "" Or txtUser.Text = "" Then
            MsgBox("All data must be filled")
            Exit Sub
        End If

        command = configCon.CreateCommand()
        command.CommandText = "insert into config(profile, server, host, db, user, pwd, ascend_login, inject_ascend, ascend_host, ascend_db, ascend_data_db, ascend_db_user, ascend_db_pass, copy_profiles_to_local) 
                                        values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
        command.Parameters.AddWithValue("profile", txtName.Text)
        command.Parameters.AddWithValue("server", cbServer.Text)
        command.Parameters.AddWithValue("host", txtHost.Text)
        command.Parameters.AddWithValue("db", txtDb.Text)
        command.Parameters.AddWithValue("user", txtUser.Text)
        command.Parameters.AddWithValue("pwd", txtPwd.Text)
        command.Parameters.AddWithValue("ascend_login", If(chkAscendLogin.Checked, 1, 0))
        command.Parameters.AddWithValue("inject_ascend", If(chkInjectAscend.Checked, 1, 0))
        command.Parameters.AddWithValue("ascend_host", tbAscendHost.Text)
        command.Parameters.AddWithValue("ascend_db", tbAscendDB.Text)
        command.Parameters.AddWithValue("ascend_data_db", tbAscendDBData.Text)
        command.Parameters.AddWithValue("ascend_db_user", tbAscendDBUser.Text)
        command.Parameters.AddWithValue("ascend_db_pass", tbAscendDBPwd.Text)
        command.Parameters.AddWithValue("copy_profiles_to_local", If(chkCopyProfiles.Checked, 1, 0))
        command.ExecuteNonQuery()

        command.CommandText = "select last_insert_rowid()"

        Dim id = command.ExecuteScalar()

        command.Dispose()

        item = ListView1.Items.Add(txtName.Text)
        item.SubItems.Add(cbServer.Text)
        item.SubItems.Add(txtHost.Text)
        item.SubItems.Add(txtDb.Text)
        item.SubItems.Add(txtUser.Text)
        item.SubItems.Add(txtPwd.Text)
        item.SubItems.Add(id)
        item.SubItems.Add(If(chkAscendLogin.Checked, 1, 0))
        item.SubItems.Add(tbAscendDB.Text)
        item.SubItems.Add(If(chkInjectAscend.Checked, 1, 0))
        item.SubItems.Add(tbAscendHost.Text)
        item.SubItems.Add(tbAscendDBUser.Text)
        item.SubItems.Add(tbAscendDBPwd.Text)
        item.SubItems.Add(tbAscendDBData.Text)
        item.SubItems.Add(If(chkCopyProfiles.Checked, 1, 0))

        Panel1.Visible = False
        btnLogin.Text = "Login"

        btnModifCon.Enabled = True
        btnDeleteCon.Enabled = True
    End Sub

    Private Sub btnModifCon_Click(sender As Object, e As EventArgs) Handles btnModifCon.Click
        Dim item As ListViewItem
        Dim command As SQLiteCommand

        If ListView1.SelectedItems.Count <> 1 Then
            MsgBox("Select the connection to modify")
        Else
            item = ListView1.SelectedItems.Item(0)

            If Not Panel1.Visible Then
                txtName.Text = item.Text
                cbServer.Text = item.SubItems(1).Text
                txtHost.Text = item.SubItems(2).Text
                txtDb.Text = item.SubItems(3).Text
                txtUser.Text = item.SubItems(4).Text
                txtPwd.Text = item.SubItems(5).Text
                If cbServer.Text = "Microsoft SQL" Then
                    chkAscendLogin.Enabled = True
                Else
                    chkAscendLogin.Enabled = False
                End If
                chkAscendLogin.Checked = If(item.SubItems(7).Text = "1", True, False)
                tbAscendDB.Text = item.SubItems(8).Text
                chkInjectAscend.Checked = If(item.SubItems(9).Text = "1", True, False)
                tbAscendHost.Text = item.SubItems(10).Text
                tbAscendDBUser.Text = item.SubItems(11).Text
                tbAscendDBPwd.Text = item.SubItems(12).Text
                tbAscendDBData.Text = item.SubItems(13).Text
                chkCopyProfiles.Checked = If(item.SubItems(14).Text = "1", True, False)

                Panel1.Visible = True
                btnLogin.Text = "Cancel"

                btnCreateCon.Enabled = False
                btnDeleteCon.Enabled = False
                Exit Sub
            End If

            If txtName.Text = "" Or cbServer.Text = "" Or
            txtDb.Text = "" Or txtHost.Text = "" Or
            txtPwd.Text = "" Or txtUser.Text = "" Then
                MsgBox("All data must be filled")
                Exit Sub
            End If

            item.Text = txtName.Text
            item.SubItems(1).Text = cbServer.Text
            item.SubItems(2).Text = txtHost.Text
            item.SubItems(3).Text = txtDb.Text
            item.SubItems(4).Text = txtUser.Text
            item.SubItems(5).Text = txtPwd.Text
            item.SubItems(7).Text = If(chkAscendLogin.Checked, 1, 0)
            item.SubItems(8).Text = tbAscendDB.Text
            item.SubItems(9).Text = If(chkInjectAscend.Checked, 1, 0)
            item.SubItems(10).Text = tbAscendHost.Text
            item.SubItems(11).Text = tbAscendDBUser.Text
            item.SubItems(12).Text = tbAscendDBPwd.Text
            item.SubItems(13).Text = tbAscendDBData.Text
            item.SubItems(14).Text = If(chkCopyProfiles.Checked, 1, 0)

            command = configCon.CreateCommand()
            command.CommandText = "update `config` set profile = ?, server = ?, host = ?, db = ?, user = ?, pwd = ?, ascend_login = ?, inject_ascend = ?, 
                                        ascend_host = ?, ascend_db = ?, ascend_data_db = ?, ascend_db_user = ?, ascend_db_pass = ?, copy_profiles_to_local = ? where id = ?"
            command.Parameters.AddWithValue("profile", txtName.Text)
            command.Parameters.AddWithValue("server", cbServer.Text)
            command.Parameters.AddWithValue("host", txtHost.Text)
            command.Parameters.AddWithValue("db", txtDb.Text)
            command.Parameters.AddWithValue("user", txtUser.Text)
            command.Parameters.AddWithValue("pwd", txtPwd.Text)
            command.Parameters.AddWithValue("ascend_login", If(chkAscendLogin.Checked, 1, 0))
            command.Parameters.AddWithValue("inject_ascend", If(chkInjectAscend.Checked, 1, 0))
            command.Parameters.AddWithValue("ascend_host", tbAscendHost.Text)
            command.Parameters.AddWithValue("ascend_db", tbAscendDB.Text)
            command.Parameters.AddWithValue("ascend_data_db", tbAscendDBData.Text)
            command.Parameters.AddWithValue("ascend_db_user", tbAscendDBUser.Text)
            command.Parameters.AddWithValue("ascend_db_pass", tbAscendDBPwd.Text)
            command.Parameters.AddWithValue("copy_profiles_to_local", If(chkCopyProfiles.Checked, 1, 0))
            command.Parameters.AddWithValue("id", item.SubItems(6).Text)
            command.ExecuteNonQuery()
            command.Dispose()

            Panel1.Visible = False
            btnLogin.Text = "Login"

            btnCreateCon.Enabled = True
            btnDeleteCon.Enabled = True
        End If
    End Sub

    Private Sub btnDeleteCon_Click(sender As Object, e As EventArgs) Handles btnDeleteCon.Click


        Dim item As ListViewItem
                Dim command As SQLiteCommand

                If ListView1.SelectedItems.Count <> 1 Then
                    MsgBox("Select the connection to modify")
                Else
                    If MsgBox("Are you sure?", vbYesNo) = vbYes Then
                        item = ListView1.SelectedItems.Item(0)

                        command = configCon.CreateCommand()
                        command.CommandText = "delete from `config` where id = ?"
                        command.Parameters.AddWithValue("id", item.SubItems(6).Text)
                        command.ExecuteNonQuery()
                        command.Dispose()

                        ListView1.Items.Remove(item)
                    End If
                End If

    End Sub

    Private Sub MainForm_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Try
            If ftp.IsConnected Then
                ftp.Disconnect()
            End If
        Catch ex As Exception
        End Try
        If configCon.State = ConnectionState.Open Then
            configCon.Close()
        End If
    End Sub

    Private Sub loadconf()
        Dim cmd As SQLiteCommand
        Dim adapter As SQLiteDataAdapter
        Dim dataset As DataSet
        Dim item As ListViewItem
        Dim conStr As String
        Dim table As String

        Try
            If Not File.Exists("config.sqlite") Then
                SQLiteConnection.CreateFile("config.sqlite")
            End If

            conStr = "Data Source=config.sqlite;Version=3;New=False;Compress=True;"
            configCon = New SQLiteConnection(conStr)
            configCon.Open()

            cmd = configCon.CreateCommand()

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='config'"
            table = cmd.ExecuteScalar()

            If table = "" Then
                cmd.CommandText = "CREATE TABLE `config` (
	                                            `id`	                    INTEGER PRIMARY KEY AUTOINCREMENT,
	                                            `profile`	                TEXT NOT NULL,
	                                            `server`	                TEXT NOT NULL,
	                                            `host`	                    TEXT NOT NULL,
	                                            `db`	                    TEXT NOT NULL,
	                                            `user`	                    TEXT NOT NULL,
	                                            `pwd`	                    TEXT NOT NULL,
	                                            `ascend_login`	            INTEGER NOT NULL DEFAULT 0,
	                                            `inject_ascend`	            INTEGER NOT NULL DEFAULT 0,
	                                            `ascend_host`	            TEXT NOT NULL,
	                                            `ascend_db`	                TEXT NOT NULL,
	                                            `ascend_data_db`	        TEXT NOT NULL,
	                                            `ascend_db_user`	        TEXT NOT NULL,
	                                            `ascend_db_pass`	        TEXT NOT NULL,
	                                            `copy_profiles_to_local`	INTEGER NOT NULL DEFAULT 0
                                            )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='ftpupdate'"
            table = cmd.ExecuteScalar()

            If table = "" Then
                cmd.CommandText = "CREATE TABLE `ftpupdate` (
	                                            `host`	TEXT,
	                                            `user`	TEXT,
	                                            `passwd`	TEXT
                                            )"
                cmd.ExecuteNonQuery()
            End If

            cmd.CommandText = "SELECT * FROM `ftpupdate`"
            adapter = New SQLiteDataAdapter(cmd)
            dataset = New DataSet
            adapter.Fill(dataset)

            If dataset.Tables(0).Rows.Count = 1 Then
                ftp = New FtpClient
                ftp.Host = dataset.Tables(0).Rows(0).Item("host")
                Dim usr As String = dataset.Tables(0).Rows(0).Item("user"), pwd As String = dataset.Tables(0).Rows(0).Item("passwd")
                ftp.Credentials = New Net.NetworkCredential(usr, pwd)
            Else
                If dataset.Tables(0).Rows.Count > 1 Then
                    cmd.CommandText = "DELETE FROM `ftpupdate`"
                    cmd.ExecuteNonQuery()
                End If
                ftp = New FtpClient
                ftp.Host = "ftp1.makmurgroup.id"
                ftp.Credentials = New Net.NetworkCredential("anonymous", "")
            End If
            ftp.DataConnectionType = FtpDataConnectionType.PASV
            TabbedParent.ftp = ftp

            cmd.CommandText = "SELECT * FROM `config`"
            adapter = New SQLiteDataAdapter(cmd)
            dataset = New DataSet
            adapter.Fill(dataset)

            For Each d In dataset.Tables(0).Rows
                item = ListView1.Items.Add(d.Item("profile"))
                item.SubItems.Add(d.Item("server"))
                item.SubItems.Add(d.Item("host"))
                item.SubItems.Add(d.Item("db"))
                item.SubItems.Add(d.Item("user"))
                item.SubItems.Add(d.Item("pwd"))
                item.SubItems.Add(d.Item("id"))
                item.SubItems.Add(d.Item("ascend_login"))
                item.SubItems.Add(d.Item("ascend_db").ToString())
                item.SubItems.Add(d.Item("inject_ascend"))
                item.SubItems.Add(d.Item("ascend_host").ToString())
                item.SubItems.Add(d.Item("ascend_db_user").ToString())
                item.SubItems.Add(d.Item("ascend_db_pass").ToString())
                item.SubItems.Add(d.Item("ascend_data_db").ToString())
                item.SubItems.Add(d.Item("copy_profiles_to_local").ToString())
            Next

            dataset.Dispose()
            adapter.Dispose()
            cmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function checkUser() As Boolean
        dbCon.SQL("SELECT count(*) AS usercount FROM users")
        Dim ds As DataSet = dbCon.getData()
        If ds.Tables.Count = 0 Then
            Return False
        End If
        If ds.Tables(0).Rows(0).Item("usercount") = 0 Then
            dbCon.SQL("INSERT INTO users(username, password, role) values(@user, @pwd, @role)")
            dbCon.addParameter("@user", "admin")
            dbCon.addParameter("@pwd", "$2a$10$3mWgvCypNt/k1GvredcaXOHks/B/0Byf   U0G2mQPCYaIK8ySsrrhwi") 'R@s3ng4n
            dbCon.addParameter("@role", "superuser")
            dbCon.execute()
        End If
        Return True
    End Function

    Private Function getCompany(Optional local As Boolean = False) As List(Of String)
        Dim list As New List(Of String)
        Dim ds As New DataSet

        If local Then
            Dim cmd As SQLiteCommand = TabbedParent.localCon.CreateCommand()

            cmd.CommandText = "SELECT DISTINCT company_code, area_code business_code FROM privileges WHERE username = ? or role = ?"
            cmd.Parameters.AddWithValue("username", user)
            cmd.Parameters.AddWithValue("role", role)

            Dim adp As New SQLiteDataAdapter(cmd)
            adp.Fill(ds)

            adp.Dispose()
            cmd.Dispose()
        Else
            dbCon.SQL("SELECT DISTINCT company_code, business_code FROM privileges WHERE username = @user or role = @role")
            dbCon.addParameter("@user", user)
            dbCon.addParameter("@role", role)
            dbCon.fillData(ds)
        End If

        For Each d In ds.Tables(0).Rows
            'list.Add(d.Item("company_code"))
            list.Add(d.Item("company_code") + "/" + d.Item("business_code"))
        Next

        Return list
    End Function

    Private Function netLatency(host As String) As Integer
        Dim ping As New Ping()
        Dim rep As PingReply = Nothing
        Try
            rep = ping.Send(host, 10000)
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


    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim app As String = AppDomain.CurrentDomain.FriendlyName
        Dim app2 As String

        If Not Directory.Exists("modules") Then
            Directory.CreateDirectory("modules")
        End If
        If Not Directory.Exists("updates") Then
            Directory.CreateDirectory("updates")
        End If
        If Not Directory.Exists("reports") Then
            Directory.CreateDirectory("reports")
        End If
        If Not Directory.Exists("images") Then
            Directory.CreateDirectory("images")
        End If

        loadconf()

        If Strings.Left(app, 4) = "new-" Then
            app2 = app.Replace("new-", "")
            While File.Exists(app2)
                File.Delete(app2)
                Threading.Thread.Sleep(100)
            End While
            File.Copy(app, app2)
            Process.Start(app2)
            End
        Else
            If CheckUpdate(False) > 0 Then
                If File.Exists("new-" + app) Then
                    Process.Start("new-" + app)
                    End
                Else
                    GetUpdate()
                End If
            Else
                If File.Exists("new-" + app) Then
                    File.Delete("new-" + app)
                End If
            End If
        End If
    End Sub

    Private Sub login()
        Dim item As ListViewItem
        Dim latency As Integer

        If ListView1.SelectedItems.Count = 1 Then
            dbCon = New DBConn()
            btnLogin.Enabled = False
            item = ListView1.SelectedItems.Item(0)

            If Not My.Computer.Keyboard.ShiftKeyDown Then
                latency = netLatency(item.SubItems(2).Text)
                If latency < 0 And item.SubItems(14).Text = "0" Then
                    Select Case latency
                        Case -1
                            MsgBox("Network request timeout")
                        Case -2
                            MsgBox("Network unreachable")
                    End Select
                    btnLogin.Enabled = True
                    Exit Sub
                End If
            End If

            Select Case item.SubItems(1).Text
                Case "Microsoft SQL"
                    dbCon.init("MSSQL", item.SubItems(2).Text, item.SubItems(3).Text,
                           item.SubItems(4).Text, item.SubItems(5).Text)
                Case "MariaDB / MySQL"
                    dbCon.init("MariaDB", item.SubItems(2).Text, item.SubItems(3).Text,
                           item.SubItems(4).Text, item.SubItems(5).Text)
            End Select

            'Me.Text = latency
            If latency >= 0 Then
                If dbCon.open() Then
                    If Not checkUser() Then
                        btnLogin.Enabled = True
                        Exit Sub
                    End If

                    LoginForm.lblCon.Text = item.Text
                    LoginForm.dbCon = dbCon
                    If item.SubItems(7).Text = "1" Then
                        LoginForm.useAscend = True
                        LoginForm.ascendConStr = "Server=" + item.SubItems(10).Text + ";Database=" + item.SubItems(8).Text + ";User Id=" +
                                                item.SubItems(11).Text + ";Password=" + item.SubItems(12).Text + ";"
                    Else
                        LoginForm.useAscend = False
                    End If
                    LoginForm.txtPwd.Text = ""

                    If LoginForm.ShowDialog() = DialogResult.OK Then
                        If Not IsNothing(user) Then
                            Dim companies As List(Of String)

                            companies = getCompany()
                            Dim ctmp As String()
                            If companies.Count = 1 Then
                                ctmp = companies(0).Split("/")

                                TabbedParent.corp = Trim(ctmp(0))
                                TabbedParent.area = Trim(ctmp(1))
                            Else
                                CompanyForm.ComboBox1.Items.Clear()

                                For Each c As String In companies
                                    CompanyForm.ComboBox1.Items.Add(c)
                                Next

                                If CompanyForm.ShowDialog() = DialogResult.OK Then
                                    ctmp = CompanyForm.ComboBox1.Text.Split("/")

                                    TabbedParent.corp = Trim(ctmp(0))
                                    TabbedParent.area = Trim(ctmp(1))
                                    CompanyForm.Button2.Visible = True
                                    CompanyForm.Button2.Enabled = True
                                Else
                                    btnLogin.Enabled = True
                                    Exit Sub
                                End If
                            End If

                            Hide()
                            TabbedParent.Owner = Me
                            TabbedParent.dbCon = dbCon
                            TabbedParent.user = user
                            TabbedParent.role = role
                            If item.SubItems(9).Text = "1" Then
                                TabbedParent.injectAscend = True
                                TabbedParent.ascendConStr = "Server=" + item.SubItems(10).Text + ";Database=" + item.SubItems(13).Text + ";User Id=" +
                                                        item.SubItems(11).Text + ";Password=" + item.SubItems(12).Text + ";"
                            Else
                                TabbedParent.injectAscend = False
                                TabbedParent.ascendConStr = ""
                            End If
                            If item.SubItems(14).Text = "1" Then
                                TabbedParent.localCon = New SQLiteConnection("Data Source=localDB.sqlite;Version=3;New=False;Compress=True;")
                                TabbedParent.localCon.Open()
                                TabbedParent.copy_profiles = True
                            End If
                            TabbedParent.configCon = configCon
                            TabbedParent.slblUser.Text = user
                            TabbedParent.slblCorp.Text = TabbedParent.corp
                            TabbedParent.slblArea.Text = TabbedParent.area
                            TabbedParent.Show()
                        End If
                    End If
                    btnLogin.Enabled = True
                Else
                    MsgBox("Cannot connect to database")
                    btnLogin.Enabled = True
                End If
            ElseIf item.SubItems(14).Text = "1" Then
                LoginForm.lblCon.Text = item.Text
                'LoginForm.dbCon = dbCon
                LoginForm.useLocal = True
                LoginForm.useAscend = False
                LoginForm.ascendConStr = ""
                If LoginForm.ShowDialog() = DialogResult.OK Then
                    If Not IsNothing(user) Then
                        Dim companies As List(Of String)

                        TabbedParent.localCon = New SQLiteConnection("Data Source=localDB.sqlite;Version=3;New=False;Compress=True;")
                        TabbedParent.localCon.Open()

                        companies = getCompany(True)
                        Dim ctmp As String()
                        If companies.Count = 1 Then
                            ctmp = companies(0).Split("/")

                            TabbedParent.corp = Trim(ctmp(0))
                            TabbedParent.area = Trim(ctmp(1))
                        Else
                            CompanyForm.ComboBox1.Items.Clear()

                            For Each c As String In companies
                                CompanyForm.ComboBox1.Items.Add(c)
                            Next

                            If CompanyForm.ShowDialog() = DialogResult.OK Then
                                ctmp = CompanyForm.ComboBox1.Text.Split("/")

                                TabbedParent.corp = Trim(ctmp(0))
                                TabbedParent.area = Trim(ctmp(1))
                                CompanyForm.Button2.Visible = True
                                CompanyForm.Button2.Enabled = True
                            Else
                                btnLogin.Enabled = True
                                Exit Sub
                            End If
                        End If

                        Hide()
                        TabbedParent.Owner = Me
                        TabbedParent.user = user
                        TabbedParent.role = role
                        TabbedParent.dbCon = dbCon
                        TabbedParent.configCon = configCon
                        TabbedParent.slblUser.Text = user
                        TabbedParent.slblCorp.Text = TabbedParent.corp
                        TabbedParent.slblArea.Text = TabbedParent.area
                        TabbedParent.injectAscend = False
                        TabbedParent.ascendConStr = ""

                        TabbedParent.Show()
                    End If
                End If
                btnLogin.Enabled = True
            End If
        Else
            MsgBox("Select which connection to login")
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        login()
    End Sub

    Private Sub ListView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ListView1.KeyPress
        If e.KeyChar = ChrW(13) Then
            login()
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Panel1.Visible Then
            Panel1.Visible = False
            btnLogin.Text = "Login"
            Button2.Visible = True
            btnCreateCon.Enabled = True
            btnModifCon.Enabled = True
            btnDeleteCon.Enabled = True
        Else
            login()
        End If
    End Sub

    Private Sub cbServer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbServer.SelectedIndexChanged
        If cbServer.Text = "Microsoft SQL" Then
            chkAscendLogin.Enabled = True
            chkInjectAscend.Enabled = True
        Else
            chkAscendLogin.Enabled = False
            chkAscendLogin.Checked = False
            chkInjectAscend.Enabled = False
            chkInjectAscend.Checked = False
        End If
    End Sub

    Private Sub chkAscend_CheckedChanged(sender As Object, e As EventArgs) Handles chkAscendLogin.CheckedChanged, chkInjectAscend.CheckedChanged
        gbAscend.Enabled = chkAscendLogin.Checked Or chkInjectAscend.Checked
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim c As New Crypto.Crypto
        'Dim test As String = c.encodeBlowfish("rasengan", "2uQGl5lV8aHETq3") 'msgr.makmurgroup.id blowfish key
        'MsgBox(test)
        'MsgBox(c.decodeBlowfish(test, "2uQGl5lV8aHETq3"))
        'Dim c As New Crypto.Crypto
        'Dim test As String = c.encodeBlowfish("rasengan", "kucing")
        'MsgBox(test)
        'MsgBox(c.decodeBlowfish(test, "kucing"))
        TextBox1.Text = "abcd"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With Pass
            .Show()
        End With
    End Sub

    'Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    '    Dim t As Type = GetType(Control)
    '    Dim mi As MethodInfo = t.GetMethod("OnValidating", BindingFlags.Instance Or BindingFlags.NonPublic)
    '    Dim ce As CancelEventArgs = New CancelEventArgs()
    '    mi.Invoke(sender, {ce})
    'End Sub

    'Private Sub TextBox1_Validating(sender As Object, e As CancelEventArgs) Handles TextBox1.Validating
    '    Debug.WriteLine("validate: " + sender.Text)
    '    Dim dec As Decimal
    '    If Not Decimal.TryParse(sender.Text, dec) Then
    '        e.Cancel = True
    '    End If
    'End Sub
End Class
