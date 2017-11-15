Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class DBConn : Implements IDisposable

    Private serverType As String

    Private con As Object
    Private cmd As Object
    Private adp As Object
    Private conStr As String

    Private result As IAsyncResult

    Public host As String

    Protected disposed As Boolean = False

    Public Sub init(server As String, host As String, dataBase As String, user As String, pwd As String)
        serverType = server
        Me.host = host
        Select Case serverType
            Case "MSSQL"
                conStr = "Server=" + host + ";Database=" + dataBase + ";User=" + user + ";Pwd=" + pwd + ";MultipleActiveResultSets=True"
                con = New SqlConnection()
            Case "MySQL", "MariaDB"
                conStr = "Server=" + host + ";Database=" + dataBase + ";Uid=" + user + ";Pwd=" + pwd
                con = New MySqlConnection()
        End Select
        con.ConnectionString = conStr
    End Sub

    Public Sub init(serverType As String, conStr As String)
        Me.serverType = serverType
        Me.conStr = conStr
        Select Case serverType
            Case "MSSQL"
                con = New SqlConnection()
            Case "MySQL", "MariaDB"
                con = New MySqlConnection()
        End Select
        con.ConnectionString = conStr
    End Sub

    Public Function open() As Boolean
        Try
            con.Open()
        Catch ex As Exception
            'MsgBox(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Sub close()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Public Sub SQL(str As String, Optional cmdObj As Object = Nothing)
        If IsNothing(cmdObj) Then
            Select Case serverType
                Case "MSSQL"
                    cmd = New SqlCommand(str, con)
                Case "MySQL", "MariaDB"
                    cmd = New MySqlCommand(str, con)
            End Select
        Else
            If IsNothing(cmdObj.Connection) Then cmdObj.Connection = con
            cmdObj.CommandText = str
        End If
    End Sub

    Public Sub SQL(str As String, cmdType As CommandType)
        Select Case serverType
            Case "MSSQL"
                cmd = New SqlCommand(str, con)
            Case "MySQL", "MariaDB"
                cmd = New MySqlCommand(str, con)
        End Select
        cmd.CommandType = cmdType
    End Sub

    Public Function SQLReader(str As String) As Object
        Select Case serverType
            Case "MSSQL"
                Return New SqlCommand(str, con)
            Case "MySQL", "MariaDB"
                Dim tcon As New MySqlConnection
                tcon.ConnectionString = conStr
                tcon.Open()
                Return New MySqlCommand(str, tcon)
        End Select
        Return Nothing
    End Function

    Public Sub addOutputParam(name As String, size As Integer, type As Object)
        cmd.Parameters.Add(name, type, size)
        Select Case serverType
            Case "MSSQL"
                DirectCast(cmd, SqlCommand).Parameters(name).Direction = ParameterDirection.Output
            Case "MySQL", "MariaDB"
                DirectCast(cmd, MySqlCommand).Parameters(name).Direction = ParameterDirection.Output
        End Select
    End Sub

    Public Sub addParameter(name As String, val As Object)
        cmd.Parameters.AddWithValue(name, val)
    End Sub

    Public Sub addParameter(name As String, val As Object, cmdRdr As Object)
        cmdRdr.Parameters.AddWithValue(name, val)
    End Sub

    Public Sub clearParameter()
        cmd.Parameters.Clear()
    End Sub

    Public Sub clearParameter(cmdRdr As Object)
        cmdRdr.Parameters.Clear()
    End Sub

    Public Sub fillData(ByRef ds As DataSet, Optional cmdObj As Object = Nothing)
        If IsNothing(cmdObj) Then
            Select Case serverType
                Case "MSSQL"
                    adp = New SqlDataAdapter(cmd)
                Case "MySQL", "MariaDB"
                    adp = New MySqlDataAdapter(cmd)
            End Select
        Else
            Select Case serverType
                Case "MSSQL"
                    adp = New SqlDataAdapter(cmdObj)
                Case "MySQL", "MariaDB"
                    adp = New MySqlDataAdapter(cmdObj)
            End Select
        End If
        adp.Fill(ds)
        adp.Dispose()
        cmd.Dispose()
    End Sub

    Public Sub fillTable(ByRef dt As DataTable, Optional cmdObj As Object = Nothing)
        If IsNothing(cmdObj) Then
            Select Case serverType
                Case "MSSQL"
                    adp = New SqlDataAdapter(cmd)
                Case "MySQL", "MariaDB"
                    adp = New MySqlDataAdapter(cmd)
            End Select
        Else
            Select Case serverType
                Case "MSSQL"
                    adp = New SqlDataAdapter(cmdObj)
                Case "MySQL", "MariaDB"
                    adp = New MySqlDataAdapter(cmdObj)
            End Select
        End If
        adp.Fill(dt)
        adp.Dispose()
        cmd.Dispose()
    End Sub

    Public Function getData() As DataSet
        Dim ds As New DataSet
        Try
            fillData(ds)
        Catch e As Exception
            MsgBox(e.Message)
        End Try
        Return ds
    End Function

    'Public Function isReading() As Boolean
    '    If IsNothing(rdr) Then
    '        Return Not rdr.IsClosed
    '    Else
    '        Return False
    '    End If
    '    'Select Case serverType
    '    '    Case "MSSQL"
    '    '        If IsNothing(sqlRdr) Then
    '    '            Return Not sqlRdr.IsClosed
    '    '        Else
    '    '            Return False
    '    '        End If
    '    '    Case "MSSQL"
    '    '        If IsNothing(myRdr) Then
    '    '            Return Not myRdr.IsClosed
    '    '        Else
    '    '            Return False
    '    '        End If
    '    'End Select
    '    'Return False
    'End Function

    Public Function beginRead(cmdRdr As Object) As Object
        result = cmdRdr.BeginExecuteReader()
        Return cmdRdr.EndExecuteReader(result)
    End Function

    'Public Sub beginRead()
    '    result = cmd.BeginExecuteReader()
    '    rdr = cmd.EndExecuteReader(result)
    '    'Select Case serverType
    '    '    Case "MSSQL"
    '    '        'If Not IsNothing(sqlRdr) Then
    '    '        '    While Not sqlRdr.IsClosed
    '    '        '        Threading.Thread.Sleep(100)
    '    '        '    End While
    '    '        'End If

    '    '        result = sqlCmd.BeginExecuteReader
    '    '        sqlRdr = sqlCmd.EndExecuteReader(result)
    '    '    Case "MySQL", "MariaDB"
    '    '        'If Not IsNothing(myRdr) Then
    '    '        '    While Not myRdr.IsClosed
    '    '        '        Threading.Thread.Sleep(100)
    '    '        '    End While
    '    '        'End If

    '    '        result = myCmd.BeginExecuteReader
    '    '        myRdr = myCmd.EndExecuteReader(result)
    '    'End Select
    'End Sub

    Public Sub endRead(rdr As Object)
        rdr.Close()
    End Sub

    Public Function doRead(rdr As Object)
        Return rdr.Read()
    End Function

    Public Function getRow(rdr As Object) As Dictionary(Of String, Object)
        Dim row As New Dictionary(Of String, Object)
        Dim obj As Object

        For i = 0 To rdr.FieldCount - 1
            obj = rdr.GetValue(i)
            If IsDBNull(obj) Then
                obj = ""
            End If
            row.Add(rdr.GetName(i), obj)
        Next
        Return row
    End Function

    Public Sub execute()
        cmd.ExecuteNonQuery()
        If cmd.CommandType <> CommandType.StoredProcedure Then
            cmd.Dispose()
        End If
    End Sub

    Public Function scalar()
        Dim result = Nothing

        result = cmd.ExecuteScalar()
        cmd.Dispose()

        Return result
    End Function

    Public Function state() As ConnectionState
        Return con.State
    End Function

    Public Function getConnectionString() As String
        Return conStr
    End Function

    Public Function getServerType() As String
        Return serverType
    End Function

    Public Sub setOnStateChange(func)
        Select Case serverType
            Case "MSSQL"
                AddHandler DirectCast(con, SqlConnection).StateChange, func
            Case "MySQL", "MariaDB"
                AddHandler DirectCast(con, MySqlConnection).StateChange, func
        End Select
    End Sub

    Public Function getParamOutput(name As String) As Object
        Select Case serverType
            Case "MSSQL"
                Return DirectCast(cmd, SqlCommand).Parameters(name).Value
            Case "MySQL", "MariaDB"
                Return DirectCast(cmd, MySqlCommand).Parameters(name).Value
        End Select
        Return Nothing
    End Function

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not disposed Then
            If disposing Then
                ' Insert code to free managed resources.
                Try
                    adp.Dispose()
                    cmd.Dispose()
                    con.Dispose()
                Catch ex As Exception
                End Try
            End If
            ' Insert code to free unmanaged resources.
        End If
        disposed = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Function hashString(str As String) As String
        Dim c As New Crypto.Crypto
        Return c.HashString(str)
    End Function

    Public Function verify(str As String, hash As String) As Boolean
        Dim c As New Crypto.Crypto
        Return c.Verify(str, hash)
    End Function

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub

End Class
