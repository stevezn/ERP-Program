<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PurchaseRequest
    Inherits System.Windows.Forms.UserControl

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkMonthFilter = New System.Windows.Forms.CheckBox()
        Me.dpDateFilter = New System.Windows.Forms.DateTimePicker()
        Me.lbSearch = New System.Windows.Forms.Label()
        Me.tbSearchPO = New System.Windows.Forms.TextBox()
        Me.lstSO = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
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
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader37 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbItemSearch = New System.Windows.Forms.TextBox()
        Me.lbItem = New System.Windows.Forms.Label()
        Me.lbQuantity = New System.Windows.Forms.Label()
        Me.tbQuantity = New System.Windows.Forms.NumericUpDown()
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.btnDelItem = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.cbSOType = New System.Windows.Forms.ComboBox()
        Me.lbExpectDate = New System.Windows.Forms.Label()
        Me.dpExpect = New System.Windows.Forms.DateTimePicker()
        Me.lbPRDate = New System.Windows.Forms.Label()
        Me.dpSO = New System.Windows.Forms.DateTimePicker()
        Me.tbSONum = New System.Windows.Forms.TextBox()
        Me.lbPRNum = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.tcItems.SuspendLayout()
        Me.tpItem.SuspendLayout()
        CType(Me.tbQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cbSOType)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbExpectDate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dpExpect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbPRDate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dpSO)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbSONum)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbPRNum)
        Me.SplitContainer1.Size = New System.Drawing.Size(1109, 651)
        Me.SplitContainer1.SplitterDistance = 222
        Me.SplitContainer1.TabIndex = 2
        '
        'chkMonthFilter
        '
        Me.chkMonthFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkMonthFilter.AutoSize = True
        Me.chkMonthFilter.Checked = True
        Me.chkMonthFilter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMonthFilter.Location = New System.Drawing.Point(903, 5)
        Me.chkMonthFilter.Name = "chkMonthFilter"
        Me.chkMonthFilter.Size = New System.Drawing.Size(56, 17)
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
        Me.dpDateFilter.Location = New System.Drawing.Point(965, 3)
        Me.dpDateFilter.Name = "dpDateFilter"
        Me.dpDateFilter.ShowUpDown = True
        Me.dpDateFilter.Size = New System.Drawing.Size(141, 20)
        Me.dpDateFilter.TabIndex = 2
        '
        'lbSearch
        '
        Me.lbSearch.AutoSize = True
        Me.lbSearch.Location = New System.Drawing.Point(0, 6)
        Me.lbSearch.Name = "lbSearch"
        Me.lbSearch.Size = New System.Drawing.Size(41, 13)
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
        Me.tbSearchPO.Size = New System.Drawing.Size(827, 20)
        Me.tbSearchPO.TabIndex = 0
        '
        'lstSO
        '
        Me.lstSO.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstSO.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader20, Me.ColumnHeader21, Me.ColumnHeader22, Me.ColumnHeader23, Me.ColumnHeader24, Me.ColumnHeader25, Me.ColumnHeader26})
        Me.lstSO.FullRowSelect = True
        Me.lstSO.HideSelection = False
        Me.lstSO.Location = New System.Drawing.Point(3, 32)
        Me.lstSO.MultiSelect = False
        Me.lstSO.Name = "lstSO"
        Me.lstSO.Size = New System.Drawing.Size(1103, 187)
        Me.lstSO.TabIndex = 3
        Me.lstSO.UseCompatibleStateImageBehavior = False
        Me.lstSO.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "PR Number"
        Me.ColumnHeader5.Width = 120
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Date"
        Me.ColumnHeader6.Width = 100
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
        Me.tcItems.Size = New System.Drawing.Size(1100, 261)
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
        Me.tpItem.Location = New System.Drawing.Point(4, 22)
        Me.tpItem.Name = "tpItem"
        Me.tpItem.Padding = New System.Windows.Forms.Padding(3)
        Me.tpItem.Size = New System.Drawing.Size(1092, 235)
        Me.tpItem.TabIndex = 0
        Me.tpItem.Text = "Items"
        Me.tpItem.UseVisualStyleBackColor = True
        '
        'tbRemark
        '
        Me.tbRemark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbRemark.ForeColor = System.Drawing.Color.Red
        Me.tbRemark.Location = New System.Drawing.Point(758, 178)
        Me.tbRemark.Multiline = True
        Me.tbRemark.Name = "tbRemark"
        Me.tbRemark.ReadOnly = True
        Me.tbRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbRemark.Size = New System.Drawing.Size(331, 51)
        Me.tbRemark.TabIndex = 30
        '
        'lstSOItem
        '
        Me.lstSOItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstSOItem.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader10, Me.ColumnHeader1, Me.ColumnHeader3, Me.ColumnHeader9, Me.ColumnHeader37})
        Me.lstSOItem.FullRowSelect = True
        Me.lstSOItem.HideSelection = False
        Me.lstSOItem.Location = New System.Drawing.Point(3, 3)
        Me.lstSOItem.MultiSelect = False
        Me.lstSOItem.Name = "lstSOItem"
        Me.lstSOItem.Size = New System.Drawing.Size(1086, 169)
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
        'ColumnHeader37
        '
        Me.ColumnHeader37.Text = "Note"
        Me.ColumnHeader37.Width = 200
        '
        'tbItemSearch
        '
        Me.tbItemSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbItemSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbItemSearch.Location = New System.Drawing.Point(63, 178)
        Me.tbItemSearch.Name = "tbItemSearch"
        Me.tbItemSearch.Size = New System.Drawing.Size(215, 20)
        Me.tbItemSearch.TabIndex = 0
        '
        'lbItem
        '
        Me.lbItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbItem.AutoSize = True
        Me.lbItem.Location = New System.Drawing.Point(0, 181)
        Me.lbItem.Name = "lbItem"
        Me.lbItem.Size = New System.Drawing.Size(27, 13)
        Me.lbItem.TabIndex = 20
        Me.lbItem.Text = "Item"
        '
        'lbQuantity
        '
        Me.lbQuantity.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbQuantity.AutoSize = True
        Me.lbQuantity.Location = New System.Drawing.Point(0, 209)
        Me.lbQuantity.Name = "lbQuantity"
        Me.lbQuantity.Size = New System.Drawing.Size(46, 13)
        Me.lbQuantity.TabIndex = 22
        Me.lbQuantity.Text = "Quantity"
        '
        'tbQuantity
        '
        Me.tbQuantity.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbQuantity.Location = New System.Drawing.Point(63, 207)
        Me.tbQuantity.Name = "tbQuantity"
        Me.tbQuantity.Size = New System.Drawing.Size(105, 20)
        Me.tbQuantity.TabIndex = 1
        Me.tbQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbQuantity.ThousandsSeparator = True
        '
        'btnAddItem
        '
        Me.btnAddItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddItem.Location = New System.Drawing.Point(284, 177)
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Size = New System.Drawing.Size(102, 25)
        Me.btnAddItem.TabIndex = 2
        Me.btnAddItem.Text = "Add Item"
        Me.btnAddItem.UseVisualStyleBackColor = True
        '
        'btnDelItem
        '
        Me.btnDelItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelItem.Location = New System.Drawing.Point(284, 205)
        Me.btnDelItem.Name = "btnDelItem"
        Me.btnDelItem.Size = New System.Drawing.Size(102, 25)
        Me.btnDelItem.TabIndex = 26
        Me.btnDelItem.Text = "Delete Item"
        Me.btnDelItem.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.Tomato
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte), True)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(1002, 61)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 52)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'cbSOType
        '
        Me.cbSOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSOType.FormattingEnabled = True
        Me.cbSOType.Location = New System.Drawing.Point(92, 3)
        Me.cbSOType.Name = "cbSOType"
        Me.cbSOType.Size = New System.Drawing.Size(74, 21)
        Me.cbSOType.TabIndex = 0
        '
        'lbExpectDate
        '
        Me.lbExpectDate.AutoSize = True
        Me.lbExpectDate.Location = New System.Drawing.Point(4, 65)
        Me.lbExpectDate.Name = "lbExpectDate"
        Me.lbExpectDate.Size = New System.Drawing.Size(59, 13)
        Me.lbExpectDate.TabIndex = 17
        Me.lbExpectDate.Text = "E.T. Arrival"
        '
        'dpExpect
        '
        Me.dpExpect.Location = New System.Drawing.Point(92, 61)
        Me.dpExpect.Name = "dpExpect"
        Me.dpExpect.Size = New System.Drawing.Size(196, 20)
        Me.dpExpect.TabIndex = 8
        '
        'lbPRDate
        '
        Me.lbPRDate.AutoSize = True
        Me.lbPRDate.Location = New System.Drawing.Point(4, 35)
        Me.lbPRDate.Name = "lbPRDate"
        Me.lbPRDate.Size = New System.Drawing.Size(48, 13)
        Me.lbPRDate.TabIndex = 7
        Me.lbPRDate.Text = "PR Date"
        '
        'dpSO
        '
        Me.dpSO.Location = New System.Drawing.Point(92, 32)
        Me.dpSO.Name = "dpSO"
        Me.dpSO.Size = New System.Drawing.Size(196, 20)
        Me.dpSO.TabIndex = 3
        '
        'tbSONum
        '
        Me.tbSONum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbSONum.Location = New System.Drawing.Point(172, 3)
        Me.tbSONum.Name = "tbSONum"
        Me.tbSONum.ReadOnly = True
        Me.tbSONum.Size = New System.Drawing.Size(116, 20)
        Me.tbSONum.TabIndex = 1
        Me.tbSONum.Text = "NEW"
        '
        'lbPRNum
        '
        Me.lbPRNum.AutoSize = True
        Me.lbPRNum.Location = New System.Drawing.Point(4, 6)
        Me.lbPRNum.Name = "lbPRNum"
        Me.lbPRNum.Size = New System.Drawing.Size(62, 13)
        Me.lbPRNum.TabIndex = 0
        Me.lbPRNum.Text = "PR Number"
        '
        'PurchaseRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "PurchaseRequest"
        Me.Size = New System.Drawing.Size(1109, 651)
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
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader21 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader22 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader23 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader24 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader25 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader26 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tcItems As System.Windows.Forms.TabControl
    Friend WithEvents tpItem As System.Windows.Forms.TabPage
    Friend WithEvents tbRemark As System.Windows.Forms.TextBox
    Friend WithEvents lstSOItem As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader37 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbItemSearch As System.Windows.Forms.TextBox
    Friend WithEvents lbItem As System.Windows.Forms.Label
    Friend WithEvents lbQuantity As System.Windows.Forms.Label
    Friend WithEvents tbQuantity As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnAddItem As System.Windows.Forms.Button
    Friend WithEvents btnDelItem As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cbSOType As System.Windows.Forms.ComboBox
    Friend WithEvents lbExpectDate As System.Windows.Forms.Label
    Friend WithEvents dpExpect As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbPRDate As System.Windows.Forms.Label
    Friend WithEvents dpSO As System.Windows.Forms.DateTimePicker
    Friend WithEvents tbSONum As System.Windows.Forms.TextBox
    Friend WithEvents lbPRNum As System.Windows.Forms.Label
End Class
