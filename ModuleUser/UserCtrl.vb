Imports System.Windows.Forms
'Imports DB
Imports ERPModules

Public Class UserCtrl : Inherits ERPModule

    'Private dbCon As DBConn
    'Private _user As String
    'Private _role As String
    'Private _corp As String
    'Private _area As String

    'Private toolMenu As New Dictionary(Of String, ToolStripItem)
    'Private toolButton As New Dictionary(Of String, ERPModules.buttonState)

    Private userBuffer As New List(Of Dictionary(Of String, Object))
    Private userSearch As New List(Of Dictionary(Of String, Object))

    Private loading As Boolean = True

    'Public Property dbCon As DBConn Implements IERPModule.dbCon
    '    Get
    '        Return dbCon
    '    End Get
    '    Set(value As DBConn)
    '        dbCon = value
    '    End Set
    'End Property

    'Public Property user As String Implements IERPModule.user
    '    Get
    '        Return _user
    '    End Get
    '    Set(value As String)
    '        _user = value
    '    End Set
    'End Property

    'Public Property role As String Implements IERPModule.role
    '    Get
    '        Return _role
    '    End Get
    '    Set(value As String)
    '        _role = value
    '    End Set
    'End Property

    'Public Property corp As String Implements IERPModule.corp
    '    Get
    '        Return _corp
    '    End Get
    '    Set(value As String)
    '        _corp = value
    '    End Set
    'End Property

    'Public Property area As String Implements IERPModule.area
    '    Get
    '        Return _area
    '    End Get
    '    Set(value As String)
    '        _area = value
    '    End Set
    'End Property

    'Public ReadOnly Property toolMenu As Dictionary(Of String, ToolStripItem) Implements IERPModule.toolMenu
    '    Get
    '        Return toolMenu
    '    End Get
    'End Property

    'Public ReadOnly Property toolButton As Dictionary(Of String, ERPModules.buttonState) Implements IERPModule.toolButton
    '    Get
    '        Return toolButton
    '    End Get
    '    'Set(value As Dictionary(Of String, ERPModules.buttonState))
    '    '    toolButton = value
    '    'End Set
    'End Property

    Public Overrides Sub Print()
        MsgBox("Nothing to print")
    End Sub

    Public Overrides Sub Save()
        MsgBox("Nothing to save")
    End Sub

    'Public Function isPrived(task As String, lvl As Integer) As Boolean Implements IERPModule.isPrived
    '    Return True
    'End Function

    Public Overloads Sub Dispose()
        Try
            toolMenu.Clear()
            userBuffer.Clear()
            userSearch.Clear()

            MyBase.Dispose()
        Catch
        End Try
    End Sub

    Private Sub PrivType_CheckedChanged(sender As Object, e As EventArgs) Handles rbPrivTypeUser.CheckedChanged, rbPrivTypeRole.CheckedChanged
        If loading Then
            Exit Sub
        End If
        If DirectCast(sender, RadioButton).Text = "User" Then
            lbPrivType.Text = "User"
            loadPrivType("user")
        Else
            lbPrivType.Text = "Role"
            loadPrivType("role")
        End If
    End Sub

    Private Sub loadPrivType(type As String)
        If dbCon.state() = ConnectionState.Open Then
            Dim dt As New DataTable

            Select Case type
                Case "user"
                    dbCon.SQL("select username [code], name [text] from users")
                Case "role"
                    dbCon.SQL("select role_code [code], role [text] from roles")
            End Select

            dbCon.fillTable(dt)

            cbPrivTarget.DataSource = New BindingSource(dt, Nothing)
            cbPrivTarget.DisplayMember = "text"
            cbPrivTarget.ValueMember = "code"
        End If
    End Sub

    Private Sub loadUsers()
        Dim rdr As Object

        If dbCon.state() = ConnectionState.Open Then
            Dim cmdRdr As Object
            If cbDeletedUsers.Checked Then
                cmdRdr = dbCon.SQLReader("select * from view_users")
            Else
                cmdRdr = dbCon.SQLReader("select * from view_users where del = 0")
            End If

            rdr = dbCon.beginRead(cmdRdr)
            userBuffer.Clear()
            userSearch.Clear()

            While dbCon.doRead(rdr)
                userBuffer.Add(dbCon.getRow(rdr))
            End While
            dbCon.endRead(rdr)
            userSearch.AddRange(userBuffer)
            lstUser.VirtualListSize = userSearch.Count()
            lstUser.Refresh()
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadRoles()
        Dim dt As New DataTable
        Dim item As ListViewItem

        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select role, role_code from roles")
            dbCon.fillTable(dt)

            cbRole.DataSource = New BindingSource(dt, Nothing)
            cbRole.DisplayMember = "role"
            cbRole.ValueMember = "role_code"

            lstRole.Items.Clear()

            For Each r As DataRow In dt.Rows
                item = lstRole.Items.Add(r.Item("role_code"))
                item.SubItems.Add(r.Item("role"))
            Next
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub lstUser_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstUser.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = userSearch(e.ItemIndex)
        e.Item = New ListViewItem(row("name").ToString)
        e.Item.SubItems.Add(row("username"))
        e.Item.SubItems.Add(If(row("rolename"), ""))
        e.Item.SubItems.Add(If(row("del"), "Yes", "No"))
        e.Item.SubItems.Add(If(row("role"), ""))
        If row("del") Then
            e.Item.ForeColor = Drawing.Color.Tomato
        End If
    End Sub

    Private Function searchUser(lst As List(Of Dictionary(Of String, Object)), val As String) As IQueryable(Of Dictionary(Of String, Object))
        searchUser = lst.AsQueryable

        Dim filter As Expressions.Expression(Of Func(Of Dictionary(Of String, Object), Boolean)) =
            Function(i As Dictionary(Of String, Object)) i.Where(Function(x) x.Value.ToString().IndexOf(val, 0, StringComparison.CurrentCultureIgnoreCase) > -1).Count > 0

        If Not String.IsNullOrEmpty(val) Then searchUser = Queryable.Where(searchUser, filter)
    End Function

    Private Sub tbSearchUser_TextChanged(sender As Object, e As EventArgs) Handles tbSearchUser.TextChanged
        Dim tmp As IQueryable(Of Dictionary(Of String, Object))
        tmp = searchUser(userBuffer, tbSearchUser.Text)
        userSearch = tmp.ToList()
        lstUser.VirtualListSize = userSearch.Count
    End Sub

    Private Sub lstUser_DoubleClick(sender As Object, e As EventArgs) Handles lstUser.DoubleClick
        Dim item As ListViewItem

        If lstUser.SelectedIndices.Count = 1 Then
            item = lstUser.Items(lstUser.SelectedIndices(0))
            tbUsername.Text = item.SubItems(1).Text
            tbName.Text = item.Text
            cbRole.SelectedValue = item.SubItems(4).Text
        End If
    End Sub

    Private Sub lstRole_DoubleClick(sender As Object, e As EventArgs) Handles lstRole.DoubleClick
        Dim item As ListViewItem

        If lstRole.SelectedItems.Count = 1 Then
            item = lstRole.SelectedItems.Item(0)
            tbRole.Text = item.SubItems(1).Text
            tbRoleCode.Text = item.Text
        End If
    End Sub

    Private Function userExists(username As String) As Boolean
        dbCon.SQL("select count(*) from users where username = @un")
        dbCon.addParameter("@un", username)
        Return dbCon.scalar()
    End Function

    Private Sub createUser(username As String, name As String, pwd As String, role As String)
        dbCon.SQL("insert into users(username, name, password, role, created_by, created_at) values(@un, @n, @pwd, @r, @cb, @ca)")
        dbCon.addParameter("@un", username)
        dbCon.addParameter("@n", name)
        dbCon.addParameter("@pwd", pwd)
        dbCon.addParameter("@r", role)
        dbCon.addParameter("@cb", user)
        dbCon.addParameter("@ca", Now.ToString("yyyy-MM-dd HH:mm:ss"))
        dbCon.execute()
    End Sub

    Private Sub updateUser(username As String, name As String, role As String)
        dbCon.SQL("update users set name = @n, role = @r where username = @un")
        dbCon.addParameter("@n", name)
        dbCon.addParameter("@r", role)
        dbCon.addParameter("@un", username)
        dbCon.execute()
    End Sub

    Private Sub changePasswd(username As String, newPass As String)
        dbCon.SQL("update users set password = @pwd where username = @un")
        dbCon.addParameter("@pwd", newPass)
        dbCon.addParameter("@un", username)
        dbCon.execute()
    End Sub

    Private Sub deleteUser(username As String)
        dbCon.SQL("update users set del = 1 where username = @un")
        dbCon.addParameter("@un", username)
        dbCon.execute()
    End Sub

    Private Sub createRole(role As String, code As String)
        dbCon.SQL("insert into roles(role_code, role) values(@rc, @r)")
        dbCon.addParameter("@rc", code)
        dbCon.addParameter("@r", role)
        dbCon.execute()
    End Sub

    Private Sub updateRole(role As String, code As String)
        dbCon.SQL("update roles set role = @r where role_code = @rc")
        dbCon.addParameter("@rc", code)
        dbCon.addParameter("@r", role)
        dbCon.execute()
    End Sub

    Private Sub deleteRole(code As String)
        dbCon.SQL("update roles set del = 1 where role_code = @rc")
        dbCon.addParameter("@rc", code)
        dbCon.execute()
    End Sub

    Private Sub tsResetPassword_Click(sender As Object, e As EventArgs) Handles tsResetPassword.Click
        If lstUser.SelectedIndices.Count = 1 Then
            Dim fPassword As New Password
            Dim item As ListViewItem

            If fPassword.ShowDialog() = DialogResult.OK Then
                item = lstUser.Items(lstUser.SelectedIndices(0))

                changePasswd(item.SubItems(1).Text, dbCon.hashString(fPassword.tbNewPasswd.Text))
            End If

            fPassword.Dispose()
        End If
    End Sub

    Private Sub btnUserCreate_Click(sender As Object, e As EventArgs) Handles btnUserCreate.Click
        If tbUsername.Text = "" Or tbName.Text = "" Then
            MsgBox("Username and Name cannot be empty")
            Exit Sub
        End If

        If cbRole.SelectedIndex = -1 Then
            MsgBox("Role must be selected")
            Exit Sub
        End If

        Dim fPassword As New Password

        If fPassword.ShowDialog() = DialogResult.OK Then
            If userExists(tbUsername.Text) = 0 Then
                createUser(tbUsername.Text, tbName.Text, dbCon.hashString(fPassword.tbNewPasswd.Text), cbRole.SelectedValue)
            Else
                MsgBox("User already exists, but might have been deleted")
            End If
        End If

        fPassword.Dispose()

        tbName.Text = ""
        tbUsername.Text = ""
        cbRole.SelectedIndex = -1

        loadUsers()
    End Sub

    Private Sub btnUserUpdate_Click(sender As Object, e As EventArgs) Handles btnUserUpdate.Click
        If lstUser.SelectedIndices.Count = 1 Then
            If tbUsername.Text = "" Or tbName.Text = "" Then
                MsgBox("Username and Name cannot be empty")
                Exit Sub
            End If

            If cbRole.SelectedIndex = -1 Then
                MsgBox("Role must be selected")
                Exit Sub
            End If

            Dim fPassword As New Password
            Dim item As ListViewItem

            item = lstUser.Items(lstUser.SelectedIndices(0))

            updateUser(item.SubItems(1).Text, tbName.Text, cbRole.SelectedValue)

            tbName.Text = ""
            tbUsername.Text = ""
            cbRole.SelectedIndex = -1

            loadUsers()
        Else
            MsgBox("No user selected")
        End If
    End Sub

    Private Sub btnUserDelete_Click(sender As Object, e As EventArgs) Handles btnUserDelete.Click
        If lstUser.SelectedIndices.Count = 1 Then
            Dim fPassword As New Password
            Dim item As ListViewItem

            item = lstUser.Items(lstUser.SelectedIndices(0))

            deleteUser(item.SubItems(1).Text)

            loadUsers()
        End If
    End Sub

    Private Sub btnRoleCreate_Click(sender As Object, e As EventArgs) Handles btnRoleCreate.Click
        If tbRole.Text = "" Or tbRoleCode.Text = "" Then
            MsgBox("Role and Role Code cannot be empty")
            Exit Sub
        End If

        createRole(tbRole.Text, tbRoleCode.Text)

        tbRole.Text = ""
        tbRoleCode.Text = ""

        loadRoles()
    End Sub

    Private Sub btnRoleUpdate_Click(sender As Object, e As EventArgs) Handles btnRoleUpdate.Click
        If lstRole.SelectedItems.Count = 1 Then
            Dim item As ListViewItem

            item = lstRole.SelectedItems.Item(0)

            updateRole(tbRole.Text, item.Text)

            tbRole.Text = ""
            tbRoleCode.Text = ""

            loadRoles()
        End If
    End Sub

    Private Sub btnRoleDelete_Click(sender As Object, e As EventArgs) Handles btnRoleDelete.Click
        If lstRole.SelectedItems.Count = 1 Then
            Dim item As ListViewItem

            item = lstRole.SelectedItems.Item(0)

            deleteRole(item.Text)

            loadRoles()
        End If
    End Sub

    Private Sub UserCtrl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim btn As ERPModules.buttonState

        lstUser.VirtualMode = True
        loadUsers()
        loadRoles()
        loadPrivType("role")
        loading = False

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
        btn.visible = False
        btn.enabled = False
        toolButton.Add("Print", btn)

    End Sub

    Private Sub cbDeletedUsers_CheckedChanged(sender As Object, e As EventArgs) Handles cbDeletedUsers.CheckedChanged
        loadUsers()
    End Sub
End Class