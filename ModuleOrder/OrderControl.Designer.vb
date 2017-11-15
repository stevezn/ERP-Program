<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OrderControl
    Inherits ERPModules.ERPModule

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OrderControl))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkMonthFilter = New System.Windows.Forms.CheckBox()
        Me.dpDateFilter = New System.Windows.Forms.DateTimePicker()
        Me.lbSearch = New System.Windows.Forms.Label()
        Me.tbSearchSO = New System.Windows.Forms.TextBox()
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
        Me.imgLstSO = New System.Windows.Forms.ImageList(Me.components)
        Me.lbLotAvail = New System.Windows.Forms.Label()
        Me.lbOnCredit = New System.Windows.Forms.Label()
        Me.lbUsedLot = New System.Windows.Forms.Label()
        Me.lbCreditLimit = New System.Windows.Forms.Label()
        Me.lbMaxLot = New System.Windows.Forms.Label()
        Me.llbLotAvail = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dpCustPO = New System.Windows.Forms.DateTimePicker()
        Me.tcItems = New System.Windows.Forms.TabControl()
        Me.tpItem = New System.Windows.Forms.TabPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbNote = New System.Windows.Forms.TextBox()
        Me.tbRemark = New System.Windows.Forms.TextBox()
        Me.lstSOItem = New System.Windows.Forms.ListView()
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader37 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmSOItemMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SpecialPriceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tbItemSearch = New System.Windows.Forms.TextBox()
        Me.lbItem = New System.Windows.Forms.Label()
        Me.lbQuantity = New System.Windows.Forms.Label()
        Me.tbQuantity = New System.Windows.Forms.NumericUpDown()
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.btnDelItem = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.tbDestination = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlExped = New System.Windows.Forms.Panel()
        Me.lstExped = New System.Windows.Forms.ListView()
        Me.ColumnHeader32 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader33 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader35 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader29 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader30 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader31 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbExped = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
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
        Me.tbCustPO = New System.Windows.Forms.TextBox()
        Me.lbCustPO = New System.Windows.Forms.Label()
        Me.cbSOType = New System.Windows.Forms.ComboBox()
        Me.taxPanel = New System.Windows.Forms.Panel()
        Me.rbTaxExclusive = New System.Windows.Forms.RadioButton()
        Me.rbTaxInclusive = New System.Windows.Forms.RadioButton()
        Me.lbTaxMode = New System.Windows.Forms.Label()
        Me.lbExpectDate = New System.Windows.Forms.Label()
        Me.dpExpect = New System.Windows.Forms.DateTimePicker()
        Me.lbEstDate = New System.Windows.Forms.Label()
        Me.dpEstimate = New System.Windows.Forms.DateTimePicker()
        Me.lbDays = New System.Windows.Forms.Label()
        Me.lbTOP = New System.Windows.Forms.Label()
        Me.lbTerm = New System.Windows.Forms.Label()
        Me.lbSODate = New System.Windows.Forms.Label()
        Me.dpSO = New System.Windows.Forms.DateTimePicker()
        Me.tbSales = New System.Windows.Forms.TextBox()
        Me.lbSales = New System.Windows.Forms.Label()
        Me.tbCust = New System.Windows.Forms.TextBox()
        Me.lbCust = New System.Windows.Forms.Label()
        Me.tbSONum = New System.Windows.Forms.TextBox()
        Me.lbSONum = New System.Windows.Forms.Label()
        Me.pnlItem = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lstItemSearch = New System.Windows.Forms.ListView()
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader16 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlCust = New System.Windows.Forms.Panel()
        Me.lstCustSearch = New System.Windows.Forms.ListView()
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlLot = New System.Windows.Forms.Panel()
        Me.lstLot = New System.Windows.Forms.ListView()
        Me.ColumnHeader27 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader34 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader36 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader28 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.itemLoadTick = New System.Windows.Forms.Timer(Me.components)
        Me.itemSearchTick = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.tcItems.SuspendLayout()
        Me.tpItem.SuspendLayout()
        Me.cmSOItemMenu.SuspendLayout()
        CType(Me.tbQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.pnlExped.SuspendLayout()
        Me.taxPanel.SuspendLayout()
        Me.pnlItem.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCust.SuspendLayout()
        Me.pnlLot.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.tbSearchSO)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstSO)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbLotAvail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbOnCredit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbUsedLot)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbCreditLimit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbMaxLot)
        Me.SplitContainer1.Panel2.Controls.Add(Me.llbLotAvail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dpCustPO)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbCustPO)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbCustPO)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cbSOType)
        Me.SplitContainer1.Panel2.Controls.Add(Me.taxPanel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbExpectDate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dpExpect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbEstDate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dpEstimate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbDays)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTOP)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbTerm)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbSODate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dpSO)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbSales)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbSales)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbCust)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbCust)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbSONum)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbSONum)
        Me.SplitContainer1.Size = New System.Drawing.Size(962, 559)
        Me.SplitContainer1.SplitterDistance = 130
        Me.SplitContainer1.TabIndex = 0
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
        'tbSearchSO
        '
        Me.tbSearchSO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSearchSO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbSearchSO.Location = New System.Drawing.Point(50, 3)
        Me.tbSearchSO.Name = "tbSearchSO"
        Me.tbSearchSO.Size = New System.Drawing.Size(680, 23)
        Me.tbSearchSO.TabIndex = 0
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
        Me.lstSO.Size = New System.Drawing.Size(956, 95)
        Me.lstSO.SmallImageList = Me.imgLstSO
        Me.lstSO.TabIndex = 3
        Me.lstSO.UseCompatibleStateImageBehavior = False
        Me.lstSO.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "SO Number"
        Me.ColumnHeader5.Width = 120
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Date"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Customer"
        Me.ColumnHeader7.Width = 150
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "Sales Person"
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
        Me.ColumnHeader20.Text = "Est. Delivery Date"
        Me.ColumnHeader20.Width = 120
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
        'imgLstSO
        '
        Me.imgLstSO.ImageStream = CType(resources.GetObject("imgLstSO.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgLstSO.TransparentColor = System.Drawing.Color.Transparent
        Me.imgLstSO.Images.SetKeyName(0, "gears.png")
        Me.imgLstSO.Images.SetKeyName(1, "antivirus-1.png")
        Me.imgLstSO.Images.SetKeyName(2, "checked (2).png")
        Me.imgLstSO.Images.SetKeyName(3, "cancel (2).png")
        '
        'lbLotAvail
        '
        Me.lbLotAvail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbLotAvail.Location = New System.Drawing.Point(469, 399)
        Me.lbLotAvail.Name = "lbLotAvail"
        Me.lbLotAvail.Size = New System.Drawing.Size(95, 14)
        Me.lbLotAvail.TabIndex = 55
        Me.lbLotAvail.Text = "0"
        Me.lbLotAvail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbOnCredit
        '
        Me.lbOnCredit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbOnCredit.Location = New System.Drawing.Point(269, 383)
        Me.lbOnCredit.Name = "lbOnCredit"
        Me.lbOnCredit.Size = New System.Drawing.Size(346, 14)
        Me.lbOnCredit.TabIndex = 54
        Me.lbOnCredit.Text = "0"
        Me.lbOnCredit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbUsedLot
        '
        Me.lbUsedLot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbUsedLot.Location = New System.Drawing.Point(266, 399)
        Me.lbUsedLot.Name = "lbUsedLot"
        Me.lbUsedLot.Size = New System.Drawing.Size(95, 14)
        Me.lbUsedLot.TabIndex = 53
        Me.lbUsedLot.Text = "0"
        Me.lbUsedLot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbCreditLimit
        '
        Me.lbCreditLimit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbCreditLimit.Location = New System.Drawing.Point(80, 383)
        Me.lbCreditLimit.Name = "lbCreditLimit"
        Me.lbCreditLimit.Size = New System.Drawing.Size(95, 14)
        Me.lbCreditLimit.TabIndex = 52
        Me.lbCreditLimit.Text = "0"
        Me.lbCreditLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbMaxLot
        '
        Me.lbMaxLot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbMaxLot.Location = New System.Drawing.Point(80, 399)
        Me.lbMaxLot.Name = "lbMaxLot"
        Me.lbMaxLot.Size = New System.Drawing.Size(95, 14)
        Me.lbMaxLot.TabIndex = 51
        Me.lbMaxLot.Text = "0"
        Me.lbMaxLot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'llbLotAvail
        '
        Me.llbLotAvail.ActiveLinkColor = System.Drawing.Color.ForestGreen
        Me.llbLotAvail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbLotAvail.AutoSize = True
        Me.llbLotAvail.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline
        Me.llbLotAvail.LinkColor = System.Drawing.Color.DarkGreen
        Me.llbLotAvail.Location = New System.Drawing.Point(383, 399)
        Me.llbLotAvail.Name = "llbLotAvail"
        Me.llbLotAvail.Size = New System.Drawing.Size(78, 15)
        Me.llbLotAvail.TabIndex = 50
        Me.llbLotAvail.TabStop = True
        Me.llbLotAvail.Text = "Lot Available"
        Me.llbLotAvail.VisitedLinkColor = System.Drawing.Color.DarkGreen
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(201, 399)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 15)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Used Lot"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 399)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 15)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Max Lot"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(201, 383)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 15)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "On Credit"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 383)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 15)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Credit Limit"
        '
        'dpCustPO
        '
        Me.dpCustPO.Enabled = False
        Me.dpCustPO.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpCustPO.Location = New System.Drawing.Point(517, 61)
        Me.dpCustPO.Name = "dpCustPO"
        Me.dpCustPO.Size = New System.Drawing.Size(129, 23)
        Me.dpCustPO.TabIndex = 7
        '
        'tcItems
        '
        Me.tcItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcItems.Controls.Add(Me.tpItem)
        Me.tcItems.Controls.Add(Me.TabPage2)
        Me.tcItems.Location = New System.Drawing.Point(6, 119)
        Me.tcItems.Name = "tcItems"
        Me.tcItems.SelectedIndex = 0
        Me.tcItems.Size = New System.Drawing.Size(953, 261)
        Me.tcItems.TabIndex = 12
        '
        'tpItem
        '
        Me.tpItem.Controls.Add(Me.Label5)
        Me.tpItem.Controls.Add(Me.tbNote)
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
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(424, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 15)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "Note"
        '
        'tbNote
        '
        Me.tbNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbNote.Location = New System.Drawing.Point(462, 176)
        Me.tbNote.Multiline = True
        Me.tbNote.Name = "tbNote"
        Me.tbNote.Size = New System.Drawing.Size(143, 51)
        Me.tbNote.TabIndex = 28
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
        Me.tbRemark.TabIndex = 27
        '
        'lstSOItem
        '
        Me.lstSOItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstSOItem.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader10, Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader11, Me.ColumnHeader3, Me.ColumnHeader9, Me.ColumnHeader4, Me.ColumnHeader17, Me.ColumnHeader37})
        Me.lstSOItem.ContextMenuStrip = Me.cmSOItemMenu
        Me.lstSOItem.FullRowSelect = True
        Me.lstSOItem.HideSelection = False
        Me.lstSOItem.Location = New System.Drawing.Point(3, 3)
        Me.lstSOItem.MultiSelect = False
        Me.lstSOItem.Name = "lstSOItem"
        Me.lstSOItem.Size = New System.Drawing.Size(939, 167)
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
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Base Price"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader2.Width = 100
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
        'cmSOItemMenu
        '
        Me.cmSOItemMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SpecialPriceToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.cmSOItemMenu.Name = "cmSOItemMenu"
        Me.cmSOItemMenu.Size = New System.Drawing.Size(141, 48)
        '
        'SpecialPriceToolStripMenuItem
        '
        Me.SpecialPriceToolStripMenuItem.Name = "SpecialPriceToolStripMenuItem"
        Me.SpecialPriceToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.SpecialPriceToolStripMenuItem.Text = "Special Price"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
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
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.tbDestination)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.pnlExped)
        Me.TabPage2.Controls.Add(Me.ListView2)
        Me.TabPage2.Controls.Add(Me.tbExped)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(945, 233)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Shipment"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'tbDestination
        '
        Me.tbDestination.Location = New System.Drawing.Point(507, 3)
        Me.tbDestination.Name = "tbDestination"
        Me.tbDestination.Size = New System.Drawing.Size(299, 23)
        Me.tbDestination.TabIndex = 42
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(424, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 15)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "Destination"
        '
        'pnlExped
        '
        Me.pnlExped.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlExped.Controls.Add(Me.lstExped)
        Me.pnlExped.Location = New System.Drawing.Point(82, 328)
        Me.pnlExped.Name = "pnlExped"
        Me.pnlExped.Size = New System.Drawing.Size(473, 190)
        Me.pnlExped.TabIndex = 40
        Me.pnlExped.Visible = False
        '
        'lstExped
        '
        Me.lstExped.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader32, Me.ColumnHeader33, Me.ColumnHeader35})
        Me.lstExped.FullRowSelect = True
        Me.lstExped.HideSelection = False
        Me.lstExped.Location = New System.Drawing.Point(3, 3)
        Me.lstExped.MultiSelect = False
        Me.lstExped.Name = "lstExped"
        Me.lstExped.Size = New System.Drawing.Size(465, 182)
        Me.lstExped.TabIndex = 0
        Me.lstExped.UseCompatibleStateImageBehavior = False
        Me.lstExped.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader32
        '
        Me.ColumnHeader32.Text = "Code"
        Me.ColumnHeader32.Width = 100
        '
        'ColumnHeader33
        '
        Me.ColumnHeader33.Text = "Name"
        Me.ColumnHeader33.Width = 150
        '
        'ColumnHeader35
        '
        Me.ColumnHeader35.Text = "City"
        Me.ColumnHeader35.Width = 120
        '
        'ListView2
        '
        Me.ListView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader29, Me.ColumnHeader30, Me.ColumnHeader31})
        Me.ListView2.FullRowSelect = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(2, 32)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(940, 188)
        Me.ListView2.TabIndex = 2
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader29
        '
        Me.ColumnHeader29.Text = "Shipping Date"
        Me.ColumnHeader29.Width = 150
        '
        'ColumnHeader30
        '
        Me.ColumnHeader30.Text = "Quantity Shipped"
        Me.ColumnHeader30.Width = 150
        '
        'ColumnHeader31
        '
        Me.ColumnHeader31.Text = "Expedition"
        Me.ColumnHeader31.Width = 250
        '
        'tbExped
        '
        Me.tbExped.Location = New System.Drawing.Point(82, 3)
        Me.tbExped.Name = "tbExped"
        Me.tbExped.Size = New System.Drawing.Size(299, 23)
        Me.tbExped.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(-1, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 15)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Expedition"
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
        Me.tbRate.Location = New System.Drawing.Point(517, 90)
        Me.tbRate.Name = "tbRate"
        Me.tbRate.Size = New System.Drawing.Size(129, 23)
        Me.tbRate.TabIndex = 10
        '
        'lbRate
        '
        Me.lbRate.AutoSize = True
        Me.lbRate.Location = New System.Drawing.Point(480, 93)
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
        Me.cbCurrency.Location = New System.Drawing.Point(396, 90)
        Me.cbCurrency.Name = "cbCurrency"
        Me.cbCurrency.Size = New System.Drawing.Size(64, 23)
        Me.cbCurrency.TabIndex = 9
        '
        'lbCurrency
        '
        Me.lbCurrency.AutoSize = True
        Me.lbCurrency.Location = New System.Drawing.Point(313, 94)
        Me.lbCurrency.Name = "lbCurrency"
        Me.lbCurrency.Size = New System.Drawing.Size(56, 15)
        Me.lbCurrency.TabIndex = 31
        Me.lbCurrency.Text = "Currency"
        '
        'tbCustPO
        '
        Me.tbCustPO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbCustPO.Location = New System.Drawing.Point(396, 61)
        Me.tbCustPO.Name = "tbCustPO"
        Me.tbCustPO.Size = New System.Drawing.Size(115, 23)
        Me.tbCustPO.TabIndex = 6
        '
        'lbCustPO
        '
        Me.lbCustPO.AutoSize = True
        Me.lbCustPO.Location = New System.Drawing.Point(313, 64)
        Me.lbCustPO.Name = "lbCustPO"
        Me.lbCustPO.Size = New System.Drawing.Size(78, 15)
        Me.lbCustPO.TabIndex = 29
        Me.lbCustPO.Text = "Customer PO"
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
        Me.taxPanel.Location = New System.Drawing.Point(670, 32)
        Me.taxPanel.Name = "taxPanel"
        Me.taxPanel.Size = New System.Drawing.Size(285, 25)
        Me.taxPanel.TabIndex = 11
        '
        'rbTaxExclusive
        '
        Me.rbTaxExclusive.AutoSize = True
        Me.rbTaxExclusive.Checked = True
        Me.rbTaxExclusive.Location = New System.Drawing.Point(185, 4)
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
        Me.rbTaxInclusive.Location = New System.Drawing.Point(89, 4)
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
        Me.lbExpectDate.Location = New System.Drawing.Point(4, 94)
        Me.lbExpectDate.Name = "lbExpectDate"
        Me.lbExpectDate.Size = New System.Drawing.Size(64, 15)
        Me.lbExpectDate.TabIndex = 17
        Me.lbExpectDate.Text = "E.T. Arrival"
        '
        'dpExpect
        '
        Me.dpExpect.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpExpect.Location = New System.Drawing.Point(92, 90)
        Me.dpExpect.Name = "dpExpect"
        Me.dpExpect.Size = New System.Drawing.Size(196, 23)
        Me.dpExpect.TabIndex = 8
        '
        'lbEstDate
        '
        Me.lbEstDate.AutoSize = True
        Me.lbEstDate.Location = New System.Drawing.Point(3, 64)
        Me.lbEstDate.Name = "lbEstDate"
        Me.lbEstDate.Size = New System.Drawing.Size(71, 15)
        Me.lbEstDate.TabIndex = 15
        Me.lbEstDate.Text = "E.T. Delivery"
        '
        'dpEstimate
        '
        Me.dpEstimate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpEstimate.Location = New System.Drawing.Point(92, 61)
        Me.dpEstimate.Name = "dpEstimate"
        Me.dpEstimate.Size = New System.Drawing.Size(196, 23)
        Me.dpEstimate.TabIndex = 5
        '
        'lbDays
        '
        Me.lbDays.AutoSize = True
        Me.lbDays.Location = New System.Drawing.Point(811, 6)
        Me.lbDays.Name = "lbDays"
        Me.lbDays.Size = New System.Drawing.Size(33, 15)
        Me.lbDays.TabIndex = 10
        Me.lbDays.Text = "days"
        '
        'lbTOP
        '
        Me.lbTOP.AutoSize = True
        Me.lbTOP.Location = New System.Drawing.Point(776, 6)
        Me.lbTOP.Name = "lbTOP"
        Me.lbTOP.Size = New System.Drawing.Size(11, 15)
        Me.lbTOP.TabIndex = 9
        Me.lbTOP.Text = "-"
        '
        'lbTerm
        '
        Me.lbTerm.AutoSize = True
        Me.lbTerm.Location = New System.Drawing.Point(670, 6)
        Me.lbTerm.Name = "lbTerm"
        Me.lbTerm.Size = New System.Drawing.Size(97, 15)
        Me.lbTerm.TabIndex = 8
        Me.lbTerm.Text = "Term of Payment"
        '
        'lbSODate
        '
        Me.lbSODate.AutoSize = True
        Me.lbSODate.Location = New System.Drawing.Point(4, 35)
        Me.lbSODate.Name = "lbSODate"
        Me.lbSODate.Size = New System.Drawing.Size(50, 15)
        Me.lbSODate.TabIndex = 7
        Me.lbSODate.Text = "SO Date"
        '
        'dpSO
        '
        Me.dpSO.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpSO.Location = New System.Drawing.Point(92, 32)
        Me.dpSO.Name = "dpSO"
        Me.dpSO.Size = New System.Drawing.Size(196, 23)
        Me.dpSO.TabIndex = 3
        '
        'tbSales
        '
        Me.tbSales.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbSales.Location = New System.Drawing.Point(396, 32)
        Me.tbSales.Name = "tbSales"
        Me.tbSales.ReadOnly = True
        Me.tbSales.Size = New System.Drawing.Size(250, 23)
        Me.tbSales.TabIndex = 4
        '
        'lbSales
        '
        Me.lbSales.AutoSize = True
        Me.lbSales.Location = New System.Drawing.Point(313, 35)
        Me.lbSales.Name = "lbSales"
        Me.lbSales.Size = New System.Drawing.Size(77, 15)
        Me.lbSales.TabIndex = 4
        Me.lbSales.Text = "Sales Person"
        '
        'tbCust
        '
        Me.tbCust.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbCust.Location = New System.Drawing.Point(396, 3)
        Me.tbCust.Name = "tbCust"
        Me.tbCust.Size = New System.Drawing.Size(250, 23)
        Me.tbCust.TabIndex = 2
        '
        'lbCust
        '
        Me.lbCust.AutoSize = True
        Me.lbCust.Location = New System.Drawing.Point(313, 6)
        Me.lbCust.Name = "lbCust"
        Me.lbCust.Size = New System.Drawing.Size(59, 15)
        Me.lbCust.TabIndex = 2
        Me.lbCust.Text = "Customer"
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
        'lbSONum
        '
        Me.lbSONum.AutoSize = True
        Me.lbSONum.Location = New System.Drawing.Point(4, 6)
        Me.lbSONum.Name = "lbSONum"
        Me.lbSONum.Size = New System.Drawing.Size(68, 15)
        Me.lbSONum.TabIndex = 0
        Me.lbSONum.Text = "SO Number"
        '
        'pnlItem
        '
        Me.pnlItem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlItem.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlItem.Location = New System.Drawing.Point(8, 623)
        Me.pnlItem.Name = "pnlItem"
        Me.pnlItem.Size = New System.Drawing.Size(608, 197)
        Me.pnlItem.TabIndex = 27
        Me.pnlItem.Visible = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PictureBox1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lstItemSearch, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(606, 195)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(459, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(144, 189)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lstItemSearch
        '
        Me.lstItemSearch.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader8, Me.ColumnHeader16})
        Me.lstItemSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstItemSearch.FullRowSelect = True
        Me.lstItemSearch.HideSelection = False
        Me.lstItemSearch.Location = New System.Drawing.Point(3, 3)
        Me.lstItemSearch.MultiSelect = False
        Me.lstItemSearch.Name = "lstItemSearch"
        Me.lstItemSearch.Size = New System.Drawing.Size(450, 189)
        Me.lstItemSearch.TabIndex = 1
        Me.lstItemSearch.UseCompatibleStateImageBehavior = False
        Me.lstItemSearch.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Item Code"
        Me.ColumnHeader8.Width = 100
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "Item Name"
        Me.ColumnHeader16.Width = 250
        '
        'pnlCust
        '
        Me.pnlCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCust.Controls.Add(Me.lstCustSearch)
        Me.pnlCust.Location = New System.Drawing.Point(396, 600)
        Me.pnlCust.Name = "pnlCust"
        Me.pnlCust.Size = New System.Drawing.Size(473, 190)
        Me.pnlCust.TabIndex = 39
        Me.pnlCust.Visible = False
        '
        'lstCustSearch
        '
        Me.lstCustSearch.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14, Me.ColumnHeader15})
        Me.lstCustSearch.FullRowSelect = True
        Me.lstCustSearch.HideSelection = False
        Me.lstCustSearch.Location = New System.Drawing.Point(3, 3)
        Me.lstCustSearch.MultiSelect = False
        Me.lstCustSearch.Name = "lstCustSearch"
        Me.lstCustSearch.Size = New System.Drawing.Size(465, 182)
        Me.lstCustSearch.TabIndex = 0
        Me.lstCustSearch.UseCompatibleStateImageBehavior = False
        Me.lstCustSearch.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Code"
        Me.ColumnHeader12.Width = 100
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Name"
        Me.ColumnHeader13.Width = 150
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Type"
        Me.ColumnHeader14.Width = 100
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "City"
        Me.ColumnHeader15.Width = 120
        '
        'pnlLot
        '
        Me.pnlLot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlLot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLot.Controls.Add(Me.lstLot)
        Me.pnlLot.Location = New System.Drawing.Point(384, 645)
        Me.pnlLot.Name = "pnlLot"
        Me.pnlLot.Size = New System.Drawing.Size(403, 187)
        Me.pnlLot.TabIndex = 51
        Me.pnlLot.Visible = False
        '
        'lstLot
        '
        Me.lstLot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstLot.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader27, Me.ColumnHeader34, Me.ColumnHeader36, Me.ColumnHeader28})
        Me.lstLot.FullRowSelect = True
        Me.lstLot.HideSelection = False
        Me.lstLot.Location = New System.Drawing.Point(3, 3)
        Me.lstLot.Name = "lstLot"
        Me.lstLot.Size = New System.Drawing.Size(395, 179)
        Me.lstLot.TabIndex = 0
        Me.lstLot.UseCompatibleStateImageBehavior = False
        Me.lstLot.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader27
        '
        Me.ColumnHeader27.Text = "Delivery Date"
        Me.ColumnHeader27.Width = 90
        '
        'ColumnHeader34
        '
        Me.ColumnHeader34.Text = "Maximum"
        Me.ColumnHeader34.Width = 75
        '
        'ColumnHeader36
        '
        Me.ColumnHeader36.Text = "On Order"
        Me.ColumnHeader36.Width = 75
        '
        'ColumnHeader28
        '
        Me.ColumnHeader28.Text = "Available"
        Me.ColumnHeader28.Width = 75
        '
        'itemLoadTick
        '
        '
        'itemSearchTick
        '
        Me.itemSearchTick.Interval = 400
        '
        'OrderControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.pnlLot)
        Me.Controls.Add(Me.pnlCust)
        Me.Controls.Add(Me.pnlItem)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "OrderControl"
        Me.Size = New System.Drawing.Size(962, 559)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.tcItems.ResumeLayout(False)
        Me.tpItem.ResumeLayout(False)
        Me.tpItem.PerformLayout()
        Me.cmSOItemMenu.ResumeLayout(False)
        CType(Me.tbQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.pnlExped.ResumeLayout(False)
        Me.taxPanel.ResumeLayout(False)
        Me.taxPanel.PerformLayout()
        Me.pnlItem.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCust.ResumeLayout(False)
        Me.pnlLot.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lstSO As System.Windows.Forms.ListView
    Friend WithEvents dpDateFilter As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbSearch As System.Windows.Forms.Label
    Friend WithEvents tbSearchSO As System.Windows.Forms.TextBox
    Friend WithEvents tbSONum As System.Windows.Forms.TextBox
    Friend WithEvents lbSONum As System.Windows.Forms.Label
    Friend WithEvents lbExpectDate As System.Windows.Forms.Label
    Friend WithEvents dpExpect As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbEstDate As System.Windows.Forms.Label
    Friend WithEvents dpEstimate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbDays As System.Windows.Forms.Label
    Friend WithEvents lbTOP As System.Windows.Forms.Label
    Friend WithEvents lbTerm As System.Windows.Forms.Label
    Friend WithEvents lbSODate As System.Windows.Forms.Label
    Friend WithEvents dpSO As System.Windows.Forms.DateTimePicker
    Friend WithEvents tbSales As System.Windows.Forms.TextBox
    Friend WithEvents lbSales As System.Windows.Forms.Label
    Friend WithEvents tbCust As System.Windows.Forms.TextBox
    Friend WithEvents lbCust As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlItem As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lstItemSearch As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents taxPanel As System.Windows.Forms.Panel
    Friend WithEvents rbTaxExclusive As System.Windows.Forms.RadioButton
    Friend WithEvents rbTaxInclusive As System.Windows.Forms.RadioButton
    Friend WithEvents lbTaxMode As System.Windows.Forms.Label
    Friend WithEvents chkMonthFilter As System.Windows.Forms.CheckBox
    Friend WithEvents cbSOType As System.Windows.Forms.ComboBox
    Friend WithEvents cbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents lbCurrency As System.Windows.Forms.Label
    Friend WithEvents tbCustPO As System.Windows.Forms.TextBox
    Friend WithEvents lbCustPO As System.Windows.Forms.Label
    Friend WithEvents lbTotal As System.Windows.Forms.Label
    Friend WithEvents tbRate As System.Windows.Forms.TextBox
    Friend WithEvents lbRate As System.Windows.Forms.Label
    Friend WithEvents lbTotalAfterTax As System.Windows.Forms.Label
    Friend WithEvents lbTax As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlCust As System.Windows.Forms.Panel
    Friend WithEvents lstCustSearch As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lbTotalAmt As System.Windows.Forms.Label
    Friend WithEvents lbTotalWithTax As System.Windows.Forms.Label
    Friend WithEvents lbTaxAmt As System.Windows.Forms.Label
    Friend WithEvents tcItems As System.Windows.Forms.TabControl
    Friend WithEvents tpItem As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents tbQuantity As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnDelItem As System.Windows.Forms.Button
    Friend WithEvents btnAddItem As System.Windows.Forms.Button
    Friend WithEvents lbQuantity As System.Windows.Forms.Label
    Friend WithEvents lbItem As System.Windows.Forms.Label
    Friend WithEvents tbItemSearch As System.Windows.Forms.TextBox
    Friend WithEvents lstSOItem As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents dpCustPO As System.Windows.Forms.DateTimePicker
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader21 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader22 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader23 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader24 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader25 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader26 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents llbLotAvail As System.Windows.Forms.LinkLabel
    Friend WithEvents pnlLot As System.Windows.Forms.Panel
    Friend WithEvents lstLot As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader27 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader28 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lbLotAvail As System.Windows.Forms.Label
    Friend WithEvents lbOnCredit As System.Windows.Forms.Label
    Friend WithEvents lbUsedLot As System.Windows.Forms.Label
    Friend WithEvents lbCreditLimit As System.Windows.Forms.Label
    Friend WithEvents lbMaxLot As System.Windows.Forms.Label
    Friend WithEvents ListView2 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader29 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader30 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader31 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbExped As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pnlExped As System.Windows.Forms.Panel
    Friend WithEvents lstExped As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader32 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader33 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader35 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbRemark As System.Windows.Forms.TextBox
    Friend WithEvents itemLoadTick As System.Windows.Forms.Timer
    Friend WithEvents ColumnHeader34 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader36 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbNote As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader37 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmSOItemMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SpecialPriceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents itemSearchTick As System.Windows.Forms.Timer
    Friend WithEvents imgLstSO As System.Windows.Forms.ImageList
    Friend WithEvents tbDestination As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
