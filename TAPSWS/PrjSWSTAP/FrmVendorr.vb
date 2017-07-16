Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading.Tasks
Imports System.Text.RegularExpressions ' Namespace untuk manipulasi registry
Imports System.Text

Imports Devart.Data
Imports Devart.Data.Oracle
Imports Devart.Common

Imports DevExpress
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Public Class FrmVendorr

    Private Sub UnlockAll()
        TextEdit6.Enabled = True
        TextEdit58.Enabled = True
        TextEdit58.Enabled = True
        TextEdit58.Enabled = True
        TextEdit56.Enabled = True
        TextEdit53.Enabled = True
        TextEdit57.Enabled = True
        TextEdit55.Enabled = True
        TextEdit54.Enabled = True
        TextEdit52.Enabled = True
        TextEdit50.Enabled = True
        TextEdit47.Enabled = True
        TextEdit51.Enabled = True
        TextEdit49.Enabled = True
        TextEdit48.Enabled = True
        TextEdit9.Enabled = True
        TextEdit95.Enabled = True
        TextEdit13.Enabled = True
        ComboBoxEdit2.Enabled = True
        TextEdit8.Enabled = True

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = True 'save
        SimpleButton3.Enabled = True 'delete
        SimpleButton4.Enabled = True 'cancel
        SimpleButton5.Enabled = True 'close
    End Sub
    Private Sub LockAll()
        TextEdit6.Enabled = False
        TextEdit58.Enabled = False
        TextEdit58.Enabled = False
        TextEdit58.Enabled = False
        TextEdit56.Enabled = False
        TextEdit53.Enabled = False
        TextEdit57.Enabled = False
        TextEdit55.Enabled = False
        TextEdit54.Enabled = False
        TextEdit52.Enabled = False
        TextEdit50.Enabled = False
        TextEdit47.Enabled = False
        TextEdit51.Enabled = False
        TextEdit49.Enabled = False
        TextEdit48.Enabled = False
        TextEdit9.Enabled = False
        TextEdit95.Enabled = False
        TextEdit13.Enabled = False
        ComboBoxEdit2.Enabled = False
        TextEdit8.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'SAVE VENDOR
        If Not IsEmptyText({TextEdit6, TextEdit58, TextEdit56, TextEdit53, TextEdit57, TextEdit55, TextEdit54, TextEdit52, TextEdit50, TextEdit47, TextEdit51, TextEdit49, TextEdit48, TextEdit95, TextEdit13, TextEdit9, TextEdit8}) = True Then
            If Not IsEmptyCombo({ComboBoxEdit2}) = True Then
                Dim VENCODE As String = TextEdit6.Text
                Dim VENNAME As String = TextEdit58.Text
                Dim NPWP As String = TextEdit56.Text
                Dim EMAIL As String = TextEdit53.Text
                Dim ADDRESS As String = TextEdit57.Text
                Dim POSTCODE As String = TextEdit55.Text
                Dim STATE As String = TextEdit54.Text
                Dim COUNTRY As String = TextEdit52.Text
                Dim PHONE As String = TextEdit50.Text
                Dim MPHONE As String = TextEdit47.Text
                Dim FAX As String = TextEdit51.Text
                Dim CP As String = TextEdit49.Text
                Dim BANKACC As String = TextEdit48.Text
                Dim ACCOUNTGROUP As String = TextEdit95.Text
                Dim ACCOUNTGROUPN As String = TextEdit13.Text
                Dim INPUT_BY As String = USERNAME
                Dim INPUT_DATE As String = Now
                Dim UPDATE_BY As String = USERNAME
                Dim UPDATE_DATE As String = Now
                Dim INACTIVEDATE As String = Now
                Dim ISACTIVE As String = ComboBoxEdit2.SelectedItem
                Dim STATUS As String = TextEdit8.Text
                SQL = "SELECT *FROM T_VENDOR WHERE VENDOR_CODE='" & TextEdit6.Text & "'"
                If CheckRecord(SQL) = 0 Then
                    SQL = "INSERT INTO T_VENDOR (VENDOR_CODE,VENDOR_NAME,NPWP,EMAIL,ADDRESS,POSTAL_CODE,STATE,COUNTRY,PHONE,MOBILEPHONE,FAX,CONTRACTPERSON,BANKACCOUNT,ACCOUNTGROUP,ACCOUNTGROUPN,INPUTBY,INPUTDATE,UPDATENY,UPDATEDATE,INACTIVEDATE,ISACTIVE,STATUS)" +
                        "VALUES('" & VENCODE & "','" & VENNAME & "','" & NPWP & "','" & EMAIL & "','" & ADDRESS & "','" & POSTCODE & "','" & STATE & "','" & COUNTRY & "','" & PHONE & "','" & MPHONE & "','" & FAX & "','" & CP & "','" & BANKACC & "','" & ACCOUNTGROUP & "','" & ACCOUNTGROUPN & "','" & ISACTIVE & "','" & STATUS & "',)"
                    ExecuteNonQuery(SQL)
                    LoadUser()
                    MsgBox("Insert  Successful", vbInformation, "Vendor")

                    If CheckRecord(SQL) > 0 Then UpdateCode("VN")
                    LoadUser()
                    MsgBox("SAVE SUCCEDED", vbInformation, "Vendor")
                    UnlockAll()
                    ClearInputVN()
                Else
                    SQL = "UPDATE T_VENDOR SET VENDOR_CODE='" & VENCODE & "',VENDOR_NAME='" & VENNAME & "',NPWP='" & NPWP & "',EMAIL='" & EMAIL & "',ADDRESS='" & ADDRESS & "',POSTAL_CODE='" & POSTCODE & "',STATE='" & STATE & "',COUNTRY='" & COUNTRY & "',PHONE='" & PHONE & "',MOBILEPHONE='" & MPHONE & "',FAX='" & FAX & "',CONTACTPERSON='" & CP & "',,BANKACCOUNT='" & BANKACC & "',ACCOUNTGROUP='" & ACCOUNTGROUP & "',ACCOUNTGROUPN='" & ACCOUNTGROUPN & "',INACTIVEDATE='" & INACTIVEDATE & "',ISACTIVE='" & ISACTIVE & "',STATUS='" & STATUS & "'" +
                       " WHERE VENDOR_CODE= '" & TextEdit6.Text & "'"
                    ExecuteNonQuery(SQL)
                    MsgBox("update SUCCEDED", vbInformation, "VENDOR")
                    ClearInputVN()
                End If
            End If
        End If
    End Sub
    Private Sub ClearInputVN()
        TextEdit6.Enabled = False
        TextEdit58.Enabled = False
        TextEdit58.Enabled = False
        TextEdit58.Enabled = False
        TextEdit56.Enabled = False
        TextEdit53.Enabled = False
        TextEdit57.Enabled = False
        TextEdit55.Enabled = False
        TextEdit54.Enabled = False
        TextEdit52.Enabled = False
        TextEdit50.Enabled = False
        TextEdit47.Enabled = False
        TextEdit51.Enabled = False
        TextEdit49.Enabled = False
        TextEdit48.Enabled = False
        TextEdit95.Enabled = False
        TextEdit13.Enabled = False
        ComboBoxEdit2.Enabled = False
        TextEdit8.Enabled = False

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl1.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"VENDOR_CODE", "VENDOR_NAME", "NPWP", "EMAIL", "ADDRESS", "POSTAL_CODE", "STATE", "COUNTRY", "PHONE", "MOBILEPHONNE", "FAX", "CONTACTPERSON", "BANKACCOUNT", "ACCOUNTGROUP", "ACCOUNTGROUPN", "INPUTBY", "INPUTDATE", "UPDATEBY", "UPDATEDATE", "INACTIVEDATE", "ISACTIVE", "STATUS"}
        Dim I As Integer
        Dim Column As DevExpress.XtraGrid.Columns.GridColumn

        view.Columns.Clear()
        For I = 0 To fieldNames.Length - 1
            Column = view.Columns.AddField(fieldNames(I))
            Column.VisibleIndex = I
        Next

        Dim repItemGraphicsEdit As New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
        repItemGraphicsEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        repItemGraphicsEdit.BestFitWidth = 50


        'GROUPING
        Dim GridView As GridView = CType(GridControl1.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("ROLENAME"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
        GridView.BestFitColumns()
        GridView.ExpandAllGroups()

    End Sub
    Private Sub LoadUser()
        SQL = "SELECT VENDOR_CODE,VENDOR_NAME,NPWP,EMAIL,ADDRESS,POSTAL_CODE,STATE,COUNTRY,PHONE,MOBILEPHONE,FAX,CONTRACTPERSON,BANKACCOUNT,ACCOUNTGROUP,ACCOUNTGROUPN,INPUTBY,INPUTDATE,UPDATENY,UPDATEDATE,INACTIVEDATE,ISACTIVE,STATUS" +
            "FROM T_VENDOR A" +
            "LEFT JOIN VENDOR_CODE B On A.VENDOR_NAME And B.aktif='Y'" +
            "WHERE A.Aktif='Y'" +
            "ORDER BY VENDOR_CODE"
        FILLGridView(SQL, GridControl1)
        GridControl1.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl1.FocusedView, GridView)
        GridView.ExpandAllGroups()

    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'ADD
        UnlockAll()
        TextEdit6.Text = Val(Strings.Right(GetCode("VN"), 2))
        TextEdit6.Enabled = False
    End Sub
    Private Sub LoadView()
        SQL = ("SELECT VENDOR_CODE AS VENDOR_CODE,VENDOR_NAME,NPWP,EMAIL,ADDRESS,POSTAL_CODE,STATE,COUNTRY,PHONE,MOBILEPHONE,FAX,CONTRACTPERSON,BANKACCOUNT,ACCOUNTGROUP,ACCOUNTGROUPN,INPUTBY,INPUTDATE,UPDATENY,UPDATEDATE,INACTIVEDATE,ISACTIVE,STATUS FROM T_VENODR ORDER BY VENDOR_CODE ")
        GridControl1.DataSource = Nothing
        FILLGridView(SQL, GridControl1)
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'DELETE
        SQL = "UPDATE T_VENDOR SET AKTIF= 'N' WHERE VENDOR_CODE'" & TextEdit6.Text & "'"
        ExecuteNonQuery(SQL)
        LoadUser()
        MsgBox("delete Successful", vbInformation, "VENDOR")
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'CANCEL
        TextEdit6.Enabled = ""
        TextEdit58.Enabled = ""
        TextEdit58.Enabled = ""
        TextEdit58.Enabled = ""
        TextEdit56.Enabled = ""
        TextEdit53.Enabled = ""
        TextEdit57.Enabled = ""
        TextEdit55.Enabled = ""
        TextEdit54.Enabled = ""
        TextEdit52.Enabled = ""
        TextEdit50.Enabled = ""
        TextEdit47.Enabled = ""
        TextEdit51.Enabled = ""
        TextEdit49.Enabled = ""
        TextEdit48.Enabled = ""
        TextEdit95.Enabled = ""
        TextEdit13.Enabled = ""
        ComboBoxEdit2.Enabled = ""
        TextEdit8.Enabled = ""
        LockAll()
        SimpleButton2.Text = "SAVE" 'SAVE
    End Sub
    Private Sub FrmVendorr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "VENDOR"
        LoadUser()
        GridHeader()
        LockAll()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()
    End Sub
    Private Sub GridView1_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GridView1.RowCellClick
        If e.RowHandle < 0 Then
            SimpleButton1.Enabled = True 'add
            SimpleButton2.Enabled = False 'save
            SimpleButton3.Enabled = False 'delete
            SimpleButton4.Enabled = True 'cancel
            SimpleButton5.Enabled = False 'close
        Else
            SimpleButton1.Enabled = False 'add
            SimpleButton2.Enabled = True 'save
            SimpleButton3.Enabled = True 'delete
            SimpleButton4.Enabled = True 'cancel
            SimpleButton5.Enabled = False 'close

            SimpleButton1.Text = "update" 'save

            TextEdit6.Text = GridView1.GetRowCellValue(e.RowHandle, "VENDOR_CODE").ToString()
            TextEdit58.Text = GridView1.GetRowCellValue(e.RowHandle, "VENDOR_NAME").ToString()
            TextEdit56.Text = GridView1.GetRowCellValue(e.RowHandle, "NPWP").ToString()
            TextEdit53.Text = GridView1.GetRowCellValue(e.RowHandle, "EMAIL").ToString()
            TextEdit57.Text = GridView1.GetRowCellValue(e.RowHandle, "ADDRESS").ToString()
            TextEdit55.Text = GridView1.GetRowCellValue(e.RowHandle, "POSTAL_CODE").ToString()
            TextEdit54.Text = GridView1.GetRowCellValue(e.RowHandle, "STATE").ToString()
            TextEdit52.Text = GridView1.GetRowCellValue(e.RowHandle, "COUNTRY").ToString()
            TextEdit50.Text = GridView1.GetRowCellValue(e.RowHandle, "PHONE").ToString()
            TextEdit47.Text = GridView1.GetRowCellValue(e.RowHandle, "MOBILEPHONE").ToString()
            TextEdit51.Text = GridView1.GetRowCellValue(e.RowHandle, "FAX").ToString()
            TextEdit49.Text = GridView1.GetRowCellValue(e.RowHandle, "CONTACTPERSON").ToString()
            TextEdit48.Text = GridView1.GetRowCellValue(e.RowHandle, "BANKACCOUNT").ToString()
            TextEdit95.Text = GridView1.GetRowCellValue(e.RowHandle, "ACCOUNTGROUP").ToString()
            TextEdit13.Text = GridView1.GetRowCellValue(e.RowHandle, "ACCOUNTGROUPN").ToString()
            ComboBoxEdit2.SelectedItem = GridView1.GetRowCellValue(e.RowHandle, "ISACTIVE").ToString()
            TextEdit8.Text = GridView1.GetRowCellValue(e.RowHandle, "STATUS").ToString()

            TextEdit6.Enabled = False
            UnlockAll()
        End If
    End Sub

End Class
