Imports System.Windows.Forms
'Imports System.IO.Ports
Imports System.Data.SQLite

Public Class ScaleSettings

    Public active_scale As String = ""
    Public uc As Object

    Private Sub getScales()
        'Dim p As ScaleMain
        Dim ds As New DataSet
        Dim item As ListViewItem
        'p = Owner

        If Not IsNothing(uc) Then
            With uc
                'If .dbCon.state() = ConnectionState.Open Then
                If .localCon.State = ConnectionState.Open Then
                    Try
                        Dim cmd As SQLiteCommand
                        Dim adp As SQLiteDataAdapter

                        cmd = .localCon.CreateCommand()
                        cmd.CommandText = "SELECT * FROM `scales` WHERE `company_code` = ?"
                        cmd.Parameters.AddWithValue("company_code", .corp)
                        adp = New SQLiteDataAdapter(cmd)
                        adp.Fill(ds)

                        adp.Dispose()
                        cmd.Dispose()

                        '.dbCon.SQL("SELECT * FROM scale WHERE company_code = @c")
                        '.dbCon.addParameter("@c", .corp)

                        '.dbCon.fillData(ds)
                    Catch ex As Exception
                        MsgBox("Database error, please wait for a while and try again." + vbCrLf + "(SS-GS-02)")
                        Exit Sub
                    End Try
                Else
                    MsgBox("Database error, please wait for a while and try again." + vbCrLf + "(SS-GS-01)")
                    Exit Sub
                End If
            End With
        Else
            'With p
            '    .dbCon.SQL("SELECT * FROM scale WHERE company_code = @c")
            '    .dbCon.addParameter("@c", .corp)

            '    .dbCon.fillData(ds)
            'End With
        End If

        ListView1.Items.Clear()

        For Each d In ds.Tables(0).Rows
            item = ListView1.Items.Add(d.Item("code"))
            item.SubItems.Add(d.Item("name"))
            item.SubItems.Add(d.Item("config"))
            item.SubItems.Add(d.Item("continuous"))
            item.SubItems.Add(d.Item("datalength"))
            item.SubItems.Add(d.Item("startchar"))
            item.SubItems.Add(d.Item("endchar"))
            item.SubItems.Add(d.Item("readstart"))
            item.SubItems.Add(d.Item("readend"))
            item.SubItems.Add(d.Item("precision"))
            item.SubItems.Add(d.Item("unitratio"))
            item.SubItems.Add(d.Item("id"))
        Next
    End Sub

    Private Function addScale() As Integer
        'Dim p As ScaleMain
        Dim conf As String

        'p = Owner
        conf = tbBaud.Text + ";" + cbParity.Text(0) + ";" + tbData.Text + ";" + tbStop.Text

        If Not IsNothing(uc) Then
            With uc
                'If .dbCon.state() = ConnectionState.Open Then
                If .localCon.State = ConnectionState.Open Then
                    Try
                        Dim cmd As SQLiteCommand
                        Dim id As Integer

                        cmd = .localCon.CreateCommand()
                        cmd.CommandText = "INSERT INTO `scales`(company_code, code, name, port, config, continuous, datalength, startchar, endchar, 
                                readstart, readend, precision, unitratio, last_update) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
                        cmd.Parameters.AddWithValue("company_code", .corp)
                        cmd.Parameters.AddWithValue("code", tbCode.Text)
                        cmd.Parameters.AddWithValue("name", tbName.Text)
                        cmd.Parameters.AddWithValue("port", cbPortList.Text)
                        cmd.Parameters.AddWithValue("config", conf)
                        cmd.Parameters.AddWithValue("continuous", chkContinuous.Checked)
                        cmd.Parameters.AddWithValue("datalength", tbLength.Text)
                        cmd.Parameters.AddWithValue("startchar", tbStartChar.Text)
                        cmd.Parameters.AddWithValue("endchar", tbEndChar.Text)
                        cmd.Parameters.AddWithValue("readstart", tbReadStart.Text)
                        cmd.Parameters.AddWithValue("readend", tbReadEnd.Text)
                        cmd.Parameters.AddWithValue("precision", Integer.Parse(tbPrecision.Text))
                        cmd.Parameters.AddWithValue("unitratio", Decimal.Parse(tbRatio.Text))
                        cmd.Parameters.AddWithValue("last_update", Now)
                        cmd.ExecuteNonQuery()

                        cmd.CommandText = "SELECT last_insert_rowid()"
                        id = cmd.ExecuteScalar()

                        cmd.Dispose()

                        Return id

                        '.dbCon.SQL("INSERT INTO scale(company_code, code, name, config, continuous, datalength, startchar, endchar, 
                        '        readstart, readend, precision, unitratio, created_by, created_at) VALUES(@cc, @c, @n, @cfg, @cont, @dl, @sc, @ec, @rs, @re, @p, @ur, @cb, @ca) 
                        '        SELECT SCOPE_IDENTITY()")
                        '.dbCon.addParameter("@cc", .corp)
                        '.dbCon.addParameter("@c", tbCode.Text)
                        '.dbCon.addParameter("@n", tbName.Text)
                        '.dbCon.addParameter("@cfg", conf)
                        '.dbCon.addParameter("@cont", chkContinuous.Checked)
                        '.dbCon.addParameter("@dl", tbLength.Text)
                        '.dbCon.addParameter("@sc", tbStartChar.Text)
                        '.dbCon.addParameter("@ec", tbEndChar.Text)
                        '.dbCon.addParameter("@rs", tbReadStart.Text)
                        '.dbCon.addParameter("@re", tbReadEnd.Text)
                        '.dbCon.addParameter("@p", Integer.Parse(tbPrecision.Text))
                        '.dbCon.addParameter("@ur", Decimal.Parse(tbRatio.Text))
                        '.dbCon.addParameter("@cb", .user)
                        '.dbCon.addParameter("@ca", Now)
                        'Return .dbCon.scalar()
                    Catch ex As Exception
                        MsgBox("Database error, please wait for a while and try again." + vbCrLf + "(SS-AS-02)")
                        Return -1
                    End Try
                Else
                    MsgBox("Database error, please wait for a while and try again." + vbCrLf + "(SS-AS-01)")
                    Return -1
                End If
            End With
        Else
            Return -1
            'With p
            '    .dbCon.SQL("INSERT INTO scale(company_code, code, name, config, continuous, datalength, startchar, endchar, 
            '                    readstart, readend, precision, created_by, created_at) VALUES(@cc, @c, @n, @cfg, @cont, @dl, @sc, @ec, @rs, @re, @p, @cb, @ca) 
            '                    SELECT SCOPE_IDENTITY()")
            '    .dbCon.addParameter("@cc", .corp)
            '    .dbCon.addParameter("@c", tbCode.Text)
            '    .dbCon.addParameter("@n", tbName.Text)
            '    .dbCon.addParameter("@cfg", conf)
            '    .dbCon.addParameter("@cont", chkContinuous.Checked)
            '    .dbCon.addParameter("@dl", tbLength.Text)
            '    .dbCon.addParameter("@sc", tbStartChar.Text)
            '    .dbCon.addParameter("@ec", tbEndChar.Text)
            '    .dbCon.addParameter("@rs", tbReadStart.Text)
            '    .dbCon.addParameter("@re", tbReadEnd.Text)
            '    .dbCon.addParameter("@p", Math.Round(Single.Parse(tbPrecision.Text), 2))
            '    .dbCon.addParameter("@cb", .user)
            '    .dbCon.addParameter("@ca", Now)
            '    Return .dbCon.scalar()
            'End With
        End If
    End Function

    Private Sub updateScale(id As Integer)
        'Dim p As ScaleMain
        Dim conf As String

        'p = Owner
        conf = tbBaud.Text + ";" + cbParity.Text(0) + ";" + tbData.Text + ";" + tbStop.Text

        If Not IsNothing(uc) Then
            With uc
                'If .dbCon.state() = ConnectionState.Open Then
                If .localCon.State = ConnectionState.Open Then
                    Try
                        Dim cmd As SQLiteCommand

                        cmd = .localCon.CreateCommand()
                        cmd.CommandText = "UPDATE `scales` SET code = ?, name = ?, port = ?, config = ?, continuous = ?, datalength = ?, startchar = ?, endchar = ?, 
                                readstart = ?, readend = ?, precision = ?, unitratio = ?, last_update = ? WHERE id = ?"
                        cmd.Parameters.AddWithValue("code", tbCode.Text)
                        cmd.Parameters.AddWithValue("name", tbName.Text)
                        cmd.Parameters.AddWithValue("port", cbPortList.Text)
                        cmd.Parameters.AddWithValue("config", conf)
                        cmd.Parameters.AddWithValue("continuous", chkContinuous.Checked)
                        cmd.Parameters.AddWithValue("datalength", tbLength.Text)
                        cmd.Parameters.AddWithValue("startchar", tbStartChar.Text)
                        cmd.Parameters.AddWithValue("endchar", tbEndChar.Text)
                        cmd.Parameters.AddWithValue("readstart", tbReadStart.Text)
                        cmd.Parameters.AddWithValue("readend", tbReadEnd.Text)
                        cmd.Parameters.AddWithValue("precision", Integer.Parse(tbPrecision.Text))
                        cmd.Parameters.AddWithValue("unitratio", Decimal.Parse(tbRatio.Text))
                        cmd.Parameters.AddWithValue("last_update", Now)
                        cmd.Parameters.AddWithValue("id", id)
                        cmd.ExecuteNonQuery()

                        cmd.Dispose()

                        '.dbCon.SQL("UPDATE scale SET code = @c, name = @n, config = @cfg, continuous = @cont, datalength = @dl, startchar = @sc, endchar = @ec, 
                        '        readstart = @rs, readend = @re, precision = @p, unitratio = @ur, updated_by = @ub, updated_at = @ua WHERE id = @id")
                        '.dbCon.addParameter("@c", tbCode.Text)
                        '.dbCon.addParameter("@n", tbName.Text)
                        '.dbCon.addParameter("@cfg", conf)
                        '.dbCon.addParameter("@cont", chkContinuous.Checked)
                        '.dbCon.addParameter("@dl", tbLength.Text)
                        '.dbCon.addParameter("@sc", tbStartChar.Text)
                        '.dbCon.addParameter("@ec", tbEndChar.Text)
                        '.dbCon.addParameter("@rs", tbReadStart.Text)
                        '.dbCon.addParameter("@re", tbReadEnd.Text)
                        '.dbCon.addParameter("@p", Integer.Parse(tbPrecision.Text))
                        '.dbCon.addParameter("@ur", Decimal.Parse(tbRatio.Text))
                        '.dbCon.addParameter("@ub", .user)
                        '.dbCon.addParameter("@ua", Now)
                        '.dbCon.addParameter("@id", id)
                        '.dbCon.execute()
                    Catch ex As Exception
                        MsgBox("Database error, please wait for a while and try again." + vbCrLf + "(SS-US-02)")
                        Exit Sub
                    End Try
                Else
                    MsgBox("Database error, please wait for a while and try again." + vbCrLf + "(SS-US-01)")
                    Exit Sub
                End If
            End With
        Else
            'With p
            '    .dbCon.SQL("UPDATE scale SET code = @c, name = @n, config = @cfg, continuous = @cont, datalength = @dl, startchar = @sc, endchar = @ec, 
            '                    readstart = @rs, readend = @re, precision = @p, updated_by = @ub, updated_at = @ua WHERE id = @id")
            '    .dbCon.addParameter("@c", tbCode.Text)
            '    .dbCon.addParameter("@n", tbName.Text)
            '    .dbCon.addParameter("@cfg", conf)
            '    .dbCon.addParameter("@cont", chkContinuous.Checked)
            '    .dbCon.addParameter("@dl", tbLength.Text)
            '    .dbCon.addParameter("@sc", tbStartChar.Text)
            '    .dbCon.addParameter("@ec", tbEndChar.Text)
            '    .dbCon.addParameter("@rs", tbReadStart.Text)
            '    .dbCon.addParameter("@re", tbReadEnd.Text)
            '    .dbCon.addParameter("@p", Math.Round(Single.Parse(tbPrecision.Text), 2))
            '    .dbCon.addParameter("@ub", .user)
            '    .dbCon.addParameter("@ua", Now)
            '    .dbCon.addParameter("@id", id)
            '    .dbCon.execute()
            'End With
        End If
    End Sub

    Private Sub deleteScale(id As Integer)
        'Dim p As ScaleMain
        Dim conf As String

        'p = Owner
        conf = tbBaud.Text + ";" + cbParity.Text(0) + ";" + tbData.Text + ";" + tbStop.Text

        If Not IsNothing(uc) Then
            With uc
                'If .dbCon.state() = ConnectionState.Open Then
                If .localCon.State = ConnectionState.Open Then
                    Try
                        Dim cmd As SQLiteCommand

                        cmd = .localCon.CreateCommand()
                        cmd.CommandText = "DELETE FROM `scales` WHERE `id` = ?"
                        cmd.Parameters.AddWithValue("id", id)
                        cmd.ExecuteNonQuery()

                        cmd.Dispose()
                        '.dbCon.SQL("DELETE FROM scale WHERE id = @id")
                        '.dbCon.addParameter("@id", id)
                        '.dbCon.execute()
                    Catch ex As Exception
                        MsgBox("Database error, please wait for a while and try again." + vbCrLf + "(SS-DS-02)")
                        Exit Sub
                    End Try
                Else
                    MsgBox("Database error, please wait for a while and try again." + vbCrLf + "(SS-DS-01)")
                    Exit Sub
                End If
            End With
        Else
            'With p
            '    .dbCon.SQL("DELETE FROM scale WHERE id = @id")
            '    .dbCon.addParameter("@id", id)
            '    .dbCon.execute()
            'End With
        End If
    End Sub

    Private Sub ScaleSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim selport As Integer = -1

        'Dim p As ScaleMain = Owner

        If Not IsNothing(uc) Then
            For Each sp As String In My.Computer.Ports.SerialPortNames
                If uc.sPort.PortName = sp Then
                    selport = cbPortList.Items.Count
                End If
                cbPortList.Items.Add(sp)
            Next
        Else
            'For Each sp As String In My.Computer.Ports.SerialPortNames
            '    If p.sPort.PortName = sp Then
            '        selport = cbPortList.Items.Count
            '    End If
            '    cbPortList.Items.Add(sp)
            'Next
        End If

        If selport >= 0 Then
            cbPortList.SelectedIndex = selport
        End If

        getScales()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim conf As String
        Dim item As ListViewItem
        Dim id As Integer

        conf = tbBaud.Text + ";" + cbParity.Text(0) + ";" + tbData.Text(0) + ";" + tbStop.Text

        If ListView1.SelectedItems.Count = 1 Then
            item = ListView1.SelectedItems.Item(0)
            id = Integer.Parse(item.SubItems(11).Text)

            updateScale(id)

            item.SubItems.Clear()
            item.Text = tbCode.Text
            item.SubItems.Add(tbName.Text)
            item.SubItems.Add(conf)
            item.SubItems.Add(If(chkContinuous.Checked, "1", "0"))
            item.SubItems.Add(tbLength.Text)
            item.SubItems.Add(tbStartChar.Text)
            item.SubItems.Add(tbEndChar.Text)
            item.SubItems.Add(tbReadStart.Text)
            item.SubItems.Add(tbReadEnd.Text)
            item.SubItems.Add(tbPrecision.Text)
            item.SubItems.Add(tbRatio.Text)
            item.SubItems.Add(id)
        Else
            id = addScale()

            item = ListView1.Items.Add(tbCode.Text)
            item.SubItems.Add(tbName.Text)
            item.SubItems.Add(conf)
            item.SubItems.Add(If(chkContinuous.Checked, "1", "0"))
            item.SubItems.Add(tbLength.Text)
            item.SubItems.Add(tbStartChar.Text)
            item.SubItems.Add(tbEndChar.Text)
            item.SubItems.Add(tbReadStart.Text)
            item.SubItems.Add(tbReadEnd.Text)
            item.SubItems.Add(tbPrecision.Text)
            item.SubItems.Add(tbRatio.Text)
            item.SubItems.Add(id)
        End If
    End Sub

    Private Sub ScaleSettings_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If active_scale <> "" Then
            For Each item As ListViewItem In ListView1.Items
                If item.Text = active_scale Then
                    ListView1.SelectedItems.Clear()
                    item.Selected = True
                    Exit For
                End If
            Next
        Else
            cbParity.SelectedIndex = 0
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Dim item As ListViewItem = ListView1.SelectedItems.Item(0)
        Dim conf() As String
        conf = item.SubItems(2).Text.Split(";")
        tbBaud.Text = conf(0)
        Select Case conf(1)
            Case "N"
                cbParity.SelectedIndex = 0
            Case "O"
                cbParity.SelectedIndex = 1
            Case "E"
                cbParity.SelectedIndex = 2
            Case "M"
                cbParity.SelectedIndex = 3
            Case "S"
                cbParity.SelectedIndex = 4
        End Select
        tbData.Text = conf(2)
        tbStop.Text = conf(3)

        tbCode.Text = item.Text
        tbName.Text = item.SubItems(1).Text
        chkContinuous.Checked = CBool(Integer.Parse(item.SubItems(3).Text))
        tbLength.Text = item.SubItems(4).Text
        tbStartChar.Text = item.SubItems(5).Text
        tbEndChar.Text = item.SubItems(6).Text
        tbReadStart.Text = item.SubItems(7).Text
        tbReadEnd.Text = item.SubItems(8).Text
        tbPrecision.Text = item.SubItems(9).Text
        tbRatio.Text = item.SubItems(10).Text

    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If cbPortList.SelectedIndex < 0 Then
            MsgBox("Port not selected")
            DialogResult = DialogResult.None
        End If
        If ListView1.SelectedItems.Count = 0 Then
            MsgBox("Configuration not selected")
            DialogResult = DialogResult.None
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim item As ListViewItem
        Dim id As Integer

        If ListView1.SelectedItems.Count = 1 Then
            item = ListView1.SelectedItems.Item(0)
            id = Integer.Parse(item.SubItems(11).Text)

            deleteScale(id)

            item.Remove()
        End If
    End Sub

    'Public Function GetAll(control As Control, type As Type) As IEnumerable(Of Control)
    '    Dim controls = control.Controls.Cast(Of Control)

    '    Return controls.SelectMany(Function(ctrl) GetAll(ctrl, type)).Concat(controls).Where(Function(c) c.GetType() = type)
    'End Function
End Class