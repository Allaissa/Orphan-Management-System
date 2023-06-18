<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangePassword
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangePassword))
		Me.btnChangePassword = New System.Windows.Forms.Button()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.btnLoginMenu = New System.Windows.Forms.Button()
		Me.txtNewPassword = New System.Windows.Forms.TextBox()
		Me.txtCurrentPassword = New System.Windows.Forms.TextBox()
		Me.txtUser = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Guna2GradientPanel1 = New Guna.UI2.WinForms.Guna2GradientPanel()
		Me.GroupBox1.SuspendLayout()
		Me.Guna2GradientPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnChangePassword
		'
		Me.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.btnChangePassword.Location = New System.Drawing.Point(414, 237)
		Me.btnChangePassword.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.btnChangePassword.Name = "btnChangePassword"
		Me.btnChangePassword.Size = New System.Drawing.Size(199, 46)
		Me.btnChangePassword.TabIndex = 4
		Me.btnChangePassword.Text = "Change Password"
		Me.btnChangePassword.UseVisualStyleBackColor = False
		'
		'GroupBox1
		'
		Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
		Me.GroupBox1.Controls.Add(Me.btnLoginMenu)
		Me.GroupBox1.Controls.Add(Me.txtNewPassword)
		Me.GroupBox1.Controls.Add(Me.txtCurrentPassword)
		Me.GroupBox1.Controls.Add(Me.txtUser)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.btnChangePassword)
		Me.GroupBox1.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold)
		Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
		Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
		Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
		Me.GroupBox1.Size = New System.Drawing.Size(658, 399)
		Me.GroupBox1.TabIndex = 7
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Change Password"
		'
		'btnLoginMenu
		'
		Me.btnLoginMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.btnLoginMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.btnLoginMenu.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold)
		Me.btnLoginMenu.ForeColor = System.Drawing.Color.Black
		Me.btnLoginMenu.Image = CType(resources.GetObject("btnLoginMenu.Image"), System.Drawing.Image)
		Me.btnLoginMenu.Location = New System.Drawing.Point(7, 340)
		Me.btnLoginMenu.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.btnLoginMenu.Name = "btnLoginMenu"
		Me.btnLoginMenu.Size = New System.Drawing.Size(75, 52)
		Me.btnLoginMenu.TabIndex = 11
		Me.btnLoginMenu.UseVisualStyleBackColor = False
		'
		'txtNewPassword
		'
		Me.txtNewPassword.Location = New System.Drawing.Point(269, 191)
		Me.txtNewPassword.Margin = New System.Windows.Forms.Padding(4)
		Me.txtNewPassword.Name = "txtNewPassword"
		Me.txtNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
		Me.txtNewPassword.Size = New System.Drawing.Size(344, 30)
		Me.txtNewPassword.TabIndex = 10
		'
		'txtCurrentPassword
		'
		Me.txtCurrentPassword.Location = New System.Drawing.Point(269, 135)
		Me.txtCurrentPassword.Margin = New System.Windows.Forms.Padding(4)
		Me.txtCurrentPassword.Name = "txtCurrentPassword"
		Me.txtCurrentPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
		Me.txtCurrentPassword.Size = New System.Drawing.Size(344, 30)
		Me.txtCurrentPassword.TabIndex = 9
		'
		'txtUser
		'
		Me.txtUser.Location = New System.Drawing.Point(269, 80)
		Me.txtUser.Margin = New System.Windows.Forms.Padding(4)
		Me.txtUser.Name = "txtUser"
		Me.txtUser.Size = New System.Drawing.Size(344, 30)
		Me.txtUser.TabIndex = 8
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(75, 187)
		Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(142, 23)
		Me.Label3.TabIndex = 7
		Me.Label3.Text = "New Password"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(75, 132)
		Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(168, 23)
		Me.Label2.TabIndex = 6
		Me.Label2.Text = "Current Password"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(75, 80)
		Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(103, 23)
		Me.Label1.TabIndex = 5
		Me.Label1.Text = "Username"
		'
		'Guna2GradientPanel1
		'
		Me.Guna2GradientPanel1.Controls.Add(Me.GroupBox1)
		Me.Guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Guna2GradientPanel1.FillColor = System.Drawing.Color.White
		Me.Guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.Guna2GradientPanel1.Location = New System.Drawing.Point(0, 0)
		Me.Guna2GradientPanel1.Name = "Guna2GradientPanel1"
		Me.Guna2GradientPanel1.Size = New System.Drawing.Size(684, 425)
		Me.Guna2GradientPanel1.TabIndex = 12
		'
		'ChangePassword
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
		Me.ClientSize = New System.Drawing.Size(684, 425)
		Me.Controls.Add(Me.Guna2GradientPanel1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "ChangePassword"
		Me.ShowIcon = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.Guna2GradientPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents btnChangePassword As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnLoginMenu As Button
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents txtCurrentPassword As TextBox
    Friend WithEvents txtUser As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
	Friend WithEvents Guna2GradientPanel1 As Guna.UI2.WinForms.Guna2GradientPanel
End Class
