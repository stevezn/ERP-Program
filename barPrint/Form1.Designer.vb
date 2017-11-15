<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

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
        Me.txtTemplate = New System.Windows.Forms.TextBox()
        Me.txtA = New System.Windows.Forms.TextBox()
        Me.txtB = New System.Windows.Forms.TextBox()
        Me.txtC = New System.Windows.Forms.TextBox()
        Me.txtD = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.labelCount = New System.Windows.Forms.NumericUpDown()
        Me.startA = New System.Windows.Forms.NumericUpDown()
        Me.startB = New System.Windows.Forms.NumericUpDown()
        Me.startC = New System.Windows.Forms.NumericUpDown()
        Me.startD = New System.Windows.Forms.NumericUpDown()
        Me.stepD = New System.Windows.Forms.NumericUpDown()
        Me.stepC = New System.Windows.Forms.NumericUpDown()
        Me.stepB = New System.Windows.Forms.NumericUpDown()
        Me.stepA = New System.Windows.Forms.NumericUpDown()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.numDigit = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.labelCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.startA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.startB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.startC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.startD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stepD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stepC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stepB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stepA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numDigit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTemplate
        '
        Me.txtTemplate.Location = New System.Drawing.Point(12, 12)
        Me.txtTemplate.Multiline = True
        Me.txtTemplate.Name = "txtTemplate"
        Me.txtTemplate.Size = New System.Drawing.Size(479, 515)
        Me.txtTemplate.TabIndex = 0
        '
        'txtA
        '
        Me.txtA.Location = New System.Drawing.Point(527, 12)
        Me.txtA.Name = "txtA"
        Me.txtA.Size = New System.Drawing.Size(74, 20)
        Me.txtA.TabIndex = 1
        '
        'txtB
        '
        Me.txtB.Location = New System.Drawing.Point(527, 61)
        Me.txtB.Name = "txtB"
        Me.txtB.Size = New System.Drawing.Size(74, 20)
        Me.txtB.TabIndex = 2
        '
        'txtC
        '
        Me.txtC.Location = New System.Drawing.Point(527, 110)
        Me.txtC.Name = "txtC"
        Me.txtC.Size = New System.Drawing.Size(74, 20)
        Me.txtC.TabIndex = 3
        '
        'txtD
        '
        Me.txtD.Location = New System.Drawing.Point(527, 159)
        Me.txtD.Name = "txtD"
        Me.txtD.Size = New System.Drawing.Size(74, 20)
        Me.txtD.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(506, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "A"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(506, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "B"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(506, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "C"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(506, 162)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(15, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "D"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(506, 258)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Jumlah Label"
        '
        'labelCount
        '
        Me.labelCount.Location = New System.Drawing.Point(581, 256)
        Me.labelCount.Name = "labelCount"
        Me.labelCount.Size = New System.Drawing.Size(53, 20)
        Me.labelCount.TabIndex = 10
        Me.labelCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'startA
        '
        Me.startA.Location = New System.Drawing.Point(631, 12)
        Me.startA.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.startA.Name = "startA"
        Me.startA.Size = New System.Drawing.Size(53, 20)
        Me.startA.TabIndex = 11
        Me.startA.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'startB
        '
        Me.startB.Location = New System.Drawing.Point(631, 61)
        Me.startB.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.startB.Name = "startB"
        Me.startB.Size = New System.Drawing.Size(53, 20)
        Me.startB.TabIndex = 12
        Me.startB.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'startC
        '
        Me.startC.Location = New System.Drawing.Point(631, 106)
        Me.startC.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.startC.Name = "startC"
        Me.startC.Size = New System.Drawing.Size(53, 20)
        Me.startC.TabIndex = 13
        Me.startC.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'startD
        '
        Me.startD.Location = New System.Drawing.Point(631, 159)
        Me.startD.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.startD.Name = "startD"
        Me.startD.Size = New System.Drawing.Size(53, 20)
        Me.startD.TabIndex = 14
        Me.startD.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'stepD
        '
        Me.stepD.Location = New System.Drawing.Point(700, 159)
        Me.stepD.Name = "stepD"
        Me.stepD.Size = New System.Drawing.Size(53, 20)
        Me.stepD.TabIndex = 18
        Me.stepD.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'stepC
        '
        Me.stepC.Location = New System.Drawing.Point(700, 106)
        Me.stepC.Name = "stepC"
        Me.stepC.Size = New System.Drawing.Size(53, 20)
        Me.stepC.TabIndex = 17
        Me.stepC.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'stepB
        '
        Me.stepB.AccessibleDescription = ""
        Me.stepB.Location = New System.Drawing.Point(700, 61)
        Me.stepB.Name = "stepB"
        Me.stepB.Size = New System.Drawing.Size(53, 20)
        Me.stepB.TabIndex = 16
        Me.stepB.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'stepA
        '
        Me.stepA.Location = New System.Drawing.Point(700, 12)
        Me.stepA.Name = "stepA"
        Me.stepA.Size = New System.Drawing.Size(53, 20)
        Me.stepA.TabIndex = 15
        Me.stepA.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(654, 253)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(99, 23)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = "Print"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'numDigit
        '
        Me.numDigit.Location = New System.Drawing.Point(581, 230)
        Me.numDigit.Name = "numDigit"
        Me.numDigit.Size = New System.Drawing.Size(53, 20)
        Me.numDigit.TabIndex = 21
        Me.numDigit.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(506, 232)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Jumlah Digit"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 539)
        Me.Controls.Add(Me.numDigit)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.stepD)
        Me.Controls.Add(Me.stepC)
        Me.Controls.Add(Me.stepB)
        Me.Controls.Add(Me.stepA)
        Me.Controls.Add(Me.startD)
        Me.Controls.Add(Me.startC)
        Me.Controls.Add(Me.startB)
        Me.Controls.Add(Me.startA)
        Me.Controls.Add(Me.labelCount)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtD)
        Me.Controls.Add(Me.txtC)
        Me.Controls.Add(Me.txtB)
        Me.Controls.Add(Me.txtA)
        Me.Controls.Add(Me.txtTemplate)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.labelCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.startA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.startB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.startC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.startD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stepD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stepC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stepB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stepA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numDigit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtTemplate As TextBox
    Friend WithEvents txtA As TextBox
    Friend WithEvents txtB As TextBox
    Friend WithEvents txtC As TextBox
    Friend WithEvents txtD As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents labelCount As NumericUpDown
    Friend WithEvents startA As NumericUpDown
    Friend WithEvents startB As NumericUpDown
    Friend WithEvents startC As NumericUpDown
    Friend WithEvents startD As NumericUpDown
    Friend WithEvents stepD As NumericUpDown
    Friend WithEvents stepC As NumericUpDown
    Friend WithEvents stepB As NumericUpDown
    Friend WithEvents stepA As NumericUpDown
    Friend WithEvents Button1 As Button
    Friend WithEvents numDigit As NumericUpDown
    Friend WithEvents Label6 As Label
End Class
