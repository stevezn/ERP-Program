Imports System.Windows.Forms
Imports System.Web.Script.Serialization
Imports DB
Imports ERPModules

Public Class CustControl : Inherits ERPModule

    'Private dbCon As New DBConn
    'Private _corp As String
    'Private _area As String
    'Private _role As String
    'Private _user As String

    'Private _parent As Object
    Private doEvents As Boolean = False

    'Private json As New JavaScriptSerializer
    'Private updateData As Boolean = False

    'Private toolMenu As New Dictionary(Of String, ToolStripItem)
    'Private toolButton As New Dictionary(Of String, ERPModules.buttonState)

    Private custBuffer As New List(Of Dictionary(Of String, Object))
    Private custSearch As New List(Of Dictionary(Of String, Object))

    Public injectAscend As Boolean = False
    Public ascendConStr As String
    Private ascendCon As DBConn

    'Public Property corp As String Implements IERPModule.corp
    '    Get
    '        Return _corp
    '    End Get
    '    Set(value As String)
    '        _corp = value
    '    End Set
    'End Property

    'Public Property area As String Implements IERPModule.area
    '    Get
    '        Return _area
    '    End Get
    '    Set(value As String)
    '        _area = value
    '    End Set
    'End Property

    'Public Property dbCon As DBConn Implements IERPModule.dbCon
    '    Get
    '        Return dbCon
    '    End Get
    '    Set(value As DBConn)
    '        dbCon = value
    '    End Set
    'End Property

    'Public Property role As String Implements IERPModule.role
    '    Get
    '        Return _role
    '    End Get
    '    Set(value As String)
    '        _role = value
    '    End Set
    'End Property

    'Public ReadOnly Property toolMenu As Dictionary(Of String, ToolStripItem) Implements IERPModule.toolMenu
    '    Get
    '        Return toolMenu
    '    End Get
    'End Property

    'Public ReadOnly Property toolButton As Dictionary(Of String, ERPModules.buttonState) Implements IERPModule.toolButton
    '    Get
    '        Return toolButton
    '    End Get
    'End Property

    'Public Property user As String Implements IERPModule.user
    '    Get
    '        Return _user
    '    End Get
    '    Set(value As String)
    '        _user = value
    '    End Set
    'End Property

    Public Overrides Sub Print()
        MsgBox("print")
    End Sub

    Public Overrides Sub Save()
        saveCustomer(updateData)
        loadCusts()
    End Sub

    'Public Function isPrived(task As String, lvl As Integer) As Boolean Implements IERPModule.isPrived
    '    Return True
    'End Function

    Public Sub NewForm()
        OpenForm()
        clearForm()
        _parent.refreshMenuState(toolButton)
    End Sub

    Private Sub OpenForm()
        SplitContainer1.Panel1Collapsed = True
        SplitContainer1.Panel2Collapsed = False

        toolButton("Save").enabled = True
        toolButton("Edit").enabled = False
        toolButton("Delete").enabled = False
        toolButton("Print").enabled = False
    End Sub

    Private Sub CloseForm()
        SplitContainer1.Panel1Collapsed = False
        SplitContainer1.Panel2Collapsed = True

        toolButton("Save").enabled = False
        If lstCust.SelectedIndices.Count = 1 Then
            toolButton("Edit").enabled = True
        End If
        If lstCust.SelectedIndices.Count = 1 Then
            toolButton("Delete").enabled = True
        End If
    End Sub

    Private Sub clearForm()
        updateData = False

        tbCode.Text = ""
        tbName.Text = ""
        cbType.SelectedIndex = -1

        tbEmail.Text = ""
        tbPhone.Text = ""

        tbMailAddr.Text = ""
        cbMailCity.SelectedIndex = -1
        cbMailState.SelectedIndex = -1
        cbMailCountry.SelectedIndex = -1

        cbTax.SelectedIndex = -1
        tbNpwp.Text = ""
        tbTaxAddr.Text = ""
        cbTaxCity.SelectedIndex = -1
        cbTaxState.SelectedIndex = -1
        cbTaxCountry.SelectedIndex = -1

        tbEmail.Text = ""
        tbPhone.Text = ""
    End Sub


    Public Sub EditForm()
        Dim item As ListViewItem
        Dim row As Dictionary(Of String, Object)

        If lstCust.SelectedIndices.Count = 1 Then
            Dim contacts As Dictionary(Of String, String)

            updateData = True

            item = lstCust.Items(lstCust.SelectedIndices(0))
            row = custSearch(item.Index)

            Try
                tbCode.Text = row("customer_code")
                tbName.Text = row("customer_name")
                cbType.SelectedValue = row("customer_type")
                tbMailAddr.Text = row("mailing_addr")
                If row("mailing_country_code") = "" Then
                    cbMailCountry.SelectedIndex = -1
                Else
                    cbMailCountry.SelectedValue = row("mailing_country_code")
                End If
                If row("mailing_state_code") = "" Then
                    cbMailState.SelectedIndex = -1
                Else
                    cbMailState.SelectedValue = row("mailing_state_code")
                End If

                tbTOP.Value = row("top")
                cbSales.SelectedValue = row("sales_person_code")
                tbCreditLimit.Value = row("credit_limit")

                loadCities(cbMailCity, row("mailing_state_code"))
                cbMailCity.SelectedValue = row("mailing_city_code")

                cbTax.SelectedValue = row("tax")
                tbNpwp.Text = row("npwp")
                tbTaxAddr.Text = row("tax_addr")
                If row("tax_country_code") = "" Then
                    cbTaxCountry.SelectedIndex = -1
                Else
                    cbTaxCountry.SelectedValue = row("tax_country_code")
                End If
                If row("tax_state_code") = "" Then
                    cbTaxState.SelectedIndex = -1
                Else
                    cbTaxState.SelectedValue = row("tax_state_code")
                End If

                loadCities(cbTaxCity, row("tax_state_code"))
                cbTaxCity.SelectedValue = row("tax_city_code")
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try

            contacts = json.Deserialize(Of Dictionary(Of String, String))(row("contacts"))

            If contacts.ContainsKey("email") Then
                tbEmail.Text = contacts("email")
            Else
                tbEmail.Text = ""
            End If
            If contacts.ContainsKey("phone") Then
                tbPhone.Text = contacts("phone")
            Else
                tbPhone.Text = ""
            End If

            OpenForm()
        End If
    End Sub

    Public Overloads Sub Dispose()
        Try
            json = Nothing
            toolMenu.Clear()
            custBuffer.Clear()
            custSearch.Clear()

            MyBase.Dispose()
        Catch
        End Try
    End Sub

    Private Sub loadCities(target As ComboBox, state As String)
        If dbCon.state() = ConnectionState.Open Then
            Dim dt As New DataTable

            dbCon.SQL("select * from cities where state_id = @si")
            dbCon.addParameter("@si", state)
            dbCon.fillTable(dt)

            target.DataSource = New BindingSource(dt, Nothing)
            target.DisplayMember = "city"
            target.ValueMember = "id"
            target.SelectedIndex = -1
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadStates()
        If dbCon.state() = ConnectionState.Open Then
            Dim dt As New DataTable

            dbCon.SQL("select * from states")
            dbCon.fillTable(dt)

            cbMailState.DataSource = New BindingSource(dt, Nothing)
            cbMailState.DisplayMember = "state"
            cbMailState.ValueMember = "id"

            cbTaxState.DataSource = New BindingSource(dt, Nothing)
            cbTaxState.DisplayMember = "state"
            cbTaxState.ValueMember = "id"

            cbMailState.SelectedIndex = -1
            cbTaxState.SelectedIndex = -1
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadCountries()
        If dbCon.state() = ConnectionState.Open Then
            Dim dt As New DataTable

            dbCon.SQL("select * from country where del = 0")
            dbCon.fillTable(dt)

            cbMailCountry.DataSource = New BindingSource(dt, Nothing)
            cbMailCountry.DisplayMember = "country"
            cbMailCountry.ValueMember = "country_code"
            cbTaxCountry.DataSource = New BindingSource(dt, Nothing)
            cbTaxCountry.DisplayMember = "country"
            cbTaxCountry.ValueMember = "country_code"
            cbMailCountry.SelectedIndex = -1
            cbTaxCountry.SelectedIndex = -1
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadCusts()
        If dbCon.state() = ConnectionState.Open Then
            Dim ctr As Integer = 0
            Dim rdr As Object

            Dim cmdRdr = dbCon.SQLReader("select * from customers where company_code = @cc and del = 0")
            dbCon.addParameter("@cc", corp, cmdRdr)

            rdr = dbCon.beginRead(cmdRdr)
            custBuffer.Clear()
            custSearch.Clear()

            _parent.spbUpdate.Visible = True
            _parent.spbUpdate.Value = 0
            _parent.spbUpdate.Style = ProgressBarStyle.Marquee
            _parent.spbUpdate.Text = "Loading Items"

            While dbCon.doRead(rdr)
                custBuffer.Add(dbCon.getRow(rdr))
                If ctr = 25 Then
                    _parent.spbUpdate.Value = (_parent.spbUpdate.Value + 1) Mod 100
                    ctr = 0
                End If
                ctr += 1
            End While
            dbCon.endRead(rdr)

            _parent.spbUpdate.Text = "Update Available"
            _parent.spbUpdate.Style = ProgressBarStyle.Continuous
            _parent.spbUpdate.Visible = False

            custSearch.AddRange(custBuffer)
            lstCust.VirtualListSize = custSearch.Count()
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadTaxes()
        If dbCon.state() = ConnectionState.Open Then
            Dim dt As New DataTable

            dbCon.SQL("select * from taxes where del = 0")
            dbCon.fillTable(dt)

            cbTax.DataSource = New BindingSource(dt, Nothing)
            cbTax.DisplayMember = "tax"
            cbTax.ValueMember = "tax_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadTypes()
        If dbCon.state() = ConnectionState.Open Then
            Dim dt As New DataTable

            dbCon.SQL("select * from customer_types where company_code = @cc and del = 0")
            dbCon.addParameter("@cc", corp)
            dbCon.fillTable(dt)

            cbType.DataSource = New BindingSource(dt, Nothing)
            cbType.DisplayMember = "customer_type"
            cbType.ValueMember = "customer_type_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadSalesPersons()
        If dbCon.state() = ConnectionState.Open Then
            Dim dt As New DataTable

            dbCon.SQL("select * from sales_persons where company_code = @cc and del = 0")
            dbCon.addParameter("@cc", corp)
            dbCon.fillTable(dt)

            cbSales.DataSource = New BindingSource(dt, Nothing)
            cbSales.DisplayMember = "sales_person"
            cbSales.ValueMember = "sales_person_code"
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub saveCustomer(update As Boolean)
        If dbCon.state() = ConnectionState.Open Then
            Dim contacts As New Dictionary(Of String, String)

            contacts.Add("email", tbEmail.Text)
            contacts.Add("phone", tbPhone.Text)
            If update Then
                dbCon.SQL("update customers set customer_name = @vn, customer_type = @vt, contacts = @con, 
                            mailing_addr = @maddr, mailing_city = @mct, mailing_city_code = @mctcd, mailing_state = @ms, 
                            mailing_state_code = @msc, mailing_country = @mctry, mailing_country_code = @mctrycd,
                            tax = @tax, npwp = @npwp,
                            tax_addr = @taddr, tax_city = @tct, tax_city_code = @tctcd, tax_state = @ts, 
                            tax_state_code = @tsc, tax_country = @tctry, tax_country_code = @tctrycd, [top] = @top,
                            sales_person_code = @spc, credit_limit = @crlmt
                            where company_code = @cc and customer_code = @vc")
                dbCon.addParameter("@vn", tbName.Text)
                dbCon.addParameter("@vt", cbType.SelectedValue)
                dbCon.addParameter("@con", json.Serialize(contacts))
                dbCon.addParameter("@maddr", tbMailAddr.Text)
                dbCon.addParameter("@mct", cbMailCity.Text)
                dbCon.addParameter("@mctcd", cbMailCity.SelectedValue)
                dbCon.addParameter("@ms", cbMailState.Text)
                dbCon.addParameter("@msc", cbMailState.SelectedValue)
                dbCon.addParameter("@mctry", cbMailCountry.Text)
                dbCon.addParameter("@mctrycd", cbMailCountry.SelectedValue)
                dbCon.addParameter("@tax", cbTax.SelectedValue)
                dbCon.addParameter("@npwp", tbNpwp.Text)
                dbCon.addParameter("@taddr", tbTaxAddr.Text)
                dbCon.addParameter("@tct", cbTaxCity.Text)
                dbCon.addParameter("@tctcd", cbTaxCity.SelectedValue)
                dbCon.addParameter("@ts", cbTaxState.Text)
                dbCon.addParameter("@tsc", cbTaxState.SelectedValue)
                dbCon.addParameter("@tctry", cbTaxCountry.Text)
                dbCon.addParameter("@tctrycd", cbTaxCountry.SelectedValue)
                dbCon.addParameter("@top", tbTOP.Value)
                dbCon.addParameter("@spc", cbSales.SelectedValue)
                dbCon.addParameter("@crlmt", tbCreditLimit.Value)

                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@vc", tbCode.Text)
            Else
                dbCon.SQL("insert into customers(company_code, customer_code, customer_name, customer_type, contacts, mailing_addr,
                        mailing_city, mailing_city_code, mailing_state, mailing_state_code, mailing_country, mailing_country_code,
                        tax, npwp, tax_addr, tax_city, tax_city_code, tax_state, tax_state_code, tax_country, tax_country_code, [top],
                        sales_person_code, credit_limit) 
                        values(@cc, @vc, @vn, @vt, @con, @maddr, @mct, @mctcd, @ms, @msc, @mctry, @mctrycd, 
                        @tax, @npwp, @taddr, @tct, @tctcd, @ts, @tsc, @tctry, @tctrycd, @top, @spc, @crlmt)")
                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@vc", tbCode.Text)
                dbCon.addParameter("@vn", tbName.Text)
                dbCon.addParameter("@vt", cbType.SelectedValue)
                dbCon.addParameter("@con", json.Serialize(contacts))
                dbCon.addParameter("@maddr", tbMailAddr.Text)
                dbCon.addParameter("@mct", cbMailCity.Text)
                dbCon.addParameter("@mctcd", cbMailCity.SelectedValue)
                dbCon.addParameter("@ms", cbMailState.Text)
                dbCon.addParameter("@msc", cbMailState.SelectedValue)
                dbCon.addParameter("@mctry", cbMailCountry.Text)
                dbCon.addParameter("@mctrycd", cbMailCountry.SelectedValue)
                dbCon.addParameter("@tax", cbTax.SelectedValue)
                dbCon.addParameter("@npwp", tbNpwp.Text)
                dbCon.addParameter("@taddr", tbTaxAddr.Text)
                dbCon.addParameter("@tct", cbTaxCity.Text)
                dbCon.addParameter("@tctcd", cbTaxCity.SelectedValue)
                dbCon.addParameter("@ts", cbTaxState.Text)
                dbCon.addParameter("@tsc", cbTaxState.SelectedValue)
                dbCon.addParameter("@tctry", cbTaxCountry.Text)
                dbCon.addParameter("@tctrycd", cbTaxCountry.SelectedValue)
                dbCon.addParameter("@top", tbTOP.Value)
                dbCon.addParameter("@spc", cbSales.SelectedValue)
                dbCon.addParameter("@crlmt", tbCreditLimit.Value)
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

    Private Sub deleteCust(code As String)
        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("update customers set del = 1 where company_code = @cc and customer_code = @vc")
            dbCon.addParameter("@cc", corp)
            dbCon.addParameter("@vc", code)
            dbCon.execute()
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub CustomerMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim btn As ERPModules.buttonState

        tbCreditLimit.Maximum = Decimal.MaxValue

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

        tbCreditLimit.Maximum = Decimal.MaxValue

        Application.DoEvents()

        loadTypes()
        loadTaxes()
        loadCountries()
        loadStates()
        loadSalesPersons()

        lstCust.VirtualMode = True
        loadCusts()

        doEvents = True
    End Sub

    Private Sub lstCust_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstCust.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = custSearch(e.ItemIndex)
        e.Item = New ListViewItem(row("customer_code").ToString)
        e.Item.SubItems.Add(row("customer_name"))
        e.Item.SubItems.Add(If(row("customer_type"), ""))
        e.Item.SubItems.Add(row("mailing_city"))
        e.Item.SubItems.Add(row("mailing_addr"))
        e.Item.SubItems.Add(row("mailing_city_code"))
        e.Item.SubItems.Add(row("mailing_state_code"))
        e.Item.SubItems.Add(row("mailing_country_code"))
        e.Item.SubItems.Add(row("tax"))
        e.Item.SubItems.Add(row("npwp"))
    End Sub

    Private Sub lstCust_DoubleClick(sender As Object, e As EventArgs) Handles lstCust.DoubleClick
        EditForm()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim item As ListViewItem

        If lstCust.SelectedIndices.Count = 1 Then
            item = lstCust.Items(lstCust.SelectedIndices(0))
            deleteCust(item.Text)
            loadCusts()
        End If
    End Sub

    Private Sub cbMailState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMailState.SelectedIndexChanged
        If Not doEvents Then Exit Sub

        If Not IsNothing(cbMailState.SelectedValue) Then
            loadCities(cbMailCity, CStr(cbMailState.SelectedValue))
        Else
            loadCities(cbMailCity, "")
        End If
    End Sub

    Private Sub cbTaxState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTaxState.SelectedIndexChanged
        If Not doEvents Then Exit Sub

        If Not IsNothing(cbTaxCity.SelectedValue) Then
            loadCities(cbTaxCity, cbTaxState.SelectedValue)
        Else
            loadCities(cbTaxCity, "")
        End If
    End Sub

    Private Function searchCust(lst As List(Of Dictionary(Of String, Object)), val As String) As IQueryable(Of Dictionary(Of String, Object))
        searchCust = lst.AsQueryable

        Dim filter As Expressions.Expression(Of Func(Of Dictionary(Of String, Object), Boolean)) =
            Function(i As Dictionary(Of String, Object)) i.Where(Function(x) x.Value.ToString().IndexOf(val, 0, StringComparison.CurrentCultureIgnoreCase) > -1).Count > 0

        If Not String.IsNullOrEmpty(val) Then searchCust = Queryable.Where(searchCust, filter)
    End Function

    Private Sub tbSearch_TextChanged(sender As Object, e As EventArgs) Handles tbSearch.TextChanged
        Dim tmp As IQueryable(Of Dictionary(Of String, Object))
        tmp = searchCust(custBuffer, tbSearch.Text)
        If tmp IsNot Nothing Then
            custSearch = tmp.ToList()
        Else
            custSearch.Clear()
        End If
        lstCust.VirtualListSize = custSearch.Count
    End Sub

    Private Sub lstCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCust.SelectedIndexChanged

    End Sub
End Class