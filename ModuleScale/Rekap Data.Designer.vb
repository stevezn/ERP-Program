<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rekap_Data
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
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.listcol1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listcol14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.listcol1, Me.listcol2, Me.listcol3, Me.listcol4, Me.listcol5, Me.listcol6, Me.listcol7, Me.listcol8, Me.listcol9, Me.listcol10, Me.listcol11, Me.listcol12, Me.listcol13, Me.listcol14})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(1428, 261)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'listcol1
        '
        Me.listcol1.Text = "No. Label Insert Inner"
        Me.listcol1.Width = 150
        '
        'listcol2
        '
        Me.listcol2.Text = "Nama Operator"
        Me.listcol2.Width = 112
        '
        'listcol3
        '
        Me.listcol3.Text = "Nama Customer"
        Me.listcol3.Width = 105
        '
        'listcol4
        '
        Me.listcol4.Text = "Cetakan"
        '
        'listcol5
        '
        Me.listcol5.Text = "Ukuran Karung"
        Me.listcol5.Width = 96
        '
        'listcol6
        '
        Me.listcol6.Text = "Ukuran Inner"
        Me.listcol6.Width = 88
        '
        'listcol7
        '
        Me.listcol7.Text = "Jumlah ( Lbr )"
        Me.listcol7.Width = 88
        '
        'listcol8
        '
        Me.listcol8.Text = "Berat Netto / Ball"
        Me.listcol8.Width = 98
        '
        'listcol9
        '
        Me.listcol9.Text = "Std Berat Karung / Lembar ( gr )"
        Me.listcol9.Width = 172
        '
        'listcol10
        '
        Me.listcol10.Text = "Std Berat Inner / Lembar"
        Me.listcol10.Width = 131
        '
        'listcol11
        '
        Me.listcol11.Text = "Aktual Berat / Lembar ( gr )"
        Me.listcol11.Width = 147
        '
        'listcol12
        '
        Me.listcol12.Text = "Selisih Berat ( Gr )"
        Me.listcol12.Width = 98
        '
        'listcol13
        '
        Me.listcol13.Text = "Selisih Berat ( % 0"
        Me.listcol13.Width = 84
        '
        'listcol14
        '
        Me.listcol14.Text = "Status"
        '
        'Rekap_Data
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1428, 261)
        Me.Controls.Add(Me.ListView1)
        Me.Name = "Rekap_Data"
        Me.Text = "Rekap_Data"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents listcol1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listcol14 As System.Windows.Forms.ColumnHeader
End Class
