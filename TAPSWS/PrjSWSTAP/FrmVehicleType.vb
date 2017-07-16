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
        TextEdit74.Enabled = True
        TextEdit81.Enabled = True

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = True 'save
        SimpleButton3.Enabled = True 'delete
        SimpleButton4.Enabled = True 'cancel
        SimpleButton5.Enabled = True 'close
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
        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
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
    ' Private Sub LoadView()
    'vt
    '    SQL = ("SELECT VEHICLE_CODE AS VEHICLETYPECODE,TOLERANCE FROM T_VEHICLET ORDER BY VEHICLE_CODE ")
    '   GridControl5.DataSource = Nothing
    '  FILLGridView(SQL, GridControl5)
    'End Sub
    Private Sub LoadUser()
        SQL = "SELECT VEHICLE_TYPE,TOLERANCE" +
            "FROM T_VHECILET A" +
            "LEFT JOIN VEHICLE_TYPE B On A.VEHICLETYPECODE And B.aktif='Y' " +
            "WHERE A.AKTIF='Y'" +
            "ORDER BY VEHICLE_TYPE"
        FILLGridView(SQL, GridControl5)

        GridControl5.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl5.FocusedView, GridView)
        GridView.ExpandAllGroups()

    End Sub
    Private Sub FrmVehicleType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "VEHICLETYPE"
        GridHeader()
        LoadUser()
        LockAll()
    End Sub


    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'SAVE VEHICLE TYPE
        If Not IsEmptyText({TextEdit74, TextEdit81}) = True Then
            SQL = " SELECT * FROM T_VEHICLETYPE WHERE VEHICLE_CODE='" & TextEdit74.Text & "'"
            Dim VEHICLETYPECODE As String = TextEdit74.Text
            Dim TOLERANCE As String = TextEdit81.Text
            If CheckRecord(SQL) = 0 Then
                SQL = "INSERT INTO T_VEHICLETYPE (VEHICLE_TYPE,TOLERANCE)" +
                " VALUES('" & VEHICLETYPECODE & "','" & TOLERANCE & "')"
                ExecuteNonQuery(SQL)
                LoadUser()
                MsgBox("Insert  Successful", vbInformation, "VehicleType")

                If CheckRecord(SQL) > 0 Then UpdateCode("VT")
                LoadUser()
                MsgBox("SAVE SUCCEDED", vbInformation, "VEHICLE_CODE")
                UnlockAll()
                ClearInputVT()
            Else
                SQL = "UPDATE T_VEHICLETYPE SET VEHICLE_TYPE='" & VEHICLETYPECODE & "',TOLERANCE='" & TOLERANCE & "'," +
             " WHERE VEHICLE_CODE= '" & TextEdit74.Text & "'"
                ExecuteNonQuery(SQL)
                MsgBox("UPDATE SUCCEDED", vbInformation, "VEHICLE TYPE")
                ClearInputVT()
            End If
        End If

    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        ' DELETE VEHICLE TYPE
        SQL = "UPDATE T_VEHICLETYPE SET AKTIF= 'N' WHERE VEHICLE_TYPE'" & TextEdit74.Text & "'"
        ExecuteNonQuery(SQL)
        LoadUser()
        MsgBox("Delete Successful", vbInformation, "VEHICLETYPE")
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'cancel
        TextEdit74.Text = ""
        TextEdit81.Text = ""
        SimpleButton2.Text = "SAVE" 'SAVE
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()

    End Sub
    Private Sub GridView5_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GridView5.RowCellClick
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

            TextEdit74.Text = GridView5.GetRowCellValue(e.RowHandle, "VEHICLE_TYPE").ToString() 'VEHICLE_TYPE
            TextEdit81.Text = GridView5.GetRowCellValue(e.RowHandle, "TOLECRANCE").ToString() 'TOLERANCE

            TextEdit74.Enabled = False
            UnlockAll()
        End If
    End Sub
End Class