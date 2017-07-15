Imports System.IO

Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors.Repository

Imports Devart.Data
Imports Devart.Data.Oracle
Imports Devart.Common
Public Class FrmVehicleType
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'ADD VEHICLE TYPE
        UnlockAll()
        TextEdit74.Text = Val(Strings.Right(GetCode("VT"), 2))
        TextEdit74.Enabled = False
    End Sub

    Private Sub UnlockAll()
        TextEdit74.Enabled = False
        TextEdit81.Enabled = False

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub LockAll()
        TextEdit74.Enabled = False
        TextEdit81.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close

    End Sub
    Private Sub ClearInputVT()
        TextEdit74.Text = ""
        TextEdit81.Text = ""
        SimpleButton1.Enabled = False
        SimpleButton3.Enabled = False
    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl5.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"VEHICLE_TYPE", "TOLERANCE"}
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
        Dim GridView As GridView = CType(GridControl5.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("ROLENAME"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
        GridView.BestFitColumns()
        GridView.ExpandAllGroups()

    End Sub
    Private Sub LoadView()
        'vt
        SQL = ("SELECT VEHICLE_CODE AS VEHICLETYPECODE,TOLERANCE FROM T_VEHICLET ORDER BY VEHICLE_CODE ")
        GridControl5.DataSource = Nothing
        FILLGridView(SQL, GridControl5)
    End Sub
    Private Sub LoadUser()
        SQL = "SELECT VEHICLE_CODE,TOLERANCE" +
            "FROM T_VHECILET A" +
            "LEFT JOIN VEHICLE_CODE B On A.VEHICLETYPECODE And B.aktif='Y' " +
            "WHERE A.AKTIF='Y'" +
            "ORDER BY VEHICLE_CODE"
        FILLGridView(SQL, GridControl5)

        GridControl5.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl5.FocusedView, GridView)
        GridView.ExpandAllGroups()

    End Sub
    Private Sub FrmVehicleType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "VEHICLETYPE"
        GridHeader()
        LockAll()
    End Sub


    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'SAVE VEHICLE TYPE
        If Not IsEmptyCombo({TextEdit74}) Then
            If Not IsEmptyText({TextEdit74, TextEdit81}) Then
                SQL = " SELECT * FROM T_VEHICLET WHERE VEHICLE_CODE='" & TextEdit74.Text & "'"
                Dim VEHICLETYPECODE As String = TextEdit74.Text
                Dim TOLERANCE As String = TextEdit81.Text
                If CheckRecord(SQL) = 0 Then
                    SQL = "INSERT INTO T_VEHICLET (VEHICLE_CODE,TOLERANCE)" +
                    " VALUES('" & VEHICLETYPECODE & "','" & TOLERANCE & "')"
                    ExecuteNonQuery(SQL)
                    SQL = "SELECT FROM T_VEHICLET WHERE VEHICLE_CODE='" & TextEdit74.Text & "'"
                    If CheckRecord(SQL) > 0 Then UpdateCode("VT")
                    LoadView()
                    MsgBox("SAVE SUCCEDED", vbInformation, "VEHICLE_CODE")
                    UnlockAll()
                    ClearInputVT()
                Else
                    SQL = "UPDATE T_VEHICLET SET VEHICLE_CODE='" & VEHICLETYPECODE & "',TOLERANCE='" & TOLERANCE & "'," +
                 " WHERE VEHICLE_CODE= '" & TextEdit74.Text & "'"
                    ExecuteNonQuery(SQL)
                    MsgBox("SAVE SUCCEDED", vbInformation, "VEHICLE TYPE")
                End If
            End If
        End If
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        ' DELETE VEHICLE TYPE
        SQL = "UPDATE T_VEHICLET SET AKTIF= 'N' WHERE VEHICLE_TYPE'" & TextEdit74.Text & "'"
        ExecuteNonQuery(SQL)
        LoadUser()
        MsgBox("Delete Successful", vbInformation, "VEHICLETYPE")
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'cancel
        TextEdit74.Text = ""
        TextEdit81.Text = ""
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()

    End Sub
End Class