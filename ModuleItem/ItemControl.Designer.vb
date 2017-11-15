<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemControl
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.tbSearchItem = New System.Windows.Forms.TextBox()
        Me.itemList = New System.Windows.Forms.ListView()
        Me.itemName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemCategory = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemSubCat = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemQty = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemUom = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemWeight = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemCOG = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.usable = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.produceable = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.buyable = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.sellable = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.isKarung = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstSuggest = New System.Windows.Forms.ListView()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.tbStockValue = New System.Windows.Forms.TextBox()
        Me.lbQtyUom = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbCOG = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbWeight = New System.Windows.Forms.TextBox()
        Me.tbQuantity = New System.Windows.Forms.TextBox()
        Me.tcDetail = New System.Windows.Forms.TabControl()
        Me.tabSack = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.gbJumboBag = New System.Windows.Forms.GroupBox()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown6 = New System.Windows.Forms.NumericUpDown()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.gbInnerBag = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.gbWovenBag = New System.Windows.Forms.GroupBox()
        Me.tbThick = New System.Windows.Forms.NumericUpDown()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tbLength = New System.Windows.Forms.NumericUpDown()
        Me.tbWidth = New System.Windows.Forms.NumericUpDown()
        Me.tbCapacity = New System.Windows.Forms.NumericUpDown()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.chkHasHandle = New System.Windows.Forms.CheckBox()
        Me.cbHandle = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tbSackWeight = New System.Windows.Forms.TextBox()
        Me.cbSackType = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbDenier = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cbWebbing = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.tbColor = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbBagType = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.tabPict = New System.Windows.Forms.TabPage()
        Me.pict = New System.Windows.Forms.PictureBox()
        Me.tabUOM = New System.Windows.Forms.TabPage()
        Me.tabLocation = New System.Windows.Forms.TabPage()
        Me.locList = New System.Windows.Forms.ListView()
        Me.locName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.locQty = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tabHistory = New System.Windows.Forms.TabPage()
        Me.dpHistTo = New System.Windows.Forms.DateTimePicker()
        Me.dpHistFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.histList = New System.Windows.Forms.ListView()
        Me.histDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.histBegin = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.histMake = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.histBuy = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.histUse = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.histSell = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.histEnd = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tabVendor = New System.Windows.Forms.TabPage()
        Me.tbVendorPrice = New System.Windows.Forms.NumericUpDown()
        Me.cbVendorCurrency = New System.Windows.Forms.ComboBox()
        Me.btnDelVendor = New System.Windows.Forms.Button()
        Me.btnEditVendor = New System.Windows.Forms.Button()
        Me.btnCreateVendor = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.tbVendorBrand = New System.Windows.Forms.TextBox()
        Me.vndList = New System.Windows.Forms.ListView()
        Me.vndName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.vndCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.vndBrand = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.vndPrice = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.vndCurrency = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.tbVendorCode = New System.Windows.Forms.TextBox()
        Me.tbVendorName = New System.Windows.Forms.TextBox()
        Me.tabCustomer = New System.Windows.Forms.TabPage()
        Me.tbCustPrice = New System.Windows.Forms.NumericUpDown()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.cbCustCurrency = New System.Windows.Forms.ComboBox()
        Me.btnDelCust = New System.Windows.Forms.Button()
        Me.btnEditCust = New System.Windows.Forms.Button()
        Me.btnCreateCust = New System.Windows.Forms.Button()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.custList = New System.Windows.Forms.ListView()
        Me.custName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.custCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.custPrice = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.custCurrency = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.minOrder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.tbCustCode = New System.Windows.Forms.TextBox()
        Me.tbCustName = New System.Windows.Forms.TextBox()
        Me.tbCustMinOrder = New System.Windows.Forms.TextBox()
        Me.tabFormula = New System.Windows.Forms.TabPage()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.tabMaterial = New System.Windows.Forms.TabPage()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.formulaList = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tabByProduct = New System.Windows.Forms.TabPage()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkConsume = New System.Windows.Forms.CheckBox()
        Me.chkIsSack = New System.Windows.Forms.CheckBox()
        Me.tbUnitWeight = New System.Windows.Forms.NumericUpDown()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.tbMaxStock = New System.Windows.Forms.NumericUpDown()
        Me.tbMinStock = New System.Windows.Forms.NumericUpDown()
        Me.lbMaxUom = New System.Windows.Forms.Label()
        Me.lbMinUom = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cbSubCategory = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbType = New System.Windows.Forms.ComboBox()
        Me.chkSell = New System.Windows.Forms.CheckBox()
        Me.chkBuy = New System.Windows.Forms.CheckBox()
        Me.chkMake = New System.Windows.Forms.CheckBox()
        Me.chkUse = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbUOM = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbCategory = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbCode = New System.Windows.Forms.TextBox()
        Me.tbName = New System.Windows.Forms.TextBox()
        Me.OpenDlg = New System.Windows.Forms.OpenFileDialog()
        Me.searchTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.tcDetail.SuspendLayout()
        Me.tabSack.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.gbJumboBag.SuspendLayout()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbInnerBag.SuspendLayout()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbWovenBag.SuspendLayout()
        CType(Me.tbThick, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPict.SuspendLayout()
        CType(Me.pict, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLocation.SuspendLayout()
        Me.tabHistory.SuspendLayout()
        Me.tabVendor.SuspendLayout()
        CType(Me.tbVendorPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCustomer.SuspendLayout()
        CType(Me.tbCustPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabFormula.SuspendLayout()
        Me.TabControl3.SuspendLayout()
        Me.tabMaterial.SuspendLayout()
        Me.tabByProduct.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.tbUnitWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMaxStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMinStock, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.tbSearchItem)
        Me.SplitContainer1.Panel1.Controls.Add(Me.itemList)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lstSuggest)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tcDetail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1175, 669)
        Me.SplitContainer1.SplitterDistance = 145
        Me.SplitContainer1.TabIndex = 2
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(0, 6)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(42, 15)
        Me.Label32.TabIndex = 8
        Me.Label32.Text = "Search"
        '
        'tbSearchItem
        '
        Me.tbSearchItem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSearchItem.Location = New System.Drawing.Point(50, 3)
        Me.tbSearchItem.Name = "tbSearchItem"
        Me.tbSearchItem.Size = New System.Drawing.Size(1122, 23)
        Me.tbSearchItem.TabIndex = 7
        '
        'itemList
        '
        Me.itemList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.itemList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.itemName, Me.itemCode, Me.itemCategory, Me.itemSubCat, Me.itemQty, Me.itemUom, Me.itemWeight, Me.itemCOG, Me.usable, Me.produceable, Me.buyable, Me.sellable, Me.isKarung})
        Me.itemList.FullRowSelect = True
        Me.itemList.HideSelection = False
        Me.itemList.Location = New System.Drawing.Point(3, 32)
        Me.itemList.MultiSelect = False
        Me.itemList.Name = "itemList"
        Me.itemList.Size = New System.Drawing.Size(1169, 110)
        Me.itemList.TabIndex = 6
        Me.itemList.UseCompatibleStateImageBehavior = False
        Me.itemList.View = System.Windows.Forms.View.Details
        '
        'itemName
        '
        Me.itemName.Text = "Item Name"
        Me.itemName.Width = 150
        '
        'itemCode
        '
        Me.itemCode.Text = "Item Code"
        Me.itemCode.Width = 80
        '
        'itemCategory
        '
        Me.itemCategory.Text = "Category"
        Me.itemCategory.Width = 100
        '
        'itemSubCat
        '
        Me.itemSubCat.Text = "Sub Category"
        Me.itemSubCat.Width = 100
        '
        'itemQty
        '
        Me.itemQty.Text = "Quantity"
        Me.itemQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'itemUom
        '
        Me.itemUom.Text = "UOM"
        '
        'itemWeight
        '
        Me.itemWeight.Text = "Weight"
        Me.itemWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'itemCOG
        '
        Me.itemCOG.Text = "COG"
        Me.itemCOG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.itemCOG.Width = 80
        '
        'usable
        '
        Me.usable.Text = "Useable"
        '
        'produceable
        '
        Me.produceable.Text = "Producible"
        Me.produceable.Width = 89
        '
        'buyable
        '
        Me.buyable.Text = "Buyable"
        '
        'sellable
        '
        Me.sellable.Text = "Sellable"
        '
        'isKarung
        '
        Me.isKarung.Text = "Karung"
        '
        'lstSuggest
        '
        Me.lstSuggest.Location = New System.Drawing.Point(498, 146)
        Me.lstSuggest.Name = "lstSuggest"
        Me.lstSuggest.Size = New System.Drawing.Size(339, 115)
        Me.lstSuggest.TabIndex = 8
        Me.lstSuggest.UseCompatibleStateImageBehavior = False
        Me.lstSuggest.View = System.Windows.Forms.View.Details
        Me.lstSuggest.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.Tomato
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(1074, 209)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(94, 52)
        Me.btnCancel.TabIndex = 39
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label41)
        Me.GroupBox2.Controls.Add(Me.tbStockValue)
        Me.GroupBox2.Controls.Add(Me.lbQtyUom)
        Me.GroupBox2.Controls.Add(Me.Label37)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.tbCOG)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.tbWeight)
        Me.GroupBox2.Controls.Add(Me.tbQuantity)
        Me.GroupBox2.Location = New System.Drawing.Point(498, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(339, 137)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Current Stock"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(6, 104)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(31, 15)
        Me.Label41.TabIndex = 39
        Me.Label41.Text = "COG"
        '
        'tbStockValue
        '
        Me.tbStockValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbStockValue.Location = New System.Drawing.Point(90, 47)
        Me.tbStockValue.Name = "tbStockValue"
        Me.tbStockValue.ReadOnly = True
        Me.tbStockValue.Size = New System.Drawing.Size(240, 23)
        Me.tbStockValue.TabIndex = 38
        '
        'lbQtyUom
        '
        Me.lbQtyUom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbQtyUom.AutoSize = True
        Me.lbQtyUom.Location = New System.Drawing.Point(298, 23)
        Me.lbQtyUom.Name = "lbQtyUom"
        Me.lbQtyUom.Size = New System.Drawing.Size(35, 15)
        Me.lbQtyUom.TabIndex = 37
        Me.lbQtyUom.Text = "UOM"
        '
        'Label37
        '
        Me.Label37.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(310, 77)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(20, 15)
        Me.Label37.TabIndex = 36
        Me.Label37.Text = "Kg"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 15)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Stock Value"
        '
        'tbCOG
        '
        Me.tbCOG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCOG.Location = New System.Drawing.Point(90, 101)
        Me.tbCOG.Name = "tbCOG"
        Me.tbCOG.ReadOnly = True
        Me.tbCOG.Size = New System.Drawing.Size(240, 23)
        Me.tbCOG.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 15)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Weight"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 15)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Quantity"
        '
        'tbWeight
        '
        Me.tbWeight.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbWeight.Location = New System.Drawing.Point(90, 74)
        Me.tbWeight.Name = "tbWeight"
        Me.tbWeight.ReadOnly = True
        Me.tbWeight.Size = New System.Drawing.Size(214, 23)
        Me.tbWeight.TabIndex = 5
        '
        'tbQuantity
        '
        Me.tbQuantity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbQuantity.Location = New System.Drawing.Point(90, 20)
        Me.tbQuantity.Name = "tbQuantity"
        Me.tbQuantity.ReadOnly = True
        Me.tbQuantity.Size = New System.Drawing.Size(202, 23)
        Me.tbQuantity.TabIndex = 3
        '
        'tcDetail
        '
        Me.tcDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcDetail.Controls.Add(Me.tabSack)
        Me.tcDetail.Controls.Add(Me.tabPict)
        Me.tcDetail.Controls.Add(Me.tabUOM)
        Me.tcDetail.Controls.Add(Me.tabLocation)
        Me.tcDetail.Controls.Add(Me.tabHistory)
        Me.tcDetail.Controls.Add(Me.tabVendor)
        Me.tcDetail.Controls.Add(Me.tabCustomer)
        Me.tcDetail.Controls.Add(Me.tabFormula)
        Me.tcDetail.Location = New System.Drawing.Point(3, 267)
        Me.tcDetail.Name = "tcDetail"
        Me.tcDetail.SelectedIndex = 0
        Me.tcDetail.Size = New System.Drawing.Size(1169, 250)
        Me.tcDetail.TabIndex = 5
        '
        'tabSack
        '
        Me.tabSack.Controls.Add(Me.TableLayoutPanel1)
        Me.tabSack.Controls.Add(Me.cbBagType)
        Me.tabSack.Controls.Add(Me.Label18)
        Me.tabSack.Location = New System.Drawing.Point(4, 24)
        Me.tabSack.Name = "tabSack"
        Me.tabSack.Size = New System.Drawing.Size(1161, 222)
        Me.tabSack.TabIndex = 7
        Me.tabSack.Text = "Bags"
        Me.tabSack.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 489.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 438.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.gbJumboBag, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.gbInnerBag, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.gbWovenBag, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 32)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1160, 190)
        Me.TableLayoutPanel1.TabIndex = 56
        '
        'gbJumboBag
        '
        Me.gbJumboBag.AutoSize = True
        Me.gbJumboBag.Controls.Add(Me.ComboBox4)
        Me.gbJumboBag.Controls.Add(Me.Label61)
        Me.gbJumboBag.Controls.Add(Me.CheckBox3)
        Me.gbJumboBag.Controls.Add(Me.CheckBox2)
        Me.gbJumboBag.Controls.Add(Me.CheckBox1)
        Me.gbJumboBag.Controls.Add(Me.ComboBox3)
        Me.gbJumboBag.Controls.Add(Me.Label60)
        Me.gbJumboBag.Controls.Add(Me.ComboBox2)
        Me.gbJumboBag.Controls.Add(Me.Label59)
        Me.gbJumboBag.Controls.Add(Me.ComboBox1)
        Me.gbJumboBag.Controls.Add(Me.Label58)
        Me.gbJumboBag.Controls.Add(Me.NumericUpDown4)
        Me.gbJumboBag.Controls.Add(Me.Label52)
        Me.gbJumboBag.Controls.Add(Me.Label53)
        Me.gbJumboBag.Controls.Add(Me.NumericUpDown5)
        Me.gbJumboBag.Controls.Add(Me.NumericUpDown6)
        Me.gbJumboBag.Controls.Add(Me.Label54)
        Me.gbJumboBag.Controls.Add(Me.Label55)
        Me.gbJumboBag.Controls.Add(Me.Label56)
        Me.gbJumboBag.Controls.Add(Me.Label57)
        Me.gbJumboBag.Enabled = False
        Me.gbJumboBag.Location = New System.Drawing.Point(492, 3)
        Me.gbJumboBag.Name = "gbJumboBag"
        Me.gbJumboBag.Size = New System.Drawing.Size(430, 145)
        Me.gbJumboBag.TabIndex = 56
        Me.gbJumboBag.TabStop = False
        Me.gbJumboBag.Text = "Jumbo Bag"
        '
        'ComboBox4
        '
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(251, 100)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(87, 23)
        Me.ComboBox4.TabIndex = 64
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(196, 104)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(44, 15)
        Me.Label61.TabIndex = 63
        Me.Label61.Text = "Handle"
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(344, 75)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(80, 19)
        Me.CheckBox3.TabIndex = 62
        Me.CheckBox3.Text = "Laminated"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(344, 48)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(80, 19)
        Me.CheckBox2.TabIndex = 61
        Me.CheckBox2.Text = "Laminated"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(344, 22)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(80, 19)
        Me.CheckBox1.TabIndex = 60
        Me.CheckBox1.Text = "Laminated"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ComboBox3
        '
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(251, 73)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(87, 23)
        Me.ComboBox3.TabIndex = 59
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(196, 77)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(34, 15)
        Me.Label60.TabIndex = 58
        Me.Label60.Text = "Body"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(251, 46)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(87, 23)
        Me.ComboBox2.TabIndex = 57
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(196, 50)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(46, 15)
        Me.Label59.TabIndex = 56
        Me.Label59.Text = "Bottom"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(251, 19)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(87, 23)
        Me.ComboBox1.TabIndex = 55
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(196, 22)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(26, 15)
        Me.Label58.TabIndex = 54
        Me.Label58.Text = "Top"
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericUpDown4.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown4.Location = New System.Drawing.Point(89, 73)
        Me.NumericUpDown4.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown4.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(66, 23)
        Me.NumericUpDown4.TabIndex = 53
        Me.NumericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown4.ThousandsSeparator = True
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(158, 77)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(22, 15)
        Me.Label52.TabIndex = 52
        Me.Label52.Text = "cm"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(7, 77)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(41, 15)
        Me.Label53.TabIndex = 51
        Me.Label53.Text = "Height"
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericUpDown5.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown5.Location = New System.Drawing.Point(89, 46)
        Me.NumericUpDown5.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown5.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(66, 23)
        Me.NumericUpDown5.TabIndex = 50
        Me.NumericUpDown5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown5.ThousandsSeparator = True
        '
        'NumericUpDown6
        '
        Me.NumericUpDown6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericUpDown6.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown6.Location = New System.Drawing.Point(89, 19)
        Me.NumericUpDown6.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown6.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.NumericUpDown6.Name = "NumericUpDown6"
        Me.NumericUpDown6.Size = New System.Drawing.Size(66, 23)
        Me.NumericUpDown6.TabIndex = 49
        Me.NumericUpDown6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown6.ThousandsSeparator = True
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(158, 50)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(22, 15)
        Me.Label54.TabIndex = 48
        Me.Label54.Text = "cm"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(158, 22)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(22, 15)
        Me.Label55.TabIndex = 47
        Me.Label55.Text = "cm"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(7, 50)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(42, 15)
        Me.Label56.TabIndex = 46
        Me.Label56.Text = "Length"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(7, 23)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(40, 15)
        Me.Label57.TabIndex = 45
        Me.Label57.Text = "Width"
        '
        'gbInnerBag
        '
        Me.gbInnerBag.AutoSize = True
        Me.gbInnerBag.Controls.Add(Me.NumericUpDown3)
        Me.gbInnerBag.Controls.Add(Me.Label50)
        Me.gbInnerBag.Controls.Add(Me.Label51)
        Me.gbInnerBag.Controls.Add(Me.NumericUpDown1)
        Me.gbInnerBag.Controls.Add(Me.NumericUpDown2)
        Me.gbInnerBag.Controls.Add(Me.Label39)
        Me.gbInnerBag.Controls.Add(Me.Label47)
        Me.gbInnerBag.Controls.Add(Me.Label48)
        Me.gbInnerBag.Controls.Add(Me.Label49)
        Me.gbInnerBag.Enabled = False
        Me.gbInnerBag.Location = New System.Drawing.Point(930, 3)
        Me.gbInnerBag.Name = "gbInnerBag"
        Me.gbInnerBag.Size = New System.Drawing.Size(221, 117)
        Me.gbInnerBag.TabIndex = 55
        Me.gbInnerBag.TabStop = False
        Me.gbInnerBag.Text = "Plain / Inner Bag"
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericUpDown3.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown3.Location = New System.Drawing.Point(124, 75)
        Me.NumericUpDown3.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown3.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(66, 23)
        Me.NumericUpDown3.TabIndex = 53
        Me.NumericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown3.ThousandsSeparator = True
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(193, 78)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(22, 15)
        Me.Label50.TabIndex = 52
        Me.Label50.Text = "cm"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(7, 80)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(41, 15)
        Me.Label51.TabIndex = 51
        Me.Label51.Text = "Height"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericUpDown1.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown1.Location = New System.Drawing.Point(124, 48)
        Me.NumericUpDown1.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(66, 23)
        Me.NumericUpDown1.TabIndex = 50
        Me.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown1.ThousandsSeparator = True
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumericUpDown2.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown2.Location = New System.Drawing.Point(124, 21)
        Me.NumericUpDown2.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(66, 23)
        Me.NumericUpDown2.TabIndex = 49
        Me.NumericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumericUpDown2.ThousandsSeparator = True
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(193, 52)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(22, 15)
        Me.Label39.TabIndex = 48
        Me.Label39.Text = "cm"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(193, 25)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(22, 15)
        Me.Label47.TabIndex = 47
        Me.Label47.Text = "cm"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(7, 54)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(42, 15)
        Me.Label48.TabIndex = 46
        Me.Label48.Text = "Length"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(7, 25)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(40, 15)
        Me.Label49.TabIndex = 45
        Me.Label49.Text = "Width"
        '
        'gbWovenBag
        '
        Me.gbWovenBag.AutoSize = True
        Me.gbWovenBag.Controls.Add(Me.tbThick)
        Me.gbWovenBag.Controls.Add(Me.Label45)
        Me.gbWovenBag.Controls.Add(Me.Label46)
        Me.gbWovenBag.Controls.Add(Me.Label17)
        Me.gbWovenBag.Controls.Add(Me.tbLength)
        Me.gbWovenBag.Controls.Add(Me.tbWidth)
        Me.gbWovenBag.Controls.Add(Me.tbCapacity)
        Me.gbWovenBag.Controls.Add(Me.Label33)
        Me.gbWovenBag.Controls.Add(Me.Label42)
        Me.gbWovenBag.Controls.Add(Me.Label40)
        Me.gbWovenBag.Controls.Add(Me.chkHasHandle)
        Me.gbWovenBag.Controls.Add(Me.cbHandle)
        Me.gbWovenBag.Controls.Add(Me.Label16)
        Me.gbWovenBag.Controls.Add(Me.Label15)
        Me.gbWovenBag.Controls.Add(Me.Label14)
        Me.gbWovenBag.Controls.Add(Me.tbSackWeight)
        Me.gbWovenBag.Controls.Add(Me.cbSackType)
        Me.gbWovenBag.Controls.Add(Me.Label13)
        Me.gbWovenBag.Controls.Add(Me.cbDenier)
        Me.gbWovenBag.Controls.Add(Me.Label12)
        Me.gbWovenBag.Controls.Add(Me.cbWebbing)
        Me.gbWovenBag.Controls.Add(Me.Label11)
        Me.gbWovenBag.Controls.Add(Me.Label10)
        Me.gbWovenBag.Controls.Add(Me.tbColor)
        Me.gbWovenBag.Controls.Add(Me.Label9)
        Me.gbWovenBag.Enabled = False
        Me.gbWovenBag.Location = New System.Drawing.Point(3, 3)
        Me.gbWovenBag.Name = "gbWovenBag"
        Me.gbWovenBag.Size = New System.Drawing.Size(482, 184)
        Me.gbWovenBag.TabIndex = 7
        Me.gbWovenBag.TabStop = False
        Me.gbWovenBag.Text = "Woven Bag"
        '
        'tbThick
        '
        Me.tbThick.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbThick.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.tbThick.Location = New System.Drawing.Point(130, 128)
        Me.tbThick.Margin = New System.Windows.Forms.Padding(0)
        Me.tbThick.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.tbThick.Name = "tbThick"
        Me.tbThick.Size = New System.Drawing.Size(118, 23)
        Me.tbThick.TabIndex = 52
        Me.tbThick.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbThick.ThousandsSeparator = True
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(251, 130)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(44, 15)
        Me.Label45.TabIndex = 51
        Me.Label45.Text = "micron"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(6, 130)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(57, 15)
        Me.Label46.TabIndex = 50
        Me.Label46.Text = "Thickness"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(458, 77)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(18, 15)
        Me.Label17.TabIndex = 46
        Me.Label17.Text = "gr"
        '
        'tbLength
        '
        Me.tbLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbLength.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.tbLength.Location = New System.Drawing.Point(358, 155)
        Me.tbLength.Margin = New System.Windows.Forms.Padding(0)
        Me.tbLength.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.tbLength.Name = "tbLength"
        Me.tbLength.Size = New System.Drawing.Size(91, 23)
        Me.tbLength.TabIndex = 44
        Me.tbLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbLength.ThousandsSeparator = True
        '
        'tbWidth
        '
        Me.tbWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbWidth.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.tbWidth.Location = New System.Drawing.Point(130, 155)
        Me.tbWidth.Margin = New System.Windows.Forms.Padding(0)
        Me.tbWidth.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.tbWidth.Name = "tbWidth"
        Me.tbWidth.Size = New System.Drawing.Size(118, 23)
        Me.tbWidth.TabIndex = 42
        Me.tbWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbWidth.ThousandsSeparator = True
        '
        'tbCapacity
        '
        Me.tbCapacity.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.tbCapacity.Location = New System.Drawing.Point(130, 20)
        Me.tbCapacity.Margin = New System.Windows.Forms.Padding(0)
        Me.tbCapacity.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.tbCapacity.Name = "tbCapacity"
        Me.tbCapacity.Size = New System.Drawing.Size(64, 23)
        Me.tbCapacity.TabIndex = 41
        Me.tbCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbCapacity.ThousandsSeparator = True
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(200, 23)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(19, 15)
        Me.Label33.TabIndex = 35
        Me.Label33.Text = "kg"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(454, 158)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(22, 15)
        Me.Label42.TabIndex = 33
        Me.Label42.Text = "cm"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(251, 158)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(22, 15)
        Me.Label40.TabIndex = 31
        Me.Label40.Text = "cm"
        '
        'chkHasHandle
        '
        Me.chkHasHandle.AutoSize = True
        Me.chkHasHandle.Location = New System.Drawing.Point(239, 21)
        Me.chkHasHandle.Name = "chkHasHandle"
        Me.chkHasHandle.Size = New System.Drawing.Size(113, 19)
        Me.chkHasHandle.TabIndex = 30
        Me.chkHasHandle.Text = "Handle / Gagang"
        Me.chkHasHandle.UseVisualStyleBackColor = True
        '
        'cbHandle
        '
        Me.cbHandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbHandle.Enabled = False
        Me.cbHandle.FormattingEnabled = True
        Me.cbHandle.Location = New System.Drawing.Point(358, 20)
        Me.cbHandle.Name = "cbHandle"
        Me.cbHandle.Size = New System.Drawing.Size(118, 23)
        Me.cbHandle.TabIndex = 29
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(310, 157)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 15)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "Length"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 158)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(40, 15)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "Width"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(306, 77)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 15)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "Weight"
        '
        'tbSackWeight
        '
        Me.tbSackWeight.Location = New System.Drawing.Point(358, 74)
        Me.tbSackWeight.Name = "tbSackWeight"
        Me.tbSackWeight.ReadOnly = True
        Me.tbSackWeight.Size = New System.Drawing.Size(92, 23)
        Me.tbSackWeight.TabIndex = 17
        Me.tbSackWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cbSackType
        '
        Me.cbSackType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSackType.FormattingEnabled = True
        Me.cbSackType.Location = New System.Drawing.Point(130, 74)
        Me.cbSackType.Name = "cbSackType"
        Me.cbSackType.Size = New System.Drawing.Size(118, 23)
        Me.cbSackType.TabIndex = 16
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 77)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(31, 15)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "Type"
        '
        'cbDenier
        '
        Me.cbDenier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDenier.FormattingEnabled = True
        Me.cbDenier.Location = New System.Drawing.Point(358, 101)
        Me.cbDenier.Name = "cbDenier"
        Me.cbDenier.Size = New System.Drawing.Size(92, 23)
        Me.cbDenier.TabIndex = 14
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(309, 104)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(42, 15)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Denier"
        '
        'cbWebbing
        '
        Me.cbWebbing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWebbing.FormattingEnabled = True
        Me.cbWebbing.Location = New System.Drawing.Point(130, 101)
        Me.cbWebbing.Name = "cbWebbing"
        Me.cbWebbing.Size = New System.Drawing.Size(118, 23)
        Me.cbWebbing.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 104)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(116, 15)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Webbing / Anyaman"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 15)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Color"
        '
        'tbColor
        '
        Me.tbColor.Location = New System.Drawing.Point(130, 47)
        Me.tbColor.Name = "tbColor"
        Me.tbColor.Size = New System.Drawing.Size(346, 23)
        Me.tbColor.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 15)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Capacity"
        '
        'cbBagType
        '
        Me.cbBagType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBagType.FormattingEnabled = True
        Me.cbBagType.Items.AddRange(New Object() {"Woven Bag", "Jumbo Bag", "Plain / Inner Bag"})
        Me.cbBagType.Location = New System.Drawing.Point(133, 3)
        Me.cbBagType.Name = "cbBagType"
        Me.cbBagType.Size = New System.Drawing.Size(231, 23)
        Me.cbBagType.TabIndex = 54
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(9, 6)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(67, 15)
        Me.Label18.TabIndex = 53
        Me.Label18.Text = "Type of Bag"
        '
        'tabPict
        '
        Me.tabPict.Controls.Add(Me.pict)
        Me.tabPict.Location = New System.Drawing.Point(4, 22)
        Me.tabPict.Name = "tabPict"
        Me.tabPict.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPict.Size = New System.Drawing.Size(1161, 224)
        Me.tabPict.TabIndex = 2
        Me.tabPict.Text = "Picture"
        Me.tabPict.UseVisualStyleBackColor = True
        '
        'pict
        '
        Me.pict.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pict.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pict.ErrorImage = Nothing
        Me.pict.InitialImage = Nothing
        Me.pict.Location = New System.Drawing.Point(6, 6)
        Me.pict.Name = "pict"
        Me.pict.Size = New System.Drawing.Size(1149, 207)
        Me.pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pict.TabIndex = 0
        Me.pict.TabStop = False
        '
        'tabUOM
        '
        Me.tabUOM.Location = New System.Drawing.Point(4, 22)
        Me.tabUOM.Name = "tabUOM"
        Me.tabUOM.Size = New System.Drawing.Size(1161, 224)
        Me.tabUOM.TabIndex = 6
        Me.tabUOM.Text = "Alternate UOM"
        Me.tabUOM.UseVisualStyleBackColor = True
        '
        'tabLocation
        '
        Me.tabLocation.Controls.Add(Me.locList)
        Me.tabLocation.Location = New System.Drawing.Point(4, 22)
        Me.tabLocation.Name = "tabLocation"
        Me.tabLocation.Padding = New System.Windows.Forms.Padding(3)
        Me.tabLocation.Size = New System.Drawing.Size(1161, 224)
        Me.tabLocation.TabIndex = 4
        Me.tabLocation.Text = "Location"
        Me.tabLocation.UseVisualStyleBackColor = True
        '
        'locList
        '
        Me.locList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.locList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.locName, Me.locQty})
        Me.locList.FullRowSelect = True
        Me.locList.HideSelection = False
        Me.locList.Location = New System.Drawing.Point(6, 6)
        Me.locList.MultiSelect = False
        Me.locList.Name = "locList"
        Me.locList.Size = New System.Drawing.Size(1149, 204)
        Me.locList.TabIndex = 1
        Me.locList.UseCompatibleStateImageBehavior = False
        Me.locList.View = System.Windows.Forms.View.Details
        '
        'locName
        '
        Me.locName.Text = "Location"
        Me.locName.Width = 75
        '
        'locQty
        '
        Me.locQty.Text = "Quantity"
        Me.locQty.Width = 75
        '
        'tabHistory
        '
        Me.tabHistory.Controls.Add(Me.dpHistTo)
        Me.tabHistory.Controls.Add(Me.dpHistFrom)
        Me.tabHistory.Controls.Add(Me.Label29)
        Me.tabHistory.Controls.Add(Me.Label30)
        Me.tabHistory.Controls.Add(Me.histList)
        Me.tabHistory.Location = New System.Drawing.Point(4, 22)
        Me.tabHistory.Name = "tabHistory"
        Me.tabHistory.Size = New System.Drawing.Size(1161, 224)
        Me.tabHistory.TabIndex = 3
        Me.tabHistory.Text = "History"
        Me.tabHistory.UseVisualStyleBackColor = True
        '
        'dpHistTo
        '
        Me.dpHistTo.Location = New System.Drawing.Point(274, 6)
        Me.dpHistTo.Name = "dpHistTo"
        Me.dpHistTo.Size = New System.Drawing.Size(173, 23)
        Me.dpHistTo.TabIndex = 16
        '
        'dpHistFrom
        '
        Me.dpHistFrom.Location = New System.Drawing.Point(45, 6)
        Me.dpHistFrom.Name = "dpHistFrom"
        Me.dpHistFrom.Size = New System.Drawing.Size(173, 23)
        Me.dpHistFrom.TabIndex = 15
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(247, 9)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(19, 15)
        Me.Label29.TabIndex = 14
        Me.Label29.Text = "To"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(3, 9)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(35, 15)
        Me.Label30.TabIndex = 13
        Me.Label30.Text = "From"
        '
        'histList
        '
        Me.histList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.histList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.histDate, Me.histBegin, Me.histMake, Me.histBuy, Me.histUse, Me.histSell, Me.histEnd})
        Me.histList.FullRowSelect = True
        Me.histList.HideSelection = False
        Me.histList.Location = New System.Drawing.Point(6, 33)
        Me.histList.MultiSelect = False
        Me.histList.Name = "histList"
        Me.histList.Size = New System.Drawing.Size(1149, 176)
        Me.histList.TabIndex = 0
        Me.histList.UseCompatibleStateImageBehavior = False
        Me.histList.View = System.Windows.Forms.View.Details
        '
        'histDate
        '
        Me.histDate.Text = "Date"
        Me.histDate.Width = 75
        '
        'histBegin
        '
        Me.histBegin.Text = "Begin"
        Me.histBegin.Width = 75
        '
        'histMake
        '
        Me.histMake.Text = "Produced"
        Me.histMake.Width = 75
        '
        'histBuy
        '
        Me.histBuy.Text = "Bought"
        Me.histBuy.Width = 75
        '
        'histUse
        '
        Me.histUse.Text = "Used"
        Me.histUse.Width = 75
        '
        'histSell
        '
        Me.histSell.Text = "Sold"
        Me.histSell.Width = 75
        '
        'histEnd
        '
        Me.histEnd.Text = "End"
        Me.histEnd.Width = 75
        '
        'tabVendor
        '
        Me.tabVendor.Controls.Add(Me.tbVendorPrice)
        Me.tabVendor.Controls.Add(Me.cbVendorCurrency)
        Me.tabVendor.Controls.Add(Me.btnDelVendor)
        Me.tabVendor.Controls.Add(Me.btnEditVendor)
        Me.tabVendor.Controls.Add(Me.btnCreateVendor)
        Me.tabVendor.Controls.Add(Me.Label24)
        Me.tabVendor.Controls.Add(Me.tbVendorBrand)
        Me.tabVendor.Controls.Add(Me.vndList)
        Me.tabVendor.Controls.Add(Me.Label23)
        Me.tabVendor.Controls.Add(Me.Label21)
        Me.tabVendor.Controls.Add(Me.Label22)
        Me.tabVendor.Controls.Add(Me.tbVendorCode)
        Me.tabVendor.Controls.Add(Me.tbVendorName)
        Me.tabVendor.Location = New System.Drawing.Point(4, 22)
        Me.tabVendor.Name = "tabVendor"
        Me.tabVendor.Padding = New System.Windows.Forms.Padding(3)
        Me.tabVendor.Size = New System.Drawing.Size(1161, 224)
        Me.tabVendor.TabIndex = 0
        Me.tabVendor.Text = "Vendor"
        Me.tabVendor.UseVisualStyleBackColor = True
        '
        'tbVendorPrice
        '
        Me.tbVendorPrice.DecimalPlaces = 2
        Me.tbVendorPrice.Increment = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.tbVendorPrice.Location = New System.Drawing.Point(244, 33)
        Me.tbVendorPrice.Margin = New System.Windows.Forms.Padding(0)
        Me.tbVendorPrice.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.tbVendorPrice.Name = "tbVendorPrice"
        Me.tbVendorPrice.Size = New System.Drawing.Size(118, 23)
        Me.tbVendorPrice.TabIndex = 41
        Me.tbVendorPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbVendorPrice.ThousandsSeparator = True
        '
        'cbVendorCurrency
        '
        Me.cbVendorCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVendorCurrency.FormattingEnabled = True
        Me.cbVendorCurrency.Location = New System.Drawing.Point(372, 33)
        Me.cbVendorCurrency.Name = "cbVendorCurrency"
        Me.cbVendorCurrency.Size = New System.Drawing.Size(75, 23)
        Me.cbVendorCurrency.TabIndex = 23
        '
        'btnDelVendor
        '
        Me.btnDelVendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelVendor.Location = New System.Drawing.Point(918, 89)
        Me.btnDelVendor.Name = "btnDelVendor"
        Me.btnDelVendor.Size = New System.Drawing.Size(75, 23)
        Me.btnDelVendor.TabIndex = 22
        Me.btnDelVendor.Text = "Delete"
        Me.btnDelVendor.UseVisualStyleBackColor = True
        '
        'btnEditVendor
        '
        Me.btnEditVendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditVendor.Location = New System.Drawing.Point(999, 89)
        Me.btnEditVendor.Name = "btnEditVendor"
        Me.btnEditVendor.Size = New System.Drawing.Size(75, 23)
        Me.btnEditVendor.TabIndex = 21
        Me.btnEditVendor.Text = "Modify"
        Me.btnEditVendor.UseVisualStyleBackColor = True
        '
        'btnCreateVendor
        '
        Me.btnCreateVendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateVendor.Location = New System.Drawing.Point(1080, 89)
        Me.btnCreateVendor.Name = "btnCreateVendor"
        Me.btnCreateVendor.Size = New System.Drawing.Size(75, 23)
        Me.btnCreateVendor.TabIndex = 20
        Me.btnCreateVendor.Text = "Create"
        Me.btnCreateVendor.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(3, 63)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(39, 15)
        Me.Label24.TabIndex = 11
        Me.Label24.Text = "Brand"
        '
        'tbVendorBrand
        '
        Me.tbVendorBrand.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbVendorBrand.Location = New System.Drawing.Point(60, 60)
        Me.tbVendorBrand.Name = "tbVendorBrand"
        Me.tbVendorBrand.Size = New System.Drawing.Size(1095, 23)
        Me.tbVendorBrand.TabIndex = 10
        '
        'vndList
        '
        Me.vndList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vndList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.vndName, Me.vndCode, Me.vndBrand, Me.vndPrice, Me.vndCurrency})
        Me.vndList.FullRowSelect = True
        Me.vndList.HideSelection = False
        Me.vndList.Location = New System.Drawing.Point(6, 116)
        Me.vndList.MultiSelect = False
        Me.vndList.Name = "vndList"
        Me.vndList.Size = New System.Drawing.Size(1149, 93)
        Me.vndList.TabIndex = 9
        Me.vndList.UseCompatibleStateImageBehavior = False
        Me.vndList.View = System.Windows.Forms.View.Details
        '
        'vndName
        '
        Me.vndName.Text = "Vendor"
        Me.vndName.Width = 120
        '
        'vndCode
        '
        Me.vndCode.Text = "Code"
        Me.vndCode.Width = 80
        '
        'vndBrand
        '
        Me.vndBrand.Text = "Brand"
        Me.vndBrand.Width = 120
        '
        'vndPrice
        '
        Me.vndPrice.Text = "Price"
        Me.vndPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.vndPrice.Width = 100
        '
        'vndCurrency
        '
        Me.vndCurrency.Text = "Currency"
        Me.vndCurrency.Width = 80
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(206, 36)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(33, 15)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "Price"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(3, 36)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(34, 15)
        Me.Label21.TabIndex = 6
        Me.Label21.Text = "Code"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(3, 9)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(37, 15)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "Name"
        '
        'tbVendorCode
        '
        Me.tbVendorCode.Location = New System.Drawing.Point(60, 33)
        Me.tbVendorCode.Name = "tbVendorCode"
        Me.tbVendorCode.Size = New System.Drawing.Size(116, 23)
        Me.tbVendorCode.TabIndex = 5
        '
        'tbVendorName
        '
        Me.tbVendorName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbVendorName.Location = New System.Drawing.Point(60, 6)
        Me.tbVendorName.Name = "tbVendorName"
        Me.tbVendorName.Size = New System.Drawing.Size(1095, 23)
        Me.tbVendorName.TabIndex = 3
        '
        'tabCustomer
        '
        Me.tabCustomer.Controls.Add(Me.tbCustPrice)
        Me.tabCustomer.Controls.Add(Me.Label31)
        Me.tabCustomer.Controls.Add(Me.cbCustCurrency)
        Me.tabCustomer.Controls.Add(Me.btnDelCust)
        Me.tabCustomer.Controls.Add(Me.btnEditCust)
        Me.tabCustomer.Controls.Add(Me.btnCreateCust)
        Me.tabCustomer.Controls.Add(Me.Label27)
        Me.tabCustomer.Controls.Add(Me.custList)
        Me.tabCustomer.Controls.Add(Me.Label25)
        Me.tabCustomer.Controls.Add(Me.Label26)
        Me.tabCustomer.Controls.Add(Me.tbCustCode)
        Me.tabCustomer.Controls.Add(Me.tbCustName)
        Me.tabCustomer.Controls.Add(Me.tbCustMinOrder)
        Me.tabCustomer.Location = New System.Drawing.Point(4, 22)
        Me.tabCustomer.Name = "tabCustomer"
        Me.tabCustomer.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCustomer.Size = New System.Drawing.Size(1161, 224)
        Me.tabCustomer.TabIndex = 1
        Me.tabCustomer.Text = "Customer"
        Me.tabCustomer.UseVisualStyleBackColor = True
        '
        'tbCustPrice
        '
        Me.tbCustPrice.DecimalPlaces = 2
        Me.tbCustPrice.Increment = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.tbCustPrice.Location = New System.Drawing.Point(265, 33)
        Me.tbCustPrice.Margin = New System.Windows.Forms.Padding(0)
        Me.tbCustPrice.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.tbCustPrice.Name = "tbCustPrice"
        Me.tbCustPrice.Size = New System.Drawing.Size(101, 23)
        Me.tbCustPrice.TabIndex = 42
        Me.tbCustPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbCustPrice.ThousandsSeparator = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(3, 63)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(93, 15)
        Me.Label31.TabIndex = 26
        Me.Label31.Text = "Minimum Order"
        '
        'cbCustCurrency
        '
        Me.cbCustCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCustCurrency.FormattingEnabled = True
        Me.cbCustCurrency.Location = New System.Drawing.Point(372, 33)
        Me.cbCustCurrency.Name = "cbCustCurrency"
        Me.cbCustCurrency.Size = New System.Drawing.Size(75, 23)
        Me.cbCustCurrency.TabIndex = 24
        '
        'btnDelCust
        '
        Me.btnDelCust.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelCust.Location = New System.Drawing.Point(918, 89)
        Me.btnDelCust.Name = "btnDelCust"
        Me.btnDelCust.Size = New System.Drawing.Size(75, 23)
        Me.btnDelCust.TabIndex = 19
        Me.btnDelCust.Text = "Delete"
        Me.btnDelCust.UseVisualStyleBackColor = True
        '
        'btnEditCust
        '
        Me.btnEditCust.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditCust.Location = New System.Drawing.Point(999, 89)
        Me.btnEditCust.Name = "btnEditCust"
        Me.btnEditCust.Size = New System.Drawing.Size(75, 23)
        Me.btnEditCust.TabIndex = 18
        Me.btnEditCust.Text = "Modify"
        Me.btnEditCust.UseVisualStyleBackColor = True
        '
        'btnCreateCust
        '
        Me.btnCreateCust.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateCust.Location = New System.Drawing.Point(1080, 89)
        Me.btnCreateCust.Name = "btnCreateCust"
        Me.btnCreateCust.Size = New System.Drawing.Size(75, 23)
        Me.btnCreateCust.TabIndex = 17
        Me.btnCreateCust.Text = "Create"
        Me.btnCreateCust.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(227, 36)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(33, 15)
        Me.Label27.TabIndex = 16
        Me.Label27.Text = "Price"
        '
        'custList
        '
        Me.custList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.custList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.custName, Me.custCode, Me.custPrice, Me.custCurrency, Me.minOrder})
        Me.custList.FullRowSelect = True
        Me.custList.HideSelection = False
        Me.custList.Location = New System.Drawing.Point(6, 116)
        Me.custList.MultiSelect = False
        Me.custList.Name = "custList"
        Me.custList.Size = New System.Drawing.Size(1149, 93)
        Me.custList.TabIndex = 15
        Me.custList.UseCompatibleStateImageBehavior = False
        Me.custList.View = System.Windows.Forms.View.Details
        '
        'custName
        '
        Me.custName.Text = "Customer"
        Me.custName.Width = 120
        '
        'custCode
        '
        Me.custCode.Text = "Code"
        '
        'custPrice
        '
        Me.custPrice.Text = "Price"
        Me.custPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.custPrice.Width = 100
        '
        'custCurrency
        '
        Me.custCurrency.Text = "Currency"
        Me.custCurrency.Width = 80
        '
        'minOrder
        '
        Me.minOrder.Text = "Minimum Order"
        Me.minOrder.Width = 100
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(3, 36)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(34, 15)
        Me.Label25.TabIndex = 13
        Me.Label25.Text = "Code"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(3, 9)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(37, 15)
        Me.Label26.TabIndex = 11
        Me.Label26.Text = "Name"
        '
        'tbCustCode
        '
        Me.tbCustCode.Location = New System.Drawing.Point(108, 33)
        Me.tbCustCode.Name = "tbCustCode"
        Me.tbCustCode.Size = New System.Drawing.Size(103, 23)
        Me.tbCustCode.TabIndex = 12
        '
        'tbCustName
        '
        Me.tbCustName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCustName.Location = New System.Drawing.Point(108, 6)
        Me.tbCustName.Name = "tbCustName"
        Me.tbCustName.Size = New System.Drawing.Size(1047, 23)
        Me.tbCustName.TabIndex = 10
        '
        'tbCustMinOrder
        '
        Me.tbCustMinOrder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCustMinOrder.Location = New System.Drawing.Point(108, 60)
        Me.tbCustMinOrder.Name = "tbCustMinOrder"
        Me.tbCustMinOrder.Size = New System.Drawing.Size(1047, 23)
        Me.tbCustMinOrder.TabIndex = 25
        '
        'tabFormula
        '
        Me.tabFormula.Controls.Add(Me.Label38)
        Me.tabFormula.Controls.Add(Me.TextBox5)
        Me.tabFormula.Controls.Add(Me.Label36)
        Me.tabFormula.Controls.Add(Me.TextBox4)
        Me.tabFormula.Controls.Add(Me.Label35)
        Me.tabFormula.Controls.Add(Me.TabControl3)
        Me.tabFormula.Controls.Add(Me.TextBox3)
        Me.tabFormula.Controls.Add(Me.Label34)
        Me.tabFormula.Location = New System.Drawing.Point(4, 22)
        Me.tabFormula.Name = "tabFormula"
        Me.tabFormula.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFormula.Size = New System.Drawing.Size(1161, 224)
        Me.tabFormula.TabIndex = 5
        Me.tabFormula.Text = "Formula"
        Me.tabFormula.UseVisualStyleBackColor = True
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(392, 36)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(49, 15)
        Me.Label38.TabIndex = 7
        Me.Label38.Text = "minutes"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(309, 33)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(77, 23)
        Me.TextBox5.TabIndex = 6
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(237, 36)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(60, 15)
        Me.Label36.TabIndex = 5
        Me.Label36.Text = "Time used"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(126, 33)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(65, 23)
        Me.TextBox4.TabIndex = 4
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(7, 36)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(105, 15)
        Me.Label35.TabIndex = 3
        Me.Label35.Text = "Number of worker"
        '
        'TabControl3
        '
        Me.TabControl3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl3.Controls.Add(Me.tabMaterial)
        Me.TabControl3.Controls.Add(Me.tabByProduct)
        Me.TabControl3.Location = New System.Drawing.Point(6, 67)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(1149, 149)
        Me.TabControl3.TabIndex = 2
        '
        'tabMaterial
        '
        Me.tabMaterial.Controls.Add(Me.Button2)
        Me.tabMaterial.Controls.Add(Me.Button1)
        Me.tabMaterial.Controls.Add(Me.TextBox2)
        Me.tabMaterial.Controls.Add(Me.Label63)
        Me.tabMaterial.Controls.Add(Me.TextBox1)
        Me.tabMaterial.Controls.Add(Me.Label62)
        Me.tabMaterial.Controls.Add(Me.formulaList)
        Me.tabMaterial.Location = New System.Drawing.Point(4, 24)
        Me.tabMaterial.Name = "tabMaterial"
        Me.tabMaterial.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMaterial.Size = New System.Drawing.Size(1141, 121)
        Me.tabMaterial.TabIndex = 0
        Me.tabMaterial.Text = "Material Used"
        Me.tabMaterial.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(197, 64)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(116, 64)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(116, 35)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(231, 23)
        Me.TextBox2.TabIndex = 5
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(6, 38)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(53, 15)
        Me.Label63.TabIndex = 4
        Me.Label63.Text = "Quantity"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(116, 6)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(231, 23)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(6, 9)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(30, 15)
        Me.Label62.TabIndex = 2
        Me.Label62.Text = "Item"
        '
        'formulaList
        '
        Me.formulaList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formulaList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.formulaList.FullRowSelect = True
        Me.formulaList.HideSelection = False
        Me.formulaList.Location = New System.Drawing.Point(353, 6)
        Me.formulaList.MultiSelect = False
        Me.formulaList.Name = "formulaList"
        Me.formulaList.Size = New System.Drawing.Size(782, 113)
        Me.formulaList.TabIndex = 1
        Me.formulaList.UseCompatibleStateImageBehavior = False
        Me.formulaList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Item Code"
        Me.ColumnHeader3.Width = 150
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Item Name"
        Me.ColumnHeader4.Width = 150
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Quantity"
        '
        'tabByProduct
        '
        Me.tabByProduct.Controls.Add(Me.Button3)
        Me.tabByProduct.Controls.Add(Me.Button4)
        Me.tabByProduct.Controls.Add(Me.TextBox6)
        Me.tabByProduct.Controls.Add(Me.Label64)
        Me.tabByProduct.Controls.Add(Me.TextBox7)
        Me.tabByProduct.Controls.Add(Me.Label65)
        Me.tabByProduct.Controls.Add(Me.ListView2)
        Me.tabByProduct.Controls.Add(Me.ListView1)
        Me.tabByProduct.Location = New System.Drawing.Point(4, 22)
        Me.tabByProduct.Name = "tabByProduct"
        Me.tabByProduct.Padding = New System.Windows.Forms.Padding(3)
        Me.tabByProduct.Size = New System.Drawing.Size(1141, 123)
        Me.tabByProduct.TabIndex = 1
        Me.tabByProduct.Text = "By Products"
        Me.tabByProduct.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(197, 64)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 14
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(116, 64)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 13
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(116, 35)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(231, 23)
        Me.TextBox6.TabIndex = 12
        Me.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(6, 38)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(53, 15)
        Me.Label64.TabIndex = 11
        Me.Label64.Text = "Quantity"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(116, 6)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(231, 23)
        Me.TextBox7.TabIndex = 10
        Me.TextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(6, 9)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(30, 15)
        Me.Label65.TabIndex = 9
        Me.Label65.Text = "Item"
        '
        'ListView2
        '
        Me.ListView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8})
        Me.ListView2.FullRowSelect = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(353, 6)
        Me.ListView2.MultiSelect = False
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(782, 111)
        Me.ListView2.TabIndex = 8
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Item Code"
        Me.ColumnHeader6.Width = 150
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Item Name"
        Me.ColumnHeader7.Width = 150
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Quantity"
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(6, 6)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(1129, 2)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Item Code"
        Me.ColumnHeader1.Width = 150
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Item Name"
        Me.ColumnHeader2.Width = 150
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(126, 6)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(320, 23)
        Me.TextBox3.TabIndex = 1
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(6, 9)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(107, 15)
        Me.Label34.TabIndex = 0
        Me.Label34.Text = "Quantity Produced"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkConsume)
        Me.GroupBox1.Controls.Add(Me.chkIsSack)
        Me.GroupBox1.Controls.Add(Me.tbUnitWeight)
        Me.GroupBox1.Controls.Add(Me.Label43)
        Me.GroupBox1.Controls.Add(Me.Label44)
        Me.GroupBox1.Controls.Add(Me.tbMaxStock)
        Me.GroupBox1.Controls.Add(Me.tbMinStock)
        Me.GroupBox1.Controls.Add(Me.lbMaxUom)
        Me.GroupBox1.Controls.Add(Me.lbMinUom)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.cbSubCategory)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cbType)
        Me.GroupBox1.Controls.Add(Me.chkSell)
        Me.GroupBox1.Controls.Add(Me.chkBuy)
        Me.GroupBox1.Controls.Add(Me.chkMake)
        Me.GroupBox1.Controls.Add(Me.chkUse)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cbUOM)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cbCategory)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.tbCode)
        Me.GroupBox1.Controls.Add(Me.tbName)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(489, 258)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item"
        '
        'chkConsume
        '
        Me.chkConsume.AutoSize = True
        Me.chkConsume.Location = New System.Drawing.Point(234, 155)
        Me.chkConsume.Name = "chkConsume"
        Me.chkConsume.Size = New System.Drawing.Size(91, 19)
        Me.chkConsume.TabIndex = 45
        Me.chkConsume.Text = "Consumable"
        Me.chkConsume.UseVisualStyleBackColor = True
        '
        'chkIsSack
        '
        Me.chkIsSack.AutoSize = True
        Me.chkIsSack.Location = New System.Drawing.Point(340, 233)
        Me.chkIsSack.Name = "chkIsSack"
        Me.chkIsSack.Size = New System.Drawing.Size(56, 19)
        Me.chkIsSack.TabIndex = 44
        Me.chkIsSack.Text = "Is Bag"
        Me.chkIsSack.UseVisualStyleBackColor = True
        '
        'tbUnitWeight
        '
        Me.tbUnitWeight.DecimalPlaces = 2
        Me.tbUnitWeight.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.tbUnitWeight.Location = New System.Drawing.Point(130, 229)
        Me.tbUnitWeight.Margin = New System.Windows.Forms.Padding(0)
        Me.tbUnitWeight.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.tbUnitWeight.Name = "tbUnitWeight"
        Me.tbUnitWeight.Size = New System.Drawing.Size(83, 23)
        Me.tbUnitWeight.TabIndex = 43
        Me.tbUnitWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbUnitWeight.ThousandsSeparator = True
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(216, 231)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(20, 15)
        Me.Label43.TabIndex = 42
        Me.Label43.Text = "Kg"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(6, 231)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(70, 15)
        Me.Label44.TabIndex = 41
        Me.Label44.Text = "Unit Weight"
        '
        'tbMaxStock
        '
        Me.tbMaxStock.DecimalPlaces = 2
        Me.tbMaxStock.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.tbMaxStock.Location = New System.Drawing.Point(340, 202)
        Me.tbMaxStock.Margin = New System.Windows.Forms.Padding(0)
        Me.tbMaxStock.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.tbMaxStock.Name = "tbMaxStock"
        Me.tbMaxStock.Size = New System.Drawing.Size(91, 23)
        Me.tbMaxStock.TabIndex = 40
        Me.tbMaxStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbMaxStock.ThousandsSeparator = True
        '
        'tbMinStock
        '
        Me.tbMinStock.DecimalPlaces = 2
        Me.tbMinStock.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.tbMinStock.Location = New System.Drawing.Point(130, 202)
        Me.tbMinStock.Margin = New System.Windows.Forms.Padding(0)
        Me.tbMinStock.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.tbMinStock.Name = "tbMinStock"
        Me.tbMinStock.Size = New System.Drawing.Size(83, 23)
        Me.tbMinStock.TabIndex = 39
        Me.tbMinStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbMinStock.ThousandsSeparator = True
        '
        'lbMaxUom
        '
        Me.lbMaxUom.AutoSize = True
        Me.lbMaxUom.Location = New System.Drawing.Point(436, 204)
        Me.lbMaxUom.Name = "lbMaxUom"
        Me.lbMaxUom.Size = New System.Drawing.Size(35, 15)
        Me.lbMaxUom.TabIndex = 37
        Me.lbMaxUom.Text = "UOM"
        '
        'lbMinUom
        '
        Me.lbMinUom.AutoSize = True
        Me.lbMinUom.Location = New System.Drawing.Point(216, 204)
        Me.lbMinUom.Name = "lbMinUom"
        Me.lbMinUom.Size = New System.Drawing.Size(35, 15)
        Me.lbMinUom.TabIndex = 36
        Me.lbMinUom.Text = "UOM"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 104)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(77, 15)
        Me.Label28.TabIndex = 18
        Me.Label28.Text = "Sub Category"
        '
        'cbSubCategory
        '
        Me.cbSubCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSubCategory.FormattingEnabled = True
        Me.cbSubCategory.Location = New System.Drawing.Point(130, 101)
        Me.cbSubCategory.Name = "cbSubCategory"
        Me.cbSubCategory.Size = New System.Drawing.Size(353, 23)
        Me.cbSubCategory.TabIndex = 17
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(269, 204)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(61, 15)
        Me.Label20.TabIndex = 16
        Me.Label20.Text = "Max Stock"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 204)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(59, 15)
        Me.Label19.TabIndex = 14
        Me.Label19.Text = "Min Stock"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(269, 131)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 15)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Type"
        Me.Label8.Visible = False
        '
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(313, 128)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(170, 23)
        Me.cbType.TabIndex = 11
        Me.cbType.Visible = False
        '
        'chkSell
        '
        Me.chkSell.AutoSize = True
        Me.chkSell.Location = New System.Drawing.Point(367, 178)
        Me.chkSell.Name = "chkSell"
        Me.chkSell.Size = New System.Drawing.Size(66, 19)
        Me.chkSell.TabIndex = 10
        Me.chkSell.Text = "Sellable"
        Me.chkSell.UseVisualStyleBackColor = True
        '
        'chkBuy
        '
        Me.chkBuy.AutoSize = True
        Me.chkBuy.Location = New System.Drawing.Point(130, 178)
        Me.chkBuy.Name = "chkBuy"
        Me.chkBuy.Size = New System.Drawing.Size(68, 19)
        Me.chkBuy.TabIndex = 9
        Me.chkBuy.Text = "Buyable"
        Me.chkBuy.UseVisualStyleBackColor = True
        '
        'chkMake
        '
        Me.chkMake.AutoSize = True
        Me.chkMake.Location = New System.Drawing.Point(367, 155)
        Me.chkMake.Name = "chkMake"
        Me.chkMake.Size = New System.Drawing.Size(83, 19)
        Me.chkMake.TabIndex = 8
        Me.chkMake.Text = "Producible"
        Me.chkMake.UseVisualStyleBackColor = True
        '
        'chkUse
        '
        Me.chkUse.AutoSize = True
        Me.chkUse.Location = New System.Drawing.Point(130, 155)
        Me.chkUse.Name = "chkUse"
        Me.chkUse.Size = New System.Drawing.Size(61, 19)
        Me.chkUse.TabIndex = 7
        Me.chkUse.Text = "Usable"
        Me.chkUse.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 131)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "UOM"
        '
        'cbUOM
        '
        Me.cbUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUOM.FormattingEnabled = True
        Me.cbUOM.Location = New System.Drawing.Point(130, 128)
        Me.cbUOM.Name = "cbUOM"
        Me.cbUOM.Size = New System.Drawing.Size(117, 23)
        Me.cbUOM.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Category"
        '
        'cbCategory
        '
        Me.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCategory.FormattingEnabled = True
        Me.cbCategory.Location = New System.Drawing.Point(130, 74)
        Me.cbCategory.Name = "cbCategory"
        Me.cbCategory.Size = New System.Drawing.Size(353, 23)
        Me.cbCategory.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Code"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Name"
        '
        'tbCode
        '
        Me.tbCode.Location = New System.Drawing.Point(130, 47)
        Me.tbCode.Name = "tbCode"
        Me.tbCode.Size = New System.Drawing.Size(353, 23)
        Me.tbCode.TabIndex = 1
        '
        'tbName
        '
        Me.tbName.Location = New System.Drawing.Point(130, 20)
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(353, 23)
        Me.tbName.TabIndex = 0
        '
        'OpenDlg
        '
        Me.OpenDlg.Filter = "Image files|*.jpg; *.png; *.bmp"
        '
        'searchTimer
        '
        Me.searchTimer.Interval = 500
        '
        'ItemControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "ItemControl"
        Me.Size = New System.Drawing.Size(1175, 669)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.tcDetail.ResumeLayout(False)
        Me.tabSack.ResumeLayout(False)
        Me.tabSack.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.gbJumboBag.ResumeLayout(False)
        Me.gbJumboBag.PerformLayout()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbInnerBag.ResumeLayout(False)
        Me.gbInnerBag.PerformLayout()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbWovenBag.ResumeLayout(False)
        Me.gbWovenBag.PerformLayout()
        CType(Me.tbThick, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPict.ResumeLayout(False)
        CType(Me.pict, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLocation.ResumeLayout(False)
        Me.tabHistory.ResumeLayout(False)
        Me.tabHistory.PerformLayout()
        Me.tabVendor.ResumeLayout(False)
        Me.tabVendor.PerformLayout()
        CType(Me.tbVendorPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCustomer.ResumeLayout(False)
        Me.tabCustomer.PerformLayout()
        CType(Me.tbCustPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabFormula.ResumeLayout(False)
        Me.tabFormula.PerformLayout()
        Me.TabControl3.ResumeLayout(False)
        Me.tabMaterial.ResumeLayout(False)
        Me.tabMaterial.PerformLayout()
        Me.tabByProduct.ResumeLayout(False)
        Me.tabByProduct.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.tbUnitWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMaxStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMinStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents tbSearchItem As System.Windows.Forms.TextBox
    Friend WithEvents itemName As System.Windows.Forms.ColumnHeader
    Friend WithEvents itemCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents itemCategory As System.Windows.Forms.ColumnHeader
    Friend WithEvents itemSubCat As System.Windows.Forms.ColumnHeader
    Friend WithEvents itemQty As System.Windows.Forms.ColumnHeader
    Friend WithEvents itemUom As System.Windows.Forms.ColumnHeader
    Friend WithEvents itemWeight As System.Windows.Forms.ColumnHeader
    Friend WithEvents itemCOG As System.Windows.Forms.ColumnHeader
    Friend WithEvents usable As System.Windows.Forms.ColumnHeader
    Friend WithEvents produceable As System.Windows.Forms.ColumnHeader
    Friend WithEvents buyable As System.Windows.Forms.ColumnHeader
    Friend WithEvents sellable As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstSuggest As System.Windows.Forms.ListView
    Friend WithEvents gbWovenBag As System.Windows.Forms.GroupBox
    Friend WithEvents tbLength As System.Windows.Forms.NumericUpDown
    Friend WithEvents tbWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents tbCapacity As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents chkHasHandle As System.Windows.Forms.CheckBox
    Friend WithEvents cbHandle As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tbSackWeight As System.Windows.Forms.TextBox
    Friend WithEvents cbSackType As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbDenier As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cbWebbing As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tbColor As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tcDetail As System.Windows.Forms.TabControl
    Friend WithEvents tabPict As System.Windows.Forms.TabPage
    Friend WithEvents pict As System.Windows.Forms.PictureBox
    Friend WithEvents tabLocation As System.Windows.Forms.TabPage
    Friend WithEvents locList As System.Windows.Forms.ListView
    Friend WithEvents locName As System.Windows.Forms.ColumnHeader
    Friend WithEvents locQty As System.Windows.Forms.ColumnHeader
    Friend WithEvents tabHistory As System.Windows.Forms.TabPage
    Friend WithEvents dpHistTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpHistFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents histList As System.Windows.Forms.ListView
    Friend WithEvents histDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents histBegin As System.Windows.Forms.ColumnHeader
    Friend WithEvents histMake As System.Windows.Forms.ColumnHeader
    Friend WithEvents histBuy As System.Windows.Forms.ColumnHeader
    Friend WithEvents histUse As System.Windows.Forms.ColumnHeader
    Friend WithEvents histSell As System.Windows.Forms.ColumnHeader
    Friend WithEvents histEnd As System.Windows.Forms.ColumnHeader
    Friend WithEvents tabVendor As System.Windows.Forms.TabPage
    Friend WithEvents tbVendorPrice As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbVendorCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents btnDelVendor As System.Windows.Forms.Button
    Friend WithEvents btnEditVendor As System.Windows.Forms.Button
    Friend WithEvents btnCreateVendor As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents tbVendorBrand As System.Windows.Forms.TextBox
    Friend WithEvents vndList As System.Windows.Forms.ListView
    Friend WithEvents vndName As System.Windows.Forms.ColumnHeader
    Friend WithEvents vndCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents vndBrand As System.Windows.Forms.ColumnHeader
    Friend WithEvents vndPrice As System.Windows.Forms.ColumnHeader
    Friend WithEvents vndCurrency As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents tbVendorCode As System.Windows.Forms.TextBox
    Friend WithEvents tbVendorName As System.Windows.Forms.TextBox
    Friend WithEvents tabCustomer As System.Windows.Forms.TabPage
    Friend WithEvents tbCustPrice As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents cbCustCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents btnDelCust As System.Windows.Forms.Button
    Friend WithEvents btnEditCust As System.Windows.Forms.Button
    Friend WithEvents btnCreateCust As System.Windows.Forms.Button
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents custList As System.Windows.Forms.ListView
    Friend WithEvents custName As System.Windows.Forms.ColumnHeader
    Friend WithEvents custCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents custPrice As System.Windows.Forms.ColumnHeader
    Friend WithEvents custCurrency As System.Windows.Forms.ColumnHeader
    Friend WithEvents minOrder As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents tbCustCode As System.Windows.Forms.TextBox
    Friend WithEvents tbCustName As System.Windows.Forms.TextBox
    Friend WithEvents tbCustMinOrder As System.Windows.Forms.TextBox
    Friend WithEvents tabFormula As System.Windows.Forms.TabPage
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents tabMaterial As System.Windows.Forms.TabPage
    Friend WithEvents formulaList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tabByProduct As System.Windows.Forms.TabPage
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tbUnitWeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents tbMaxStock As System.Windows.Forms.NumericUpDown
    Friend WithEvents tbMinStock As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbMaxUom As System.Windows.Forms.Label
    Friend WithEvents lbMinUom As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents cbSubCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbType As System.Windows.Forms.ComboBox
    Friend WithEvents chkSell As System.Windows.Forms.CheckBox
    Friend WithEvents chkBuy As System.Windows.Forms.CheckBox
    Friend WithEvents chkMake As System.Windows.Forms.CheckBox
    Friend WithEvents chkUse As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbUOM As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbCode As System.Windows.Forms.TextBox
    Friend WithEvents tbName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lbQtyUom As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbCOG As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbWeight As System.Windows.Forms.TextBox
    Friend WithEvents tbQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkIsSack As System.Windows.Forms.CheckBox
    Friend WithEvents tbThick As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents tbStockValue As System.Windows.Forms.TextBox
    Friend WithEvents isKarung As System.Windows.Forms.ColumnHeader
    Friend WithEvents searchTimer As System.Windows.Forms.Timer
    Friend WithEvents tabUOM As System.Windows.Forms.TabPage
    Friend WithEvents chkConsume As System.Windows.Forms.CheckBox
    Friend WithEvents tabSack As System.Windows.Forms.TabPage
    Friend WithEvents cbBagType As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents gbInnerBag As System.Windows.Forms.GroupBox
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbJumboBag As System.Windows.Forms.GroupBox
    Friend WithEvents NumericUpDown4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown5 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown6 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents ListView2 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Public WithEvents itemList As System.Windows.Forms.ListView
End Class
