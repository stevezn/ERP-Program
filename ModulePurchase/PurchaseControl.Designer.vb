<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PurchaseControl
    Inherits System.Windows.Forms.UserControl

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
        Me.chkMonthFilter = New System.Windows.Forms.CheckBox()
        Me.dpDateFilter = New System.Windows.Forms.DateTimePicker()
        Me.lbSearch = New System.Windows.Forms.Label()
        Me.tbSearchPO = New System.Windows.Forms.TextBox()
        Me.lstSO = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader18 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader19 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader20 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader21 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader22 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader23 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader24 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader25 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader26 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tcItems = New System.Windows.Forms.TabControl()
        Me.tpItem = New System.Windows.Forms.TabPage()
        Me.tbRemark = New System.Windows.Forms.TextBox()
        Me.lstSOItem = New System.Windows.Forms.ListView()
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader37 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbItemSearch = New System.Windows.Forms.TextBox()
        Me.lbItem = New System.Windows.Forms.Label()
        Me.lbQuantity = New System.Windows.Forms.Label()
        Me.tbQuantity = New System.Windows.Forms.NumericUpDown()
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.btnDelItem = New System.Windows.Forms.Button()
        Me.lbTotalAmt = New System.Windows.Forms.Label()
        Me.lbTotalWithTax = New System.Windows.Forms.Label()
        Me.lbTaxAmt = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.tbRate = New System.Windows.Forms.TextBox()
        Me.lbRate = New System.Windows.Forms.Label()
        Me.lbTotalAfterTax = New System.Windows.Forms.Label()
        Me.lbTax = New System.Windows.Forms.Label()
        Me.lbTotal = New System.Windows.Forms.Label()
        Me.cbCurrency = New System.Windows.Forms.ComboBox()
        Me.lbCurrency = New System.Windows.Forms.Label()
        Me.cbSOType = New System.Windows.Forms.ComboBox()
        Me.taxPanel = New System.Windows.Forms.Panel()
        Me.rbTaxExclusive = New System.Windows.Forms.RadioButton()
        Me.rbTaxInclusive = New System.Windows.Forms.RadioButton()
        Me.lbTaxMode = New System.Windows.Forms.Label()
        Me.lbExpectDate = New System.Windows.Forms.Label()
        Me.dpExpect = New System.Windows.Forms.DateTimePicker()
        Me.lbDays = New System.Windows.Forms.Label()
        Me.lbTOP = New System.Windows.Forms.Label()
        Me.lbTerm = New System.Windows.Forms.Label()
        Me.lbPODate = New System.Windows.Forms.Label()
        Me.dpSO = New System.Windows.Forms.DateTimePicker()
        Me.tbCust = New System.Windows.Forms.TextBox()
        Me.lbVendor = New System.Windows.Forms.Label()
        Me.tbSONum = New System.Windows.Forms.TextBox()
        Me.lbPONum = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.tcItems.SuspendLayout()
        Me.tpItem.SuspendLayout()
        CType(Me.tbQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.taxPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkMonthFilter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dpDateFilter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbSearch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.tbSearchPO)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstSO)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tcItems)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTotalAmt)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTotalWithTax)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTaxAmt)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbRate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbRate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTotalAfterTax)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTax)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTotal)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cbCurrency)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbCurrency)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cbSOType)
        Me.SplitContainer1.Panel2.Controls.Add(Me.taxPanel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbExpectDate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dpExpect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbDays)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTOP)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTerm)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbPODate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dpSO)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbCust)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbVendor)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbSONum)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbPONum)
        Me.SplitContainer1.Size = New System.Drawing.Size(962, 585)
        Me.SplitContainer1.SplitterDistance = 156
        Me.SplitContainer1.TabIndex = 1
        '
        'chkMonthFilter
        '
        Me.chkMonthFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkMonthFilter.AutoSize = True
        Me.chkMonthFilter.Checked = True
        Me.chkMonthFilter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMonthFilter.Location = New System.Drawing.Point(750, 5)
        Me.chkMonthFilter.Name = "chkMonthFilter"
        Me.chkMonthFilter.Size = New System.Drawing.Size(62, 19)
        Me.chkMonthFilter.TabIndex = 1
        Me.chkMonthFilter.Text = "Month"
        Me.chkMonthFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkMonthFilter.UseVisualStyleBackColor = True
        '
        'dpDateFilter
        '
        Me.dpDateFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dpDateFilter.CustomFormat = "MMMM yyyy"
        Me.dpDateFilter.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpDateFilter.Location = New System.Drawing.Point(818, 3)
        Me.dpDateFilter.Name = "dpDateFilter"
        Me.dpDateFilter.ShowUpDown = True
        Me.dpDateFilter.Size = New System.Drawing.Size(141, 23)
        Me.dpDateFilter.TabIndex = 2
        '
        'lbSearch
        '
        Me.lbSearch.AutoSize = True
        Me.lbSearch.Location = New System.Drawing.Point(0, 6)
        Me.lbSearch.Name = "lbSearch"
        Me.lbSearch.Size = New System.Drawing.Size(44, 15)
        Me.lbSearch.TabIndex = 7
        Me.lbSearch.Text = "Search"
        '
        'tbSearchPO
        '
        Me.tbSearchPO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSearchPO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbSearchPO.Location = New System.Drawing.Point(50, 3)
        Me.tbSearchPO.Name = "tbSearchPO"
        Me.tbSearchPO.Size = New System.Drawing.Size(680, 23)
        Me.tbSearchPO.TabIndex = 0
        '
        'lstSO
        '
        Me.lstSO.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstSO.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader18, Me.ColumnHeader19, Me.ColumnHeader20, Me.ColumnHeader21, Me.ColumnHeader22, Me.ColumnHeader23, Me.ColumnHeader24, Me.ColumnHeader25, Me.ColumnHeader26})
        Me.lstSO.FullRowSelect = True
        Me.lstSO.HideSelection = False
        Me.lstSO.Location = New System.Drawing.Point(3, 32)
        Me.lstSO.MultiSelect = False
        Me.lstSO.Name = "lstSO"
        Me.lstSO.Size = New System.Drawing.Size(956, 121)
        Me.lstSO.TabIndex = 3
        Me.lstSO.UseCompatibleStateImageBehavior = False
        Me.lstSO.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "PO Number"
        Me.ColumnHeader5.Width = 120
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Date"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Vendor"
        Me.ColumnHeader7.Width = 150
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "Request Numbers"
        Me.ColumnHeader18.Width = 150
        '
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "Net Total"
        Me.ColumnHeader19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader19.Width = 120
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.Text = "Expected Date"
        Me.ColumnHeader20.Width = 100
        '
        'ColumnHeader21
        '
        Me.ColumnHeader21.Text = "Created At"
        Me.ColumnHeader21.Width = 140
        '
        'ColumnHeader22
        '
        Me.ColumnHeader22.Text = "Created By"
        Me.ColumnHeader22.Width = 100
        '
        'ColumnHeader23
        '
        Me.ColumnHeader23.Text = "Approval"
        Me.ColumnHeader23.Width = 140
        '
        'ColumnHeader24
        '
        Me.ColumnHeader24.Text = "Last Approve Date"
        Me.ColumnHeader24.Width = 120
        '
        'ColumnHeader25
        '
        Me.ColumnHeader25.Text = "Close Date"
        Me.ColumnHeader25.Width = 100
        '
        'ColumnHeader26
        '
        Me.ColumnHeader26.Text = "Void Date"
        Me.ColumnHeader26.Width = 100
        '
        'tcItems
        '
        Me.tcItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcItems.Controls.Add(Me.tpItem)
        Me.tcItems.Location = New System.Drawing.Point(6, 119)
        Me.tcItems.Name = "tcItems"
        Me.tcItems.SelectedIndex = 0
        Me.tcItems.Size = New System.Drawing.Size(953, 261)
        Me.tcItems.TabIndex = 12
        '
        'tpItem
        '
        Me.tpItem.Controls.Add(Me.tbRemark)
        Me.tpItem.Controls.Add(Me.lstSOItem)
        Me.tpItem.Controls.Add(Me.tbItemSearch)
        Me.tpItem.Controls.Add(Me.lbItem)
        Me.tpItem.Controls.Add(Me.lbQuantity)
        Me.tpItem.Controls.Add(Me.tbQuantity)
        Me.tpItem.Controls.Add(Me.btnAddItem)
        Me.tpItem.Controls.Add(Me.btnDelItem)
        Me.tpItem.Location = New System.Drawing.Point(4, 24)
        Me.tpItem.Name = "tpItem"
        Me.tpItem.Padding = New System.Windows.Forms.Padding(3)
        Me.tpItem.Size = New System.Drawing.Size(945, 233)
        Me.tpItem.TabIndex = 0
        Me.tpItem.Text = "Items"
        Me.tpItem.UseVisualStyleBackColor = True
        '
        'tbRemark
        '
        Me.tbRemark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbRemark.ForeColor = System.Drawing.Color.Red
        Me.tbRemark.Location = New System.Drawing.Point(611, 176)
        Me.tbRemark.Multiline = True
        Me.tbRemark.Name = "tbRemark"
        Me.tbRemark.ReadOnly = True
        Me.tbRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbRemark.Size = New System.Drawing.Size(331, 51)
        Me.tbRemark.TabIndex = 30
        '
        'lstSOItem
        '
        Me.lstSOItem.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader10, Me.ColumnHeader1, Me.ColumnHeader11, Me.ColumnHeader3, Me.ColumnHeader9, Me.ColumnHeader4, Me.ColumnHeader17, Me.ColumnHeader37})
        Me.lstSOItem.FullRowSelect = True
        Me.lstSOItem.HideSelection = False
        Me.lstSOItem.Location = New System.Drawing.Point(3, 3)
        Me.lstSOItem.MultiSelect = False
        Me.lstSOItem.Name = "lstSOItem"
        Me.lstSOItem.Size = New System.Drawing.Size(942, 167)
        Me.lstSOItem.TabIndex = 3
        Me.lstSOItem.UseCompatibleStateImageBehavior = False
        Me.lstSOItem.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Item Code"
        Me.ColumnHeader10.Width = 100
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Item Name"
        Me.ColumnHeader1.Width = 150
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Sales Price"
        Me.ColumnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader11.Width = 100
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Quantity"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader3.Width = 100
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "UOM"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Item Total"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader4.Width = 150
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "Item Total Before tax"
        Me.ColumnHeader17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader17.Width = 150
        '
        'ColumnHeader37
        '
        Me.ColumnHeader37.Text = "Note"
        Me.ColumnHeader37.Width = 200
        '
        'tbItemSearch
        '
        Me.tbItemSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbItemSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbItemSearch.Location = New System.Drawing.Point(63, 176)
        Me.tbItemSearch.Name = "tbItemSearch"
        Me.tbItemSearch.Size = New System.Drawing.Size(215, 23)
        Me.tbItemSearch.TabIndex = 0
        '
        'lbItem
        '
        Me.lbItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbItem.AutoSize = True
        Me.lbItem.Location = New System.Drawing.Point(0, 179)
        Me.lbItem.Name = "lbItem"
        Me.lbItem.Size = New System.Drawing.Size(31, 15)
        Me.lbItem.TabIndex = 20
        Me.lbItem.Text = "Item"
        '
        'lbQuantity
        '
        Me.lbQuantity.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbQuantity.AutoSize = True
        Me.lbQuantity.Location = New System.Drawing.Point(0, 207)
        Me.lbQuantity.Name = "lbQuantity"
        Me.lbQuantity.Size = New System.Drawing.Size(54, 15)
        Me.lbQuantity.TabIndex = 22
        Me.lbQuantity.Text = "Quantity"
        '
        'tbQuantity
        '
        Me.tbQuantity.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbQuantity.Location = New System.Drawing.Point(63, 205)
        Me.tbQuantity.Name = "tbQuantity"
        Me.tbQuantity.Size = New System.Drawing.Size(105, 23)
        Me.tbQuantity.TabIndex = 1
        Me.tbQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbQuantity.ThousandsSeparator = True
        '
        'btnAddItem
        '
        Me.btnAddItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddItem.Location = New System.Drawing.Point(284, 175)
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Size = New System.Drawing.Size(102, 25)
        Me.btnAddItem.TabIndex = 2
        Me.btnAddItem.Text = "Add Item"
        Me.btnAddItem.UseVisualStyleBackColor = True
        '
        'btnDelItem
        '
        Me.btnDelItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelItem.Location = New System.Drawing.Point(284, 203)
        Me.btnDelItem.Name = "btnDelItem"
        Me.btnDelItem.Size = New System.Drawing.Size(102, 25)
        Me.btnDelItem.TabIndex = 26
        Me.btnDelItem.Text = "Delete Item"
        Me.btnDelItem.UseVisualStyleBackColor = True
        '
        'lbTotalAmt
        '
        Me.lbTotalAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTotalAmt.Location = New System.Drawing.Point(836, 383)
        Me.lbTotalAmt.Name = "lbTotalAmt"
        Me.lbTotalAmt.Size = New System.Drawing.Size(119, 14)
        Me.lbTotalAmt.TabIndex = 42
        Me.lbTotalAmt.Text = "0"
        Me.lbTotalAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalWithTax
        '
        Me.lbTotalWithTax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTotalWithTax.Location = New System.Drawing.Point(836, 399)
        Me.lbTotalWithTax.Name = "lbTotalWithTax"
        Me.lbTotalWithTax.Size = New System.Drawing.Size(119, 14)
        Me.lbTotalWithTax.TabIndex = 41
        Me.lbTotalWithTax.Text = "0"
        Me.lbTotalWithTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTaxAmt
        '
        Me.lbTaxAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTaxAmt.Location = New System.Drawing.Point(614, 399)
        Me.lbTaxAmt.Name = "lbTaxAmt"
        Me.lbTaxAmt.Size = New System.Drawing.Size(88, 14)
        Me.lbTaxAmt.TabIndex = 40
        Me.lbTaxAmt.Text = "0"
        Me.lbTaxAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.Tomato
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte), True)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(855, 61)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 52)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'tbRate
        '
        Me.tbRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbRate.Location = New System.Drawing.Point(517, 32)
        Me.tbRate.Name = "tbRate"
        Me.tbRate.Size = New System.Drawing.Size(129, 23)
        Me.tbRate.TabIndex = 10
        '
        'lbRate
        '
        Me.lbRate.AutoSize = True
        Me.lbRate.Location = New System.Drawing.Point(480, 35)
        Me.lbRate.Name = "lbRate"
        Me.lbRate.Size = New System.Drawing.Size(31, 15)
        Me.lbRate.TabIndex = 36
        Me.lbRate.Text = "Rate"
        '
        'lbTotalAfterTax
        '
        Me.lbTotalAfterTax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTotalAfterTax.AutoSize = True
        Me.lbTotalAfterTax.Location = New System.Drawing.Point(733, 399)
        Me.lbTotalAfterTax.Name = "lbTotalAfterTax"
        Me.lbTotalAfterTax.Size = New System.Drawing.Size(84, 15)
        Me.lbTotalAfterTax.TabIndex = 35
        Me.lbTotalAfterTax.Text = "Total after Tax"
        '
        'lbTax
        '
        Me.lbTax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTax.AutoSize = True
        Me.lbTax.Location = New System.Drawing.Point(574, 399)
        Me.lbTax.Name = "lbTax"
        Me.lbTax.Size = New System.Drawing.Size(25, 15)
        Me.lbTax.TabIndex = 34
        Me.lbTax.Text = "Tax"
        '
        'lbTotal
        '
        Me.lbTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTotal.AutoSize = True
        Me.lbTotal.Location = New System.Drawing.Point(783, 383)
        Me.lbTotal.Name = "lbTotal"
        Me.lbTotal.Size = New System.Drawing.Size(34, 15)
        Me.lbTotal.TabIndex = 33
        Me.lbTotal.Text = "Total"
        '
        'cbCurrency
        '
        Me.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCurrency.FormattingEnabled = True
        Me.cbCurrency.Location = New System.Drawing.Point(396, 32)
        Me.cbCurrency.Name = "cbCurrency"
        Me.cbCurrency.Size = New System.Drawing.Size(64, 23)
        Me.cbCurrency.TabIndex = 9
        '
        'lbCurrency
        '
        Me.lbCurrency.AutoSize = True
        Me.lbCurrency.Location = New System.Drawing.Point(313, 36)
        Me.lbCurrency.Name = "lbCurrency"
        Me.lbCurrency.Size = New System.Drawing.Size(56, 15)
        Me.lbCurrency.TabIndex = 31
        Me.lbCurrency.Text = "Currency"
        '
        'cbSOType
        '
        Me.cbSOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSOType.FormattingEnabled = True
        Me.cbSOType.Location = New System.Drawing.Point(92, 3)
        Me.cbSOType.Name = "cbSOType"
        Me.cbSOType.Size = New System.Drawing.Size(74, 23)
        Me.cbSOType.TabIndex = 0
        '
        'taxPanel
        '
        Me.taxPanel.Controls.Add(Me.rbTaxExclusive)
        Me.taxPanel.Controls.Add(Me.rbTaxInclusive)
        Me.taxPanel.Controls.Add(Me.lbTaxMode)
        Me.taxPanel.Enabled = False
        Me.taxPanel.Location = New System.Drawing.Point(313, 91)
        Me.taxPanel.Name = "taxPanel"
        Me.taxPanel.Size = New System.Drawing.Size(333, 25)
        Me.taxPanel.TabIndex = 11
        '
        'rbTaxExclusive
        '
        Me.rbTaxExclusive.AutoSize = True
        Me.rbTaxExclusive.Checked = True
        Me.rbTaxExclusive.Location = New System.Drawing.Point(204, 3)
        Me.rbTaxExclusive.Name = "rbTaxExclusive"
        Me.rbTaxExclusive.Size = New System.Drawing.Size(76, 19)
        Me.rbTaxExclusive.TabIndex = 1
        Me.rbTaxExclusive.TabStop = True
        Me.rbTaxExclusive.Text = "Exclusive"
        Me.rbTaxExclusive.UseVisualStyleBackColor = True
        '
        'rbTaxInclusive
        '
        Me.rbTaxInclusive.AutoSize = True
        Me.rbTaxInclusive.Location = New System.Drawing.Point(83, 4)
        Me.rbTaxInclusive.Name = "rbTaxInclusive"
        Me.rbTaxInclusive.Size = New System.Drawing.Size(75, 19)
        Me.rbTaxInclusive.TabIndex = 0
        Me.rbTaxInclusive.Text = "Inclusive"
        Me.rbTaxInclusive.UseVisualStyleBackColor = True
        '
        'lbTaxMode
        '
        Me.lbTaxMode.AutoSize = True
        Me.lbTaxMode.Location = New System.Drawing.Point(1, 6)
        Me.lbTaxMode.Name = "lbTaxMode"
        Me.lbTaxMode.Size = New System.Drawing.Size(25, 15)
        Me.lbTaxMode.TabIndex = 14
        Me.lbTaxMode.Text = "Tax"
        '
        'lbExpectDate
        '
        Me.lbExpectDate.AutoSize = True
        Me.lbExpectDate.Location = New System.Drawing.Point(4, 65)
        Me.lbExpectDate.Name = "lbExpectDate"
        Me.lbExpectDate.Size = New System.Drawing.Size(64, 15)
        Me.lbExpectDate.TabIndex = 17
        Me.lbExpectDate.Text = "E.T. Arrival"
        '
        'dpExpect
        '
        Me.dpExpect.Location = New System.Drawing.Point(92, 61)
        Me.dpExpect.Name = "dpExpect"
        Me.dpExpect.Size = New System.Drawing.Size(196, 23)
        Me.dpExpect.TabIndex = 8
        '
        'lbDays
        '
        Me.lbDays.AutoSize = True
        Me.lbDays.Location = New System.Drawing.Point(454, 65)
        Me.lbDays.Name = "lbDays"
        Me.lbDays.Size = New System.Drawing.Size(33, 15)
        Me.lbDays.TabIndex = 10
        Me.lbDays.Text = "days"
        '
        'lbTOP
        '
        Me.lbTOP.AutoSize = True
        Me.lbTOP.Location = New System.Drawing.Point(419, 65)
        Me.lbTOP.Name = "lbTOP"
        Me.lbTOP.Size = New System.Drawing.Size(11, 15)
        Me.lbTOP.TabIndex = 9
        Me.lbTOP.Text = "-"
        '
        'lbTerm
        '
        Me.lbTerm.AutoSize = True
        Me.lbTerm.Location = New System.Drawing.Point(313, 65)
        Me.lbTerm.Name = "lbTerm"
        Me.lbTerm.Size = New System.Drawing.Size(97, 15)
        Me.lbTerm.TabIndex = 8
        Me.lbTerm.Text = "Term of Payment"
        '
        'lbPODate
        '
        Me.lbPODate.AutoSize = True
        Me.lbPODate.Location = New System.Drawing.Point(4, 35)
        Me.lbPODate.Name = "lbPODate"
        Me.lbPODate.Size = New System.Drawing.Size(51, 15)
        Me.lbPODate.TabIndex = 7
        Me.lbPODate.Text = "PO Date"
        '
        'dpSO
        '
        Me.dpSO.Location = New System.Drawing.Point(92, 32)
        Me.dpSO.Name = "dpSO"
        Me.dpSO.Size = New System.Drawing.Size(196, 23)
        Me.dpSO.TabIndex = 3
        '
        'tbCust
        '
        Me.tbCust.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbCust.Location = New System.Drawing.Point(396, 3)
        Me.tbCust.Name = "tbCust"
        Me.tbCust.Size = New System.Drawing.Size(250, 23)
        Me.tbCust.TabIndex = 2
        '
        'lbVendor
        '
        Me.lbVendor.AutoSize = True
        Me.lbVendor.Location = New System.Drawing.Point(313, 6)
        Me.lbVendor.Name = "lbVendor"
        Me.lbVendor.Size = New System.Drawing.Size(45, 15)
        Me.lbVendor.TabIndex = 2
        Me.lbVendor.Text = "Vendor"
        '
        'tbSONum
        '
        Me.tbSONum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbSONum.Location = New System.Drawing.Point(172, 3)
        Me.tbSONum.Name = "tbSONum"
        Me.tbSONum.ReadOnly = True
        Me.tbSONum.Size = New System.Drawing.Size(116, 23)
        Me.tbSONum.TabIndex = 1
        Me.tbSONum.Text = "NEW"
        '
        'lbPONum
        '
        Me.lbPONum.AutoSize = True
        Me.lbPONum.Location = New System.Drawing.Point(4, 6)
        Me.lbPONum.Name = "lbPONum"
        Me.lbPONum.Size = New System.Drawing.Size(69, 15)
        Me.lbPONum.TabIndex = 0
        Me.lbPONum.Text = "PO Number"
        '
        'PurchaseControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "PurchaseControl"
        Me.Size = New System.Drawing.Size(962, 585)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.tcItems.ResumeLayout(False)
        Me.tpItem.ResumeLayout(False)
        Me.tpItem.PerformLayout()
        CType(Me.tbQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.taxPanel.ResumeLayout(False)
        Me.taxPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkMonthFilter As System.Windows.Forms.CheckBox
    Friend WithEvents dpDateFilter As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbSearch As System.Windows.Forms.Label
    Friend WithEvents tbSearchPO As System.Windows.Forms.TextBox
    Friend WithEvents lstSO As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader21 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader22 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader23 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader24 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader25 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader26 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tcItems As System.Windows.Forms.TabControl
    Friend WithEvents tpItem As System.Windows.Forms.TabPage
    Friend WithEvents lstSOItem As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader37 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbItemSearch As System.Windows.Forms.TextBox
    Friend WithEvents lbItem As System.Windows.Forms.Label
    Friend WithEvents lbQuantity As System.Windows.Forms.Label
    Friend WithEvents tbQuantity As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnAddItem As System.Windows.Forms.Button
    Friend WithEvents btnDelItem As System.Windows.Forms.Button
    Friend WithEvents lbTotalAmt As System.Windows.Forms.Label
    Friend WithEvents lbTotalWithTax As System.Windows.Forms.Label
    Friend WithEvents lbTaxAmt As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents tbRate As System.Windows.Forms.TextBox
    Friend WithEvents lbRate As System.Windows.Forms.Label
    Friend WithEvents lbTotalAfterTax As System.Windows.Forms.Label
    Friend WithEvents lbTax As System.Windows.Forms.Label
    Friend WithEvents lbTotal As System.Windows.Forms.Label
    Friend WithEvents cbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents lbCurrency As System.Windows.Forms.Label
    Friend WithEvents cbSOType As System.Windows.Forms.ComboBox
    Friend WithEvents taxPanel As System.Windows.Forms.Panel
    Friend WithEvents rbTaxExclusive As System.Windows.Forms.RadioButton
    Friend WithEvents rbTaxInclusive As System.Windows.Forms.RadioButton
    Friend WithEvents lbTaxMode As System.Windows.Forms.Label
    Friend WithEvents lbExpectDate As System.Windows.Forms.Label
    Friend WithEvents dpExpect As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbDays As System.Windows.Forms.Label
    Friend WithEvents lbTOP As System.Windows.Forms.Label
    Friend WithEvents lbTerm As System.Windows.Forms.Label
    Friend WithEvents lbPODate As System.Windows.Forms.Label
    Friend WithEvents dpSO As System.Windows.Forms.DateTimePicker
    Friend WithEvents tbCust As System.Windows.Forms.TextBox
    Friend WithEvents lbVendor As System.Windows.Forms.Label
    Friend WithEvents tbSONum As System.Windows.Forms.TextBox
    Friend WithEvents lbPONum As System.Windows.Forms.Label
    Friend WithEvents tbRemark As System.Windows.Forms.TextBox
End Class
