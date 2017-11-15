Public Class ScaleClass

    Private dataLength As Integer
    Private startChar As Char
    Private endChar As Char
    Private startPos As Integer
    Private endPos As Integer
    Private precision As Integer = 0
    Private unitRatio As Decimal = 1
    Private buff As String
    Private lastResult As Decimal = 0
    Private lastBuffLength As Decimal = 0
    Private nodata As Integer = 0

    Public Sub New(dataLength As Integer, startChar As Char, endChar As Char, startPos As Integer, endPos As Integer, Optional starter As String = "")
        Me.dataLength = dataLength
        Me.startChar = startChar
        Me.endChar = endChar
        Me.startPos = startPos
        Me.endPos = endPos
        buff = starter
    End Sub

    Public Sub New(dataLength As Integer, startChar As Char, endChar As Char, startPos As Integer, endPos As Integer, precision As Integer)
        Me.dataLength = dataLength
        Me.startChar = startChar
        Me.endChar = endChar
        Me.startPos = startPos
        Me.endPos = endPos
        Me.precision = precision
    End Sub

    Public Sub New(dataLength As Integer, startChar As Char, endChar As Char, startPos As Integer, endPos As Integer, unitRatio As Decimal)
        Me.dataLength = dataLength
        Me.startChar = startChar
        Me.endChar = endChar
        Me.startPos = startPos
        Me.endPos = endPos
        Me.unitRatio = unitRatio
    End Sub

    Public Sub New(dataLength As Integer, startChar As Char, endChar As Char, startPos As Integer, endPos As Integer, unitRatio As Decimal, precision As Integer)
        Me.dataLength = dataLength
        Me.startChar = startChar
        Me.endChar = endChar
        Me.startPos = startPos
        Me.endPos = endPos
        Me.unitRatio = unitRatio
        Me.precision = precision
    End Sub

    'Public Sub reInit(dataLength As Integer, startChar As Char, endChar As Char, startPos As Integer, endPos As Integer)
    '    Me.dataLength = dataLength
    '    Me.startChar = startChar
    '    Me.endChar = endChar
    '    Me.startPos = startPos
    '    Me.endPos = endPos
    '    buff = ""
    'End Sub

    Public Sub setPrecision(precision As Integer)
        Me.precision = precision
    End Sub

    Public Sub setRatio(ratio As Integer)
        unitRatio = ratio
    End Sub

    Public Sub appendData(data As String)
        buff += data
    End Sub

    Public Function getValue() As String
        Dim data As Decimal
        Dim tmp As String = ""
        Dim s As Integer, e As Integer, l As Integer
        Dim offset As Integer = 0

        If buff.Length > dataLength * 2 Then
            buff = Right(buff, dataLength * 2)
        End If

        If lastBuffLength = buff.Length Then
            nodata += 1
        Else
            nodata = 0
        End If
        If nodata > 10 And lastBuffLength > 0 Then
            buff = ""
            lastBuffLength = 0
            Return Nothing
        ElseIf buff.Length < dataLength Then
            If buff.Length = 0 Then
                Return Nothing
            ElseIf buff.Length < endPos Then
                If precision = 0 Then
                    Return String.Format("{0:#,0}", lastResult)
                Else
                    Return String.Format("{0:#,0." + StrDup(precision, "0") + "}", lastResult)
                End If
            ElseIf buff.Length + 1 = dataLength Then
                buff = buff + vbLf
            ElseIf buff.Length + 2 = dataLength Then
                buff = buff + vbCrLf
            End If
        End If

        lastBuffLength = buff.Length

        If startChar <> vbNullChar And endChar <> vbNullChar Then
            s = buff.IndexOf(startChar)
            e = buff.LastIndexOf(endChar)
            If dataLength = e - s + 1 Then
                tmp = buff.Substring(s, e - s + 1)
                l = e + 1
            ElseIf dataLength * 2 = e - s + 1 Then
                s = buff.LastIndexOf(startChar)
                tmp = buff.Substring(s, e - s + 1)
                l = e + 1
            Else
                Return Nothing
            End If
        ElseIf buff.LastIndexOf(vbCrLf) >= 0 Then
            e = buff.LastIndexOf(vbCrLf)
            If e = 0 And buff.Length > endPos + 2 Then
                tmp = buff.Substring(2, endPos + 2)
                l = 2
                offset = 0
            ElseIf e - dataLength + 2 >= 0 Then
                tmp = buff.Substring(e - dataLength + 2, dataLength)
                l = e + 3
                offset = 0
            ElseIf e - dataLength + 2 + startPos >= 0 Then
                l = e + 3
                offset = e - dataLength + 2
                tmp = buff.Substring(0, dataLength + offset)
            Else
                Return Nothing
            End If
        End If

        If tmp = "" Then
            Return Nothing
        End If

        If l > Len(buff) Then
            If l - dataLength - 1 >= 0 Then
                buff = buff.Substring(l - dataLength - 1)
            Else
                buff = ""
            End If
        ElseIf l + dataLength > Len(buff) Then
        Else
            buff = buff.Substring(l)
        End If
        Try
            data = Decimal.Parse(Mid(tmp, startPos + offset, offset + endPos - startPos + 1)) * unitRatio
            lastResult = data
        Catch ex As Exception
        End Try
        If precision = 0 Then
            Return String.Format("{0:#,0}", data)
        Else
            Return String.Format("{0:#,0." + StrDup(precision, "0") + "}", data)
        End If

    End Function

End Class
