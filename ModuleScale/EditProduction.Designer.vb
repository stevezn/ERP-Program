<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditProduction
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
        Me.propGrid = New System.Windows.Forms.PropertyGrid()
        Me.SuspendLayout()
        '
        'propGrid
        '
        Me.propGrid.Location = New System.Drawing.Point(14, 14)
        Me.propGrid.Name = "propGrid"
        Me.propGrid.Size = New System.Drawing.Size(590, 318)
        Me.propGrid.TabIndex = 0
        '
        'EditProduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(617, 387)
        Me.Controls.Add(Me.propGrid)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "EditProduction"
        Me.Text = "EditProduction"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents propGrid As System.Windows.Forms.PropertyGrid
End Class
