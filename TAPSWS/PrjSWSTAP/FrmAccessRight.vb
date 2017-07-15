Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors.Repository

Public Class FrmAccessRight
    Private Sub FrmAccessRight_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "ACCESS RIGHT MENU"
        GridHeader()
        LoadAccesmenu()
        FillGroup()
        FilltFormName()
        ComboBoxEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    End Sub

    Private Sub GridHeader()
        Dim View As ColumnView = CType(GridControl1.MainView, ColumnView)
        Dim FieldNames() As String = New String() {"GROUP_ID", "GROUP_NAME", "MENUID", "MENU", "TYPEMENU", "IMGID", "FRMNAME"}
        Dim I As Integer
        Dim Column As DevExpress.XtraGrid.Columns.GridColumn

        View.Columns.Clear()
        For I = 0 To FieldNames.Length - 1
            Column = View.Columns.AddField(FieldNames(I))
            Column.VisibleIndex = I
        Next
        ''TEST + CHECK BOX
        'Dim edit As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
        'Dim trueValue As Int64 = 1
        'Dim falseValue As Int64 = 0
        'edit.ValueChecked = trueValue
        'edit.ValueUnchecked = falseValue
        'View.Columns("CHK").ColumnEdit = edit
        'GridControl1.RepositoryItems.Add(edit)
        'View.Columns("CHK").OptionsColumn.AllowEdit = True
        'GridView1.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True
        'END TEST

        'GROUPING
        Dim GridView As GridView = CType(GridControl1.FocusedView, GridView)
        GridView.SortInfo.ClearAndAddRange(New GridColumnSortInfo() {
        New GridColumnSortInfo(GridView.Columns("GROUP_ID"), DevExpress.Data.ColumnSortOrder.Ascending),
        New GridColumnSortInfo(GridView.Columns("GROUP_NAME"), DevExpress.Data.ColumnSortOrder.Ascending),
        New GridColumnSortInfo(GridView.Columns("MENUID"), DevExpress.Data.ColumnSortOrder.Ascending)
        }, 1)
        GridView1.BestFitColumns()
        GridView1.ExpandAllGroups()

    End Sub
    Private Sub FilltFormName()
        Dim myAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim types As Type() = myAssembly.GetTypes()
        ComboBoxEdit3.Properties.Items.Clear()
        For Each myType As Object In types
            If myType.name.ToString <> "" Then
                Dim frmname As String = myType.name
                If (frmname.Substring(0, 3)) = "Frm" Then
                    ComboBoxEdit3.Properties.Items.Add(myType.name.ToString)
                End If
            End If
        Next
        ComboBoxEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    End Sub
    Private Sub FillGroup()
        SQL = "Select ACCESSNAME,ACCESSID FROM  T_ACCESSRIGHTS  WHERE  TYPE=0 ORDER BY ACCESSID"
        FILLComboBoxEdit(SQL, 0, ComboBoxEdit2, False)
    End Sub

    Private Sub FrmAccessRight_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        PanelControl1.Height = Me.Height - 230
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        'CLOSE
        Close()
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        'GROUP
        Dim Parent As String = GetParentId(ComboBoxEdit2.Text)
        If ComboBoxEdit1.SelectedIndex = 0 Then  'GROUP
            TextEdit1.Text = GetCodeGrp()
            ComboBoxEdit2.Enabled = False
            ComboBoxEdit3.Enabled = False
        Else
            TextEdit1.Text = Parent
            ComboBoxEdit2.Enabled = True
            ComboBoxEdit3.Enabled = True
        End If
        TextEdit1.Enabled = False
    End Sub

    Private Sub LoadAccesmenu()
        SQL = " SELECT NVL(B.ACCESSID,A.ACCESSID) AS GROUP_ID,NVL(B.ACCESSNAME,A.ACCESSNAME) AS GROUP_NAME,A.ACCESSID AS MENUID,A.ACCESSNAME AS MENU, " +
        " CASE A.TYPE " +
        " WHEN '0' THEN 'GROUP' " +
        " WHEN '1' THEN 'MENU' " +
        " End As TYPEMENU  ,A.IMGID,A.FRMNAME " +
        " FROM  T_ACCESSRIGHTS A " +
        " LEFT JOIN T_ACCESSRIGHTS B On A.PARENTID=B.ACCESSID " +
        " ORDER BY A.ACCESSID,B.ACCESSNAME"
        FILLGridView(SQL, GridControl1)
    End Sub

    Private Sub GridView1_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GridView1.RowCellClick
        'GRID ACCESS RIGHT
        '"MENUID", "MENU", "TYPEMENU", "IMGID", "FRMNAME"
        On Error Resume Next
        If e.RowHandle < 0 Then
            SimpleButton1.Enabled = True 'ADD
            SimpleButton3.Enabled = False 'SAVE
            SimpleButton2.Enabled = False 'EDIT
        Else
            SimpleButton1.Enabled = False 'ADD
            SimpleButton2.Enabled = True 'EDIT
            SimpleButton3.Enabled = False 'SAVE

            'TextEdit1.Text = GridView1.GetRowCellValue(e.RowHandle, "MENUID").ToString()  'ID
            TextEdit2.Text = GridView1.GetRowCellValue(e.RowHandle, "MENU").ToString() 'NAME
            ComboBoxEdit1.Text = GridView1.GetRowCellValue(e.RowHandle, "TYPEMENU").ToString() 'NAME
            ComboBoxEdit2.Text = GridView1.GetRowCellValue(e.RowHandle, "GROUP_NAME").ToString() 'NAME
            TextEdit3.Text = GridView1.GetRowCellValue(e.RowHandle, "IMGID").ToString() 'NAME
            ComboBoxEdit3.Text = GridView1.GetRowCellValue(e.RowHandle, "FRMNAME").ToString() 'NAME
            TextEdit1.Text = GridView1.GetRowCellValue(e.RowHandle, "MENUID").ToString()  'ID
            TextEdit1.Enabled = False

            GridView1.Columns("CHK").OptionsColumn.AllowEdit = True
        End If
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        'CANCEL

        TextEdit2.Text = ""
        ComboBoxEdit1.Text = ""
        TextEdit3.Text = ""
        ComboBoxEdit2.Text = ""
        ComboBoxEdit3.Text = ""
        TextEdit1.Select()
        TextEdit1.Text = ""

        SimpleButton1.Enabled = True 'ADD
        SimpleButton2.Enabled = False 'EDIT
        SimpleButton3.Enabled = False 'SAVE

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        ''EDIT
        'TextEdit1.Enabled = False
        'TextEdit2.Select()
        'SimpleButton1.Enabled = False 'ADD
        'SimpleButton2.Enabled = False 'EDIT
        'SimpleButton3.Enabled = True 'SAVE
        SQL = ""
        ExecuteNonQuery(SQL)
        LoadAccesmenu()
        MsgBox("Delete Successful", vbInformation, "Acces Right Menu")
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'ADD
        TextEdit1.Text = ""
        TextEdit1.Enabled = False


        TextEdit2.Text = ""
        ComboBoxEdit1.Text = ""
        ComboBoxEdit2.Text = ""
        TextEdit3.Text = ""
        ComboBoxEdit3.Text = ""

        TextEdit2.Select()

        SimpleButton1.Enabled = False 'ADD
        SimpleButton2.Enabled = False 'EDIT
        SimpleButton3.Enabled = True 'SAVE

        FillGroup()
        FilltFormName()
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        'SAVE
        If Not IsEmptyText({TextEdit1, TextEdit2}) = True Then
            If Not IsEmptyCombo({ComboBoxEdit1}) = True Then
                Dim MENUID As String = TextEdit1.Text
                Dim MENU As String = TextEdit2.Text
                Dim TYPE As String = ComboBoxEdit1.Text
                Dim PARENTID As String = GetParentId(ComboBoxEdit2.Text)
                Dim IMGID As String = TextEdit3.Text
                Dim FRMNAME As String = ComboBoxEdit3.Text

                If TYPE = "MENU" Then
                    TYPE = "1"
                Else
                    TYPE = "0"
                End If

                SQL = "Select * FROM T_ACCESSRIGHTS WHERE ACCESSID ='" & TextEdit1.Text & "' "
                If CheckRecord(SQL) = 0 Then
                    SQL = "INSERT INTO T_ACCESSRIGHTS (ACCESSID,ACCESSNAME,TYPE,PARENTID,IMGID,FRMNAME)" +
                    " VALUES ('" & MENUID & "','" & MENU & "','" & TYPE & "','" & PARENTID & "','" & IMGID & "','" & FRMNAME & "')	"
                    ExecuteNonQuery(SQL)
                    LoadAccesmenu()
                    MsgBox("Insert  Successful", vbInformation, "Access Right Menu")
                Else
                    SQL = "UPDATE T_ACCESSRIGHTS SET ACCESSNAME='" & MENU & "',TYPE='" & TYPE & "',PARENTID='" & PARENTID & "',IMGID='" & IMGID & "',FRMNAME='" & FRMNAME & "' " +
                      "WHERE ACCESSID='" & MENUID & "'"
                    ExecuteNonQuery(SQL)
                    LoadAccesmenu()
                    MsgBox("Update Successful", vbInformation, "Access Right Menu")
                End If
                TextEdit1.Text = ""
                TextEdit2.Text = ""
                TextEdit3.Text = ""

                ComboBoxEdit1.Text = ""
                ComboBoxEdit2.Text = ""
                ComboBoxEdit3.Text = ""

                TextEdit1.Select()

                SimpleButton1.Enabled = True 'ADD
                SimpleButton2.Enabled = False 'EDIT
                SimpleButton3.Enabled = False 'SAVE
            End If
        End If
    End Sub

    Private Sub ComboBoxEdit2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit2.SelectedIndexChanged
        'PARENT ID
        Dim parent As String = GetParentId(ComboBoxEdit2.Text)
        If ComboBoxEdit1.SelectedIndex = 1 Then TextEdit1.Text = GetCodeSub(parent)
    End Sub

    Private Sub gridViewT_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        If e.Column.Name = "CheckColumn" AndAlso e.RowHandle > -1 Then
            If Not (e.Value Is Nothing) AndAlso DirectCast(e.Value, Boolean) Then
                ' Dim dr As DataRow = DirectCast(GridControl1.DataSource, DataTable).Rows(e.RowHandle)
                Dim dr As DataRow = DirectCast(sender, GridView).GetDataRow(e.RowHandle)

            End If
        End If
    End Sub
End Class
