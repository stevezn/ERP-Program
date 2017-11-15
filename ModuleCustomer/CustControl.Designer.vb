<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustControl
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.tbSearch = New System.Windows.Forms.TextBox()
        Me.lstCust = New System.Windows.Forms.ListView()
        Me.chCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chCust = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chCity = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.tbCreditLimit = New System.Windows.Forms.NumericUpDown()
        Me.lbCreditLimit = New System.Windows.Forms.Label()
        Me.cbSales = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.tbTOP = New System.Windows.Forms.NumericUpDown()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.gbTax = New System.Windows.Forms.GroupBox()
        Me.cbTaxCountry = New System.Windows.Forms.ComboBox()
        Me.cbTaxState = New System.Windows.Forms.ComboBox()
        Me.tbNpwp = New System.Windows.Forms.MaskedTextBox()
        Me.cbTaxCity = New System.Windows.Forms.ComboBox()
        Me.tbTaxAddr = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbTax = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.gbMail = New System.Windows.Forms.GroupBox()
        Me.cbMailCountry = New System.Windows.Forms.ComboBox()
        Me.cbMailState = New System.Windows.Forms.ComboBox()
        Me.cbMailCity = New System.Windows.Forms.ComboBox()
        Me.tbMailAddr = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbPhone = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbEmail = New System.Windows.Forms.TextBox()
        Me.cbType = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbCode = New System.Windows.Forms.Label()
        Me.tbCode = New System.Windows.Forms.TextBox()
        Me.lbName = New System.Windows.Forms.Label()
        Me.tbName = New System.Windows.Forms.TextBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.tbCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTOP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTax.SuspendLayout()
        Me.gbMail.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label32)
        Me.SplitContainer1.Panel1.Controls.Add(Me.tbSearch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstCust)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label17)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbCreditLimit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbCreditLimit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cbSales)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label16)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label15)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbTOP)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label14)
        Me.SplitContainer1.Panel2.Controls.Add(Me.gbTax)
        Me.SplitContainer1.Panel2.Controls.Add(Me.gbMail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label9)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbPhone)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbEmail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cbType)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbCode)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbCode)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbName)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbName)
        Me.SplitContainer1.Size = New System.Drawing.Size(1052, 538)
        Me.SplitContainer1.SplitterDistance = 144
        Me.SplitContainer1.TabIndex = 2
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(0, 6)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(44, 15)
        Me.Label32.TabIndex = 10
        Me.Label32.Text = "Search"
        '
        'tbSearch
        '
        Me.tbSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSearch.Location = New System.Drawing.Point(48, 3)
        Me.tbSearch.Name = "tbSearch"
        Me.tbSearch.Size = New System.Drawing.Size(1001, 23)
        Me.tbSearch.TabIndex = 9
        '
        'lstCust
        '
        Me.lstCust.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstCust.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chCode, Me.chCust, Me.chType, Me.chCity})
        Me.lstCust.FullRowSelect = True
        Me.lstCust.HideSelection = False
        Me.lstCust.Location = New System.Drawing.Point(3, 32)
        Me.lstCust.MultiSelect = False
        Me.lstCust.Name = "lstCust"
        Me.lstCust.Size = New System.Drawing.Size(1046, 109)
        Me.lstCust.TabIndex = 8
        Me.lstCust.UseCompatibleStateImageBehavior = False
        Me.lstCust.View = System.Windows.Forms.View.Details
        '
        'chCode
        '
        Me.chCode.Text = "Code"
        Me.chCode.Width = 100
        '
        'chCust
        '
        Me.chCust.Text = "Customer"
        Me.chCust.Width = 160
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
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(5, 151)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(90, 15)
        Me.Label17.TabIndex = 62
        Me.Label17.Text = "Contact Person"
        '
        'TextBox1
        '
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Location = New System.Drawing.Point(107, 148)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(397, 23)
        Me.TextBox1.TabIndex = 61
        '
        'tbCreditLimit
        '
        Me.tbCreditLimit.Location = New System.Drawing.Point(350, 119)
        Me.tbCreditLimit.Name = "tbCreditLimit"
        Me.tbCreditLimit.Size = New System.Drawing.Size(154, 23)
        Me.tbCreditLimit.TabIndex = 60
        Me.tbCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbCreditLimit.ThousandsSeparator = True
        '
        'lbCreditLimit
        '
        Me.lbCreditLimit.AutoSize = True
        Me.lbCreditLimit.Location = New System.Drawing.Point(277, 121)
        Me.lbCreditLimit.Name = "lbCreditLimit"
        Me.lbCreditLimit.Size = New System.Drawing.Size(70, 15)
        Me.lbCreditLimit.TabIndex = 59
        Me.lbCreditLimit.Text = "Credit Limit"
        '
        'cbSales
        '
        Me.cbSales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSales.FormattingEnabled = True
        Me.cbSales.Location = New System.Drawing.Point(350, 90)
        Me.cbSales.Name = "cbSales"
        Me.cbSales.Size = New System.Drawing.Size(154, 23)
        Me.cbSales.TabIndex = 58
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(271, 93)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 15)
        Me.Label16.TabIndex = 57
        Me.Label16.Text = "Sales Person"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(186, 121)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(33, 15)
        Me.Label15.TabIndex = 56
        Me.Label15.Text = "days"
        '
        'tbTOP
        '
        Me.tbTOP.Location = New System.Drawing.Point(107, 119)
        Me.tbTOP.Maximum = New Decimal(New Integer() {365, 0, 0, 0})
        Me.tbTOP.Name = "tbTOP"
        Me.tbTOP.Size = New System.Drawing.Size(73, 23)
        Me.tbTOP.TabIndex = 55
        Me.tbTOP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbTOP.ThousandsSeparator = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(5, 121)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(97, 15)
        Me.Label14.TabIndex = 54
        Me.Label14.Text = "Term of Payment"
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
        Me.gbTax.Location = New System.Drawing.Point(519, 3)
        Me.gbTax.Name = "gbTax"
        Me.gbTax.Size = New System.Drawing.Size(530, 384)
        Me.gbTax.TabIndex = 53
        Me.gbTax.TabStop = False
        Me.gbTax.Text = "Tax"
        '
        'cbTaxCountry
        '
        Me.cbTaxCountry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTaxCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTaxCountry.FormattingEnabled = True
        Me.cbTaxCountry.Location = New System.Drawing.Point(104, 176)
        Me.cbTaxCountry.Name = "cbTaxCountry"
        Me.cbTaxCountry.Size = New System.Drawing.Size(417, 23)
        Me.cbTaxCountry.TabIndex = 17
        '
        'cbTaxState
        '
        Me.cbTaxState.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTaxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTaxState.FormattingEnabled = True
        Me.cbTaxState.Location = New System.Drawing.Point(104, 205)
        Me.cbTaxState.Name = "cbTaxState"
        Me.cbTaxState.Size = New System.Drawing.Size(417, 23)
        Me.cbTaxState.TabIndex = 16
        '
        'tbNpwp
        '
        Me.tbNpwp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbNpwp.Location = New System.Drawing.Point(104, 47)
        Me.tbNpwp.Mask = "99-999-999-9-999-999"
        Me.tbNpwp.Name = "tbNpwp"
        Me.tbNpwp.Size = New System.Drawing.Size(417, 23)
        Me.tbNpwp.TabIndex = 23
        '
        'cbTaxCity
        '
        Me.cbTaxCity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTaxCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTaxCity.FormattingEnabled = True
        Me.cbTaxCity.Location = New System.Drawing.Point(104, 234)
        Me.cbTaxCity.Name = "cbTaxCity"
        Me.cbTaxCity.Size = New System.Drawing.Size(417, 23)
        Me.cbTaxCity.TabIndex = 15
        '
        'tbTaxAddr
        '
        Me.tbTaxAddr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbTaxAddr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbTaxAddr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tbTaxAddr.Location = New System.Drawing.Point(104, 76)
        Me.tbTaxAddr.Multiline = True
        Me.tbTaxAddr.Name = "tbTaxAddr"
        Me.tbTaxAddr.Size = New System.Drawing.Size(417, 94)
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
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 179)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 15)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Country"
        '
        'cbTax
        '
        Me.cbTax.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTax.FormattingEnabled = True
        Me.cbTax.Location = New System.Drawing.Point(104, 18)
        Me.cbTax.Name = "cbTax"
        Me.cbTax.Size = New System.Drawing.Size(417, 23)
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
        Me.gbMail.Location = New System.Drawing.Point(5, 177)
        Me.gbMail.Name = "gbMail"
        Me.gbMail.Size = New System.Drawing.Size(508, 210)
        Me.gbMail.TabIndex = 52
        Me.gbMail.TabStop = False
        Me.gbMail.Text = "Mailing"
        '
        'cbMailCountry
        '
        Me.cbMailCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMailCountry.FormattingEnabled = True
        Me.cbMailCountry.Location = New System.Drawing.Point(102, 118)
        Me.cbMailCountry.Name = "cbMailCountry"
        Me.cbMailCountry.Size = New System.Drawing.Size(397, 23)
        Me.cbMailCountry.TabIndex = 14
        '
        'cbMailState
        '
        Me.cbMailState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMailState.FormattingEnabled = True
        Me.cbMailState.Location = New System.Drawing.Point(102, 147)
        Me.cbMailState.Name = "cbMailState"
        Me.cbMailState.Size = New System.Drawing.Size(397, 23)
        Me.cbMailState.TabIndex = 13
        '
        'cbMailCity
        '
        Me.cbMailCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMailCity.FormattingEnabled = True
        Me.cbMailCity.Location = New System.Drawing.Point(102, 176)
        Me.cbMailCity.Name = "cbMailCity"
        Me.cbMailCity.Size = New System.Drawing.Size(397, 23)
        Me.cbMailCity.TabIndex = 12
        '
        'tbMailAddr
        '
        Me.tbMailAddr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbMailAddr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbMailAddr.Location = New System.Drawing.Point(102, 18)
        Me.tbMailAddr.Multiline = True
        Me.tbMailAddr.Name = "tbMailAddr"
        Me.tbMailAddr.Size = New System.Drawing.Size(397, 94)
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
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(303, 64)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 15)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Phone"
        '
        'tbPhone
        '
        Me.tbPhone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbPhone.Location = New System.Drawing.Point(350, 61)
        Me.tbPhone.Name = "tbPhone"
        Me.tbPhone.Size = New System.Drawing.Size(155, 23)
        Me.tbPhone.TabIndex = 50
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 15)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "Email"
        '
        'tbEmail
        '
        Me.tbEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbEmail.Location = New System.Drawing.Point(107, 61)
        Me.tbEmail.Name = "tbEmail"
        Me.tbEmail.Size = New System.Drawing.Size(141, 23)
        Me.tbEmail.TabIndex = 48
        '
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(107, 90)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(141, 23)
        Me.cbType.TabIndex = 47
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(5, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 15)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "Type"
        '
        'lbCode
        '
        Me.lbCode.AutoSize = True
        Me.lbCode.Location = New System.Drawing.Point(5, 35)
        Me.lbCode.Name = "lbCode"
        Me.lbCode.Size = New System.Drawing.Size(34, 15)
        Me.lbCode.TabIndex = 45
        Me.lbCode.Text = "Code"
        '
        'tbCode
        '
        Me.tbCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbCode.Location = New System.Drawing.Point(107, 32)
        Me.tbCode.Name = "tbCode"
        Me.tbCode.Size = New System.Drawing.Size(398, 23)
        Me.tbCode.TabIndex = 44
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Location = New System.Drawing.Point(5, 6)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(38, 15)
        Me.lbName.TabIndex = 43
        Me.lbName.Text = "Name"
        '
        'tbName
        '
        Me.tbName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbName.Location = New System.Drawing.Point(107, 3)
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(398, 23)
        Me.tbName.TabIndex = 42
        '
        'CustControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "CustControl"
        Me.Size = New System.Drawing.Size(1052, 538)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.tbCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTOP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTax.ResumeLayout(False)
        Me.gbTax.PerformLayout()
        Me.gbMail.ResumeLayout(False)
        Me.gbMail.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents tbSearch As System.Windows.Forms.TextBox
    Friend WithEvents lstCust As System.Windows.Forms.ListView
    Friend WithEvents chCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents chCust As System.Windows.Forms.ColumnHeader
    Friend WithEvents chType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chCity As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbCreditLimit As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbCreditLimit As System.Windows.Forms.Label
    Friend WithEvents cbSales As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents tbTOP As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents gbTax As System.Windows.Forms.GroupBox
    Friend WithEvents cbTaxCountry As System.Windows.Forms.ComboBox
    Friend WithEvents cbTaxState As System.Windows.Forms.ComboBox
    Friend WithEvents tbNpwp As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cbTaxCity As System.Windows.Forms.ComboBox
    Friend WithEvents tbTaxAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbTax As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents gbMail As System.Windows.Forms.GroupBox
    Friend WithEvents cbMailCountry As System.Windows.Forms.ComboBox
    Friend WithEvents cbMailState As System.Windows.Forms.ComboBox
    Friend WithEvents cbMailCity As System.Windows.Forms.ComboBox
    Friend WithEvents tbMailAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tbPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbEmail As System.Windows.Forms.TextBox
    Friend WithEvents cbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbCode As System.Windows.Forms.Label
    Friend WithEvents tbCode As System.Windows.Forms.TextBox
    Friend WithEvents lbName As System.Windows.Forms.Label
    Friend WithEvents tbName As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
