Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Periode
    Inherits Form

    'Form overrides dispose to clean up the component list.
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
        Me.dpAwal = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbPeriode = New System.Windows.Forms.RadioButton()
        Me.rb1day = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dpAkhir = New System.Windows.Forms.DateTimePicker()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.cbLine = New System.Windows.Forms.ComboBox()
        Me.lblLine = New System.Windows.Forms.Label()
        Me.lblShift = New System.Windows.Forms.Label()
        Me.cbShift = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'dpAwal
        '
        Me.dpAwal.Location = New System.Drawing.Point(113, 43)
        Me.dpAwal.Name = "dpAwal"
        Me.dpAwal.Size = New System.Drawing.Size(213, 23)
        Me.dpAwal.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Awal"
        '
        'rbPeriode
        '
        Me.rbPeriode.AutoSize = True
        Me.rbPeriode.Checked = True
        Me.rbPeriode.Location = New System.Drawing.Point(12, 14)
        Me.rbPeriode.Name = "rbPeriode"
        Me.rbPeriode.Size = New System.Drawing.Size(67, 19)
        Me.rbPeriode.TabIndex = 2
        Me.rbPeriode.TabStop = True
        Me.rbPeriode.Text = "Periode"
        Me.rbPeriode.UseVisualStyleBackColor = True
        '
        'rb1day
        '
        Me.rb1day.AutoSize = True
        Me.rb1day.Location = New System.Drawing.Point(113, 14)
        Me.rb1day.Name = "rb1day"
        Me.rb1day.Size = New System.Drawing.Size(59, 19)
        Me.rb1day.TabIndex = 3
        Me.rb1day.Text = "1 Hari"
        Me.rb1day.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 15)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Akhir"
        '
        'dpAkhir
        '
        Me.dpAkhir.Location = New System.Drawing.Point(113, 73)
        Me.dpAkhir.Name = "dpAkhir"
        Me.dpAkhir.Size = New System.Drawing.Size(213, 23)
        Me.dpAkhir.TabIndex = 4
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(239, 103)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(87, 27)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'cbLine
        '
        Me.cbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLine.FormattingEnabled = True
        Me.cbLine.Items.AddRange(New Object() {"EXTRUDER", "CIRCULAR LOOM", "CUTTING SEWING", "INSERT INNER", "OPP", "PRINTING", "PACKING", "ROLL TO ROLL", "JAHIT", "CUTTING INNER"})
        Me.cbLine.Location = New System.Drawing.Point(113, 13)
        Me.cbLine.Name = "cbLine"
        Me.cbLine.Size = New System.Drawing.Size(213, 23)
        Me.cbLine.TabIndex = 7
        Me.cbLine.Visible = False
        '
        'lblLine
        '
        Me.lblLine.AutoSize = True
        Me.lblLine.Location = New System.Drawing.Point(9, 16)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(29, 15)
        Me.lblLine.TabIndex = 8
        Me.lblLine.Text = "Line"
        Me.lblLine.Visible = False
        '
        'lblShift
        '
        Me.lblShift.AutoSize = True
        Me.lblShift.Location = New System.Drawing.Point(9, 76)
        Me.lblShift.Name = "lblShift"
        Me.lblShift.Size = New System.Drawing.Size(32, 15)
        Me.lblShift.TabIndex = 10
        Me.lblShift.Text = "Shift"
        Me.lblShift.Visible = False
        '
        'cbShift
        '
        Me.cbShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbShift.FormattingEnabled = True
        Me.cbShift.Location = New System.Drawing.Point(113, 73)
        Me.cbShift.Name = "cbShift"
        Me.cbShift.Size = New System.Drawing.Size(213, 23)
        Me.cbShift.TabIndex = 9
        Me.cbShift.Visible = False
        '
        'Periode
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 141)
        Me.Controls.Add(Me.lblShift)
        Me.Controls.Add(Me.cbShift)
        Me.Controls.Add(Me.lblLine)
        Me.Controls.Add(Me.cbLine)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dpAkhir)
        Me.Controls.Add(Me.rb1day)
        Me.Controls.Add(Me.rbPeriode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dpAwal)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Periode"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Periode"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dpAwal As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents rbPeriode As RadioButton
    Friend WithEvents rb1day As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents dpAkhir As DateTimePicker
    Friend WithEvents btnOK As Button
    Friend WithEvents cbLine As ComboBox
    Friend WithEvents lblLine As Label
    Friend WithEvents lblShift As Label
    Friend WithEvents cbShift As ComboBox
End Class
