Imports System.IO

Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors.Repository

Imports Devart.Data
Imports Devart.Data.Oracle
Imports Devart.Common
Public Class FrmVehicle
    Private Sub UnlockAll()
        TextEdit15.Enabled = True
        TextEdit28.Enabled = True
        TextEdit23.Enabled = True
        TextEdit28.Enabled = True
        TextEdit25.Enabled = True
        TextEdit22.Enabled = True
        ComboBoxEdit1.Enabled = True
        TextEdit18.Enabled = True
        TextEdit21.Enabled = True

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = True 'save
        SimpleButton3.Enabled = True 'delete
        SimpleButton4.Enabled = True 'cancel
        SimpleButton5.Enabled = True 'close
    End Sub
    Private Sub LockAll()
        TextEdit15.Enabled = False
        TextEdit28.Enabled = False
        TextEdit23.Enabled = False
        TextEdit28.Enabled = False
        TextEdit25.Enabled = False
        TextEdit22.Enabled = False
        ComboBoxEdit1.Enabled = False
        TextEdit18.Enabled = False
        TextEdit21.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub CLearInputVC()
        TextEdit15.Enabled = ""
        TextEdit28.Enabled = ""
        TextEdit23.Enabled = ""
        TextEdit28.Enabled = ""
        TextEdit25.Enabled = ""
        TextEdit22.Enabled = ""
        ComboBoxEdit1.Enabled = ""
        TextEdit18.Enabled = ""
        TextEdit21.Enabled = ""

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub LoadView()
        'vt
        SQL = ("SELECT VEHICLE_CODE AS  VEHICLE_CODE,VEHICLE_TYPE,PLATE_NUMBER,TARRE,OWNERSHIP,TRANSPORTER_CODE,INACTIVE,INACTIVE_DATE,STATUS FROM T_VEHICLE ORDER BY VEHICLE_CODE ")
        GridControl4.DataSource = Nothing
        FILLGridView(SQL, GridControl4)
    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl4.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"VEHICLE_CODE,VEHICLE_TYPE,PLATE_NUMBER,TARRE,OWNERSHIP,TRANSPORTER_CODE,INACTIVE,INACTIVE_DATE,STATUS"}
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
        Dim GridView As GridView = CType(GridControl4.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("ROLENAME"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
        GridView.BestFitColumns()
        GridView.ExpandAllGroups()

    End Sub
    Private Sub LoadUser()
        SQL = "SELECT VEHICLE_CODE,VEHICLE_TYPE,PLATE_NUMBER,TARRE,OWNERSHIP,TRANSPORTER_CODE,INACTIVE,INACTIVE_DATE,STATUS" +
            "FROM T_VEHICLE A" +
            "LEFT JOIN VEHICLE_CODE B On A.VEHICLE_TYPE And B.aktif='Y'" +
            "WHERE .AKTIF='Y'" +
            "ORDER BY VEHICLE_CODE"
        FILLGridView(SQL, GridControl4)

        GridControl4.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl4.FocusedView, GridView)
        GridView.ExpandAllGroups()

    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'SAVE VEHICLE
        If Not IsEmptyText({TextEdit15, TextEdit28, TextEdit23, TextEdit20, TextEdit25, TextEdit22, TextEdit18, TextEdit21}) = True Then
            If Not IsEmptyCombo({ComboBoxEdit1}) = True Then
                SQL = " SELECT * FROM T_VEHICLE WHERE " & TextEdit15.Text & "'"
                Dim VEHICLECODE As String = TextEdit15.Text
                Dim VEHICLETYPE As String = TextEdit28.Text
                Dim PLATENUMBER As String = TextEdit23.Text
                Dim TARRA As String = TextEdit28.Text
                Dim OWNERSHIP As String = TextEdit25.Text
                Dim TRANSPORTERCODE As String = TextEdit22.Text
                Dim ISACTIVE As String = ComboBoxEdit1.SelectedText
                Dim INACTIVEDATE As String = TextEdit18.Text
                Dim STATUS As String = TextEdit21.Text
                If CheckRecord(SQL) = 0 Then
                    SQL = "INSERT INTO T_VEHICLE (VEHICLE_CODE,VEHICLE_TYPE,PLATE_NUMBER,TARRE,OWNERSHIP,TRANSPORTER_CODE,INACTIVE,INACTIVE_DATE,STATUS)" +
                "VALUES ('" & VEHICLECODE & "','" & VEHICLETYPE & "','" & PLATENUMBER & "','" & TARRA & "','" & OWNERSHIP & "','" & ISACTIVE & "','" & STATUS & "')"
                    ExecuteNonQuery(SQL)
                    LoadUser()
                    MsgBox("Insert Successful", vbInformation, "Vehicle")
                    If CheckRecord(SQL) > 0 Then UpdateCode("VC")
                    LoadUser()
                    MsgBox("SAVE SUCCEEDED", vbInformation, "VEHICLE")
                    UnlockAll()
                    CLearInputVC()
                Else
                    SQL = "UPDATE T_VEHICLE SET VEHICLE_CODE='" & VEHICLECODE & "',VEHICLE_TYPE='" & VEHICLETYPE & "',PLATE_NUMBER='" & PLATENUMBER & "',OWNERSHIP='" & OWNERSHIP & "',TRANSPORTER_CODE='" & TRANSPORTERCODE & "',INACTIVE='" & ISACTIVE & "',INACTIVE_DATE='" & INACTIVEDATE & "',STATUS='" & STATUS & "'" +
                 " WHERE VEHICLECODE= '" & TextEdit15.Text & "'"
                    ExecuteNonQuery(SQL)
                    MsgBox("SAVE SUCCEEDED", vbInformation, "VEHICLE")
                    CLearInputVC()
                End If
            End If
        End If
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'delete
        SQL = "UPDATE T_VEHICLE SET AKTIF= 'N' WHERE VEHICLECODE'" & TextEdit15.Text & "'"
        LoadUser()
        MsgBox("delete Successful", vbInformation, "VEHICLE")
    End Sub
    Private Sub FrmVehicle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "VEHICLE"
        GridHeader()
        LoadUser()
        LockAll()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'add
        UnlockAll()
        TextEdit15.Text = Val(Strings.Right(GetCode("VC"), 2))
        TextEdit15.Enabled = False
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'CANCEL
        TextEdit15.Enabled = ""
        TextEdit28.Enabled = ""
        TextEdit23.Enabled = ""
        TextEdit28.Enabled = ""
        TextEdit25.Enabled = ""
        TextEdit22.Enabled = ""
        ComboBoxEdit1.Enabled = ""
        TextEdit18.Enabled = ""
        TextEdit21.Enabled = ""
        UnlockAll()
        SimpleButton2.Text = "SAVE" 'SAVE
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()
    End Sub
    Private Sub GridView4_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GridView4.RowCellClick
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

            TextEdit15.Text = GridView4.GetRowCellValue(e.RowHandle, "VEHICLE_CODE").ToString() 'VEHICLECODE
            TextEdit28.Text = GridView4.GetRowCellValue(e.RowHandle, "VEHICLE_TYPE").ToString() 'VEHICLETYPE
            TextEdit23.Text = GridView4.GetRowCellValue(e.RowHandle, "PLATE_NUMBER").ToString() 'PLATENUMBER
            TextEdit28.Text = GridView4.GetRowCellValue(e.RowHandle, "TARRE").ToString()  'TARRE
            TextEdit25.Text = GridView4.GetRowCellValue(e.RowHandle, "OWNERSHIP").ToString() 'OWNERSHIP
            TextEdit22.Text = GridView4.GetRowCellValue(e.RowHandle, "TRANSPORTER_CODE").ToString() 'TRANSPORTERCODE
            ComboBoxEdit1.SelectedText = GridView4.GetRowCellValue(e.RowHandle, "INACTIVE").ToString() 'INACTIVE
            TextEdit18.Text = GridView4.GetRowCellValue(e.RowHandle, "INACTIVE_DATE").ToString() 'INACTIVEDATE
            TextEdit21.Text = GridView4.GetRowCellValue(e.RowHandle, "STATUS").ToString() 'STATUS

            TextEdit15.Enabled = False
            UnlockAll()
        End If
    End Sub
End Class