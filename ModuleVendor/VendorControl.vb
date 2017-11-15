Imports System.Windows.Forms
Imports System.Web.Script.Serialization
Imports DB
Imports ERPModules

Public Class VendorControl : Inherits ERPModule

    'Private dbCon As New DBConn
    'Private _corp As String
    'Private _role As String
    'Private _user As String
    'Private _area As String

    Private doEvents As Boolean = False

    'Private json As New JavaScriptSerializer
    'Private updateData As Boolean = False

    'Private toolMenu As New Dictionary(Of String, ToolStripItem)
    'Private toolButton As New Dictionary(Of String, ERPModules.buttonState)

    Private venBuffer As New List(Of Dictionary(Of String, Object))
    Private venSearch As New List(Of Dictionary(Of String, Object))

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
    '    'Set(value As Dictionary(Of String, ERPModules.buttonState))
    '    '    toolButton = value
    '    'End Set
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
        If tcMain.SelectedIndex = 1 Then
            saveVendor(updateData)
            loadVendors()
            tcMain.SelectedIndex = 0
        End If
    End Sub

    Public Overloads Sub Dispose()
        Try
            json = Nothing
            toolMenu.Clear()
            venBuffer.Clear()
            venSearch.Clear()

            MyBase.Dispose()
        Catch
        End Try
    End Sub

    'Public Function isPrived(task As String, lvl As Integer) As Boolean Implements IERPModule.isPrived
    '    Return True
    'End Function

    Private Sub loadCities(target As ComboBox, state As String)
        If dbCon.state() = ConnectionState.Open Then
            Dim dt As New DataTable

            dbCon.SQL("select * from cities where state_id = @si")
            dbCon.addParameter("@si", state)
            dbCon.fillTable(dt)

            target.DataSource = New BindingSource(dt, Nothing)
            target.DisplayMember = "city"
            target.ValueMember = "id"
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
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub loadTypes()
        If dbCon.state() = ConnectionState.Open Then
            Dim dt As New DataTable

            dbCon.SQL("select * from vendor_types where company_code = @cc and del = 0")
            dbCon.addParameter("@cc", corp)
            dbCon.fillTable(dt)

            cbType.DataSource = New BindingSource(dt, Nothing)
            cbType.DisplayMember = "vendor_type"
            cbType.ValueMember = "vendor_type_code"
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

    Private Sub loadVendors()
        Dim rdr As Object

        If dbCon.state() = ConnectionState.Open Then
            Dim cmdRdr As Object = dbCon.SQLReader("select * from vendors where company_code = @cc and del = 0")
            dbCon.addParameter("@cc", corp, cmdRdr)

            rdr = dbCon.beginRead(cmdRdr)
            venBuffer.Clear()
            venSearch.Clear()

            While dbCon.doRead(rdr)
                venBuffer.Add(dbCon.getRow(rdr))
            End While
            dbCon.endRead(rdr)
            venSearch.AddRange(venBuffer)
            lstVen.VirtualListSize = venSearch.Count()
        Else
            MsgBox("Connection error, please check your network connection and try again")
        End If
    End Sub

    Private Sub saveVendor(update As Boolean)
        If dbCon.state() = ConnectionState.Open Then
            Dim contacts As New Dictionary(Of String, String)

            contacts.Add("email", tbEmail.Text)
            contacts.Add("phone", tbPhone.Text)
            If update Then
                dbCon.SQL("update vendors set vendor_name = @vn, vendor_type = @vt, contacts = @con, 
                            mailing_addr = @maddr, mailing_city = @mct, mailing_city_code = @mctcd, mailing_state = @ms, 
                            mailing_state_code = @msc, mailing_country = @mctry, mailing_country_code = @mctrycd,
                            tax = @tax, npwp = @npwp,
                            tax_addr = @taddr, tax_city = @tct, tax_city_code = @tctcd, tax_state = @ts, 
                            tax_state_code = @tsc, tax_country = @tctry, tax_country_code = @tctrycd
                            where company_code = @cc and vendor_code = @vc")
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

                dbCon.addParameter("@cc", corp)
                dbCon.addParameter("@vc", tbCode.Text)
            Else
                dbCon.SQL("insert into vendors(company_code, vendor_code, vendor_name, vendor_type, contacts, mailing_addr,
                        mailing_city, mailing_city_code, mailing_state, mailing_state_code, mailing_country, mailing_country_code,
                        tax, npwp, tax_addr, tax_city, tax_city_code, tax_state, tax_state_code, tax_country, tax_country_code) 
                        values(@cc, @vc, @vn, @vt, @con, @maddr, @mct, @mctcd, @ms, @msc, @mctry, @mctrycd, 
                        @tax, @npwp, @taddr, @tct, @tctcd, @ts, @tsc, @tctry, @tctrycd)")
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
                dbCon.addParameter("@taddr", tbMailAddr.Text)
                dbCon.addParameter("@tct", cbMailCity.Text)
                dbCon.addParameter("@tctcd", cbMailCity.SelectedValue)
                dbCon.addParameter("@ts", cbMailState.Text)
                dbCon.addParameter("@tsc", cbMailState.SelectedValue)
                dbCon.addParameter("@tctry", cbMailCountry.Text)
                dbCon.addParameter("@tctrycd", cbMailCountry.SelectedValue)
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

    Private Sub deleteVendor(code As String)
        If dbCon.state() = ConnectionState.Open Then
            dbCon.SQL("update vendors set del = 1 where company_code = @cc and vendor_code = @vc")
            dbCon.addParameter("@cc", corp)
            dbCon.addParameter("@vc", code)
            dbCon.execute()
        Else
            MsgBox("Connection error, please check your network connection and try again")
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

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        clearForm()

        tcMain.SelectedIndex = 1
    End Sub

    Private Sub VendorMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim btn As ERPModules.buttonState

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

        loadTypes()
        loadTaxes()
        loadCountries()
        loadStates()

        lstVen.VirtualMode = True
        loadVendors()

        doEvents = True
    End Sub

    Private Sub lstVen_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles lstVen.RetrieveVirtualItem
        Dim row As Dictionary(Of String, Object)

        row = venSearch(e.ItemIndex)
        e.Item = New ListViewItem(row("vendor_code").ToString)
        e.Item.SubItems.Add(row("vendor_name"))
        e.Item.SubItems.Add(row("vendor_type"))
        e.Item.SubItems.Add(row("mailing_city"))
        e.Item.SubItems.Add(row("mailing_addr"))
    End Sub

    Private Sub lstVen_DoubleClick(sender As Object, e As EventArgs) Handles lstVen.DoubleClick
        btnEdit.PerformClick()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim item As ListViewItem
        Dim row As Dictionary(Of String, Object)

        If lstVen.SelectedIndices.Count = 1 Then
            Dim contacts As Dictionary(Of String, String)

            updateData = True

            item = lstVen.Items(lstVen.SelectedIndices(0))

            row = venSearch(item.Index)

            Try
                tbCode.Text = row("vendor_code")
                tbName.Text = row("vendor_name")
                cbType.SelectedValue = row("vendor_type")
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

                loadCities(cbMailCity, row("mailing_state_code"))
                If row("mailing_city_code") = "" Then
                    cbMailCity.SelectedIndex = -1
                Else
                    cbMailCity.SelectedValue = row("mailing_city_code")
                End If

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
                If row("tax_city_code") = "" Then
                    cbTaxCity.SelectedIndex = -1
                Else
                    cbTaxCity.SelectedValue = row("tax_city_code")
                End If
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

            tcMain.SelectedIndex = 1
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim item As ListViewItem

        If lstVen.SelectedIndices.Count = 1 Then
            item = lstVen.Items(lstVen.SelectedIndices(0))
            deleteVendor(item.Text)
            loadVendors()
        End If
    End Sub

    Private Function searchVendor(lst As List(Of Dictionary(Of String, Object)), val As String) As IQueryable(Of Dictionary(Of String, Object))
        searchVendor = lst.AsQueryable

        Dim filter As Expressions.Expression(Of Func(Of Dictionary(Of String, Object), Boolean)) =
            Function(i As Dictionary(Of String, Object)) i.Where(Function(x) x.Value.ToString().IndexOf(val, 0, StringComparison.CurrentCultureIgnoreCase) > -1).Count > 0

        If Not String.IsNullOrEmpty(val) Then searchVendor = Queryable.Where(searchVendor, filter)
    End Function

    Private Sub tbSearch_TextChanged(sender As Object, e As EventArgs) Handles tbSearch.TextChanged
        Dim tmp As IQueryable(Of Dictionary(Of String, Object))
        tmp = searchVendor(venBuffer, tbSearch.Text)
        If tmp IsNot Nothing Then
            venSearch = tmp.ToList()
        Else
            venSearch.Clear()
        End If
        lstVen.VirtualListSize = venSearch.Count
    End Sub

    Private Sub cbMailState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMailState.SelectedIndexChanged
        If Not doEvents Then Exit Sub

        If cbMailState.SelectedIndex >= 0 Then
            loadCities(cbMailCity, cbMailState.SelectedValue)
        Else
            loadCities(cbMailCity, "")
        End If
    End Sub

    Private Sub cbTaxState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTaxState.SelectedIndexChanged
        If Not doEvents Then Exit Sub

        If cbTaxState.SelectedIndex >= 0 Then
            loadCities(cbTaxCity, cbTaxState.SelectedValue)
        Else
            loadCities(cbTaxCity, "")
        End If
    End Sub

    Private Sub VendorMain_RightToLeftChanged(sender As Object, e As EventArgs) Handles Me.RightToLeftChanged

    End Sub
End Class