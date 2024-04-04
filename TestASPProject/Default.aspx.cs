using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace TestASPProject
{
    public partial class Default : Page
    {
        private const string connectionString = "Data Source=RADON;Initial Catalog=ASProject;Integrated Security=True;";


        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
                BEGIN
                    CREATE TABLE Users (
                        Id INT PRIMARY KEY IDENTITY,
                        Username NVARCHAR(50),
                        Password NVARCHAR(50)
                    )
                END";


                using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string confirmPassword = txtPasswordVF.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
                {
                    errorLabel.ForeColor = Color.Red;
                    errorLabel.Text = "*Введите имя пользователя и пароль.";
                    return;
                }

                if (password != confirmPassword)
                {
                    errorLabel.ForeColor = Color.Red;
                    errorLabel.Text = "*Пароли не совпадают.";
                    return;
                }

                // Проверка наличия пользователя с таким же именем
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                using (SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection))
                {
                    checkUserCommand.Parameters.AddWithValue("@username", username);
                    int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());

                    if (userCount > 0)
                    {
                        errorLabel.ForeColor = Color.Red;
                        errorLabel.Text = "*Пользователь с таким именем уже существует.";
                        return;
                    }
                }

                if (Regex.IsMatch(username, @"\s+"))
                {
                    errorLabel.ForeColor = Color.Red;
                    errorLabel.Text = "*Имя пользователя не должно содержать пробелов";
                    return;
                }

                string insertUserQuery = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                using (SqlCommand command = new SqlCommand(insertUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    try
                    {
                        command.ExecuteNonQuery();
                        errorLabel.ForeColor = Color.Green;
                        errorLabel.Text = "*Регистрация прошла успешно.";
                        Response.Redirect("Auth.aspx");

                    }
                    catch (SqlException ex)
                    {
                        errorLabel.ForeColor = Color.Red;
                        errorLabel.Text = "Ошибка регистрации: " + ex.Message;
                    }
                }
            }
        }
    }
}