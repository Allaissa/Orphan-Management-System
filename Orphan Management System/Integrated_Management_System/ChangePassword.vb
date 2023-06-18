Imports MySql.Data.MySqlClient
Public Class ChangePassword
	Private connectionString As String = "Server=localhost;Database=integrated_management_system;Uid=root;Pwd=''"

	Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
		Using connection As New MySqlConnection(connectionString)
			connection.Open()

			Dim command As New MySqlCommand("UPDATE users SET user_password = @newPassword, user_updated_at = NOW() WHERE user_name = @username", connection)
			command.Parameters.AddWithValue("@username", txtUser.Text)
			command.Parameters.AddWithValue("@oldPassword", txtCurrentPassword.Text)
			command.Parameters.AddWithValue("@newPassword", txtNewPassword.Text)


			Dim rowsAffected As Integer = command.ExecuteNonQuery()
			If rowsAffected > 0 Then
				MessageBox.Show("Password changed successfully.")
			Else
				MessageBox.Show("Failed to change password. Please check the entered information and try again.")
			End If
		End Using
	End Sub
	Private Sub btnLoginMenu_Click(sender As Object, e As EventArgs) Handles btnLoginMenu.Click
        Me.Hide()
        LogIn.Show()
    End Sub

	Private Sub txtCurrentPassword_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentPassword.TextChanged

	End Sub
End Class