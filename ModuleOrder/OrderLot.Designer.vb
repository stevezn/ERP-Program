<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderLot
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
        Me.lstLot = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dpFilterFrom = New System.Windows.Forms.DateTimePicker()
        Me.dpFilterTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dpLot = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbMaxOrder = New System.Windows.Forms.NumericUpDown()
        Me.lbUOM = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.dpLot2 = New System.Windows.Forms.DateTimePicker()
        CType(Me.tbMaxOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstLot
        '
        Me.lstLot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstLot.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lstLot.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.lstLot.FullRowSelect = True
        Me.lstLot.HideSelection = False
        Me.lstLot.Location = New System.Drawing.Point(3, 3)
        Me.lstLot.Name = "lstLot"
        Me.lstLot.Size = New System.Drawing.Size(1005, 382)
        Me.lstLot.TabIndex = 0
        Me.lstLot.UseCompatibleStateImageBehavior = False
        Me.lstLot.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Date"
        Me.ColumnHeader1.Width = 200
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Maximum Order"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader2.Width = 200
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Existing Order"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader3.Width = 200
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Balance"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader4.Width = 200
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 395)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From"
        '
        'dpFilterFrom
        '
        Me.dpFilterFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dpFilterFrom.CustomFormat = ""
        Me.dpFilterFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpFilterFrom.Location = New System.Drawing.Point(103, 391)
        Me.dpFilterFrom.Name = "dpFilterFrom"
        Me.dpFilterFrom.Size = New System.Drawing.Size(138, 23)
        Me.dpFilterFrom.TabIndex = 2
        '
        'dpFilterTo
        '
        Me.dpFilterTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dpFilterTo.CustomFormat = ""
        Me.dpFilterTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpFilterTo.Location = New System.Drawing.Point(321, 391)
        Me.dpFilterTo.Name = "dpFilterTo"
        Me.dpFilterTo.Size = New System.Drawing.Size(138, 23)
        Me.dpFilterTo.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(296, 395)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(19, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(465, 390)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(138, 25)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dpLot
        '
        Me.dpLot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dpLot.CustomFormat = ""
        Me.dpLot.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpLot.Location = New System.Drawing.Point(103, 443)
        Me.dpLot.Name = "dpLot"
        Me.dpLot.Size = New System.Drawing.Size(138, 23)
        Me.dpLot.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 447)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Lot Dates"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 476)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 15)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Maximum Order"
        '
        'tbMaxOrder
        '
        Me.tbMaxOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbMaxOrder.Location = New System.Drawing.Point(103, 472)
        Me.tbMaxOrder.Name = "tbMaxOrder"
        Me.tbMaxOrder.Size = New System.Drawing.Size(138, 23)
        Me.tbMaxOrder.TabIndex = 9
        Me.tbMaxOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbMaxOrder.ThousandsSeparator = True
        '
        'lbUOM
        '
        Me.lbUOM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbUOM.AutoSize = True
        Me.lbUOM.Location = New System.Drawing.Point(247, 476)
        Me.lbUOM.Name = "lbUOM"
        Me.lbUOM.Size = New System.Drawing.Size(35, 15)
        Me.lbUOM.TabIndex = 10
        Me.lbUOM.Text = "UOM"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(465, 442)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(138, 25)
        Me.btnAdd.TabIndex = 11
        Me.btnAdd.Text = "Add Lots"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Location = New System.Drawing.Point(465, 471)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(138, 25)
        Me.btnUpdate.TabIndex = 12
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'dpLot2
        '
        Me.dpLot2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dpLot2.CustomFormat = ""
        Me.dpLot2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpLot2.Location = New System.Drawing.Point(321, 443)
        Me.dpLot2.Name = "dpLot2"
        Me.dpLot2.Size = New System.Drawing.Size(138, 23)
        Me.dpLot2.TabIndex = 13
        '
        'OrderLot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dpLot2)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lbUOM)
        Me.Controls.Add(Me.tbMaxOrder)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dpLot)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dpFilterTo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dpFilterFrom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstLot)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "OrderLot"
        Me.Size = New System.Drawing.Size(1012, 528)
        CType(Me.tbMaxOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lstLot As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dpFilterFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFilterTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents dpLot As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbMaxOrder As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbUOM As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents dpLot2 As System.Windows.Forms.DateTimePicker
End Class
