Imports DB
Imports System.Windows.Forms
Imports System.Web.Script.Serialization

Public Class buttonState
    Public visible As Boolean
    Public enabled As Boolean
End Class

'Public Interface IERPModule

'    Property dbCon As DBConn
'    Property user As String
'    Property role As String
'    Property corp As String
'    Property area As String

'    ReadOnly Property toolMenu As Dictionary(Of String, ToolStripItem)
'    ReadOnly Property toolButton As Dictionary(Of String, buttonState)
'    ReadOnly Property privs As Dictionary(Of String, Integer)

'    Sub Save()
'    Sub Print()

'    Function isPrived(task As String, lvl As Integer) As Boolean

'End Interface

Public Class ERPModule : Inherits UserControl
    Private _dbCon As DBConn
    Private _corp As String
    Private _area As String
    Private _user As String
    Private _role As String

    Private _menu As New Dictionary(Of String, ToolStripItem)
    Private _tool As New Dictionary(Of String, ERPModules.buttonState)
    Private _privs As New Dictionary(Of String, Integer)

    Protected _parent As Object

    Protected json As New JavaScriptSerializer
    Protected updateData As Boolean = False

    Protected numformat As String = "{0:#,0.##}"

    Public canOffline As Boolean = False

    Public Property dbCon As DBConn 'Implements IERPModule.dbCon
        Get
            Return _dbCon
        End Get
        Set(value As DBConn)
            _dbCon = value
        End Set
    End Property

    Public Property corp As String 'Implements IERPModule.corp
        Get
            Return _corp
        End Get
        Set(value As String)
            _corp = value
        End Set
    End Property

    Public Property area As String 'Implements IERPModule.area
        Get
            Return _area
        End Get
        Set(value As String)
            _area = value
        End Set
    End Property

    Public Property user As String 'Implements IERPModule.user
        Get
            Return _user
        End Get
        Set(value As String)
            _user = value
        End Set
    End Property

    Public Property role As String 'Implements IERPModule.role
        Get
            Return _role
        End Get
        Set(value As String)
            _role = value
        End Set
    End Property

    Public ReadOnly Property toolButton As Dictionary(Of String, buttonState) 'Implements IERPModule.toolButton
        Get
            Return _tool
        End Get
    End Property

    Public ReadOnly Property toolMenu As Dictionary(Of String, ToolStripItem) 'Implements IERPModule.toolMenu
        Get
            Return _menu
        End Get
    End Property

    Public ReadOnly Property privs As Dictionary(Of String, Integer) 'Implements IERPModule.privs
        Get
            Return _privs
        End Get
    End Property

    Public Sub setPrivs(p As Dictionary(Of String, Integer))
        _privs = p
    End Sub

    Public Function isPrived(task As String, lvl As Integer) As Boolean 'Implements IERPModule.isPrived
        If _privs.ContainsKey(task) Then
            If _privs(task) >= lvl Then
                Return True
            Else
                Return False
            End If
        End If
        Return False
    End Function

    Public Overridable Sub Print() 'Implements IERPModule.Print
    End Sub

    Public Overridable Sub Save() 'Implements IERPModule.Save
    End Sub
End Class