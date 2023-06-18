Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing.Imaging


Public Class Dashboard
	Private connectionString As String = "Server=localhost;Database=integrated_management_system;Uid=root;Pwd=''"
	Dim connection As MySqlConnection = New MySqlConnection(connectionString)
	Public isExitConfirmed As Boolean = False
	Private dataView As DataView
	Private row As DataGridViewRow


	'CODE FOR DROP PANEL BUTTON
	Dim isCollapsed As Boolean = True
	Dim isCollapsed2 As Boolean = True
	Dim isCollapsed3 As Boolean = True

	Private Function ConfirmExit() As Boolean
		Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If result = DialogResult.Yes Then
			Return True
		Else
			Return False
		End If
	End Function
	Private Sub Dashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
		If Not isExitConfirmed Then
			Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If result = DialogResult.No Then
				e.Cancel = True ' Cancel the form closing event
			Else
				' Set the flag to true to indicate that the exit has been confirmed
				isExitConfirmed = True

				' Close the current form
				Me.Close()
			End If
		End If
	End Sub
	Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		For Each ctrl As Control In Me.Controls
			ctrl.Cursor = Cursors.Default
		Next

		dtpDateofAdmission.Format = DateTimePickerFormat.Custom
		dtpDateofAdmission.CustomFormat = "dd/MM/yy"
		DtpPatientBirthdate.Format = DateTimePickerFormat.Custom
		DtpPatientBirthdate.CustomFormat = "dd/MM/yy"

		DtpDonationDateDonated.Format = DateTimePickerFormat.Custom
		DtpDonationDateDonated.CustomFormat = "dd/MM/yy"
		DtpMedicalDateRecorded.Format = DateTimePickerFormat.Custom
		DtpMedicalDateRecorded.CustomFormat = "dd/MM/yy"
		dtpEmployeeBirthday.Format = DateTimePickerFormat.Custom
		dtpEmployeeBirthday.CustomFormat = "dd/MM/yy"

		comboload()
		comboloadPayroll()

		'Hide all created at 
		lblUserCreatedAt.Hide()
		lblUserUpdatedAt.Hide()
		txtUserCreatedAt.Hide()
		txtUserUpdatedAt.Hide()

		lblOrphanCreatedAt.Hide()
		lblOrphanUpdatedAt.Hide()
		txtPatientCreatedAt.Hide()
		txtPatientUpdatedAt.Hide()


		lblMedicalCreatedAt.Hide()
		lblMedicalUpdatedAt.Hide()
		txtMedicalHistoryMedicalCreatedAt.Hide()
		txtMedicalHistoryMedicalUpdatedAt.Hide()

		lblDonationCreatedAt.Hide()
		lblDonationUpdatedAt.Hide()
		txtDonationCreatedAt.Hide()
		txtDonationUpdatedAt.Hide()

		lblEmployeeID.Hide()
		lblEmployeeCreatedAt.Hide()
		lblEmployeeUpdatedAt.Hide()

		LblMedicationID.Hide()
		LblSuppliescreatedat.Hide()
		LblMedicalUpdated.Hide()
		txtMedicationID.Hide()
		txtMedicalcreatedat.Hide()
		txtMedicationupdatedat.Hide()

		LblPayrollid.Hide()
		lblPayrollCreatedat.Hide()
		lblPayrollupdatedat.Hide()
		txtPayrollid.Hide()
		txtPayrollCreatedat.Hide()
		txtPayrollUpdatedat.Hide()

		txtEmployeeID.Hide()
		txtEmployeeCreatedAt.Hide()
		txtEmployeeUpdatedAt.Hide()


		DropPanel1.Size = DropPanel1.MinimumSize
		'DropPanel2.Size = DropPanel2.MinimumSize
		DropPanel3.Size = DropPanel3.MinimumSize

		TabControl1.SizeMode = TabSizeMode.Fixed
		TabControl1.ItemSize = New Size(0, 1)
		For Each tabPage As TabPage In TabControl1.TabPages
			tabPage.BackColor = Color.White
			tabPage.Hide()

		Next

		Label4.Hide()
		txtUserID.Hide()
		txtUserCreatedAt.Enabled = False
		txtUserUpdatedAt.Enabled = False

		LoadDataUsers()
		DisabledTextboxesUsers()
		DisabledButtonsUsers()
		Guna2DataGridViewUsers.ReadOnly = True
		' Set the labels to display the logged in user's id and name
		DBlblUserId.Text = "User ID: " & LogIn.loggedInUserId.ToString()
		DBlblUserId.Hide()


		DBlblUserName.Text = "   " & LogIn.loggedInUserName

		'---------------------------------------Orphans
		Label6.Hide()
		txtPatientID.Hide()
		txtPatientCreatedAt.Enabled = False
		txtPatientUpdatedAt.Enabled = False

		LoadDataPatients()
		DisabledTextboxesPatients()
		DisabledButtonsPatients()
		Guna2DataGridViewOrphans.ReadOnly = True

		'--------------------------------Medical History
		Label25.Hide()
		txtMedicalHistoryMedicalID.Hide()
		txtMedicalHistoryMedicalCreatedAt.Enabled = False
		txtMedicalHistoryMedicalUpdatedAt.Enabled = False

		LoadDataMedicalHistory()
		DisabledTextboxesMedicalHistory()
		DisabledButtonsMedicalHistory()
		Guna2DataGridViewMedicalHistory.ReadOnly = True

		'--------------------------------Donations
		Label34.Hide()
		txtDonationID.Hide()
		txtDonationCreatedAt.Enabled = False
		txtDonationUpdatedAt.Enabled = False

		LoadDataDonations()
		DisabledTextboxesDonations()
		DisabledButtonsDonations()
		Guna2DataGridViewDonations.ReadOnly = True

		'--------------------------------Employee
		txtEmployeeID.Hide()
		txtEmployeeCreatedAt.Enabled = False
		txtEmployeeUpdatedAt.Enabled = False

		LoadDataEmployees()
		DisabledTextboxesEmployees()
		DisabledButtonsEmployees()
		Guna2DataGridViewEmployees.ReadOnly = True

		'--------------------------------Medical Supplies

		LoadDataMedicalSupplies()
		DisabledTextboxesMedicalSupplies()
		DisabledButtonsMedicalSupplies()
		Guna2DataGridView3.ReadOnly = True

		'--------------------------------Payroll

		LoadDataPayroll()
		EnabledTextboxesPayroll()
		DisabledButtonsPayroll()
		btnCancelpayroll.Enabled = False
		btnSavepayroll.Enabled = True
		Guna2DataGridView1.ReadOnly = True


		'Display total Users
		DisplayTotalUsers(lblTotalUsers)

		'Display total Donations
		DisplayTotalDonations(lblDonationSponsors)

		'Display total Employees
		DisplayTotalEmployee(lblTotalEmployee)

		'Display total Orphans
		DisplayTotalOrphans(lblTotalOrphans)

		'Display total Medcial History
		DisplayTotalMedicalHistory(lblTotalMedicalHistory)


		'Display total MedicalSupplies
		DisplayTotalMedicineSupplies(lblTotalMedicalSupplies)


		btnPatientDetails.Hide()
		btnStaffRecord.Hide()
		btnMedicalHistory.Hide()

		'btnStaff_Click.Hide()
		LoadDataEmployees()

	End Sub

	'****************************************Start DashBoard*******************************************************************************************
	Private Sub DisplayTotalUsers(ByVal lblTotalUsers As Label)
		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Select the count of rows from the users table
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM users", connection)
				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())

				' Display the count in the label
				lblTotalUsers.Text = count.ToString()
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while retrieving the data: " & ex.Message)
		End Try
	End Sub

	Private Sub DisplayTotalDonations(ByVal lblTotalDonations As Label)
		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Select the count of rows from the users table
				'Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM donations", connection)
				Dim selectCommand As New MySqlCommand("SELECT SUM(cash_amount) FROM donations", connection)
				Dim totalCashAmountObject As Object = selectCommand.ExecuteScalar()
				Dim totalCashAmount As Decimal = If(totalCashAmountObject IsNot DBNull.Value, Convert.ToDecimal(totalCashAmountObject), 0)
				'Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())

				lblDonationSponsors.Text = If(totalCashAmount = 0, "₱0", "₱" + totalCashAmount.ToString())

				' Display the count in the label
				'lblDonationSponsors.Text = "Total Donations/Sponsors: " & count.ToString()
				'lblDonationSponsors.Text = count.ToString()
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while retrieving the data: " & ex.ToString)
		End Try
	End Sub


	Private Sub DisplayTotalEmployee(ByVal lblTotalEmployee As Label)
		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Select the count of rows from the users table
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM employee", connection)
				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())

				' Display the count in the label

				lblTotalEmployee.Text = count.ToString()
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while retrieving the data: " & ex.Message)
		End Try
	End Sub

	Private Sub DisplayTotalOrphans(ByVal lblTotalOrphans As Label)
		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Select the count of rows from the users table
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM patients", connection)
				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())

				' Display the count in the label

				lblTotalOrphans.Text = count.ToString()
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while retrieving the data: " & ex.Message)
		End Try
	End Sub

	Private Sub DisplayTotalMedicalHistory(ByVal lblTotalOrphans As Label)
		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Select the count of rows from the users table
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM medical_history", connection)
				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())

				' Display the count in the label

				lblTotalOrphans.Text = count.ToString()
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while retrieving the data: " & ex.Message)
		End Try
	End Sub

	Private Sub DisplayTotalMedicineSupplies(ByVal lblMedicalSupplies As Label)
		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Select the count of rows from the users table
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM medication_schedule", connection)
				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())

				' Display the count in the label

				lblTotalMedicalSupplies.Text = count.ToString()
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while retrieving the data: " & ex.Message)
		End Try
	End Sub
	
	Private Sub DisplayTotalPayroll(ByVal lblTotalPayroll As Label)
		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Select the count of rows from the users table
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM payroll", connection)
				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())

				' Display the count in the label

				lblTotalPayroll.Text = count.ToString()
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while retrieving the data: " & ex.Message)
		End Try
	End Sub


	'****************************************End   DashBoard*******************************************************************************************
	Private Sub ClearTextboxesUsers()
		txtUserName.Focus()
		txtUserID.Clear()
		txtUserName.Clear()
		txtUserPassword.Clear()
		txtUserCreatedAt.Clear()
		txtUserUpdatedAt.Clear()
	End Sub
	Private Sub DisabledTextboxesUsers()
		txtUserName.Enabled = False
		txtUserPassword.Enabled = False
		'txtCreatedAt.Enabled = False
		'txtUpdatedAt.Enabled = False
	End Sub
	Private Sub EnabledTextboxesUsers()

		txtUserName.Enabled = True
		txtUserPassword.Enabled = True

		'txtCreatedAt.Enabled = True
		'txtUpdatedAt.Enabled = True
	End Sub
	Private Sub DisabledButtonsUsers()

		'btnNew.Enabled = False
		btnUsersSave.Enabled = False

		btnUsersUpdate.Enabled = False
		btnUsersDelete.Enabled = False
		btnUsersCancel.Enabled = False
	End Sub
	Private Sub Guna2DataGridViewUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridViewUsers.CellClick
		EnabledTextboxesUsers()
		btnUsersNew.Enabled = False
		btnUsersUpdate.Enabled = True
		btnUsersDelete.Enabled = True
		btnUsersCancel.Enabled = True

		' Get the selected row
		Dim selectedRow As DataGridViewRow = Nothing
		If e.RowIndex >= 0 AndAlso e.RowIndex < Guna2DataGridViewUsers.Rows.Count Then
			selectedRow = Guna2DataGridViewUsers.Rows(e.RowIndex)
		End If

		' Display the values in the text boxes if the row is not null
		If selectedRow IsNot Nothing Then
			txtUserID.Text = selectedRow.Cells("UserId").Value.ToString()
			txtUserName.Text = selectedRow.Cells("Username").Value.ToString()
			txtUserPassword.Text = selectedRow.Cells("Password").Value.ToString()
			txtUserCreatedAt.Text = selectedRow.Cells("CreatedAt").Value.ToString()
			txtUserUpdatedAt.Text = selectedRow.Cells("UpdatedAt").Value.ToString()


		End If
	End Sub
	Private Sub LoadDataUsers()
		Dim searchQuery As String = "SELECT user_id as 'UserId', user_name AS 'Username'," &
								"REPEAT('*', CHAR_LENGTH(user_password)) AS 'Password'," &
								"user_created_at AS 'CreatedAt', user_updated_at AS 'UpdatedAt' FROM users"

		If Not String.IsNullOrWhiteSpace(txtUsersSearch.Text) Then
			searchQuery += $" WHERE user_name LIKE '%{txtUsersSearch.Text}%'"
		End If

		Dim dataAdapter As New MySqlDataAdapter(searchQuery, connection)

		cboUsersType.Items.Clear() ' Clear existing items in cboUsersType
		Guna2DataGridViewUsers.ScrollBars = ScrollBars.Vertical

		' Set the default cell style for each column
		For Each col As DataGridViewColumn In Guna2DataGridViewUsers.Columns
			col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
		Next

		' Change the font size of column headers
		Guna2DataGridViewUsers.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Change the font size of cell values
		Guna2DataGridViewUsers.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Set the CellBorderStyle property of the GunaDataGridView control
		Guna2DataGridViewUsers.CellBorderStyle = DataGridViewCellBorderStyle.Single

		' Set the GridColor property of the GunaDataGridView control
		Guna2DataGridViewUsers.GridColor = Color.Black

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				Dim dataAdapterUsers As New MySqlDataAdapter("SELECT user_id as 'UserId', user_name AS 'Username'," &
													"REPEAT('*', CHAR_LENGTH(user_password)) AS 'Password'," &
													"user_created_at AS 'CreatedAt', user_updated_at AS 'UpdatedAt' FROM users", connection)

				Dim table As New DataTable()
				dataAdapterUsers.Fill(table)

				Guna2DataGridViewUsers.DataSource = table

				' Hide the UserId column
				If Guna2DataGridViewUsers.Columns.Contains("UserId") Then
					Guna2DataGridViewUsers.Columns("UserId").Visible = False
				End If

				' Set the size of columns
				If Guna2DataGridViewUsers.Columns.Contains("Username") Then
					Guna2DataGridViewUsers.Columns("Username").Width = 150
				Else
					'MessageBox.Show("Column does not exist")
				End If

				If Guna2DataGridViewUsers.Columns.Contains("Password") Then
					Guna2DataGridViewUsers.Columns("Password").Width = 150
				End If

				If Guna2DataGridViewUsers.Columns.Contains("CreatedAt") Then
					Guna2DataGridViewUsers.Columns("CreatedAt").Width = 200
				End If

				If Guna2DataGridViewUsers.Columns.Contains("UpdatedAt") Then
					Guna2DataGridViewUsers.Columns("UpdatedAt").Width = 200
				End If


				' Set the cell alignment
				If Guna2DataGridViewUsers.Columns.Contains("Password") Then
					Guna2DataGridViewUsers.Columns("Password").Width = 150
				End If

				If Guna2DataGridViewUsers.Columns.Contains("CreatedAt") Then
					Guna2DataGridViewUsers.Columns("CreatedAt").Width = 200
				End If

				If Guna2DataGridViewUsers.Columns.Contains("UpdatedAt") Then
					Guna2DataGridViewUsers.Columns("UpdatedAt").Width = 200
				End If

				If Guna2DataGridViewUsers.Columns.Contains("Username") Then
					Guna2DataGridViewUsers.Columns("Username").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
				End If

				If Guna2DataGridViewUsers.Columns.Contains("Password") Then
					Guna2DataGridViewUsers.Columns("Password").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
				End If


				If Guna2DataGridViewUsers.Columns.Contains("CreatedAt") Then
					Guna2DataGridViewUsers.Columns("CreatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				End If

				If Guna2DataGridViewUsers.Columns.Contains("UpdatedAt") Then
					Guna2DataGridViewUsers.Columns("UpdatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				End If

				' Set the cell padding
				If Guna2DataGridViewUsers.RowCount > 0 Then
					Guna2DataGridViewUsers.Columns("Username").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
					Guna2DataGridViewUsers.Columns("Password").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
					Guna2DataGridViewUsers.Columns("CreatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
					Guna2DataGridViewUsers.Columns("UpdatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

					Guna2DataGridViewUsers.Columns("Username").DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
					Guna2DataGridViewUsers.Columns("Password").DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
					Guna2DataGridViewUsers.Columns("CreatedAt").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
					Guna2DataGridViewUsers.Columns("UpdatedAt").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
				End If

				' Set the header text
				If Guna2DataGridViewUsers.Columns.Contains("Username") Then
					Guna2DataGridViewUsers.Columns("Username").HeaderText = "Username"
				End If

				If Guna2DataGridViewUsers.Columns.Contains("Password") Then
					Guna2DataGridViewUsers.Columns("Password").HeaderText = "Password"
				End If


				If Guna2DataGridViewUsers.Columns.Contains("CreatedAt") Then
					Guna2DataGridViewUsers.Columns("CreatedAt").HeaderText = "Created At"
				End If

				If Guna2DataGridViewUsers.Columns.Contains("UpdatedAt") Then
					Guna2DataGridViewUsers.Columns("UpdatedAt").HeaderText = "Updated At"
				End If

				' Center the header text
				If Guna2DataGridViewUsers.Columns.Contains("Username") Then
					Guna2DataGridViewUsers.Columns("Username").Width = 150
					Guna2DataGridViewUsers.Columns("Username").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
					Guna2DataGridViewUsers.Columns("Username").DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
					Guna2DataGridViewUsers.Columns("Username").HeaderText = "Username"
					Guna2DataGridViewUsers.Columns("Username").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				End If

				If Guna2DataGridViewUsers.Columns.Contains("Password") Then
					Guna2DataGridViewUsers.Columns("Password").Width = 150
					Guna2DataGridViewUsers.Columns("Password").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
					Guna2DataGridViewUsers.Columns("Password").DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
					Guna2DataGridViewUsers.Columns("Password").HeaderText = "Password"
					Guna2DataGridViewUsers.Columns("Password").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				End If

				If Guna2DataGridViewUsers.Columns.Contains("CreatedAt") Then
					Guna2DataGridViewUsers.Columns("CreatedAt").Width = 200
					Guna2DataGridViewUsers.Columns("CreatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
					Guna2DataGridViewUsers.Columns("CreatedAt").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
					Guna2DataGridViewUsers.Columns("CreatedAt").HeaderText = "Created At"
					Guna2DataGridViewUsers.Columns("CreatedAt").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				End If

				If Guna2DataGridViewUsers.Columns.Contains("UpdatedAt") Then
					Guna2DataGridViewUsers.Columns("UpdatedAt").Width = 200
					Guna2DataGridViewUsers.Columns("UpdatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
					Guna2DataGridViewUsers.Columns("UpdatedAt").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
					Guna2DataGridViewUsers.Columns("UpdatedAt").HeaderText = "Updated At"
					Guna2DataGridViewUsers.Columns("UpdatedAt").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				End If

			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while loading the data: " + ex.Message)
		End Try



	End Sub



	Private Sub btnLogout_Click(sender As Object, e As EventArgs)

	End Sub



	Private Sub btnOrphanInformation_Click(sender As Object, e As EventArgs)

	End Sub

	Private Sub Guna2Button8_Click(sender As Object, e As EventArgs)

	End Sub

	Private Sub btnUsersNew_Click(sender As Object, e As EventArgs) Handles btnUsersNew.Click
		EnabledTextboxesUsers()
		ClearTextboxesUsers()
		txtUserName.Focus()
		btnUsersNew.Enabled = False
		btnUsersSave.Enabled = True
		btnUsersDelete.Enabled = False
		btnUsersCancel.Enabled = True
		Guna2DataGridViewUsers.Enabled = False
	End Sub

	Private Sub btnLogout_Click_1(sender As Object, e As EventArgs) Handles btnLogout.Click
		Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If result = DialogResult.Yes Then
			' Clear the previously logged in user name
			LogIn.loggedInUserName = ""

			' Close the current form
			Me.Close()
			LogIn.Show()
		End If
	End Sub

	Private Sub btnUsersSave_Click(sender As Object, e As EventArgs) Handles btnUsersSave.Click
		If String.IsNullOrEmpty(txtUserName.Text) OrElse String.IsNullOrEmpty(txtUserPassword.Text) OrElse String.IsNullOrEmpty(txtUserStatus.Text) OrElse String.IsNullOrEmpty(cboUsersType.Text) Then
			MessageBox.Show("Please fill in all required fields.")
			Return
		End If

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Check if a record with the same user name already exists
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM users WHERE user_name = @user_name", connection)
				selectCommand.Parameters.AddWithValue("@user_name", txtUserName.Text)

				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
				If count > 0 Then
					MessageBox.Show("A user with the same user name already exists.")
					Return
				End If

				' Insert a new record
				Dim insertcommand As New MySqlCommand("INSERT INTO users (user_name, user_password,user_created_at) VALUES (@user_name, @user_password,@user_created_at)", connection)
				insertcommand.Parameters.AddWithValue("@user_name", txtUserName.Text)
				insertcommand.Parameters.AddWithValue("@user_password", txtUserPassword.Text)


				insertcommand.Parameters.AddWithValue("@user_created_at", DateTime.Now)

				insertcommand.ExecuteNonQuery()
				btnUsersSave.Enabled = False
				btnUsersNew.Enabled = True
				btnUsersCancel.Enabled = False
				MessageBox.Show("Data saved successfully.")
				LoadDataUsers()
				ClearTextboxesUsers()
				DisabledTextboxesUsers()
				Guna2DataGridViewUsers.Enabled = True
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while saving the data: " + ex.Message)
		End Try
	End Sub
	Private Sub btnUsersUpdate_Click(sender As Object, e As EventArgs) Handles btnUsersUpdate.Click
		If Guna2DataGridViewUsers.SelectedRows.Count > 0 Then
			Dim selectedRow = Guna2DataGridViewUsers.SelectedRows(0)
			Dim userId = selectedRow.Cells("UserId").Value

			' Check if any changes have been made
			If txtUserName.Text = selectedRow.Cells("Username").Value AndAlso
		   txtUserPassword.Text = selectedRow.Cells("Password").Value AndAlso
				MessageBox.Show("There are no changes to update.") Then
				Return
			End If

			' Show confirmation prompt
			If MessageBox.Show("Are you sure you want to update this user?", "Confirm Update", MessageBoxButtons.YesNo) = DialogResult.Yes Then
				Try
					Using connection As New MySqlConnection(connectionString)
						connection.Open()

						' Check if user type is not empty before updating
						'If Not String.IsNullOrEmpty(cboUsersType.SelectedItem) Then
						Dim updateCommand As New MySqlCommand("update users set user_name=@user_name, user_password=@user_password, user_updated_at=@user_updated_at where user_id=@user_id", connection)

							updateCommand.Parameters.AddWithValue("@user_name", txtUserName.Text)
							updateCommand.Parameters.AddWithValue("@user_password", txtUserPassword.Text)

							updateCommand.Parameters.AddWithValue("@user_updated_at", DateTime.Now)
							updateCommand.Parameters.AddWithValue("@user_id", userId)

							updateCommand.ExecuteNonQuery()

							MessageBox.Show("Data updated successfully.")
							LoadDataUsers()
							ClearTextboxesUsers()
							DisabledTextboxesUsers()
							DisplayTotalUsers(lblTotalUsers)
							DisabledButtonsUsers()
							btnUsersNew.Enabled = True
						'Else
						'MessageBox.Show("Please select a user type.")
						'End If
					End Using
				Catch ex As Exception
					MessageBox.Show("An error occurred while updating the data: " + ex.Message)
				End Try
			End If
		Else
			MessageBox.Show("Please select a row to update.")
		End If
	End Sub




	Private Sub Guna2DataGridViewUsers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridViewUsers.CellContentClick

	End Sub

	Private Sub btnUsersDelete_Click(sender As Object, e As EventArgs) Handles btnUsersDelete.Click
		' Get the selected row ID from the DataGridView
		If Guna2DataGridViewUsers.SelectedRows.Count = 0 Then
			MessageBox.Show("Please select a row to delete.")
			Return
		End If
		Dim rowId As Integer = CInt(Guna2DataGridViewUsers.SelectedRows(0).Cells("UserId").Value)

		' Confirm deletion with the user
		Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo)
		If result = DialogResult.Yes Then
			Try
				Using connection As New MySqlConnection(connectionString)
					connection.Open()

					Dim deleteCommand As New MySqlCommand("DELETE FROM users WHERE user_id = @id", connection)
					deleteCommand.Parameters.AddWithValue("@id", rowId)

					deleteCommand.ExecuteNonQuery()
				End Using
				LoadDataUsers()
				MessageBox.Show("Record deleted successfully.")
				ClearTextboxesUsers()
				DisabledTextboxesUsers()
				DisabledButtonsUsers()
				DisplayTotalUsers(lblTotalUsers)
				btnUsersSave.Enabled = False
				btnUsersNew.Enabled = True
			Catch ex As Exception
				MessageBox.Show("An error occurred while deleting the row: " & ex.Message)
			End Try
		End If
		'btnDelete.Enabled = False
	End Sub


	Private Sub cboUsersType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUsersType.SelectedIndexChanged

	End Sub

	Private Sub btnUsersCancel_Click(sender As Object, e As EventArgs) Handles btnUsersCancel.Click
		DisabledButtonsUsers()
		ClearTextboxesUsers()
		DisabledTextboxesUsers()
		btnUsersNew.Enabled = True
		Guna2DataGridViewUsers.Enabled = True
	End Sub







	Private Sub TabPageUsers_Click(sender As Object, e As EventArgs) Handles TabPageUsers.Click

	End Sub
	Private Sub btnUsersSearch_Click(sender As Object, e As EventArgs) Handles btnUsersSearch.Click
		Dim searchText As String = txtUsersSearch.Text

		' Query the database or data source to get the search results
		' For example, if you're using a MySQL database:
		Dim sql As String = $"SELECT user_id AS UserId, user_name AS Username, user_created_at AS CreatedAt, user_updated_at AS UpdatedAt FROM users WHERE user_name LIKE '%{searchText}%'"
		Dim searchResults As New DataTable()
		Using connection As New MySqlConnection(connectionString)
			Using command As New MySqlCommand(sql, connection)
				connection.Open()
				searchResults.Load(command.ExecuteReader())
			End Using
		End Using

		' Bind the results to the DataGridView
		Guna2DataGridViewUsers.DataSource = searchResults

		' Check if any rows were found
		If searchResults.Rows.Count = 0 Then
			MessageBox.Show("No data found.")
		End If
	End Sub







	Private Sub txtUsersSearch_TextChanged(sender As Object, e As EventArgs) Handles txtUsersSearch.TextChanged
		If String.IsNullOrEmpty(txtUsersSearch.Text.Trim()) Then
			LoadDataUsers()
			btnUsersSearch.Enabled = False
		Else
			btnUsersSearch.Enabled = True
		End If
		'Dim searchText As String = txtUsersSearch.Text.Trim()

		'If String.IsNullOrEmpty(searchText) Then
		'    LoadDataUsers()
		'    btnUsersSearch.Enabled = False
		'Else
		'    btnUsersSearch.Enabled = True
		'    Dim sql As String = $"SELECT * FROM users WHERE user_name LIKE '%{searchText}%'"
		'    Dim searchResults As New DataTable()
		'    Using connection As New MySqlConnection(connectionString)
		'        Using command As New MySqlCommand(sql, connection)
		'            connection.Open()
		'            searchResults.Load(command.ExecuteReader())
		'        End Using
		'    End Using

		'    If searchResults.Rows.Count > 0 Then
		'        Guna2DataGridViewUsers.DataSource = searchResults
		'    Else
		'        MessageBox.Show("No results found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
		'        LoadDataUsers()
		'    End If
		'End If
	End Sub

	Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

	End Sub



	'-------------------------------------------------End Users----------------------------------------------------------------

	'-------------------------------------------------Start Orphans---------------------------------------------------------------
	Private Sub Guna2DataGridViewOrphans_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridViewOrphans.CellClick
		For Each column As DataGridViewColumn In Guna2DataGridViewOrphans.Columns
			Console.WriteLine(column.Name)
		Next
		EnabledTextboxesPatients()
		btnPatientsNew.Enabled = False
		btnPatientsUpdate.Enabled = True
		btnPatientsDelete.Enabled = True
		btnPatientsCancel.Enabled = True

		' Get the selected row
		Dim selectedRow As DataGridViewRow = Nothing
		If e.RowIndex >= 0 AndAlso e.RowIndex < Guna2DataGridViewOrphans.Rows.Count Then
			selectedRow = Guna2DataGridViewOrphans.Rows(e.RowIndex)
		End If

		' Display the values in the text boxes if the row is not null
		If selectedRow IsNot Nothing Then
			''txtPatientID.Text = selectedRow.Cells("PatientId").Value.ToString()
			dtpDateofAdmission.Text = selectedRow.Cells("DateofAdmission").Value.ToString()
			txtPatientFirstName.Text = selectedRow.Cells("FirstName").Value.ToString()
			txtPatientMiddleName.Text = selectedRow.Cells("MiddleName").Value.ToString()
			txtPatientLastName.Text = selectedRow.Cells("LastName").Value.ToString()
			DtpPatientBirthdate.Text = selectedRow.Cells("BirthDate").Value.ToString()
			txtPlaceofBirth.Text = selectedRow.Cells("PlaceofBirth").Value.ToString()
			cboPatientStatus.Text = selectedRow.Cells("Status").Value.ToString()
			txtPatientFamilyMemberName.Text = selectedRow.Cells("FamilyMemberName").Value.ToString()
			txtReligion.Text = selectedRow.Cells("Religion").Value.ToString()
			txtEducational.Text = selectedRow.Cells("EducationalAttainment").Value.ToString()
			txtRelationtoClient.Text = selectedRow.Cells("RelationtoClient").Value.ToString()
			txtOrphanAddress.Text = selectedRow.Cells("Address").Value.ToString()
			txtFamilyAddress.Text = selectedRow.Cells("FamilyAddress").Value.ToString()
			txtPatientEmergencyNumber.Text = selectedRow.Cells("EmergencyNumber").Value.ToString()
			txtPatientDescription.Text = selectedRow.Cells("Description").Value.ToString()

			txtUserCreatedAt.Text = selectedRow.Cells("CreatedAt").Value.ToString()
			txtUserUpdatedAt.Text = selectedRow.Cells("UpdatedAt").Value.ToString()
		End If
	End Sub

	Private Sub LoadDataPatients()
		cboPatientStatus.Items.Clear() ' Clear existing items in cboPatientStatus
		Guna2DataGridViewOrphans.ScrollBars = ScrollBars.Vertical

		' Set the default cell style for each column
		For Each col As DataGridViewColumn In Guna2DataGridViewOrphans.Columns
			col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
		Next

		' Change the font size of column headerssss
		Guna2DataGridViewOrphans.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Change the font size of cell values
		Guna2DataGridViewOrphans.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Set the CellBorderStyle property of the GunaDataGridView control
		Guna2DataGridViewOrphans.CellBorderStyle = DataGridViewCellBorderStyle.Single

		' Set the GridColor property of the GunaDataGridView control
		Guna2DataGridViewOrphans.GridColor = Color.Black

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				Dim dataAdapterOrphans As New MySqlDataAdapter("SELECT patient_id AS 'PatientId'," &
															"patient_date_of_admission AS 'DateofAdmission', " &
															"patient_first_name AS 'FirstName'," &
															"patient_middle_name AS 'MiddleName', " &
															"patient_last_name AS 'LastName', " &
															"patient_orphan_address AS 'Address', " &
															"patient_birth_date AS 'BirthDate', " &
															"patient_place_of_birth AS 'PlaceofBirth', " &
															"patient_status AS 'Status', " &
															"patient_religion AS 'Religion', " &
															"patient_educational_attainment AS 'EducationalAttainment', " &
															"patient_family_member_name AS 'FamilyMemberName', " &
															"patient_relation_to_client AS 'RelationtoClient', " &
															"patient_family_address AS 'FamilyAddress', " &
															"patient_emergency_number AS 'EmergencyNumber', " &
															"patient_description AS 'Description', " &
															"patient_created_at AS 'CreatedAt', patient_updated_at AS 'UpdatedAt' FROM patients", connection)

				Dim table As New DataTable()
				dataAdapterOrphans.Fill(table)



				Guna2DataGridViewOrphans.DataSource = table
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while loading the data: " + ex.Message)
		End Try

		' Add the items to the GunaComboBox control using an array
		Dim items() As String = {"Single", "Married", "Widowed"}
		cboPatientStatus.Items.AddRange(items)
	End Sub


	Private Sub ClearTextboxesPatients()
		txtPatientFirstName.Focus()
		txtPatientFirstName.Clear()
		txtPatientID.Clear()
		txtPatientMiddleName.Clear()
		txtPatientLastName.Clear()
		txtOrphanAddress.Clear()
		txtFamilyAddress.Clear()
		txtPatientDescription.Clear()
		txtPlaceofBirth.Clear()
		txtReligion.Clear()
		txtEducational.Clear()


		cboPatientStatus.SelectedItem = Nothing
		txtPatientFamilyMemberName.Clear()
		txtPatientEmergencyNumber.Clear()
		txtRelationtoClient.Clear()


	End Sub
	Private Sub DisabledTextboxesPatients()
		txtPatientFirstName.Enabled = False
		txtPatientMiddleName.Enabled = False
		txtPatientLastName.Enabled = False
		DtpPatientBirthdate.Enabled = False
		cboPatientStatus.Enabled = False
		txtPatientEmergencyNumber.Enabled = False
		txtPatientFamilyMemberName.Enabled = False
		txtPatientDescription.Enabled = False
		txtOrphanAddress.Enabled = False
		txtFamilyAddress.Enabled = False
		txtPlaceofBirth.Enabled = False
		txtReligion.Enabled = False
		txtEducational.Enabled = False
		txtRelationtoClient.Enabled = False
		dtpDateofAdmission.Enabled = False

	End Sub
	Private Sub EnabledTextboxesPatients()
		txtPatientFirstName.Enabled = True
		txtPatientMiddleName.Enabled = True
		txtPatientLastName.Enabled = True
		DtpPatientBirthdate.Enabled = True
		cboPatientStatus.Enabled = True
		txtPatientEmergencyNumber.Enabled = True
		txtPatientFamilyMemberName.Enabled = True
		txtPatientDescription.Enabled = True
		txtOrphanAddress.Enabled = True
		txtPlaceofBirth.Enabled = True
		txtFamilyAddress.Enabled = True
		txtReligion.Enabled = True
		txtEducational.Enabled = True
		txtRelationtoClient.Enabled = True
		dtpDateofAdmission.Enabled = True


	End Sub
	Private Sub DisabledButtonsPatients()
		'btnNew.Enabled = False
		btnPatientsSave.Enabled = False
		btnPatientsUpdate.Enabled = False
		btnPatientsDelete.Enabled = False
		btnPatientsCancel.Enabled = False
	End Sub

	Private Sub TabPagePatients_Click(sender As Object, e As EventArgs) Handles TabPagePatients.Click

	End Sub

	Private Sub btnPatientsCancel_Click(sender As Object, e As EventArgs) Handles btnPatientsCancel.Click
		DisabledButtonsPatients()
		ClearTextboxesPatients()
		DisabledTextboxesPatients()
		btnPatientsNew.Enabled = True
		Guna2DataGridViewOrphans.Enabled = True
	End Sub

	Private Sub btnPatientsNew_Click(sender As Object, e As EventArgs) Handles btnPatientsNew.Click
		EnabledTextboxesPatients()
		ClearTextboxesPatients()
		txtPatientFirstName.Focus()
		btnPatientsNew.Enabled = False
		btnPatientsSave.Enabled = True
		btnPatientsDelete.Enabled = False
		btnPatientsCancel.Enabled = True
		Guna2DataGridViewOrphans.Enabled = False
	End Sub

	Private Sub btnPatientsSave_Click(sender As Object, e As EventArgs) Handles btnPatientsSave.Click
		'If String.IsNullOrEmpty(txtPatientFirstName.Text) OrElse String.IsNullOrEmpty(txtPatientMiddleName.Text) OrElse String.IsNullOrEmpty(txtPatientLastName.Text) OrElse String.IsNullOrEmpty(cboPatientStatus.Text) Then
		'	MessageBox.Show("Please fill in all required fields.")
		'	Return
		'End If

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Check if a record with the same user name already exists
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM patients WHERE patient_fullname = @patient_fullname", connection)
				selectCommand.Parameters.AddWithValue("@patient_fullname", txtPatientFirstName.Text + " " + txtPatientMiddleName.Text + " " + txtPatientLastName.Text)

				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
				If count > 0 Then
					MessageBox.Show("An orphan with the same name already exists.")
					Return
				End If



				' Insert a new record
				' If inputs are empty insert N/A to textboxes
				Dim insertcommand As New MySqlCommand("INSERT INTO patients (patient_date_of_admission, patient_first_name, patient_middle_name, patient_last_name, patient_fullname, patient_birth_date, patient_description, patient_status,patient_family_member_name,patient_emergency_number,patient_created_at,patient_orphan_address,patient_place_of_birth,patient_religion,patient_educational_attainment,patient_family_address,patient_relation_to_client) VALUES (@patient_date_of_admission, @patient_first_name, @patient_middle_name, @patient_last_name, @patient_fullname, @patient_birth_date,@patient_description,@patient_status,@patient_family_member_name,@patient_emergency_number,@patient_created_at,@patient_orphan_address,@patient_place_of_birth,@patient_religion,@patient_educational_attainment,@patient_family_address,@patient_relation_to_client)", connection)

				insertcommand.Parameters.AddWithValue("@patient_date_of_admission", dtpDateofAdmission.Text)
				insertcommand.Parameters.AddWithValue("@patient_first_name", txtPatientFirstName.Text)
				insertcommand.Parameters.AddWithValue("@patient_middle_name", txtPatientMiddleName.Text)
				insertcommand.Parameters.AddWithValue("@patient_last_name", txtPatientLastName.Text)
				insertcommand.Parameters.AddWithValue("@patient_fullname", txtPatientFirstName.Text + " " + txtPatientMiddleName.Text + " " + txtPatientLastName.Text)
				insertcommand.Parameters.AddWithValue("@patient_birth_date", DtpPatientBirthdate.Text)
				insertcommand.Parameters.AddWithValue("@patient_description", txtPatientDescription.Text)
				insertcommand.Parameters.AddWithValue("@patient_status", cboPatientStatus.Text)
				insertcommand.Parameters.AddWithValue("@patient_family_member_name", txtPatientFamilyMemberName.Text)
				insertcommand.Parameters.AddWithValue("@patient_orphan_address", txtOrphanAddress.Text)
				insertcommand.Parameters.AddWithValue("@patient_place_of_birth", txtPlaceofBirth.Text)
				insertcommand.Parameters.AddWithValue("@patient_religion", txtReligion.Text)
				insertcommand.Parameters.AddWithValue("@patient_educational_attainment", txtEducational.Text)
				insertcommand.Parameters.AddWithValue("@patient_relation_to_client", txtRelationtoClient.Text)
				insertcommand.Parameters.AddWithValue("@patient_family_address", txtFamilyAddress.Text)

				Dim input As String = txtPatientEmergencyNumber.Text
				Dim regex As New Regex("^[0-9]{11}$")
				If regex.IsMatch(input) Or String.IsNullOrEmpty(input) Then
					insertcommand.Parameters.AddWithValue("@patient_emergency_number", input)
				Else
					MessageBox.Show("Input 11 digits starting from 0-9.")
					Return
				End If


				insertcommand.Parameters.AddWithValue("@patient_created_at", DateTime.Now)

				insertcommand.ExecuteNonQuery()
				btnPatientsSave.Enabled = False
				btnPatientsNew.Enabled = True
				btnPatientsCancel.Enabled = False
				MessageBox.Show("Data saved successfully.")
				LoadDataPatients()
				ClearTextboxesPatients()
				DisabledTextboxesPatients()
				DisplayTotalOrphans(lblTotalOrphans)
				Guna2DataGridViewOrphans.Enabled = True
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while saving the data: " + ex.Message)
		End Try
		comboload()
	End Sub

	Private Sub btnPatientsUpdate_Click(sender As Object, e As EventArgs) Handles btnPatientsUpdate.Click
		If Guna2DataGridViewOrphans.SelectedRows.Count > 0 Then
			Dim selectedRow = Guna2DataGridViewOrphans.SelectedRows(0)
			If selectedRow.Cells("BirthDate") IsNot Nothing AndAlso selectedRow.Cells("Status") IsNot Nothing AndAlso selectedRow.Cells("FamilyMemberName") IsNot Nothing AndAlso selectedRow.Cells("EmergencyNumber") IsNot Nothing Then
				Dim patientId = selectedRow.Cells("PatientId").Value
				' Check if any changes have been made
				If txtPatientFirstName.Text = selectedRow.Cells("FirstName").Value AndAlso
				   dtpDateofAdmission.Text = selectedRow.Cells("DateofAdmission").Value AndAlso
					txtPatientMiddleName.Text = selectedRow.Cells("MiddleName").Value AndAlso
					txtPatientLastName.Text = selectedRow.Cells("LastName").Value AndAlso
					txtOrphanAddress.Text = selectedRow.Cells("Address").Value AndAlso
					DtpPatientBirthdate.Text = selectedRow.Cells("BirthDate").Value AndAlso
					txtPlaceofBirth.Text = selectedRow.Cells("PlaceofBirth").Value AndAlso
					cboPatientStatus.Text = selectedRow.Cells("Status").Value AndAlso
					txtReligion.Text = selectedRow.Cells("Religion").Value AndAlso
					txtEducational.Text = selectedRow.Cells("EducationalAttainment").Value AndAlso
					txtRelationtoClient.Text = selectedRow.Cells("RelationtoClient").Value AndAlso
					txtFamilyAddress.Text = selectedRow.Cells("FamilyAddress").Value AndAlso
					txtPatientFamilyMemberName.Text = selectedRow.Cells("FamilyMemberName").Value AndAlso
					txtPatientEmergencyNumber.Text = selectedRow.Cells("EmergencyNumber").Value AndAlso
					 txtPatientDescription.Text = selectedRow.Cells("Description").Value Then
					MessageBox.Show("There are no changes to update.")
					Return
				End If

				' Show confirmation prompt
				If MessageBox.Show("Are you sure you want to update this Elderly Orphan?", "Confirm Update", MessageBoxButtons.YesNo) = DialogResult.Yes Then
					Try
						Using connection As New MySqlConnection(connectionString)
							connection.Open()
							' Check if a record with the same user name already exists
							Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM patients WHERE patient_fullname = @patient_fullname AND patient_id <> @patient_id", connection)

							selectCommand.Parameters.AddWithValue("@patient_fullname", txtPatientFirstName.Text + " " + txtPatientMiddleName.Text + " " + txtPatientLastName.Text)
							selectCommand.Parameters.AddWithValue("@patient_id", patientId)

							Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
							If count > 0 Then
								MessageBox.Show("An orphan with the same name already exists.")
								Return
							End If

							' Check if patient status is not empty before updating
							If Not String.IsNullOrEmpty(cboPatientStatus.SelectedItem) Then
								Dim updateCommand As New MySqlCommand("UPDATE patients SET	patient_date_of_admission=@patient_date_of_admission,patient_first_name=@patient_first_name, patient_middle_name=@patient_middle_name, patient_last_name=@patient_last_name,patient_fullname=@patient_fullname,patient_birth_date=@patient_birth_date, patient_description = @patient_description ,patient_status=@patient_status, patient_family_member_name=@patient_family_member_name, patient_emergency_number=@patient_emergency_number, patient_updated_at=@patient_updated_at,patient_orphan_address=@patient_orphan_address,patient_place_of_birth=@patient_place_of_birth,patient_religion=@patient_religion,patient_educational_attainment=@patient_educational_attainment,patient_family_address=@patient_family_address,patient_relation_to_client=@patient_relation_to_client WHERE patient_id=@patient_id", connection)

								updateCommand.Parameters.AddWithValue("@patient_date_of_admission", dtpDateofAdmission.Text)
								updateCommand.Parameters.AddWithValue("@patient_first_name", txtPatientFirstName.Text)
								updateCommand.Parameters.AddWithValue("@patient_middle_name", txtPatientMiddleName.Text)
								updateCommand.Parameters.AddWithValue("@patient_last_name", txtPatientLastName.Text)
								updateCommand.Parameters.AddWithValue("@patient_fullname", txtPatientFirstName.Text + " " + txtPatientMiddleName.Text + " " + txtPatientLastName.Text)
								updateCommand.Parameters.AddWithValue("@patient_orphan_address", txtOrphanAddress.Text)
								updateCommand.Parameters.AddWithValue("@patient_birth_date", DtpPatientBirthdate.Text)
								updateCommand.Parameters.AddWithValue("@patient_place_of_birth", txtPlaceofBirth.Text)
								updateCommand.Parameters.AddWithValue("@patient_status", cboPatientStatus.Text)
								updateCommand.Parameters.AddWithValue("@patient_religion", txtReligion.Text)
								updateCommand.Parameters.AddWithValue("@patient_educational_attainment", txtEducational.Text)
								updateCommand.Parameters.AddWithValue("@patient_family_member_name", txtPatientFamilyMemberName.Text)
								updateCommand.Parameters.AddWithValue("@patient_relation_to_client", txtRelationtoClient.Text)
								updateCommand.Parameters.AddWithValue("@patient_family_address", txtFamilyAddress.Text)
								updateCommand.Parameters.AddWithValue("@patient_description", txtPatientDescription.Text)
								Dim input As String = txtPatientEmergencyNumber.Text
								Dim regex As New Regex("^[0-9]{11}$")
								If regex.IsMatch(input) Or String.IsNullOrEmpty(input) Then
									updateCommand.Parameters.AddWithValue("@patient_emergency_number", input)
								Else
									MessageBox.Show("Input 11 digits starting from 0-9.")
									Return
								End If

								updateCommand.Parameters.AddWithValue("@patient_updated_at", DateTime.Now)
								updateCommand.Parameters.AddWithValue("@patient_id", patientId)

								updateCommand.ExecuteNonQuery()

								MessageBox.Show("Data updated successfully.")
								LoadDataPatients()
								comboload()
								ClearTextboxesPatients()
								DisabledTextboxesPatients()
								DisplayTotalOrphans(lblTotalOrphans)
								DisabledButtonsPatients()
								btnPatientsNew.Enabled = True
							Else
								MessageBox.Show("Please select a patient status.")
							End If
						End Using
					Catch ex As Exception
						MessageBox.Show("An error occurred while updating the data: " + ex.Message)
					End Try
				End If
			Else
				MessageBox.Show("Please select a valid row to update.")
			End If
		Else
			MessageBox.Show("Please select a row to update.")
		End If
	End Sub


	Private Sub btnPatientsDelete_Click(sender As Object, e As EventArgs) Handles btnPatientsDelete.Click
		' Get the selected row ID from the DataGridView
		If Guna2DataGridViewOrphans.SelectedRows.Count = 0 Then
			MessageBox.Show("Please select a row to delete.")
			Return
		End If
		Dim rowId As Integer = CInt(Guna2DataGridViewOrphans.SelectedRows(0).Cells("PatientId").Value)

		' Confirm deletion with the user
		Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo)
		If result = DialogResult.Yes Then
			Try
				Using connection As New MySqlConnection(connectionString)
					connection.Open()

					Dim deleteCommand As New MySqlCommand("DELETE FROM patients WHERE patient_id = @id", connection)
					deleteCommand.Parameters.AddWithValue("@id", rowId)

					deleteCommand.ExecuteNonQuery()
				End Using
				LoadDataPatients()
				MessageBox.Show("Record deleted successfully.")
				ClearTextboxesPatients()
				DisabledTextboxesPatients()
				comboload()
				DisplayTotalOrphans(lblTotalOrphans)
				DisabledButtonsPatients()
				btnPatientsSave.Enabled = False
				btnPatientsNew.Enabled = True
			Catch ex As Exception
				MessageBox.Show("An error occurred while deleting the row: " & ex.Message)
			End Try
		End If
	End Sub

	Private Sub btnPatientsSearch_Click(sender As Object, e As EventArgs) Handles btnPatientsSearch.Click
		Dim searchText As String = txtPatientsSearch.Text

		' Query the database or data source to get the search results
		' For example, if you're using a MySQL database:

		Dim sql As String = $"SELECT patient_id AS PatientId, patient_date_of_admission AS DateofAdmission, patient_first_name AS FirstName, patient_middle_name AS MiddleName, patient_last_name AS LastName, patient_orphan_address AS Address, patient_birth_date AS BirthDate, patient_place_of_birth AS PlaceOfBirth ,patient_status AS Status, patient_religion AS religion, patient_educational_attainment AS EducationalAttainment, patient_family_member_name AS FamilyMemberName, patient_relation_to_client AS RelationtoClient, patient_family_address AS FamilyAddress ,patient_emergency_number AS EmergencyNumber, patient_description AS Description, patient_created_at AS CreatedAt, patient_updated_at AS UpdatedAt FROM patients WHERE patient_first_name LIKE @searchText OR patient_last_name LIKE @searchText OR patient_middle_name LIKE @searchText OR patient_family_member_name LIKE @searchText"

		Dim searchResults As New DataTable()
		Using connection As New MySqlConnection(connectionString)

			Using command As New MySqlCommand(sql, connection)
				command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
				connection.Open()
				searchResults.Load(command.ExecuteReader())
			End Using
		End Using

		' Bind the results to the DataGridView
		Guna2DataGridViewOrphans.DataSource = searchResults

		' Check if any rows were found
		If searchResults.Rows.Count = 0 Then
			MessageBox.Show("No data found.")
		End If
	End Sub



	Private Sub txtPatientsSearch_TextChanged(sender As Object, e As EventArgs) Handles txtPatientsSearch.TextChanged
		If String.IsNullOrEmpty(txtPatientsSearch.Text.Trim()) Then
			LoadDataPatients()
			btnPatientsSearch.Enabled = False
		Else
			btnPatientsSearch.Enabled = True
		End If
	End Sub

	Private Sub TextBox31_TextChanged(sender As Object, e As EventArgs)

	End Sub

	Private Sub Label63_Click(sender As Object, e As EventArgs) Handles Label63.Click

	End Sub





	'-------------------------------------------------End Orphans----------------------------------------------------------------
	'-------------------------------------------------Start Medical History----------------------------------------------------------------

	Private Sub ClearTextboxesMedicalHistory()
		cmbMedicalHistoryMedicalName.Focus()
		txtMedicalHistoryMedicalID.Clear()
		cmbMedicalHistoryMedicalName.Text = ""
		txtMedicalHistoryMedicalDiagnostic.Clear()
		txtMedicalIntake.Clear()
		txtMedicalHistoryMedicalWeight.Clear()
		txtMedicalHistoryMedicalHeight.Clear()
		txtMedicalHistoryMedicalDoctorsName.Clear()
		txtMedicalBP.Clear()
		txtMedicalHistoryMedicalCreatedAt.Clear()
		txtMedicalHistoryMedicalUpdatedAt.Clear()
		txtTemperature.Clear()


	End Sub
	Private Sub DisabledTextboxesMedicalHistory()
		cmbMedicalHistoryMedicalName.Enabled = False
		txtMedicalHistoryMedicalDiagnostic.Enabled = False
		txtMedicalIntake.Enabled = False
		txtMedicalHistoryMedicalWeight.Enabled = False
		txtMedicalBP.Enabled = False
		txtMedicalHistoryMedicalHeight.Enabled = False
		txtMedicalHistoryMedicalDoctorsName.Enabled = False
		DtpMedicalDateRecorded.Enabled = False
		txtTemperature.Enabled = False

	End Sub

	Private Sub EnabledTextboxesMedicalHistory()

		cmbMedicalHistoryMedicalName.Enabled = True
		txtMedicalHistoryMedicalDiagnostic.Enabled = True
		txtMedicalIntake.Enabled = True
		txtMedicalBP.Enabled = True
		txtMedicalHistoryMedicalWeight.Enabled = True
		txtMedicalHistoryMedicalHeight.Enabled = True
		txtMedicalHistoryMedicalDoctorsName.Enabled = True
		DtpMedicalDateRecorded.Enabled = True
		txtTemperature.Enabled = True
	End Sub

	Private Sub DisabledButtonsMedicalHistory()
		btnMedicalSave.Enabled = False
		btnMedicalUpdate.Enabled = False
		btnMedicalDelete.Enabled = False
		btnMedicalCancel.Enabled = False
	End Sub

	Private Sub Guna2DataGridViewMedicalHistory_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridViewMedicalHistory.CellClick
		EnabledTextboxesMedicalHistory()
		btnMedicalNew.Enabled = False
		btnMedicalUpdate.Enabled = True
		btnMedicalDelete.Enabled = True
		btnMedicalCancel.Enabled = True

		' Get the selected row
		Dim selectedRow As DataGridViewRow = Nothing
		If e.RowIndex >= 0 AndAlso e.RowIndex < Guna2DataGridViewMedicalHistory.Rows.Count Then
			selectedRow = Guna2DataGridViewMedicalHistory.Rows(e.RowIndex)
		End If

		' Display the values in the text boxes if the row is not null
		If selectedRow IsNot Nothing Then
			txtMedicalHistoryMedicalID.Text = selectedRow.Cells("MedicalId").Value.ToString()
			cmbMedicalHistoryMedicalName.Text = selectedRow.Cells("MedicalName").Value.ToString()
			txtMedicalHistoryMedicalDiagnostic.Text = selectedRow.Cells("Diagnostic").Value.ToString()
			txtMedicalIntake.Text = selectedRow.Cells("Intake").Value.ToString()
			txtMedicalBP.Text = selectedRow.Cells("BloodPressure").Value.ToString()
			txtTemperature.Text = selectedRow.Cells("Temperature").Value.ToString()
			txtMedicalHistoryMedicalWeight.Text = selectedRow.Cells("Weight").Value.ToString()
			txtMedicalHistoryMedicalHeight.Text = selectedRow.Cells("Height").Value.ToString()
			txtMedicalHistoryMedicalDoctorsName.Text = selectedRow.Cells("DoctorsName").Value.ToString()
			'DtpMedicalDateRecorded.Text = selectedRow.Cells("DateRecorded").Value.ToString()
			txtMedicalHistoryMedicalCreatedAt.Text = selectedRow.Cells("CreatedAt").Value.ToString()
			txtMedicalHistoryMedicalUpdatedAt.Text = selectedRow.Cells("UpdatedAt").Value.ToString()


		End If
	End Sub
	Private Sub LoadDataMedicalHistory()
		Dim searchQuery As String = "SELECT *  FROM medical_history"

		If Not String.IsNullOrWhiteSpace(txtUsersSearch.Text) Then
			searchQuery += $" WHERE user_name LIKE '%{txtUsersSearch.Text}%'"
		End If

		Dim dataAdapter As New MySqlDataAdapter(searchQuery, connection)

		cboUsersType.Items.Clear() ' Clear existing items in cboUsersType
		Guna2DataGridViewMedicalHistory.ScrollBars = ScrollBars.Vertical

		' Set the default cell style for each column
		For Each col As DataGridViewColumn In Guna2DataGridViewMedicalHistory.Columns
			col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
		Next

		' Change the font size of column headers
		Guna2DataGridViewMedicalHistory.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Change the font size of cell values
		Guna2DataGridViewMedicalHistory.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Set the CellBorderStyle property of the GunaDataGridView control
		Guna2DataGridViewMedicalHistory.CellBorderStyle = DataGridViewCellBorderStyle.Single

		' Set the GridColor property of the GunaDataGridView control
		Guna2DataGridViewMedicalHistory.GridColor = Color.Black

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()
				Dim dataAdapterMedicalHistory As New MySqlDataAdapter("SELECT medical_id as 'MedicalId', " &
													  "medical_name AS 'MedicalName', " &
													  "medical_diagnostic AS 'Diagnostic', " &
													  "medical_intake AS 'Intake', " &
													  "medical_weight AS 'Weight', " &
													  "medical_height AS 'Height', " &
													  "medical_blood_pressure AS 'BloodPressure', " &
													  "medical_temperature AS 'Temperature', " &
													  "medical_doctors_name AS 'DoctorsName', " &
													   "medical_date_recorded AS 'DateRecorded', " &
													  "medical_created_at AS 'CreatedAt', " &
													  "medical_updated_at AS 'UpdatedAt' " &
													  "FROM medical_history", connection)


				Dim table As New DataTable()
				dataAdapterMedicalHistory.Fill(table)

				Guna2DataGridViewMedicalHistory.DataSource = table

				'' Hide the UserId column
				'If Guna2DataGridViewMedicalHistory.Columns.Contains("UserId") Then
				'    Guna2DataGridViewMedicalHistory.Columns("UserId").Visible = False
				'End If

				'' Set the size of columns
				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Username") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Username").Width = 150
				'Else
				'    'MessageBox.Show("Column does not exist")
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Password") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Password").Width = 150
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Status") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Status").Width = 100
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Usertype") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Usertype").Width = 100
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("CreatedAt") Then
				'    Guna2DataGridViewMedicalHistory.Columns("CreatedAt").Width = 200
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("UpdatedAt") Then
				'    Guna2DataGridViewMedicalHistory.Columns("UpdatedAt").Width = 200
				'End If


				' Set the cell alignment
				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Password") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Password").Width = 150
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Status") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Status").Width = 100
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Usertype") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Usertype").Width = 100
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("CreatedAt") Then
				'    Guna2DataGridViewMedicalHistory.Columns("CreatedAt").Width = 200
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("UpdatedAt") Then
				'    Guna2DataGridViewMedicalHistory.Columns("UpdatedAt").Width = 200
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Username") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Username").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Password") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Password").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Status") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("Usertype") Then
				'    Guna2DataGridViewMedicalHistory.Columns("Usertype").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("CreatedAt") Then
				'    Guna2DataGridViewMedicalHistory.Columns("CreatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

				'If Guna2DataGridViewMedicalHistory.Columns.Contains("UpdatedAt") Then
				'    Guna2DataGridViewMedicalHistory.Columns("UpdatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

				' Set the cell padding
				'If Guna2DataGridViewMedicalHistory.RowCount > 0 Then
				'    Guna2DataGridViewMedicalHistory.Columns("Username").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
				'    Guna2DataGridViewMedicalHistory.Columns("Password").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
				'    Guna2DataGridViewUsers.Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'    Guna2DataGridViewUsers.Columns("Usertype").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'    Guna2DataGridViewUsers.Columns("CreatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'    Guna2DataGridViewUsers.Columns("UpdatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

				'    Guna2DataGridViewUsers.Columns("Username").DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("Password").DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("Status").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("CreatedAt").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("UpdatedAt").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
				'End If

				' Set the header text
				'If Guna2DataGridViewUsers.Columns.Contains("Username") Then
				'    Guna2DataGridViewUsers.Columns("Username").HeaderText = "Username"
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("Password") Then
				'    Guna2DataGridViewUsers.Columns("Password").HeaderText = "Password"
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("Status") Then
				'    Guna2DataGridViewUsers.Columns("Status").HeaderText = "Status"
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("Usertype") Then
				'    Guna2DataGridViewUsers.Columns("Usertype").HeaderText = "User Type"
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("CreatedAt") Then
				'    Guna2DataGridViewUsers.Columns("CreatedAt").HeaderText = "Created At"
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("UpdatedAt") Then
				'    Guna2DataGridViewUsers.Columns("UpdatedAt").HeaderText = "Updated At"
				'End If

				'Center the header text
				'If Guna2DataGridViewUsers.Columns.Contains("Username") Then
				'    Guna2DataGridViewUsers.Columns("Username").Width = 150
				'    Guna2DataGridViewUsers.Columns("Username").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
				'    Guna2DataGridViewUsers.Columns("Username").DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("Username").HeaderText = "Username"
				'    Guna2DataGridViewUsers.Columns("Username").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("Password") Then
				'    Guna2DataGridViewUsers.Columns("Password").Width = 150
				'    Guna2DataGridViewUsers.Columns("Password").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
				'    Guna2DataGridViewUsers.Columns("Password").DefaultCellStyle.Padding = New Padding(5, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("Password").HeaderText = "Password"
				'    Guna2DataGridViewUsers.Columns("Password").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("Status") Then
				'    Guna2DataGridViewUsers.Columns("Status").Width = 100
				'    Guna2DataGridViewUsers.Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'    Guna2DataGridViewUsers.Columns("Status").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("Status").HeaderText = "Status"
				'    Guna2DataGridViewUsers.Columns("Status").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("Usertype") Then
				'    Guna2DataGridViewUsers.Columns("Usertype").Width = 100
				'    Guna2DataGridViewUsers.Columns("Usertype").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'    Guna2DataGridViewUsers.Columns("Usertype").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("Usertype").HeaderText = "User Type"
				'    Guna2DataGridViewUsers.Columns("Usertype").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("CreatedAt") Then
				'    Guna2DataGridViewUsers.Columns("CreatedAt").Width = 200
				'    Guna2DataGridViewUsers.Columns("CreatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'    Guna2DataGridViewUsers.Columns("CreatedAt").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("CreatedAt").HeaderText = "Created At"
				'    Guna2DataGridViewUsers.Columns("CreatedAt").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

				'If Guna2DataGridViewUsers.Columns.Contains("UpdatedAt") Then
				'    Guna2DataGridViewUsers.Columns("UpdatedAt").Width = 200
				'    Guna2DataGridViewUsers.Columns("UpdatedAt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				'    Guna2DataGridViewUsers.Columns("UpdatedAt").DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
				'    Guna2DataGridViewUsers.Columns("UpdatedAt").HeaderText = "Updated At"
				'    Guna2DataGridViewUsers.Columns("UpdatedAt").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
				'End If

			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while loading the data: " + ex.Message)
		End Try

		' Add the items to the GunaComboBox control using an array
		Dim items() As String = {"Admin", "Staff", "Owner"}
		cboUsersType.Items.AddRange(items)
	End Sub

	Private Sub btnMedicalNew_Click(sender As Object, e As EventArgs) Handles btnMedicalNew.Click
		EnabledTextboxesMedicalHistory()
		ClearTextboxesMedicalHistory()
		cmbMedicalHistoryMedicalName.Focus()
		btnMedicalNew.Enabled = False
		btnMedicalSave.Enabled = True
		btnMedicalDelete.Enabled = False
		btnMedicalCancel.Enabled = True
		Guna2DataGridViewMedicalHistory.Enabled = False
	End Sub

	Private Sub btnMedicalSave_Click(sender As Object, e As EventArgs) Handles btnMedicalSave.Click
		If String.IsNullOrEmpty(cmbMedicalHistoryMedicalName.Text) OrElse String.IsNullOrEmpty(txtMedicalHistoryMedicalDiagnostic.Text) OrElse String.IsNullOrEmpty(txtMedicalIntake.Text) OrElse String.IsNullOrEmpty(txtMedicalHistoryMedicalDoctorsName.Text) OrElse String.IsNullOrEmpty(DtpMedicalDateRecorded.Text) Then
			MessageBox.Show("Please fill in all required fields.")
			Return
		End If

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Check if a record with the same user name already exists
				'Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM medical_history WHERE medical_name = @medical_name", connection)
				'selectCommand.Parameters.AddWithValue("@medical_name", cmbMedicalHistoryMedicalName.Text)

				'Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
				'If count > 0 Then
				'MessageBox.Show("A patient with the same name already exists.")
				'Return
				'End If

				' Insert a new record
				Dim insertcommand As New MySqlCommand("INSERT INTO medical_history (medical_name, medical_diagnostic, medical_intake, medical_blood_pressure,medical_temperature, medical_weight,medical_height,medical_doctors_name,medical_date_recorded,medical_created_at) VALUES (@medical_name, @medical_diagnostic, @medical_intake, @medical_blood_pressure,@medical_temperature, @medical_weight,@medical_height,@medical_doctors_name,@medical_date_recorded,@medical_created_at)", connection)
				insertcommand.Parameters.AddWithValue("@medical_name", cmbMedicalHistoryMedicalName.Text)
				insertcommand.Parameters.AddWithValue("@medical_diagnostic", txtMedicalHistoryMedicalDiagnostic.Text)
				insertcommand.Parameters.AddWithValue("@medical_intake", txtMedicalIntake.Text)
				insertcommand.Parameters.AddWithValue("@medical_blood_pressure", txtMedicalBP.Text)
				insertcommand.Parameters.AddWithValue("@medical_temperature", txtTemperature.Text + " °")
				insertcommand.Parameters.AddWithValue("@medical_weight", txtMedicalHistoryMedicalWeight.Text + " KG")
				insertcommand.Parameters.AddWithValue("@medical_height", txtMedicalHistoryMedicalHeight.Text + " FT")
				insertcommand.Parameters.AddWithValue("@medical_doctors_name", txtMedicalHistoryMedicalDoctorsName.Text)
				insertcommand.Parameters.AddWithValue("@medical_date_recorded", DtpMedicalDateRecorded.Text)

				insertcommand.Parameters.AddWithValue("@medical_created_at", DateTime.Now)

				insertcommand.ExecuteNonQuery()
				btnMedicalSave.Enabled = False
				btnMedicalNew.Enabled = True
				btnMedicalCancel.Enabled = False
				MessageBox.Show("Data saved successfully.")
				LoadDataMedicalHistory()
				ClearTextboxesMedicalHistory()
				DisplayTotalMedicalHistory(lblTotalMedicalHistory)
				DisabledTextboxesMedicalHistory()
				Guna2DataGridViewMedicalHistory.Enabled = True
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while saving the data: " + ex.Message)
		End Try
	End Sub

	Private Sub btnMedicalUpdate_Click(sender As Object, e As EventArgs) Handles btnMedicalUpdate.Click
		If Guna2DataGridViewMedicalHistory.SelectedRows.Count > 0 Then
			Dim selectedRow = Guna2DataGridViewMedicalHistory.SelectedRows(0)
			Dim medicalId = selectedRow.Cells("MedicalId").Value


			' Check if any changes have been made
			If cmbMedicalHistoryMedicalName.Text = selectedRow.Cells("MedicalName").Value AndAlso
		   txtMedicalHistoryMedicalDiagnostic.Text = selectedRow.Cells("Diagnostic").Value AndAlso
		   txtMedicalBP.Text = selectedRow.Cells("BloodPressure").Value AndAlso
		   txtTemperature.Text = selectedRow.Cells("Temperature").Value AndAlso
		   txtMedicalIntake.Text = selectedRow.Cells("Intake").Value AndAlso
		   txtMedicalHistoryMedicalWeight.Text = selectedRow.Cells("Weight").Value AndAlso
		   txtMedicalHistoryMedicalHeight.Text = selectedRow.Cells("Height").Value AndAlso
		   txtMedicalHistoryMedicalDoctorsName.Text = selectedRow.Cells("DoctorsName").Value AndAlso
		   DtpMedicalDateRecorded.Text = selectedRow.Cells("DateRecorded").Value Then
				MessageBox.Show("There are no changes to update.")
				Return
			End If

			' Show confirmation prompt
			If MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo) = DialogResult.Yes Then
				Try
					Using connection As New MySqlConnection(connectionString)
						connection.Open()

						'Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM medical_history WHERE medical_name = @medical_name AND medical_id <> @medical_id", connection)

						'selectCommand.Parameters.AddWithValue("@medical_name", cmbMedicalHistoryMedicalName.Text)
						'selectCommand.Parameters.AddWithValue("@medical_id", medicalId)

						'Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
						'If count > 0 Then
						'MessageBox.Show("A patient with the same name already exists.")
						'Return
						'End If

						' Check if medical name is not empty before updating
						If Not String.IsNullOrEmpty(cmbMedicalHistoryMedicalName.Text) Then
							Dim updateCommand As New MySqlCommand("UPDATE medical_history SET medical_name=@medical_name, medical_diagnostic=@medical_diagnostic, medical_intake=@medical_intake, medical_blood_pressure=@medical_blood_pressure, medical_temperature=@medical_temperature,medical_weight=@medical_weight, medical_height=@medical_height, medical_doctors_name=@medical_doctors_name, medical_date_recorded=@medical_date_recorded, medical_updated_at=@medical_updated_at WHERE medical_id=@medical_id", connection)

							updateCommand.Parameters.AddWithValue("@medical_name", cmbMedicalHistoryMedicalName.Text)
							updateCommand.Parameters.AddWithValue("@medical_diagnostic", txtMedicalHistoryMedicalDiagnostic.Text)
							updateCommand.Parameters.AddWithValue("@medical_intake", txtMedicalIntake.Text)
							updateCommand.Parameters.AddWithValue("@medical_blood_pressure", txtMedicalBP.Text)
							updateCommand.Parameters.AddWithValue("@medical_temperature", txtTemperature.Text)
							updateCommand.Parameters.AddWithValue("@medical_weight", txtMedicalHistoryMedicalWeight.Text)
							updateCommand.Parameters.AddWithValue("@medical_height", txtMedicalHistoryMedicalHeight.Text)
							updateCommand.Parameters.AddWithValue("@medical_doctors_name", txtMedicalHistoryMedicalDoctorsName.Text)
							updateCommand.Parameters.AddWithValue("@medical_date_recorded", DtpMedicalDateRecorded.Text)
							updateCommand.Parameters.AddWithValue("@medical_updated_at", DateTime.Now)
							updateCommand.Parameters.AddWithValue("@medical_id", medicalId)

							updateCommand.ExecuteNonQuery()

							MessageBox.Show("Record updated successfully.")
							LoadDataMedicalHistory()
							ClearTextboxesMedicalHistory()
							DisplayTotalMedicalHistory(lblTotalMedicalHistory)
							DisabledTextboxesMedicalHistory()
							DisabledButtonsMedicalHistory()
							btnMedicalNew.Enabled = True
						Else
							MessageBox.Show("Please enter a medical name.")
						End If
					End Using
				Catch ex As Exception
					MessageBox.Show("An error occurred while updating the record: " + ex.ToString)
				End Try
			End If
		Else
			MessageBox.Show("Please select a record to update.")
		End If
	End Sub


	Private Sub btnMedicalDelete_Click(sender As Object, e As EventArgs) Handles btnMedicalDelete.Click
		If Guna2DataGridViewMedicalHistory.SelectedRows.Count = 0 Then
			MessageBox.Show("Please select a row to delete.")
			Return
		End If
		Dim rowId As Integer = CInt(Guna2DataGridViewMedicalHistory.SelectedRows(0).Cells("medicalId").Value)
		' Confirm deletion with the user
		Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo)
		If result = DialogResult.Yes Then
			Try
				Using connection As New MySqlConnection(connectionString)
					connection.Open()

					Dim deleteCommand As New MySqlCommand("DELETE FROM medical_history WHERE medical_id = @medical_id", connection)
					deleteCommand.Parameters.AddWithValue("@medical_id", rowId)

					deleteCommand.ExecuteNonQuery()
				End Using
				LoadDataMedicalHistory()
				MessageBox.Show("Record deleted successfully.")
				ClearTextboxesMedicalHistory()
				DisabledTextboxesMedicalHistory()
				DisplayTotalMedicalHistory(lblTotalMedicalHistory)
				DisabledButtonsMedicalHistory()
				btnMedicalSave.Enabled = False
				btnMedicalNew.Enabled = True
			Catch ex As Exception
				MessageBox.Show("An error occurred while deleting the row: " & ex.Message)
			End Try
		End If
		'btnDelete.Enabled = False

	End Sub

	Private Sub btnMedicalCancel_Click(sender As Object, e As EventArgs) Handles btnMedicalCancel.Click
		DisabledButtonsMedicalHistory()
		ClearTextboxesMedicalHistory()
		DisabledTextboxesMedicalHistory()
		btnMedicalNew.Enabled = True
		Guna2DataGridViewMedicalHistory.Enabled = True
	End Sub
	Private Sub btnMedicalSearch_Click(sender As Object, e As EventArgs) Handles btnMedicalSearch.Click
		Dim searchText As String = txtMedicalHistorySearch.Text
		Dim sql As String = "SELECT medical_id AS MedicalId, medical_name AS MedicalName, medical_diagnostic AS Diagnostic, medical_intake AS Intake, medical_weight AS Weight, medical_height AS Height, medical_blood_pressure AS BloodPressure, medical_temperature AS Temperature, medical_doctors_name AS DoctorsName, medical_date_recorded AS DateRecorded, medical_created_at AS CreatedAt, medical_updated_at AS UpdatedAt FROM medical_history WHERE medical_name LIKE @searchText OR medical_diagnostic LIKE @searchText OR medical_doctors_name LIKE @searchText OR medical_temperature LIKE @searchText"

		Dim searchResults As New DataTable()
		Using connection As New MySqlConnection(connectionString)
			Using command As New MySqlCommand(sql, connection)
				command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
				connection.Open()
				searchResults.Load(command.ExecuteReader())
			End Using
		End Using

		' Bind the results to the DataGridView
		Guna2DataGridViewMedicalHistory.DataSource = searchResults

		' Check if any rows were found
		If searchResults.Rows.Count = 0 Then
			MessageBox.Show("No data found.")
		End If
	End Sub



	Private Sub txtMedicalHistorySearch_TextChanged(sender As Object, e As EventArgs) Handles txtMedicalHistorySearch.TextChanged
		If String.IsNullOrEmpty(txtMedicalHistorySearch.Text.Trim()) Then
			LoadDataMedicalHistory()
			btnMedicalSearch.Enabled = False
		Else
			btnMedicalSearch.Enabled = True
		End If
	End Sub




	'-------------------------------------------------End Medical History----------------------------------------------------------------


	'-------------------------------------------------Start Donations-----------------------------------------------------------

	Private Sub ClearTextboxesDonations()
		txtSponsorName.Clear()
		txtClothes.Visible = False
		txtFood.Visible = False
		txtEquipment.Visible = False
		LabelClothes.Visible = False
		LabelFood.Visible = False
		LabelEquipment.Visible = False
		LabelQuantity.Visible = False
		LabelCash.Visible = False
		txtCashAmount.Visible = False
		txtQuantity.Visible = False
		txtDonationID.Clear()
		txtSponsorAddress.Clear()
		txtDonationPhoneNumber.Clear()
		txtEquipment.Clear()
		cboSponsorGender.SelectedItem = Nothing
		cboDonationType.SelectedItem = Nothing
		cboinventorytype.SelectedItem = Nothing
		txtQuantity.Clear()
		txtCashAmount.Clear()
		txtDonationCreatedAt.Clear()
		txtDonationUpdatedAt.Clear()
	End Sub
	Private Sub DisabledTextboxesDonations()


		txtSponsorName.Enabled = False
		cboSponsorGender.Enabled = False
		txtSponsorAddress.Enabled = False
		txtDonationPhoneNumber.Enabled = False
		DtpDonationDateDonated.Enabled = False
		cboDonationType.Enabled = False
		cboinventorytype.Enabled = False
		txtQuantity.Enabled = False
		txtCashAmount.Enabled = False
		'txtDonationCreatedAt.Enabled = False
		'txtDonationUpdatedAt.Enabled = False
	End Sub
	Private Sub EnabledTextboxesDonations()
		txtSponsorName.Enabled = True
		cboSponsorGender.Enabled = True
		txtSponsorAddress.Enabled = True
		txtDonationPhoneNumber.Enabled = True
		DtpDonationDateDonated.Enabled = True
		cboDonationType.Enabled = True
		cboinventorytype.Enabled = True
		txtQuantity.Enabled = True
		txtCashAmount.Enabled = True
		'txtDonationCreatedAt.Enabled = true
		'txtDonationUpdatedAt.Enabled = true
	End Sub
	Private Sub DisabledButtonsDonations()

		'btnNew.Enabled = False
		btnDonationsSave.Enabled = False
		btnDonationsUpdate.Enabled = False
		btnDonationsDelete.Enabled = False
		btnDonationsCancel.Enabled = False
	End Sub

	Private Sub Guna2DataGridViewDonations_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridViewDonations.CellClick
		EnabledTextboxesDonations()
		btnDonationsNew.Enabled = False
		btnDonationsUpdate.Enabled = True
		btnDonationsDelete.Enabled = True
		btnDonationsCancel.Enabled = True

		' Get the selected row
		Dim selectedRow As DataGridViewRow = Nothing
		If e.RowIndex >= 0 AndAlso e.RowIndex < Guna2DataGridViewDonations.Rows.Count Then
			selectedRow = Guna2DataGridViewDonations.Rows(e.RowIndex)
		End If

		' Display the values in the text boxes if the row is not null
		If selectedRow IsNot Nothing Then
			txtDonationID.Text = selectedRow.Cells("DonationId").Value.ToString()
			txtSponsorName.Text = selectedRow.Cells("SponsorName").Value.ToString()
			cboSponsorGender.Text = selectedRow.Cells("Gender").Value.ToString()
			txtSponsorAddress.Text = selectedRow.Cells("SponsorAddress").Value.ToString()
			txtDonationPhoneNumber.Text = selectedRow.Cells("PhoneNumber").Value.ToString()
			DtpDonationDateDonated.Text = selectedRow.Cells("DateDonated").Value.ToString()
			txtCashAmount.Text = selectedRow.Cells("CashAmount").Value.ToString()
			txtQuantity.Text = selectedRow.Cells("Quantity").Value.ToString()
			cboinventorytype.Text = selectedRow.Cells("InventoryType").Value.ToString()
			txtDonationCreatedAt.Text = selectedRow.Cells("CreatedAt").Value.ToString()
			txtDonationUpdatedAt.Text = selectedRow.Cells("UpdatedAt").Value.ToString()
			txtEquipment.Text = selectedRow.Cells("InventoryType").Value.ToString()


			' Display the value in the combo box
			cboDonationType.Text = selectedRow.Cells("DonationType").Value.ToString()

		End If
	End Sub
	Private Sub LoadDataDonations()
		Dim searchQuery As String = "SELECT *  FROM donations"

		If Not String.IsNullOrWhiteSpace(txtDonationsSearch.Text) Then
			searchQuery += $" WHERE donation_name LIKE '%{txtDonationsSearch.Text}%'"
		End If

		Dim dataAdapter As New MySqlDataAdapter(searchQuery, connection)
		cboSponsorGender.Items.Clear()
		cboDonationType.Items.Clear() ' Clear existing items in cboUsersType
		Guna2DataGridViewDonations.ScrollBars = ScrollBars.Vertical

		' Set the default cell style for each column
		For Each col As DataGridViewColumn In Guna2DataGridViewDonations.Columns
			col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
		Next

		' Change the font size of column headers
		Guna2DataGridViewDonations.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Change the font size of cell values
		Guna2DataGridViewDonations.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Set the CellBorderStyle property of the GunaDataGridView control
		Guna2DataGridViewDonations.CellBorderStyle = DataGridViewCellBorderStyle.Single

		' Set the GridColor property of the GunaDataGridView control
		Guna2DataGridViewDonations.GridColor = Color.Black

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()
				Dim dataAdapterDonations As New MySqlDataAdapter("SELECT donation_id as 'DonationId', " &
																 "sponsor_name AS 'SponsorName', " &
																 "sponsor_gender AS 'Gender', " &
																 "sponsor_address AS 'SponsorAddress', " &
																 "donation_phone_number AS 'PhoneNumber', " &
																 "donation_date_donated AS 'DateDonated', " &
																 "donation_type AS 'DonationType', " &
																 "inventory_type AS 'InventoryType', " &
																 "donation_quantity AS 'Quantity', " &
																 "cash_amount AS 'CashAmount', " &
																 "donation_created_at AS 'CreatedAt', " &
																 "donation_updated_at AS 'UpdatedAt' " &
																 "FROM donations", connection)


				Dim table As New DataTable()
				dataAdapterDonations.Fill(table)

				Guna2DataGridViewDonations.DataSource = table
				' Calculate the total cash amount and add it to a new row in the DataGridView
				Dim totalCashAmount As Decimal = 0

				For Each row As DataGridViewRow In Guna2DataGridViewDonations.Rows
					If Not row.IsNewRow Then
						Dim cashAmount As Decimal = 0
						Decimal.TryParse(row.Cells("CashAmount").Value.ToString(), cashAmount)
						totalCashAmount += cashAmount
					End If
				Next

				If totalCashAmount <> 0 Then
					Dim row2 As DataRow = table.NewRow()
					row2("CashAmount") = totalCashAmount
					row2("Quantity") = "Total: "
					table.Rows.Add(row2)
				End If

			End Using

		Catch ex As Exception
			MessageBox.Show("An error occurred while loading the data: " + ex.ToString)
		End Try

		' Add the items to the GunaComboBox control using an array
		Dim items() As String = {"Cash", "Food", "Clothes", "Equipment"}
		cboDonationType.Items.AddRange(items)
		Dim items2() As String = {"Male", "Female", "Other"}
		cboSponsorGender.Items.AddRange(items2)
	End Sub

	Private Sub btnDonationsNew_Click(sender As Object, e As EventArgs) Handles btnDonationsNew.Click
		EnabledTextboxesDonations()
		ClearTextboxesDonations()
		txtSponsorName.Focus()
		btnDonationsNew.Enabled = False
		btnDonationsSave.Enabled = True
		btnDonationsDelete.Enabled = False
		btnDonationsCancel.Enabled = True
		Guna2DataGridViewDonations.Enabled = False
	End Sub
	Private Sub btnDonationsSave_Click(sender As Object, e As EventArgs) Handles btnDonationsSave.Click
		If String.IsNullOrEmpty(txtSponsorName.Text) OrElse String.IsNullOrEmpty(cboDonationType.Text) Then
			MessageBox.Show("Please fill in all required fields.")
			Return
		End If

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Check if a record with the same donation name already exists
				'Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM donations WHERE sponsor_name = @sponsor_name", connection)
				'selectCommand.Parameters.AddWithValue("@sponsor_name", txtSponsorName.Text)

				'Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
				'If count > 0 Then
				'MessageBox.Show("A sponsor with the same name already exists.")
				'Return
				'End If

				Dim inventoryType As String = ""
				Select Case cboDonationType.SelectedItem.ToString()
					Case "Cash"
						inventoryType = "N/A"
						txtQuantity.Text = ""
					Case "Food"
						inventoryType = txtFood.Text
						txtCashAmount.Text = ""
					Case "Equipment"
						inventoryType = txtEquipment.Text
						txtCashAmount.Text = ""

					Case "Clothes"
						inventoryType = txtClothes.Text
						txtCashAmount.Text = ""
				End Select
				' Insert a new record
				Dim insertcommand As New MySqlCommand("INSERT INTO donations (sponsor_name, sponsor_gender, sponsor_address, donation_phone_number, donation_date_donated, donation_type, inventory_type, donation_quantity, cash_amount, donation_created_at) VALUES (@sponsor_name, @sponsor_gender, @sponsor_address, @donation_phone_number, @donation_date_donated, @donation_type, @inventory_type, @donation_quantity, @cash_amount, @donation_created_at)", connection)
				Dim input As String = txtDonationPhoneNumber.Text
				Dim regex As New Regex("^[0-9]{11}$")
				insertcommand.Parameters.AddWithValue("@sponsor_name", txtSponsorName.Text)
				insertcommand.Parameters.AddWithValue("@sponsor_gender", cboSponsorGender.Text)
				insertcommand.Parameters.AddWithValue("@sponsor_address", txtSponsorAddress.Text)

				If regex.IsMatch(input) Or String.IsNullOrEmpty(input) Then
					insertcommand.Parameters.AddWithValue("@donation_phone_number", input)
				Else
					MessageBox.Show("Input 11 digits starting from 0-9.")
					Return
				End If
				insertcommand.Parameters.AddWithValue("@donation_date_donated", DtpDonationDateDonated.Text)
				insertcommand.Parameters.AddWithValue("@donation_type", cboDonationType.Text)
				insertcommand.Parameters.AddWithValue("@inventory_type", inventoryType)
				If String.IsNullOrEmpty(txtQuantity.Text) Then
					insertcommand.Parameters.AddWithValue("@donation_quantity", 0)
				Else
					insertcommand.Parameters.AddWithValue("@donation_quantity", txtQuantity.Text)
				End If
				insertcommand.Parameters.AddWithValue("@cash_amount", txtCashAmount.Text)
				insertcommand.Parameters.AddWithValue("@donation_created_at", DateTime.Now)


				insertcommand.ExecuteNonQuery()
				btnDonationsSave.Enabled = False
				btnDonationsNew.Enabled = True
				btnDonationsCancel.Enabled = False
				MessageBox.Show("Data saved successfully.")
				LoadDataDonations()
				DisplayTotalDonations(lblDonationSponsors)
				ClearTextboxesDonations()
				DisabledTextboxesDonations()
				Guna2DataGridViewDonations.Enabled = True
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while saving the data: " + ex.ToString())
		End Try
	End Sub


	Private Sub btnDonationsUpdate_Click(sender As Object, e As EventArgs) Handles btnDonationsUpdate.Click
		If Guna2DataGridViewDonations.SelectedRows.Count > 0 Then
			Dim selectedRow = Guna2DataGridViewDonations.SelectedRows(0)
			Dim donationId = selectedRow.Cells("DonationId").Value
			Dim inventoryType As String = ""
			Select Case cboDonationType.SelectedItem.ToString()
				Case "Cash"
					inventoryType = "N/A"
					txtQuantity.Text = ""
				Case "Food"
					inventoryType = txtFood.Text
					txtCashAmount.Text = ""
				Case "Equipment"
					inventoryType = txtEquipment.Text
					txtCashAmount.Text = ""

				Case "Clothes"
					inventoryType = txtClothes.Text
					txtCashAmount.Text = ""
			End Select

			' Check if any changes have been made
			If (Not IsDBNull(selectedRow.Cells("SponsorName").Value) AndAlso txtSponsorName.Text = selectedRow.Cells("SponsorName").Value) AndAlso
	cboSponsorGender.Text = selectedRow.Cells("Gender").Value AndAlso
	(Not IsDBNull(selectedRow.Cells("SponsorAddress").Value) AndAlso txtSponsorAddress.Text = selectedRow.Cells("sponsorAddress").Value) AndAlso
	(Not IsDBNull(selectedRow.Cells("PhoneNumber").Value) AndAlso txtDonationPhoneNumber.Text = selectedRow.Cells("PhoneNumber").Value) AndAlso
	(Not IsDBNull(selectedRow.Cells("DateDonated").Value) AndAlso DtpDonationDateDonated.Text = selectedRow.Cells("DateDonated").Value) AndAlso
	cboDonationType.Text = selectedRow.Cells("DonationType").Value AndAlso
	(Not IsDBNull(selectedRow.Cells("InventoryType").Value) AndAlso cboDonationType.Text = selectedRow.Cells("InventoryType").Value) AndAlso
	(Not IsDBNull(selectedRow.Cells("Quantity").Value) AndAlso txtQuantity.Text = selectedRow.Cells("Quantity").Value) AndAlso
	(Not IsDBNull(selectedRow.Cells("CashAmount").Value) AndAlso txtCashAmount.Text = selectedRow.Cells("CashAmount").Value) Then
				MessageBox.Show("There are no changes to update.")
				Return
			End If


			' Show confirmation prompt
			If MessageBox.Show("Are you sure you want to update this donation?", "Confirm Update", MessageBoxButtons.YesNo) = DialogResult.Yes Then
				Try
					Using connection As New MySqlConnection(connectionString)
						connection.Open()

						' Check if donation type is not empty before updating
						If Not String.IsNullOrEmpty(cboDonationType.Text) Then
							Dim updateCommand As New MySqlCommand("UPDATE donations SET sponsor_name=@sponsor_name, sponsor_gender=@sponsor_gender, sponsor_address=@sponsor_address, donation_phone_number=@donation_phone_number, donation_date_donated=@donation_date_donated,donation_quantity=@donation_quantity, donation_type=@donation_type, inventory_type=@inventory_type, cash_amount=@cash_amount, donation_updated_at=@donation_updated_at WHERE donation_id=@donation_id", connection)

							updateCommand.Parameters.AddWithValue("@sponsor_name", txtSponsorName.Text)
							updateCommand.Parameters.AddWithValue("@sponsor_gender", cboSponsorGender.Text)
							updateCommand.Parameters.AddWithValue("@sponsor_address", txtSponsorAddress.Text)
							Dim input As String = txtDonationPhoneNumber.Text
							Dim regex As New Regex("^[0-9]{11}$")
							If regex.IsMatch(input) Or String.IsNullOrEmpty(input) Then
								updateCommand.Parameters.AddWithValue("@donation_phone_number", input)
							Else
								MessageBox.Show("Input 11 digits starting from 0-9.")
								Return
							End If
							updateCommand.Parameters.AddWithValue("@donation_date_donated", DtpDonationDateDonated.Text)
							updateCommand.Parameters.AddWithValue("@donation_type", cboDonationType.Text)
							updateCommand.Parameters.AddWithValue("@inventory_type", inventoryType)


							' Use TryParse method to handle DBNull error for CashAmount parameter
							Dim cashAmount As Decimal
							If Decimal.TryParse(txtCashAmount.Text, cashAmount) Then
								updateCommand.Parameters.AddWithValue("@cash_amount", cashAmount)
							Else
								updateCommand.Parameters.AddWithValue("@cash_amount", 0)
							End If

							' Use TryParse method to handle DBNull error for Quantity parameter
							Dim Quantity As Decimal
							If Decimal.TryParse(txtQuantity.Text, Quantity) Then
								updateCommand.Parameters.AddWithValue("@donation_quantity", Quantity)
							Else
								updateCommand.Parameters.AddWithValue("@donation_quantity", 0)
							End If



							updateCommand.Parameters.AddWithValue("@donation_updated_at", DateTime.Now)
							updateCommand.Parameters.AddWithValue("@donation_id", donationId)

							updateCommand.ExecuteNonQuery()

							MessageBox.Show("Data updated successfully.")
							LoadDataDonations()
							ClearTextboxesDonations()
							DisplayTotalDonations(lblDonationSponsors)
							DisabledTextboxesDonations()
							DisabledButtonsDonations()
							btnDonationsNew.Enabled = True
						Else
							MessageBox.Show("Please enter a donation type.")
						End If
					End Using
				Catch ex As Exception
					MessageBox.Show("An error occurred while updating the data: " + ex.Message)
				End Try
			End If
		Else
			MessageBox.Show("Please select a row to update.")
		End If
	End Sub



	Private Sub btnDonationsDelete_Click(sender As Object, e As EventArgs) Handles btnDonationsDelete.Click
		' Get the selected row ID from the DataGridView
		If Guna2DataGridViewDonations.SelectedRows.Count = 0 Then
			MessageBox.Show("Please select a row to delete.")
			Return
		End If
		Dim rowId As Integer = CInt(Guna2DataGridViewDonations.SelectedRows(0).Cells("DonationId").Value)

		' Confirm deletion with the user
		Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo)
		If result = DialogResult.Yes Then
			Try
				Using connection As New MySqlConnection(connectionString)
					connection.Open()

					Dim deleteCommand As New MySqlCommand("DELETE FROM donations WHERE donation_id = @id", connection)
					deleteCommand.Parameters.AddWithValue("@id", rowId)

					deleteCommand.ExecuteNonQuery()
				End Using
				LoadDataDonations()
				MessageBox.Show("Record deleted successfully.")
				ClearTextboxesDonations()
				DisabledTextboxesDonations()
				DisabledButtonsDonations()
				DisplayTotalDonations(lblDonationSponsors)
				btnDonationsSave.Enabled = False
				btnDonationsNew.Enabled = True
			Catch ex As Exception
				MessageBox.Show("An error occurred while deleting the row: " & ex.Message)
			End Try
		End If
	End Sub

	Private Sub btnDonationsCancel_Click(sender As Object, e As EventArgs) Handles btnDonationsCancel.Click
		ClearTextboxesDonations()
		DisabledTextboxesDonations()
		DisabledButtonsDonations()
		btnDonationsNew.Enabled = True
		Guna2DataGridViewDonations.Enabled = True
	End Sub
	Private Sub btnDonationsSearch_Click(sender As Object, e As EventArgs) Handles btnDonationsSearch.Click
		Dim searchText As String = txtDonationsSearch.Text

		' Query the database or data source to get the search results
		' For example, if you're using a MySQL database:


		Dim sql As String = $"SELECT donation_id AS DonationId, sponsor_name AS SponsorName, sponsor_gender As Gender, sponsor_address As SponsorAddress, donation_phone_number As PhoneNumber, donation_date_donated As DateDonated, donation_type As DonationType, inventory_type As InventoryType, donation_quantity As Quantity, cash_amount As CashAmount, donation_created_at As CreatedAt, donation_updated_at AS UpdatedAt FROM donations WHERE sponsor_name LIKE @searchText OR sponsor_address LIKE @searchText OR donation_type LIKE @searchText OR inventory_type LIKE @searchText"
		Dim searchResults As New DataTable()
		Using connection As New MySqlConnection(connectionString)
			Using command As New MySqlCommand(sql, connection)
				command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
				connection.Open()
				searchResults.Load(command.ExecuteReader())
			End Using
		End Using

		' Bind the results to the DataGridView
		Guna2DataGridViewDonations.DataSource = searchResults

		' Check if any rows were found
		If searchResults.Rows.Count = 0 Then
			MessageBox.Show("No data found.")
		End If
	End Sub





	Private Sub txtDonationsSearch_TextChanged(sender As Object, e As EventArgs) Handles txtDonationsSearch.TextChanged
		If String.IsNullOrEmpty(txtDonationsSearch.Text.Trim()) Then
			LoadDataDonations()
			btnDonationsSearch.Enabled = False
		Else
			btnDonationsSearch.Enabled = True
		End If
	End Sub








	'-------------------------------------------------End Donations-----------------------------------------------------------

	'-------------------------------------------------Start Employees-----------------------------------------------------------
	Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
		' Open a file dialog to select an image file
		Dim fileDialog As New OpenFileDialog()
		fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp"
		If fileDialog.ShowDialog() = DialogResult.OK Then
			' Load the selected image file into the picture box
			UploadPicture.Image = Image.FromFile(fileDialog.FileName)
		End If
	End Sub



	Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)

	End Sub
	Private previouslySelectedButton As Button ' declare a variable to keep track of the previously selected button
	Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
		TabControl1.SelectedIndex = 0
		HighlightSelectedButton(btnDashboard, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
		Timer2.Start()
		Timer4.Start()
	End Sub
	Private Sub btnusers_click(sender As Object, e As EventArgs) Handles btnUsers.Click
		TabControl1.SelectedIndex = 1
		HighlightSelectedButton(btnUsers, Color.FromArgb(128, 128, 255)) ' set the rgb value for the selected button
		Timer2.Start()
		Timer4.Start()
	End Sub

	Private Sub btnPatientDetails_Click(sender As Object, e As EventArgs) Handles btnPatientDetails.Click
		TabControl1.SelectedIndex = 2
		HighlightSelectedButton(btnPatientDetails, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button

	End Sub

	Private Sub btnMedicalHistory_Click(sender As Object, e As EventArgs) Handles btnMedicalHistory.Click
		TabControl1.SelectedIndex = 3
		HighlightSelectedButton(btnMedicalHistory, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
	End Sub

	Private Sub btnDonation_Click(sender As Object, e As EventArgs) Handles btnDonation.Click
		TabControl1.SelectedIndex = 4
		HighlightSelectedButton(btnDonation, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
		Timer2.Start()
		Timer4.Start()
	End Sub

	Private Sub btnSuppliesOfMedicine_Click(sender As Object, e As EventArgs) Handles btnSuppliesOfMedicine.Click
		TabControl1.SelectedIndex = 5
		HighlightSelectedButton(btnSuppliesOfMedicine, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
	End Sub

	Private Sub btnStaffRecord_Click(sender As Object, e As EventArgs) Handles btnStaffRecord.Click
		TabControl1.SelectedIndex = 6
		HighlightSelectedButton(btnStaffRecord, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
	End Sub

	Private Sub btnPayroll_Click(sender As Object, e As EventArgs) Handles btnPayroll.Click
		TabControl1.SelectedIndex = 7
		HighlightSelectedButton(btnPayroll, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
	End Sub

	Private Sub btnDashBoardChangePassword_Click(sender As Object, e As EventArgs) Handles btnDashBoardChangePassword.Click
		TabControl1.SelectedIndex = 8
		HighlightSelectedButton(btnDashBoardChangePassword, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
	End Sub

	Private Sub HighlightSelectedButton(selectedButton As Button, selectedColor As Color)
		If previouslySelectedButton IsNot Nothing Then
			'previouslySelectedButton.BackColor = Color.PaleVioletRed ' change the background color of the previously selected button back to the default color
			previouslySelectedButton.BackColor = Color.FromArgb(192, 192, 255) ' change the background color of the previously selected button back to the default color
		End If
		selectedButton.BackColor = selectedColor ' highlight the selected button with a custom RGB background color
		previouslySelectedButton = selectedButton ' update the previously selected button
	End Sub

	Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
		Form2.Show()
		Me.Hide()


	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		If isCollapsed Then
			btnOrphanInfo.Image = My.Resources.icons8_sort_right_15__1_
			DropPanel1.Height += 10
			If DropPanel1.Size = DropPanel1.MaximumSize Then
				Timer1.Stop()
				isCollapsed = False
			End If
		Else
			btnOrphanInfo.Image = My.Resources.icons8_sort_right_15
			DropPanel1.Height -= 10
			If DropPanel1.Size = DropPanel1.MinimumSize Then
				Timer1.Stop()
				isCollapsed = True
			End If
		End If
	End Sub
	Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
		btnOrphanInfo.Image = My.Resources.icons8_sort_right_15
		DropPanel1.Height -= 10
		If DropPanel1.Size = DropPanel1.MinimumSize Then
			Timer2.Stop()
			isCollapsed = True
		End If
	End Sub

	Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
		If isCollapsed2 Then
			btnstaff2.Image = My.Resources.icons8_sort_right_15__1_
			DropPanel4.Height += 10
			If DropPanel4.Size = DropPanel4.MaximumSize Then
				Timer3.Stop()
				isCollapsed2 = False
			End If
		Else
			btnstaff2.Image = My.Resources.icons8_sort_right_15
			DropPanel4.Height -= 10
			If DropPanel4.Size = DropPanel4.MinimumSize Then
				Timer3.Stop()
				isCollapsed2 = True
			End If
		End If
	End Sub
	Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
		btnstaff2.Image = My.Resources.icons8_sort_right_15
		DropPanel4.Height -= 10
		If DropPanel4.Size = DropPanel4.MinimumSize Then
			Timer4.Stop()
			isCollapsed2 = True
		End If
	End Sub

	Private Sub btnOrphanInfo_Click(sender As Object, e As EventArgs) Handles btnOrphanInfo.Click
		HighlightSelectedButton(btnDashboard, Color.FromArgb(192, 192, 255))
		isCollapsed2 = False
		Timer1.Start()
		Timer4.Start()
	End Sub

	'Private Sub btnOrphanInv_Click(sender As Object, e As EventArgs)
	'	isCollapsed = False
	'Timer2.Start()
	'End Sub

	Private Sub btnstaff2_Click(sender As Object, e As EventArgs) Handles btnstaff2.Click
		HighlightSelectedButton(btnUsers, Color.FromArgb(192, 192, 255))
		isCollapsed = False
		Timer3.Start()
		Timer2.Start()
	End Sub

	Private Sub btnMedicalHistory1_Click(sender As Object, e As EventArgs) Handles btnMedicalHistory1.Click
		TabControl1.SelectedIndex = 3
		HighlightSelectedButton(btnMedicalHistory, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button 
		Timer1.Start()
	End Sub

	Private Sub btnOrphanDetails_Click(sender As Object, e As EventArgs) Handles btnOrphanDetails.Click
		TabControl1.SelectedIndex = 2
		HighlightSelectedButton(btnPatientDetails, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
		Timer1.Start()
	End Sub

	Private Sub btnMedicalSupplies_Click(sender As Object, e As EventArgs)
		TabControl1.SelectedIndex = 5
		HighlightSelectedButton(btnSuppliesOfMedicine, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button 

	End Sub

	Private Sub btnDonationSponsors_Click(sender As Object, e As EventArgs)
		TabControl1.SelectedIndex = 4
		HighlightSelectedButton(btnDonation, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button 
	End Sub

	Private Sub btnstaffrecord2_Click(sender As Object, e As EventArgs) Handles btnstaffrecord2.Click
		TabControl1.SelectedIndex = 6
		HighlightSelectedButton(btnStaffRecord, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button 
		Timer3.Start()
	End Sub

	Private Sub btnpayroll2_Click(sender As Object, e As EventArgs) Handles btnpayroll2.Click
		TabControl1.SelectedIndex = 7
		HighlightSelectedButton(btnPayroll, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button 
		Timer3.Start()
	End Sub

	Sub comboload()
		Using connection As New MySqlConnection(connectionString)
			connection.Open()
			Dim adapter As New MySqlDataAdapter("SELECT CONCAT(patient_first_name, ' ', patient_last_name) AS fullname FROM patients", connection)
			Dim searchresult As New DataTable()

			adapter.Fill(searchresult)
			cmbMedicalHistoryMedicalName.DataSource = searchresult
			cmbMedicalHistoryMedicalName.ValueMember = "fullname"
			cmbMedicalHistoryMedicalName.DisplayMember = "fullname"
		End Using
	End Sub

	Sub payrollload()
		Using connection As New MySqlConnection(connectionString)
			connection.Open()
			Dim adapter As New MySqlDataAdapter("SELECT CONCAT(employee_first_name, ' ', employee_last_name) AS fullname FROM employee", connection)
			Dim searchresult As New DataTable()

			adapter.Fill(searchresult)
			cmbPayroll.DataSource = searchresult
			cmbPayroll.ValueMember = "fullname"
			cmbPayroll.DisplayMember = "fullname"
		End Using
	End Sub

	'-------------------------------------------------Employee -------------------------------------------
	Private Sub ClearTextboxesEmployees()
		txtEmployeeFirstName.Focus()
		txtEmployeeID.Clear()
		txtEmployeeFirstName.Clear()
		txtEmployeeMiddleName.Clear()
		txtEmployeeLastName.Clear()
		cboEmployeeGender.SelectedItem = Nothing
		txtEmployeeContactNumber.Clear()
		txtEmployeeAddress.Clear()
		txtEmployeeEmployeeDesignation.Clear()
		txtUserCreatedAt.Clear()
		txtUserUpdatedAt.Clear()
		txtEmployeeEducational.Clear()
		txtEmployeeReligion.Clear()
	End Sub

	Private Sub DisabledTextboxesEmployees()

		txtEmployeeFirstName.Enabled = False
		txtEmployeeMiddleName.Enabled = False
		txtEmployeeLastName.Enabled = False
		cboEmployeeGender.Enabled = False
		txtEmployeeContactNumber.Enabled = False
		txtEmployeeAddress.Enabled = False
		txtEmployeeEmployeeDesignation.Enabled = False
		txtEmployeeEducational.Enabled = False
		txtEmployeeReligion.Enabled = False
		cmbEmployeeStatus.Enabled = False
		dtpEmployeeBirthday.Enabled = False

	End Sub
	Private Sub EnabledTextboxesEmployees()

		txtEmployeeFirstName.Enabled = True
		txtEmployeeMiddleName.Enabled = True
		txtEmployeeLastName.Enabled = True
		cboEmployeeGender.Enabled = True
		txtEmployeeContactNumber.Enabled = True
		txtEmployeeAddress.Enabled = True
		txtEmployeeEmployeeDesignation.Enabled = True
		txtEmployeeEducational.Enabled = True
		txtEmployeeReligion.Enabled = True
		cmbEmployeeStatus.Enabled = True
		dtpEmployeeBirthday.Enabled = True

	End Sub
	Private Sub DisabledButtonsEmployees()



		btnEmployeeSave.Enabled = False
		btnEmployeeUpdate.Enabled = False
		btnEmployeeDelete.Enabled = False
		btnEmployeeCancel.Enabled = False
	End Sub


	Private Sub Guna2DataGridViewEmployees_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridViewEmployees.CellClick
		EnabledTextboxesEmployees()
		btnEmployeeNew.Enabled = False
		btnEmployeeUpdate.Enabled = True
		btnEmployeeDelete.Enabled = True
		btnEmployeeCancel.Enabled = True

		' Get the selected row
		Dim selectedRow As DataGridViewRow = Nothing
		If e.RowIndex >= 0 AndAlso e.RowIndex < Guna2DataGridViewEmployees.Rows.Count Then
			selectedRow = Guna2DataGridViewEmployees.Rows(e.RowIndex)
		End If

		' Display the values in the text boxes if the row is not null
		If selectedRow IsNot Nothing Then

			Dim imageData As Byte() = Nothing
			If Not IsDBNull(selectedRow.Cells("EmployeePicture").Value) Then
				imageData = DirectCast(selectedRow.Cells("EmployeePicture").Value, Byte())
			End If

			If imageData IsNot Nothing Then
				Using ms As New MemoryStream(imageData)
					Dim image As Image = New Bitmap(Image.FromStream(ms))
					UploadPicture.Image = image
				End Using
			Else
				UploadPicture.Image = Nothing
			End If

			txtEmployeeID.Text = selectedRow.Cells("EmployeeId").Value.ToString()
			txtEmployeeFirstName.Text = selectedRow.Cells("Firstname").Value.ToString()
			txtEmployeeMiddleName.Text = selectedRow.Cells("Middlename").Value.ToString()
			txtEmployeeLastName.Text = selectedRow.Cells("Lastname").Value.ToString()
			txtEmployeeContactNumber.Text = selectedRow.Cells("ContactNumber").Value.ToString()
			txtEmployeeAddress.Text = selectedRow.Cells("Address").Value.ToString()
			txtEmployeeReligion.Text = selectedRow.Cells("Religion").Value.ToString()
			cmbEmployeeStatus.Text = selectedRow.Cells("Status").Value.ToString()
			txtEmployeeEducational.Text = selectedRow.Cells("Educational").Value.ToString()
			txtEmployeeEmployeeDesignation.Text = selectedRow.Cells("Designation").Value.ToString()
			txtEmployeeCreatedAt.Text = selectedRow.Cells("CreatedAt").Value.ToString()
			txtUserUpdatedAt.Text = selectedRow.Cells("UpdatedAt").Value.ToString()

			' Display the value in the combo box
			cboEmployeeGender.Text = selectedRow.Cells("Gender").Value.ToString()
		End If
	End Sub
	Private Sub LoadDataEmployees()
		Dim searchQuery As String = "SELECT *  FROM employees"

		If Not String.IsNullOrWhiteSpace(txtEmployeeSearch.Text) Then
			searchQuery += $" WHERE employee_first_name LIKE '%{txtEmployeeSearch.Text}%'"
		End If

		Dim dataAdapter As New MySqlDataAdapter(searchQuery, connection)

		cboEmployeeGender.Items.Clear() ' Clear existing items in cboUsersType
		cmbEmployeeStatus.Items.Clear() ' Clear existing items in cboUsersType
		Guna2DataGridViewEmployees.ScrollBars = ScrollBars.Vertical

		' Set the default cell style for each column
		For Each col As DataGridViewColumn In Guna2DataGridViewEmployees.Columns
			col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
		Next

		' Change the font size of column headers
		Guna2DataGridViewEmployees.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Change the font size of cell values
		Guna2DataGridViewEmployees.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Set the CellBorderStyle property of the GunaDataGridView control
		Guna2DataGridViewEmployees.CellBorderStyle = DataGridViewCellBorderStyle.Single

		' Set the GridColor property of the GunaDataGridView control
		Guna2DataGridViewEmployees.GridColor = Color.Black

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()
				Dim dataAdapterDonations As New MySqlDataAdapter("SELECT employee_id as 'EmployeeId', " &
													  "employee_first_name AS 'Firstname', " &
													  "employee_middle_name AS 'Middlename', " &
													  "employee_last_name AS 'Lastname', " &
													  "employee_address AS 'Address', " &
													  "employee_religion AS 'Religion', " &
													  "employee_birthday AS 'Birthday', " &
													  "employee_gender AS 'Gender', " &
													  "employee_status AS 'Status', " &
													  "employee_contact_number AS 'ContactNumber', " &
													  "employee_designation AS 'Designation', " &
													  "employee_educational AS 'Educational', " &
													  "employee_image AS 'EmployeePicture', " &
													  "employee_created_at AS 'CreatedAt', " &
													  "employee_updated_at AS 'UpdatedAt' " &
													  "FROM employee", connection)


				Dim table As New DataTable()
				dataAdapterDonations.Fill(table)

				Guna2DataGridViewEmployees.DataSource = table
				Guna2DataGridViewEmployees.Columns("EmployeePicture").Visible = False

			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while loading the data: " + ex.Message)
		End Try

		' Add the items to the GunaComboBox control using an array
		Dim items() As String = {"Male", "Female", "Other"}
		cboEmployeeGender.Items.AddRange(items)
		Dim items2() As String = {"Single", "Widowed", "Married"}
		cmbEmployeeStatus.Items.AddRange(items2)
	End Sub

	Private Sub btnEmployeeSave_Click_1(sender As Object, e As EventArgs) Handles btnEmployeeSave.Click



		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Check if a record with the same user name already exists
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM employee WHERE employee_fullname = @employee_fullname", connection)
				selectCommand.Parameters.AddWithValue("@employee_fullname", txtEmployeeFirstName.Text + " " + txtEmployeeMiddleName.Text + " " + txtEmployeeLastName.Text)

				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
				If count > 0 Then
					MessageBox.Show("An employee with the same name has already exists.")
					Return
				End If

				' Insert a new record
				Dim insertcommand As New MySqlCommand("INSERT INTO employee(employee_first_name, employee_middle_name, employee_last_name, employee_fullname, employee_image, employee_gender, employee_contact_number, employee_address, employee_designation, employee_religion, employee_educational, employee_status, employee_birthday, employee_created_at) VALUES (@employee_first_name, @employee_middle_name, @employee_last_name, @employee_fullname, @employee_image, @employee_gender,@employee_contact_number,@employee_address,@employee_designation, @employee_religion, @employee_educational, @employee_status, @employee_birthday,@employee_created_at)", connection)
				Dim newImageData As Byte() = Nothing
				Dim existingImageData As Byte() = Nothing

				' Check if a new image has been uploaded
				If UploadPicture.Image IsNot Nothing Then
					' Convert the new image to byte array and assign it to newImageData
					Dim ms As New MemoryStream()
					UploadPicture.Image.Save(ms, ImageFormat.Jpeg)
					newImageData = ms.ToArray()
				ElseIf existingImageData IsNot Nothing Then
					' If no new image has been uploaded, use the existing image data as the default value for newImageData
					newImageData = existingImageData
				End If

				' Add the newImageData parameter to the update command
				insertcommand.Parameters.AddWithValue("@employee_image", If(newImageData IsNot Nothing, newImageData, DBNull.Value))
				insertcommand.Parameters.AddWithValue("@employee_first_name", txtEmployeeFirstName.Text)
				insertcommand.Parameters.AddWithValue("@employee_middle_name", txtEmployeeMiddleName.Text)
				insertcommand.Parameters.AddWithValue("@employee_last_name", txtEmployeeLastName.Text)
				insertcommand.Parameters.AddWithValue("@employee_fullname", txtEmployeeFirstName.Text + " " + txtEmployeeMiddleName.Text + " " + txtEmployeeLastName.Text)
				insertcommand.Parameters.AddWithValue("@employee_gender", cboEmployeeGender.Text)
				insertcommand.Parameters.AddWithValue("@employee_religion", txtEmployeeReligion.Text)
				insertcommand.Parameters.AddWithValue("@employee_educational", txtEmployeeEducational.Text)
				insertcommand.Parameters.AddWithValue("@employee_status", cmbEmployeeStatus.Text)
				insertcommand.Parameters.AddWithValue("@employee_birthday", dtpEmployeeBirthday.Text)
				Dim input As String = txtEmployeeContactNumber.Text
				Dim regex As New Regex("^[0-9]{11}$")
				If regex.IsMatch(input) Or String.IsNullOrEmpty(input) Then
					insertcommand.Parameters.AddWithValue("@employee_contact_number", input)
				Else
					MessageBox.Show("Input 11 digits starting from 0-9.")
					Return
				End If
				insertcommand.Parameters.AddWithValue("@employee_address", txtEmployeeAddress.Text)
				insertcommand.Parameters.AddWithValue("@employee_designation", txtEmployeeEmployeeDesignation.Text)
				insertcommand.Parameters.AddWithValue("@employee_created_at", DateTime.Now)

				insertcommand.ExecuteNonQuery()
				btnEmployeeSave.Enabled = False
				btnEmployeeNew.Enabled = True
				btnEmployeeCancel.Enabled = False
				btnEmployeeUpdate.Enabled = False
				btnEmployeeDelete.Enabled = False
				MessageBox.Show("Data saved successfully.")
				LoadDataEmployees()
				ClearTextboxesEmployees()
				DisabledTextboxesEmployees()
				DisplayTotalEmployee(lblTotalEmployee)
				payrollload()
				UploadPicture.Image = Nothing
				Guna2DataGridViewEmployees.Enabled = True
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while saving the data: " + ex.ToString)
		End Try
	End Sub
	Private currentEmployeeId As Integer
	Private Sub btnEmployeeUpdate_Click(sender As Object, e As EventArgs) Handles btnEmployeeUpdate.Click



		If Guna2DataGridViewEmployees.SelectedRows.Count > 0 Then
			Dim selectedRow = Guna2DataGridViewEmployees.SelectedRows(0)
			Dim employeeId = selectedRow.Cells("EmployeeId").Value

			' Check if any changes have been made
			If (Not IsDBNull(selectedRow.Cells("Firstname").Value) AndAlso txtEmployeeFirstName.Text = selectedRow.Cells("Firstname").Value) AndAlso
				(Not IsDBNull(selectedRow.Cells("Middlename").Value) AndAlso txtEmployeeMiddleName.Text = selectedRow.Cells("Middlename").Value) AndAlso
				(Not IsDBNull(selectedRow.Cells("Lastname").Value) AndAlso txtEmployeeLastName.Text = selectedRow.Cells("Lastname").Value) AndAlso
				cboEmployeeGender.Text = selectedRow.Cells("Gender").Value AndAlso
				(Not IsDBNull(selectedRow.Cells("ContactNumber").Value) AndAlso txtEmployeeContactNumber.Text = selectedRow.Cells("ContactNumber").Value) AndAlso
				(Not IsDBNull(selectedRow.Cells("Address").Value) AndAlso txtEmployeeAddress.Text = selectedRow.Cells("Address").Value) AndAlso
				(Not IsDBNull(selectedRow.Cells("Designation").Value) AndAlso txtEmployeeEmployeeDesignation.Text = selectedRow.Cells("Designation").Value) AndAlso
				(Not IsDBNull(selectedRow.Cells("Religion").Value) AndAlso txtEmployeeReligion.Text = selectedRow.Cells("Religion").Value) AndAlso
				cmbEmployeeStatus.Text = selectedRow.Cells("Status").Value AndAlso
				dtpEmployeeBirthday.Text = selectedRow.Cells("Birthday").Value AndAlso
				(Not IsDBNull(selectedRow.Cells("Educational").Value) AndAlso txtEmployeeEducational.Text = selectedRow.Cells("Educational").Value) Then
				MessageBox.Show("There are no changes to update.")
				Return
			End If
			'If MessageBox.Show("Are you sure you want to update this donation?", "Confirm Update", MessageBoxButtons.YesNo) = DialogResult.Yes Then
			Try
				Using connection As New MySqlConnection(connectionString)
					connection.Open()

					' Check if a record with the same user name already exists
					Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM employee WHERE employee_fullname = @employee_fullname AND employee_id <> @employee_id", connection)
					selectCommand.Parameters.AddWithValue("@employee_fullname", txtEmployeeFirstName.Text + " " + txtEmployeeMiddleName.Text + " " + txtEmployeeLastName.Text)
					selectCommand.Parameters.AddWithValue("@employee_id", employeeId)

					Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
					If count > 0 Then
						MessageBox.Show("An employee with the same name already exists.")
						Return
					End If

					' Update the record

					Dim updateCommand As New MySqlCommand("UPDATE employee SET employee_first_name = @employee_first_name, employee_middle_name = @employee_middle_name, employee_last_name = @employee_last_name, employee_fullname = @employee_fullname, employee_image = @employee_image ,employee_gender = @employee_gender, employee_contact_number = @employee_contact_number, employee_address = @employee_address, employee_designation = @employee_designation, employee_religion = @employee_religion, employee_educational = @employee_educational, employee_status = @employee_status, employee_birthday = @employee_birthday ,employee_updated_at = @employee_updated_at WHERE employee_id = @employee_id", connection)
					updateCommand.Parameters.AddWithValue("@employee_first_name", txtEmployeeFirstName.Text)
					updateCommand.Parameters.AddWithValue("@employee_middle_name", txtEmployeeMiddleName.Text)
					updateCommand.Parameters.AddWithValue("@employee_last_name", txtEmployeeLastName.Text)
					updateCommand.Parameters.AddWithValue("@employee_fullname", txtEmployeeFirstName.Text + " " + txtEmployeeMiddleName.Text + " " + txtEmployeeLastName.Text)

					Dim newImageData As Byte() = Nothing
					Dim existingImageData As Byte() = Nothing

					' Check if a new image has been uploaded
					If UploadPicture.Image IsNot Nothing Then
						' Convert the new image to byte array and assign it to newImageData
						Dim ms As New MemoryStream()
						UploadPicture.Image.Save(ms, ImageFormat.Jpeg)
						newImageData = ms.ToArray()
					ElseIf existingImageData IsNot Nothing Then
						' If no new image has been uploaded, use the existing image data as the default value for newImageData
						newImageData = existingImageData
					End If

					' Add the newImageData parameter to the update command
					updateCommand.Parameters.AddWithValue("@employee_image", If(newImageData IsNot Nothing, newImageData, DBNull.Value))


					updateCommand.Parameters.AddWithValue("@employee_gender", cboEmployeeGender.Text)
					updateCommand.Parameters.AddWithValue("@employee_religion", txtEmployeeReligion.Text)
					updateCommand.Parameters.AddWithValue("@employee_educational", txtEmployeeEducational.Text)
					updateCommand.Parameters.AddWithValue("@employee_status", cmbEmployeeStatus.Text)
					updateCommand.Parameters.AddWithValue("@employee_birthday", dtpEmployeeBirthday.Text)

					Dim input As String = txtEmployeeContactNumber.Text
					Dim regex As New Regex("^[0-9]{11}$")
					If regex.IsMatch(input) Or String.IsNullOrEmpty(input) Then
						updateCommand.Parameters.AddWithValue("@employee_contact_number", input)
					Else
						MessageBox.Show("Input 11 digits starting from 0-9.")
						Return
					End If

					updateCommand.Parameters.AddWithValue("@employee_address", txtEmployeeAddress.Text)
					updateCommand.Parameters.AddWithValue("@employee_designation", txtEmployeeEmployeeDesignation.Text)
					updateCommand.Parameters.AddWithValue("@employee_id", employeeId)
					updateCommand.Parameters.AddWithValue("@employee_updated_at", DateTime.Now)

					updateCommand.ExecuteNonQuery()

					btnEmployeeSave.Enabled = False
					btnEmployeeNew.Enabled = True
					btnEmployeeCancel.Enabled = False
					MessageBox.Show("Data updated successfully.")
					LoadDataEmployees()
					ClearTextboxesEmployees()
					DisabledTextboxesEmployees()
					DisabledButtonsEmployees()
					comboloadPayroll()
					DisplayTotalEmployee(lblTotalEmployee)
					btnEmployeeNew.Enabled = True
					UploadPicture.Image = Nothing
					Guna2DataGridViewEmployees.Enabled = True
				End Using
			Catch ex As Exception
				MessageBox.Show("An error occurred while updating the data: " + ex.ToString)
			End Try
		Else
			MessageBox.Show("Please select a row to update.")
		End If

	End Sub

	Private Sub cboDonationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDonationType.SelectedIndexChanged
		If cboDonationType.SelectedItem = "Clothes" Then
			txtClothes.Visible = True
			LabelClothes.Visible = True
			txtCashAmount.Visible = False
			LabelCash.Visible = False
			txtEquipment.Visible = False
			LabelEquipment.Visible = False
			txtFood.Visible = False
			LabelFood.Visible = False
			txtQuantity.Visible = True
			LabelQuantity.Visible = True
		ElseIf cboDonationType.SelectedItem = "Cash" Then
			txtClothes.Visible = False
			LabelClothes.Visible = False
			txtCashAmount.Visible = True
			LabelCash.Visible = True
			txtEquipment.Visible = False
			LabelEquipment.Visible = False
			txtFood.Visible = False
			LabelFood.Visible = False
			txtQuantity.Visible = False
			LabelQuantity.Visible = False
		ElseIf cboDonationType.SelectedItem = "Equipment" Then
			txtClothes.Visible = False
			LabelClothes.Visible = False
			txtCashAmount.Visible = False
			LabelCash.Visible = False
			txtEquipment.Visible = True
			LabelEquipment.Visible = True
			txtFood.Visible = False
			LabelFood.Visible = False
			txtQuantity.Visible = True
			LabelQuantity.Visible = True
		ElseIf cboDonationType.SelectedItem = "Food" Then
			txtClothes.Visible = False
			LabelClothes.Visible = False
			txtCashAmount.Visible = False
			LabelCash.Visible = False
			txtEquipment.Visible = False
			LabelEquipment.Visible = False
			txtFood.Visible = True
			LabelFood.Visible = True
			txtQuantity.Visible = True
			LabelQuantity.Visible = True
		End If
	End Sub

	Private Sub btnEmployeeNew_Click(sender As Object, e As EventArgs) Handles btnEmployeeNew.Click
		EnabledTextboxesEmployees()
		ClearTextboxesEmployees()
		txtEmployeeLastName.Focus()
		btnEmployeeNew.Enabled = False
		btnEmployeeSave.Enabled = True
		btnEmployeeDelete.Enabled = False
		btnEmployeeCancel.Enabled = True
		Guna2DataGridViewEmployees.Enabled = False
	End Sub

	Private Sub btnEmployeeCancel_Click(sender As Object, e As EventArgs) Handles btnEmployeeCancel.Click
		ClearTextboxesEmployees()
		DisabledTextboxesEmployees()
		DisabledButtonsEmployees()
		btnEmployeeNew.Enabled = True
		Guna2DataGridViewEmployees.Enabled = True
		UploadPicture.Image = Nothing
	End Sub

	Private Sub txtEmployeeFirstName_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeFirstName.TextChanged

	End Sub

	Private Sub txtUserName_TextChanged(sender As Object, e As EventArgs) Handles txtUserName.TextChanged

	End Sub


	'-------------------------------------------------Start Medical Supplies-----------------------------------------------------------

	Private Sub ClearTextboxesMedicalSupplies()
		txtNamemedicine.Clear()
		txtMedicationID.Clear()
		txtExpirationdate.Clear()
		txtDosage.Clear()
		txtStock.Clear()
	End Sub
	Private Sub DisabledTextboxesMedicalSupplies()


		txtNamemedicine.Enabled = False
		txtExpirationdate.Enabled = False
		txtDosage.Enabled = False
		txtStock.Enabled = False
		'txtDonationCreatedAt.Enabled = False
		'txtDonationUpdatedAt.Enabled = False
	End Sub
	Private Sub EnabledTextboxesMedicalSupplies()
		txtNamemedicine.Enabled = True
		txtExpirationdate.Enabled = True
		txtDosage.Enabled = True
		txtStock.Enabled = True
		'txtDonationCreatedAt.Enabled = True
		'txtDonationUpdatedAt.Enabled = True
	End Sub
	Private Sub DisabledButtonsMedicalSupplies()

		'btnNew.Enabled = False
		btnMedicineSave.Enabled = False
		btnMedicineUpdate.Enabled = False
		btnMedicineDelete.Enabled = False
		btnMedicineCancel.Enabled = False
	End Sub

	Private Sub Guna2DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView3.CellClick
		EnabledTextboxesMedicalSupplies()
		btnDonationsNew.Enabled = False
		btnMedicineUpdate.Enabled = True
		btnMedicineDelete.Enabled = True
		btnMedicineCancel.Enabled = True

		' Get the selected row
		Dim selectedRow As DataGridViewRow = Nothing
		If e.RowIndex >= 0 AndAlso e.RowIndex < Guna2DataGridView3.Rows.Count Then
			selectedRow = Guna2DataGridView3.Rows(e.RowIndex)
		End If

		' Display the values in the text boxes if the row is not null
		If selectedRow IsNot Nothing Then
			txtMedicationID.Text = selectedRow.Cells("MedicationID").Value.ToString()
			txtNamemedicine.Text = selectedRow.Cells("Medicinename").Value.ToString()
			txtExpirationdate.Text = selectedRow.Cells("Expirationdate").Value.ToString()
			txtStock.Text = selectedRow.Cells("Stock").Value.ToString()
			txtDosage.Text = selectedRow.Cells("Dosage").Value.ToString()


		End If
	End Sub
	Private Sub LoadDataMedicalSupplies()
		Dim searchQuery As String = "SELECT * FROM `medication_schedule`"

		If Not String.IsNullOrWhiteSpace(Guna2TextBox32.Text) Then
			searchQuery += $" WHERE medication_medicine_name LIKE '%{Guna2TextBox32.Text}%'"
		End If

		Dim dataAdapter As New MySqlDataAdapter(searchQuery, connection)

		Guna2DataGridView3.ScrollBars = ScrollBars.Vertical

		' Set the default cell style for each column
		For Each col As DataGridViewColumn In Guna2DataGridView3.Columns
			col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
		Next

		' Change the font size of column headers
		Guna2DataGridView3.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Change the font size of cell values
		Guna2DataGridView3.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Set the CellBorderStyle property of the GunaDataGridView control
		Guna2DataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.Single

		' Set the GridColor property of the GunaDataGridView control
		Guna2DataGridView3.GridColor = Color.Black

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()
				Dim dataAdapterMedicationSchedule As New MySqlDataAdapter("SELECT medication_id as 'MedicationId', " &
													  "medication_medicine_name AS 'MedicineName', " &
													  "medication_dosage AS 'Dosage', " &
													  "medication_stock AS 'Stock', " &
													  "medication_expiration_date AS 'ExpirationDate', " &
													  "medication_created_at AS 'CreatedAt', " &
													  "medication_updated_at AS 'UpdatedAt' " &
													  "FROM medication_schedule", connection)


				Dim table As New DataTable()
				dataAdapterMedicationSchedule.Fill(table)

				Guna2DataGridView3.DataSource = table


			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while loading the data: " + ex.Message)
		End Try

	End Sub

	Private Sub btnMedicineNew_Click(sender As Object, e As EventArgs) Handles btnMedicineNew.Click
		EnabledTextboxesMedicalSupplies()
		ClearTextboxesMedicalSupplies()
		txtNamemedicine.Focus()
		btnMedicineNew.Enabled = False
		btnMedicineSave.Enabled = True
		btnMedicineDelete.Enabled = False
		btnMedicineCancel.Enabled = True
		Guna2DataGridView3.Enabled = False
	End Sub
	Private Sub btnMedicineSave_Click(sender As Object, e As EventArgs) Handles btnMedicineSave.Click
		If String.IsNullOrEmpty(txtNamemedicine.Text) OrElse String.IsNullOrEmpty(txtDosage.Text) Then
			MessageBox.Show("Please fill in all required fields.")
			Return
		End If

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Check if a record with the same Medical Supplies name already exists
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM medication_schedule WHERE medication_medicine_name = @medication_medicine_name", connection)
				selectCommand.Parameters.AddWithValue("@medication_medicine_name", txtNamemedicine.Text)

				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
				If count > 0 Then
					MessageBox.Show("A Medicine Supplies with the same name already exists.")
					Return
				End If

				' Insert a new record
				Dim insertcommand As New MySqlCommand("INSERT INTO medication_schedule (medication_medicine_name, medication_dosage, medication_stock, medication_created_at, medication_expiration_date) VALUES (@medication_medicine_name, @medication_dosage, @medication_stock, @medication_created_at, @medication_expiration_date)", connection)
				insertcommand.Parameters.AddWithValue("@medication_medicine_name", txtNamemedicine.Text)
				insertcommand.Parameters.AddWithValue("@medication_dosage", txtDosage.Text)
				insertcommand.Parameters.AddWithValue("@medication_stock", txtStock.Text)
				insertcommand.Parameters.AddWithValue("@medication_created_at", DateTime.Now)
				insertcommand.Parameters.AddWithValue("@medication_expiration_date", txtExpirationdate.Text)

				insertcommand.ExecuteNonQuery()
				btnMedicineSave.Enabled = False
				btnMedicineNew.Enabled = True
				btnMedicineCancel.Enabled = False
				MessageBox.Show("Data saved successfully.")
				LoadDataMedicalSupplies()
				ClearTextboxesMedicalSupplies()
				DisabledTextboxesMedicalSupplies()
				Guna2DataGridView3.Enabled = True
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while saving the data: " + ex.Message)
		End Try
	End Sub


	Private Sub btnMedicineUpdate_Click(sender As Object, e As EventArgs) Handles btnMedicineUpdate.Click
		If Guna2DataGridView3.SelectedRows.Count > 0 Then
			Dim selectedRow = Guna2DataGridView3.SelectedRows(0)
			Dim MedicationId = selectedRow.Cells("MedicationId").Value

			' Check if any changes have been made
			If (Not IsDBNull(selectedRow.Cells("MedicineName").Value) AndAlso txtNamemedicine.Text = selectedRow.Cells("MedicineName").Value) AndAlso
	(Not IsDBNull(selectedRow.Cells("Dosage").Value) AndAlso txtDosage.Text = selectedRow.Cells("Dosage").Value) AndAlso
	(Not IsDBNull(selectedRow.Cells("Stock").Value) AndAlso txtStock.Text = selectedRow.Cells("Stock").Value) AndAlso
	(Not IsDBNull(selectedRow.Cells("ExpirationDate").Value) AndAlso txtExpirationdate.Text = selectedRow.Cells("ExpirationDate").Value) Then

				MessageBox.Show("There are no changes to update.")
				Return
			End If


			' Show confirmation prompt
			If MessageBox.Show("Are you sure you want to update this Medical Supplies?", "Confirm Update", MessageBoxButtons.YesNo) = DialogResult.Yes Then
				Try
					Using connection As New MySqlConnection(connectionString)
						connection.Open()

						' Check if Medical Supplies type is not empty before updating

						Dim updateCommand As New MySqlCommand("UPDATE medication_schedule SET medication_medicine_name = @medication_medicine_name, medication_dosage = @medication_dosage, medication_stock = @medication_stock, medication_expiration_date = @medication_expiration_date, medication_updated_at = @medication_updated_at WHERE medication_id=@medication_id", connection)
						updateCommand.Parameters.AddWithValue("@medication_medicine_name", txtNamemedicine.Text)
						updateCommand.Parameters.AddWithValue("@medication_dosage", txtDosage.Text)
						updateCommand.Parameters.AddWithValue("@medication_stock", txtStock.Text)
						updateCommand.Parameters.AddWithValue("@medication_expiration_date", txtExpirationdate.Text)
						updateCommand.Parameters.AddWithValue("@medication_id", MedicationId)
						updateCommand.Parameters.AddWithValue("@medication_updated_at", DateTime.Now)
						updateCommand.ExecuteNonQuery()


						MessageBox.Show("Data updated successfully.")
						LoadDataMedicalSupplies()
						ClearTextboxesMedicalSupplies()
						DisabledTextboxesMedicalSupplies()
						DisabledButtonsMedicalSupplies()
						btnMedicineNew.Enabled = True


					End Using
				Catch ex As Exception
					MessageBox.Show("An error occurred while updating the data: " + ex.Message)
				End Try
			End If
		Else
			MessageBox.Show("Please select a row to update.")
		End If
	End Sub



	Private Sub btnMedicineDelete_Click(sender As Object, e As EventArgs) Handles btnMedicineDelete.Click
		' Get the selected row ID from the DataGridView
		If Guna2DataGridView3.SelectedRows.Count = 0 Then
			MessageBox.Show("Please select a row to delete.")
			Return
		End If
		Dim rowId As Integer = CInt(Guna2DataGridView3.SelectedRows(0).Cells("MedicationId").Value)

		' Confirm deletion with the user
		Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo)
		If result = DialogResult.Yes Then
			Try
				Using connection As New MySqlConnection(connectionString)
					connection.Open()

					Dim deleteCommand As New MySqlCommand("DELETE FROM medication_schedule WHERE medication_id = @id", connection)
					deleteCommand.Parameters.AddWithValue("@id", rowId)

					deleteCommand.ExecuteNonQuery()
				End Using
				LoadDataMedicalSupplies()
				MessageBox.Show("Record deleted successfully.")
				ClearTextboxesMedicalSupplies()
				DisabledTextboxesMedicalSupplies()
				DisabledButtonsMedicalSupplies()
				btnMedicineSave.Enabled = False
				btnMedicineNew.Enabled = True
			Catch ex As Exception
				MessageBox.Show("An error occurred while deleting the row: " & ex.Message)
			End Try
		End If
	End Sub

	Private Sub btnMedicinecCancel_Click(sender As Object, e As EventArgs) Handles btnMedicineCancel.Click
		ClearTextboxesMedicalSupplies()
		DisabledTextboxesMedicalSupplies()
		DisabledButtonsMedicalSupplies()
		btnMedicineNew.Enabled = True
		Guna2DataGridView3.Enabled = True
	End Sub
	Private Sub Guna2Button24_Click(sender As Object, e As EventArgs) Handles Guna2Button24.Click
		Dim searchText As String = Guna2TextBox32.Text

		' Query the database or data source to get the search results
		' For example, if you're using a MySQL database:
		Dim sql As String = $"SELECT medication_id AS MedicationId, medication_medicine_name AS MedicineName,  medication_dosage AS Dosage, medication_stock AS Stock,medication_expiration_date AS ExpirationDate,  FROM medication_schedule WHERE medication_medicine_name LIKE '%{searchText}%' OR medication_dosage LIKE '%{searchText}%' OR medication_stock LIKE '%{searchText}%' OR medication_expiration_date LIKE '%{searchText}%'"
		Dim searchResults As New DataTable()
		Using connection As New MySqlConnection(connectionString)
			Using command As New MySqlCommand(sql, connection)
				connection.Open()
				searchResults.Load(command.ExecuteReader())
			End Using
		End Using



		' Bind the results to the DataGridView
		Guna2DataGridView3.DataSource = searchResults

		' Check if any rows were found
		If searchResults.Rows.Count = 0 Then
			MessageBox.Show("No data found.")
		End If
	End Sub

	Private Sub Guna2TextBox32_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox32.TextChanged
		If String.IsNullOrEmpty(Guna2TextBox32.Text.Trim()) Then
			LoadDataMedicalSupplies()
			Guna2Button24.Enabled = False
		Else
			Guna2Button24.Enabled = True
		End If
	End Sub


	'-------------------------------------------------End Medical Supplies-----------------------------------------------------------


	Private Sub btnEmployeeDelete_Click(sender As Object, e As EventArgs) Handles btnEmployeeDelete.Click
		' Get the selected row ID from the DataGridView
		If Guna2DataGridViewEmployees.SelectedRows.Count = 0 Then
			MessageBox.Show("Please select a row to delete.")
			Return
		End If
		Dim rowId As Integer = CInt(Guna2DataGridViewEmployees.SelectedRows(0).Cells("EmployeeId").Value)

		' Confirm deletion with the user
		Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo)
		If result = DialogResult.Yes Then
			Try
				Using connection As New MySqlConnection(connectionString)
					connection.Open()

					Dim deleteCommand As New MySqlCommand("DELETE FROM employee WHERE employee_id = @id", connection)
					deleteCommand.Parameters.AddWithValue("@id", rowId)

					deleteCommand.ExecuteNonQuery()
				End Using
				LoadDataEmployees()
				MessageBox.Show("Record deleted successfully.")
				ClearTextboxesEmployees()
				DisabledTextboxesEmployees()
				DisabledButtonsEmployees()
				comboloadPayroll()
				DisplayTotalEmployee(lblTotalEmployee)
				btnEmployeeSave.Enabled = False
				btnEmployeeNew.Enabled = True
			Catch ex As Exception
				MessageBox.Show("An error occurred while deleting the row: " & ex.Message)
			End Try
		End If
	End Sub
	Sub comboloadPayroll()
		Using connection As New MySqlConnection(connectionString)
			connection.Open()
			Dim adapter As New MySqlDataAdapter("SELECT CONCAT(employee_first_name, ' ', employee_last_name) AS fullname FROM employee", connection)
			Dim searchresult As New DataTable()

			adapter.Fill(searchresult)
			cmbPayroll.DataSource = searchresult
			cmbPayroll.ValueMember = "fullname"
			cmbPayroll.DisplayMember = "fullname"
		End Using
	End Sub

	Private Sub LoadDataPayroll()
		Dim searchQuery As String = "SELECT * FROM `payroll`"

		If Not String.IsNullOrWhiteSpace(txtPayrollSearch.Text) Then
			searchQuery += $" WHERE user_id LIKE '%{txtPayrollSearch.Text}%'"
		End If

		Dim dataAdapter As New MySqlDataAdapter(searchQuery, connection)

		Guna2DataGridView1.ScrollBars = ScrollBars.Vertical

		' Set the default cell style for each column
		For Each col As DataGridViewColumn In Guna2DataGridView1.Columns
			col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
		Next

		' Change the font size of column headers
		Guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Change the font size of cell values
		Guna2DataGridView1.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

		' Set the CellBorderStyle property of the GunaDataGridView control
		Guna2DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single

		' Set the GridColor property of the GunaDataGridView control
		Guna2DataGridView1.GridColor = Color.Black

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()
				Dim dataAdapterPayroll As New MySqlDataAdapter("SELECT payroll_id as 'ID', " &
													  "user_id AS 'EmployeeName', " &
													  "basic_salary AS 'DailyWage', " &
													  "number_of_days AS 'NoOfDays', " &
													  "total_salary AS 'Salary', " &
													  "payroll_created_at AS 'CreatedAt', " &
													  "payroll_updated_at AS 'UpdatedAt' " &
													  "FROM payroll", connection)




				Dim table As New DataTable()
				dataAdapterPayroll.Fill(table)

				Guna2DataGridView1.DataSource = table

			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while loading the data: " + ex.Message)
		End Try

	End Sub

	Private Sub ClearTextboxesPayroll()
		cmbPayroll.SelectedItem = Nothing
		txtSalary.Clear()
		txtNoOfDays.Clear()
	End Sub

	Private Sub DisabledTextboxesPayroll()

		cmbPayroll.Enabled = False
		txtSalary.Enabled = False
		txtNoOfDays.Enabled = False

	End Sub
	Private Sub EnabledTextboxesPayroll()

		cmbPayroll.Enabled = True
		txtSalary.Enabled = True
		txtNoOfDays.Enabled = True

	End Sub
	Private Sub DisabledButtonsPayroll()

		btnSavepayroll.Enabled = False
		btyUpdatepayroll.Enabled = False
		btnDeletepayroll.Enabled = False
	End Sub
	Private Sub EnabledButtonsPayroll()

		btnSavepayroll.Enabled = True
		btyUpdatepayroll.Enabled = True
		btnDeletepayroll.Enabled = True
	End Sub
	Private Sub btnSavepayroll_Click(sender As Object, e As EventArgs) Handles btnSavepayroll.Click
		If String.IsNullOrEmpty(cmbPayroll.Text) OrElse String.IsNullOrEmpty(txtNoOfDays.Text) OrElse String.IsNullOrEmpty(txtSalary.Text) Then
			MessageBox.Show("Please fill in all required fields.")
			Return
		End If

		Try
			Using connection As New MySqlConnection(connectionString)
				connection.Open()

				' Check if a record with the same user name already exists
				Dim selectCommand As New MySqlCommand("SELECT COUNT(*) FROM payroll WHERE user_id = @user_id", connection)
				selectCommand.Parameters.AddWithValue("@user_id", cmbPayroll.Text)

				Dim count As Integer = Convert.ToInt32(selectCommand.ExecuteScalar())
				If count > 0 Then
					MessageBox.Show("An employee is already added please check.")
					Return
				End If

				' Insert a new record
				Dim insertcommand As New MySqlCommand("INSERT INTO payroll (user_id, basic_salary, number_of_days, total_salary, payroll_created_at) VALUES (@user_id, @basic_salary, @number_of_days, @total_salary, @payroll_created_at)", connection)
				Dim total_salary As Integer

				total_salary = txtSalary.Text * txtNoOfDays.Text

				insertcommand.Parameters.AddWithValue("@user_id", cmbPayroll.Text)
				insertcommand.Parameters.AddWithValue("@basic_salary", txtSalary.Text)
				insertcommand.Parameters.AddWithValue("@number_of_days", txtNoOfDays.Text)
				insertcommand.Parameters.AddWithValue("@total_salary", total_salary)
				insertcommand.Parameters.AddWithValue("@payroll_created_at", DateTime.Now)
				insertcommand.ExecuteNonQuery()

				btnSavepayroll.Enabled = False
				btnCancelpayroll.Enabled = False
				MessageBox.Show("Data saved successfully.")
				LoadDataPayroll()
				ClearTextboxesPayroll()
				EnabledTextboxesPayroll()
				btnSavepayroll.Enabled = True
				Guna2DataGridView1.Enabled = True
			End Using
		Catch ex As Exception
			MessageBox.Show("An error occurred while saving the data: " + ex.Message)
		End Try
	End Sub

	Private Sub btyUpdatepayroll_Click(sender As Object, e As EventArgs) Handles btyUpdatepayroll.Click
		If Guna2DataGridView1.SelectedRows.Count > 0 Then
			Dim selectedRow = Guna2DataGridView1.SelectedRows(0)
			Dim Payroll_ID = selectedRow.Cells("ID").Value
			Dim total_salary As Integer

			total_salary = txtSalary.Text * txtNoOfDays.Text

			' Check if any changes have been made
			If cmbPayroll.Text = selectedRow.Cells("EmployeeName").Value AndAlso
			   txtSalary.Text = selectedRow.Cells("DailyWage").Value AndAlso
			   txtNoOfDays.Text = selectedRow.Cells("NoOfDays").Value Then
				MessageBox.Show("There are no changes to update.")
				Return
			End If

			' Show confirmation prompt
			If MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo) = DialogResult.Yes Then
				Try
					Using connection As New MySqlConnection(connectionString)
						connection.Open()

						Dim updateCommand As New MySqlCommand("UPDATE payroll SET user_id = @user_id, basic_salary = @basic_salary, number_of_days = @number_of_days, total_salary = @total_salary, payroll_updated_at = @payroll_updated_at WHERE payroll_id  = @payroll_id", connection)
						updateCommand.Parameters.AddWithValue("@user_id", cmbPayroll.Text)
						updateCommand.Parameters.AddWithValue("@basic_salary", txtSalary.Text)
						updateCommand.Parameters.AddWithValue("@number_of_days", txtNoOfDays.Text)
						updateCommand.Parameters.AddWithValue("@total_salary", total_salary)
						updateCommand.Parameters.AddWithValue("payroll_id", Payroll_ID)
						updateCommand.Parameters.AddWithValue("@payroll_updated_at", DateTime.Now)
						updateCommand.ExecuteNonQuery()

						MessageBox.Show("Record updated successfully.")
						LoadDataPayroll()
						ClearTextboxesPayroll()
						EnabledTextboxesPayroll()
						DisabledButtonsPayroll()
						btnSavepayroll.Enabled = True
						btnCancelpayroll.Enabled = False

					End Using
				Catch ex As Exception
					MessageBox.Show("An error occurred while updating the record: " + ex.Message)
				End Try
			End If
		Else
			MessageBox.Show("Please select a record to update.")
		End If
	End Sub

	Private Sub btnDeletepayroll_Click(sender As Object, e As EventArgs) Handles btnDeletepayroll.Click
		' Get the selected row ID from the DataGridView
		If Guna2DataGridView1.SelectedRows.Count = 0 Then
			MessageBox.Show("Please select a row to delete.")
			Return
		End If
		Dim rowId As Integer = CInt(Guna2DataGridView1.SelectedRows(0).Cells("ID").Value)

		' Confirm deletion with the user
		Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo)
		If result = DialogResult.Yes Then
			Try
				Using connection As New MySqlConnection(connectionString)
					connection.Open()

					Dim deleteCommand As New MySqlCommand("DELETE FROM payroll WHERE payroll_id = @id", connection)
					deleteCommand.Parameters.AddWithValue("@id", rowId)

					deleteCommand.ExecuteNonQuery()
				End Using
				LoadDataPayroll()
				MessageBox.Show("Record deleted successfully.")
				ClearTextboxesPayroll()
				EnabledTextboxesPayroll()
				DisabledButtonsPayroll()
				btnSavepayroll.Enabled = True
				btnCancelpayroll.Enabled = False
			Catch ex As Exception
				MessageBox.Show("An error occurred while deleting the row: " & ex.Message)
			End Try
		End If
	End Sub

	Private Sub btnCancelpayroll_Click(sender As Object, e As EventArgs) Handles btnCancelpayroll.Click
		ClearTextboxesPayroll()
		EnabledTextboxesPayroll()
		DisabledButtonsPayroll()
		btnSavepayroll.Enabled = True
		btnCancelpayroll.Enabled = False
		Guna2DataGridView1.Enabled = True
	End Sub

	Private Sub Guna2DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellClick
		EnabledTextboxesPayroll()
		btnSavepayroll.Enabled = False
		btyUpdatepayroll.Enabled = True
		btnDeletepayroll.Enabled = True
		btnCancelpayroll.Enabled = True

		' Get the selected row
		Dim selectedRow As DataGridViewRow = Nothing

		If e.RowIndex >= 0 AndAlso e.RowIndex < Guna2DataGridView1.Rows.Count Then
			selectedRow = Guna2DataGridView1.Rows(e.RowIndex)
		End If

		' Display the values in the text boxes if the row is not null
		If selectedRow IsNot Nothing Then
			cmbPayroll.Text = selectedRow.Cells("EmployeeName").Value.ToString()
			txtSalary.Text = selectedRow.Cells("DailyWage").Value.ToString()
			txtNoOfDays.Text = selectedRow.Cells("NoOfDays").Value.ToString()
			txtPayrollCreatedat.Text = selectedRow.Cells("CreatedAt").Value.ToString()
			txtPayrollUpdatedat.Text = selectedRow.Cells("UpdatedAt").Value.ToString()


		End If
	End Sub

	Private Sub btnEmployeeeSearch_Click(sender As Object, e As EventArgs) Handles btnEmployeeeSearch.Click
		Dim searchText As String = txtEmployeeSearch.Text

		' Query the database or data source to get the search results
		' For example, if you're using a MySQL database:
		Dim sql As String = $"SELECT employee_id AS EmployeeId, employee_first_name AS Firstname, employee_middle_name AS Middlename, employee_last_name AS Lastname, employee_address AS Address, employee_religion AS Religion, employee_birthday AS Birthday, employee_gender AS Gender, employee_status AS Status, employee_contact_number AS ContactNumber, employee_designation AS Designation, employee_educational AS Educational,employee_image AS EmployeePicture, employee_created_at AS CreatedAt, employee_updated_at AS UpdatedAt FROM employee WHERE employee_first_name LIKE @searchText OR employee_middle_name LIKE @searchText OR employee_last_name LIKE @searchText"
		Dim searchResults As New DataTable()
		Using connection As New MySqlConnection(connectionString)
			Using command As New MySqlCommand(sql, connection)
				command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
				connection.Open()
				searchResults.Load(command.ExecuteReader())
			End Using
		End Using

		' Bind the results to the DataGridView
		Guna2DataGridViewEmployees.DataSource = searchResults

		' Check if any rows were found
		If searchResults.Rows.Count = 0 Then
			MessageBox.Show("No data found.")
		End If
	End Sub

	Private Sub btnPayrollSearch_Click(sender As Object, e As EventArgs) Handles btnPayrollSearch.Click
		Dim searchText As String = txtPayrollSearch.Text

		' Query the database or data source to get the search results
		' For example, if you're using a MySQL database:
		Dim sql As String = $"SELECT payroll_id  AS ID, user_id AS EmployeeName, basic_salary AS DailyWage, number_of_days AS NoOfDays, total_salary AS Salary, payroll_created_at AS CreatedAt, payroll_updated_at AS UpdatedAt FROM payroll WHERE user_id LIKE @searchText OR basic_salary LIKE @searchText OR number_of_days LIKE @searchText"
		Dim searchResults As New DataTable()
		Using connection As New MySqlConnection(connectionString)
			Using command As New MySqlCommand(sql, connection)
				command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
				connection.Open()
				searchResults.Load(command.ExecuteReader())
			End Using
		End Using

		' Bind the results to the DataGridView
		Guna2DataGridView1.DataSource = searchResults

		' Check if any rows were found
		If searchResults.Rows.Count = 0 Then
			MessageBox.Show("No data found.")
		End If
	End Sub

	Private Sub btnTotal_Click(sender As Object, e As EventArgs)
		Dim totalCashAmount As Decimal = 0

		For Each row As DataGridViewRow In Guna2DataGridViewDonations.Rows
			If Not row.IsNewRow Then
				Dim cashAmount As Decimal = 0
				Decimal.TryParse(row.Cells("CashAmount").Value.ToString(), cashAmount)
				totalCashAmount += cashAmount
			End If
		Next

		MessageBox.Show("Total Cash Amount: " & totalCashAmount.ToString())
	End Sub

	Private Sub btnTotalPayroll_Click(sender As Object, e As EventArgs)
		Dim totalSalary As Decimal = 0

		For Each row As DataGridViewRow In Guna2DataGridView1.Rows
			If Not row.IsNewRow Then
				Dim salaryAmount As Decimal = 0
				Decimal.TryParse(row.Cells("Salary").Value.ToString(), salaryAmount)
				totalSalary += salaryAmount
			End If
		Next

		MessageBox.Show("Total Cash Amount: " & totalSalary.ToString())
	End Sub

	Private Sub TotalOrphanspanel_Click(sender As Object, e As EventArgs) Handles TotalOrphanspanel.Click
		TabControl1.SelectedIndex = 2
		HighlightSelectedButton(btnDonation, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
		Timer2.Start()
		Timer4.Start()
	End Sub

	Private Sub Guna2GradientPanel4_Click(sender As Object, e As EventArgs) Handles Guna2GradientPanel4.Click
		TabControl1.SelectedIndex = 4
		HighlightSelectedButton(btnDonation, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
		Timer2.Start()
		Timer4.Start()
	End Sub

	Private Sub Guna2GradientPanel5_Click(sender As Object, e As EventArgs) Handles Guna2GradientPanel5.Click
		TabControl1.SelectedIndex = 6
		HighlightSelectedButton(btnDonation, Color.FromArgb(128, 128, 255)) ' set the RGB value for the selected button
		Timer2.Start()
		Timer4.Start()
	End Sub
End Class