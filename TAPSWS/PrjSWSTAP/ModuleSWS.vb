﻿Imports System.Net.Sockets
Imports System.Threading.Tasks
Imports System.Text.RegularExpressions ' Namespace untuk manipulasi registry
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Windows.Forms
Imports System.Reflection
Imports System.Environment

Imports DevExpress
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Imports DevExpress.XtraNavBar
Imports DevExpress.XtraSplashScreen

Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository

Imports DevExpress.Utils

Imports FastReport

'Imports Oracle.DataAccess.Client >> ganti dengan devart.data & devart.data.oracle
Imports Devart.Data
Imports Devart.Data.Oracle
Imports Devart.Common

Imports AForge.Video

'Imports PrjSWSTAP

Module ModuleSWS

    Public SQL As String

    Public USERNAME, ROLEID, ROLENAME As String
    Public bScaleStartOff As Boolean

    'id PT
    Public nPT As String
    Public nAlamat As String
    Public nKota As String
    Public nTelp As String
    Public nRptName As String
    Public GETwEIGHT As Boolean

    Public WEIGHT As String
    Public nNamaSite, nIdSite As String
    Public nCountConect As String
    Public nPortIndicator As Integer

    Public CAMERAON As Boolean
    Public IndikatorON As Boolean
    Public WeightIndicator As String

    Public StKoneksiIndikator As String
    Public TxtIndikator As TextBox


    Public _Listener As TcpListener
    Public _Connections As New List(Of ConnectionInfo)
    Public _ConnectionMontior As Task
    Public nDataBerat As String
    Public net As Double




    Public CompanyCode As String
    Public Company As String
    Public LocationSite As String

    Public MillPlant As String
    Public StoreLocation1 As String
    Public StoreLocation2 As String
    Public ComportSetting As String
    Public WBCode As String
    Public IPCamera1 As String
    Public IPCamera2 As String
    Public IPIndicator As String
    Public LoadingRampTransit As String
    Public SAP As String
    Public AppVersion As String
    Public config As Boolean = False
    Public STonlineUser As Boolean = False

    'KONEKSI SERVER LOCAL
    Public CONN As New OracleConnection
    Public CMD As New OracleCommand
    Public TRANS As OracleTransaction
    Public DR As OracleDataReader
    Public DA As OracleDataAdapter
    Public DS As DataSet
    Public DT As DataTable

    Public DBSourceLocal As String
    Public DBPortLocal As String
    Public DBNameLocal As String
    Public DBVerLocal As String
    Public DBUserLocal As String
    Public DBPassLocal As String
    Public ConStringLocal As String

    'KONEKSI SERVER STAGING(1) 
    Public CONN1 As New OracleConnection
    Public CMD1 As New OracleCommand
    Public TRANS1 As OracleTransaction
    Public DR1 As OracleDataReader
    Public DA1 As OracleDataAdapter
    Public DS1 As DataSet
    Public DT1 As DataTable

    Public DBSourceStaging As String
    Public DBPortStaging As String
    Public DBNameStaging As String
    Public DBVerStaging As String
    Public DBUserStaging As String
    Public DBPassStaging As String
    Public ConStringStaging As String

    Public TblLogin As String = "Sign In"

    Public blobParameter As New OracleParameter



    Delegate Sub SetTextCallback(ByVal [text] As String) 'Added to prevent threading errors during receiveing of data

    'Public Stream As MJPEGStream
    'Public Parameter As String
    'Public PIC As DevExpress.XtraEditors.PictureEdit
    'Sub New()
    '    If Parameter = "" Then Parameter = "127.0.0.1:8080"
    '    Stream = New MJPEGStream(Parameter)
    '    AddHandler Stream.NewFrame, New NewFrameEventHandler(AddressOf Stream_NewFream)
    'End Sub

    'Private Sub Stream_NewFream(sender As Object, eventargs As NewFrameEventArgs)
    '    Dim bmp As Bitmap = DirectCast(eventargs.Frame.Clone(), Bitmap)
    '    PIC.Image = bmp
    'End Sub

#Region "General Config"
    Public Sub GetConfig()
        CompanyCode = My.Settings.CompanyCode.ToString
        Company = My.Settings.Company.ToString
        LocationSite = My.Settings.LocationSite.ToString
        MillPlant = My.Settings.Millplant.ToString
        StoreLocation1 = My.Settings.StoreLocation1.ToString
        StoreLocation2 = My.Settings.StoreLocation2.ToString
        ComportSetting = My.Settings.ComportSetting.ToString
        WBCode = My.Settings.WBCode.ToString
        IPCamera1 = My.Settings.IPCamera1.ToString
        IPCamera2 = My.Settings.IPCamera2.ToString
        IPIndicator = My.Settings.IPIndicator.ToString
        LoadingRampTransit = My.Settings.LoadingRampTransit.ToString
        SAP = My.Settings.SAP.ToString
        AppVersion = My.Settings.AppVersion.ToString
        'local seting
        DBSourceLocal = My.Settings.DBSourceLocal.ToString
        DBPortLocal = My.Settings.DBPortLocal.ToString
        DBNameLocal = My.Settings.DBNameLocal.ToString
        DBVerLocal = My.Settings.DBVerLocal.ToString
        DBUserLocal = My.Settings.DBUserLocal.ToString
        DBPassLocal = My.Settings.DBPassLocal.ToString
        'staging
        DBSourceStaging = My.Settings.DBSourceStaging.ToString
        DBPortStaging = My.Settings.DBPortStaging.ToString
        DBNameStaging = My.Settings.DBNameStaging.ToString
        DBVerStaging = My.Settings.DBVerStaging.ToString
        DBUserStaging = My.Settings.DBUserStaging.ToString
        DBPassStaging = My.Settings.DBPassStaging.ToString



    End Sub

