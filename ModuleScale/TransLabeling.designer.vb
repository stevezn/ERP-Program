<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TransLabeling
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
        Me.gbLabel = New System.Windows.Forms.GroupBox()
        Me.lblNetto = New System.Windows.Forms.Label()
        Me.tbNetto = New System.Windows.Forms.TextBox()
        Me.cbUOM = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tbOPP = New System.Windows.Forms.TextBox()
        Me.rbOut = New System.Windows.Forms.RadioButton()
        Me.rbIn = New System.Windows.Forms.RadioButton()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnDelItem = New System.Windows.Forms.Button()
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tbFromDocNum = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbJumlah = New System.Windows.Forms.TextBox()
        Me.lstItems = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbCetakan = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbWarna = New System.Windows.Forms.TextBox()
        Me.btnPanjang = New System.Windows.Forms.Button()
        Me.btnLebar = New System.Windows.Forms.Button()
        Me.btnAnyaman = New System.Windows.Forms.Button()
        Me.btnDenier = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tbPanjang = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.tbDenier = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.tbLebar = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.tbAnyaman = New System.Windows.Forms.TextBox()
        Me.cbToLine = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbFromLine = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblStationEX = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblOperator = New System.Windows.Forms.Label()
        Me.btnItemFind = New System.Windows.Forms.Button()
        Me.cbShifts = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbItemName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbItem = New System.Windows.Forms.TextBox()
        Me.tick = New System.Windows.Forms.Timer(Me.components)
        Me.btnTanggal = New System.Windows.Forms.Button()
        Me.tbCari = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lstData = New System.Windows.Forms.ListView()
        Me.listCol1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listCol2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listCol3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listCol4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listCol5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listCol6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmDocs = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tmReprint = New System.Windows.Forms.ToolStripMenuItem()
        Me.VoidToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dateTick = New System.Windows.Forms.Timer(Me.components)
        Me.lstFind = New System.Windows.Forms.ListView()
        Me.findCol1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.syncTick = New System.Windows.Forms.Timer(Me.components)
        Me.gbLabel.SuspendLayout()
        Me.cmDocs.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbLabel
        '
        Me.gbLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbLabel.Controls.Add(Me.lblNetto)
        Me.gbLabel.Controls.Add(Me.tbNetto)
        Me.gbLabel.Controls.Add(Me.cbUOM)
        Me.gbLabel.Controls.Add(Me.btnClear)
        Me.gbLabel.Controls.Add(Me.Label12)
        Me.gbLabel.Controls.Add(Me.tbOPP)
        Me.gbLabel.Controls.Add(Me.rbOut)
        Me.gbLabel.Controls.Add(Me.rbIn)
        Me.gbLabel.Controls.Add(Me.btnPrint)
        Me.gbLabel.Controls.Add(Me.btnDelItem)
        Me.gbLabel.Controls.Add(Me.btnAddItem)
        Me.gbLabel.Controls.Add(Me.Label11)
        Me.gbLabel.Controls.Add(Me.tbFromDocNum)
        Me.gbLabel.Controls.Add(Me.Label9)
        Me.gbLabel.Controls.Add(Me.tbJumlah)
        Me.gbLabel.Controls.Add(Me.lstItems)
        Me.gbLabel.Controls.Add(Me.Label8)
        Me.gbLabel.Controls.Add(Me.tbCetakan)
        Me.gbLabel.Controls.Add(Me.Label5)
        Me.gbLabel.Controls.Add(Me.tbWarna)
        Me.gbLabel.Controls.Add(Me.btnPanjang)
        Me.gbLabel.Controls.Add(Me.btnLebar)
        Me.gbLabel.Controls.Add(Me.btnAnyaman)
        Me.gbLabel.Controls.Add(Me.btnDenier)
        Me.gbLabel.Controls.Add(Me.Label14)
        Me.gbLabel.Controls.Add(Me.tbPanjang)
        Me.gbLabel.Controls.Add(Me.Label41)
        Me.gbLabel.Controls.Add(Me.tbDenier)
        Me.gbLabel.Controls.Add(Me.Label42)
        Me.gbLabel.Controls.Add(Me.tbLebar)
        Me.gbLabel.Controls.Add(Me.Label43)
        Me.gbLabel.Controls.Add(Me.tbAnyaman)
        Me.gbLabel.Controls.Add(Me.cbToLine)
        Me.gbLabel.Controls.Add(Me.Label4)
        Me.gbLabel.Controls.Add(Me.cbFromLine)
        Me.gbLabel.Controls.Add(Me.Label2)
        Me.gbLabel.Controls.Add(Me.lblStationEX)
        Me.gbLabel.Controls.Add(Me.Label1)
        Me.gbLabel.Controls.Add(Me.lblDate)
        Me.gbLabel.Controls.Add(Me.Label3)
        Me.gbLabel.Controls.Add(Me.lblOperator)
        Me.gbLabel.Controls.Add(Me.btnItemFind)
        Me.gbLabel.Controls.Add(Me.cbShifts)
        Me.gbLabel.Controls.Add(Me.Label10)
        Me.gbLabel.Controls.Add(Me.Label7)
        Me.gbLabel.Controls.Add(Me.tbItemName)
        Me.gbLabel.Controls.Add(Me.Label6)
        Me.gbLabel.Controls.Add(Me.tbItem)
        Me.gbLabel.Location = New System.Drawing.Point(3, 3)
        Me.gbLabel.Name = "gbLabel"
        Me.gbLabel.Size = New System.Drawing.Size(1097, 358)
        Me.gbLabel.TabIndex = 34
        Me.gbLabel.TabStop = False
        Me.gbLabel.Text = "Label Data"
        '
        'lblNetto
        '
        Me.lblNetto.AutoSize = True
        Me.lblNetto.Location = New System.Drawing.Point(969, 140)
        Me.lblNetto.Name = "lblNetto"
        Me.lblNetto.Size = New System.Drawing.Size(36, 15)
        Me.lblNetto.TabIndex = 126
        Me.lblNetto.Text = "Netto"
        Me.lblNetto.Visible = False
        '
        'tbNetto
        '
        Me.tbNetto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbNetto.Location = New System.Drawing.Point(1011, 137)
        Me.tbNetto.Name = "tbNetto"
        Me.tbNetto.Size = New System.Drawing.Size(80, 23)
        Me.tbNetto.TabIndex = 14
        Me.tbNetto.Visible = False
        '
        'cbUOM
        '
        Me.cbUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUOM.FormattingEnabled = True
        Me.cbUOM.Items.AddRange(New Object() {"MTR", "LBR", "KG"})
        Me.cbUOM.Location = New System.Drawing.Point(908, 137)
        Me.cbUOM.Name = "cbUOM"
        Me.cbUOM.Size = New System.Drawing.Size(55, 23)
        Me.cbUOM.TabIndex = 13
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(969, 240)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(122, 31)
        Me.btnClear.TabIndex = 17
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(517, 140)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(30, 15)
        Me.Label12.TabIndex = 123
        Me.Label12.Text = "OPP"
        '
        'tbOPP
        '
        Me.tbOPP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbOPP.Enabled = False
        Me.tbOPP.Location = New System.Drawing.Point(596, 137)
        Me.tbOPP.Name = "tbOPP"
        Me.tbOPP.Size = New System.Drawing.Size(121, 23)
        Me.tbOPP.TabIndex = 11
        '
        'rbOut
        '
        Me.rbOut.AutoSize = True
        Me.rbOut.Location = New System.Drawing.Point(900, 79)
        Me.rbOut.Name = "rbOut"
        Me.rbOut.Size = New System.Drawing.Size(45, 19)
        Me.rbOut.TabIndex = 4
        Me.rbOut.Text = "Out"
        Me.rbOut.UseVisualStyleBackColor = True
        '
        'rbIn
        '
        Me.rbIn.AutoSize = True
        Me.rbIn.Location = New System.Drawing.Point(841, 79)
        Me.rbIn.Name = "rbIn"
        Me.rbIn.Size = New System.Drawing.Size(36, 19)
        Me.rbIn.TabIndex = 3
        Me.rbIn.Text = "In"
        Me.rbIn.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Calibri", 17.0!)
        Me.btnPrint.Location = New System.Drawing.Point(969, 277)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(122, 70)
        Me.btnPrint.TabIndex = 18
        Me.btnPrint.Text = "Print Label"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnDelItem
        '
        Me.btnDelItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelItem.Location = New System.Drawing.Point(969, 203)
        Me.btnDelItem.Name = "btnDelItem"
        Me.btnDelItem.Size = New System.Drawing.Size(122, 31)
        Me.btnDelItem.TabIndex = 16
        Me.btnDelItem.Text = "Delete"
        Me.btnDelItem.UseVisualStyleBackColor = True
        '
        'btnAddItem
        '
        Me.btnAddItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddItem.Location = New System.Drawing.Point(969, 166)
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Size = New System.Drawing.Size(122, 31)
        Me.btnAddItem.TabIndex = 15
        Me.btnAddItem.Text = "Add"
        Me.btnAddItem.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(517, 81)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 15)
        Me.Label11.TabIndex = 116
        Me.Label11.Text = "Dari Doc Number"
        '
        'tbFromDocNum
        '
        Me.tbFromDocNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbFromDocNum.Enabled = False
        Me.tbFromDocNum.Location = New System.Drawing.Point(628, 78)
        Me.tbFromDocNum.Name = "tbFromDocNum"
        Me.tbFromDocNum.Size = New System.Drawing.Size(173, 23)
        Me.tbFromDocNum.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(753, 140)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 15)
        Me.Label9.TabIndex = 114
        Me.Label9.Text = "Jumlah"
        '
        'tbJumlah
        '
        Me.tbJumlah.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbJumlah.Location = New System.Drawing.Point(815, 137)
        Me.tbJumlah.Name = "tbJumlah"
        Me.tbJumlah.Size = New System.Drawing.Size(87, 23)
        Me.tbJumlah.TabIndex = 12
        '
        'lstItems
        '
        Me.lstItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11})
        Me.lstItems.FullRowSelect = True
        Me.lstItems.HideSelection = False
        Me.lstItems.Location = New System.Drawing.Point(9, 166)
        Me.lstItems.MultiSelect = False
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(954, 181)
        Me.lstItems.TabIndex = 112
        Me.lstItems.UseCompatibleStateImageBehavior = False
        Me.lstItems.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Anyaman"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Denier"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Lebar"
        Me.ColumnHeader3.Width = 100
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Panjang"
        Me.ColumnHeader4.Width = 100
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Warna"
        Me.ColumnHeader5.Width = 100
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Cetakan"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "OPP"
        Me.ColumnHeader7.Width = 100
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Jumlah"
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "UOM"
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Netto"
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Source Label"
        Me.ColumnHeader11.Width = 160
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(298, 140)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 15)
        Me.Label8.TabIndex = 111
        Me.Label8.Text = "Cetakan"
        '
        'tbCetakan
        '
        Me.tbCetakan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbCetakan.Enabled = False
        Me.tbCetakan.Location = New System.Drawing.Point(351, 137)
        Me.tbCetakan.Name = "tbCetakan"
        Me.tbCetakan.Size = New System.Drawing.Size(121, 23)
        Me.tbCetakan.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 15)
        Me.Label5.TabIndex = 109
        Me.Label5.Text = "Warna"
        '
        'tbWarna
        '
        Me.tbWarna.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbWarna.Enabled = False
        Me.tbWarna.Location = New System.Drawing.Point(124, 137)
        Me.tbWarna.Name = "tbWarna"
        Me.tbWarna.Size = New System.Drawing.Size(121, 23)
        Me.tbWarna.TabIndex = 9
        '
        'btnPanjang
        '
        Me.btnPanjang.Location = New System.Drawing.Point(908, 106)
        Me.btnPanjang.Name = "btnPanjang"
        Me.btnPanjang.Size = New System.Drawing.Size(29, 25)
        Me.btnPanjang.TabIndex = 107
        Me.btnPanjang.TabStop = False
        Me.btnPanjang.Text = "..."
        Me.btnPanjang.UseVisualStyleBackColor = True
        '
        'btnLebar
        '
        Me.btnLebar.Location = New System.Drawing.Point(689, 106)
        Me.btnLebar.Name = "btnLebar"
        Me.btnLebar.Size = New System.Drawing.Size(29, 25)
        Me.btnLebar.TabIndex = 106
        Me.btnLebar.TabStop = False
        Me.btnLebar.Text = "..."
        Me.btnLebar.UseVisualStyleBackColor = True
        '
        'btnAnyaman
        '
        Me.btnAnyaman.Location = New System.Drawing.Point(217, 106)
        Me.btnAnyaman.Name = "btnAnyaman"
        Me.btnAnyaman.Size = New System.Drawing.Size(29, 25)
        Me.btnAnyaman.TabIndex = 105
        Me.btnAnyaman.TabStop = False
        Me.btnAnyaman.Text = "..."
        Me.btnAnyaman.UseVisualStyleBackColor = True
        '
        'btnDenier
        '
        Me.btnDenier.Location = New System.Drawing.Point(444, 106)
        Me.btnDenier.Name = "btnDenier"
        Me.btnDenier.Size = New System.Drawing.Size(29, 25)
        Me.btnDenier.TabIndex = 104
        Me.btnDenier.TabStop = False
        Me.btnDenier.Text = "..."
        Me.btnDenier.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(753, 110)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(52, 15)
        Me.Label14.TabIndex = 103
        Me.Label14.Text = "Panjang"
        '
        'tbPanjang
        '
        Me.tbPanjang.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbPanjang.Enabled = False
        Me.tbPanjang.Location = New System.Drawing.Point(815, 107)
        Me.tbPanjang.Name = "tbPanjang"
        Me.tbPanjang.Size = New System.Drawing.Size(87, 23)
        Me.tbPanjang.TabIndex = 8
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(298, 110)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(43, 15)
        Me.Label41.TabIndex = 102
        Me.Label41.Text = "Denier"
        '
        'tbDenier
        '
        Me.tbDenier.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbDenier.Enabled = False
        Me.tbDenier.Location = New System.Drawing.Point(351, 107)
        Me.tbDenier.Name = "tbDenier"
        Me.tbDenier.Size = New System.Drawing.Size(87, 23)
        Me.tbDenier.TabIndex = 6
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(517, 110)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(37, 15)
        Me.Label42.TabIndex = 101
        Me.Label42.Text = "Lebar"
        '
        'tbLebar
        '
        Me.tbLebar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbLebar.Enabled = False
        Me.tbLebar.Location = New System.Drawing.Point(596, 107)
        Me.tbLebar.Name = "tbLebar"
        Me.tbLebar.Size = New System.Drawing.Size(87, 23)
        Me.tbLebar.TabIndex = 7
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(6, 110)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(58, 15)
        Me.Label43.TabIndex = 100
        Me.Label43.Text = "Anyaman"
        '
        'tbAnyaman
        '
        Me.tbAnyaman.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbAnyaman.Enabled = False
        Me.tbAnyaman.Location = New System.Drawing.Point(124, 107)
        Me.tbAnyaman.Name = "tbAnyaman"
        Me.tbAnyaman.Size = New System.Drawing.Size(87, 23)
        Me.tbAnyaman.TabIndex = 5
        '
        'cbToLine
        '
        Me.cbToLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbToLine.FormattingEnabled = True
        Me.cbToLine.Items.AddRange(New Object() {"Circular Loom", "Cutting Sewing", "Insert Inner", "Printing", "OPP", "Packing", "Jahit"})
        Me.cbToLine.Location = New System.Drawing.Point(351, 78)
        Me.cbToLine.Name = "cbToLine"
        Me.cbToLine.Size = New System.Drawing.Size(121, 23)
        Me.cbToLine.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(298, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 15)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Ke Line"
        '
        'cbFromLine
        '
        Me.cbFromLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFromLine.FormattingEnabled = True
        Me.cbFromLine.Items.AddRange(New Object() {"Extruder", "Circular Loom", "Cutting Sewing", "Insert Inner", "Printing", "OPP", "Jahit"})
        Me.cbFromLine.Location = New System.Drawing.Point(124, 78)
        Me.cbFromLine.Name = "cbFromLine"
        Me.cbFromLine.Size = New System.Drawing.Size(121, 23)
        Me.cbFromLine.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 15)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Dari Line"
        '
        'lblStationEX
        '
        Me.lblStationEX.AutoSize = True
        Me.lblStationEX.Location = New System.Drawing.Point(298, 52)
        Me.lblStationEX.Name = "lblStationEX"
        Me.lblStationEX.Size = New System.Drawing.Size(43, 15)
        Me.lblStationEX.TabIndex = 36
        Me.lblStationEX.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 15)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Date and Time"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(121, 23)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(43, 15)
        Me.lblDate.TabIndex = 33
        Me.lblDate.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 15)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Operator"
        '
        'lblOperator
        '
        Me.lblOperator.AutoSize = True
        Me.lblOperator.Location = New System.Drawing.Point(121, 52)
        Me.lblOperator.Name = "lblOperator"
        Me.lblOperator.Size = New System.Drawing.Size(43, 15)
        Me.lblOperator.TabIndex = 35
        Me.lblOperator.Text = "Label2"
        '
        'btnItemFind
        '
        Me.btnItemFind.Location = New System.Drawing.Point(772, 19)
        Me.btnItemFind.Name = "btnItemFind"
        Me.btnItemFind.Size = New System.Drawing.Size(29, 25)
        Me.btnItemFind.TabIndex = 28
        Me.btnItemFind.TabStop = False
        Me.btnItemFind.Text = "..."
        Me.btnItemFind.UseVisualStyleBackColor = True
        Me.btnItemFind.Visible = False
        '
        'cbShifts
        '
        Me.cbShifts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbShifts.FormattingEnabled = True
        Me.cbShifts.Location = New System.Drawing.Point(351, 20)
        Me.cbShifts.Name = "cbShifts"
        Me.cbShifts.Size = New System.Drawing.Size(119, 23)
        Me.cbShifts.TabIndex = 17
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(298, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 15)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Shift"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(517, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 15)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Item Name"
        Me.Label7.Visible = False
        '
        'tbItemName
        '
        Me.tbItemName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbItemName.Location = New System.Drawing.Point(596, 49)
        Me.tbItemName.Name = "tbItemName"
        Me.tbItemName.ReadOnly = True
        Me.tbItemName.Size = New System.Drawing.Size(205, 23)
        Me.tbItemName.TabIndex = 19
        Me.tbItemName.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(517, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 15)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Item Code"
        Me.Label6.Visible = False
        '
        'tbItem
        '
        Me.tbItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbItem.Location = New System.Drawing.Point(596, 20)
        Me.tbItem.Name = "tbItem"
        Me.tbItem.Size = New System.Drawing.Size(170, 23)
        Me.tbItem.TabIndex = 18
        Me.tbItem.Visible = False
        '
        'btnTanggal
        '
        Me.btnTanggal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTanggal.Location = New System.Drawing.Point(1030, 371)
        Me.btnTanggal.Name = "btnTanggal"
        Me.btnTanggal.Size = New System.Drawing.Size(67, 25)
        Me.btnTanggal.TabIndex = 41
        Me.btnTanggal.Text = "Date"
        Me.btnTanggal.UseVisualStyleBackColor = True
        '
        'tbCari
        '
        Me.tbCari.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCari.Location = New System.Drawing.Point(75, 372)
        Me.tbCari.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbCari.Name = "tbCari"
        Me.tbCari.Size = New System.Drawing.Size(947, 23)
        Me.tbCari.TabIndex = 40
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 375)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(44, 15)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "Search"
        '
        'lstData
        '
        Me.lstData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstData.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.listCol1, Me.listCol2, Me.listCol3, Me.listCol4, Me.listCol5, Me.listCol6})
        Me.lstData.ContextMenuStrip = Me.cmDocs
        Me.lstData.FullRowSelect = True
        Me.lstData.Location = New System.Drawing.Point(3, 403)
        Me.lstData.Name = "lstData"
        Me.lstData.Size = New System.Drawing.Size(1096, 177)
        Me.lstData.TabIndex = 42
        Me.lstData.UseCompatibleStateImageBehavior = False
        Me.lstData.View = System.Windows.Forms.View.Details
        '
        'listCol1
        '
        Me.listCol1.Text = "Document Number"
        Me.listCol1.Width = 160
        '
        'listCol2
        '
        Me.listCol2.Text = "Tanggal"
        Me.listCol2.Width = 100
        '
        'listCol3
        '
        Me.listCol3.Text = "From"
        '
        'listCol4
        '
        Me.listCol4.Text = "To"
        '
        'listCol5
        '
        Me.listCol5.Text = "Operator"
        Me.listCol5.Width = 120
        '
        'listCol6
        '
        Me.listCol6.Text = "In/Out"
        '
        'cmDocs
        '
        Me.cmDocs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmReprint, Me.VoidToolStripMenuItem})
        Me.cmDocs.Name = "ContextMenuStrip1"
        Me.cmDocs.Size = New System.Drawing.Size(113, 48)
        '
        'tmReprint
        '
        Me.tmReprint.Name = "tmReprint"
        Me.tmReprint.Size = New System.Drawing.Size(112, 22)
        Me.tmReprint.Text = "Reprint"
        '
        'VoidToolStripMenuItem
        '
        Me.VoidToolStripMenuItem.Name = "VoidToolStripMenuItem"
        Me.VoidToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.VoidToolStripMenuItem.Text = "Void"
        '
        'dateTick
        '
        Me.dateTick.Enabled = True
        Me.dateTick.Interval = 250
        '
        'lstFind
        '
        Me.lstFind.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.findCol1})
        Me.lstFind.FullRowSelect = True
        Me.lstFind.HideSelection = False
        Me.lstFind.Location = New System.Drawing.Point(174, 378)
        Me.lstFind.MultiSelect = False
        Me.lstFind.Name = "lstFind"
        Me.lstFind.Size = New System.Drawing.Size(136, 156)
        Me.lstFind.TabIndex = 44
        Me.lstFind.UseCompatibleStateImageBehavior = False
        Me.lstFind.View = System.Windows.Forms.View.Details
        Me.lstFind.Visible = False
        '
        'findCol1
        '
        Me.findCol1.Width = 97
        '
        'syncTick
        '
        Me.syncTick.Interval = 60000
        '
        'TransLabeling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lstFind)
        Me.Controls.Add(Me.gbLabel)
        Me.Controls.Add(Me.lstData)
        Me.Controls.Add(Me.btnTanggal)
        Me.Controls.Add(Me.tbCari)
        Me.Controls.Add(Me.Label16)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "TransLabeling"
        Me.Size = New System.Drawing.Size(1103, 584)
        Me.gbLabel.ResumeLayout(False)
        Me.gbLabel.PerformLayout()
        Me.cmDocs.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbLabel As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbItem As System.Windows.Forms.TextBox
    Friend WithEvents tick As System.Windows.Forms.Timer
    Friend WithEvents btnTanggal As System.Windows.Forms.Button
    Friend WithEvents tbCari As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lstData As System.Windows.Forms.ListView
    Friend WithEvents dateTick As System.Windows.Forms.Timer
    Friend WithEvents cbShifts As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents listCol1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listCol2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstFind As System.Windows.Forms.ListView
    Friend WithEvents btnItemFind As System.Windows.Forms.Button
    Friend WithEvents findCol1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listCol3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listCol4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblOperator As System.Windows.Forms.Label
    Friend WithEvents lblStationEX As System.Windows.Forms.Label
    Friend WithEvents cbFromLine As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbToLine As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnPanjang As System.Windows.Forms.Button
    Friend WithEvents btnLebar As System.Windows.Forms.Button
    Friend WithEvents btnAnyaman As System.Windows.Forms.Button
    Friend WithEvents btnDenier As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tbPanjang As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents tbDenier As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents tbLebar As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents tbAnyaman As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbCetakan As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbWarna As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tbJumlah As System.Windows.Forms.TextBox
    Friend WithEvents lstItems As System.Windows.Forms.ListView
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tbFromDocNum As System.Windows.Forms.TextBox
    Friend WithEvents btnDelItem As System.Windows.Forms.Button
    Friend WithEvents btnAddItem As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents rbOut As System.Windows.Forms.RadioButton
    Friend WithEvents rbIn As System.Windows.Forms.RadioButton
    Friend WithEvents listCol5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tbOPP As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listCol6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmDocs As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tmReprint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents syncTick As System.Windows.Forms.Timer
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents cbUOM As System.Windows.Forms.ComboBox
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents VoidToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblNetto As System.Windows.Forms.Label
    Friend WithEvents tbNetto As System.Windows.Forms.TextBox
End Class
