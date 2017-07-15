Imports System.IO

Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors.Repository

Imports Devart.Data
Imports Devart.Data.Oracle
Imports Devart.Common
Public Class FrmTransporter
    Private Sub UnlockAll()
        TextEdit17.Enabled = False
        TextEdit72.Enabled = False
        TextEdit70.Enabled = False
        TextEdit67.Enabled = False
        TextEdit71.Enabled = False
        TextEdit69.Enabled = False
        TextEdit68.Enabled = False
        ComboBoxEdit9.Enabled = False
        TextEdit64.Enabled = False
        TextEdit30.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub LockAll()
        TextEdit17.Enabled = False
        TextEdit72.Enabled = False
        TextEdit70.Enabled = False
        TextEdit67.Enabled = False
        TextEdit71.Enabled = False
        TextEdit69.Enabled = False
        TextEdit68.Enabled = False
        ComboBoxEdit9.Enabled = False
        TextEdit64.Enabled = False
        TextEdit30.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub ClearInputTP()
        TextEdit17.Enabled = False
        TextEdit72.Enabled = False
        TextEdit70.Enabled = False
        TextEdit67.Enabled = False
        TextEdit71.Enabled = False
        TextEdit69.Enabled = False
        TextEdit68.Enabled = False
        ComboBoxEdit9.Enabled = False
        TextEdit64.Enabled = False
        TextEdit30.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl3.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"TRANSPORTER_CODE,TRANSPORTER_NAME,NPWP_TRANS,ADDRESS_TRANS,PHONE_TRANS,FAX_TRANS,CONTACT_PERSON_TRANS,ISACTIVE,INACTIVE_DATE,REMARKS_TRANS"}
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
        view.Columns("IMAGE").ColumnEdit = repItemGraphicsEdit

        'GROUPING
        Dim GridView As GridView = CType(GridControl3.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("ROLENAME"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
        GridView.BestFitColumns()
        GridView.ExpandAllGroups()

    End Sub
    Private Sub LoadView()
        SQL = ("SELECT TRANSPORTER_CODE AS TRANSPORTER_CODE,TRANSPORTER_NAME,NPWP_TRANS,ADDRESS_TRANS,PHONE_TRANS,FAX_TRANS,CONTACT_PERSON_TRANS,ISACTIVE,INACTIVE_DATE,REMARKS_TRANS FROM T_TRANSPORTER ORDER BY TRANSPORTER_CODE")
        GridControl3.DataSource = Nothing
        FILLGridView(SQL, GridControl3)
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'ADD TRANSPORTER
        UnlockAll()
        TextEdit17.Text = Val(Strings.Right(GetCode("TP"), 2))
        TextEdit17.Enabled = False

    End Sub
    Private Sub LoadUser()
        SQL = "SELECT TRANSPORTER_CODE,TRANSPORTER_NAME,NPWP_TRANS,ADDRESS_TRANS,PHONE_TRANS,FAX_TRANS,CONTACT_PERSON_TRANS,ISACTIVE,INACTIVE_DATE,REMARKS_TRANS" +
            "FROM T_TRANSPORTER A" +
            "LEFT JOIN TRANSPORTER_CODE B On A.TRANSPORTERCODE And B,aktif='Y' " +
            "WHERE A.AKTIF= 'Y'" +
            "ORDER BY TRANSPORTER_CODE"
        FILLGridView(SQL, GridControl3)

        GridControl3.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl3.FocusedView, GridView)
        GridView.ExpandAllGroups()
    End Sub



    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'SAVE TRANSPORTER
        If Not IsEmptyText({TextEdit17, TextEdit72, TextEdit70, TextEdit67, TextEdit71, TextEdit69, TextEdit68, ComboBoxEdit9, TextEdit64, TextEdit30}) Then
            SQL = " SELECT * FROM T_TRANSPORTER WHERE TRANSPORTER_CODE= " & TextEdit17.Text & "'"
            Dim TRANSPORTERCODE As String = TextEdit17.Text
            Dim TRANSPORTERNAME As String = TextEdit72.Text
            Dim NPWP As String = TextEdit70.Text
            Dim ADDRESS As String = TextEdit67.Text
            Dim PHONE As String = TextEdit71.Text
            Dim FAX As String = TextEdit69.Text
            Dim CONTACTPERSON As String = TextEdit68.Text
            Dim ISACTIVE As String = ComboBoxEdit9.SelectedItem
            Dim INACTIVEDATE As String = TextEdit64.Text
            Dim STATUS As String = TextEdit30.Text
            If CheckRecord(SQL) = 0 Then
                SQL = "INSERT INTO T_TRANSPORTER (TRANSPORTER_CODE,TRANSPORTER_NAME,NPWP_TRANS,ADDRESS_TRANS,PHONE_TRANS,FAX_TRANS,CONTACT_PERSON_TRANS,ISACTIVE,INACTIVE_DATE,REMARKS_TRANS)" +
            "VALUES ('" & TRANSPORTERCODE & "','" & TRANSPORTERNAME & "','" & NPWP & "','" & ADDRESS & "','" & PHONE & "','" & FAX & "','" & CONTACTPERSON & "','" & ISACTIVE & "','" & INACTIVEDATE & "','" & STATUS & "',)"
                ExecuteNonQuery(SQL)
                SQL = "SELECT FROM T_TRANSPORTER WHERE TRANSPORTER_CODE='" & TextEdit17.Text & "'"
                If CheckRecord(SQL) > 0 Then UpdateCode("TP")
                LoadView()
                MsgBox("SAVE SUCCEEDED", vbInformation, "TRANSPORTER")
                UnlockAll()
                ClearInputTP()
            Else
                SQL = "UPDATE T_TRANSPORTER SET TRANSPORTERCODE='" & TRANSPORTERCODE & "',TRANSPORTER_NAME='" & TRANSPORTERNAME & "',NPWP='" & NPWP & "',ADDRESS_TRANS='" & ADDRESS & "',PHONE_TRANS='" & PHONE & "',FAX_TRANS='" & FAX & "',CONTACT_PERSON_TRANS='" & CONTACTPERSON & "',ISACTIVE='" & ISACTIVE & "',INACTIVE_DATE='" & INACTIVEDATE & "',REMARKS_TRANS='" & STATUS & "'," +
                 " WHERE TRANSPORTER_CODE= '" & TextEdit17.Text & "'"
                ExecuteNonQuery(SQL)
                MsgBox("SAVE SUCCEDED", vbInformation, "TRANSPORTER")
            End If
        End If
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'DELETE TRANSPORTER
        SQL = "UPDATE T_TRANSPORTER SET AKTIF= 'N' WHERE TRANSPORTER_CODE'" & TextEdit17.Text & "'"
        LoadUser()
        MsgBox("Delete Successful", vbInformation, "TRANSPORTER")
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'CANCEL TRANSPORTER
        TextEdit17.Text = ""
        TextEdit72.Text = ""
        TextEdit70.Text = ""
        TextEdit67.Text = ""
        TextEdit71.Text = ""
        TextEdit69.Text = ""
        TextEdit68.Text = ""
        ComboBoxEdit9.SelectedItem = ""
        TextEdit64.Text = ""
        TextEdit30.Text = ""
    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        'CLOSE
        Me.Close()
    End Sub

    Private Sub FrmTransporter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "TRANSPORTER"
        Gridheader()
        LockAll()
    End Sub
End Class