#End Region

#Region "FTP"
    Public Sub FtpUploadFile(ByVal filetoupload As String, ByVal ftpuri As String, ByVal ftpusername As String, ByVal ftppassword As String)
        ' Create a web request that will be used to talk with the server and set the request method to upload a file by ftp.
        Dim ftpRequest As FtpWebRequest = CType(WebRequest.Create(ftpuri), FtpWebRequest)

        Try
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile

            ' Confirm the Network credentials based on the user name and password passed in.
            ftpRequest.Credentials = New NetworkCredential(ftpusername, ftppassword)

            ' Read into a Byte array the contents of the file to be uploaded 
            Dim bytes() As Byte = System.IO.File.ReadAllBytes(filetoupload)

            ' Transfer the byte array contents into the request stream, write and then close when done.
            ftpRequest.ContentLength = bytes.Length
            Using UploadStream As Stream = ftpRequest.GetRequestStream()
                UploadStream.Write(bytes, 0, bytes.Length)
                UploadStream.Close()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("Process Complete")
    End Sub
    Private Sub FTPDownloadFile(ByVal downloadpath As String, ByVal ftpuri As String, ByVal ftpusername As String, ByVal ftppassword As String)
        'Create a WebClient.
        Dim request As New WebClient()

        ' Confirm the Network credentials based on the user name and password passed in.
        request.Credentials = New NetworkCredential(ftpusername, ftppassword)

        'Read the file data into a Byte array
        Dim bytes() As Byte = request.DownloadData(ftpuri)

        Try
            '  Create a FileStream to read the file into
            Dim DownloadStream As FileStream = IO.File.Create(downloadpath)
            '  Stream this data into the file
            DownloadStream.Write(bytes, 0, bytes.Length)
            '  Close the FileStream
            DownloadStream.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("Process Complete")

    End Sub
#End Region

#Region "Oracle Koneksi"

    Public Sub GetStagingDbConfig()
        'ConStringStaging = "DATA SOURCE=" & DBSourceStaging & ":" & DBPortStaging & "/" & DBVerStaging & "; USER ID=" & DBUserStaging & "; PASSWORD=" & DBPassStaging & ";"
        ConStringStaging = "Data Source=(DESCRIPTION=" + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" + "(HOST=" & DBSourceStaging & ")" + "(PORT=" & DBPortStaging & ")))(CONNECT_DATA=(SERVER=DEDICATED)" + "(SERVICE_NAME=" & DBVerStaging & ")));" + "User Id=" & DBUserStaging & ";Password=" & DBPassStaging & ";"
    End Sub
    Public Sub GetLocalDbConfig()
        ConStringLocal = "Data Source=(DESCRIPTION=" + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" + "(HOST=" & DBSourceLocal & ")" + "(PORT=" & DBPortLocal & ")))(CONNECT_DATA=(SERVER=DEDICATED)" + "(SERVICE_NAME=" & DBVerLocal & ")));" + "User Id=" & DBUserLocal & ";Password=" & DBPassLocal & ";"
    End Sub
    Public Function OpenConnLocal() As Boolean
        OpenConnLocal = False
        GetLocalDbConfig()
        Try
            If CONN.State = ConnectionState.Closed Then
                CONN = New OracleConnection(ConStringLocal)
                CONN.Open()
                OpenConnLocal = True
            Else
                OpenConnLocal = True
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString)
            CONN.Close()
            OpenConnLocal = False
        End Try
    End Function

    Public Sub CloseConnLocal()
        If CONN.State = ConnectionState.Open Then
            CONN.Close()
        End If
    End Sub

    Public Function OpenConnStaging() As Boolean
        OpenConnStaging = False
        GetStagingDbConfig()
        Try
            CONN1 = New OracleConnection(ConStringStaging)
            If CONN1.State = ConnectionState.Closed Then
                CONN1 = New OracleConnection(ConStringStaging)
                CONN1.Open()
                OpenConnStaging = True
            Else
                OpenConnStaging = True
            End If
        Catch ex As Exception
            CONN1.Close()
            OpenConnStaging = False
        End Try
        ' Return OpenConnStaging()
    End Function
    Public Sub CloseConnStaging()
        If CONN1.State = ConnectionState.Open Then
            CONN1.Close()
        End If
    End Sub
