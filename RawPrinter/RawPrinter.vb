Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports System.Security
Imports System.Text
Imports System.ComponentModel

Public Class Printer
    ' Structure and API declarions:
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Structure DOCINFOW
        <MarshalAs(UnmanagedType.LPWStr)> Public pDocName As String
        <MarshalAs(UnmanagedType.LPWStr)> Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPWStr)> Public pDataType As String
    End Structure

    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterW",
       SetLastError:=True, CharSet:=CharSet.Unicode,
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function OpenPrinterW(ByVal src As String, ByRef hPrinter As IntPtr, ByVal pd As Integer) As Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="ClosePrinter",
       SetLastError:=True, CharSet:=CharSet.Unicode,
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="StartDocPrinterW",
       SetLastError:=True, CharSet:=CharSet.Unicode,
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Integer, ByRef pDI As DOCINFOW) As Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="EndDocPrinter",
       SetLastError:=True, CharSet:=CharSet.Unicode,
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="StartPagePrinter",
       SetLastError:=True, CharSet:=CharSet.Unicode,
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="EndPagePrinter",
       SetLastError:=True, CharSet:=CharSet.Unicode,
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="WritePrinter",
       SetLastError:=True, CharSet:=CharSet.Unicode,
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Integer, ByRef dwWritten As Integer) As Boolean
    End Function

    ' SendBytesToPrinter()
    ' When the function is given a printer name and an unmanaged array of  
    ' bytes, the function sends those bytes to the print queue.
    ' Returns True on success or False on failure.
    Public Shared Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Integer) As Boolean
        Dim hPrinter As IntPtr       ' The printer handle.
        Dim dwError As Int32         ' Last error - in case there was trouble.
        Dim di As DOCINFOW = Nothing ' Describes your document (name, port, data type).
        Dim dwWritten As Int32       ' The number of bytes written by WritePrinter().
        Dim bSuccess As Boolean      ' Your success code.

        ' Set up the DOCINFO structure.
        With di
            .pDocName = "My Visual Basic .NET RAW Document"
            .pDataType = "RAW"
        End With
        ' Assume failure unless you specifically succeed.
        bSuccess = False
        Try
            If OpenPrinterW(szPrinterName, hPrinter, 0) Then
                If StartDocPrinter(hPrinter, 1, di) Then
                    If StartPagePrinter(hPrinter) Then
                        ' Write your printer-specific bytes to the printer.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                        EndPagePrinter(hPrinter)
                    End If
                    EndDocPrinter(hPrinter)
                End If
                ClosePrinter(hPrinter)
            End If
        Catch ex As Exception

        End Try
        ' If you did not succeed, GetLastError may give more information
        ' about why not.
        If bSuccess = False Then
            dwError = Marshal.GetLastWin32Error()
        End If
        Return bSuccess
    End Function ' SendBytesToPrinter()

    ' SendFileToPrinter()
    ' When the function is given a file name and a printer name, 
    ' the function reads the contents of the file and sends the
    ' contents to the printer.
    ' Presumes that the file contains printer-ready data.
    ' Shows how to use the SendBytesToPrinter function.
    ' Returns True on success or False on failure.
    Public Shared Function SendFileToPrinter(ByVal szPrinterName As String, ByVal szFileName As String) As Boolean
        ' Open the file.
        Dim fs As New FileStream(szFileName, FileMode.Open)
        ' Create a BinaryReader on the file.
        Dim br As New BinaryReader(fs)
        ' Dim an array of bytes large enough to hold the file's contents.
        Dim bytes(fs.Length) As Byte
        Dim bSuccess As Boolean
        ' Your unmanaged pointer.
        Dim pUnmanagedBytes As IntPtr

        ' Read the contents of the file into the array.
        bytes = br.ReadBytes(fs.Length)
        ' Allocate some unmanaged memory for those bytes.
        pUnmanagedBytes = Marshal.AllocCoTaskMem(fs.Length)
        ' Copy the managed byte array into the unmanaged array.
        Marshal.Copy(bytes, 0, pUnmanagedBytes, fs.Length)
        ' Send the unmanaged bytes to the printer.
        bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, fs.Length)
        ' Free the unmanaged memory that you allocated earlier.
        Marshal.FreeCoTaskMem(pUnmanagedBytes)
        Return bSuccess
    End Function ' SendFileToPrinter()

    ' When the function is given a string and a printer name,
    ' the function sends the string to the printer as raw bytes.
    Public Shared Function SendStringToPrinter(ByVal szPrinterName As String, ByVal szString As String)
        Dim pBytes As IntPtr
        Dim dwCount As Int32
        ' How many characters are in the string?
        dwCount = szString.Length()
        ' Assume that the printer is expecting ANSI text, and then convert
        ' the string to ANSI text.
        pBytes = Marshal.StringToCoTaskMemAnsi(szString)
        ' Send the converted ANSI string to the printer.
        Dim bSuccess As Boolean = SendBytesToPrinter(szPrinterName, pBytes, dwCount)
        Marshal.FreeCoTaskMem(pBytes)
        Return bSuccess
    End Function

    'custom paper ================================================================================

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Friend Structure structSize
        Public width As Integer
        Public height As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Friend Structure structRect
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Friend Structure structPrinterDefaults
        <MarshalAs(UnmanagedType.LPTStr)>
        Public pDatatype As String
        Public pDevMode As IntPtr
        <MarshalAs(UnmanagedType.I4)>
        Public DesiredAccess As Integer
    End Structure

    <StructLayout(LayoutKind.Explicit, CharSet:=CharSet.Unicode)>
    Friend Structure FormInfo1
        <FieldOffset(0), MarshalAs(UnmanagedType.I4)>
        Public Flags As UInteger
        <FieldOffset(4), MarshalAs(UnmanagedType.LPWStr)>
        Public pName As String
        <FieldOffset(8)>
        Public Size As structSize
        <FieldOffset(16)>
        Public ImageableArea As structRect
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Friend Structure structDevMode
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Public dmDeviceName As [String]
        <MarshalAs(UnmanagedType.U2)>
        Public dmSpecVersion As Short
        <MarshalAs(UnmanagedType.U2)>
        Public dmDriverVersion As Short
        <MarshalAs(UnmanagedType.U2)>
        Public dmSize As Short
        <MarshalAs(UnmanagedType.U2)>
        Public dmDriverExtra As Short
        <MarshalAs(UnmanagedType.U4)>
        Public dmFields As Integer
        <MarshalAs(UnmanagedType.I2)>
        Public dmOrientation As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmPaperSize As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmPaperLength As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmPaperWidth As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmScale As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmCopies As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmDefaultSource As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmPrintQuality As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmColor As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmDuplex As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmYResolution As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmTTOption As Short
        <MarshalAs(UnmanagedType.I2)>
        Public dmCollate As Short
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Public dmFormName As [String]
        <MarshalAs(UnmanagedType.U2)>
        Public dmLogPixels As Short
        <MarshalAs(UnmanagedType.U4)>
        Public dmBitsPerPel As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmPelsWidth As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmPelsHeight As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmNup As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmDisplayFrequency As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmICMMethod As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmICMIntent As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmMediaType As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmDitherType As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmReserved1 As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dmReserved2 As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Friend Structure PRINTER_INFO_9
        Public pDevMode As IntPtr
    End Structure

    <DllImport("winspool.Drv", EntryPoint:="OpenPrinter", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall), SuppressUnmanagedCodeSecurity>
    Friend Shared Function OpenPrinter(<MarshalAs(UnmanagedType.LPTStr)> printerName As String, ByRef phPrinter As IntPtr, ByRef pd As structPrinterDefaults) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="AddFormW", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute>
    Friend Shared Function AddForm(phPrinter As IntPtr, <MarshalAs(UnmanagedType.I4)> level As Integer, ByRef form As FormInfo1) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="DeleteForm", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall), SuppressUnmanagedCodeSecurity>
    Friend Shared Function DeleteForm(phPrinter As IntPtr, <MarshalAs(UnmanagedType.LPTStr)> pName As String) As Boolean
    End Function

    <DllImport("kernel32.dll", EntryPoint:="GetLastError", SetLastError:=False, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall), SuppressUnmanagedCodeSecurity>
    Friend Shared Function GetLastError() As Int32
    End Function

    <DllImport("winspool.Drv", EntryPoint:="DocumentPropertiesA", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function DocumentProperties(hwnd As IntPtr, hPrinter As IntPtr, <MarshalAs(UnmanagedType.LPStr)> pDeviceName As String, pDevModeOutput As IntPtr, pDevModeInput As IntPtr, fMode As Integer) As Integer
    End Function

    <DllImport("winspool.Drv", EntryPoint:="GetPrinterA", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function GetPrinter(hPrinter As IntPtr, dwLevel As Integer, pPrinter As IntPtr, dwBuf As Integer, ByRef dwNeeded As Integer) As Boolean
        ' changed from Int32
    End Function

    <DllImport("winspool.Drv", EntryPoint:="SetPrinterA", SetLastError:=True, CharSet:=CharSet.Auto, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute>
    Public Shared Function SetPrinter(hPrinter As IntPtr, <MarshalAs(UnmanagedType.I4)> level As Integer, pPrinter As IntPtr, <MarshalAs(UnmanagedType.I4)> command As Integer) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Shared Function SendMessageTimeout(windowHandle As IntPtr, Msg As UInteger, wParam As IntPtr, lParam As IntPtr, flags As SendMessageTimeoutFlags, timeout As UInteger,
            ByRef result As IntPtr) As IntPtr
    End Function

    <DllImport("GDI32.dll", EntryPoint:="CreateDC", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute>
    Friend Shared Function CreateDC(<MarshalAs(UnmanagedType.LPTStr)> pDrive As String, <MarshalAs(UnmanagedType.LPTStr)> pName As String, <MarshalAs(UnmanagedType.LPTStr)> pOutput As String, ByRef pDevMode As structDevMode) As IntPtr
    End Function

    <DllImport("GDI32.dll", EntryPoint:="ResetDC", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute>
    Friend Shared Function ResetDC(hDC As IntPtr, ByRef pDevMode As structDevMode) As IntPtr
    End Function

    <DllImport("GDI32.dll", EntryPoint:="DeleteDC", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=False, CallingConvention:=CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute>
    Friend Shared Function DeleteDC(hDC As IntPtr) As Boolean
    End Function

    <Flags>
    Public Enum SendMessageTimeoutFlags As UInteger
        SMTO_NORMAL = &H0
        SMTO_BLOCK = &H1
        SMTO_ABORTIFHUNG = &H2
        SMTO_NOTIMEOUTIFNOTHUNG = &H8
    End Enum
    Const WM_SETTINGCHANGE As Integer = &H1A
    Const HWND_BROADCAST As Integer = &HFFFF

    Public Shared Sub AddCustomPaperSizeToDefaultPrinter(paperName As String, widthMm As Single, heightMm As Single)
        Dim pd As New PrintDocument
        Dim sPrinterName As String = pd.PrinterSettings.PrinterName
        AddCustomPaperSize(sPrinterName, paperName, widthMm, heightMm)
    End Sub

    Public Shared Sub AddCustomPaperSize(printerName As String, paperName As String, widthMm As Single, heightMm As Single)
        If PlatformID.Win32NT = Environment.OSVersion.Platform Then
            ' The code to add a custom paper size is different for Windows NT then it is
            ' for previous versions of windows

            Const PRINTER_ACCESS_USE As Integer = &H8
            Const PRINTER_ACCESS_ADMINISTER As Integer = &H4
            'Const FORM_PRINTER As Integer = &H2

            Dim defaults As New structPrinterDefaults()
            defaults.pDatatype = Nothing
            defaults.pDevMode = IntPtr.Zero
            defaults.DesiredAccess = PRINTER_ACCESS_ADMINISTER Or PRINTER_ACCESS_USE

            Dim hPrinter As IntPtr = IntPtr.Zero

            ' Open the printer.
            If OpenPrinter(printerName, hPrinter, defaults) Then
                Try
                    ' delete the form incase it already exists
                    DeleteForm(hPrinter, paperName)
                    ' create and initialize the FORM_INFO_1 structure
                    Dim formInfo As New FormInfo1()
                    formInfo.Flags = 0
                    formInfo.pName = paperName
                    ' all sizes in 1000ths of millimeters
                    formInfo.Size.width = CInt(widthMm * 1000.0)
                    formInfo.Size.height = CInt(heightMm * 1000.0)
                    formInfo.ImageableArea.left = 0
                    formInfo.ImageableArea.right = formInfo.Size.width
                    formInfo.ImageableArea.top = 0
                    formInfo.ImageableArea.bottom = formInfo.Size.height
                    If Not AddForm(hPrinter, 1, formInfo) Then
                        Dim strBuilder As New StringBuilder()
                        strBuilder.AppendFormat("Failed to add the custom paper size {0} to the printer {1}, System error number: {2}", paperName, printerName, GetLastError())
                        Throw New ApplicationException(strBuilder.ToString())
                    End If

                    ' INIT
                    Const DM_OUT_BUFFER As Integer = 2
                    Const DM_IN_BUFFER As Integer = 8
                    Dim devMode As New structDevMode()
                    Dim hPrinterInfo As IntPtr, hDummy As IntPtr
                    Dim printerInfo As PRINTER_INFO_9
                    printerInfo.pDevMode = IntPtr.Zero
                    Dim iPrinterInfoSize As Integer, iDummyInt As Integer


                    ' GET THE SIZE OF THE DEV_MODE BUFFER
                    Dim iDevModeSize As Integer = DocumentProperties(IntPtr.Zero, hPrinter, printerName, IntPtr.Zero, IntPtr.Zero, 0)

                    If iDevModeSize < 0 Then
                        Throw New ApplicationException("Cannot get the size of the DEVMODE structure.")
                    End If

                    ' ALLOCATE THE BUFFER
                    Dim hDevMode As IntPtr = Marshal.AllocCoTaskMem(iDevModeSize + 100)

                    ' GET A POINTER TO THE DEV_MODE BUFFER 
                    Dim iRet As Integer = DocumentProperties(IntPtr.Zero, hPrinter, printerName, hDevMode, IntPtr.Zero, DM_OUT_BUFFER)

                    If iRet < 0 Then
                        Throw New ApplicationException("Cannot get the DEVMODE structure.")
                    End If

                    ' FILL THE DEV_MODE STRUCTURE
                    devMode = CType(Marshal.PtrToStructure(hDevMode, devMode.[GetType]()), structDevMode)

                    ' SET THE FORM NAME FIELDS TO INDICATE THAT THIS FIELD WILL BE MODIFIED
                    devMode.dmFields = &H10000
                    ' DM_FORMNAME 
                    ' SET THE FORM NAME
                    devMode.dmFormName = paperName

                    ' PUT THE DEV_MODE STRUCTURE BACK INTO THE POINTER
                    Marshal.StructureToPtr(devMode, hDevMode, True)

                    ' MERGE THE NEW CHAGES WITH THE OLD
                    iRet = DocumentProperties(IntPtr.Zero, hPrinter, printerName, printerInfo.pDevMode, printerInfo.pDevMode, DM_IN_BUFFER Or DM_OUT_BUFFER)

                    If iRet < 0 Then
                        Throw New ApplicationException("Unable to set the orientation setting for this printer.")
                    End If

                    ' GET THE PRINTER INFO SIZE
                    GetPrinter(hPrinter, 9, IntPtr.Zero, 0, iPrinterInfoSize)
                    If iPrinterInfoSize = 0 Then
                        Throw New ApplicationException("GetPrinter failed. Couldn't get the # bytes needed for shared PRINTER_INFO_9 structure")
                    End If

                    ' ALLOCATE THE BUFFER
                    hPrinterInfo = Marshal.AllocCoTaskMem(iPrinterInfoSize + 100)

                    ' GET A POINTER TO THE PRINTER INFO BUFFER
                    Dim bSuccess As Boolean = GetPrinter(hPrinter, 9, hPrinterInfo, iPrinterInfoSize, iDummyInt)

                    If Not bSuccess Then
                        Throw New ApplicationException("GetPrinter failed. Couldn't get the shared PRINTER_INFO_9 structure")
                    End If

                    ' FILL THE PRINTER INFO STRUCTURE
                    printerInfo = CType(Marshal.PtrToStructure(hPrinterInfo, printerInfo.[GetType]()), PRINTER_INFO_9)
                    printerInfo.pDevMode = hDevMode

                    ' GET A POINTER TO THE PRINTER INFO STRUCTURE
                    Marshal.StructureToPtr(printerInfo, hPrinterInfo, True)

                    ' SET THE PRINTER SETTINGS
                    bSuccess = SetPrinter(hPrinter, 9, hPrinterInfo, 0)

                    If Not bSuccess Then
                        Throw New Win32Exception(Marshal.GetLastWin32Error(), "SetPrinter() failed.  Couldn't set the printer settings")
                    End If

                    ' Tell all open programs that this change occurred.
                    SendMessageTimeout(New IntPtr(HWND_BROADCAST), WM_SETTINGCHANGE, IntPtr.Zero, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_NORMAL, 1000,
                        hDummy)
                Finally
                    ClosePrinter(hPrinter)
                End Try
            Else
                Dim strBuilder As New StringBuilder()
                strBuilder.AppendFormat("Failed to open the {0} printer, System error number: {1}", printerName, GetLastError())
                Throw New ApplicationException(strBuilder.ToString())
            End If
        Else
            Dim pDevMode As New structDevMode()
            Dim hDC As IntPtr = CreateDC(Nothing, printerName, Nothing, pDevMode)
            If hDC <> IntPtr.Zero Then
                Const DM_PAPERSIZE As Long = &H2L
                Const DM_PAPERLENGTH As Long = &H4L
                Const DM_PAPERWIDTH As Long = &H8L
                pDevMode.dmFields = DM_PAPERSIZE Or DM_PAPERWIDTH Or DM_PAPERLENGTH
                pDevMode.dmPaperSize = 256
                pDevMode.dmPaperWidth = CShort(widthMm * 1000.0)
                pDevMode.dmPaperLength = CShort(heightMm * 1000.0)
                ResetDC(hDC, pDevMode)
                DeleteDC(hDC)
            End If
        End If
    End Sub
End Class
