Imports System.IO

Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors.Repository

Imports Devart.Data
Imports Devart.Data.Oracle
Imports Devart.Common
Public Class FrmPurchaseContract

    Private Sub FrmPurchaseContract_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Purchase Contract"
        LoadUser()
        GridHeader()
        lockAll()
    End Sub
    Private Sub UnlockAll()
        TextEdit88.Enabled = True
        TextEdit105.Enabled = True
        TextEdit103.Enabled = True
        TextEdit100.Enabled = True
        TextEdit104.Enabled = True
        TextEdit102.Enabled = True
        TextEdit101.Enabled = True
        TextEdit108.Enabled = True
        TextEdit97.Enabled = True
        TextEdit94.Enabled = True
        ComboBoxEdit12.Enabled = True
        TextEdit112.Enabled = True
        TextEdit114.Enabled = True

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = True 'save
        SimpleButton3.Enabled = True 'delete
        SimpleButton4.Enabled = True 'cancel
        SimpleButton5.Enabled = True 'close
    End Sub
    Private Sub lockAll()
        TextEdit88.Enabled = False
        TextEdit105.Enabled = False
        TextEdit103.Enabled = False
        TextEdit100.Enabled = False
        TextEdit104.Enabled = False
        TextEdit102.Enabled = False
        TextEdit101.Enabled = False
        TextEdit108.Enabled = False
        TextEdit97.Enabled = False
        TextEdit94.Enabled = False
        ComboBoxEdit12.Enabled = False
        TextEdit112.Enabled = False
        TextEdit114.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub ClearInputPC()
        TextEdit88.Text = ""
        TextEdit105.Text = ""
        TextEdit103.Text = ""
        TextEdit100.Text = ""
        TextEdit104.Text = ""
        TextEdit102.Text = ""
        TextEdit101.Text = ""
        TextEdit108.Text = ""
        TextEdit97.Text = ""
        TextEdit94.Text = ""
        ComboBoxEdit12.Text = ""
        TextEdit112.Text = ""
        TextEdit114.Text = ""

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl7.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"CONTRACT_NUMBER,DOCUMENT_TYPE,CONTRACT_STARTDATE,CONTRACT_ENDDATE,VENDOR_CODE,INCOTERMS1,INCOTERMS2,ITEMNO,MATERIAL_CODE,FLAT_RATE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,GRADING,STATUS"}
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
        Dim GridView As GridView = CType(GridControl7.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("ROLENAME"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
        GridView.BestFitColumns()
        GridView.ExpandAllGroups()

    End Sub
    Private Sub LoadUser()
        SQL = "SELECT CONTRACT_NUMBER AS CONTRACT_NUMBER,DOCUMENT_TYPE,CONTRACT_STARTDATE,CONTRACT_ENDDATE,VENDOR_CODE,INCOTERMS1,INCOTERMS2,ITEMNO,MATERIAL_CODE,FLAT_RATE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,GRADING,STATUS" +
            "FROM T_PURCHASECONTRACT A" +
            "LEFT JOIN CONTRACT_NUMBER B On A.DOCUMENT_TYPE And B.Aktif='Y' " +
            "WHERE A.Aktif='Y'" +
            "ORDER BY CONTRACT_NUMBER"
        FILLGridView(SQL, GridControl7)

        GridControl7.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl7.FocusedView, GridView)
        GridView.ExpandAllGroups()

    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'save purchase contract
        If Not IsEmptyText({TextEdit88, TextEdit105, TextEdit103, TextEdit100, TextEdit104, TextEdit102, TextEdit101, TextEdit108, TextEdit97, TextEdit94, TextEdit98, TextEdit96, TextEdit90, TextEdit92, ComboBoxEdit12}) Then
            SQL = "SELECT *FROM T_PURCHASECONTRACT WHERE" & TextEdit88.Text & "'"
            Dim CONTRACTN As String = TextEdit88.Text
            Dim DOCTYPE As String = TextEdit105.Text
            Dim CONTRACTSTARTDATE As String = TextEdit103.Text
            Dim CONTRACTENDDATE As String = TextEdit100.Text
            Dim VENDORCODE As String = TextEdit104.Text
            Dim INCOTERMS1 As String = TextEdit102.Text
            Dim INCOTERMS2 As String = TextEdit101.Text
            Dim ITEMNO As String = TextEdit108.Text
            Dim MATERIALCODE As String = TextEdit97.Text
            Dim FLATRATE As String = TextEdit94.Text
            Dim INACTIVE As String = ComboBoxEdit12.SelectedItem
            Dim INACTIVEDATE As String = TextEdit112.Text
            Dim STATUS As String = TextEdit114.Text
            If CheckRecord(SQL) = 0 Then
                SQL = "INSERT INTO T_CONTRACT (CONTRACT_NUMBER,DOCUMENT_TYPE,CONTRACT_STARTDATE,CONTRACT_ENDDATE,VENDOR_CODE,INCOTERMS1,INCOTERMS2,ITEMNO,MATERIAL_CODE,FLAT_RATE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,GRADING,STATUS)" +
                    " VALUES('" & CONTRACTN & "','" & DOCTYPE & "','" & CONTRACTSTARTDATE & "','" & CONTRACTENDDATE & "','" & VENDORCODE & "','" & INCOTERMS1 & "','" & INCOTERMS2 & "','" & ITEMNO & "','" & MATERIALCODE & "','" & FLATRATE & "','" & INACTIVE & "','" & INACTIVEDATE & "','" & STATUS & "')"
                ExecuteNonQuery(SQL)
                LoadUser()
                SQL = "SELECT FROM T_PURCHASECONTRACT WHERE CONTRACT_NUMBER='" & TextEdit88.Text & "'"
                If CheckRecord(SQL) > 0 Then UpdateCode("PC")
                LoadUser()
                MsgBox("SAVE SUCCEEDED", vbInformation, "PURCHASECONTRACT")
                UnlockAll()
                ClearInputPC()
            Else
                SQL = "UPDATE T_PURCHASECONTRACT SET CONTRACT_NUMBER='" & CONTRACTN & "',DOCUMENT_TYPE='" & DOCTYPE & "',CONTRACT_STARTDATE='" & CONTRACTSTARTDATE & "',CONTRACT_ENDDATE='" & CONTRACTENDDATE & "',VENDOR_CODE='" & VENDORCODE & "',INCOTERM1='" & INCOTERMS1 & "',INCOTERMS2='" & INCOTERMS2 & "',ITEMNO='" & ITEMNO & "',MATERIAL_CODE='" & MATERIALCODE & "',FLATRATE='" & FLATRATE & "',INACTIVE='" & INACTIVE & "',INACTIVE_DATE='" & INACTIVE & "',STATUS='" & STATUS & "'" +
                 " WHERE T_PURCHASECONTRACT= '" & TextEdit88.Text & "'"
                ExecuteNonQuery(SQL)
            End If
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'ADD
        UnlockAll()
        TextEdit88.Text = Val(Strings.Right(GetCode("PC"), 2))
        TextEdit88.Enabled = False
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'DELETE
        SQL = "UPDATE T_PURCHASECONTRACT SET AKTIF= 'N' WHERE CONTRACT_NUMBER'" & TextEdit88.Text & "'"
        ExecuteNonQuery(SQL)
        LoadUser()
        MsgBox("Delete Successful", vbInformation, "PURCHASECONTRACT")
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'CANCEL
        TextEdit88.Text = ""
        TextEdit105.Text = ""
        TextEdit103.Text = ""
        TextEdit100.Text = ""
        TextEdit104.Text = ""
        TextEdit102.Text = ""
        TextEdit101.Text = ""
        TextEdit108.Text = ""
        TextEdit97.Text = ""
        TextEdit94.Text = ""
        ComboBoxEdit12.Text = ""
        TextEdit112.Text = ""
        TextEdit114.Text = ""
        ClearInputPC()
        lockAll()
        SimpleButton2.Text = "SAVE" 'SAVE
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()

    End Sub
    Private Sub GridView7_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GridView7.RowCellClick
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
            Dim CONTRACTN As String = TextEdit88.Text = GridView7.GetRowCellValue(e.RowHandle, "CONTRACT_NUMMBER").ToString()
            Dim DOCTYPE As String = TextEdit105.Text = GridView7.GetRowCellValue(e.RowHandle, "CONTRACT_TYPE").ToString()
            Dim CONTRACTSTARTDATE As String = TextEdit103.Text = GridView7.GetRowCellValue(e.RowHandle, "CONTRACT_STARTDATE").ToString()
            Dim CONTRACTENDDATE As String = TextEdit100.Text = GridView7.GetRowCellValue(e.RowHandle, "CONTRACT_ENDDATE").ToString()
            Dim VENDORCODE As String = TextEdit104.Text = GridView7.GetRowCellValue(e.RowHandle, "VENDOR_CODE").ToString()
            Dim INCOTERMS1 As String = TextEdit102.Text = GridView7.GetRowCellValue(e.RowHandle, "INCOTERMS1").ToString()
            Dim INCOTERMS2 As String = TextEdit101.Text = GridView7.GetRowCellValue(e.RowHandle, "INCOTRERMS2").ToString()
            Dim ITEMNO As String = TextEdit108.Text = GridView7.GetRowCellValue(e.RowHandle, "ITEMNO").ToString()
            Dim MATERIALCODE As String = TextEdit97.Text = GridView7.GetRowCellValue(e.RowHandle, "MATERIAL_CODE").ToString()
            Dim FLATRATE As String = TextEdit94.Text = GridView7.GetRowCellValue(e.RowHandle, "FLAT_RATE").ToString()
            Dim INACTIVE As String = ComboBoxEdit12.SelectedItem = GridView7.GetRowCellValue(e.RowHandle, "INACTIVE").ToString()
            Dim INACTIVEDATE As String = TextEdit112.Text = GridView7.GetRowCellValue(e.RowHandle, "INACTIVE_DATE").ToString()
            Dim STATUS As String = TextEdit114.Text = GridView7.GetRowCellValue(e.RowHandle, "STATUS").ToString()

        End If
    End Sub
End Class