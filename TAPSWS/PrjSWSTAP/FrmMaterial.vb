
Imports System.IO

Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors.Repository

Imports Devart.Data
Imports Devart.Data.Oracle
Imports Devart.Common
Public Class FrmMaterial
    Private Sub FrmMaterial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "MATERIAL"
        GridHeader()
        LOADUSER()
        lockAll()
    End Sub
    Private Sub ClearInputMT()
        TextEdit107.Text = ""
        TextEdit110.Text = ""
        TextEdit62.Text = ""

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close

    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl8.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"MATERIAL_CODE", "MATERIAL_NAME", "MATERIAL_TYPE"}
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
        Dim GridView As GridView = CType(GridControl8.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("ROLENAME"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
        GridView.BestFitColumns()
        GridView.ExpandAllGroups()

    End Sub
    Private Sub LOADUSER()
        SQL = "SELECT MATERIAL_CODE,MATERIAL_NAME,MATERIAL_TYPE" +
            "FORM T_MATERIAL A" +
            "LEFT JOIN MATERIAL_CODE B On A.MATERIAL_NAME AnD B.AKTIF='Y'" +
            "WHERE A.AKTIF='Y'" +
            "ORDER BY MATERIAL_CODE"
        FILLGridView(SQL, GridControl8)
        GridControl8.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl8.FocusedView, GridView)
        GridView.ExpandAllGroups()

    End Sub
    ' Private Sub LoadView()
    '    SQL = ("SELECT VMATERIAL_CODE AS MATERIAL_CODE,MATERIAL_NAME,MATERIAL_TYPE FROM T_MATERIAL ORDER BY MATERIAL_CODE ")
    '  GridControl8.DataSource = Nothing
    ' FILLGridView(SQL, GridControl8)
    'End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'save
        If Not IsEmptyText({TextEdit107, TextEdit110, TextEdit62}) = True Then

            Dim MATERIALCODE As String = TextEdit62.Text
            Dim MATERIALNAME As String = TextEdit110.Text
            Dim MATERIALTYPE As String = TextEdit107.Text
            'TRY
            SQL = "SELECT *FROM T_MATERIAL WHERE AKTIF='Y' AND MATERIAL_CODE='" & TextEdit62.Text & "'"
            If CheckRecord(SQL) = 0 Then

                SQL = "INSERT INTO T_MATERIAL (MATERIAL_CODE,MATERIAL_NAME,MATERIAL_TYPE)" +
                        "VALUES ('" & MATERIALCODE & "','" & MATERIALNAME & "','" & MATERIALTYPE & "')"
                ExecuteNonQuery(SQL)
                LOADUSER()
                MsgBox("SAVE SUCCEDED", vbInformation, "MATERIAL_CODE")

                If CheckRecord(SQL) > 0 Then UpdateCode("MT")
                LOADUSER()
                MsgBox("SAVE SUCCEDED", vbInformation, "MATERIAL_CODE")
                UnlockAll()
                CLearInputMT()
            Else
                SQL = "UPDATE T_MATERIAL SET MATERIAL_CODE'" & MATERIALCODE & "',MATERIAL_NAME='" & MATERIALNAME & "',MATERIALTYPE'" & MATERIALTYPE & "'," +
                        "WHERE MATERIAL_CODE='" & TextEdit62.Text & "'"
                ExecuteNonQuery(SQL)
                MsgBox("SAVE SUCCEDED", vbInformation, "MATERIAL_CODE")
                ClearInputMT()

            End If
        End If
    End Sub

    Private Sub lockAll()
        TextEdit107.Enabled = False
        TextEdit110.Enabled = False
        TextEdit62.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub UnlockAll()
        TextEdit107.Enabled = True
        TextEdit110.Enabled = True
        TextEdit62.Enabled = True

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = True 'save
        SimpleButton3.Enabled = True 'delete
        SimpleButton4.Enabled = True 'cancel
        SimpleButton5.Enabled = True 'close
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'ADD
        UnlockAll()
        TextEdit62.AllowHtmlTextInToolTip = Val(Strings.Right(GetCode("MT"), 2))
        TextEdit62.Enabled = False
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'DELETE
        SQL = "UPDATE T_MATERIAL SET AKTIF= 'N' WHERE MATERIAL_CODE'" & TextEdit62.Text & "'"
        ExecuteNonQuery(SQL)
        LOADUSER()
        MsgBox("Delete Successful", vbInformation, "MATERIAL")
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'CANCEL
        ClearInputMT()
        lockAll()
        SimpleButton2.Text = "SAVE" 'SAVE
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()
    End Sub
    Private Sub GridView8_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GridView8.RowCellClick
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

            TextEdit62.Text = GridView8.GetRowCellValue(e.RowHandle, "MATERIAL_CODE").ToString() 'MATERIALCODE
            TextEdit110.Text = GridView8.GetRowCellValue(e.RowHandle, "MATERIAL_NAME").ToString() 'MATERIALNAME
            TextEdit107.Text = GridView8.GetRowCellValue(e.RowHandle, "MATERIAL_TYPE").ToString() 'MATERIALTYPE

            TextEdit62.Enabled = False
            UnlockAll()

        End If
    End Sub
End Class