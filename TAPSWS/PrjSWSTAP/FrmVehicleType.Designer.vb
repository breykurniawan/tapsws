<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVehicleType
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmVehicleType))
        Me.BunifuGradientPanel2 = New ns1.BunifuGradientPanel()
        Me.LabelControl89 = New DevExpress.XtraEditors.LabelControl()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.LabelControl92 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl93 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl6 = New DevExpress.XtraEditors.PanelControl()
        Me.SimpleButton4 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton5 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton3 = New DevExpress.XtraEditors.SimpleButton()
        Me.TextEdit74 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit81 = New DevExpress.XtraEditors.TextEdit()
        Me.GridControl5 = New DevExpress.XtraGrid.GridControl()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BunifuGradientPanel2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl6.SuspendLayout()
        CType(Me.TextEdit74.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit81.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BunifuGradientPanel2
        '
        Me.BunifuGradientPanel2.BackgroundImage = CType(resources.GetObject("BunifuGradientPanel2.BackgroundImage"), System.Drawing.Image)
        Me.BunifuGradientPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BunifuGradientPanel2.Controls.Add(Me.LabelControl89)
        Me.BunifuGradientPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.BunifuGradientPanel2.GradientBottomLeft = System.Drawing.Color.DarkGreen
        Me.BunifuGradientPanel2.GradientBottomRight = System.Drawing.Color.White
        Me.BunifuGradientPanel2.GradientTopLeft = System.Drawing.Color.Green
        Me.BunifuGradientPanel2.GradientTopRight = System.Drawing.Color.White
        Me.BunifuGradientPanel2.Location = New System.Drawing.Point(0, 0)
        Me.BunifuGradientPanel2.Name = "BunifuGradientPanel2"
        Me.BunifuGradientPanel2.Quality = 10
        Me.BunifuGradientPanel2.Size = New System.Drawing.Size(1019, 43)
        Me.BunifuGradientPanel2.TabIndex = 72
        '
        'LabelControl89
        '
        Me.LabelControl89.Appearance.Font = New System.Drawing.Font("Tahoma", 9.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl89.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl89.Appearance.Options.UseFont = True
        Me.LabelControl89.Appearance.Options.UseForeColor = True
        Me.LabelControl89.Location = New System.Drawing.Point(12, 15)
        Me.LabelControl89.Name = "LabelControl89"
        Me.LabelControl89.Size = New System.Drawing.Size(59, 14)
        Me.LabelControl89.TabIndex = 0
        Me.LabelControl89.Text = "Customer"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel7.Controls.Add(Me.LabelControl92)
        Me.Panel7.Controls.Add(Me.LabelControl93)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel7.ForeColor = System.Drawing.Color.Black
        Me.Panel7.Location = New System.Drawing.Point(0, 43)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(153, 576)
        Me.Panel7.TabIndex = 73
        '
        'LabelControl92
        '
        Me.LabelControl92.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl92.Appearance.Options.UseFont = True
        Me.LabelControl92.Location = New System.Drawing.Point(12, 70)
        Me.LabelControl92.Name = "LabelControl92"
        Me.LabelControl92.Size = New System.Drawing.Size(56, 13)
        Me.LabelControl92.TabIndex = 4
        Me.LabelControl92.Text = "Tolerance"
        '
        'LabelControl93
        '
        Me.LabelControl93.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl93.Appearance.Options.UseFont = True
        Me.LabelControl93.Location = New System.Drawing.Point(12, 48)
        Me.LabelControl93.Name = "LabelControl93"
        Me.LabelControl93.Size = New System.Drawing.Size(71, 13)
        Me.LabelControl93.TabIndex = 3
        Me.LabelControl93.Text = "Vehicle Type"
        '
        'PanelControl6
        '
        Me.PanelControl6.Controls.Add(Me.SimpleButton4)
        Me.PanelControl6.Controls.Add(Me.SimpleButton2)
        Me.PanelControl6.Controls.Add(Me.SimpleButton1)
        Me.PanelControl6.Controls.Add(Me.SimpleButton5)
        Me.PanelControl6.Controls.Add(Me.SimpleButton3)
        Me.PanelControl6.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl6.Location = New System.Drawing.Point(153, 43)
        Me.PanelControl6.Name = "PanelControl6"
        Me.PanelControl6.Size = New System.Drawing.Size(866, 39)
        Me.PanelControl6.TabIndex = 75
        '
        'SimpleButton4
        '
        Me.SimpleButton4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton4.Appearance.ForeColor = System.Drawing.Color.Green
        Me.SimpleButton4.Appearance.Options.UseFont = True
        Me.SimpleButton4.Appearance.Options.UseForeColor = True
        Me.SimpleButton4.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.SimpleButton4.Location = New System.Drawing.Point(236, 5)
        Me.SimpleButton4.Name = "SimpleButton4"
        Me.SimpleButton4.Size = New System.Drawing.Size(75, 27)
        Me.SimpleButton4.TabIndex = 53
        Me.SimpleButton4.Text = "Cancel"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton2.Appearance.ForeColor = System.Drawing.Color.Green
        Me.SimpleButton2.Appearance.Options.UseFont = True
        Me.SimpleButton2.Appearance.Options.UseForeColor = True
        Me.SimpleButton2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.SimpleButton2.Location = New System.Drawing.Point(82, 5)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(75, 27)
        Me.SimpleButton2.TabIndex = 51
        Me.SimpleButton2.Text = "Save"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton1.Appearance.ForeColor = System.Drawing.Color.Green
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Appearance.Options.UseForeColor = True
        Me.SimpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.SimpleButton1.Location = New System.Drawing.Point(5, 5)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(75, 27)
        Me.SimpleButton1.TabIndex = 50
        Me.SimpleButton1.Text = "Add"
        '
        'SimpleButton5
        '
        Me.SimpleButton5.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton5.Appearance.ForeColor = System.Drawing.Color.Green
        Me.SimpleButton5.Appearance.Options.UseFont = True
        Me.SimpleButton5.Appearance.Options.UseForeColor = True
        Me.SimpleButton5.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.SimpleButton5.Location = New System.Drawing.Point(313, 5)
        Me.SimpleButton5.Name = "SimpleButton5"
        Me.SimpleButton5.Size = New System.Drawing.Size(75, 27)
        Me.SimpleButton5.TabIndex = 49
        Me.SimpleButton5.Text = "Close"
        '
        'SimpleButton3
        '
        Me.SimpleButton3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton3.Appearance.ForeColor = System.Drawing.Color.Green
        Me.SimpleButton3.Appearance.Options.UseFont = True
        Me.SimpleButton3.Appearance.Options.UseForeColor = True
        Me.SimpleButton3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.SimpleButton3.Location = New System.Drawing.Point(159, 5)
        Me.SimpleButton3.Name = "SimpleButton3"
        Me.SimpleButton3.Size = New System.Drawing.Size(75, 27)
        Me.SimpleButton3.TabIndex = 48
        Me.SimpleButton3.Text = "Delete"
        '
        'TextEdit74
        '
        Me.TextEdit74.Location = New System.Drawing.Point(158, 88)
        Me.TextEdit74.Name = "TextEdit74"
        Me.TextEdit74.Size = New System.Drawing.Size(236, 20)
        Me.TextEdit74.TabIndex = 54
        '
        'TextEdit81
        '
        Me.TextEdit81.Location = New System.Drawing.Point(157, 110)
        Me.TextEdit81.Name = "TextEdit81"
        Me.TextEdit81.Size = New System.Drawing.Size(236, 20)
        Me.TextEdit81.TabIndex = 55
        '
        'GridControl5
        '
        Me.GridControl5.Location = New System.Drawing.Point(153, 165)
        Me.GridControl5.MainView = Me.GridView5
        Me.GridControl5.Name = "GridControl5"
        Me.GridControl5.Size = New System.Drawing.Size(866, 297)
        Me.GridControl5.TabIndex = 76
        Me.GridControl5.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView5})
        '
        'GridView5
        '
        Me.GridView5.GridControl = Me.GridControl5
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsView.ShowGroupPanel = False
        '
        'FrmVehicleType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 619)
        Me.Controls.Add(Me.GridControl5)
        Me.Controls.Add(Me.TextEdit74)
        Me.Controls.Add(Me.TextEdit81)
        Me.Controls.Add(Me.PanelControl6)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.BunifuGradientPanel2)
        Me.Name = "FrmVehicleType"
        Me.Text = "FrmVehicleType"
        Me.BunifuGradientPanel2.ResumeLayout(False)
        Me.BunifuGradientPanel2.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl6.ResumeLayout(False)
        CType(Me.TextEdit74.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit81.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BunifuGradientPanel2 As ns1.BunifuGradientPanel
    Friend WithEvents LabelControl89 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Panel7 As Panel
    Friend WithEvents LabelControl92 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl93 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl6 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents SimpleButton4 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton5 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton3 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TextEdit74 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit81 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridControl5 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
