<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScaleControl
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
        Me.btnTanggal = New System.Windows.Forms.Button()
        Me.tick = New System.Windows.Forms.Timer(Me.components)
        Me.btnCapture = New System.Windows.Forms.Button()
        Me.lblWeight = New System.Windows.Forms.Label()
        Me.sPort = New System.IO.Ports.SerialPort(Me.components)
        Me.btnKosong = New System.Windows.Forms.Button()
        Me.btnCetak = New System.Windows.Forms.Button()
        Me.btnSimpan = New System.Windows.Forms.Button()
        Me.tbKeterangan = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tbNoContainer = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dpKeluar = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dpMasuk = New System.Windows.Forms.DateTimePicker()
        Me.tbSupir = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbCustomer = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbSupplier = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbBarang = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbNoDo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbNopol = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dpTanggal = New System.Windows.Forms.DateTimePicker()
        Me.tbNoTiket = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbTare = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tbBruto = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblCom = New System.Windows.Forms.Label()
        Me.tbCari = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lstTiket = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tbNetto = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnTanggal
        '
        Me.btnTanggal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTanggal.Location = New System.Drawing.Point(979, 14)
        Me.btnTanggal.Name = "btnTanggal"
        Me.btnTanggal.Size = New System.Drawing.Size(67, 25)
        Me.btnTanggal.TabIndex = 38
        Me.btnTanggal.Text = "Tanggal"
        Me.btnTanggal.UseVisualStyleBackColor = True
        '
        'tick
        '
        '
        'btnCapture
        '
        Me.btnCapture.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCapture.Location = New System.Drawing.Point(472, 11)
        Me.btnCapture.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.btnCapture.Name = "btnCapture"
        Me.btnCapture.Size = New System.Drawing.Size(170, 147)
        Me.btnCapture.TabIndex = 31
        Me.btnCapture.Text = "Catat Berat"
        Me.btnCapture.UseVisualStyleBackColor = True
        '
        'lblWeight
        '
        Me.lblWeight.BackColor = System.Drawing.Color.Black
        Me.lblWeight.Font = New System.Drawing.Font("Tahoma", 60.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeight.ForeColor = System.Drawing.Color.LawnGreen
        Me.lblWeight.Location = New System.Drawing.Point(11, 11)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(455, 147)
        Me.lblWeight.TabIndex = 30
        Me.lblWeight.Text = "0"
        Me.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnKosong
        '
        Me.btnKosong.Location = New System.Drawing.Point(358, 203)
        Me.btnKosong.Name = "btnKosong"
        Me.btnKosong.Size = New System.Drawing.Size(87, 27)
        Me.btnKosong.TabIndex = 28
        Me.btnKosong.Text = "Kosongkan"
        Me.btnKosong.UseVisualStyleBackColor = True
        '
        'btnCetak
        '
        Me.btnCetak.Enabled = False
        Me.btnCetak.Location = New System.Drawing.Point(451, 203)
        Me.btnCetak.Name = "btnCetak"
        Me.btnCetak.Size = New System.Drawing.Size(87, 27)
        Me.btnCetak.TabIndex = 27
        Me.btnCetak.Text = "Cetak"
        Me.btnCetak.UseVisualStyleBackColor = True
        '
        'btnSimpan
        '
        Me.btnSimpan.Location = New System.Drawing.Point(544, 203)
        Me.btnSimpan.Name = "btnSimpan"
        Me.btnSimpan.Size = New System.Drawing.Size(87, 27)
        Me.btnSimpan.TabIndex = 24
        Me.btnSimpan.Text = "Simpan"
        Me.btnSimpan.UseVisualStyleBackColor = True
        '
        'tbKeterangan
        '
        Me.tbKeterangan.Location = New System.Drawing.Point(477, 172)
        Me.tbKeterangan.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbKeterangan.Name = "tbKeterangan"
        Me.tbKeterangan.Size = New System.Drawing.Size(154, 23)
        Me.tbKeterangan.TabIndex = 23
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(331, 175)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 15)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Keterangan"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnKosong)
        Me.GroupBox1.Controls.Add(Me.btnCetak)
        Me.GroupBox1.Controls.Add(Me.btnSimpan)
        Me.GroupBox1.Controls.Add(Me.tbKeterangan)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.tbNoContainer)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.dpKeluar)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.dpMasuk)
        Me.GroupBox1.Controls.Add(Me.tbSupir)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.tbCustomer)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.tbSupplier)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.tbBarang)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.tbNoDo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.tbNopol)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dpTanggal)
        Me.GroupBox1.Controls.Add(Me.tbNoTiket)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 333)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(639, 242)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tiket"
        '
        'tbNoContainer
        '
        Me.tbNoContainer.Location = New System.Drawing.Point(477, 140)
        Me.tbNoContainer.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbNoContainer.Name = "tbNoContainer"
        Me.tbNoContainer.Size = New System.Drawing.Size(154, 23)
        Me.tbNoContainer.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(331, 143)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(101, 15)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "Nomor Container"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 175)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(67, 15)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Jam Keluar"
        '
        'dpKeluar
        '
        Me.dpKeluar.CustomFormat = "HH:mm"
        Me.dpKeluar.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpKeluar.Location = New System.Drawing.Point(156, 172)
        Me.dpKeluar.Name = "dpKeluar"
        Me.dpKeluar.Size = New System.Drawing.Size(154, 23)
        Me.dpKeluar.TabIndex = 18
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 146)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 15)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Jam Masuk"
        '
        'dpMasuk
        '
        Me.dpMasuk.CustomFormat = "HH:mm"
        Me.dpMasuk.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpMasuk.Location = New System.Drawing.Point(156, 140)
        Me.dpMasuk.Name = "dpMasuk"
        Me.dpMasuk.Size = New System.Drawing.Size(154, 23)
        Me.dpMasuk.TabIndex = 16
        '
        'tbSupir
        '
        Me.tbSupir.Location = New System.Drawing.Point(477, 109)
        Me.tbSupir.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbSupir.Name = "tbSupir"
        Me.tbSupir.Size = New System.Drawing.Size(154, 23)
        Me.tbSupir.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(331, 113)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 15)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Nama Supir"
        '
        'tbCustomer
        '
        Me.tbCustomer.Location = New System.Drawing.Point(477, 78)
        Me.tbCustomer.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbCustomer.Name = "tbCustomer"
        Me.tbCustomer.Size = New System.Drawing.Size(154, 23)
        Me.tbCustomer.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(331, 82)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 15)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Nama Customer"
        '
        'tbSupplier
        '
        Me.tbSupplier.Location = New System.Drawing.Point(477, 47)
        Me.tbSupplier.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbSupplier.Name = "tbSupplier"
        Me.tbSupplier.Size = New System.Drawing.Size(154, 23)
        Me.tbSupplier.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(331, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Nama Supplier"
        '
        'tbBarang
        '
        Me.tbBarang.Location = New System.Drawing.Point(477, 16)
        Me.tbBarang.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbBarang.Name = "tbBarang"
        Me.tbBarang.Size = New System.Drawing.Size(154, 23)
        Me.tbBarang.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(331, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Nama Barang"
        '
        'tbNoDo
        '
        Me.tbNoDo.Location = New System.Drawing.Point(156, 109)
        Me.tbNoDo.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbNoDo.Name = "tbNoDo"
        Me.tbNoDo.Size = New System.Drawing.Size(154, 23)
        Me.tbNoDo.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Nomor DO / Sub DO"
        '
        'tbNopol
        '
        Me.tbNopol.Location = New System.Drawing.Point(156, 78)
        Me.tbNopol.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbNopol.Name = "tbNopol"
        Me.tbNopol.Size = New System.Drawing.Size(154, 23)
        Me.tbNopol.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Nomor Polisi"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tanggal"
        '
        'dpTanggal
        '
        Me.dpTanggal.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpTanggal.Location = New System.Drawing.Point(156, 47)
        Me.dpTanggal.Name = "dpTanggal"
        Me.dpTanggal.Size = New System.Drawing.Size(154, 23)
        Me.dpTanggal.TabIndex = 2
        '
        'tbNoTiket
        '
        Me.tbNoTiket.Location = New System.Drawing.Point(156, 16)
        Me.tbNoTiket.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbNoTiket.Name = "tbNoTiket"
        Me.tbNoTiket.Size = New System.Drawing.Size(154, 23)
        Me.tbNoTiket.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nomor"
        '
        'tbTare
        '
        Me.tbTare.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTare.Location = New System.Drawing.Point(156, 63)
        Me.tbTare.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbTare.Name = "tbTare"
        Me.tbTare.ReadOnly = True
        Me.tbTare.Size = New System.Drawing.Size(475, 36)
        Me.tbTare.TabIndex = 17
        Me.tbTare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(6, 67)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(62, 29)
        Me.Label14.TabIndex = 16
        Me.Label14.Text = "Tare"
        '
        'tbBruto
        '
        Me.tbBruto.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBruto.Location = New System.Drawing.Point(156, 17)
        Me.tbBruto.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbBruto.Name = "tbBruto"
        Me.tbBruto.ReadOnly = True
        Me.tbBruto.Size = New System.Drawing.Size(475, 36)
        Me.tbBruto.TabIndex = 15
        Me.tbBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(5, 21)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 29)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Bruto"
        '
        'lblCom
        '
        Me.lblCom.BackColor = System.Drawing.Color.Black
        Me.lblCom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblCom.ForeColor = System.Drawing.Color.LawnGreen
        Me.lblCom.Location = New System.Drawing.Point(23, 132)
        Me.lblCom.Name = "lblCom"
        Me.lblCom.Size = New System.Drawing.Size(100, 15)
        Me.lblCom.TabIndex = 37
        '
        'tbCari
        '
        Me.tbCari.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCari.Location = New System.Drawing.Point(725, 15)
        Me.tbCari.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbCari.Name = "tbCari"
        Me.tbCari.Size = New System.Drawing.Size(248, 23)
        Me.tbCari.TabIndex = 36
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(656, 18)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 15)
        Me.Label16.TabIndex = 35
        Me.Label16.Text = "Pencarian"
        '
        'lstTiket
        '
        Me.lstTiket.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstTiket.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9})
        Me.lstTiket.FullRowSelect = True
        Me.lstTiket.Location = New System.Drawing.Point(659, 48)
        Me.lstTiket.Name = "lstTiket"
        Me.lstTiket.Size = New System.Drawing.Size(387, 527)
        Me.lstTiket.TabIndex = 34
        Me.lstTiket.UseCompatibleStateImageBehavior = False
        Me.lstTiket.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "No. Tiket"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Tanggal"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "No. Polisi"
        Me.ColumnHeader3.Width = 100
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Nama Barang"
        Me.ColumnHeader4.Width = 100
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Supplier"
        Me.ColumnHeader5.Width = 100
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Customer"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Bruto"
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Tare"
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Netto"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(4, 117)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(71, 29)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "Netto"
        '
        'tbNetto
        '
        Me.tbNetto.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNetto.Location = New System.Drawing.Point(156, 109)
        Me.tbNetto.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.tbNetto.Name = "tbNetto"
        Me.tbNetto.ReadOnly = True
        Me.tbNetto.Size = New System.Drawing.Size(475, 36)
        Me.tbNetto.TabIndex = 19
        Me.tbNetto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.tbNetto)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.tbTare)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.tbBruto)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 166)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(639, 159)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Berat"
        '
        'ScaleControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnCapture)
        Me.Controls.Add(Me.btnTanggal)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblCom)
        Me.Controls.Add(Me.tbCari)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lstTiket)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblWeight)
        Me.Font = New System.Drawing.Font("Calibri", 9.5!)
        Me.Name = "ScaleControl"
        Me.Size = New System.Drawing.Size(1056, 586)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnTanggal As System.Windows.Forms.Button
    Friend WithEvents tick As System.Windows.Forms.Timer
    Friend WithEvents btnCapture As System.Windows.Forms.Button
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Public WithEvents sPort As IO.Ports.SerialPort
    Friend WithEvents btnKosong As System.Windows.Forms.Button
    Friend WithEvents btnCetak As System.Windows.Forms.Button
    Friend WithEvents btnSimpan As System.Windows.Forms.Button
    Friend WithEvents tbKeterangan As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tbNoContainer As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dpKeluar As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dpMasuk As System.Windows.Forms.DateTimePicker
    Friend WithEvents tbSupir As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbCustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbSupplier As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbBarang As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbNoDo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbNopol As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dpTanggal As System.Windows.Forms.DateTimePicker
    Friend WithEvents tbNoTiket As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbTare As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tbBruto As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblCom As System.Windows.Forms.Label
    Friend WithEvents tbCari As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lstTiket As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tbNetto As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
