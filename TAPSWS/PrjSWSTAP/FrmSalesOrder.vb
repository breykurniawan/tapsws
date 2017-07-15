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
        LockAll()

    End Sub
    Private Sub UnlockAll()
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

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close

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

        SimpleButton1.Enabled = False 'add
        SimpleButton2.Enabled = False 'save
        SimpleButton3.Enabled = False 'delete
        SimpleButton4.Enabled = False 'cancel
        SimpleButton5.Enabled = False 'close
    End Sub
    Private Sub GridHeader()
        Dim view As ColumnView = CType(GridControl6.MainView, ColumnView)
        Dim fieldNames() As String = New String() {"SONUMBER,SOQUANTITY,SOSTARTDATE,SOENDDATE,CUST_CODE,CUST_NAME,SALDO,STATUS,INCOTERMS1,INCOTERMS2,ITEMNO,SELES_CONTRACTN,MATERIAL_CODE,TOLERANCE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,COMPLETE"}
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
        SQL = "SELECT SONUMBER,SOQUANTITY,SOSTARTDATE,SOENDDATE,CUST_CODE,CUST_NAME,SALDO,STATUS,INCOTERMS1,INCOTERMS2,ITEMNO,SELES_CONTRACTN,MATERIAL_CODE,TOLERANCE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,COMPLETE" +
            "FROM T_SALESORDER A" +
            "LEFT JOIN SONUMBER B On A.SOQUANTITY And B.aktif= 'Y' " +
            "WHERE A.aktif= 'Y' " +
            "ORDER BY SONUMBER"
        FILLGridView(SQL, GridControl6)

        GridControl6.DataSource = ExecuteQuery(SQL)
        Dim GridView As GridView = CType(GridControl6.FocusedView, GridView)
        GridView.ExpandAllGroups()

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If Not IsEmptyText({TextEdit59, TextEdit87, TextEdit85, TextEdit82, TextEdit86, TextEdit84, TextEdit83, TextEdit80, TextEdit78, TextEdit75, TextEdit79, TextEdit77, TextEdit76, TextEdit73, ComboBoxEdit10, TextEdit99, ComboBoxEdit11}) Then
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
                SQL = "INSERT INTO T_SALESORDER (SONUMBER,SOQUANTITY,SOSTARTDATE,SOENDDATE,CUST_CODE,CUST_NAME,SALDO,STATUS,INCOTERMS1,INCOTERMS2,ITEMNO,SELES_CONTRACTN,MATERIAL_CODE,TOLERANCE,INPUT_BY,INPUT_DATE,UPDATE_BY,UPDATE_DATE,INACTIVE,INACTIVE_DATE,COMPLETE)" +
                    " VALUES('" & SONUMBER & "','" & SOQUANTITY & "','" & SOSTARTDATE & "',,'" & SOENDDATE & "','" & CUSTCODE & "','" & CUSTNAME & "','" & SALDO & "','" & STATUS & "','" & INCOTERMS1 & "','" & INCOTERMS2 & "','" & ITEMNO & "','" & SCNUMBER & "','" & MATERIALCODE & "','" & TOLERANCE & "','" & INACTIVEDATE & "','" & COMPLETE & "')"
                ExecuteNonQuery(SQL)
                SQL = "SELECT FROM T_SALESORDER WHERE SONUMBER='" & TextEdit59.Text & "'"
                If CheckRecord(SQL) > 0 Then UpdateCode("SO")
                LoadView()
                MsgBox("SAVE SUCCEEDED", vbInformation, "SALESORDER")
                UnlockAll()
                ClearInputSO()
            Else
                SQL = "UPDATE T_SALESORDER SET SONUMBER='" & SONUMBER & "',SOQUANTITY='" & SOQUANTITY & "',SOSATARTDATE='" & SOSTARTDATE & "',SOENDDATE='" & SOENDDATE & "',CUST_CODE='" & CUSTCODE & "',CUST_NAME='" & CUSTNAME & "',SALDO ='" & SALDO & "',STATUS='" & STATUS & "',INCOTERMS1='" & INCOTERMS1 & "',INCOTERMS2='" & INCOTERMS2 & "',ITEMNO='" & ITEMNO & "',SALES_CONTRARTN='" & SCNUMBER & "',MATERIAL_CODE='" & MATERIALCODE & "',TOLERANCE='" & TOLERANCE & "',INACTIVE='" & INACTIVE & "',INACTIVE_DATEE='" & INACTIVEDATE & "',COMPLETE='" & COMPLETE & "'" +
                 " WHERE T_SALESORDER= '" & TextEdit59.Text & "'"
                ExecuteNonQuery(SQL)

                MsgBox("SAVE SUCCEEDED", vbInformation, "SALESORDER")
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
        SQL = "UPDATE T_SALESORDER SET AKTIF= 'N' WHERE SONUMBER'" & TextEdit59.Text & "'"
        ExecuteNonQuery(SQL)
        LoadUser()
        MsgBox("SAVE SUCCEEDED", vbInformation, "SALESORDER")
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
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

    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Me.Close()

    End Sub
End Class