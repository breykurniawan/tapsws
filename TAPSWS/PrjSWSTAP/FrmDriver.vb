Imports System.IO

Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors.Repository

Imports Devart.Data
Imports Devart.Data.Oracle
Imports Devart.Common
Public Class FrmDriver
    Private Sub UnlockAll()
        TextEdit61.Enabled = True
        TextEdit111.Enabled = True
        TextEdit109.Enabled = True
        TextEdit106.Enabled = True
        ComboBoxEdit13.Enabled = True
        TextEdit115.Enabled = True
        TextEdit89.Enabled = True

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = True 'save
        SimpleButton3.Enabled = True 'delete
        SimpleButton4.Enabled = True 'cancel
        SimpleButton5.Enabled = True 'close
    End Sub
    Private Sub LockAll()
        TextEdit61.Enabled = False
        TextEdit111.Enabled = False
        TextEdit109.Enabled = False
        TextEdit106.Enabled = False
        ComboBoxEdit13.Enabled = False
        TextEdit115.Enabled = False
        TextEdit89.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub ClearInputDR()
        TextEdit61.Text = ""
        TextEdit111.Text = ""
        TextEdit109.Text = ""
        TextEdit106.Text = ""
        ComboBoxEdit13.Text = ""
        TextEdit115.Text = ""
        TextEdit89.Text = ""

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl8.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"DRIVER_CODE", "TRANSPORTER_CODE", "DRIVER_NAME", "SIM"}
        Dim I As Integer
        Dim Column As DevExpress.XtraGrid.Columns.GridColumn

        view.Columns.Clear()
        For I = 0 To fieldNames.Length - 1
            Column = view.Columns.AddField(fieldNames(I))
            Column.VisibleIndex = I
        Next

        'Dim repItemGraphicsEdit As New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
        'repItemGraphicsEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        'repItemGraphicsEdit.BestFitWidth = 50
        'view.Columns("IMAGE").ColumnEdit = repItemGraphicsEdit

        'GROUPING
        Dim GridView As GridView = CType(GridControl8.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("TRANSPORTER_CODE"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
        GridView.BestFitColumns()
        GridView.ExpandAllGroups()

    End Sub
    Private Sub LoadView()
        'vt
        SQL = ("SELECT DRIVER_CODE AS DRIVER_CODE,TRANSPORTER_CODE,DRIVER_NAME,SIM,INACTIVE,INACTIVEDATE,STATUS FROM T_DRIVER ORDER BY DRIVER_CODE ")
        GridControl8.DataSource = Nothing
        FILLGridView(SQL, GridControl8)
    End Sub
    Private Sub LoadUser()
        SQL = "SELECT DRIVER_CODE,TRANSPORTER_CODE,DRIVER_NAME,SIM,INACTIVE,INACTIVEDATE,STATUS" +
            "FROM T_DRIVER A" +
            "LEFT JOIN DRIVER_CODE B On A.TRANSPORTER_CODE And B.aktif='Y'" +
            "WHERE A.Aktif='Y'" +
            "ORDER BY DRIVER_CODE"
        FILLGridView(SQL, GridControl8)

        GridControl8.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl8.FocusedView, GridView)
        GridView.ExpandAllGroups()
    End Sub


    Private Sub FrmDriver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "DRIVER"
        GridHEader()
        LockAll()
    End Sub


    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'SAVE DRIVER
        If Not IsEmptyText({TextEdit61, TextEdit111, TextEdit109, TextEdit106, ComboBoxEdit13, TextEdit115, TextEdit89}) Then
            SQL = "SELECT *FROM T_DRIVER WHERE DRIVER_CODE=" & TextEdit61.Text & "'"
            Dim DRIVERCODE As String = TextEdit61.Text
            Dim DRIVERNAME As String = TextEdit111.Text
            Dim TRANSPORTERCODE As String = TextEdit109.Text
            Dim LINCENSENUMBER As String = TextEdit106.Text
            Dim INACTIVE As String = ComboBoxEdit13.SelectedItem
            Dim INACTIVEDATE As String = TextEdit115.Text
            Dim STATUS As String = TextEdit89.Text
            If CheckRecord(SQL) = 0 Then
                SQL = "INSERT INTO T_DRIVER (DRIVER_CODE,TRANSPORTER_CODE,DRIVER_NAME,SIM,INACTIVE,INACTIVEDATE,STATUS)" +
                    " VALUES('" & DRIVERCODE & "','" & DRIVERNAME & "','" & TRANSPORTERCODE & "','" & LINCENSENUMBER & "','" & INACTIVE & "','" & INACTIVEDATE & "',)"
                ExecuteNonQuery(SQL)
                SQL = "SELECT FROM T_DRIVER WHERE DRIVER_CODE='" & TextEdit61.Text & "'"
                If CheckRecord(SQL) > 0 Then UpdateCode("DR")
                LoadView()
                MsgBox("SAVE SUCCEEDED", vbInformation, "DRIVER")
                UnlockAll()
                ClearInputDR()
            Else
                SQL = "UPDATE T_DRIVER SET DRIVER_CODE='" & DRIVERCODE & "',DRIVER_NAME='" & DRIVERNAME & "',TRANSPORTER_CODE='" & TRANSPORTERCODE & "',TRANSPOTER_CODE='" & TRANSPORTERCODE & "',SIM='" & LINCENSENUMBER & "',INACTIVE='" & INACTIVE & "',INACTIVE_DATE='" & INACTIVEDATE & "',STATUS='" & STATUS & "'," +
                 " WHERE T_DRIVER= '" & TextEdit61.Text & "'"
                ExecuteNonQuery(SQL)
                MsgBox("SAVE SUCCEEDED", vbInformation, "DRIVER")
            End If
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'ADD
        UnlockAll()
        TextEdit61.Text = Val(Strings.Right(GetCode("DR"), 2))
        TextEdit61.Enabled = False
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'delete
        SQL = "UPDATE T_DRIVER SET AKTIF= 'N' WHERE DRIVER_CODE'" & TextEdit61.Text & "'"
        ExecuteNonQuery(SQL)
        LoadUser()
        MsgBox("Delete Successful", vbInformation, "DRIVER")
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'CANCEL
        TextEdit61.Text = ""
        TextEdit111.Text = ""
        TextEdit109.Text = ""
        TextEdit106.Text = ""
        ComboBoxEdit13.Text = ""
        TextEdit115.Text = ""
        TextEdit89.Text = ""
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()
    End Sub
End Class