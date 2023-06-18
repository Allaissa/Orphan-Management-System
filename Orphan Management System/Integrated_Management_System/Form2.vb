Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine

Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Web.Services
Imports Mysqlx.XDevAPI.Relational
Imports System.Reflection
Imports System.Globalization
Imports System.Diagnostics.Eventing.Reader

Public Class Form2
    Dim con1 As MySqlConnection
    Dim cmd1 As MySqlCommand

    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim dtb As DataTableCollection
    Dim source1 As New BindingSource

    'Set a functions to be passed if the specific commands has been pushed

    Private orphan, employee, payroll, medical, donation, jan, feb, mar, ap, may, jun, jul, aug, sep, oct, nov, dec, all, month, year, specific As Boolean
    Private selectedMonth, selectedYear As Integer
    Private specificYear As String
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Date_filter_false()
        donation_buttons_false()
    End Sub
    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            e.Cancel = True ' Cancel the form closing event
        Else
            Dim dashboardForm As New Dashboard()
            dashboardForm.Show()
        End If
    End Sub
    Private Sub report_button_false()
        donation = False
        orphan = False
        medical = False
        employee = False
        payroll = False
    End Sub
    Private Sub Grid_Views_False()
        DataGridView1.Visible = False
        DataGridView2.Visible = False
        DataGridView3.Visible = False
        DataGridView4.Visible = False
        DataGridView5.Visible = False
        btnReportAll.Visible = False
    End Sub
    Private Sub donation_buttons_false()
        lblMonth.Visible = False
        lblYear.Visible = False
        cmbMonth.Visible = False
        cmbYear.Visible = False
        btnFilter.Visible = False
        btnReportAll.Visible = False
        btnSearch.Visible = False
        txtSearch.Visible = False
    End Sub
    Private Sub donation_buttons_enable()
        lblMonth.Visible = True
        lblYear.Visible = True
        cmbMonth.Visible = True
        cmbYear.Visible = True
        btnFilter.Visible = True
        btnReportAll.Visible = True
        btnSearch.Visible = True
        txtSearch.Visible = True
    End Sub
    Private Sub Date_filter_false()
        jan = False
        feb = False
        mar = False
        ap = False
        may = False
        jun = False
        jul = False
        aug = False
        sep = False
        oct = False
        nov = False
        dec = False
        all = False
        year = False
        month = False
        specific = False
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim connectionString As String = "Server=localhost;Database=integrated_management_system;Uid=root;Pwd=''"
        Dim connection As MySqlConnection = New MySqlConnection(connectionString)
        Dim searchText As String = txtSearch.Text

        ' Query the database or data source to get the search results
        ' For example, if you're using a MySQL database:
        If orphan = True Then
            Dim sql As String = $"SELECT patient_id AS PatientId, patient_date_of_admission AS DateofAdmission, patient_first_name AS FirstName, patient_middle_name AS MiddleName, patient_last_name AS LastName, patient_orphan_address AS Address, patient_birth_date AS BirthDate, patient_place_of_birth AS PlaceOfBirth ,patient_status AS Status, patient_religion AS religion, patient_educational_attainment AS EducationalAttainment, patient_family_member_name AS FamilyMemberName, patient_relation_to_client AS RelationtoClient, patient_family_address AS FamilyAddress ,patient_emergency_number AS EmergencyNumber, patient_description AS Description, patient_created_at AS CreatedAt, patient_updated_at AS UpdatedAt FROM patients WHERE patient_first_name LIKE @searchText OR patient_last_name LIKE @searchText OR patient_middle_name LIKE @searchText OR patient_family_member_name LIKE @searchText"

            Dim searchResults As New DataTable()

            Using command As New MySqlCommand(sql, connection)
                command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
                connection.Open()
                searchResults.Load(command.ExecuteReader())
            End Using

            ' Bind the results to the DataGridView
            DataGridView1.DataSource = searchResults

            ' Check if any rows were found
            If searchResults.Rows.Count = 0 Then
                MessageBox.Show("No data found.")
            End If
        ElseIf medical = True Then
            Dim sql As String = "SELECT medical_id AS MedicalId, medical_name AS MedicalName, medical_diagnostic AS Diagnostic, medical_intake AS Intake, medical_weight AS Weight, medical_height AS Height, medical_blood_pressure AS BloodPressure, medical_temperature AS Temperature, medical_doctors_name AS DoctorsName, medical_date_recorded AS DateRecorded, medical_created_at AS CreatedAt, medical_updated_at AS UpdatedAt FROM medical_history WHERE medical_name LIKE @searchText OR medical_diagnostic LIKE @searchText OR medical_doctors_name LIKE @searchText OR medical_temperature LIKE @searchText"

            Dim searchResults As New DataTable()
            Using command As New MySqlCommand(sql, connection)
                command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
                connection.Open()
                searchResults.Load(command.ExecuteReader())
            End Using

            ' Bind the results to the DataGridView
            DataGridView2.DataSource = searchResults

            ' Check if any rows were found
            If searchResults.Rows.Count = 0 Then
                MessageBox.Show("No data found.")
            End If
        ElseIf donation = True Then
            If year = True Then
                Dim sql As String = $"SELECT donation_id AS DonationId, sponsor_name AS SponsorName, sponsor_gender As Gender, sponsor_address As SponsorAddress, donation_phone_number As PhoneNumber, donation_date_donated As DateDonated, donation_type As DonationType, inventory_type As InventoryType, donation_quantity As Quantity, cash_amount As CashAmount, donation_created_at As CreatedAt, donation_updated_at AS UpdatedAt FROM donations WHERE (sponsor_name LIKE @searchText OR sponsor_address LIKE @searchText OR donation_type LIKE @searchText OR inventory_type LIKE @searchText) AND year(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = @specificYear"
                Dim searchResults As New DataTable()
                Using command As New MySqlCommand(sql, connection)
                    command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
                    command.Parameters.AddWithValue("@specificYear", "20" + specificYear)
                    connection.Open()
                    searchResults.Load(command.ExecuteReader())
                End Using

                ' Bind the results to the DataGridView
                DataGridView3.DataSource = searchResults

                ' Check if any rows were found
                If searchResults.Rows.Count = 0 Then
                    MessageBox.Show("No data found.")
                End If
            ElseIf specific = True Then
                Dim sql As String = $"SELECT donation_id AS DonationId, sponsor_name AS SponsorName, sponsor_gender As Gender, sponsor_address As SponsorAddress, donation_phone_number As PhoneNumber, donation_date_donated As DateDonated, donation_type As DonationType, inventory_type As InventoryType, donation_quantity As Quantity, cash_amount As CashAmount, donation_created_at As CreatedAt, donation_updated_at AS UpdatedAt FROM donations WHERE (sponsor_name LIKE @searchText OR sponsor_address LIKE @searchText OR donation_type LIKE @searchText OR inventory_type LIKE @searchText) AND  month(donation_date_donated) = @selectedMonth And year(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = @specificYear"
                Dim searchResults As New DataTable()
                Using command As New MySqlCommand(sql, connection)
                    command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
                    command.Parameters.AddWithValue("@specificYear", "20" + specificYear)
                    command.Parameters.AddWithValue("@selectedMonth", selectedMonth)
                    connection.Open()
                    searchResults.Load(command.ExecuteReader())
                End Using

                ' Bind the results to the DataGridView
                DataGridView3.DataSource = searchResults

                ' Check if any rows were found
                If searchResults.Rows.Count = 0 Then
                    MessageBox.Show("No data found.")
                End If

            ElseIf month = True Then
                Dim sql As String = $"SELECT donation_id AS DonationId, sponsor_name AS SponsorName, sponsor_gender As Gender, sponsor_address As SponsorAddress, donation_phone_number As PhoneNumber, donation_date_donated As DateDonated, donation_type As DonationType, inventory_type As InventoryType, donation_quantity As Quantity, cash_amount As CashAmount, donation_created_at As CreatedAt, donation_updated_at AS UpdatedAt FROM donations WHERE (sponsor_name LIKE @searchText OR sponsor_address LIKE @searchText OR donation_type LIKE @searchText OR inventory_type LIKE @searchText) AND MONTH(donation_date_donated) = @selectedMonth"
                Dim searchResults As New DataTable()
                Using command As New MySqlCommand(sql, connection)
                    command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
                    command.Parameters.AddWithValue("@selectedMonth", selectedMonth)
                    connection.Open()
                    searchResults.Load(command.ExecuteReader())
                End Using

                ' Bind the results to the DataGridView
                DataGridView3.DataSource = searchResults

                ' Check if any rows were found
                If searchResults.Rows.Count = 0 Then
                    MessageBox.Show("No data found.")
                End If
            Else
                Dim sql As String = $"SELECT donation_id AS DonationId, sponsor_name AS SponsorName, sponsor_gender As Gender, sponsor_address As SponsorAddress, donation_phone_number As PhoneNumber, donation_date_donated As DateDonated, donation_type As DonationType, inventory_type As InventoryType, donation_quantity As Quantity, cash_amount As CashAmount, donation_created_at As CreatedAt, donation_updated_at AS UpdatedAt FROM donations WHERE sponsor_name LIKE @searchText OR sponsor_address LIKE @searchText OR donation_type LIKE @searchText OR inventory_type LIKE @searchText"
                'Command.Parameters.AddWithValue("@monthFilter", selectedMonth)
                Dim searchResults As New DataTable()
                Using command As New MySqlCommand(sql, connection)
                    command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
                    connection.Open()
                    searchResults.Load(command.ExecuteReader())
                End Using

                ' Bind the results to the DataGridView
                DataGridView3.DataSource = searchResults

                ' Check if any rows were found
                If searchResults.Rows.Count = 0 Then
                    MessageBox.Show("No data found.")
                End If
            End If
        ElseIf payroll = True Then
            Dim sql As String = $"SELECT payroll_id  AS ID, user_id AS EmployeeName, basic_salary AS DailyWage, number_of_days AS NoOfDays, total_salary AS Salary, payroll_created_at AS CreatedAt, payroll_updated_at AS UpdatedAt FROM payroll WHERE user_id LIKE @searchText OR basic_salary LIKE @searchText OR number_of_days LIKE @searchText"

            Dim searchResults As New DataTable()
            Using command As New MySqlCommand(sql, connection)
                command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
                connection.Open()
                searchResults.Load(command.ExecuteReader())
            End Using

            ' Bind the results to the DataGridView
            DataGridView4.DataSource = searchResults

            ' Check if any rows were found
            If searchResults.Rows.Count = 0 Then
                MessageBox.Show("No data found.")
            End If
        ElseIf employee = True Then
            Dim sql As String = $"SELECT employee_id AS EmployeeId, employee_first_name AS Firstname, employee_middle_name AS Middlename, employee_last_name AS Lastname, employee_address AS Address, employee_religion AS Religion, employee_birthday AS Birthday, employee_gender AS Gender, employee_status AS Status, employee_contact_number AS ContactNumber, employee_designation AS Designation, employee_educational AS Educational,employee_image AS EmployeePicture, employee_created_at AS CreatedAt, employee_updated_at AS UpdatedAt FROM employee WHERE employee_first_name LIKE @searchText OR employee_middle_name LIKE @searchText OR employee_last_name LIKE @searchText"

            Dim searchResults As New DataTable()
            Using command As New MySqlCommand(sql, connection)
                command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
                connection.Open()
                searchResults.Load(command.ExecuteReader())
            End Using

            ' Bind the results to the DataGridView
            DataGridView5.DataSource = searchResults

            ' Check if any rows were found
            If searchResults.Rows.Count = 0 Then
                MessageBox.Show("No data found.")
            End If

        End If
    End Sub
    Private Sub LblReportPatient_Click_1(sender As Object, e As EventArgs) Handles LblReportPatient.Click
        report_button_false()
        Grid_Views_False()
        donation_buttons_false()
        Date_filter_false()
        cmbMonth.Items.Clear()
        cmbYear.Items.Clear()
        orphan = True
        DataGridView1.Visible = True
        btnReportAll.Visible = True
        btnSearch.Visible = True
        txtSearch.Visible = True

        DataGridView1.ScrollBars = ScrollBars.Vertical

        ' Set the default cell style for each column
        For Each col As DataGridViewColumn In DataGridView1.Columns
            col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
        Next

        ' Change the font size of column headerssss
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Change the font size of cell values
        DataGridView1.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Set the CellBorderStyle property of the GunaDataGridView control
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single

        ' Set the GridColor property of the GunaDataGridView control
        DataGridView1.GridColor = Color.Black

        Try
            Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
            myConnection.Open()

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
                                                            "patient_created_at AS 'CreatedAt', patient_updated_at AS 'UpdatedAt' FROM patients", myConnection)

            Dim table As New DataTable()
            dataAdapterOrphans.Fill(table)

            Dim buttonColumn As New DataGridViewButtonColumn()
            buttonColumn.Name = "SelectButton"
            buttonColumn.HeaderText = "Select"
            buttonColumn.Text = "Print Report"
            buttonColumn.UseColumnTextForButtonValue = True

            Dim columnExists As Boolean = False
            For Each column As DataGridViewColumn In DataGridView1.Columns
                If column.Name = buttonColumn.Name Then
                    columnExists = True
                    Exit For
                End If
            Next

            ' Add the column if it doesn't exist
            If Not columnExists Then
                DataGridView1.Columns.Add(buttonColumn)
            End If

            DataGridView1.DataSource = table
            AddHandler DataGridView1.CellContentClick, AddressOf DataGridView1_CellContentClick
            CrystalReportViewer1.ReportSource = Nothing
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading the data: " + ex.Message)
        End Try

    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        ' Check if the clicked cell is in the "SelectButton" column
        If e.ColumnIndex = DataGridView1.Columns("SelectButton").Index AndAlso e.RowIndex >= 0 Then
            ' Retrieve the selected row and its data
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim patientId As Integer = CInt(selectedRow.Cells("PatientId").Value)
            Dim firstName As String = CStr(selectedRow.Cells("FirstName").Value)
            Dim middleName As String = CStr(selectedRow.Cells("MiddleName").Value)
            Dim lastName As String = CStr(selectedRow.Cells("LastName").Value)
            Dim birthPlace As String = CStr(selectedRow.Cells("PlaceofBirth").Value)
            Dim dateofbirth As String = CStr(selectedRow.Cells("BirthDate").Value)
            Dim Address As String = CStr(selectedRow.Cells("Address").Value)
            Dim admission As String = CStr(selectedRow.Cells("DateofAdmission").Value)
            Dim status As String = CStr(selectedRow.Cells("Status").Value)
            Dim familyMember As String = CStr(selectedRow.Cells("FamilyMemberName").Value)
            Dim relationClient As String = CStr(selectedRow.Cells("RelationtoClient").Value)
            Dim emergencyNumber As String = CStr(selectedRow.Cells("EmergencyNumber").Value)
            Dim religion As String = CStr(selectedRow.Cells("Religion").Value)
            Dim education As String = CStr(selectedRow.Cells("EducationalAttainment").Value)
            Dim clientAddress As String = CStr(selectedRow.Cells("FamilyAddress").Value)
            Dim description As String = CStr(selectedRow.Cells("Description").Value)

            ' Perform the desired action with the selected row data
            Dim rpt As New ReportOrphan()
            Dim fullName As String = firstName & " " & middleName & " " & lastName
            rpt.SetParameterValue("patient_id", patientId)
            rpt.SetParameterValue("patient_admission", admission)
            rpt.SetParameterValue("patient_full_name", fullName)
            rpt.SetParameterValue("patient_birth_place", birthPlace)
            rpt.SetParameterValue("patient_dob", dateofbirth)
            rpt.SetParameterValue("patient_address", Address)
            rpt.SetParameterValue("patient_status", status)
            rpt.SetParameterValue("patient_family_member", familyMember)
            rpt.SetParameterValue("patient_relation_client", relationClient)
            rpt.SetParameterValue("patient_client_address", clientAddress)
            rpt.SetParameterValue("patient_emergency_number", emergencyNumber)
            rpt.SetParameterValue("patient_religion", religion)
            rpt.SetParameterValue("patient_description", description)
            rpt.SetParameterValue("patient_education", education)

            CrystalReportViewer1.ReportSource = rpt
            Grid_Views_False()
            donation_buttons_false()
        End If
    End Sub
    Private Sub LblReportMedicalHistory_Click(sender As Object, e As EventArgs) Handles LblReportMedicalHistory.Click
        Grid_Views_False()
        report_button_false()
        donation_buttons_false()
        Date_filter_false()
        cmbMonth.Items.Clear()
        cmbYear.Items.Clear()
        medical = True
        DataGridView2.Visible = True
        btnReportAll.Visible = True
        btnSearch.Visible = True
        txtSearch.Visible = True

        DataGridView2.ScrollBars = ScrollBars.Vertical

        ' Set the default cell style for each column
        For Each col As DataGridViewColumn In DataGridView2.Columns
            col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
        Next

        ' Change the font size of column headers
        DataGridView2.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Change the font size of cell values
        DataGridView2.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Set the CellBorderStyle property of the GunaDataGridView control
        DataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.Single

        ' Set the GridColor property of the GunaDataGridView control
        DataGridView2.GridColor = Color.Black

        Try
            Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
            myConnection.Open()
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
                                                      "FROM medical_history", myConnection)


            Dim table As New DataTable()
            dataAdapterMedicalHistory.Fill(table)


            Dim buttonColumn As New DataGridViewButtonColumn()
            buttonColumn.Name = "SelectButton"
            buttonColumn.HeaderText = "Report"
            buttonColumn.Text = "Print Report"
            buttonColumn.UseColumnTextForButtonValue = True

            Dim columnExists As Boolean = False
            For Each column As DataGridViewColumn In DataGridView2.Columns
                If column.Name = buttonColumn.Name Then
                    columnExists = True
                    Exit For
                End If
            Next

            ' Add the column if it doesn't exist
            If Not columnExists Then
                DataGridView2.Columns.Add(buttonColumn)
            End If

            DataGridView2.DataSource = table
            AddHandler DataGridView2.CellContentClick, AddressOf DataGridView2_CellContentClick
            CrystalReportViewer1.ReportSource = Nothing
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading the data: " + ex.Message)
        End Try

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        ' Check if the clicked cell is in the "SelectButton" column
        If e.ColumnIndex = DataGridView2.Columns("SelectButton").Index AndAlso e.RowIndex >= 0 Then
            ' Retrieve the selected row and its data
            Dim selectedRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            Dim id As Integer = CInt(selectedRow.Cells("MedicalId").Value)
            Dim name As String = CStr(selectedRow.Cells("MedicalName").Value)
            Dim diagnostic As String = CStr(selectedRow.Cells("Diagnostic").Value)
            Dim bp As String = CStr(selectedRow.Cells("BloodPressure").Value)
            Dim temp As String = CStr(selectedRow.Cells("Temperature").Value)
            Dim weight As String = CStr(selectedRow.Cells("Weight").Value)
            Dim height As String = CStr(selectedRow.Cells("Height").Value)
            Dim intake As String = CStr(selectedRow.Cells("Intake").Value)
            Dim doctor As String = CStr(selectedRow.Cells("DoctorsName").Value)
            Dim dateRecorded As String = CStr(selectedRow.Cells("DateRecorded").Value)

            ' Perform the desired action with the selected row data
            Dim rpt As New ReportMedicalHistory()
            rpt.SetParameterValue("medical_id", id)
            rpt.SetParameterValue("medical_name", name)
            rpt.SetParameterValue("medical_diagnostic", diagnostic)
            rpt.SetParameterValue("medical_blood_pressure", bp)
            rpt.SetParameterValue("medical_temperature", temp)
            rpt.SetParameterValue("medical_weight", weight)
            rpt.SetParameterValue("medical_height", height)
            rpt.SetParameterValue("medical_date_recorded", dateRecorded)
            rpt.SetParameterValue("medical_doctor_name", doctor)
            rpt.SetParameterValue("medical_intake", intake)

            CrystalReportViewer1.ReportSource = rpt
            Grid_Views_False()
            donation_buttons_false()
        End If
    End Sub
    Private Sub LblReportDonations_Click(sender As Object, e As EventArgs) Handles LblReportDonations.Click
        report_button_false()
        Grid_Views_False()
        donation_buttons_enable()
        Date_filter_false()
        cmbMonth.Items.Clear()
        cmbYear.Items.Clear()

        Dim items() As String = {"2023", "2024", "2025", "All Years"}
        cmbYear.Items.AddRange(items)
        Dim items2() As String = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", "All Months"}
        cmbMonth.Items.AddRange(items2)

        cmbYear.Text = "All Years"
        cmbMonth.Text = "All Months"

        DataGridView3.Visible = True
        btnReportAll.Visible = True
        donation = True
        all = True

        DataGridView3.ScrollBars = ScrollBars.Vertical

        ' Set the default cell style for each column
        For Each col As DataGridViewColumn In DataGridView3.Columns
            col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
        Next

        ' Change the font size of column headers
        DataGridView3.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Change the font size of cell values
        DataGridView3.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Set the CellBorderStyle property of the GunaDataGridView control
        DataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.Single

        ' Set the GridColor property of the GunaDataGridView control
        DataGridView3.GridColor = Color.Black

        Try
            Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
            myConnection.Open()
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
                                                             "FROM donations", myConnection)


            Dim table As New DataTable()
            dataAdapterDonations.Fill(table)

            Dim buttonColumn As New DataGridViewButtonColumn()
            buttonColumn.Name = "SelectButton"
            buttonColumn.HeaderText = "Report"
            buttonColumn.Text = "Print Report"
            buttonColumn.UseColumnTextForButtonValue = True

            Dim columnExists As Boolean = False
            For Each column As DataGridViewColumn In DataGridView3.Columns
                If column.Name = buttonColumn.Name Then
                    columnExists = True
                    Exit For
                End If
            Next

            ' Add the column if it doesn't exist
            If Not columnExists Then
                DataGridView3.Columns.Add(buttonColumn)
            End If

            DataGridView3.DataSource = table
            AddHandler DataGridView3.CellContentClick, AddressOf DataGridView3_CellContentClick
            CrystalReportViewer1.ReportSource = Nothing
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading the data: " + ex.Message)
        End Try
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        ' Check if the clicked cell is in the "SelectButton" column
        If e.ColumnIndex = DataGridView3.Columns("SelectButton").Index AndAlso e.RowIndex >= 0 Then
            ' Retrieve the selected row and its data
            Dim selectedRow As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
            Dim id As Integer = CInt(selectedRow.Cells("DonationId").Value)
            Dim sponsorName As String = CStr(selectedRow.Cells("SponsorName").Value)
            Dim gender As String = CStr(selectedRow.Cells("Gender").Value)
            Dim sponsorAddress As String = CStr(selectedRow.Cells("SponsorAddress").Value)
            Dim phoneNumber As String = CStr(selectedRow.Cells("PhoneNumber").Value)
            Dim dateDonated As String = CStr(selectedRow.Cells("DateDonated").Value)
            Dim DType As String = CStr(selectedRow.Cells("DonationType").Value)
            Dim Itype As String = CStr(selectedRow.Cells("InventoryType").Value)
            Dim quantity As String = CStr(selectedRow.Cells("Quantity").Value)
            Dim cash As String = CStr(selectedRow.Cells("CashAmount").Value)

            ' Perform the desired action with the selected row data
            Dim rpt As New ReportDonation()
            rpt.SetParameterValue("donation_id", id)
            rpt.SetParameterValue("sponsor_name", If(sponsorName IsNot Nothing, sponsorName, String.Empty))
            rpt.SetParameterValue("sponsor_gender", If(gender IsNot Nothing, gender, String.Empty))
            rpt.SetParameterValue("sponsor_address", If(sponsorAddress IsNot Nothing, sponsorAddress, String.Empty))
            rpt.SetParameterValue("donation_phone_number", If(phoneNumber IsNot Nothing, phoneNumber, String.Empty))
            rpt.SetParameterValue("donation_type", If(DType IsNot Nothing, DType, String.Empty))
            rpt.SetParameterValue("inventory_type", If(Itype IsNot Nothing, Itype, String.Empty))
            rpt.SetParameterValue("donation_quantity", If(quantity IsNot Nothing, quantity, String.Empty))
            rpt.SetParameterValue("cash_amount", If(cash IsNot Nothing, cash, String.Empty))
            rpt.SetParameterValue("donation_date_donated", If(dateDonated IsNot Nothing, dateDonated, String.Empty))

            CrystalReportViewer1.ReportSource = rpt
            Grid_Views_False()
            donation_buttons_false()

        End If
    End Sub
    Private Sub LblReportPayroll_Click(sender As Object, e As EventArgs) Handles LblReportPayroll.Click
        Grid_Views_False()
        report_button_false()
        donation_buttons_false()
        Date_filter_false()
        cmbMonth.Items.Clear()
        cmbYear.Items.Clear()
        payroll = True
        DataGridView4.Visible = True
        btnReportAll.Visible = True
        btnSearch.Visible = True
        txtSearch.Visible = True

        DataGridView4.ScrollBars = ScrollBars.Vertical

        ' Set the default cell style for each column
        For Each col As DataGridViewColumn In DataGridView4.Columns
            col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
        Next

        ' Change the font size of column headerssss
        DataGridView4.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Change the font size of cell values
        DataGridView4.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Set the CellBorderStyle property of the GunaDataGridView control
        DataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.Single

        ' Set the GridColor property of the GunaDataGridView control
        DataGridView4.GridColor = Color.Black

        Try
            Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
            myConnection.Open()

            Dim dataAdapterPayroll As New MySqlDataAdapter("SELECT payroll_id as 'ID', " &
                                                      "user_id AS 'EmployeeName', " &
                                                      "basic_salary AS 'DailyWage', " &
                                                      "number_of_days AS 'NoOfDays', " &
                                                      "total_salary AS 'Salary', " &
                                                      "payroll_created_at AS 'CreatedAt', " &
                                                      "payroll_updated_at AS 'UpdatedAt' " &
                                                      "FROM payroll", myConnection)

            Dim table As New DataTable()
            dataAdapterPayroll.Fill(table)

            Dim buttonColumn As New DataGridViewButtonColumn()
            buttonColumn.Name = "SelectButton"
            buttonColumn.HeaderText = "Report"
            buttonColumn.Text = "Print Report"
            buttonColumn.UseColumnTextForButtonValue = True

            Dim columnExists As Boolean = False
            For Each column As DataGridViewColumn In DataGridView4.Columns
                If column.Name = buttonColumn.Name Then
                    columnExists = True
                    Exit For
                End If
            Next

            ' Add the column if it doesn't exist
            If Not columnExists Then
                DataGridView4.Columns.Add(buttonColumn)
            End If

            DataGridView4.DataSource = table
            AddHandler DataGridView4.CellContentClick, AddressOf DataGridView4_CellContentClick
            CrystalReportViewer1.ReportSource = Nothing
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading the data: " + ex.Message)
        End Try
    End Sub
    Private Sub DataGridView4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        ' Check if the clicked cell is in the "SelectButton" column
        If e.ColumnIndex = DataGridView4.Columns("SelectButton").Index AndAlso e.RowIndex >= 0 Then
            ' Retrieve the selected row and its data
            Dim selectedRow As DataGridViewRow = DataGridView4.Rows(e.RowIndex)
            Dim id As Integer = CInt(selectedRow.Cells("ID").Value)
            Dim name As String = CStr(selectedRow.Cells("EmployeeName").Value)
            Dim wage As String = CStr(selectedRow.Cells("DailyWage").Value)
            Dim days As String = CStr(selectedRow.Cells("NoOfDays").Value)
            Dim salary As String = CStr(selectedRow.Cells("Salary").Value)

            ' Perform the desired action with the selected row data
            Dim rpt As New ReportPayroll()
            rpt.SetParameterValue("payroll_id", id)
            rpt.SetParameterValue("payroll_full_name", name)
            rpt.SetParameterValue("payroll_wage", wage)
            rpt.SetParameterValue("payroll_days", days)
            rpt.SetParameterValue("payroll_salary", salary)

            CrystalReportViewer1.ReportSource = rpt
            Grid_Views_False()
            donation_buttons_false()

        End If
    End Sub
    Private Sub LblReportEmployee_Click(sender As Object, e As EventArgs) Handles LblReportEmployee.Click
        report_button_false()
        Grid_Views_False()
        donation_buttons_false()
        Date_filter_false()
        cmbMonth.Items.Clear()
        cmbYear.Items.Clear()
        employee = True
        DataGridView5.Visible = True
        btnReportAll.Visible = True
        btnSearch.Visible = True
        txtSearch.Visible = True

        DataGridView5.ScrollBars = ScrollBars.Vertical

        ' Set the default cell style for each column
        For Each col As DataGridViewColumn In DataGridView5.Columns
            col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
        Next

        ' Change the font size of column headers
        DataGridView5.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Change the font size of cell values
        DataGridView5.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

        ' Set the CellBorderStyle property of the GunaDataGridView control
        DataGridView5.CellBorderStyle = DataGridViewCellBorderStyle.Single

        ' Set the GridColor property of the GunaDataGridView control
        DataGridView5.GridColor = Color.Black

        Try
            Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
            myConnection.Open()
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
                                                      "employee_created_at AS 'CreatedAt', " &
                                                      "employee_updated_at AS 'UpdatedAt' " &
                                                      "FROM employee", myConnection)


            Dim table As New DataTable()
            dataAdapterDonations.Fill(table)
            Dim buttonColumn As New DataGridViewButtonColumn()
            buttonColumn.Name = "SelectButton"
            buttonColumn.HeaderText = "Report"
            buttonColumn.Text = "Print Report"
            buttonColumn.UseColumnTextForButtonValue = True

            Dim columnExists As Boolean = False
            For Each column As DataGridViewColumn In DataGridView5.Columns
                If column.Name = buttonColumn.Name Then
                    columnExists = True
                    Exit For
                End If
            Next

            ' Add the column if it doesn't exist
            If Not columnExists Then
                DataGridView5.Columns.Add(buttonColumn)
            End If

            DataGridView5.DataSource = table
            AddHandler DataGridView5.CellContentClick, AddressOf DataGridView5_CellContentClick
            CrystalReportViewer1.ReportSource = Nothing
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading the data: " + ex.ToString)
        End Try
    End Sub
    Private Sub DataGridView5_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        ' Check if the clicked cell is in the "SelectButton" column
        If e.ColumnIndex = DataGridView5.Columns("SelectButton").Index AndAlso e.RowIndex >= 0 Then
            ' Retrieve the selected row and its data
            Dim selectedRow As DataGridViewRow = DataGridView5.Rows(e.RowIndex)
            Dim id As Integer = CInt(selectedRow.Cells("EmployeeId").Value)
            Dim fname As String = CStr(selectedRow.Cells("Firstname").Value)
            Dim mname As String = CStr(selectedRow.Cells("Middlename").Value)
            Dim lname As String = CStr(selectedRow.Cells("Lastname").Value)
            Dim birthday As String = CStr(selectedRow.Cells("Birthday").Value)
            Dim gender As String = CStr(selectedRow.Cells("Gender").Value)
            Dim status As String = CStr(selectedRow.Cells("Status").Value)
            Dim religion As String = CStr(selectedRow.Cells("Religion").Value)
            Dim contactnumber As String = CStr(selectedRow.Cells("ContactNumber").Value)
            Dim address As String = CStr(selectedRow.Cells("Address").Value)
            Dim educational As String = CStr(selectedRow.Cells("Educational").Value)
            Dim designation As String = CStr(selectedRow.Cells("Designation").Value)

            ' Perform the desired action with the selected row data
            Dim rpt As New ReportEmployee()
            Dim fullName As String = fname & " " & mname & " " & lname
            rpt.SetParameterValue("employee_id", id)
            rpt.SetParameterValue("employee_full_name", fullName)
            rpt.SetParameterValue("employee_birthday", birthday)
            rpt.SetParameterValue("employee_gender", gender)
            rpt.SetParameterValue("employee_status", status)
            rpt.SetParameterValue("employee_religion", religion)
            rpt.SetParameterValue("employee_contact_number", contactnumber)
            rpt.SetParameterValue("employee_address", address)
            rpt.SetParameterValue("employee_education", educational)
            rpt.SetParameterValue("employee_designation", designation)


            CrystalReportViewer1.ReportSource = rpt
            Grid_Views_False()
            donation_buttons_false()

        End If
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        If cmbMonth.Text = "All Months" And cmbYear.Text = "All Years" Then
            report_button_false()
            Grid_Views_False()
            donation_buttons_enable()
            Date_filter_false()
            cmbMonth.Items.Clear()
            cmbYear.Items.Clear()
            all = True
            donation = True

            Dim items() As String = {"2023", "2024", "2025", "All Years"}
            cmbYear.Items.AddRange(items)
            Dim items2() As String = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", "All Months"}
            cmbMonth.Items.AddRange(items2)

            cmbYear.Text = "All Years"
            cmbMonth.Text = "All Months"

            DataGridView3.Visible = True
            btnReportAll.Visible = True

            DataGridView3.ScrollBars = ScrollBars.Vertical

            ' Set the default cell style for each column
            For Each col As DataGridViewColumn In DataGridView3.Columns
                col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
            Next

            ' Change the font size of column headers
            DataGridView3.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

            ' Change the font size of cell values
            DataGridView3.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

            ' Set the CellBorderStyle property of the GunaDataGridView control
            DataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.Single

            ' Set the GridColor property of the GunaDataGridView control
            DataGridView3.GridColor = Color.Black

            Try
                Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
                myConnection.Open()
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
                                                             "FROM donations", myConnection)


                Dim table As New DataTable()
                dataAdapterDonations.Fill(table)

                Dim buttonColumn As New DataGridViewButtonColumn()
                buttonColumn.Name = "SelectButton"
                buttonColumn.HeaderText = "Report"
                buttonColumn.Text = "Print Report"
                buttonColumn.UseColumnTextForButtonValue = True

                Dim columnExists As Boolean = False
                For Each column As DataGridViewColumn In DataGridView3.Columns
                    If column.Name = buttonColumn.Name Then
                        columnExists = True
                        Exit For
                    End If
                Next

                ' Add the column if it doesn't exist
                If Not columnExists Then
                    DataGridView3.Columns.Add(buttonColumn)
                End If

                DataGridView3.DataSource = table
                AddHandler DataGridView3.CellContentClick, AddressOf DataGridView3_CellContentClick
                CrystalReportViewer1.ReportSource = Nothing
            Catch ex As Exception
                MessageBox.Show("An error occurred while loading the data: " + ex.Message)
            End Try
        ElseIf cmbMonth.Text = "All Months" Then
            report_button_false()
            Grid_Views_False()
            donation_buttons_enable()
            Date_filter_false()
            DataGridView3.Visible = True
            btnReportAll.Visible = True
            DataGridView3.ScrollBars = ScrollBars.Vertical
            donation = True

            ' Set the default cell style for each column
            For Each col As DataGridViewColumn In DataGridView3.Columns
                col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
            Next

            ' Change the font size of column headers
            DataGridView3.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

            ' Change the font size of cell values
            DataGridView3.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

            ' Set the CellBorderStyle property of the GunaDataGridView control
            DataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.Single

            ' Set the GridColor property of the GunaDataGridView control
            DataGridView3.GridColor = Color.Black

            Try
                Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
                myConnection.Open()
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
                                                                 "FROM donations", myConnection)


                Dim table As New DataTable()
                dataAdapterDonations.Fill(table)
                Dim filteredTable As New DataTable()
                filteredTable = table.Clone()

                If cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedYear = 2023
                    specificYear = "23"
                    year = True
                ElseIf cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedYear = 2024
                    year = True
                    specificYear = "24"
                ElseIf cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedYear = 2025
                    year = True
                    specificYear = "25"
                End If


                For Each row As DataRow In table.Rows
                    Dim dateDonated As Date = CDate(row("DateDonated"))
                    If dateDonated.Year = selectedYear Then
                        filteredTable.ImportRow(row)
                    End If
                Next

                Dim buttonColumn As New DataGridViewButtonColumn()
                buttonColumn.Name = "SelectButton"
                buttonColumn.HeaderText = "Report"
                buttonColumn.Text = "Print Report"
                buttonColumn.UseColumnTextForButtonValue = True

                Dim columnExists As Boolean = False
                For Each column As DataGridViewColumn In DataGridView3.Columns
                    If column.Name = buttonColumn.Name Then
                        columnExists = True
                        Exit For
                    End If
                Next

                ' Add the column if it doesn't exist
                If Not columnExists Then
                    DataGridView3.Columns.Add(buttonColumn)
                End If

                DataGridView3.DataSource = filteredTable
                AddHandler DataGridView3.CellContentClick, AddressOf DataGridView3_CellContentClick
                CrystalReportViewer1.ReportSource = Nothing
            Catch ex As Exception
                MessageBox.Show("An error occurred while loading the data: " + ex.Message)
            End Try
        ElseIf cmbYear.Text = "All Years" Then
            report_button_false()
            Grid_Views_False()
            donation_buttons_enable()
            Date_filter_false()
            donation = True
            DataGridView3.Visible = True
            btnReportAll.Visible = True


            DataGridView3.ScrollBars = ScrollBars.Vertical
            ' Set the default cell style for each column
            For Each col As DataGridViewColumn In DataGridView3.Columns
                col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
            Next

            ' Change the font size of column headers
            DataGridView3.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

            ' Change the font size of cell values
            DataGridView3.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

            ' Set the CellBorderStyle property of the GunaDataGridView control
            DataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.Single

            ' Set the GridColor property of the GunaDataGridView control
            DataGridView3.GridColor = Color.Black

            Try
                Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
                myConnection.Open()
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
                                                                     "FROM donations", myConnection)


                Dim table As New DataTable()
                dataAdapterDonations.Fill(table)
                Dim filteredTable As New DataTable()
                filteredTable = table.Clone()

                ' Filter the data based on the month
                If cmbMonth.Text = "January" Then
                    Date_filter_false()
                    selectedMonth = 1 ' January
                    jan = True
                    month = True
                ElseIf cmbMonth.Text = "February" Then
                    Date_filter_false()
                    selectedMonth = 2 ' February
                    feb = True
                    month = True
                ElseIf cmbMonth.Text = "March" Then
                    Date_filter_false()
                    selectedMonth = 3 ' March
                    mar = True
                    month = True
                ElseIf cmbMonth.Text = "April" Then
                    Date_filter_false()
                    selectedMonth = 4 ' April
                    ap = True
                    month = True
                ElseIf cmbMonth.Text = "May" Then
                    Date_filter_false()
                    selectedMonth = 5 ' May
                    may = True
                    month = True
                ElseIf cmbMonth.Text = "June" Then
                    Date_filter_false()
                    selectedMonth = 6 ' June
                    jun = True
                    month = True
                ElseIf cmbMonth.Text = "July" Then
                    Date_filter_false()
                    selectedMonth = 7 ' July
                    jul = True
                    month = True
                ElseIf cmbMonth.Text = "August" Then
                    Date_filter_false()
                    selectedMonth = 8 ' August
                    aug = True
                    month = True
                ElseIf cmbMonth.Text = "September" Then
                    Date_filter_false()
                    selectedMonth = 9 ' September
                    sep = True
                    month = True
                ElseIf cmbMonth.Text = "October" Then
                    Date_filter_false()
                    selectedMonth = 10 ' October
                    oct = True
                    month = True
                ElseIf cmbMonth.Text = "November" Then
                    Date_filter_false()
                    selectedMonth = 11 ' November
                    nov = True
                    month = True
                ElseIf cmbMonth.Text = "December" Then
                    Date_filter_false()
                    selectedMonth = 12 ' December
                    dec = True
                    month = True
                End If

                For Each row As DataRow In table.Rows
                    Dim dateDonated As Date = CDate(row("DateDonated"))
                    If dateDonated.Month = selectedMonth Then
                        filteredTable.ImportRow(row)
                    End If
                Next

                Dim buttonColumn As New DataGridViewButtonColumn()
                buttonColumn.Name = "SelectButton"
                buttonColumn.HeaderText = "Report"
                buttonColumn.Text = "Print Report"
                buttonColumn.UseColumnTextForButtonValue = True

                Dim columnExists As Boolean = False
                For Each column As DataGridViewColumn In DataGridView3.Columns
                    If column.Name = buttonColumn.Name Then
                        columnExists = True
                        Exit For
                    End If
                Next

                ' Add the column if it doesn't exist
                If Not columnExists Then
                    DataGridView3.Columns.Add(buttonColumn)
                End If

                DataGridView3.DataSource = filteredTable
                AddHandler DataGridView3.CellContentClick, AddressOf DataGridView3_CellContentClick
                CrystalReportViewer1.ReportSource = Nothing
            Catch ex As Exception
                MessageBox.Show("An error occurred while loading the data: " + ex.Message)
            End Try
        Else
            report_button_false()
            Grid_Views_False()
            donation_buttons_enable()
            Date_filter_false()
            DataGridView3.Visible = True
            btnReportAll.Visible = True
            DataGridView3.ScrollBars = ScrollBars.Vertical
            donation = True
            ' Set the default cell style for each column
            For Each col As DataGridViewColumn In DataGridView3.Columns
                col.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)
            Next

            ' Change the font size of column headers
            DataGridView3.ColumnHeadersDefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

            ' Change the font size of cell values
            DataGridView3.DefaultCellStyle.Font = New Font("Century Gothic", 12, FontStyle.Bold)

            ' Set the CellBorderStyle property of the GunaDataGridView control
            DataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.Single

            ' Set the GridColor property of the GunaDataGridView control
            DataGridView3.GridColor = Color.Black

            Try
                Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
                myConnection.Open()
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
                                                                     "FROM donations", myConnection)


                Dim table As New DataTable()
                dataAdapterDonations.Fill(table)
                Dim filteredTable As New DataTable()
                filteredTable = table.Clone()

                ' Filter the data based on the month
                If cmbMonth.Text = "January" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 1 ' January
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    jan = True
                ElseIf cmbMonth.Text = "February" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 2 ' February
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    feb = True
                ElseIf cmbMonth.Text = "March" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 3 ' March
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    mar = True
                ElseIf cmbMonth.Text = "April" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 4 ' April
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    ap = True
                ElseIf cmbMonth.Text = "May" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 5 ' May
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    may = True
                ElseIf cmbMonth.Text = "June" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 6 ' June
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    jun = True
                ElseIf cmbMonth.Text = "July" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 7 ' July
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    jul = True
                ElseIf cmbMonth.Text = "August" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 8 ' August
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    aug = True
                ElseIf cmbMonth.Text = "September" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 9 ' September
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    sep = True
                ElseIf cmbMonth.Text = "October" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 10 ' October
                    selectedYear = 2023
                    specific = True
                    specificYear = "23"
                    oct = True
                ElseIf cmbMonth.Text = "November" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 11 ' November
                    selectedYear = 2023
                    specificYear = "23"
                    specific = True
                    nov = True
                ElseIf cmbMonth.Text = "December" And cmbYear.Text = "2023" Then
                    Date_filter_false()
                    selectedMonth = 12 ' December
                    selectedYear = 2023
                    specificYear = "23"
                    specific = True
                    dec = True
                ElseIf cmbMonth.Text = "January" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 1 ' January
                    selectedYear = 2024
                    specificYear = "24"
                    specific = True
                    jan = True
                ElseIf cmbMonth.Text = "February" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 2 ' February
                    selectedYear = 2024
                    specificYear = "24"
                    specific = True
                    feb = True
                ElseIf cmbMonth.Text = "March" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 3 ' March
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    mar = True
                ElseIf cmbMonth.Text = "April" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 4 ' April
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    ap = True
                ElseIf cmbMonth.Text = "May" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 5 ' May
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    may = True
                ElseIf cmbMonth.Text = "June" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 6 ' June
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    jun = True
                ElseIf cmbMonth.Text = "July" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 7 ' July
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    jul = True
                ElseIf cmbMonth.Text = "August" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 8 ' August
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    aug = True
                ElseIf cmbMonth.Text = "September" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 9 ' September
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    sep = True
                ElseIf cmbMonth.Text = "October" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 10 ' October
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    oct = True
                ElseIf cmbMonth.Text = "November" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 11 ' November
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    nov = True
                ElseIf cmbMonth.Text = "December" And cmbYear.Text = "2024" Then
                    Date_filter_false()
                    selectedMonth = 12 ' December
                    selectedYear = 2024
                    specific = True
                    specificYear = "24"
                    dec = True
                ElseIf cmbMonth.Text = "January" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 1 ' January
                    specific = True
                    selectedYear = 2025
                    specificYear = "25"
                    jan = True
                ElseIf cmbMonth.Text = "February" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 2 ' February
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    feb = True
                ElseIf cmbMonth.Text = "March" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 3 ' March
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    mar = True
                ElseIf cmbMonth.Text = "April" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 4 ' April
                    specific = True
                    selectedYear = 2025
                    specificYear = "25"
                    ap = True
                ElseIf cmbMonth.Text = "May" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 5 ' May
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    may = True
                ElseIf cmbMonth.Text = "June" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 6 ' June
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    jun = True
                ElseIf cmbMonth.Text = "July" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 7 ' July
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    jul = True
                ElseIf cmbMonth.Text = "August" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 8 ' August
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    aug = True
                ElseIf cmbMonth.Text = "September" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 9 ' September
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    sep = True
                ElseIf cmbMonth.Text = "October" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 10 ' October
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    oct = True
                ElseIf cmbMonth.Text = "November" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 11 ' November
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    nov = True
                ElseIf cmbMonth.Text = "December" And cmbYear.Text = "2025" Then
                    Date_filter_false()
                    selectedMonth = 12 ' December
                    selectedYear = 2025
                    specific = True
                    specificYear = "25"
                    dec = True
                End If

                Dim dateFormat As String = "dd/MM/yy"
                For Each row As DataRow In table.Rows
                    Dim dateDonatedString As String = row("DateDonated").ToString()
                    Dim dateDonated As Date
                    If Date.TryParseExact(dateDonatedString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, dateDonated) Then
                        If dateDonated.Year = selectedYear AndAlso dateDonated.Month = selectedMonth Then
                            filteredTable.ImportRow(row)
                        End If
                    End If
                Next

                Dim buttonColumn As New DataGridViewButtonColumn()
                buttonColumn.Name = "SelectButton"
                buttonColumn.HeaderText = "Report"
                buttonColumn.Text = "Print Report"
                buttonColumn.UseColumnTextForButtonValue = True

                Dim columnExists As Boolean = False
                For Each column As DataGridViewColumn In DataGridView3.Columns
                    If column.Name = buttonColumn.Name Then
                        columnExists = True
                        Exit For
                    End If
                Next

                ' Add the column if it doesn't exist
                If Not columnExists Then
                    DataGridView3.Columns.Add(buttonColumn)
                End If

                DataGridView3.DataSource = filteredTable
                AddHandler DataGridView3.CellContentClick, AddressOf DataGridView3_CellContentClick
                CrystalReportViewer1.ReportSource = Nothing
            Catch ex As Exception
                MessageBox.Show("An error occurred while loading the data: " + ex.Message)
            End Try
        End If

    End Sub

    Private Sub btnReportAll_Click(sender As Object, e As EventArgs) Handles btnReportAll.Click
        Dim myConnection As New MySqlConnection("server=localhost;port=3306;database=integrated_management_system;username=root;password=")
        If orphan = True Then
            Dim rpt As New ReportOrphanAll
            ds = New DataSet
            dtb = ds.Tables
            da = New MySqlDataAdapter("SELECT * FROM patients order by patient_id", myConnection)
            da.Fill(ds, "patients")
            Dim view As New DataView(dtb(0))
            rpt.SetDataSource(ds.Tables(0))
            CrystalReportViewer1.ReportSource = rpt
            orphan = False

        ElseIf employee = True Then
            Dim rpt As New ReportEmployeeAll
            ds = New DataSet
            dtb = ds.Tables
            da = New MySqlDataAdapter("SELECT * FROM employee order by employee_id", myConnection)
            da.Fill(ds, "employee")
            Dim view As New DataView(dtb(0))
            rpt.SetDataSource(ds.Tables(0))
            CrystalReportViewer1.ReportSource = rpt
            employee = False

        ElseIf donation = True Then
            If all = True Then
                Dim rpt As New ReportDonationAll
                ds = New DataSet
                dtb = ds.Tables
                da = New MySqlDataAdapter("SELECT * FROM donations order by donation_id", myConnection)
                da.Fill(ds, "donations")
                Dim view As New DataView(dtb(0))
                rpt.SetDataSource(ds.Tables(0))
                CrystalReportViewer1.ReportSource = rpt
                all = False
                donation = False
            End If
            If month = True Then
                If jan = True Then
                    Dim rpt As New ReportDonationAllJan
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 1 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    jan = False
                    donation = False
                    month = False

                ElseIf feb = True Then
                    Dim rpt As New ReportDonationAllFeb
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 2 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    feb = False
                    donation = False
                    month = False

                ElseIf mar = True Then
                    Dim rpt As New ReportDonationAllMarch
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 3 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    mar = False
                    donation = False
                    month = False

                ElseIf ap = True Then
                    Dim rpt As New ReportDonationAllApril
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 4 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    ap = False
                    donation = False
                    month = False

                ElseIf may = True Then
                    Dim rpt As New ReportDonationAllMay
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 5 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    may = False
                    donation = False
                    month = False

                ElseIf jun = True Then
                    Dim rpt As New ReportDonationAllJun
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 6 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    jun = False
                    donation = False
                    month = False

                ElseIf jul = True Then
                    Dim rpt As New ReportDonationAllJul
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 7 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    jul = False
                    donation = False
                    month = False

                ElseIf aug = True Then
                    Dim rpt As New ReportDonationAllAug
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 8 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    aug = False
                    donation = False
                    month = False

                ElseIf sep = True Then
                    Dim rpt As New ReportDonationAllSep
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 9 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    sep = False
                    donation = False
                    month = False

                ElseIf oct = True Then
                    Dim rpt As New ReportDonationAllOct
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 10 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    oct = False
                    donation = False
                    month = False

                ElseIf nov = True Then
                    Dim rpt As New ReportDonationAllNov
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 11 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    nov = False
                    donation = False
                    month = False

                ElseIf dec = True Then
                    Dim rpt As New ReportDonationAllDec
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) = 12 ORDER BY donation_id", myConnection)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    dec = False
                    donation = False
                    month = False
                End If
            End If
            If year = True Then
                Dim rpt As New ReportDonationAll
                ds = New DataSet
                dtb = ds.Tables
                da = New MySqlDataAdapter("SELECT * FROM donations WHERE YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                da.Fill(ds, "donations")
                Dim view As New DataView(dtb(0))
                rpt.SetDataSource(ds.Tables(0))
                CrystalReportViewer1.ReportSource = rpt
                year = False
                donation = False
                Grid_Views_False()
                donation_buttons_false()
            End If
            If specific = True Then
                If jan = True Then
                    Dim rpt As New ReportDonationAllJan
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    jan = False
                    donation = False
                    specific = False

                ElseIf feb = True Then
                    Dim rpt As New ReportDonationAllFeb
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    feb = False
                    donation = False
                    specific = False

                ElseIf mar = True Then
                    Dim rpt As New ReportDonationAllMarch
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    mar = False
                    donation = False
                    specific = False

                ElseIf ap = True Then
                    Dim rpt As New ReportDonationAllApril
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    ap = False
                    donation = False
                    specific = False

                ElseIf may = True Then
                    Dim rpt As New ReportDonationAllMay
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    may = False
                    donation = False
                    specific = False

                ElseIf jun = True Then
                    Dim rpt As New ReportDonationAllJun
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    jun = False
                    donation = False
                    specific = False

                ElseIf jul = True Then
                    Dim rpt As New ReportDonationAllJul
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    jul = False
                    donation = False
                    specific = False

                ElseIf aug = True Then
                    Dim rpt As New ReportDonationAllAug
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    aug = False
                    donation = False
                    specific = False

                ElseIf sep = True Then
                    Dim rpt As New ReportDonationAllSep
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    sep = False
                    donation = False
                    specific = False

                ElseIf oct = True Then
                    Dim rpt As New ReportDonationAllOct
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    oct = False
                    donation = False
                    specific = False

                ElseIf nov = True Then
                    Dim rpt As New ReportDonationAllNov
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    nov = False
                    donation = False
                    specific = False

                ElseIf dec = True Then
                    Dim rpt As New ReportDonationAllDec
                    ds = New DataSet
                    dtb = ds.Tables
                    da = New MySqlDataAdapter("SELECT * FROM donations WHERE MONTH(donation_date_donated) =  " + selectedMonth.ToString + " and YEAR(STR_TO_DATE(donation_date_donated, '%d/%m/%y')) = 20" + specificYear + " ORDER BY donation_id", myConnection)
                    da.SelectCommand.Parameters.AddWithValue("@specificYear", specificYear)
                    da.Fill(ds, "donations")
                    Dim view As New DataView(dtb(0))
                    rpt.SetDataSource(ds.Tables(0))
                    CrystalReportViewer1.ReportSource = rpt
                    Grid_Views_False()
                    donation_buttons_false()
                    dec = False
                    donation = False
                    specific = False
                End If
            End If
        ElseIf medical = True Then
            Dim rpt As New ReportMedicalHistoryAll
            ds = New DataSet
            dtb = ds.Tables
            da = New MySqlDataAdapter("SELECT * FROM medical_history order by medical_id", myConnection)
            da.Fill(ds, "medical_history")
            Dim view As New DataView(dtb(0))
            rpt.SetDataSource(ds.Tables(0))
            CrystalReportViewer1.ReportSource = rpt
            medical = False

        ElseIf payroll = True Then
            Dim rpt As New ReportPayrollAll
            ds = New DataSet
            dtb = ds.Tables
            da = New MySqlDataAdapter("SELECT * FROM payroll order by payroll_id", myConnection)
            da.Fill(ds, "payroll")
            Dim view As New DataView(dtb(0))
            rpt.SetDataSource(ds.Tables(0))
            CrystalReportViewer1.ReportSource = rpt
            payroll = False

        End If
        Grid_Views_False()
        donation_buttons_false()

    End Sub
End Class