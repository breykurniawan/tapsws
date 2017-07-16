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
        TextEdit17.Enabled = True
        TextEdit72.Enabled = True
        TextEdit70.Enabled = True
        TextEdit67.Enabled = True
        TextEdit71.Enabled = True
        TextEdit69.Enabled = True
        TextEdit68.Enabled = True
        ComboBoxEdit9.Enabled = True
        TextEdit64.Enabled = True
        TextEdit30.Enabled = True

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = True 'save
        SimpleButton3.Enabled = True 'delete
        SimpleButton4.Enabled = True 'cancel
        SimpleButton5.Enabled = True 'close
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
        Dim fieldNames() As String = New String() {"TRANSPORTER_CODE,TRANSPORTER_NAME,NPWP,ADDRESS,PHONE,FAX,CONTACT_PERSON,INACTIVE,INACTIVE_DATE,STATUS"}
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
        SQL = ("SELECT TRANSPORTER_CODE AS TRANSPORTER_CODE,TRANSPORTER_NAME,NPWP,ADDRESS,PHONE,FAX,CONTACT_PERSON,INACTIVE,INACTIVE_DATE,STATUS FROM T_TRANSPORTER ORDER BY TRANSPORTER_CODE")
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
        SQL = "SELECT TRANSPORTER_CODE,TRANSPORTER_NAME,NPWP,ADDRESS,PHONE,FAX,CONTACT_PERSON,INACTIVE,INACTIVE_DATE,STATUS" +
            "FROM T_TRANSPORTER A" +
            "LEFT JOIN TRANSPORTER_CODE B On A.TRANSPORTER_CODE And B,aktif='Y' " +
            "WHERE A.AKTIF= 'Y'" +
            "ORDER BY TRANSPORTER_CODE"
        FILLGridView(SQL, GridControl3)

        GridControl3.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl3.FocusedView, GridView)
        GridView.ExpandAllGroups()
    End Sub



    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'SAVE TRANSPORTER
        If Not IsEmptyText({TextEdit17, TextEdit72, TextEdit70, TextEdit67, TextEdit71, TextEdit69, TextEdit68, TextEdit64, TextEdit30}) = True Then
            If Not IsEmptyCombo({ComboBoxEdit9}) = True Then


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
                    SQL = "INSERT INTO T_TRANSPORTER (TRANSPORTER_CODE,TRANSPORTER_NAME,NPWP,ADDRESS,PHONE,FAX,CONTACT_PERSON,INACTIVE,INACTIVE_DATE,STATUS)" +
                "VALUES ('" & TRANSPORTERCODE & "','" & TRANSPORTERNAME & "','" & NPWP & "','" & ADDRESS & "','" & PHONE & "','" & FAX & "','" & CONTACTPERSON & "','" & ISACTIVE & "','" & INACTIVEDATE & "','" & STATUS & "',)"
                    ExecuteNonQuery(SQL)
                    LoadUser()
                    MsgBox("Insert Successful", vbInformation, "Transporter")
                    SQL = "SELECT FROM T_TRANSPORTER WHERE TRANSPORTER_CODE='" & TextEdit17.Text & "'"
                    If CheckRecord(SQL) > 0 Then UpdateCode("TP")
                    LoadUser()
                    MsgBox("SAVE SUCCEEDED", vbInformation, "TRANSPORTER")
                    UnlockAll()
                    ClearInputTP()
                Else
                    SQL = "UPDATE T_TRANSPORTER SET TRANSPORTER_CODE='" & TRANSPORTERCODE & "',TRANSPORTER_NAME='" & TRANSPORTERNAME & "',NPWP='" & NPWP & "',ADDRESS='" & ADDRESS & "',PHONE='" & PHONE & "',FAX='" & FAX & "',CONTACT_PERSON='" & CONTACTPERSON & "',INACTIVE='" & ISACTIVE & "',INACTIVE_DATE='" & INACTIVEDATE & "',STATUS='" & STATUS & "'," +
                     " WHERE TRANSPORTER_CODE= '" & TextEdit17.Text & "'"
                    ExecuteNonQuery(SQL)
                    MsgBox("UPDATE SUCCEDED", vbInformation, "TRANSPORTER")
                    ClearInputTP()
                End If
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
        LockAll()
        SimpleButton2.Text = "SAVE" 'SAVE
    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        'CLOSE
        Me.Close()
    End Sub

    Private Sub FrmTransporter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "TRANSPORTER"
        GridHeader()
        LoadUser()
        LockAll()
    End Sub
    Private Sub GridView3_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GridView3.RowCellClick
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
            TextEdit17.Text = GridView3.GetRowCellValue(e.RowHandle, "TRANSPORTER_CODE").ToString() 'TRANSPORTERCODE
            TextEdit72.Text = GridView3.GetRowCellValue(e.RowHandle, "TRANSPORTER_NAME").ToString() 'TRANSPORTERNAME
            TextEdit70.Text = GridView3.GetRowCellValue(e.RowHandle, "NPWP").ToString() 'NPWP
            TextEdit67.Text = GridView3.GetRowCellValue(e.RowHandle, "ADDRESS").ToString() 'ADDRESS
            TextEdit71.Text = GridView3.GetRowCellValue(e.RowHandle, "PHONE").ToString()  'PHONE
            TextEdit69.Text = GridView3.GetRowCellValue(e.RowHandle, "FAX").ToString()  'FAX
            TextEdit68.Text = GridView3.GetRowCellValue(e.RowHandle, "CONTACT_PERSON").ToString()  'CONTACTPERSON
            ComboBoxEdit9.SelectedItem = GridView3.GetRowCellValue(e.RowHandle, "INACTIVE").ToString()  'INACTIVE
            TextEdit64.Text = GridView3.GetRowCellValue(e.RowHandle, "INACTIVE_DATE").ToString() 'INACTIVEDATE
            TextEdit30.Text = GridView3.GetRowCellValue(e.RowHandle, "STATUS").ToString() 'STATUS

            TextEdit17.Enabled = False
            UnlockAll()

        End If
    End Sub
End Class