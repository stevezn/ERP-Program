<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing AndAlso dbCon IsNot Nothing Then
                dbCon.Dispose()
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnCreateCon = New System.Windows.Forms.Button()
        Me.btnModifCon = New System.Windows.Forms.Button()
        Me.btnDeleteCon = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPwd = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDb = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtHost = New System.Windows.Forms.TextBox()
        Me.cbServer = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkAscendLogin = New System.Windows.Forms.CheckBox()
        Me.tbAscendDB = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkInjectAscend = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkCopyProfiles = New System.Windows.Forms.CheckBox()
        Me.gbAscend = New System.Windows.Forms.GroupBox()
        Me.tbAscendDBData = New System.Windows.Forms.TextBox()
        Me.tbAscendHost = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.tbAscendDBPwd = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbAscendDBUser = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.gbAscend.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(14, 72)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(255, 341)
        Me.ListView1.TabIndex = 13
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Connections"
        Me.ColumnHeader1.Width = 150
        '
        'btnCreateCon
        '
        Me.btnCreateCon.Enabled = False
        Me.btnCreateCon.Location = New System.Drawing.Point(275, 320)
        Me.btnCreateCon.Name = "btnCreateCon"
        Me.btnCreateCon.Size = New System.Drawing.Size(83, 27)
        Me.btnCreateCon.TabIndex = 10
        Me.btnCreateCon.Text = "Create"
        Me.btnCreateCon.UseVisualStyleBackColor = True
        '
        'btnModifCon
        '
        Me.btnModifCon.Enabled = False
        Me.btnModifCon.Location = New System.Drawing.Point(275, 353)
        Me.btnModifCon.Name = "btnModifCon"
        Me.btnModifCon.Size = New System.Drawing.Size(83, 27)
        Me.btnModifCon.TabIndex = 11
        Me.btnModifCon.Text = "Modify"
        Me.btnModifCon.UseVisualStyleBackColor = True
        '
        'btnDeleteCon
        '
        Me.btnDeleteCon.Enabled = False
        Me.btnDeleteCon.Location = New System.Drawing.Point(275, 386)
        Me.btnDeleteCon.Name = "btnDeleteCon"
        Me.btnDeleteCon.Size = New System.Drawing.Size(83, 27)
        Me.btnDeleteCon.TabIndex = 12
        Me.btnDeleteCon.Text = "Delete"
        Me.btnDeleteCon.UseVisualStyleBackColor = True
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(275, 72)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(83, 27)
        Me.btnLogin.TabIndex = 9
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(314, 105)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(44, 23)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "test"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(-2, 151)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 15)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Password"
        '
        'txtPwd
        '
        Me.txtPwd.Location = New System.Drawing.Point(87, 148)
        Me.txtPwd.Name = "txtPwd"
        Me.txtPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwd.Size = New System.Drawing.Size(168, 23)
        Me.txtPwd.TabIndex = 5
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(87, 118)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtUser.Size = New System.Drawing.Size(168, 23)
        Me.txtUser.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(-2, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "User"
        '
        'txtDb
        '
        Me.txtDb.Location = New System.Drawing.Point(87, 88)
        Me.txtDb.Name = "txtDb"
        Me.txtDb.Size = New System.Drawing.Size(168, 23)
        Me.txtDb.TabIndex = 3
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(87, 0)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(168, 23)
        Me.txtName.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-2, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Database"
        '
        'txtHost
        '
        Me.txtHost.Location = New System.Drawing.Point(87, 58)
        Me.txtHost.Name = "txtHost"
        Me.txtHost.Size = New System.Drawing.Size(168, 23)
        Me.txtHost.TabIndex = 2
        '
        'cbServer
        '
        Me.cbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbServer.FormattingEnabled = True
        Me.cbServer.Items.AddRange(New Object() {"Microsoft SQL", "MariaDB / MySQL"})
        Me.cbServer.Location = New System.Drawing.Point(87, 29)
        Me.cbServer.Name = "cbServer"
        Me.cbServer.Size = New System.Drawing.Size(168, 23)
        Me.cbServer.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-2, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Host"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(-2, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 15)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Server Type"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(-2, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 15)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Name"
        '
        'chkAscendLogin
        '
        Me.chkAscendLogin.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.chkAscendLogin.Location = New System.Drawing.Point(0, 176)
        Me.chkAscendLogin.Name = "chkAscendLogin"
        Me.chkAscendLogin.Size = New System.Drawing.Size(80, 30)
        Me.chkAscendLogin.TabIndex = 6
        Me.chkAscendLogin.Text = "Use Ascend Login"
        Me.chkAscendLogin.UseVisualStyleBackColor = True
        '
        'tbAscendDB
        '
        Me.tbAscendDB.Location = New System.Drawing.Point(86, 44)
        Me.tbAscendDB.Name = "tbAscendDB"
        Me.tbAscendDB.Size = New System.Drawing.Size(80, 23)
        Me.tbAscendDB.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(2, 47)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 15)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Database"
        '
        'chkInjectAscend
        '
        Me.chkInjectAscend.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.chkInjectAscend.Location = New System.Drawing.Point(87, 176)
        Me.chkInjectAscend.Name = "chkInjectAscend"
        Me.chkInjectAscend.Size = New System.Drawing.Size(80, 30)
        Me.chkInjectAscend.TabIndex = 7
        Me.chkInjectAscend.Text = "Inject Ascend"
        Me.chkInjectAscend.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkCopyProfiles)
        Me.Panel1.Controls.Add(Me.gbAscend)
        Me.Panel1.Controls.Add(Me.chkInjectAscend)
        Me.Panel1.Controls.Add(Me.chkAscendLogin)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cbServer)
        Me.Panel1.Controls.Add(Me.txtHost)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtName)
        Me.Panel1.Controls.Add(Me.txtDb)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtUser)
        Me.Panel1.Controls.Add(Me.txtPwd)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(14, 72)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(255, 341)
        Me.Panel1.TabIndex = 31
        Me.Panel1.Visible = False
        '
        'chkCopyProfiles
        '
        Me.chkCopyProfiles.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.chkCopyProfiles.Location = New System.Drawing.Point(158, 176)
        Me.chkCopyProfiles.Name = "chkCopyProfiles"
        Me.chkCopyProfiles.Size = New System.Drawing.Size(91, 30)
        Me.chkCopyProfiles.TabIndex = 35
        Me.chkCopyProfiles.Text = "Copy Profiles to Local"
        Me.chkCopyProfiles.UseVisualStyleBackColor = True
        '
        'gbAscend
        '
        Me.gbAscend.Controls.Add(Me.tbAscendDBData)
        Me.gbAscend.Controls.Add(Me.tbAscendHost)
        Me.gbAscend.Controls.Add(Me.Label10)
        Me.gbAscend.Controls.Add(Me.tbAscendDBPwd)
        Me.gbAscend.Controls.Add(Me.Label9)
        Me.gbAscend.Controls.Add(Me.tbAscendDBUser)
        Me.gbAscend.Controls.Add(Me.Label8)
        Me.gbAscend.Controls.Add(Me.tbAscendDB)
        Me.gbAscend.Controls.Add(Me.Label7)
        Me.gbAscend.Enabled = False
        Me.gbAscend.Location = New System.Drawing.Point(1, 207)
        Me.gbAscend.Name = "gbAscend"
        Me.gbAscend.Size = New System.Drawing.Size(254, 133)
        Me.gbAscend.TabIndex = 8
        Me.gbAscend.TabStop = False
        Me.gbAscend.Text = "Ascend"
        '
        'tbAscendDBData
        '
        Me.tbAscendDBData.Location = New System.Drawing.Point(168, 44)
        Me.tbAscendDBData.Name = "tbAscendDBData"
        Me.tbAscendDBData.Size = New System.Drawing.Size(80, 23)
        Me.tbAscendDBData.TabIndex = 40
        '
        'tbAscendHost
        '
        Me.tbAscendHost.Location = New System.Drawing.Point(86, 15)
        Me.tbAscendHost.Name = "tbAscendHost"
        Me.tbAscendHost.Size = New System.Drawing.Size(162, 23)
        Me.tbAscendHost.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(2, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 15)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "Host"
        '
        'tbAscendDBPwd
        '
        Me.tbAscendDBPwd.Location = New System.Drawing.Point(86, 102)
        Me.tbAscendDBPwd.Name = "tbAscendDBPwd"
        Me.tbAscendDBPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbAscendDBPwd.Size = New System.Drawing.Size(162, 23)
        Me.tbAscendDBPwd.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(2, 105)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 15)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "Password"
        '
        'tbAscendDBUser
        '
        Me.tbAscendDBUser.Location = New System.Drawing.Point(86, 73)
        Me.tbAscendDBUser.Name = "tbAscendDBUser"
        Me.tbAscendDBUser.Size = New System.Drawing.Size(162, 23)
        Me.tbAscendDBUser.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(2, 76)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 15)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "User"
        '
        'TextBox1
        '
        Me.TextBox1.CausesValidation = False
        Me.TextBox1.Location = New System.Drawing.Point(314, 130)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(44, 23)
        Me.TextBox1.TabIndex = 35
        Me.TextBox1.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.Image = Global.ERP.My.Resources.Resources.banner091209112617
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(370, 57)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 33
        Me.PictureBox1.TabStop = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(275, 293)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(83, 119)
        Me.Button2.TabIndex = 36
        Me.Button2.Text = "Settings"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 427)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.btnDeleteCon)
        Me.Controls.Add(Me.btnModifCon)
        Me.Controls.Add(Me.btnCreateCon)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Makmur Group"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gbAscend.ResumeLayout(False)
        Me.gbAscend.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents btnCreateCon As Button
    Friend WithEvents btnModifCon As Button
    Friend WithEvents btnDeleteCon As Button
    Friend WithEvents btnLogin As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtPwd As TextBox
    Friend WithEvents txtUser As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtDb As TextBox
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtHost As TextBox
    Friend WithEvents cbServer As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents chkAscendLogin As CheckBox
    Friend WithEvents tbAscendDB As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents chkInjectAscend As CheckBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents gbAscend As GroupBox
    Friend WithEvents tbAscendHost As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents tbAscendDBPwd As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents tbAscendDBUser As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents tbAscendDBData As TextBox
    Friend WithEvents chkCopyProfiles As CheckBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button2 As Button
End Class
