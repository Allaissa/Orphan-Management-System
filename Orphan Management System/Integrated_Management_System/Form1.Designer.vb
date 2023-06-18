<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.btnLogin = New System.Windows.Forms.Button()
		Me.txtUser = New System.Windows.Forms.TextBox()
		Me.txtPassword = New System.Windows.Forms.TextBox()
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(210, 65)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(83, 19)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Username"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(210, 142)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(81, 19)
		Me.Label2.TabIndex = 1
		Me.Label2.Text = "Password"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Label3.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(519, 326)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(123, 19)
		Me.Label3.TabIndex = 2
		Me.Label3.Text = "Forget Password"
		'
		'btnLogin
		'
		Me.btnLogin.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnLogin.Location = New System.Drawing.Point(300, 235)
		Me.btnLogin.Name = "btnLogin"
		Me.btnLogin.Size = New System.Drawing.Size(129, 35)
		Me.btnLogin.TabIndex = 3
		Me.btnLogin.Text = "Login"
		Me.btnLogin.UseVisualStyleBackColor = True
		'
		'txtUser
		'
		Me.txtUser.Location = New System.Drawing.Point(213, 104)
		Me.txtUser.Name = "txtUser"
		Me.txtUser.Size = New System.Drawing.Size(318, 22)
		Me.txtUser.TabIndex = 4
		'
		'txtPassword
		'
		Me.txtPassword.Location = New System.Drawing.Point(213, 185)
		Me.txtPassword.Name = "txtPassword"
		Me.txtPassword.Size = New System.Drawing.Size(318, 22)
		Me.txtPassword.TabIndex = 5
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.ClientSize = New System.Drawing.Size(800, 450)
		Me.Controls.Add(Me.txtPassword)
		Me.Controls.Add(Me.txtUser)
		Me.Controls.Add(Me.btnLogin)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Name = "Form1"
		Me.Text = "Form1"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents btnLogin As Button
	Friend WithEvents txtUser As TextBox
	Friend WithEvents txtPassword As TextBox
End Class
