Imports DB
Imports ERPModules
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports System.Threading
'Imports System.Globalization

Public Class ItemControl : Inherits ERPModule
    Public Class loadThread
        Public dbcon As DBConn
        Public mainThd As Object
        Public mainForm As Object

        Private thdIdx As Integer

        Private Delegate Sub progBarNotify(max As Integer, prog As Integer, txt As String)
        Private Delegate Sub progBarNotify2()

        Private modNotifyEnd As progBarNotify2

        Private pNotify As progBarNotify

        Public Sub New(ByRef MainWindow As Object, ByRef main As Object, ByRef db As DBConn)
            mainForm = MainWindow
            dbcon = db
            mainThd = main

            modNotifyEnd = AddressOf mainThd.progBarNotify2End

            pNotify = AddressOf MainWindow.progBarNotify
        End Sub

        Public Sub loadItems()
            If dbcon.state() = ConnectionState.Open Then
                Dim ctr As Integer = 0
                Dim rdr As Object

                Dim cmdRdr = dbcon.SQLReader("select * from view_items
                        where company_code = @cc")
                dbcon.addParameter("@cc", mainThd.corp, cmdRdr)

                rdr = dbcon.beginRead(cmdRdr)
                mainThd.itemBuffer.Clear()
                mainThd.itemSearch.Clear()

                Dim args(2) As Object
                args(0) = -1
                args(1) = 0
                args(2) = "Loading Items"
                mainForm.Invoke(pNotify, args)

                While dbcon.doRead(rdr)
                    mainThd.itemBuffer.Add(dbcon.getRow(rdr))
                    If ctr = 100 Then
                        args(1) = args(1) + 1
                        mainForm.Invoke(pNotify, args)
                        ctr = 0
                    End If
                    ctr += 1
                End While

                dbcon.endRead(rdr)

                args(1) = args(0)
                mainForm.Invoke(pNotify, args)

                mainThd.itemSearch.AddRange(mainThd.itemBuffer)
                mainThd.Invoke(modNotifyEnd)
            Else
                MsgBox("Connection error, please check your network connection and try again")
            End If
        End Sub
    End Class

    Public Sub progBarNotify2End()
        itemList.VirtualListSize = itemSearch.Count()
        itemList.SelectedIndices.Clear()
    End Sub

    Private delItemCust As New List(Of String)
    Private delItemVend As New List(Of String)

    Public itemBuffer As New List(Of Dictionary(Of String, Object))
    Public itemSearch As New List(Of Dictionary(Of String, Object))
    Private itemSortColumn As Integer = 0
    Private itemOrderMap As New Dictionary(Of Integer, SortOrder) _
        From {{0, SortOrder.None},
              {1, SortOrder.None},
              {2, SortOrder.None},
              {3, SortOrder.None},
              {4, SortOrder.None}}
    Private itemComparers As New Dictionary(Of Integer, Comparison(Of Dictionary(Of String, Object))) _
        From {{0, Function(a, b)
                      If a.ContainsKey("item_name") And b.ContainsKey("item_name") Then
                          Return a("item_name").CompareTo(b("item_name"))
                      Else
                          Return 0
                      End If
                  End Function},
              {1, Function(a, b)
                      If a.ContainsKey("item_code") And b.ContainsKey("item_code") Then
                          Return a("item_code").CompareTo(b("item_code"))
                      Else
                          Return 0
                      End If
                  End Function},
              {2, Function(a, b)
                      If a.ContainsKey("category") And b.ContainsKey("category") Then
                          Return a("category").CompareTo(b("category"))
                      Else
                          Return 0
                      End If
                  End Function},
              {3, Function(a, b)
                      If a.ContainsKey("quantity") And b.ContainsKey("quantity") Then
                          Return a("quantity").CompareTo(b("quantity"))
                      Else
                          Return 0
                      End If
                  End Function},
              {4, Function(a, b)
                      If a.ContainsKey("weight") And b.ContainsKey("weight") Then
                          Return a("weight").CompareTo(b("weight"))
                      Else
                          Return 0
                      End If
                  End Function}}
    Private itemToggle As New Dictionary(Of SortOrder, SortOrder) _
        From {{SortOrder.None, SortOrder.Ascending},
              {SortOrder.Ascending, SortOrder.Descending},
              {SortOrder.Descending, SortOrder.Ascending}}

    Public Overrides Sub Print()
        MsgBox("print item")
    End Sub

    Public Overrides Sub Save()
        saveItem(updateData)
        saveVendors()
        saveCusts()
        'tcMain.SelectedIndex = 0
        asyncLoadItems()
    End Sub

    Public Overloads Sub Dispose()
        Try
            'json = Nothing
            toolMenu.Clear()
            itemBuffer.Clear()
            itemSearch.Clear()

            itemBuffer = Nothing
            itemSearch = Nothing

            MyBase.Dispose()
        Catch
        End Try
    End Sub

    Public Sub NewForm()
        OpenForm()
        clearForm()
        _parent.refreshMenuState(toolButton)
    End Sub

    Public Sub EditForm()
        Dim item As ListViewItem
        Dim row As Dictionary(Of String, Object)

        If itemList.SelectedIndices.Count = 1 Then
            updateData = True

            item = itemList.Items(itemList.SelectedIndices(0))
            row = itemSearch(item.Index)

            tbName.Text = row("item_name")
            tbCode.Text = row("item_code")
            cbCategory.SelectedValue = row("category_code")

            If cbSubCategory.Items.Count = 0 Then
                loadSubCategories(cbCategory.SelectedValue, False)
            End If

            cbSubCategory.SelectedValue = row("subcat_code")
            'cbType.SelectedValue = row("type_code")
            tbQuantity.Text = String.Format(numformat, row("quantity"))
            cbUOM.SelectedValue = row("uom_code")

            chkIsSack.Checked = row("is_sack")

            lbMaxUom.Text = row("uom")
            lbMinUom.Text = row("uom")

            tbMinStock.Text = String.Format(numformat, row("min_stock"))
            tbMaxStock.Text = String.Format(numformat, row("max_stock"))
            tbUnitWeight.Text = String.Format(numformat, row("unit_weight"))

            lbQtyUom.Text = row("uom")

            tbWeight.Text = String.Format(numformat, row("weight"))
            tbCOG.Text = String.Format(numformat, row("cog"))
            If row("usable") Then
                chkUse.Checked = True
            Else
                chkUse.Checked = False
            End If

            If row("consumable") Then
                chkConsume.Checked = True
            Else
                chkConsume.Checked = False
            End If

            If row("producible") Then
                chkMake.Checked = True
            Else
                chkMake.Checked = False
            End If

            If row("buyable") Then
                chkBuy.Checked = True
            Else
                chkBuy.Checked = False
            End If

            If row("sellable") Then
                chkSell.Checked = True
            Else
                chkSell.Checked = False
            End If

            tbCapacity.Text = String.Format(numformat, row("capacity"))
            tbColor.Text = row("color")

            cbWebbing.SelectedValue = row("webbing")
            cbSackType.SelectedValue = row("sack_type")
            cbDenier.SelectedValue = row("denier")

            tbWidth.Text = String.Format(numformat, row("width"))
            tbLength.Text = String.Format(numformat, row("length"))
            tbSackWeight.Text = String.Format(numformat, row("sackweight"))

            'chkHasInner.Checked = row("has_inner")

            'cbInner.SelectedValue = row("inner_code")

            'chkHasInner.Checked = row("has_handle")
            cbHandle.SelectedValue = row("handle_code")

            If row("picture") = 0 Then
                pict.Image = Nothing
            Else
                If File.Exists("images\" + row("item_code")) Then
                    File.Delete("images\" + row("item_code"))
                End If
                If dbCon.state() = ConnectionState.Open Then
                    Dim r As Dictionary(Of String, Object)
                    Dim rdr As Object

                    Dim cmdRdr = dbCon.SQLReader("select picture from items where item_code = @ic")
                    dbCon.addParameter("@ic", row("item_code"), cmdRdr)

                    rdr = dbCon.beginRead(cmdRdr)
                    dbCon.doRead(rdr)
                    r = dbCon.getRow(rdr)
                    dbCon.endRead(rdr)

                    File.WriteAllBytes("images\" + row("item_code"), r("picture"))
                    pict.ImageLocation = "images\" + row("item_code")
                End If
            End If

            loadVendors(row("item_code"))
            loadCusts(row("item_code"))

            'tcDetail.SelectedIndex = 0
            'tcMain.SelectedIndex = 1
            OpenForm()
            _parent.refreshMenuState(toolButton)
        Else
            MsgBox("Please select the item to modify")
        End If
    End Sub

    Private Sub asyncLoadItems()
        Dim loadThd As New loadThread(_parent, Me, dbCon)
        Dim thd As New Thread(AddressOf loadThd.loadItems)
        thd.IsBackground = True
        thd.Start()
    End Sub

    'Private Sub loadItems()
    '    If dbCon.state() = ConnectionState.Open Then
    '        Dim ctr As Integer = 0
    '        dbCon.SQL("select * from view_items
    '                    where company_code = @cc")
    '        dbCon.addParameter("@cc", corp)

    '        dbCon.beginRead()
    '        itemBuffer.Clear()
    '        itemSearch.Clear()

    '        _parent.spbUpdate.Visible = True
    '        _parent.spbUpdate.Value = 0
    '        _parent.spbUpdate.Style = ProgressBarStyle.Marquee
    '        _parent.spbUpdate.Text = "Loading Items"

    '        While dbCon.doRead()
    '            itemBuffer.Add(dbCon.getRow)
    '            If ctr = 25 Then
    '                _parent.spbUpdate.Value = (_parent.spbUpdate.Value + 1) Mod 100
    '                ctr = 0
    '            End If
    '            ctr += 1
    '        End While

    '        dbCon.endRead()

    '        _parent.spbUpdate.Text = "Update Available"
    '        _parent.spbUpdate.Style = ProgressBarStyle.Continuous
    '        _parent.spbUpdate.Visible = False

    '        itemSearch.AddRange(itemBuffer)
    '        itemList.VirtualListSize = itemSearch.Count()
    '        itemList.SelectedIndices.Clear()
    '    Else
    '        MsgBox("Connection error, please check your network connection and try again")
    '    End If
    'End Sub

    Private Sub loadCategories()
        Dim dt As New DataTable

        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select category, category_code from item_categories where company_code = @cc and (parent_code is null or parent_code = @pc)")
            dbCon.addParameter("@cc", corp)
            dbCon.addParameter("@pc", "")
            dbCon.fillTable(dt)

            cbCategory.DataSource = New BindingSource(dt, Nothing)
            cbCategory.DisplayMember = "category"
            cbCategory.ValueMember = "category_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadSubCategories(parent As String, withall As Boolean)
        Dim dt As New DataTable

        If dbCon.state() = ConnectionState.Open Then
            If withall Then
                dbCon.SQL("select 'All' [category], '' [category_code] union all
                            select category, category_code from item_categories where company_code = @cc and parent_code = @pc")
                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@pc", parent)
                dbCon.fillTable(dt)
            Else
                dbCon.SQL("select category, category_code from item_categories where company_code = @cc and parent_code = @pc")
                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@pc", parent)
                dbCon.fillTable(dt)
            End If

            cbSubCategory.DataSource = New BindingSource(dt, Nothing)
            cbSubCategory.DisplayMember = "category"
            cbSubCategory.ValueMember = "category_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    'Private Sub loadItemTypes()
    '    Dim dt As New DataTable

    '    If dbCon.state() = ConnectionState.Open Then
    '        dbCon.SQL("select type, type_code, is_sack from item_types where company_code = @cc")
    '        dbCon.addParameter("@cc", corp)
    '        dbCon.fillTable(dt)

    '        cbType.DataSource = New BindingSource(dt, Nothing)
    '        cbType.DisplayMember = "type"
    '        cbType.ValueMember = "type_code"
    '    Else
    '        MsgBox("Connection error, please check your network connection and try again")
    '    End If
    'End Sub

    Private Sub loadCurrencies()
        Dim dt As New DataTable
        Dim bs As BindingSource

        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select currency, currency_code from currencies")
            'dbCon.addParameter("@cc", corp)
            dbCon.fillTable(dt)

            bs = New BindingSource(dt, Nothing)
            cbVendorCurrency.DataSource = bs
            cbVendorCurrency.DisplayMember = "currency_code"
            cbVendorCurrency.ValueMember = "currency_code"
            cbCustCurrency.DataSource = bs
            cbCustCurrency.DisplayMember = "currency_code"
            cbCustCurrency.ValueMember = "currency_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadUOMs()
        Dim dt As New DataTable

        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select uom, uom_code from uoms where company_code = @cc")
            dbCon.addParameter("@cc", corp)
            dbCon.fillTable(dt)

            cbUOM.DataSource = New BindingSource(dt, Nothing)
            cbUOM.DisplayMember = "uom"
            cbUOM.ValueMember = "uom_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadSackTypes()
        Dim dt As New DataTable

        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select sack, sack_code, woven from sack_types where company_code = @cc")
            dbCon.addParameter("@cc", corp)
            dbCon.fillTable(dt)

            cbSackType.DataSource = New BindingSource(dt, Nothing)
            cbSackType.DisplayMember = "sack"
            cbSackType.ValueMember = "sack_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadWebbings()
        Dim dt As New DataTable

        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select webbing, webbing_code, [value] from sack_webbings where company_code = @cc")
            dbCon.addParameter("@cc", corp)
            dbCon.fillTable(dt)

            cbWebbing.DataSource = New BindingSource(dt, Nothing)
            cbWebbing.DisplayMember = "webbing"
            cbWebbing.ValueMember = "webbing_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadDenier()
        Dim dt As New DataTable

        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select denier, denier_code, weight from sack_denier where company_code = @cc")
            dbCon.addParameter("@cc", corp)
            dbCon.fillTable(dt)

            cbDenier.DataSource = New BindingSource(dt, Nothing)
            cbDenier.DisplayMember = "denier"
            cbDenier.ValueMember = "denier_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    'Private Sub loadHandle()
    '    Dim dt As New DataTable

    '    If dbCon.state() = ConnectionState.Open Then
    '        dbCon.SQL("select handle, handle_code from sack_handle where company_code = @cc")
    '        dbCon.addParameter("@cc", corp)
    '        dbCon.fillTable(dt)

    '        cbHandle.DataSource = New BindingSource(dt, Nothing)
    '        cbHandle.DisplayMember = "handle"
    '        cbHandle.ValueMember = "handle_code"
    '    Else
    '        MsgBox("Connection error, please check your network connection and try again")
    '    End If
    'End Sub

    Private Sub addSackDetail()
        If dbCon.state() = ConnectionState.Open Then
            'dbCon.SQL("insert into item_sacks(company_code, item_code, capacity, color, webbing, sack_type, denier, weight, width, length, has_inner, inner_code, has_handle, handle_code, is_used)
            '                values(@cc, @ic, @cap, @col, @web, @stype, @denier, @weight, @width, @length, @has_inner, @icode, @has_handle, @handle, 1)")
            dbCon.SQL("insert into item_sacks(company_code, item_code, capacity, color, webbing, sack_type, denier, weight, width, length, has_handle, handle_code, is_used)
                            values(@cc, @ic, @cap, @col, @web, @stype, @denier, @weight, @width, @length, @has_inner, @icode, @has_handle, @handle, 1)")
            dbCon.addParameter("@cc", corp)
            dbCon.addParameter("@ic", tbCode.Text)
            dbCon.addParameter("@cap", If(tbCapacity.Text = "", 0, Decimal.Parse(tbCapacity.Text)))
            dbCon.addParameter("@col", tbColor.Text)
            dbCon.addParameter("@web", cbWebbing.SelectedValue)
            dbCon.addParameter("@stype", cbSackType.SelectedValue)
            dbCon.addParameter("@denier", cbDenier.SelectedValue)
            dbCon.addParameter("@weight", If(tbSackWeight.Text = "", 0, Decimal.Parse(tbSackWeight.Text)))
            dbCon.addParameter("@width", If(tbWidth.Text = "", 0, Decimal.Parse(tbWidth.Text)))
            dbCon.addParameter("@length", If(tbLength.Text = "", 0, Decimal.Parse(tbLength.Text)))
            'dbCon.addParameter("@has_inner", chkHasInner.Checked)
            'dbCon.addParameter("@icode", cbInner.SelectedValue)
            dbCon.addParameter("@has_handle", chkHasHandle.Checked And Not IsNothing(cbHandle.SelectedValue))
            If Not IsNothing(cbHandle.SelectedValue) And chkHasHandle.Checked Then
                dbCon.addParameter("@handle", cbHandle.SelectedValue)
            Else
                dbCon.addParameter("@handle", "")
            End If

            Try
                dbCon.execute()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub updateSackDetail()
        If dbCon.state() = ConnectionState.Open Then
            'dbCon.SQL("update item_sacks set capacity = @cap, color = @col, webbing = @web, sack_type = @stype, 
            '                denier = @denier, weight = @weight, width = @width, length = @length, has_inner = @has_inner, inner_code = @icode, 
            '                has_handle = @has_handle, handle_code = @handle, is_used = 1
            '                where company_code = @cc and item_code = @ic")
            dbCon.SQL("update item_sacks set capacity = @cap, color = @col, webbing = @web, sack_type = @stype, 
                            denier = @denier, weight = @weight, width = @width, length = @length, 
                            has_handle = @has_handle, handle_code = @handle, is_used = 1
                            where company_code = @cc and item_code = @ic")
            dbCon.addParameter("@cap", If(tbCapacity.Text = "", 0, Decimal.Parse(tbCapacity.Text)))
            dbCon.addParameter("@col", tbColor.Text)
            dbCon.addParameter("@web", cbWebbing.SelectedValue)
            dbCon.addParameter("@stype", cbSackType.SelectedValue)
            dbCon.addParameter("@denier", cbDenier.SelectedValue)
            dbCon.addParameter("@weight", If(tbSackWeight.Text = "", 0, Decimal.Parse(tbSackWeight.Text)))
            dbCon.addParameter("@width", If(tbWidth.Text = "", 0, Decimal.Parse(tbWidth.Text)))
            dbCon.addParameter("@length", If(tbLength.Text = "", 0, Decimal.Parse(tbLength.Text)))
            'dbCon.addParameter("@has_inner", chkHasInner.Checked)
            'dbCon.addParameter("@icode", cbInner.SelectedValue)
            dbCon.addParameter("@has_handle", chkHasHandle.Checked And Not IsNothing(cbHandle.SelectedValue))
            If Not IsNothing(cbHandle.SelectedValue) And chkHasHandle.Checked Then
                dbCon.addParameter("@handle", cbHandle.SelectedValue)
            Else
                dbCon.addParameter("@handle", "")
            End If
            dbCon.addParameter("@cc", corp)
            dbCon.addParameter("@ic", tbCode.Text)

            Try
                dbCon.execute()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub disableSackDetail()
        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("update item_sacks set is_used = 0
                            where company_code = @cc and item_code = @ic")
            dbCon.addParameter("@cc", corp)
            dbCon.addParameter("@ic", tbCode.Text)

            Try
                dbCon.execute()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Function vendorExists(itemCode As String, vendorCode As String) As Boolean
        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select count(id) rows from item_vendors where item_code = @ic and vendor_code = @vc and company_code = @cc")
            dbCon.addParameter("@ic", itemCode)
            dbCon.addParameter("@vc", vendorCode)
            dbCon.addParameter("@cc", corp)
            Dim rows As Integer = CInt(dbCon.scalar())

            If rows > 0 Then
                Return True
            End If
        Else
            Throw New Exception("Connection error, please check your network connection and try again")
        End If

        Return False
    End Function

    Private Sub saveVendor(update As Boolean, ParamArray params() As Object)
        If dbCon.state() = ConnectionState.Open Then
            If update Then
                dbCon.SQL("update item_vendors set vendor_code = @vc, brand = @b, price = @p, currency_code = @ccc where id = @id")
                dbCon.addParameter("@vc", params(0))
                dbCon.addParameter("@b", params(1))
                dbCon.addParameter("@p", params(2))
                dbCon.addParameter("@ccc", params(3))
                dbCon.addParameter("@id", params(4))
            Else
                dbCon.SQL("insert into item_vendors(company_code, item_code, vendor_code, brand, price, currency_code) values(@cc, @ic, @vc, @b, @p, @ccc)")
                dbCon.addParameter("@cc", params(0))
                dbCon.addParameter("@ic", params(1))
                dbCon.addParameter("@vc", params(2))
                dbCon.addParameter("@b", params(3))
                dbCon.addParameter("@p", params(4))
                dbCon.addParameter("@ccc", params(5))
            End If
            dbCon.execute()
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub delVendor(id As String)
        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("delete from item_vendors where id = @id")
            dbCon.addParameter("@id", id)
            dbCon.execute()
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub saveVendors()
        For Each item As ListViewItem In vndList.Items
            If item.SubItems(5).Text = "new" Then
                saveVendor(False, corp, tbCode.Text, item.SubItems(1).Text, item.SubItems(2).Text, Decimal.Parse(item.SubItems(3).Text), item.SubItems(4).Text)
            Else
                saveVendor(True, item.SubItems(1).Text, item.SubItems(2).Text, Decimal.Parse(item.SubItems(3).Text), item.SubItems(4).Text, item.SubItems(6).Text)
            End If
        Next
        For Each id In delItemVend
            delVendor(id)
        Next
        delItemVend.Clear()
    End Sub

    Private Sub loadVendors(itemCode As String)
        Dim row As Dictionary(Of String, Object)
        Dim item As ListViewItem
        Dim rdr As Object

        tbVendorName.Text = ""
        tbVendorCode.Text = ""
        tbVendorBrand.Text = ""
        tbVendorPrice.Text = ""
        cbVendorCurrency.SelectedIndex = -1

        If dbCon.state() = ConnectionState.Open Then
            Dim cmdRdr = dbCon.SQLReader("select iv.*, v.vendor_name from item_vendors iv join vendors v on iv.vendor_code = v.vendor_code and iv.company_code = v.company_code
                            where iv.company_code = @cc and iv.item_code = @ic and iv.del = 0")
            dbCon.addParameter("@cc", corp, cmdRdr)
            dbCon.addParameter("@ic", itemCode, cmdRdr)

            rdr = dbCon.beginRead(cmdRdr)
            vndList.Items.Clear()

            While dbCon.doRead(rdr)
                row = dbCon.getRow(rdr)
                item = vndList.Items.Add(row("vendor_name"))
                item.SubItems.Add(row("vendor_code"))
                item.SubItems.Add(row("brand"))
                item.SubItems.Add(String.Format("{0:#,0.#}", row("price")))
                item.SubItems.Add(row("currency_code"))
                item.SubItems.Add("existing")
                item.SubItems.Add(row("id"))
            End While
            dbCon.endRead(rdr)
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Function custExists(itemCode As String, custCode As String) As Boolean
        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("select count(id) rows from item_customers where item_code = @ic and customer_code = @cmc and company_code = @cc")
            dbCon.addParameter("@ic", itemCode)
            dbCon.addParameter("@cmc", custCode)
            dbCon.addParameter("@cc", corp)
            Dim rows As Integer = CInt(dbCon.scalar())

            If rows > 0 Then
                Return True
            End If
        Else
            Throw New Exception("Connection error, please check your network connection and try again")
        End If

        Return False
    End Function

    Private Sub saveCust(update As Boolean, ParamArray params() As Object)
        If dbCon.state() = ConnectionState.Open Then
            If update Then
                dbCon.SQL("update item_customers set customer_code = @cmc, min_order = @mo, price = @p, currency_code = @ccc where id = @id")
                dbCon.addParameter("@cmc", params(0))
                dbCon.addParameter("@mo", params(1))
                dbCon.addParameter("@p", params(2))
                dbCon.addParameter("@ccc", params(3))
                dbCon.addParameter("@id", params(4))
            Else
                dbCon.SQL("insert into item_customers(company_code, item_code, customer_code, min_order, price, currency_code) values(@cc, @ic, @cmc, @mo, @p, @ccc)")
                dbCon.addParameter("@cc", params(0))
                dbCon.addParameter("@ic", params(1))
                dbCon.addParameter("@cmc", params(2))
                dbCon.addParameter("@mo", params(3))
                dbCon.addParameter("@p", params(4))
                dbCon.addParameter("@ccc", params(5))
            End If
            dbCon.execute()
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub delCust(id As String)
        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("delete from item_customers where id = @id")
            dbCon.addParameter("@id", id)
            dbCon.execute()
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub saveCusts()
        For Each item As ListViewItem In custList.Items
            If item.SubItems(5).Text = "new" Then
                saveCust(False, corp, tbCode.Text, item.SubItems(1).Text, Decimal.Parse(item.SubItems(4).Text), Decimal.Parse(item.SubItems(2).Text), item.SubItems(3).Text)
            Else
                saveCust(True, item.SubItems(1).Text, Decimal.Parse(item.SubItems(4).Text), Decimal.Parse(item.SubItems(2).Text), item.SubItems(3).Text, item.SubItems(6).Text)
            End If
        Next
        For Each id In delItemCust
            delCust(id)
        Next
        delItemCust.Clear()
    End Sub

    Private Sub loadCusts(itemCode As String)
        Dim row As Dictionary(Of String, Object)
        Dim item As ListViewItem
        Dim rdr As Object

        tbCustName.Text = ""
        tbCustCode.Text = ""
        tbCustPrice.Text = ""
        cbCustCurrency.SelectedIndex = -1
        tbCustMinOrder.Text = ""

        If dbCon.state() = ConnectionState.Open Then
            Dim cmdRdr = dbCon.SQLReader("select ic.*, c.customer_name from item_customers ic join customers c on ic.customer_code = c.customer_code and ic.company_code = c.company_code
                            where ic.company_code = @cc and ic.item_code = @ic and ic.del = 0")
            dbCon.addParameter("@cc", corp, cmdRdr)
            dbCon.addParameter("@ic", itemCode, cmdRdr)

            rdr = dbCon.beginRead(cmdRdr)
            custList.Items.Clear()

            While dbCon.doRead(rdr)
                row = dbCon.getRow(rdr)
                item = custList.Items.Add(row("customer_name"))
                item.SubItems.Add(row("customer_code"))
                item.SubItems.Add(String.Format("{0:#,0.#}", row("price")))
                item.SubItems.Add(row("currency_code"))
                item.SubItems.Add(String.Format("{0:#,0.#}", row("min_order")))
                item.SubItems.Add("existing")
                item.SubItems.Add(row("id"))
            End While
            dbCon.endRead(rdr)
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub saveItem(update As Boolean)
        If dbCon.state() = ConnectionState.Open Then
            If update Then
                Dim sql = "update items set item_name = @in, category = @c, uom_code = @uom, min_stock = @min, max_stock = @max,
                            unit_weight = @uw, usable = @use, producible = @make, buyable = @buy, sellable = @sell
                            where company_code = @cc and item_code = @ic"
                Dim sqlPict = "update items set item_name = @in, category = @c, uom_code = @uom, min_stock = @min, max_stock = @max,
                            unit_weight = @uw, usable = @use, producible = @make, buyable = @buy, sellable = @sell, picture = @pict
                            where company_code = @cc and item_code = @ic"
                'dbCon.SQL("update items set item_name = @in, item_type = @it, category = @c, uom_code = @uom, min_stock = @min, max_stock = @max,
                '            unit_weight = @uw, usable = @use, producible = @make, buyable = @buy, sellable = @sell, picture = @pict
                '            where company_code = @cc and item_code = @ic")
                If Not IsNothing(pict.ImageLocation) Then
                    Dim fname As String = tbCode.Text
                    Dim f As Byte()
                    If pict.ImageLocation <> "images/" + fname Then
                        f = File.ReadAllBytes(pict.ImageLocation)
                        dbCon.SQL(sqlPict)
                        dbCon.addParameter("@pict", f)
                    Else
                        dbCon.SQL(sql)
                    End If
                Else
                    dbCon.SQL(sqlPict)
                    dbCon.addParameter("@pict", Nothing)
                End If
                dbCon.addParameter("@in", tbName.Text)
                'dbCon.addParameter("@it", cbType.SelectedValue)
                dbCon.addParameter("@c", cbSubCategory.SelectedValue)
                dbCon.addParameter("@uom", cbUOM.SelectedValue)
                dbCon.addParameter("@min", If(tbMinStock.Text = "", 0, Decimal.Parse(tbMinStock.Text)))
                dbCon.addParameter("@max", If(tbMaxStock.Text = "", 0, Decimal.Parse(tbMaxStock.Text)))
                dbCon.addParameter("@uw", If(tbUnitWeight.Text = "", 0, Decimal.Parse(tbUnitWeight.Text)))
                dbCon.addParameter("@use", chkUse.Checked)
                dbCon.addParameter("@make", chkMake.Checked)
                dbCon.addParameter("@buy", chkBuy.Checked)
                dbCon.addParameter("@sell", chkSell.Checked)
                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@ic", tbCode.Text)

                Try
                    dbCon.execute()

                    dbCon.SQL("select id from item_sacks where company_code = @cc and item_code = @ic")
                    dbCon.addParameter("@cc", corp)
                    dbCon.addParameter("@ic", tbCode.Text)
                    Dim ds As DataSet = dbCon.getData()

                    If chkIsSack.Checked Then
                        If ds.Tables(0).Rows.Count = 1 Then
                            updateSackDetail()
                        Else
                            addSackDetail()
                        End If
                    Else
                        If ds.Tables(0).Rows.Count = 1 Then
                            disableSackDetail()
                        End If
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                'dbCon.SQL("insert into items(company_code, item_code, item_name, item_type, category, uom_code, min_stock, max_stock, unit_weught, usable, producible, buyable, sellable, picture)
                '        values(@cc, @ic, @in, @it, @c, @uom, @min, @max, @use, @make, @buy, @sell, @pict)")
                dbCon.SQL("insert into items(company_code, item_code, item_name, category, uom_code, min_stock, max_stock, unit_weught, usable, consumable, producible, buyable, sellable, picture)
                        values(@cc, @ic, @in, @c, @uom, @min, @max, @use, @consume, @make, @buy, @sell, @pict)")
                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@ic", tbCode.Text)
                dbCon.addParameter("@in", tbName.Text)
                'dbCon.addParameter("@it", cbType.SelectedValue)
                dbCon.addParameter("@c", cbSubCategory.SelectedValue)
                dbCon.addParameter("@uom", cbUOM.SelectedValue)
                dbCon.addParameter("@min", If(tbMinStock.Text = "", 0, Decimal.Parse(tbMinStock.Text)))
                dbCon.addParameter("@max", If(tbMaxStock.Text = "", 0, Decimal.Parse(tbMaxStock.Text)))
                dbCon.addParameter("@uw", If(tbUnitWeight.Text = "", 0, Decimal.Parse(tbUnitWeight.Text)))
                dbCon.addParameter("@use", chkUse.Checked)
                dbCon.addParameter("@consume", chkConsume.Checked)
                dbCon.addParameter("@make", chkMake.Checked)
                dbCon.addParameter("@buy", chkBuy.Checked)
                dbCon.addParameter("@sell", chkSell.Checked)
                If Not IsNothing(pict.ImageLocation) Then
                    Dim fname As String = tbCode.Text + Path.GetExtension(pict.ImageLocation)
                    If pict.ImageLocation <> "images/" + fname Then
                        File.Copy(pict.ImageLocation, "images/" + fname, True)
                    End If
                    dbCon.addParameter("@pict", fname)
                Else
                    dbCon.addParameter("@pict", "")
                End If

                Try
                    dbCon.execute()

                    If chkIsSack.Checked Then
                        addSackDetail()
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles pict.Click
        OpenDlg.FileName = ""
        If OpenDlg.ShowDialog() = DialogResult.OK Then
            pict.ImageLocation = OpenDlg.FileName
            pict.Load()
        End If
    End Sub

    'Private Sub ItemMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    '    itemList.VirtualMode = True
    '    loadItems()
    'End Sub

    Private Sub ItemMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim btn As ERPModules.buttonState

        _parent = FindForm()

        SplitContainer1.Panel1Collapsed = False
        SplitContainer1.Panel2Collapsed = True

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = True
        toolButton.Add("New", btn)

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = False
        toolButton.Add("Edit", btn)

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = False
        toolButton.Add("Save", btn)

        btn = New ERPModules.buttonState()
        btn.visible = True
        btn.enabled = False
        toolButton.Add("Delete", btn)

        btn = New ERPModules.buttonState()
        btn.visible = False
        btn.enabled = False
        toolButton.Add("Print", btn)

        Application.DoEvents()

        loadCategories()
        loadSubCategories("", True)
        loadCurrencies()
        loadUOMs()
        loadSackTypes()
        loadWebbings()
        loadDenier()

        itemList.VirtualMode = True
        asyncLoadItems()

    End Sub

    Private Sub ItemMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.F5 Then
            asyncLoadItems()
        End If
    End Sub

    Private Sub cbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCategory.SelectedIndexChanged
        If Not IsNothing(cbCategory.SelectedValue) Then
            Try
                loadSubCategories(cbCategory.SelectedValue, False)
            Catch
            End Try
        Else
            cbSubCategory.DataSource = Nothing
        End If
    End Sub

    'Private Sub cbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbType.SelectedIndexChanged
    '    If cbType.SelectedIndex > -1 Then
    '        If cbType.SelectedItem.Item("is_sack") Then
    '            sackGroup.Enabled = True
    '        Else
    '            sackGroup.Enabled = False
    '        End If
    '    Else
    '        sackGroup.Enabled = False
    '    End If
    'End Sub

    Private Function calcSackWeight() As Decimal
        Dim p As Decimal = 0, l As Decimal = 0, a As Decimal = 0, d As Decimal = 0, w As Decimal

        Decimal.TryParse(tbLength.Text, p)
        Decimal.TryParse(tbWidth.Text, l)
        If cbWebbing.SelectedIndex > -1 Then
            a = cbWebbing.SelectedItem.Item("value")
        End If
        If cbDenier.SelectedIndex > -1 Then
            d = cbDenier.SelectedItem.Item("weight")
        End If

        If p < l Then
            Dim tmp = p
            p = l
            l = tmp
        End If

        w = 2 * l * p * 2 * a * d / 2286
        Return w
    End Function

    Private Sub cbSackType_DropDown(sender As Object, e As EventArgs) Handles cbSackType.DropDown
        With DirectCast(sender, ComboBox)
            Dim w As Integer = .DropDownWidth
            Dim g As Graphics = .CreateGraphics()
            Dim f As Font = .Font
            Dim vscroll As Integer = If(.Items.Count > .MaxDropDownItems, SystemInformation.VerticalScrollBarWidth, 0)

            Dim nw As Integer
            For Each s In .Items
                nw = Math.Round(g.MeasureString(s.Item(.DisplayMember), f).Width) + vscroll
                If w < nw Then
                    w = nw
                End If
            Next
            .DropDownWidth = w
        End With
    End Sub

    Private Sub sack_TextChanged(sender As Object, e As EventArgs) Handles tbLength.ValueChanged, tbWidth.ValueChanged, cbDenier.SelectedValueChanged, cbWebbing.SelectedValueChanged
        tbSackWeight.Text = String.Format(numformat, calcSackWeight())
    End Sub

    'Private Sub chkHasInner_CheckedChanged(sender As Object, e As EventArgs)
    '    cbInner.Enabled = chkHasInner.Checked
    'End Sub

    'Private Sub chkHasHandle_CheckedChanged(sender As Object, e As EventArgs) Handles chkHasInner.CheckedChanged
    '    cbHandle.Enabled = chkHasHandle.Checked
    'End Sub

    Private Sub itemList_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles itemList.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = itemSearch(e.ItemIndex)
        e.Item = New ListViewItem(row("item_name").ToString)
        e.Item.SubItems.Add(row("item_code"))
        e.Item.SubItems.Add(row("category"))
        e.Item.SubItems.Add(row("subcat"))
        e.Item.SubItems.Add(String.Format("{0:#,0.#}", row("quantity")))
        e.Item.SubItems.Add(row("uom"))
        e.Item.SubItems.Add(String.Format("{0:#,0.#}", row("weight")))
        e.Item.SubItems.Add(String.Format("{0:#,0.#}", row("cog")))
        If row("usable") Then
            e.Item.SubItems.Add("Yes")
        Else
            e.Item.SubItems.Add("No")
        End If
        If row("producible") Then
            e.Item.SubItems.Add("Yes")
        Else
            e.Item.SubItems.Add("No")
        End If
        If row("buyable") Then
            e.Item.SubItems.Add("Yes")
        Else
            e.Item.SubItems.Add("No")
        End If
        If row("sellable") Then
            e.Item.SubItems.Add("Yes")
        Else
            e.Item.SubItems.Add("No")
        End If
        If row("is_sack") Then
            e.Item.SubItems.Add("Yes")
        Else
            e.Item.SubItems.Add("No")
        End If
    End Sub

    Private Sub ItemSort(order As SortOrder, comparer As Comparison(Of Dictionary(Of String, Object)))
        itemSearch.Sort(Function(a, b)
                            Dim lret As Integer = comparer(a, b)
                            If order = SortOrder.Descending Then
                                lret = lret * -1
                            End If
                            Return lret
                        End Function)
    End Sub

    Private Sub itemList_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles itemList.ColumnClick
        Dim sort As Integer
        Dim dosort As Boolean = False

        Select Case e.Column
            Case 0
                sort = 0
                dosort = True
            Case 1
                sort = 1
                dosort = True
            Case 2
                sort = 2
                dosort = True
            Case 5
                sort = 3
                dosort = True
            Case 7
                sort = 4
                dosort = True
        End Select

        If dosort Then
            Dim newOrder
            If e.Column = itemSortColumn Then
                newOrder = itemToggle(itemOrderMap(sort))
            Else
                newOrder = SortOrder.Ascending
                itemSortColumn = e.Column
            End If
            itemOrderMap(sort) = newOrder
            ItemSort(newOrder, itemComparers(sort))
            itemList.Refresh()
        End If
    End Sub

    Private Sub itemList_DoubleClick(sender As Object, e As EventArgs) Handles itemList.DoubleClick
        EditForm()
    End Sub

    Private Function searchItem(lst As List(Of Dictionary(Of String, Object)), val As String) As IQueryable(Of Dictionary(Of String, Object))
        searchItem = lst.AsQueryable

        Dim filter As Expressions.Expression(Of Func(Of Dictionary(Of String, Object), Boolean)) =
            Function(i As Dictionary(Of String, Object)) i.Where(Function(x) x.Value.ToString().IndexOf(val, 0, StringComparison.CurrentCultureIgnoreCase) > -1).Count > 0

        If Not String.IsNullOrEmpty(val) Then searchItem = Queryable.Where(searchItem, filter)
    End Function

    Private Sub tbSearchItem_TextChanged(sender As Object, e As EventArgs) Handles tbSearchItem.TextChanged
        searchTimer.Enabled = False
        searchTimer.Enabled = True
    End Sub

    Private Sub cbUOM_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbUOM.SelectionChangeCommitted
        lbMaxUom.Text = cbUOM.Text
        lbMinUom.Text = cbUOM.Text
        lbQtyUom.Text = cbUOM.Text
    End Sub

    Private Sub btnCreateVendor_Click(sender As Object, e As EventArgs) Handles btnCreateVendor.Click
        Dim item As ListViewItem

        item = vndList.Items.Add(tbVendorName.Text)
        item.SubItems.Add(tbVendorCode.Text)
        item.SubItems.Add(tbVendorBrand.Text)
        item.SubItems.Add(tbVendorPrice.Text)
        item.SubItems.Add(cbVendorCurrency.SelectedValue)
        item.SubItems.Add("new")
    End Sub

    Private Sub btnEditVendor_Click(sender As Object, e As EventArgs) Handles btnEditVendor.Click
        Dim item As ListViewItem

        If vndList.SelectedItems.Count = 1 Then
            item = vndList.SelectedItems(0)
            item.Text = tbVendorName.Text
            item.SubItems(1).Text = tbVendorCode.Text
            item.SubItems(2).Text = tbVendorBrand.Text
            item.SubItems(3).Text = tbVendorPrice.Text
            item.SubItems(4).Text = cbVendorCurrency.SelectedValue
        End If
    End Sub

    Private Sub btnEditCust_Click(sender As Object, e As EventArgs) Handles btnEditCust.Click
        Dim item As ListViewItem

        If custList.SelectedItems.Count = 1 Then
            item = custList.SelectedItems(0)
            item.Text = tbCustName.Text
            item.SubItems(1).Text = tbCustCode.Text
            item.SubItems(2).Text = tbCustPrice.Text
            item.SubItems(3).Text = cbCustCurrency.SelectedValue
            item.SubItems(4).Text = tbCustMinOrder.Text
        End If
    End Sub

    Private Sub btnCreateCust_Click(sender As Object, e As EventArgs) Handles btnCreateCust.Click
        Dim item As ListViewItem

        item = custList.Items.Add(tbCustName.Text)
        item.SubItems.Add(tbCustCode.Text)
        item.SubItems.Add(tbCustPrice.Text)
        item.SubItems.Add(cbCustCurrency.SelectedValue)
        item.SubItems.Add(tbCustMinOrder.Text)
        item.SubItems.Add("new")
    End Sub

    Private Sub vndList_DoubleClick(sender As Object, e As EventArgs) Handles vndList.DoubleClick
        Dim item As ListViewItem

        If vndList.SelectedItems.Count = 1 Then
            item = vndList.SelectedItems(0)
            tbVendorName.Text = item.Text
            tbVendorCode.Text = item.SubItems(1).Text
            tbVendorBrand.Text = item.SubItems(2).Text
            tbVendorPrice.Text = item.SubItems(3).Text
            cbVendorCurrency.SelectedValue = item.SubItems(4).Text
        End If
    End Sub

    Private Sub custList_DoubleClick(sender As Object, e As EventArgs) Handles vndList.DoubleClick
        Dim item As ListViewItem

        If custList.SelectedItems.Count = 1 Then
            item = custList.SelectedItems(0)
            tbCustName.Text = item.Text
            tbCustCode.Text = item.SubItems(1).Text
            tbCustPrice.Text = item.SubItems(2).Text
            cbCustCurrency.SelectedValue = item.SubItems(3).Text
            tbCustMinOrder.Text = item.SubItems(4).Text
        End If
    End Sub

    Private Sub OpenForm()
        SplitContainer1.Panel1Collapsed = True
        SplitContainer1.Panel2Collapsed = False

        tcDetail.SelectedIndex = 0

        toolButton("Save").enabled = True
        toolButton("Edit").enabled = False
        toolButton("Delete").enabled = False
        toolButton("Print").enabled = False
    End Sub

    Private Sub CloseForm()
        SplitContainer1.Panel1Collapsed = False
        SplitContainer1.Panel2Collapsed = True
        toolButton("Save").enabled = False
        If itemList.SelectedIndices.Count = 1 Then
            toolButton("Edit").enabled = True
        End If
        If itemList.SelectedIndices.Count = 1 Then
            toolButton("Delete").enabled = True
        End If
    End Sub

    Private Sub clearForm()
        updateData = False

        tbName.Text = ""
        tbCode.Text = ""

        cbCategory.SelectedIndex = -1
        loadSubCategories("", False)

        cbSubCategory.SelectedIndex = -1

        'cbType.SelectedIndex = -1

        tbQuantity.Text = ""

        cbUOM.SelectedIndex = -1

        lbMaxUom.Text = ""
        lbMinUom.Text = ""
        lbQtyUom.Text = ""

        tbWeight.Text = ""
        tbCOG.Text = ""

        chkUse.Checked = False
        chkMake.Checked = False
        chkBuy.Checked = False
        chkSell.Checked = False

        chkIsSack.Checked = False

        tbMinStock.Text = ""
        tbMaxStock.Text = ""
        tbCapacity.Text = ""
        tbColor.Text = ""

        cbWebbing.SelectedIndex = -1
        cbDenier.SelectedIndex = -1
        cbSackType.SelectedIndex = -1

        tbWidth.Text = "0"
        tbLength.Text = "0"
        tbSackWeight.Text = "0"

        'chkHasInner.Checked = False
        'cbInner.SelectedValue = ""

        chkHasHandle.Checked = False
        cbHandle.SelectedIndex = -1

        pict.Image = Nothing

        gbWovenBag.Enabled = False

        vndList.Items.Clear()
        custList.Items.Clear()
        locList.Items.Clear()
        histList.Items.Clear()
        formulaList.Items.Clear()
    End Sub

    'Private Sub btnCreateItem_Click(sender As Object, e As EventArgs)
    '    clearForm()

    '    tcDetail.SelectedIndex = 0
    '    'tcMain.SelectedIndex = 1
    'End Sub

    Private Sub btnDelVendor_Click(sender As Object, e As EventArgs)
        Dim item As ListViewItem

        If vndList.SelectedItems.Count = 1 Then
            item = vndList.SelectedItems(0)
            If item.SubItems(5).Text <> "new" Then
                delItemVend.Add(item.SubItems(6).Text)
            End If
            item.Remove()
        End If
    End Sub

    Private Sub btnDelCust_Click(sender As Object, e As EventArgs) Handles btnDelCust.Click
        Dim item As ListViewItem

        If custList.SelectedItems.Count = 1 Then
            item = custList.SelectedItems(0)
            If item.SubItems(5).Text <> "new" Then
                delItemCust.Add(item.SubItems(6).Text)
            End If
            item.Remove()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearForm()
        CloseForm()
        _parent.refreshMenuState(toolButton)
    End Sub

    Private Sub cbIsSack_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsSack.CheckedChanged
        gbWovenBag.Enabled = chkIsSack.Checked
        gbJumboBag.Enabled = chkIsSack.Checked
        gbInnerBag.Enabled = chkIsSack.Checked
    End Sub

    Private Sub cbSackType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSackType.SelectedIndexChanged
        If Not IsNothing(cbSackType.SelectedItem) Then
            If cbSackType.SelectedItem.Item("woven") Then
                tbThick.Enabled = False
                tbThick.Value = 0
                cbDenier.Enabled = True
                cbWebbing.Enabled = True
            Else
                tbThick.Enabled = True
                cbDenier.Enabled = False
                cbDenier.SelectedIndex = -1
                cbWebbing.Enabled = False
                cbWebbing.SelectedIndex = -1
            End If
        End If
    End Sub

    Private Sub itemList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles itemList.SelectedIndexChanged
        If itemList.SelectedIndices.Count = 0 Then
            toolButton("Edit").enabled = False
            toolButton("Delete").enabled = False
        Else
            toolButton("Edit").enabled = True
            toolButton("Delete").enabled = True
        End If
        _parent.refreshMenuState(toolButton)
    End Sub

    Private Sub searchTimer_Tick(sender As Object, e As EventArgs) Handles searchTimer.Tick
        Dim tmp As IQueryable(Of Dictionary(Of String, Object))
        tmp = searchItem(itemBuffer, tbSearchItem.Text)
        If tmp IsNot Nothing Then
            itemSearch = tmp.ToList()
        Else
            itemSearch.Clear()
        End If
        itemList.VirtualListSize = itemSearch.Count
        itemList.SelectedIndices.Clear()
        searchTimer.Enabled = False
    End Sub

    Private Sub cbBagType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbBagType.SelectedIndexChanged
        'For i = 0 To TableLayoutPanel1.ColumnCount - 1
        '    With TableLayoutPanel1.ColumnStyles(i)
        '        .SizeType = SizeType.Percent
        '        .Width = 0
        '    End With
        'Next
        'With TableLayoutPanel1.ColumnStyles(cbBagType.SelectedIndex)
        '    .SizeType = SizeType.Percent
        '    .Width = 100
        'End With
    End Sub

End Class
