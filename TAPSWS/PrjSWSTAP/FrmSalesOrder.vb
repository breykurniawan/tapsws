Imports System.IO

Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors.Repository

Imports Devart.Data
Imports Devart.Data.Oracle
Imports Devart.Common
Public Class FrmSalesOrder
    Private Sub FrmSalesOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "SalesOrder"
        GridHeader()
        LoadUser()
        LockAll()

    End Sub
    Private Sub UnlockAll()
        TextEdit59.Enabled = True
        TextEdit87.Enabled = True
        TextEdit85.Enabled = True
        TextEdit82.Enabled = True
        TextEdit86.Enabled = True
        TextEdit84.Enabled = True
        TextEdit83.Enabled = True
        TextEdit80.Enabled = True
        TextEdit78.Enabled = True
        TextEdit75.Enabled = True
        TextEdit79.Enabled = True
        TextEdit77.Enabled = True
        TextEdit76.Enabled = True
        TextEdit73.Enabled = True
        ComboBoxEdit10.Enabled = True
        TextEdit99.Enabled = True
        ComboBoxEdit11.Enabled = True

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = True 'save
        SimpleButton3.Enabled = True 'delete
        SimpleButton4.Enabled = True 'cancel
        SimpleButton5.Enabled = True 'close

    End Sub
    Private Sub LockAll()
        TextEdit59.Enabled = False
        TextEdit87.Enabled = False
        TextEdit85.Enabled = False
        TextEdit82.Enabled = False
        TextEdit86.Enabled = False
        TextEdit84.Enabled = False
        TextEdit83.Enabled = False
        TextEdit80.Enabled = False
        TextEdit78.Enabled = False
        TextEdit75.Enabled = False
        TextEdit79.Enabled = False
        TextEdit77.Enabled = False
        TextEdit76.Enabled = False
        TextEdit73.Enabled = False
        ComboBoxEdit10.Enabled = False
        TextEdit99.Enabled = False
        ComboBoxEdit11.Enabled = False

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close

    End Sub
    Private Sub ClearInputSO()
        TextEdit59.Text = ""
        TextEdit87.Text = ""
        TextEdit85.Text = ""
        TextEdit82.Text = ""
        TextEdit86.Text = ""
        TextEdit84.Text = ""
        TextEdit83.Text = ""
        TextEdit80.Text = ""
        TextEdit78.Text = ""
        TextEdit75.Text = ""
        TextEdit79.Text = ""
        TextEdit77.Text = ""
        TextEdit76.Text = ""
        TextEdit73.Text = ""
        ComboBoxEdit10.Text = ""
        TextEdit99.Text = ""
        ComboBoxEdit11.Text = ""

        SimpleButton1.Enabled = True 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl6.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"SO_NUMBER,SO_QUANTITY,SO_STARTDATE,SO_ENDDATE,CUSTOMER_CODE,CUSTOMER_NAME,SALDO,STATUS,INCOTERMS1,INCOTERMS2,ITEMNO,SALES_CONTRACT_NUMBER,MATERIAL_CODE,TOLERANCE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,COMPLETE"}
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
        Dim GridView As GridView = CType(GridControl6.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("ROLENAME"), DevExpress.Data.ColumnSortOrder.Ascending)}, 1)
        GridView.BestFitColumns()
        GridView.ExpandAllGroups()

    End Sub
    Private Sub LoadView()

        SQL = ("SELECT SONUMBER AS SONUMBER,SOQUANTITY,SOSTARTDATE,SOENDDATE,CUST_CODE,CUST_NAME,SALDO,STATUS,INCOTERMS1,INCOTERMS2,ITEMNO,SELES_CONTRACTN,MATERIAL_CODE,TOLERANCE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,COMPLETE FROM T_SALESORDER ORDER BY SONUMBER")
        GridControl6.DataSource = Nothing
        FILLGridView(SQL, GridControl6)
    End Sub
    Private Sub LoadUser()
        SQL = "SELECT SO_NUMBER,SO_QUANTITY,SO_STARTDATE,SO_ENDDATE,CUSTOMER_CODE,CUSTOMER_NAME,SALDO,STATUS,INCOTERMS1,INCOTERMS2,ITEMNO,SALES_CONTRACT_NUMBER,MATERIAL_CODE,TOLERANCE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,COMPLETE" +
            "FROM T_SALESORDER A" +
            "LEFT JOIN SO_NUMBER B On A.SO_QUANTITY And B.aktif= 'Y' " +
            "WHERE A.aktif= 'Y' " +
            "ORDER BY SO_NUMBER"
        FILLGridView(SQL, GridControl6)

        GridControl6.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl6.FocusedView, GridView)
        GridView.ExpandAllGroups()

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'SAVE
        If Not IsEmptyText({TextEdit59, TextEdit87, TextEdit85, TextEdit82, TextEdit86, TextEdit84, TextEdit83, TextEdit80, TextEdit78, TextEdit75, TextEdit79, TextEdit77, TextEdit76, TextEdit73, ComboBoxEdit10, TextEdit99}) = True Then
            If Not IsEmptyCombo({ComboBoxEdit10, ComboBoxEdit11}) Then
                SQL = "SELECT *FROM T_SALESORDER WHERE" & TextEdit59.Text & "'"
                Dim SONUMBER As String = TextEdit59.Text
                Dim SOQUANTITY As String = TextEdit87.Text
                Dim SOSTARTDATE As String = TextEdit85.Text
                Dim SOENDDATE As String = TextEdit82.Text
                Dim CUSTCODE As String = TextEdit86.Text
                Dim CUSTNAME As String = TextEdit84.Text
                Dim SALDO As String = TextEdit83.Text
                Dim STATUS As String = TextEdit80.Text
                Dim INCOTERMS1 As String = TextEdit78.Text
                Dim INCOTERMS2 As String = TextEdit75.Text
                Dim ITEMNO As String = TextEdit79.Text
                Dim SCNUMBER As String = TextEdit77.Text
                Dim MATERIALCODE As String = TextEdit76.Text
                Dim TOLERANCE As String = TextEdit73.Text
                Dim INACTIVE As String = ComboBoxEdit10.SelectedItem
                Dim INACTIVEDATE As String = TextEdit99.Text
                Dim COMPLETE As String = ComboBoxEdit11.SelectedItem
                If CheckRecord(SQL) = 0 Then
                    SQL = "INSERT INTO T_SALESORDER (SO_NUMBER,SO_QUANTITY,SO_STARTDATE,SO_ENDDATE,CUSTOMER_CODE,CUSTOMER_NAME,SALDO,STATUS,INCOTERMS1,INCOTERMS2,ITEMNO,SELES_CONTRACT_NUMBER,MATERIAL_CODE,TOLERANCE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,COMPLETE)" +
                    " VALUES('" & SONUMBER & "','" & SOQUANTITY & "','" & SOSTARTDATE & "',,'" & SOENDDATE & "','" & CUSTCODE & "','" & CUSTNAME & "','" & SALDO & "','" & STATUS & "','" & INCOTERMS1 & "','" & INCOTERMS2 & "','" & ITEMNO & "','" & SCNUMBER & "','" & MATERIALCODE & "','" & TOLERANCE & "','" & INACTIVEDATE & "','" & COMPLETE & "')"
                    ExecuteNonQuery(SQL)
                    LoadUser()
                    MsgBox("SAVE SUCCEEDED", vbInformation, "SALESORDER")
                    SQL = "SELECT FROM T_SALESORDER WHERE SONUMBER='" & TextEdit59.Text & "'"
                    If CheckRecord(SQL) > 0 Then UpdateCode("SO")
                    LoadUser()
                    MsgBox("SAVE SUCCEEDED", vbInformation, "SALESORDER")
                    UnlockAll()
                    ClearInputSO()
                Else
                    SQL = "UPDATE T_SALESORDER SET SO_NUMBER='" & SONUMBER & "',SO_QUANTITY='" & SOQUANTITY & "',SO_SATARTDATE='" & SOSTARTDATE & "',SO_ENDDATE='" & SOENDDATE & "',CUSTOMER_CODE='" & CUSTCODE & "',CUSTOMER_NAME='" & CUSTNAME & "',SALDO ='" & SALDO & "',STATUS='" & STATUS & "',INCOTERMS1='" & INCOTERMS1 & "',INCOTERMS2='" & INCOTERMS2 & "',ITEMNO='" & ITEMNO & "',SALES_CONTRART_NUMBER='" & SCNUMBER & "',MATERIAL_CODE='" & MATERIALCODE & "',TOLERANCE='" & TOLERANCE & "',INACTIVE='" & INACTIVE & "',INACTIVE_DATE='" & INACTIVEDATE & "',COMPLETE='" & COMPLETE & "'" +
                 " WHERE SO_NUMBER= '" & TextEdit59.Text & "'"
                    ExecuteNonQuery(SQL)
                    MsgBox("SAVE SUCCEEDED", vbInformation, "SALESORDER")
                    ClearInputSO()

                End If
            End If
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'add
        UnlockAll()
        TextEdit59.Text = Val(Strings.Right(GetCode("SO"), 2))
        TextEdit59.Enabled = False
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'DELETE
        SQL = "UPDATE T_SALESORDER SET AKTIF= 'N' WHERE SO_NUMBER'" & TextEdit59.Text & "'"
        ExecuteNonQuery(SQL)
        LoadUser()
        MsgBox("SAVE SUCCEEDED", vbInformation, "SALESORDER")
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'CANCEL
        TextEdit59.Text = ""
        TextEdit87.Text = ""
        TextEdit85.Text = ""
        TextEdit82.Text = ""
        TextEdit86.Text = ""
        TextEdit84.Text = ""
        TextEdit83.Text = ""
        TextEdit80.Text = ""
        TextEdit78.Text = ""
        TextEdit75.Text = ""
        TextEdit79.Text = ""
        TextEdit77.Text = ""
        TextEdit76.Text = ""
        TextEdit73.Text = ""
        ComboBoxEdit10.Text = ""
        TextEdit99.Text = ""
        ComboBoxEdit11.Text = ""
        LockAll()
        SimpleButton2.Text = "SAVE" 'SAVE
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()
    End Sub
    Private Sub GridView6_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GridView6.RowCellClick
        If e.RowHandle < 0 Then
            SimpleButton1.Enabled = True 'add
            SimpleButton2.Enabled = False 'save
            SimpleButton3.Enabled = False 'delete
            SimpleButton4.Enabled = True 'cancel
            SimpleButton5.Enabled = True 'close
        Else
            SimpleButton1.Enabled = False 'add
            SimpleButton2.Enabled = True 'save
            SimpleButton3.Enabled = True 'delete
            SimpleButton4.Enabled = True 'cancel
            SimpleButton5.Enabled = True 'close

            SimpleButton1.Text = "UPDATE" 'SAVE

            TextEdit59.Text = GridView6.GetRowCellValue(e.RowHandle, "SO_NUMBER").ToString() 'SONUMBER
            TextEdit87.Text = GridView6.GetRowCellValue(e.RowHandle, "SO_QUANTITY").ToString() 'SOQUANTITY
            TextEdit85.Text = GridView6.GetRowCellValue(e.RowHandle, "SO_STARTDATE").ToString() 'SOSTARTDATE
            TextEdit82.Text = GridView6.GetRowCellValue(e.RowHandle, "SO_ENDDATE").ToString() 'SOENDDATE
            TextEdit86.Text = GridView6.GetRowCellValue(e.RowHandle, "CUSTOMER_CODE").ToString() 'CUSTOMERCODE
            TextEdit84.Text = GridView6.GetRowCellValue(e.RowHandle, "CUSTOMER_NAME").ToString() 'CUSTOMERNAME
            TextEdit83.Text = GridView6.GetRowCellValue(e.RowHandle, "SALDO").ToString() 'SALDO
            TextEdit80.Text = GridView6.GetRowCellValue(e.RowHandle, "STATUS ").ToString() 'STATUS
            TextEdit78.Text = GridView6.GetRowCellValue(e.RowHandle, "INCOTERMS1").ToString() 'INCOTERMS1
            TextEdit75.Text = GridView6.GetRowCellValue(e.RowHandle, "INCOTERMS2").ToString() 'INNCOTERMMS2
            TextEdit79.Text = GridView6.GetRowCellValue(e.RowHandle, "ITEMNO").ToString()  'ITEMNO
            TextEdit77.Text = GridView6.GetRowCellValue(e.RowHandle, "SALES_CONTRACT_NUMBER").ToString()  'SCNUMBER
            TextEdit76.Text = GridView6.GetRowCellValue(e.RowHandle, "MATERIAL_CODE").ToString() 'MATERIALCODE
            TextEdit73.Text = GridView6.GetRowCellValue(e.RowHandle, "TOLERANCE ").ToString()  'TOLERANCE
            ComboBoxEdit10.SelectedItem = GridView6.GetRowCellValue(e.RowHandle, "INACTIVE").ToString() 'INACTIVE
            TextEdit99.Text = GridView6.GetRowCellValue(e.RowHandle, "INACTIVE_DATE").ToString()  'INACTIVEDATE
            ComboBoxEdit11.SelectedItem = GridView6.GetRowCellValue(e.RowHandle, "COMPLETE").ToString()  'COMPLETE

            TextEdit59.Enabled = False
            UnlockAll()
        End If
    End Sub
End Class