using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace TestASPProject
{
    public partial class Reg : Page
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

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


        public void ButtonReg_Click(object sender, EventArgs e)
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

                // Проверка пароля на стандарты качественного пароля
                if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
                {
                    errorLabel.ForeColor = Color.Red;
                    errorLabel.Text = "*Пароль должен содержать как минимум одну заглавную букву, одну строчную букву и одну цифру, и быть длиной не менее 8 символов.";
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

                string hashedPassword = HashPassword(password);

                string insertUserQuery = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                using (SqlCommand command = new SqlCommand(insertUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", hashedPassword);
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