<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.LblReportPatient = New Guna.UI2.WinForms.Guna2Button()
        Me.LblReportMedicalHistory = New Guna.UI2.WinForms.Guna2Button()
        Me.LblReportEmployee = New Guna.UI2.WinForms.Guna2Button()
        Me.LblReportDonations = New Guna.UI2.WinForms.Guna2Button()
        Me.LblReportPayroll = New Guna.UI2.WinForms.Guna2Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.DataGridView4 = New System.Windows.Forms.DataGridView()
        Me.DataGridView5 = New System.Windows.Forms.DataGridView()
        Me.btnReportAll = New Guna.UI2.WinForms.Guna2Button()
        Me.cmbYear = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.cmbMonth = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.btnFilter = New Guna.UI2.WinForms.Guna2Button()
        Me.txtSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btnSearch = New Guna.UI2.WinForms.Guna2Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1370, 749)
        Me.CrystalReportViewer1.TabIndex = 0
        '
        'LblReportPatient
        '
        Me.LblReportPatient.AutoRoundedCorners = True
        Me.LblReportPatient.BackColor = System.Drawing.Color.Transparent
        Me.LblReportPatient.BorderRadius = 26
        Me.LblReportPatient.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.LblReportPatient.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.LblReportPatient.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.LblReportPatient.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.LblReportPatient.FillColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblReportPatient.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LblReportPatient.ForeColor = System.Drawing.Color.White
        Me.LblReportPatient.Location = New System.Drawing.Point(12, 54)
        Me.LblReportPatient.Name = "LblReportPatient"
        Me.LblReportPatient.Size = New System.Drawing.Size(180, 54)
        Me.LblReportPatient.TabIndex = 2
        Me.LblReportPatient.Text = "Elderly"
        Me.LblReportPatient.UseTransparentBackground = True
        '
        'LblReportMedicalHistory
        '
        Me.LblReportMedicalHistory.AutoRoundedCorners = True
        Me.LblReportMedicalHistory.BackColor = System.Drawing.Color.Transparent
        Me.LblReportMedicalHistory.BorderRadius = 26
        Me.LblReportMedicalHistory.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.LblReportMedicalHistory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.LblReportMedicalHistory.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.LblReportMedicalHistory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.LblReportMedicalHistory.FillColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblReportMedicalHistory.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LblReportMedicalHistory.ForeColor = System.Drawing.Color.White
        Me.LblReportMedicalHistory.Location = New System.Drawing.Point(12, 121)
        Me.LblReportMedicalHistory.Name = "LblReportMedicalHistory"
        Me.LblReportMedicalHistory.Size = New System.Drawing.Size(180, 54)
        Me.LblReportMedicalHistory.TabIndex = 3
        Me.LblReportMedicalHistory.Text = "Medical History"
        Me.LblReportMedicalHistory.UseTransparentBackground = True
        '
        'LblReportEmployee
        '
        Me.LblReportEmployee.AutoRoundedCorners = True
        Me.LblReportEmployee.BackColor = System.Drawing.Color.Transparent
        Me.LblReportEmployee.BorderRadius = 26
        Me.LblReportEmployee.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.LblReportEmployee.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.LblReportEmployee.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.LblReportEmployee.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.LblReportEmployee.FillColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblReportEmployee.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LblReportEmployee.ForeColor = System.Drawing.Color.White
        Me.LblReportEmployee.Location = New System.Drawing.Point(12, 326)
        Me.LblReportEmployee.Name = "LblReportEmployee"
        Me.LblReportEmployee.Size = New System.Drawing.Size(180, 54)
        Me.LblReportEmployee.TabIndex = 4
        Me.LblReportEmployee.Text = "Employee"
        Me.LblReportEmployee.UseTransparentBackground = True
        '
        'LblReportDonations
        '
        Me.LblReportDonations.AutoRoundedCorners = True
        Me.LblReportDonations.BackColor = System.Drawing.Color.Transparent
        Me.LblReportDonations.BorderRadius = 26
        Me.LblReportDonations.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.LblReportDonations.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.LblReportDonations.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.LblReportDonations.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.LblReportDonations.FillColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblReportDonations.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LblReportDonations.ForeColor = System.Drawing.Color.White
        Me.LblReportDonations.Location = New System.Drawing.Point(12, 189)
        Me.LblReportDonations.Name = "LblReportDonations"
        Me.LblReportDonations.Size = New System.Drawing.Size(180, 54)
        Me.LblReportDonations.TabIndex = 6
        Me.LblReportDonations.Text = "Donations"
        Me.LblReportDonations.UseTransparentBackground = True
        '
        'LblReportPayroll
        '
        Me.LblReportPayroll.AutoRoundedCorners = True
        Me.LblReportPayroll.BackColor = System.Drawing.Color.Transparent
        Me.LblReportPayroll.BorderRadius = 26
        Me.LblReportPayroll.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.LblReportPayroll.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.LblReportPayroll.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.LblReportPayroll.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.LblReportPayroll.FillColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblReportPayroll.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LblReportPayroll.ForeColor = System.Drawing.Color.White
        Me.LblReportPayroll.Location = New System.Drawing.Point(12, 258)
        Me.LblReportPayroll.Name = "LblReportPayroll"
        Me.LblReportPayroll.Size = New System.Drawing.Size(180, 54)
        Me.LblReportPayroll.TabIndex = 7
        Me.LblReportPayroll.Text = "Payroll"
        Me.LblReportPayroll.UseTransparentBackground = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(637, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 38)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Reports"
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(222, 112)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1130, 615)
        Me.DataGridView1.TabIndex = 10
        '
        'DataGridView2
        '
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(222, 112)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(1130, 615)
        Me.DataGridView2.TabIndex = 11
        '
        'DataGridView3
        '
        Me.DataGridView3.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Location = New System.Drawing.Point(222, 112)
        Me.DataGridView3.MultiSelect = False
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.ReadOnly = True
        Me.DataGridView3.Size = New System.Drawing.Size(1130, 615)
        Me.DataGridView3.TabIndex = 12
        '
        'DataGridView4
        '
        Me.DataGridView4.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView4.Location = New System.Drawing.Point(222, 112)
        Me.DataGridView4.MultiSelect = False
        Me.DataGridView4.Name = "DataGridView4"
        Me.DataGridView4.ReadOnly = True
        Me.DataGridView4.Size = New System.Drawing.Size(1130, 615)
        Me.DataGridView4.TabIndex = 13
        '
        'DataGridView5
        '
        Me.DataGridView5.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView5.Location = New System.Drawing.Point(222, 112)
        Me.DataGridView5.MultiSelect = False
        Me.DataGridView5.Name = "DataGridView5"
        Me.DataGridView5.ReadOnly = True
        Me.DataGridView5.Size = New System.Drawing.Size(1130, 615)
        Me.DataGridView5.TabIndex = 14
        '
        'btnReportAll
        '
        Me.btnReportAll.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnReportAll.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnReportAll.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnReportAll.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnReportAll.FillColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnReportAll.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnReportAll.ForeColor = System.Drawing.Color.White
        Me.btnReportAll.Location = New System.Drawing.Point(1233, 67)
        Me.btnReportAll.Name = "btnReportAll"
        Me.btnReportAll.Size = New System.Drawing.Size(119, 36)
        Me.btnReportAll.TabIndex = 15
        Me.btnReportAll.Text = "Print"
        Me.btnReportAll.Visible = False
        '
        'cmbYear
        '
        Me.cmbYear.BackColor = System.Drawing.Color.Transparent
        Me.cmbYear.BorderColor = System.Drawing.Color.Black
        Me.cmbYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbYear.DropDownHeight = 130
        Me.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYear.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbYear.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbYear.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cmbYear.ForeColor = System.Drawing.Color.Black
        Me.cmbYear.IntegralHeight = False
        Me.cmbYear.ItemHeight = 30
        Me.cmbYear.Location = New System.Drawing.Point(222, 67)
        Me.cmbYear.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbYear.Name = "cmbYear"
        Me.cmbYear.Size = New System.Drawing.Size(135, 36)
        Me.cmbYear.TabIndex = 264
        Me.cmbYear.UseWaitCursor = True
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.BackColor = System.Drawing.SystemColors.MenuBar
        Me.lblYear.Font = New System.Drawing.Font("Century Gothic", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.Location = New System.Drawing.Point(224, 42)
        Me.lblYear.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(53, 23)
        Me.lblYear.TabIndex = 265
        Me.lblYear.Text = "Year"
        Me.lblYear.UseWaitCursor = True
        '
        'lblMonth
        '
        Me.lblMonth.AutoSize = True
        Me.lblMonth.BackColor = System.Drawing.SystemColors.MenuBar
        Me.lblMonth.Font = New System.Drawing.Font("Century Gothic", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonth.Location = New System.Drawing.Point(392, 42)
        Me.lblMonth.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(67, 23)
        Me.lblMonth.TabIndex = 266
        Me.lblMonth.Text = "Month"
        Me.lblMonth.UseWaitCursor = True
        '
        'cmbMonth
        '
        Me.cmbMonth.BackColor = System.Drawing.Color.Transparent
        Me.cmbMonth.BorderColor = System.Drawing.Color.Black
        Me.cmbMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMonth.DropDownHeight = 130
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMonth.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMonth.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cmbMonth.ForeColor = System.Drawing.Color.Black
        Me.cmbMonth.IntegralHeight = False
        Me.cmbMonth.ItemHeight = 30
        Me.cmbMonth.Location = New System.Drawing.Point(389, 67)
        Me.cmbMonth.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(232, 36)
        Me.cmbMonth.TabIndex = 267
        Me.cmbMonth.UseWaitCursor = True
        '
        'btnFilter
        '
        Me.btnFilter.BorderColor = System.Drawing.Color.Red
        Me.btnFilter.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnFilter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnFilter.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnFilter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnFilter.FillColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnFilter.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnFilter.ForeColor = System.Drawing.Color.White
        Me.btnFilter.Location = New System.Drawing.Point(640, 67)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(128, 36)
        Me.btnFilter.TabIndex = 268
        Me.btnFilter.Text = "Filter"
        '
        'txtSearch
        '
        Me.txtSearch.BorderColor = System.Drawing.Color.Black
        Me.txtSearch.BorderRadius = 10
        Me.txtSearch.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.txtSearch.DefaultText = ""
        Me.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.Location = New System.Drawing.Point(840, 66)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtSearch.PlaceholderText = ""
        Me.txtSearch.SelectedText = ""
        Me.txtSearch.Size = New System.Drawing.Size(356, 38)
        Me.txtSearch.TabIndex = 270
        Me.txtSearch.UseWaitCursor = True
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.Transparent
        Me.btnSearch.BorderRadius = 10
        Me.btnSearch.BorderThickness = 2
        Me.btnSearch.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnSearch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnSearch.FillColor = System.Drawing.Color.Transparent
        Me.btnSearch.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageSize = New System.Drawing.Size(30, 30)
        Me.btnSearch.Location = New System.Drawing.Point(791, 67)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(37, 36)
        Me.btnSearch.TabIndex = 269
        Me.btnSearch.UseWaitCursor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 749)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnFilter)
        Me.Controls.Add(Me.cmbMonth)
        Me.Controls.Add(Me.lblMonth)
        Me.Controls.Add(Me.lblYear)
        Me.Controls.Add(Me.cmbYear)
        Me.Controls.Add(Me.btnReportAll)
        Me.Controls.Add(Me.DataGridView5)
        Me.Controls.Add(Me.DataGridView4)
        Me.Controls.Add(Me.DataGridView3)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblReportPayroll)
        Me.Controls.Add(Me.LblReportDonations)
        Me.Controls.Add(Me.LblReportEmployee)
        Me.Controls.Add(Me.LblReportMedicalHistory)
        Me.Controls.Add(Me.LblReportPatient)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Name = "Form2"
        Me.Text = "Form2"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents LblReportPatient As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents LblReportMedicalHistory As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents LblReportEmployee As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents LblReportDonations As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents LblReportPayroll As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label1 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents DataGridView3 As DataGridView
    Friend WithEvents DataGridView4 As DataGridView
    Friend WithEvents DataGridView5 As DataGridView
    Friend WithEvents btnReportAll As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents cmbYear As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents lblYear As Label
    Friend WithEvents lblMonth As Label
    Friend WithEvents cmbMonth As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents btnFilter As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents txtSearch As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btnSearch As Guna.UI2.WinForms.Guna2Button
End Class