#End Region

#Region "Tool"
    Public Sub FrmChildShow(frm As Form)
        SplashScreenManager.ShowDefaultWaitForm()
        frm.MdiParent = FrmMain
        frm.Show()
        SplashScreenManager.CloseDefaultWaitForm()
    End Sub
    Public Sub FrmShowUp(frm As Form)
        SplashScreenManager.ShowDefaultWaitForm()
        frm.ShowDialog()
        SplashScreenManager.CloseDefaultWaitForm()
    End Sub

    Public Function getListItem(ByRef lst As ListView, ByVal i As Integer) As String
        On Error Resume Next
        Dim tt As String
        getListItem = lst.SelectedItems.Item(0).SubItems(i).ToString
        Return getListItem
    End Function
#End Region
#Region "Olah Data"
    Public Function LogOn() As Boolean
        Dim Time As String = Now
        Dim IP As String = GetIPAddr()
        LogOn = False
        'CHECK LOG
        SQL = "SELECT * FROM TLOGINHISTORY WHERE USERID='" & USERNAME & "' AND USED='Y'"
        If CheckRecord(SQL) > 0 Then
            Dim OnPC As String = GetLastOnline(USERNAME)
            MsgBox("YOU ARE ONLINE ON " & OnPC, vbInformation, "Info Login")
            LogOn = True
        Else
            'INSERT LOG
            SQL = "INSERT INTO TLOGINHISTORY (LOGINDATE,USERID,IPADDRESS,REMARK,USED) " +
              "VALUES('" & Time & "','" & USERNAME & "','" & IP & "','SUCCESS','Y')"
            ExecuteNonQuery(SQL)
            'LogOn = False
        End If
        Return LogOn
    End Function
    Public Sub LogOFF()
        Dim Time As String = Now
        Dim IP As String = GetIPAddr()
        'CHECK LOG
        SQL = "SELECT * FROM TLOGINHISTORY WHERE USERID='" & USERNAME & "' AND USED='Y'"
        If CheckRecord(SQL) > 0 Then
            Dim OnPC As String = GetLastOnline(USERNAME)
            SQL = "UPDATE TLOGINHISTORY SET used='N',LOGOUTDATE='" & Time & "' WHERE used='Y' and userid='" & USERNAME & "' AND IPADDRESS='" & OnPC & "'"
            ExecuteNonQuery(SQL)
        End If
    End Sub


    Public Sub AddaColumn(ByRef ColumnString As String, ByRef LV As ListView)
        Dim NewCH As New ColumnHeader

        NewCH.Text = ColumnString
        LV.Columns.Add(NewCH)
    End Sub

    Public Function GetLsvItem(ByVal nLsv As Object, i As Integer) As String
        Try
            Dim lst As ListView
            lst = CType(nLsv, ListView)
            GetLsvItem = lst.Items(lst.FocusedItem.Index).SubItems(i).Text
        Catch
            GetLsvItem = ""
        End Try
        Return GetLsvItem
    End Function
    Public Function GetLstSEQ(ByVal nLsv As Object) As String
        GetLstSEQ = "0"
        Try
            Dim lst As ListView
            Dim i As Integer
            lst = CType(nLsv, ListView)
            For i = 0 To lst.Items.Count - 1
                GetLstSEQ = CType(CInt(lst.Items(i).Text) = i + 1, String)
            Next
        Catch
            GetLstSEQ = "0"
        End Try
        Return GetLstSEQ
    End Function

    Public Function ExecuteQuery(ByVal Query As String) As DataTable
        ExecuteQuery = Nothing
        If Not OpenConnLocal() Then
            Exit Function
        End If
        CMD = CONN.CreateCommand()
        TRANS = CONN.BeginTransaction(IsolationLevel.ReadCommitted)
        CMD.Transaction = TRANS
        Try
            CMD.CommandText = Query
            CMD.ExecuteReader()
            TRANS.Commit()
            DA = New OracleDataAdapter
            DA.SelectCommand = CMD
            DS = New DataSet
            DA.Fill(DS)
            DT = DS.Tables(0)
            CONN.Close()
            CMD = Nothing
        Catch ex As Exception
            TRANS.Rollback()
            CMD = Nothing
            CONN.Close()
        End Try
        Return DT
    End Function
    Public Function GetLastOnline(ByVal nUsername As String) As String
        GetLastOnline = Nothing
        If Not OpenConnLocal() Then
            Exit Function
        End If
        Dim strsql As String = " select * from TLOGINHISTORY where ROWNUM <= 1 And used='Y' and userid='" & nUsername & "' order by logindate desc "
        Dim CONN As New OracleConnection(ConStringLocal)
        CMD = New OracleCommand(strsql, CONN)
        CONN.Open()
        Dim RDR As OracleDataReader = CMD.ExecuteReader
        While RDR.Read
            GetLastOnline = RDR("IPADDRESS").ToString
        End While
        RDR.Close()
        CMD = Nothing
    End Function


    Public Function CheckRecord(ByVal SSQL As String) As Double
        CheckRecord = 0
        Dim i As Integer = 0
        If Not OpenConnLocal() Then
            Exit Function
        End If
        CMD = New OracleCommand(SSQL, CONN)
        Dim Odr As OracleDataReader = CMD.ExecuteReader
        Try
            While Odr.Read()
                CheckRecord = CheckRecord + 1
            End While
        Finally
            Odr.Close()
            CMD = Nothing
        End Try
        Return CheckRecord
    End Function


    Public Sub ExecuteNonQuery(ByVal query As String)
        If Not OpenConnLocal() Then
            Exit Sub
        End If
        CMD = CONN.CreateCommand
        TRANS = CONN.BeginTransaction(IsolationLevel.ReadCommitted)
        CMD.Transaction = TRANS
        Try
            CMD.CommandText = query
            CMD.ExecuteNonQuery()
            TRANS.Commit()
        Catch e As Exception
            TRANS.Rollback()
        End Try
        CMD = Nothing
        CONN.Close()
    End Sub

    Public Function AmbilTgl() As String
        AmbilTgl = Nothing
        If Not OpenConnLocal() Then
            Exit Function
        End If
        Dim strsql As String = "select SYSDATE TGL FROM DUAL"
        Dim CONN As New OracleConnection(ConStringLocal)
        CMD = New OracleCommand(strsql, CONN)
        CONN.Open()
        Dim RDR As OracleDataReader = CMD.ExecuteReader
        While RDR.Read
            AmbilTgl = RDR("TGL").ToString
        End While
        RDR.Close()
        CMD = Nothing
    End Function

    Public Function GetSite() As String
        GetSite = Nothing
        If Not OpenConnLocal() Then
            Exit Function
        End If
        Dim strsql As String = " select IDSITE , MASTER NAMASITE from d_master " +
                                   " where aktif='Y' and IDTABEL='MS'"
        Dim CONN As New OracleConnection(ConStringLocal)
        CMD = New OracleCommand(strsql, CONN)
        CONN.Open()
        Dim RDR As OracleDataReader = CMD.ExecuteReader
        While RDR.Read
            nNamaSite = RDR("NAMASITE").ToString
            nIdSite = RDR("IDSITE").ToString
        End While
        RDR.Close()
        CMD = Nothing
    End Function

    Public Function CheckLogin(ByVal nUser As String, ByVal nPass As String) As Boolean
        CheckLogin = False
        If Not OpenConnLocal() Then
            Exit Function
        End If
        Dim i As Integer = 0
        Dim queryString As String = "SELECT * FROM T_USERPROFILE WHERE USERNAME='" & nUser & "' and PASSWD ='" & nPass & "' "
        Dim conn As New OracleConnection(ConStringLocal)
        Dim cmd As New OracleCommand(queryString, conn)
        conn.Open()
        Dim DR As OracleDataReader = cmd.ExecuteReader
        Try
            While DR.Read()
                USERNAME = DR("USERNAME").ToString
                ROLEID = DR("ROLEID").ToString
                CheckLogin = True
            End While
            i = i + 1

        Finally
            DR.Close()
            cmd = Nothing
        End Try
        Return CheckLogin
    End Function


    Public Function GetTiket(Ind As String) As String
        Dim sql As String
        Dim Th As String
        Dim Bln As String
        Dim nTiket As String
        Dim date1 As Date = Now
        Dim Code As String

        Th = date1.ToString("yyMM")
        Bln = date1.ToString("MM")

        Code = GetCode(Ind)
        nTiket = ""
        GetTiket = ""
        sql = " Select MAX(SUBSTR(IDTR, -10)) IDTR " +
                   " From D_TRANSAKSI  Where substr(IDTR,1,2) ='" & Ind & "' "
        Dim conn As OracleConnection = New OracleConnection(ConStringLocal)
        Try
            conn.Open()
            Dim cmd As OracleCommand = New OracleCommand(sql, conn)
            Dim rdr As OracleDataReader = cmd.ExecuteReader
            While rdr.Read
                'reset urut jika ganti bln
                If rdr(0).ToString = "" Then
                    nTiket = Th & Right(Code, 6)
                ElseIf Th > Left(rdr(0).ToString, 4) Then
                    Call ResetCode(Ind)
                    nTiket = Th & Right(Code, 6)
                ElseIf CInt(Left(rdr(0).ToString, 4)) <> 0 Then
                    nTiket = Th & Right(Code, 6)
                End If
                GetTiket = Ind & nTiket
            End While
            rdr.Close()
        Catch ex As Exception
            MsgBox("Error:" & ex.ToString)
        Finally
            conn.Close()
        End Try
        Return GetTiket
    End Function
    Public Function GetFrmName(nMenuName As String) As String
        GetFrmName = ""
        Dim ssql As String
        If Not OpenConnLocal() Then
            Exit Function
        End If

        ssql = "SELECT FRMNAME  FROM T_ACCESSRIGHTS WHERE ACCESSNAME='" & nMenuName & "'"
        Dim cmd As New OracleCommand(ssql, CONN)
        Dim oRs As OracleDataReader = cmd.ExecuteReader
        Try
            While oRs.Read
                GetFrmName = oRs("frmname")
            End While
        Catch
        End Try
        Return GetFrmName
    End Function


    Public Function GetCode(nKode As String) As String
        GetCode = ""
        Dim ssql As String
        If Not OpenConnLocal() Then
            Exit Function
        End If
        ssql = "Select urut From  M_CODE " +
                   " where IDTABEL = '" & nKode & "'"
        Dim cmd As New OracleCommand(ssql, CONN)
        Dim oRs As OracleDataReader = cmd.ExecuteReader
        While oRs.Read
            GetCode = nKode & Right("00000000" & CInt(oRs("urut").ToString) + 1, 6)
        End While
        Return GetCode
    End Function

    Public Function GetTime()
        GetTime = ""
        SQL = "SELECT TO_CHAR (SYSDATE, 'MM-DD-YYYY HH24:MI:SS') NOW FROM DUAL"
        ExecuteQuery(SQL)
        If Not OpenConnLocal() Then
            Exit Function
        End If
        Try
            GetTime = DT("now").ToString()
        Catch
        End Try
        Return GetTime
    End Function
    Public Sub UpdateCode(nKode As String)
        If Not OpenConnLocal() Then
            Exit Sub
        End If
        Dim ssql As String
        ssql = "update M_CODE " +
                " set urut =urut+1 " +
                " where IDTABEL = '" & nKode & "'"
        ExecuteNonQuery(ssql)
    End Sub
    Public Function ResetCode(nKode As String) As String
        ResetCode = ""
        Dim ssql As String
        ssql = "update M_CODE" +
                   " set urut =0" +
                   " where IDTABEL = '" & nKode & "'"
        ExecuteNonQuery(ssql)
        CMD = Nothing
        Return ResetCode
    End Function
    Public Sub FILLComboBoxEdit(ByVal sql As String, ByVal index As Integer, ByVal Cbx As DevExpress.XtraEditors.ComboBoxEdit, ByVal editText As Boolean)
        Cbx.Properties.Items.Clear()
        Try
            If Not OpenConnLocal() Then
                Exit Sub
            End If
            If sql = Nothing Then Exit Sub
            CMD = New OracleCommand(sql, CONN)
            CONN.Open()
            Dim rdr As OracleDataReader = CMD.ExecuteReader
            Cbx.Properties.Items.Clear()
            While rdr.Read
                Cbx.Properties.Items.Add(rdr(index).ToString)
            End While
            If editText = False Then Cbx.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            rdr.Close()
        Catch ex As Exception
            MsgBox("Error:" & ex.ToString)
        Finally
            CONN.Close()
        End Try
    End Sub
    Public Sub FILLGridView(ByVal sql As String, ByVal DgView As DevExpress.XtraGrid.GridControl)
        Dim View As GridView = CType(DgView.FocusedView, GridView)
        Try
            If Not OpenConnLocal() Then
                Exit Sub
            End If
            DgView.DataSource = Nothing
            DgView.DataSource = ExecuteQuery(sql)
            View.BestFitColumns()
            View.OptionsBehavior.Editable = False
        Catch ex As Exception
            MsgBox("Error:" & ex.ToString)
        Finally
            CONN.Close()
        End Try
    End Sub

    Public Sub FillListView(ByVal sqlData As DataTable, ByVal lvList As ListView, ByVal imageID As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim xsize As Integer
        lvList.Clear()
        If sqlData Is Nothing Then Exit Sub
        For i = 0 To sqlData.Columns.Count - 1
            lvList.Columns.Add(sqlData.Columns(i).ColumnName)
        Next i

        For i = 0 To sqlData.Rows.Count - 1
            lvList.Items.Add(sqlData.Rows(i).Item(0).ToString, imageID)
            For j = 1 To sqlData.Columns.Count - 1
                If Not IsDBNull(sqlData.Rows(i).Item(j)) Then
                    lvList.Items(i).SubItems.Add(sqlData.Rows(i).Item(j).ToString)
                Else
                    lvList.Items(i).SubItems.Add("")
                End If
            Next j
        Next i

        For i = 0 To sqlData.Columns.Count - 1
            xsize = CInt(lvList.Width / sqlData.Columns.Count - 8)
            'MsgBox(xsize)
            If xsize > 1440 Then
                lvList.Columns(i).Width = xsize
            Else
                lvList.Columns(i).Width = xsize
            End If
            lvList.Columns(i).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
        Next i
    End Sub

    Public Function GetImgGrp(ByVal nGroup As String) As String
        GetImgGrp = ""
        Try
            If Not OpenConnLocal() Then
                Exit Function
            End If
            Dim strsql As String = "SELECT DISTINCT IMGID FROM T_ACCESSRIGHTS  WHERE ACCESSNAME='" & nGroup & "'"
            CMD = New OracleCommand(strsql, CONN)
            Dim rdr As OracleDataReader = CMD.ExecuteReader
            While rdr.Read
                If Val(rdr("IMGID").ToString) <> 0 Then
                    GetImgGrp = rdr("IMGID").ToString
                Else
                    GetImgGrp = 0
                End If
            End While
            rdr.Close()
            CMD = Nothing
        Catch
        End Try
        Return GetImgGrp
    End Function
    Public Function GetCodeGrp() As String
        GetCodeGrp = ""
        Try
            If Not OpenConnLocal() Then
                Exit Function
            End If
            Dim strsql As String = "SELECT CONCAT('000',MAX(ACCESSID)+1) GKODE FROM  T_ACCESSRIGHTS  WHERE  TYPE=0"
            CMD = New OracleCommand(strsql, CONN)
            Dim rdr As OracleDataReader = CMD.ExecuteReader
            While rdr.Read
                If Val(rdr("GKODE").ToString) <> 0 Then
                    GetCodeGrp = rdr("GKODE").ToString
                    GetCodeGrp = Right(GetCodeGrp, 4)
                End If
            End While
            rdr.Close()
            CMD = Nothing
        Catch
            CMD = Nothing
        End Try
        Return GetCodeGrp
    End Function
    Public Function GetCodeUser(ByVal UNAME As String) As String
        GetCodeUser = ""
        If Not OpenConnLocal() Then
            Exit Function
        End If
        Try
            Dim strsql As String = "SELECT USERID FROM  T_USERPROFILE WHERE  USERNAME='" & UNAME & "'"
            CMD = New OracleCommand(strsql, CONN)
            Dim rdr As OracleDataReader = CMD.ExecuteReader
            While rdr.Read
                If Val(rdr("USERID").ToString) <> 0 Then
                    GetCodeUser = rdr("USERID").ToString
                End If
            End While
            rdr.Close()
        Catch
        End Try
        CMD = Nothing
        Return GetCodeUser
    End Function
    Public Function GetCodeRole(ByVal nRole As String) As String
        GetCodeRole = ""
        If Not OpenConnLocal() Then
            Exit Function
        End If
        Try
            Dim strsql As String = "SELECT ROLEID FROM  T_ROLE WHERE  ROLENAME='" & nRole & "'"
            CMD = New OracleCommand(strsql, CONN)
            Dim rdr As OracleDataReader = CMD.ExecuteReader
            While rdr.Read
                GetCodeRole = rdr("ROLEID").ToString
            End While
            rdr.Close()
        Catch
        End Try
        CMD = Nothing
        Return GetCodeRole
    End Function


    Public Function GetParent(ByVal nMenu As String) As String
        GetParent = ""
        If Not OpenConnLocal() Then
            Exit Function
        End If
        Try
            Dim strsql As String = "Select Case PARENTID from T_ACCESSRIGHTS WHERE ACCESSID='" & nMenu & "' AND TYPE='1'"
            CMD = New OracleCommand(strsql, CONN)
            Dim rdr As OracleDataReader = CMD.ExecuteReader
            While rdr.Read
                GetParent = rdr("PARENTID").ToString
            End While
            rdr.Close()
        Catch
        End Try
        CMD = Nothing
        Return GetParent
    End Function


    Public Function GetCodeSub(ByVal parentid As String) As String
        GetCodeSub = ""
        Try
            If Not OpenConnLocal() Then
                Exit Function
            End If
            Dim strsql As String = "SELECT CONCAT('000',NVL(MAX(ACCESSID)+1,0)) MKODE FROM  T_ACCESSRIGHTS  WHERE  TYPE=1 AND PARENTID='" & parentid & "'"
            CMD = New OracleCommand(strsql, CONN)
            Dim rdr As OracleDataReader = CMD.ExecuteReader
            While rdr.Read
                GetCodeSub = rdr("MKODE").ToString
                GetCodeSub = Right(GetCodeSub, 4)
                If GetCodeSub = "0000" Then GetCodeSub = Right(parentid, 2) & "01"

            End While
            rdr.Close()
            CMD = Nothing
        Catch
            CMD = Nothing
        End Try
        Return GetCodeSub
    End Function
    Public Function GetParentId(ByVal menuname As String) As String
        GetParentId = ""
        Try
            If Not OpenConnLocal() Then
                Exit Function
            End If

            Dim strsql As String = "select accessid,PARENTID from T_ACCESSRIGHTS where accessid='" & menuname & "' AND TYPE=1"
            CMD = New OracleCommand(strsql, CONN)
            Dim rdr As OracleDataReader = CMD.ExecuteReader
            While rdr.Read
                GetParentId = rdr("PARENTID").ToString
            End While
            rdr.Close()
            CMD = Nothing
        Catch
            GetParentId = ""
            CMD = Nothing
        End Try
        Return GetParentId
    End Function



    Public Function GetIPAddr()
        Dim h As System.Net.IPHostEntry = Nothing
        GetIPAddr = "127.0.0.1"
        Try
            h = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)
            GetIPAddr = h.AddressList.GetValue(4).ToString
        Catch
        End Try
        Return GetIPAddr
    End Function

    '-------------------------Fast Report -------Start
    Public Sub ShowReport(ByRef nRpt As String, ByRef queryString As String, ByRef nTabelRpt As String)
        Dim Rpt As New Report
        Dim Fdss As New DataSet
        Dim Cmd As New OracleCommand
        Dim Da As New OracleDataAdapter

        Dim conn As OracleConnection = New OracleConnection(ConStringLocal)
        conn.ConnectionString = ConStringLocal
        conn.Open()

        With Cmd
            .Connection = conn
            .CommandText = queryString
            .CommandType = CommandType.Text
        End With
        Da.SelectCommand = Cmd
        'fill data to dataset
        Da.Fill(Fdss, nTabelRpt)
        ' register the dataset
        Rpt.RegisterData(Fdss)
        ' load the existing report
        Rpt.Load("..\..\" & nRpt & ".frx")
        'registe parameter
        Rpt.SetParameterValue("P1", " PT Triputra Agro Persada")
        Rpt.SetParameterValue("P2", " The East Building")
        Rpt.SetParameterValue("P3", " Kuningan - Jakarta")
        Rpt.SetParameterValue("P4", " Phone 021-123456789")
        Rpt.SetParameterValue("P5", " ")
        ' register the dataset
        Rpt.RegisterData(Fdss)
        ' run the report
        Rpt.Show()
        ' free resources used by report
        Rpt.Dispose()
        Da = Nothing
        conn.Close()
        conn = Nothing
    End Sub
    '-------------------------Fast Report -------End

    '-------------------------Check COMBO dan TEXT-------Start
    Public Function IsEmptyText(ByVal objText() As DevExpress.XtraEditors.TextEdit) As Boolean
        Dim i As Integer

        For i = 0 To objText.GetUpperBound(0) ' lakukan perulangan sebanyak array objek
            If Not (objText(i).Text.Length > 0) Then ' validas inputkan text, klo enggak diisi tampilkan peringatan
                MessageBox.Show("Sorry, the text should be filled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                objText(i).Focus()
                ' objText(i).BackColor = Color.YellowGreen
                Return True
                Exit Function
            End If
        Next

        Return False
    End Function

    Public Function IsEmptyCombo(ByVal objText() As XtraEditors.ComboBoxEdit) As Boolean
        Dim i As Integer

        For i = 0 To objText.GetUpperBound(0) ' lakukan perulangan sebanyak array objek
            If Not (objText(i).Text.Length > 0) Then ' validas inputkan text, klo enggak diisi tampilkan peringatan
                MessageBox.Show("Sorry, the Combo should be filled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                objText(i).Focus()
                Return True
                Exit Function
            End If
        Next

        Return False
    End Function
    '-------------------------Check COMBO dan TEXT-------END
#End Region


#Region "SCALE WEIGHT"
    Public Sub UpdateConnectionCountLabel()
        nCountConect = String.Format("{0} Connections", _Connections.Count)
    End Sub
    Public Function num(value As String) As String
        'Dim rgx As New Regex("[^a-zA-Z ]")  'replace angka
        Dim rgx As New Regex("[^\d ]")
        Dim wordy As String = rgx.Replace(value, "")
        num = wordy
    End Function
    Public Function GetSCSMessage(ByVal IP As String, ByVal Port As Int32) As String
        Dim tcpClient As New System.Net.Sockets.TcpClient()
        Try
            tcpClient.Connect(IP, Port)
            tcpClient.NoDelay = True
            Dim networkStream As NetworkStream = tcpClient.GetStream()
            If networkStream.CanWrite And networkStream.CanRead Then
                ' Do a simple write.
                Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes("$gimme")
                networkStream.Write(sendBytes, 0, sendBytes.Length)
                ' Read the NetworkStream into a byte buffer.
                Dim bytes(tcpClient.ReceiveBufferSize) As Byte
                networkStream.Read(bytes, 0, CInt(tcpClient.ReceiveBufferSize))
                ' Output the data received from the host to the console.
                Dim ReturnData As String = Encoding.ASCII.GetString(bytes)
                If InStr(ReturnData, "Socket Server") > 0 Then
                    ' Rewrite output to server to get real data
                    networkStream.Write(sendBytes, 0, sendBytes.Length)
                    Dim newbytes(tcpClient.ReceiveBufferSize) As Byte
                    networkStream.Read(newbytes, 0, CInt(tcpClient.ReceiveBufferSize))
                    ReturnData = Encoding.ASCII.GetString(newbytes)
                End If
                Dim i As Integer = InStr(ReturnData, Chr(0), CompareMethod.Binary)
                ReturnData = Microsoft.VisualBasic.Left(ReturnData, i - 1)
                GetSCSMessage = CType(num(ReturnData), String)
                'TxtWeight.Text 
                'TxtIndikator.Text = GetSCSMessage
                'LabelControl1.Text
                StKoneksiIndikator = "Conection Status : Successed Conected"

                tcpClient.Close()
            Else
                If Not networkStream.CanRead Then
                    'LabelControl1.Text 
                    StKoneksiIndikator = "Conection Status : Off"
                    tcpClient.Close()
                Else
                    If Not networkStream.CanWrite Then
                        'LabelControl1.Text 
                        StKoneksiIndikator = "Conection Status : On"
                        tcpClient.Close()
                    End If
                End If
                GetSCSMessage = ""
            End If
        Catch ex As Exception
            GetSCSMessage = ""
            ' MsgBox("Could not connect to SCS: " & ex.ToString, MsgBoxStyle.Critical, "Connection Error")
            'LabelControl1.Text 
            StKoneksiIndikator = "Conection Status : Faill Conected"
        End Try
    End Function
#End Region

End Module


'Provides a simple container object to be used for the state object passed to thIFe connection monitoring thread
Public Class MonitorInfo
    Public Property Cancel As Boolean
    Private _Connections As List(Of ConnectionInfo)
    Public ReadOnly Property Connections As List(Of ConnectionInfo)
        Get
            Return _Connections
        End Get
    End Property

    Private _Listener As TcpListener
    Public ReadOnly Property Listener As TcpListener
        Get
            Return _Listener
        End Get
    End Property

    Public Sub New(tcpListener As TcpListener, connectionInfoList As List(Of ConnectionInfo))
        _Listener = tcpListener
        _Connections = connectionInfoList
    End Sub
End Class

'Provides a container object to serve as the state object for async client and stream operations
Public Class ConnectionInfo
    'hold a reference to entire monitor instead of just the listener
    Private _Monitor As MonitorInfo
    Public ReadOnly Property Monitor As MonitorInfo
        Get
            Return _Monitor
        End Get
    End Property

    Private _Client As TcpClient
    Public ReadOnly Property Client As TcpClient
        Get
            Return _Client
        End Get
    End Property

    Private _Stream As NetworkStream
    Public ReadOnly Property Stream As NetworkStream
        Get
            Return _Stream
        End Get
    End Property

    Private _DataQueue As System.Collections.Concurrent.ConcurrentQueue(Of Byte)
    Public ReadOnly Property DataQueue As System.Collections.Concurrent.ConcurrentQueue(Of Byte)
        Get
            Return _DataQueue
        End Get
    End Property

    Private _LastReadLength As Integer
    Public ReadOnly Property LastReadLength As Integer
        Get
            Return _LastReadLength
        End Get
    End Property

    'The buffer size is an arbitrary value which should be selected based on the
    'amount of data you need to transmit, the rate of transmissions, and the
    'anticipalted number of clients. These are the considerations for designing
    'the communicaition protocol for data transmissions, and the size of the read
    'buffer must be based upon the needs of the protocol.
    Private _Buffer(63) As Byte

    Public Sub New(monitor As MonitorInfo)
        _Monitor = monitor
        _DataQueue = New System.Collections.Concurrent.ConcurrentQueue(Of Byte)
    End Sub

    Public Sub AcceptClient(result As IAsyncResult)
        _Client = _Monitor.Listener.EndAcceptTcpClient(result)
        If _Client IsNot Nothing AndAlso _Client.Connected Then
            _Stream = _Client.GetStream
        End If
    End Sub

    Public Sub AwaitData()
        _Stream.BeginRead(_Buffer, 0, _Buffer.Length, AddressOf DoReadData, Me)
    End Sub

    Private Sub DoReadData(result As IAsyncResult)
        Dim info As ConnectionInfo = CType(result.AsyncState, ConnectionInfo)
        Try
            'If the stream is valid for reading, get the current data and then
            'begin another async read
            If info.Stream IsNot Nothing AndAlso info.Stream.CanRead Then
                info._LastReadLength = info.Stream.EndRead(result)
                For index As Integer = 0 To _LastReadLength - 1
                    info._DataQueue.Enqueue(info._Buffer(index))
                Next

                'The example responds to all data reception with the number of bytes received;
                'you would likely change this behavior when implementing your own protocol.
                'ini  gak pake aja
                info.SendMessage("Received " & info._LastReadLength & " Bytes")

                For Each otherInfo As ConnectionInfo In info.Monitor.Connections
                    If Not otherInfo Is info Then
                        otherInfo.SendMessage(System.Text.Encoding.ASCII.GetString(info._Buffer))
                    End If
                Next

                info.AwaitData()
            Else
                'If we cannot read from the stream, the example assumes the connection is
                'invalid and closes the client connection. You might modify this behavior
                'when implementing your own protocol.
                info.Client.Close()
            End If
        Catch ex As Exception
            info._LastReadLength = -1
        End Try
    End Sub

    Private Sub SendMessage(message As String)
        If _Stream IsNot Nothing Then
            Dim messageData() As Byte = System.Text.Encoding.ASCII.GetBytes(message)
            Stream.Write(messageData, 0, messageData.Length)
        End If
    End Sub
End Class