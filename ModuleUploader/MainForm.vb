Imports System.IO
Imports System.Data.SQLite
Imports DB

Public Class MainForm

    Private configCon As SQLiteConnection
    Private dbCon As DBConn
    Private Data As DataTable

    Private Sub loadconf()
        Dim command As SQLiteCommand
        Dim adapter As SQLiteDataAdapter
        Dim dataset As DataSet
        Dim item As ListViewItem
        Dim conStr As String
        Dim createTable As Boolean

        Try
            If File.Exists("config.sqlite") Then
                createTable = False
            Else
                SQLiteConnection.CreateFile("config.sqlite")
                createTable = True
            End If

            conStr = "Data Source=config.sqlite;Version=3;New=False;Compress=True;"
            configCon = New SQLiteConnection(conStr)
            configCon.Open()

            command = configCon.CreateCommand()

            If (createTable) Then
                If (configCon.State <> ConnectionState.Open) Then
                    MsgBox("connect?")
                Else
                    command.CommandText = "CREATE TABLE `config`(`id` INTEGER PRIMARY KEY AUTOINCREMENT, `profile` TEXT, `server` TEXT, `host` TEXT, `db` TEXT, `user` TEXT, `pwd` TEXT)"
                    command.ExecuteNonQuery()
                    command.CommandText = "CREATE TABLE `modules`(`name` TEXT, `dll` TEXT, `form` TEXT, `version` TEXT, PRIMARY KEY(dll,form))"
                    command.ExecuteNonQuery()
                End If
            End If

            command.CommandText = "SELECT * FROM `config`"
            adapter = New SQLiteDataAdapter(command)
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
            Next

            dataset.Dispose()
            adapter.Dispose()
            command.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub loaddata()
        Dim item As ListViewItem

        Dim ds As DataSet
        Dim row As DataRow

        If ListView1.SelectedItems.Count = 1 Then
            dbCon = New DBConn
            ds = New DataSet
            item = ListView1.SelectedItems.Item(0)
            Select Case item.SubItems(1).Text
                Case "Microsoft SQL"
                    dbCon.init("MSSQL", item.SubItems(2).Text, item.SubItems(3).Text,
                               item.SubItems(4).Text, item.SubItems(5).Text)
                Case "MariaDB / MySQL"
                    dbCon.init("MariaDB", item.SubItems(2).Text, item.SubItems(3).Text,
                               item.SubItems(4).Text, item.SubItems(5).Text)
            End Select
            dbCon.open()
            dbCon.SQL("SELECT name, dll, form, version FROM modules")
            ds = dbCon.getData()
            Data.Rows.Clear()

            For Each r As DataRow In ds.Tables(0).Rows
                row = Data.Rows.Add()
                row.Item("Module") = r.Item("name")
                row.Item("DLL") = r.Item("dll")
                row.Item("Form") = r.Item("form")
                row.Item("Version") = r.Item("version")
            Next

            ds.Dispose()
            dbCon.close()
            dbCon.Dispose()
        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Data = New DataTable

        Data.Columns.Add("Module")
        Data.Columns.Add("DLL")
        Data.Columns.Add("Form")
        Data.Columns.Add("Version")
        Data.Columns.Add("Full Path")

        DataGridView1.DataSource = Data

        loadconf()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim folder As String
        Dim row As DataRow
        Dim info As FileInfo
        Dim vinfo As FileVersionInfo

        FolderBrowserDialog1.SelectedPath = Directory.GetCurrentDirectory
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Data.Rows.Clear()
            folder = FolderBrowserDialog1.SelectedPath
            For Each f In Directory.GetFiles(folder)
                info = New FileInfo(f)
                vinfo = FileVersionInfo.GetVersionInfo(f)

                If info.Extension = ".dll" Then
                    row = Data.Rows.Add()
                    row.Item("Module") = ""
                    row.Item("DLL") = Path.GetFileNameWithoutExtension(info.Name)
                    row.Item("Form") = ""
                    row.Item("Version") = vinfo.FileVersion
                    row.Item("Full Path") = info.FullName
                End If
            Next
        End If
    End Sub

    Private Sub btnAddFiles_Click(sender As Object, e As EventArgs) Handles btnAddFiles.Click
        Dim info As FileInfo
        Dim vinfo As FileVersionInfo
        Dim row As DataRow
        Dim addRow As Boolean

        OpenFileDialog1.InitialDirectory = Directory.GetCurrentDirectory
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            For Each f In OpenFileDialog1.FileNames
                info = New FileInfo(f)
                vinfo = FileVersionInfo.GetVersionInfo(f)

                If info.Extension = ".dll" Then
                    addRow = False
                    If OpenFileDialog1.FileNames.Count > 1 Then
                        addRow = True
                    End If
                    If DataGridView1.SelectedRows.Count = 1 And Data.Rows.Count > 0 Then
                        If DataGridView1.SelectedRows.Item(0).Index < Data.Rows.Count Then
                            row = Data.Rows(DataGridView1.SelectedRows.Item(0).Index)
                            row.Item("Version") = vinfo.FileVersion
                            row.Item("Full Path") = info.FullName
                        Else
                            addRow = True
                        End If
                    Else
                        addRow = True
                    End If
                    If addRow Then
                        row = Data.Rows.Add()
                        row.Item("Module") = ""
                        row.Item("DLL") = Path.GetFileNameWithoutExtension(info.Name)
                        row.Item("Form") = ""
                        row.Item("Version") = vinfo.FileVersion
                        row.Item("Full Path") = info.FullName
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim item As ListViewItem
        Dim f As Byte()

        For Each d As DataRow In Data.Rows
            If d.Item("Module") = "" Then
                MsgBox("Please name the module of " & d.Item("DLL"))
                Exit Sub
            End If
            If d.Item("Form") = "" Then
                MsgBox("Please specify the form to be loaded for module " & d.Item("Module"))
                Exit Sub
            End If
            If d.Item("Version") = "" Then
                MsgBox("Please specify the version of module " & d.Item("Module"))
                Exit Sub
            End If
        Next
        If ListView1.SelectedItems.Count = 1 Then
            dbCon = New DBConn
            item = ListView1.SelectedItems.Item(0)
            Select Case item.SubItems(1).Text
                Case "Microsoft SQL"
                    dbCon.init("MSSQL", item.SubItems(2).Text, item.SubItems(3).Text,
                               item.SubItems(4).Text, item.SubItems(5).Text)
                Case "MariaDB / MySQL"
                    dbCon.init("MariaDB", item.SubItems(2).Text, item.SubItems(3).Text,
                               item.SubItems(4).Text, item.SubItems(5).Text)
            End Select
            dbCon.open()
            For Each d As DataRow In Data.Rows
                If d.Item("Full Path").ToString = "" Then
                    Continue For
                End If
                f = File.ReadAllBytes(d.Item("Full Path"))
                Select Case dbCon.getServerType
                    Case "MSSQL"
                        dbCon.SQL("MERGE modules AS m USING (values (@version, @file)) as v(version, [file]) " &
                                 "ON m.dll = @dll AND m.form = @form " &
                                 "WHEN MATCHED THEN UPDATE SET [file] = v.[file], version = v.version " &
                                 "WHEN NOT MATCHED THEN INSERT (name, dll, form, version, [file]) " &
                                 "VALUES (@name, @dll, @form, v.version, v.[file]);")
                    Case "MySQL", "MariaDB"
                        dbCon.SQL("REPLACE INTO modules(name, dll, form, version, `file`) " &
                                  "VALUES(@name, @dll, @form, @version, @file)")
                End Select
                dbCon.addParameter("@name", d.Item("Module"))
                dbCon.addParameter("@dll", d.Item("DLL"))
                dbCon.addParameter("@form", d.Item("Form"))
                dbCon.addParameter("@version", d.Item("Version"))
                dbCon.addParameter("@file", f)
                dbCon.execute()
            Next
            MsgBox("Upload finished")
        Else
            MsgBox("Please select the connection to be uploaded")
        End If
    End Sub

    Private Sub DataGridView1_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView1.ColumnAdded
        e.Column.SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub

    Private Sub btnCreateCon_Click(sender As Object, e As EventArgs) Handles btnCreateCon.Click
        Dim command As SQLiteCommand
        Dim adp As SQLiteDataAdapter
        Dim ds As DataSet

        Dim item As ListViewItem

        command = configCon.CreateCommand()
        command.CommandText = "insert into config(profile, server, host, db, user, pwd) values(?, ?, ?, ?, ?, ?)"
        command.Parameters.AddWithValue("profile", txtName.Text)
        command.Parameters.AddWithValue("server", cbServer.Text)
        command.Parameters.AddWithValue("host", txtHost.Text)
        command.Parameters.AddWithValue("db", txtDb.Text)
        command.Parameters.AddWithValue("user", txtUser.Text)
        command.Parameters.AddWithValue("pwd", txtPwd.Text)
        command.ExecuteNonQuery()
        command.Dispose()

        command = configCon.CreateCommand()
        command.CommandText = "select id from config where profile = ?"
        command.Parameters.AddWithValue("profile", txtName.Text)
        adp = New SQLiteDataAdapter(command)
        ds = New DataSet
        adp.Fill(ds)

        Dim id = ds.Tables(0).Rows(0).Item("id")

        ds.Dispose()
        adp.Dispose()
        command.Dispose()

        item = ListView1.Items.Add(txtName.Text)
        item.SubItems.Add(cbServer.Text)
        item.SubItems.Add(txtHost.Text)
        item.SubItems.Add(txtDb.Text)
        item.SubItems.Add(txtUser.Text)
        item.SubItems.Add(txtPwd.Text)
        item.SubItems.Add(id)
    End Sub

    Private Sub btnModifCon_Click(sender As Object, e As EventArgs) Handles btnModifCon.Click
        Dim item As ListViewItem
        Dim command As SQLiteCommand

        If ListView1.SelectedItems.Count <> 1 Then
            MsgBox("Select the connection to modify")
        Else
            item = ListView1.SelectedItems.Item(0)
            item.Text = txtName.Text
            item.SubItems(1).Text = cbServer.Text
            item.SubItems(2).Text = txtHost.Text
            item.SubItems(3).Text = txtDb.Text
            item.SubItems(4).Text = txtUser.Text
            item.SubItems(5).Text = txtPwd.Text

            command = configCon.CreateCommand()
            command.CommandText = "update `config` set profile = ?, host = ?, db = ?, user = ?, pwd = ? where id = ?"
            command.Parameters.AddWithValue("profile", txtName.Text)
            command.Parameters.AddWithValue("server", cbServer.Text)
            command.Parameters.AddWithValue("host", txtHost.Text)
            command.Parameters.AddWithValue("db", txtDb.Text)
            command.Parameters.AddWithValue("user", txtUser.Text)
            command.Parameters.AddWithValue("pwd", txtPwd.Text)
            command.Parameters.AddWithValue("id", item.SubItems(6).Text)
            command.ExecuteNonQuery()
            command.Dispose()
        End If
    End Sub

    Private Sub btnDeleteCon_Click(sender As Object, e As EventArgs) Handles btnDeleteCon.Click
        Dim item As ListViewItem
        Dim command As SQLiteCommand

        If ListView1.SelectedItems.Count <> 1 Then
            MsgBox("Select the connection to modify")
        Else
            item = ListView1.SelectedItems.Item(0)

            command = configCon.CreateCommand()
            command.CommandText = "delete from `config` where id = ?"
            command.Parameters.AddWithValue("id", item.SubItems(6).Text)
            command.ExecuteNonQuery()
            command.Dispose()

            ListView1.Items.Remove(item)

        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim item As ListViewItem

        If ListView1.SelectedItems.Count = 1 Then
            item = ListView1.SelectedItems.Item(0)

            txtName.Text = item.Text
            cbServer.Text = item.SubItems(1).Text
            txtHost.Text = item.SubItems(2).Text
            txtDb.Text = item.SubItems(3).Text
            txtUser.Text = item.SubItems(4).Text
            txtPwd.Text = item.SubItems(5).Text

            loaddata()
        End If
    End Sub

End Class

