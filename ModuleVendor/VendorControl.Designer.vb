<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VendorControl
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
        Me.gbMail = New System.Windows.Forms.GroupBox()
        Me.cbMailCountry = New System.Windows.Forms.ComboBox()
        Me.cbMailState = New System.Windows.Forms.ComboBox()
        Me.cbMailCity = New System.Windows.Forms.ComboBox()
        Me.tbMailAddr = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbTaxCountry = New System.Windows.Forms.ComboBox()
        Me.cbTaxState = New System.Windows.Forms.ComboBox()
        Me.tbNpwp = New System.Windows.Forms.MaskedTextBox()
        Me.cbTaxCity = New System.Windows.Forms.ComboBox()
        Me.tbTaxAddr = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbPhone = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbEmail = New System.Windows.Forms.TextBox()
        Me.cbType = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbCode = New System.Windows.Forms.Label()
        Me.tbCode = New System.Windows.Forms.TextBox()
        Me.tbName = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.tpVenList = New System.Windows.Forms.TabPage()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.tbSearch = New System.Windows.Forms.TextBox()
        Me.lstVen = New System.Windows.Forms.ListView()
        Me.chCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chVen = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chCity = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tpVenDetail = New System.Windows.Forms.TabPage()
        Me.gbTax = New System.Windows.Forms.GroupBox()
        Me.cbTax = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbName = New System.Windows.Forms.Label()
        Me.gbMail.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.tpVenList.SuspendLayout()
        Me.tpVenDetail.SuspendLayout()
        Me.gbTax.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbMail
        '
        Me.gbMail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbMail.Controls.Add(Me.cbMailCountry)
        Me.gbMail.Controls.Add(Me.cbMailState)
        Me.gbMail.Controls.Add(Me.cbMailCity)
        Me.gbMail.Controls.Add(Me.tbMailAddr)
        Me.gbMail.Controls.Add(Me.Label1)
        Me.gbMail.Controls.Add(Me.Label2)
        Me.gbMail.Controls.Add(Me.Label3)
        Me.gbMail.Controls.Add(Me.Label4)
        Me.gbMail.Location = New System.Drawing.Point(6, 122)
        Me.gbMail.Name = "gbMail"
        Me.gbMail.Size = New System.Drawing.Size(466, 249)
        Me.gbMail.TabIndex = 24
        Me.gbMail.TabStop = False
        Me.gbMail.Text = "Mailing"
        '
        'cbMailCountry
        '
        Me.cbMailCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMailCountry.FormattingEnabled = True
        Me.cbMailCountry.Location = New System.Drawing.Point(69, 118)
        Me.cbMailCountry.Name = "cbMailCountry"
        Me.cbMailCountry.Size = New System.Drawing.Size(388, 23)
        Me.cbMailCountry.TabIndex = 14
        '
        'cbMailState
        '
        Me.cbMailState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMailState.FormattingEnabled = True
        Me.cbMailState.Location = New System.Drawing.Point(69, 147)
        Me.cbMailState.Name = "cbMailState"
        Me.cbMailState.Size = New System.Drawing.Size(388, 23)
        Me.cbMailState.TabIndex = 13
        '
        'cbMailCity
        '
        Me.cbMailCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMailCity.FormattingEnabled = True
        Me.cbMailCity.Location = New System.Drawing.Point(69, 176)
        Me.cbMailCity.Name = "cbMailCity"
        Me.cbMailCity.Size = New System.Drawing.Size(388, 23)
        Me.cbMailCity.TabIndex = 12
        '
        'tbMailAddr
        '
        Me.tbMailAddr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbMailAddr.Location = New System.Drawing.Point(69, 18)
        Me.tbMailAddr.Multiline = True
        Me.tbMailAddr.Name = "tbMailAddr"
        Me.tbMailAddr.Size = New System.Drawing.Size(388, 94)
        Me.tbMailAddr.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 15)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Address"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 179)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 15)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "City"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "State"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 15)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Country"
        '
        'cbTaxCountry
        '
        Me.cbTaxCountry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTaxCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTaxCountry.FormattingEnabled = True
        Me.cbTaxCountry.Location = New System.Drawing.Point(69, 176)
        Me.cbTaxCountry.Name = "cbTaxCountry"
        Me.cbTaxCountry.Size = New System.Drawing.Size(384, 23)
        Me.cbTaxCountry.TabIndex = 17
        '
        'cbTaxState
        '
        Me.cbTaxState.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTaxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTaxState.FormattingEnabled = True
        Me.cbTaxState.Location = New System.Drawing.Point(69, 205)
        Me.cbTaxState.Name = "cbTaxState"
        Me.cbTaxState.Size = New System.Drawing.Size(384, 23)
        Me.cbTaxState.TabIndex = 16
        '
        'tbNpwp
        '
        Me.tbNpwp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbNpwp.Location = New System.Drawing.Point(69, 47)
        Me.tbNpwp.Mask = "99-999-999-9-999-999"
        Me.tbNpwp.Name = "tbNpwp"
        Me.tbNpwp.Size = New System.Drawing.Size(384, 23)
        Me.tbNpwp.TabIndex = 23
        '
        'cbTaxCity
        '
        Me.cbTaxCity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTaxCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTaxCity.FormattingEnabled = True
        Me.cbTaxCity.Location = New System.Drawing.Point(69, 234)
        Me.cbTaxCity.Name = "cbTaxCity"
        Me.cbTaxCity.Size = New System.Drawing.Size(384, 23)
        Me.cbTaxCity.TabIndex = 15
        '
        'tbTaxAddr
        '
        Me.tbTaxAddr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbTaxAddr.Location = New System.Drawing.Point(69, 76)
        Me.tbTaxAddr.Multiline = True
        Me.tbTaxAddr.Name = "tbTaxAddr"
        Me.tbTaxAddr.Size = New System.Drawing.Size(384, 94)
        Me.tbTaxAddr.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 79)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(51, 15)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Address"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(8, 237)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(28, 15)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "City"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 208)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 15)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "State"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(270, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 15)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Phone"
        '
        'tbPhone
        '
        Me.tbPhone.Location = New System.Drawing.Point(317, 64)
        Me.tbPhone.Name = "tbPhone"
        Me.tbPhone.Size = New System.Drawing.Size(155, 23)
        Me.tbPhone.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 15)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Email"
        '
        'tbEmail
        '
        Me.tbEmail.Location = New System.Drawing.Point(70, 64)
        Me.tbEmail.Name = "tbEmail"
        Me.tbEmail.Size = New System.Drawing.Size(179, 23)
        Me.tbEmail.TabIndex = 19
        '
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(70, 93)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(402, 23)
        Me.cbType.TabIndex = 18
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 15)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Type"
        '
        'lbCode
        '
        Me.lbCode.AutoSize = True
        Me.lbCode.Location = New System.Drawing.Point(6, 38)
        Me.lbCode.Name = "lbCode"
        Me.lbCode.Size = New System.Drawing.Size(34, 15)
        Me.lbCode.TabIndex = 3
        Me.lbCode.Text = "Code"
        '
        'tbCode
        '
        Me.tbCode.Location = New System.Drawing.Point(70, 35)
        Me.tbCode.Name = "tbCode"
        Me.tbCode.Size = New System.Drawing.Size(402, 23)
        Me.tbCode.TabIndex = 2
        '
        'tbName
        '
        Me.tbName.Location = New System.Drawing.Point(70, 6)
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(402, 23)
        Me.tbName.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 179)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 15)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Country"
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.tpVenList)
        Me.tcMain.Controls.Add(Me.tpVenDetail)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(956, 405)
        Me.tcMain.TabIndex = 2
        '
        'tpVenList
        '
        Me.tpVenList.Controls.Add(Me.btnDelete)
        Me.tpVenList.Controls.Add(Me.btnEdit)
        Me.tpVenList.Controls.Add(Me.btnCreate)
        Me.tpVenList.Controls.Add(Me.Label32)
        Me.tpVenList.Controls.Add(Me.tbSearch)
        Me.tpVenList.Controls.Add(Me.lstVen)
        Me.tpVenList.Location = New System.Drawing.Point(4, 24)
        Me.tpVenList.Name = "tpVenList"
        Me.tpVenList.Padding = New System.Windows.Forms.Padding(3)
        Me.tpVenList.Size = New System.Drawing.Size(948, 377)
        Me.tpVenList.TabIndex = 0
        Me.tpVenList.Text = "Vendor List"
        Me.tpVenList.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(168, 348)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 10
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.Location = New System.Drawing.Point(87, 348)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 9
        Me.btnEdit.Text = "Modify"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCreate.Location = New System.Drawing.Point(6, 348)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(75, 23)
        Me.btnCreate.TabIndex = 8
        Me.btnCreate.Text = "Create"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(4, 9)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(44, 15)
        Me.Label32.TabIndex = 7
        Me.Label32.Text = "Search"
        '
        'tbSearch
        '
        Me.tbSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSearch.Location = New System.Drawing.Point(57, 6)
        Me.tbSearch.Name = "tbSearch"
        Me.tbSearch.Size = New System.Drawing.Size(883, 23)
        Me.tbSearch.TabIndex = 6
        '
        'lstVen
        '
        Me.lstVen.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstVen.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chCode, Me.chVen, Me.chType, Me.chCity})
        Me.lstVen.FullRowSelect = True
        Me.lstVen.HideSelection = False
        Me.lstVen.Location = New System.Drawing.Point(6, 35)
        Me.lstVen.MultiSelect = False
        Me.lstVen.Name = "lstVen"
        Me.lstVen.Size = New System.Drawing.Size(934, 307)
        Me.lstVen.TabIndex = 0
        Me.lstVen.UseCompatibleStateImageBehavior = False
        Me.lstVen.View = System.Windows.Forms.View.Details
        '
        'chCode
        '
        Me.chCode.Text = "Code"
        Me.chCode.Width = 100
        '
        'chVen
        '
        Me.chVen.Text = "Vendor"
        Me.chVen.Width = 160
        '
        'chType
        '
        Me.chType.Text = "Type"
        Me.chType.Width = 100
        '
        'chCity
        '
        Me.chCity.Text = "City"
        Me.chCity.Width = 100
        '
        'tpVenDetail
        '
        Me.tpVenDetail.Controls.Add(Me.gbTax)
        Me.tpVenDetail.Controls.Add(Me.gbMail)
        Me.tpVenDetail.Controls.Add(Me.Label9)
        Me.tpVenDetail.Controls.Add(Me.tbPhone)
        Me.tpVenDetail.Controls.Add(Me.Label8)
        Me.tpVenDetail.Controls.Add(Me.tbEmail)
        Me.tpVenDetail.Controls.Add(Me.cbType)
        Me.tpVenDetail.Controls.Add(Me.Label7)
        Me.tpVenDetail.Controls.Add(Me.lbCode)
        Me.tpVenDetail.Controls.Add(Me.tbCode)
        Me.tpVenDetail.Controls.Add(Me.lbName)
        Me.tpVenDetail.Controls.Add(Me.tbName)
        Me.tpVenDetail.Location = New System.Drawing.Point(4, 24)
        Me.tpVenDetail.Name = "tpVenDetail"
        Me.tpVenDetail.Padding = New System.Windows.Forms.Padding(3)
        Me.tpVenDetail.Size = New System.Drawing.Size(948, 377)
        Me.tpVenDetail.TabIndex = 1
        Me.tpVenDetail.Text = "Vendor Detail"
        Me.tpVenDetail.UseVisualStyleBackColor = True
        '
        'gbTax
        '
        Me.gbTax.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTax.Controls.Add(Me.cbTaxCountry)
        Me.gbTax.Controls.Add(Me.cbTaxState)
        Me.gbTax.Controls.Add(Me.tbNpwp)
        Me.gbTax.Controls.Add(Me.cbTaxCity)
        Me.gbTax.Controls.Add(Me.tbTaxAddr)
        Me.gbTax.Controls.Add(Me.Label10)
        Me.gbTax.Controls.Add(Me.Label11)
        Me.gbTax.Controls.Add(Me.Label12)
        Me.gbTax.Controls.Add(Me.Label13)
        Me.gbTax.Controls.Add(Me.cbTax)
        Me.gbTax.Controls.Add(Me.Label6)
        Me.gbTax.Controls.Add(Me.Label5)
        Me.gbTax.Location = New System.Drawing.Point(479, 6)
        Me.gbTax.Name = "gbTax"
        Me.gbTax.Size = New System.Drawing.Size(462, 365)
        Me.gbTax.TabIndex = 25
        Me.gbTax.TabStop = False
        Me.gbTax.Text = "Tax"
        '
        'cbTax
        '
        Me.cbTax.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTax.FormattingEnabled = True
        Me.cbTax.Location = New System.Drawing.Point(69, 18)
        Me.cbTax.Name = "cbTax"
        Me.cbTax.Size = New System.Drawing.Size(384, 23)
        Me.cbTax.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 15)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "NPWP"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(25, 15)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Tax"
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Location = New System.Drawing.Point(6, 9)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(38, 15)
        Me.lbName.TabIndex = 1
        Me.lbName.Text = "Name"
        '
        'VendorControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tcMain)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "VendorControl"
        Me.Size = New System.Drawing.Size(956, 405)
        Me.gbMail.ResumeLayout(False)
        Me.gbMail.PerformLayout()
        Me.tcMain.ResumeLayout(False)
        Me.tpVenList.ResumeLayout(False)
        Me.tpVenList.PerformLayout()
        Me.tpVenDetail.ResumeLayout(False)
        Me.tpVenDetail.PerformLayout()
        Me.gbTax.ResumeLayout(False)
        Me.gbTax.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gbMail As System.Windows.Forms.GroupBox
    Friend WithEvents cbMailCountry As System.Windows.Forms.ComboBox
    Friend WithEvents cbMailState As System.Windows.Forms.ComboBox
    Friend WithEvents cbMailCity As System.Windows.Forms.ComboBox
    Friend WithEvents tbMailAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbTaxCountry As System.Windows.Forms.ComboBox
    Friend WithEvents cbTaxState As System.Windows.Forms.ComboBox
    Friend WithEvents tbNpwp As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cbTaxCity As System.Windows.Forms.ComboBox
    Friend WithEvents tbTaxAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tbPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbEmail As System.Windows.Forms.TextBox
    Friend WithEvents cbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbCode As System.Windows.Forms.Label
    Friend WithEvents tbCode As System.Windows.Forms.TextBox
    Friend WithEvents tbName As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tcMain As System.Windows.Forms.TabControl
    Friend WithEvents tpVenList As System.Windows.Forms.TabPage
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents tbSearch As System.Windows.Forms.TextBox
    Friend WithEvents lstVen As System.Windows.Forms.ListView
    Friend WithEvents chCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents chVen As System.Windows.Forms.ColumnHeader
    Friend WithEvents chType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chCity As System.Windows.Forms.ColumnHeader
    Friend WithEvents tpVenDetail As System.Windows.Forms.TabPage
    Friend WithEvents gbTax As System.Windows.Forms.GroupBox
    Friend WithEvents cbTax As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbName As System.Windows.Forms.Label
End Class
