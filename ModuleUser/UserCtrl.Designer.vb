<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserCtrl
    Inherits ERPModules.ERPModule

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnRoleCreate = New System.Windows.Forms.Button()
        Me.chRoleCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbRole = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbUsername = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.rbPrivTypeUser = New System.Windows.Forms.RadioButton()
        Me.rbPrivTypeRole = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbPrivTarget = New System.Windows.Forms.ComboBox()
        Me.lbPrivType = New System.Windows.Forms.Label()
        Me.tbSearchUser = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnUserDelete = New System.Windows.Forms.Button()
        Me.btnUserUpdate = New System.Windows.Forms.Button()
        Me.btnUserCreate = New System.Windows.Forms.Button()
        Me.cbRole = New System.Windows.Forms.ComboBox()
        Me.lstUser = New System.Windows.Forms.ListView()
        Me.chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chUserName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chUserRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsResetPassword = New System.Windows.Forms.ToolStripMenuItem()
        Me.lstRole = New System.Windows.Forms.ListView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbRoleCode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnRoleDelete = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnRoleUpdate = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.cbDeletedUsers = New System.Windows.Forms.CheckBox()
        Me.chDel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage3.SuspendLayout()
        Me.cmStrip.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRoleCreate
        '
        Me.btnRoleCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRoleCreate.Location = New System.Drawing.Point(331, 5)
        Me.btnRoleCreate.Name = "btnRoleCreate"
        Me.btnRoleCreate.Size = New System.Drawing.Size(64, 25)
        Me.btnRoleCreate.TabIndex = 10
        Me.btnRoleCreate.Text = "Create"
        Me.btnRoleCreate.UseVisualStyleBackColor = True
        '
        'chRoleCode
        '
        Me.chRoleCode.Text = "Role Code"
        Me.chRoleCode.Width = 100
        '
        'chRole
        '
        Me.chRole.Text = "Role"
        Me.chRole.Width = 160
        '
        'tbRole
        '
        Me.tbRole.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbRole.Location = New System.Drawing.Point(63, 6)
        Me.tbRole.Name = "tbRole"
        Me.tbRole.Size = New System.Drawing.Size(262, 23)
        Me.tbRole.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 15)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Role"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Role"
        '
        'tbName
        '
        Me.tbName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbName.Location = New System.Drawing.Point(98, 33)
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(285, 23)
        Me.tbName.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Name"
        '
        'tbUsername
        '
        Me.tbUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbUsername.Location = New System.Drawing.Point(98, 6)
        Me.tbUsername.Name = "tbUsername"
        Me.tbUsername.Size = New System.Drawing.Size(285, 23)
        Me.tbUsername.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.PropertyGrid1)
        Me.TabPage3.Controls.Add(Me.Button7)
        Me.TabPage3.Controls.Add(Me.rbPrivTypeUser)
        Me.TabPage3.Controls.Add(Me.rbPrivTypeRole)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.cbPrivTarget)
        Me.TabPage3.Controls.Add(Me.lbPrivType)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(471, 454)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Privileges"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropertyGrid1.Location = New System.Drawing.Point(6, 59)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(459, 392)
        Me.PropertyGrid1.TabIndex = 15
        Me.PropertyGrid1.ToolbarVisible = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.Location = New System.Drawing.Point(388, 28)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(77, 25)
        Me.Button7.TabIndex = 14
        Me.Button7.Text = "Apply"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'rbPrivTypeUser
        '
        Me.rbPrivTypeUser.AutoSize = True
        Me.rbPrivTypeUser.Location = New System.Drawing.Point(228, 7)
        Me.rbPrivTypeUser.Name = "rbPrivTypeUser"
        Me.rbPrivTypeUser.Size = New System.Drawing.Size(50, 19)
        Me.rbPrivTypeUser.TabIndex = 12
        Me.rbPrivTypeUser.Text = "User"
        Me.rbPrivTypeUser.UseVisualStyleBackColor = True
        '
        'rbPrivTypeRole
        '
        Me.rbPrivTypeRole.AutoSize = True
        Me.rbPrivTypeRole.Checked = True
        Me.rbPrivTypeRole.Location = New System.Drawing.Point(145, 7)
        Me.rbPrivTypeRole.Name = "rbPrivTypeRole"
        Me.rbPrivTypeRole.Size = New System.Drawing.Size(49, 19)
        Me.rbPrivTypeRole.TabIndex = 11
        Me.rbPrivTypeRole.TabStop = True
        Me.rbPrivTypeRole.Text = "Role"
        Me.rbPrivTypeRole.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Privilege Type"
        '
        'cbPrivTarget
        '
        Me.cbPrivTarget.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPrivTarget.FormattingEnabled = True
        Me.cbPrivTarget.Location = New System.Drawing.Point(145, 29)
        Me.cbPrivTarget.Name = "cbPrivTarget"
        Me.cbPrivTarget.Size = New System.Drawing.Size(237, 23)
        Me.cbPrivTarget.TabIndex = 9
        '
        'lbPrivType
        '
        Me.lbPrivType.AutoSize = True
        Me.lbPrivType.Location = New System.Drawing.Point(3, 33)
        Me.lbPrivType.Name = "lbPrivType"
        Me.lbPrivType.Size = New System.Drawing.Size(31, 15)
        Me.lbPrivType.TabIndex = 8
        Me.lbPrivType.Text = "Role"
        '
        'tbSearchUser
        '
        Me.tbSearchUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSearchUser.Location = New System.Drawing.Point(61, 427)
        Me.tbSearchUser.Name = "tbSearchUser"
        Me.tbSearchUser.Size = New System.Drawing.Size(265, 23)
        Me.tbSearchUser.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 430)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 15)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Search"
        '
        'btnUserDelete
        '
        Me.btnUserDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUserDelete.Location = New System.Drawing.Point(389, 59)
        Me.btnUserDelete.Name = "btnUserDelete"
        Me.btnUserDelete.Size = New System.Drawing.Size(77, 25)
        Me.btnUserDelete.TabIndex = 10
        Me.btnUserDelete.Text = "Delete"
        Me.btnUserDelete.UseVisualStyleBackColor = True
        '
        'btnUserUpdate
        '
        Me.btnUserUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUserUpdate.Location = New System.Drawing.Point(389, 32)
        Me.btnUserUpdate.Name = "btnUserUpdate"
        Me.btnUserUpdate.Size = New System.Drawing.Size(77, 25)
        Me.btnUserUpdate.TabIndex = 9
        Me.btnUserUpdate.Text = "Update"
        Me.btnUserUpdate.UseVisualStyleBackColor = True
        '
        'btnUserCreate
        '
        Me.btnUserCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUserCreate.Location = New System.Drawing.Point(389, 5)
        Me.btnUserCreate.Name = "btnUserCreate"
        Me.btnUserCreate.Size = New System.Drawing.Size(77, 25)
        Me.btnUserCreate.TabIndex = 8
        Me.btnUserCreate.Text = "Create"
        Me.btnUserCreate.UseVisualStyleBackColor = True
        '
        'cbRole
        '
        Me.cbRole.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRole.FormattingEnabled = True
        Me.cbRole.Location = New System.Drawing.Point(98, 60)
        Me.cbRole.Name = "cbRole"
        Me.cbRole.Size = New System.Drawing.Size(285, 23)
        Me.cbRole.TabIndex = 7
        '
        'lstUser
        '
        Me.lstUser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstUser.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chName, Me.chUserName, Me.chUserRole, Me.chDel})
        Me.lstUser.ContextMenuStrip = Me.cmStrip
        Me.lstUser.FullRowSelect = True
        Me.lstUser.HideSelection = False
        Me.lstUser.Location = New System.Drawing.Point(6, 87)
        Me.lstUser.MultiSelect = False
        Me.lstUser.Name = "lstUser"
        Me.lstUser.Size = New System.Drawing.Size(459, 334)
        Me.lstUser.TabIndex = 6
        Me.lstUser.UseCompatibleStateImageBehavior = False
        Me.lstUser.View = System.Windows.Forms.View.Details
        '
        'chName
        '
        Me.chName.Text = "Name"
        Me.chName.Width = 150
        '
        'chUserName
        '
        Me.chUserName.Text = "User Name"
        Me.chUserName.Width = 100
        '
        'chUserRole
        '
        Me.chUserRole.Text = "Role"
        Me.chUserRole.Width = 80
        '
        'cmStrip
        '
        Me.cmStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsResetPassword})
        Me.cmStrip.Name = "cmStrip"
        Me.cmStrip.Size = New System.Drawing.Size(156, 26)
        '
        'tsResetPassword
        '
        Me.tsResetPassword.Name = "tsResetPassword"
        Me.tsResetPassword.Size = New System.Drawing.Size(155, 22)
        Me.tsResetPassword.Text = "Reset Password"
        '
        'lstRole
        '
        Me.lstRole.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstRole.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chRoleCode, Me.chRole})
        Me.lstRole.FullRowSelect = True
        Me.lstRole.HideSelection = False
        Me.lstRole.Location = New System.Drawing.Point(6, 63)
        Me.lstRole.MultiSelect = False
        Me.lstRole.Name = "lstRole"
        Me.lstRole.Size = New System.Drawing.Size(459, 385)
        Me.lstRole.TabIndex = 9
        Me.lstRole.UseCompatibleStateImageBehavior = False
        Me.lstRole.View = System.Windows.Forms.View.Details
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Username"
        '
        'tbRoleCode
        '
        Me.tbRoleCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbRoleCode.Location = New System.Drawing.Point(63, 33)
        Me.tbRoleCode.Name = "tbRoleCode"
        Me.tbRoleCode.Size = New System.Drawing.Size(262, 23)
        Me.tbRoleCode.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 37)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 15)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Code"
        '
        'btnRoleDelete
        '
        Me.btnRoleDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRoleDelete.Location = New System.Drawing.Point(331, 32)
        Me.btnRoleDelete.Name = "btnRoleDelete"
        Me.btnRoleDelete.Size = New System.Drawing.Size(134, 25)
        Me.btnRoleDelete.TabIndex = 12
        Me.btnRoleDelete.Text = "Delete"
        Me.btnRoleDelete.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.tbRoleCode)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.btnRoleDelete)
        Me.TabPage2.Controls.Add(Me.btnRoleUpdate)
        Me.TabPage2.Controls.Add(Me.btnRoleCreate)
        Me.TabPage2.Controls.Add(Me.lstRole)
        Me.TabPage2.Controls.Add(Me.tbRole)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(471, 454)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Roles"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnRoleUpdate
        '
        Me.btnRoleUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRoleUpdate.Location = New System.Drawing.Point(401, 5)
        Me.btnRoleUpdate.Name = "btnRoleUpdate"
        Me.btnRoleUpdate.Size = New System.Drawing.Size(64, 25)
        Me.btnRoleUpdate.TabIndex = 11
        Me.btnRoleUpdate.Text = "Update"
        Me.btnRoleUpdate.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cbDeletedUsers)
        Me.TabPage1.Controls.Add(Me.tbSearchUser)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.btnUserDelete)
        Me.TabPage1.Controls.Add(Me.btnUserUpdate)
        Me.TabPage1.Controls.Add(Me.btnUserCreate)
        Me.TabPage1.Controls.Add(Me.cbRole)
        Me.TabPage1.Controls.Add(Me.lstUser)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.tbName)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.tbUsername)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(471, 454)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Users"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(479, 482)
        Me.TabControl1.TabIndex = 1
        '
        'cbDeletedUsers
        '
        Me.cbDeletedUsers.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbDeletedUsers.AutoSize = True
        Me.cbDeletedUsers.Location = New System.Drawing.Point(332, 429)
        Me.cbDeletedUsers.Name = "cbDeletedUsers"
        Me.cbDeletedUsers.Size = New System.Drawing.Size(133, 19)
        Me.cbDeletedUsers.TabIndex = 13
        Me.cbDeletedUsers.Text = "Show Deleted Users"
        Me.cbDeletedUsers.UseVisualStyleBackColor = True
        '
        'chDel
        '
        Me.chDel.Text = "Deleted"
        '
        'UserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "UserCtrl"
        Me.Size = New System.Drawing.Size(479, 482)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.cmStrip.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnRoleCreate As System.Windows.Forms.Button
    Friend WithEvents chRoleCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents chRole As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbRole As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbUsername As System.Windows.Forms.TextBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents rbPrivTypeUser As System.Windows.Forms.RadioButton
    Friend WithEvents rbPrivTypeRole As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbPrivTarget As System.Windows.Forms.ComboBox
    Friend WithEvents lbPrivType As System.Windows.Forms.Label
    Friend WithEvents tbSearchUser As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnUserDelete As System.Windows.Forms.Button
    Friend WithEvents btnUserUpdate As System.Windows.Forms.Button
    Friend WithEvents btnUserCreate As System.Windows.Forms.Button
    Friend WithEvents cbRole As System.Windows.Forms.ComboBox
    Friend WithEvents lstUser As System.Windows.Forms.ListView
    Friend WithEvents chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chUserName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chUserRole As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsResetPassword As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lstRole As System.Windows.Forms.ListView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbRoleCode As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnRoleDelete As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnRoleUpdate As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents cbDeletedUsers As System.Windows.Forms.CheckBox
    Friend WithEvents chDel As System.Windows.Forms.ColumnHeader
End Class
