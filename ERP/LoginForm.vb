Imports DB
Imports System.IO
Imports System.Data.SQLite
Imports DesktopWorkplace.Library
Imports DesktopWorkplace.Library.AuthenticationManagement

Public Class LoginForm
    Public dbCon As DBConn
    Public useAscend As Boolean = False
    Public ascendConStr As String = ""

    Public useLocal As Boolean = False
    Public localCon As SQLiteConnection

    Private ds As DataSet

    Private Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Close()
    End Sub

    Private Sub btLogin_Click(sender As Object, e As EventArgs) Handles btLogin.Click
        If useLocal Then
            If Not File.Exists("localDB.sqlite") Then
                MsgBox("No data in local database, please connect to network first." + vbCrLf + "(LF-LC-01)")
                Exit Sub
            End If

            localCon = New SQLiteConnection("Data Source=localDB.sqlite;Version=3;New=False;Compress=True;")
            localCon.Open()

            Dim cmd As SQLiteCommand = localCon.CreateCommand()
            Dim found As Integer

            cmd.CommandText = "SELECT COUNT(*) FROM `users`"
            found = cmd.ExecuteScalar()
            If found = 0 Then
                MsgBox("No data in local database, please connect to network first." + vbCrLf + "(LF-LC-02)")
                Exit Sub
            End If

            cmd.CommandText = "SELECT COUNT(*) FROM `companies`"
            found = cmd.ExecuteScalar()
            If found = 0 Then
                MsgBox("No data in local database, please connect to network first." + vbCrLf + "(LF-LC-03)")
                Exit Sub
            End If

            cmd.CommandText = "SELECT COUNT(username) FROM `users` WHERE `username` = ? AND `password` = ? AND `status` = 1"
            cmd.Parameters.AddWithValue("username", txtUser.Text)
            cmd.Parameters.AddWithValue("password", txtPwd.Text)
            found = cmd.ExecuteScalar()

            If found = 1 Then
                MainForm.user = txtUser.Text
                MainForm.role = "user"
                DialogResult = DialogResult.OK
                Close()
            Else
                cmd.CommandText = "SELECT `password` FROM `users` WHERE `username` = ? AND `status` = 1"
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("username", txtUser.Text)
                Dim pwd As String = cmd.ExecuteScalar()

                Dim c As New Crypto.Crypto
                If c.Verify(txtPwd.Text, pwd) Then
                    MainForm.user = txtUser.Text
                    MainForm.role = "user"
                    DialogResult = DialogResult.OK
                    Close()
                Else
                    MsgBox("Wrong username or password")
                End If
            End If

            localCon.Close()
            Exit Sub
        End If

        If dbCon.state() <> ConnectionState.Open Then
            dbCon.open()
        End If

        'Try
        dbCon.SQL("SELECT * FROM view_users WHERE username = @user AND del = 0")
        dbCon.addParameter("@user", txtUser.Text)
        ds = dbCon.getData()

        If ds.Tables.Count = 1 Then
            If ds.Tables(0).Rows.Count = 1 Then
                If dbCon.verify(txtPwd.Text, ds.Tables(0).Rows(0).Item("password")) Then
                    MainForm.user = ds.Tables(0).Rows(0).Item("username")
                    MainForm.role = ds.Tables(0).Rows(0).Item("role")
                    'MainForm.user = "amos"
                    'MainForm.role = "amos"
                    DialogResult = DialogResult.OK
                    Close()
                Else
                    MsgBox("Wrong username or password")
                End If
            ElseIf useAscend Then
                Try
                    Workplace.CreateRegistryProvider(ascendConStr)
                    Dim s As Session = Session.Login(txtUser.Text, txtPwd.Text)
                    Dim u As UserAccount = s.Account
                    MainForm.user = txtUser.Text
                    MainForm.role = "superuser"

                    'save to user table when exists in ascend but not exists in db
                    dbCon.SQL("insert into users(username, password, name, role, del, created_by, created_at) values(@un, @pw, @n, @r, @d, @cb, @ca)")
                    dbCon.addParameter("@un", txtUser.Text)

                    Dim c As New Crypto.Crypto
                    dbCon.addParameter("@pw", c.HashPassword(txtPwd.Text))

                    dbCon.addParameter("@n", s.Account.FullName)
                    dbCon.addParameter("@r", "user")
                    dbCon.addParameter("@d", False)
                    dbCon.addParameter("@cb", "ASCEND")
                    dbCon.addParameter("@ca", Now)

                    dbCon.execute()

                    DialogResult = DialogResult.OK
                    Close()
                Catch ex As Exception
                    MsgBox("Wrong username or password")
                End Try
                'Exit Sub
            Else
                MsgBox("Wrong username or password")
            End If
        End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtPwd_TextChanged(sender As Object, e As EventArgs) Handles txtPwd.TextChanged

    End Sub
End Class