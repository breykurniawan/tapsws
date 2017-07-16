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
        TextEdit1.Enabled = True
        TextEdit2.Enabled = True
        TextEdit3.Enabled = True
        ComboBoxEdit1.Enabled = True
        TextEdit4.Enabled = True

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = True 'save
        SimpleButton3.Enabled = False 'delete
    End Sub
    Private Sub LockAll()
        TextEdit1.Enabled = False
        TextEdit2.Enabled = False
        TextEdit3.Enabled = False
        ComboBoxEdit1.Enabled = False
        TextEdit4.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
    End Sub
    Private Sub ClearInput()
        TextEdit1.Text = ""
        TextEdit2.Text = ""
        TextEdit3.Text = ""
        ComboBoxEdit1.Text = ""
        TextEdit4.Text = ""
    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl8.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"DRIVER_CODE", "DRIVER_NAME", "TRANSPORTER_NAME", "LICENSE_NUMBER", "STATUS"}
        Dim I As Integer
        Dim Column As DevExpress.XtraGrid.Columns.GridColumn

        view.Columns.Clear()
        For I = 0 To fieldNames.Length - 1
            Column = view.Columns.AddField(fieldNames(I))
            Column.VisibleIndex = I
        Next

        'GROUPING & ORDER BY
        Dim GridView As GridView = CType(GridControl8.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("TRANSPORTER_NAME"), DevExpress.Data.ColumnSortOrder.Ascending),
        New GridColumnSortInfo(GridView.Columns("DRIVER_CODE"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
        GridView.BestFitColumns()
        GridView.ExpandAllGroups()

    End Sub
    Private Sub LoadDriver()
        SQL = " SELECT DRIVER_CODE,TRANSPORTER_NAME,DRIVER_NAME,LICENSE_NUMBER,STATUS" +
            " From T_DRIVER A " +
            " LEFT JOIN T_TRANSPORTER B ON A.TRANSPORTER_CODE=B.TRANSPORTER_CODE And B.INACTIVE='N'" +
            " Where A.INACTIVE='N' " +
            " Order By TRANSPORTER_NAME, DRIVER_CODE"
        FILLGridView(SQL, GridControl8)
        GridControl8.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl8.FocusedView, GridView)
        GridView.ExpandAllGroups()
    End Sub

    Private Sub FrmDriver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "DRIVER"
        GridHeader()
        LoadDriver()
        LockAll()
        'MODUL INI DI PAKAI BUAT CHECK STATUS SITE SAP/BUKAN
        If StatusSite = True Or SAP = "" Then
            PanelControl6.Enabled = False  'PANEL CRUDE
        End If
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'SAVE DRIVER
        If Not IsEmptyText({TextEdit1, TextEdit2, TextEdit3, TextEdit4}) Then
            If Not IsEmptyCombo({ComboBoxEdit1}) Then
                Dim DRIVERCODE As String = Trim(TextEdit1.Text)
                Dim DRIVERNAME As String = TextEdit2.Text
                Dim TRANSPORTERCODE As String = GetCodeTrans(ComboBoxEdit1.Text)
                Dim LINCENSENUMBER As String = TextEdit3.Text
                Dim STATUS As String = TextEdit4.Text
                SQL = "Select * FROM T_DRIVER WHERE DRIVER_CODE='" & TextEdit1.Text & "'"
                If CheckRecord(SQL) = 0 Then
                    SQL = "INSERT INTO T_DRIVER (DRIVER_CODE,DRIVER_NAME,TRANSPORTER_CODE,LICENSE_NUMBER,INACTIVE,INACTIVE_DATE,STATUS)" +
                    " VALUES('" & DRIVERCODE & "','" & DRIVERNAME & "','" & TRANSPORTERCODE & "','" & LINCENSENUMBER & "','N',SYSDATE,'" & STATUS & "')"
                    ExecuteNonQuery(SQL)
                    SQL = "SELECT * FROM T_DRIVER WHERE DRIVER_CODE='" & TextEdit1.Text & "' AND INACTIVE='N'"
                    If CheckRecord(SQL) > 0 Then UpdateCode("DR")
                    LoadDriver()
                    MsgBox("Save Successful", vbInformation, "DRIVER")
                    UnlockAll()
                    ClearInput()
                Else
                    SQL = "UPDATE T_DRIVER SET DRIVER_CODE='" & DRIVERCODE & "',DRIVER_NAME='" & DRIVERNAME & "',TRANSPORTER_CODE='" & TRANSPORTERCODE & "',TRANSPOTER_CODE='" & TRANSPORTERCODE & "',LICENSE_NUMBER='" & LINCENSENUMBER & "',INACTIVE='Y',INACTIVE_DATE=SYSDATE,STATUS='" & STATUS & "'," +
                    " WHERE T_DRIVER= '" & TextEdit1.Text & "' AND INACTIVE='N'"
                    ExecuteNonQuery(SQL)
                    MsgBox("Update Successful", vbInformation, "DRIVER")
                End If
            End If
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'ADD
        UnlockAll()
        TextEdit1.Text = GetCode("DR")
        TextEdit1.Enabled = False
        TextEdit2.Text = ""
        TextEdit3.Text = ""
        ComboBoxEdit1.Text = ""
        TextEdit4.Text = ""
        'UNTUK INPUTAN HANYA NUMERIC (FORMAT UANG)
        'TextEdit3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'delete
        SQL = "UPDATE T_DRIVER SET INAKTIF= 'Y' WHERE DRIVER_CODE'" & TextEdit1.Text & "'"
        ExecuteNonQuery(SQL)
        LoadDriver()
        MsgBox("Delete Successful", vbInformation, "DRIVER")
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'CANCEL
        TextEdit1.Text = ""
        TextEdit2.Text = ""
        TextEdit3.Text = ""
        TextEdit4.Text = ""
        ComboBoxEdit1.Text = ""
        SimpleButton2.Text = "Save"
        LockAll()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()
    End Sub

    Private Sub TextEdit3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit3.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub GridView8_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GridView8.RowCellClick
        If e.RowHandle < 0 Then
            SimpleButton1.Enabled = True 'add
            SimpleButton2.Enabled = False 'save
            SimpleButton3.Enabled = False 'del
        Else
            SimpleButton1.Enabled = False 'ADD
            SimpleButton2.Enabled = True 'SAVE
            SimpleButton3.Enabled = True 'DEL

            SimpleButton2.Text = "Update" 'SAVE

            TextEdit1.Text = GridView8.GetRowCellValue(e.RowHandle, "DRIVER_CODE").ToString()  'ID
            TextEdit2.Text = GridView8.GetRowCellValue(e.RowHandle, "DRIVER_NAME").ToString() 'NAME
            ComboBoxEdit1.Text = GridView8.GetRowCellValue(e.RowHandle, "TRANSPORTER_NAME").ToString() 'NAME
            TextEdit3.Text = GridView8.GetRowCellValue(e.RowHandle, "LICENSE_NUMBER").ToString() 'SIM
            TextEdit4.Text = GridView8.GetRowCellValue(e.RowHandle, "STATUS").ToString() 'STATUS

            TextEdit1.Enabled = False
            UnlockAll()
        End If
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class