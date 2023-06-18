'Imports MySql.Data.MySqlClient

'Public Class LogIn
'    Private connectionString As String = "Server=localhost;Database=integrated_management_system;Uid=root;Pwd=''"
'    Public isExitConfirmed As Boolean = False

'    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
'        Dim isLoginSuccessful As Boolean = CheckLogin(TxtUser.Text, TxtPass.Text)
'        If isLoginSuccessful Then
'            'The entered username and password are correct, allow the user to log in
'            'Me.Hide()

'            Dashboard.Show()
'            TxtUser.Clear()
'            TxtPass.Clear()

'        Else
'            'The entered username and password are incorrect, show an error message
'            MessageBox.Show("Username or password does not match.")
'            TxtUser.Clear()
'            TxtPass.Clear()
'        End If
'    End Sub

'    Private Function CheckLogin(username As String, password As String) As Boolean
'        Using connection As New MySqlConnection(connectionString)
'            connection.Open()

'            Dim command As New MySqlCommand("SELECT COUNT(*) FROM users WHERE user_name = @username AND user_password = @password", connection)
'            command.Parameters.AddWithValue("@username", username)
'            command.Parameters.AddWithValue("@password", password)

'            Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
'            If count > 0 Then
'                Return True
'            Else
'                Return False
'            End If
'        End Using
'    End Function

'    Private Sub LogIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load

'    End Sub
'End Class
Imports System.ComponentModel
Imports MySql.Data.MySqlClient

Public Class LogIn
    Private connectionString As String = "Server=localhost;Database=integrated_management_system;Uid=root;Pwd=''"
    Public isExitConfirmed As Boolean = False
    Public Shared loggedInUserId As Integer = 0 ' Define shared property
    Public Shared loggedInUserName As String = "" ' Define shared property

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim isLoginSuccessful As Boolean = CheckLogin(TxtUser.Text, TxtPass.Text)
        If isLoginSuccessful Then
            'The entered username and password are correct, allow the user to log in
            Me.Hide()

            Dashboard.Show()
            TxtUser.Clear()
            TxtPass.Clear()

        Else
            'The entered username and password are incorrect, show an error message
            MessageBox.Show("Username or password does not match.")
            TxtUser.Clear()
            TxtPass.Clear()
        End If
    End Sub

    Private Function CheckLogin(username As String, password As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim command As New MySqlCommand("SELECT COUNT(*) FROM users WHERE user_name = @username AND user_password = @password", connection)
            command.Parameters.AddWithValue("@username", username)
            command.Parameters.AddWithValue("@password", password)

            Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
            If count > 0 Then
                Dim userIdCommand As New MySqlCommand("SELECT user_id FROM users WHERE user_name = @username AND user_password = @password", connection)
                userIdCommand.Parameters.AddWithValue("@username", username)
                userIdCommand.Parameters.AddWithValue("@password", password)

                Dim userId As Integer = Convert.ToInt32(userIdCommand.ExecuteScalar())
                loggedInUserId = userId ' Store logged in user id in shared property

                Dim userNameCommand As New MySqlCommand("SELECT user_name FROM users WHERE user_id = @userId", connection)
                userNameCommand.Parameters.AddWithValue("@userId", userId)

                loggedInUserName = userNameCommand.ExecuteScalar().ToString() ' Store logged in user name in shared property

                Dim userTypeCommand As New MySqlCommand("SELECT user_id FROM users WHERE user_name = @username AND user_password = @password", connection)
                userTypeCommand.Parameters.AddWithValue("@username", username)
                userTypeCommand.Parameters.AddWithValue("@password", password)

                Dim userType As String = userTypeCommand.ExecuteScalar().ToString()
                If userType = "Admin" Then
                    ' Enable all buttons if user_type is admin
                    Dashboard.btnMedicalHistory.Enabled = True
                    Dashboard.btnSuppliesOfMedicine.Enabled = True
                Else
                    ' Disable Medical History and Medical Supply buttons if user_type is not admin
                    Dashboard.btnMedicalHistory.Enabled = False
                    Dashboard.btnSuppliesOfMedicine.Enabled = False
                End If

                Return True
            Else
                Return False
            End If
        End Using
    End Function



    Private Sub LogIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '  lblForgotPassword.Hide()
        btnLogin.BackColor = Color.FromArgb(128, 128, 255)
        btnExit.BackColor = Color.FromArgb(128, 128, 255)
        btnChangePassword.BackColor = Color.FromArgb(128, 128, 255)


    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        Me.Hide()
        ChangePassword.Show()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to |Exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            isExitConfirmed = True
            Me.Dispose()

        End If
    End Sub

    'Private Sub LogIn_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
    '    If Not isExitConfirmed Then
    '        Dim result As DialogResult = MessageBox.Show("Are you sure you want to go Homepage?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '        If result = DialogResult.No Then
    '            e.Cancel = True
    '        Else
    '            isExitConfirmed = True

    '        End If
    '    End If
    'End Sub


    Private Sub TxtUser_TextChanged(sender As Object, e As EventArgs) Handles TxtUser.TextChanged

    End Sub

    Private Sub Guna2CircleButton1_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton1.Click
        If MsgBox("Do you want to Exit?", vbExclamation + vbYesNo) = vbYes Then
            Me.Close()
        Else
            Return

        End If
    End Sub


    Private Sub forgotpassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblForgotPass.Hide()
    End Sub

    Private Sub Guna2GradientPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2GradientPanel1.Paint

    End Sub

    Private Sub Guna2GradientPanel2_Paint(sender As Object, e As PaintEventArgs) Handles Guna2GradientPanel2.Paint

    End Sub

    Private Sub Guna2GradientPanel2_AutoSizeChanged(sender As Object, e As EventArgs) Handles Guna2GradientPanel2.AutoSizeChanged

    End Sub





    'Private Sub btnGetPassword_Click(sender As Object, e As EventArgs) Handles btnGetPassword.Click
    '    Dim password As String = GetPassword(TxtUser.Text, txtAnswer.Text)
    '    If password <> "" Then
    '        MessageBox.Show("Your password is: " & password)
    '    Else
    '        MessageBox.Show("Username or secret answer is incorrect.")
    '    End If
    'End Sub
End Class


