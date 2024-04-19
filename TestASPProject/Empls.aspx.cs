using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestASPProject
{
    public partial class Empls : System.Web.UI.Page
    {
        private const string connectionString = "Data Source=RADON;Initial Catalog=ASProject;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            var masterPage = this.Master as SiteMaster;

            if (Session["IsAuthenticated"] == null || !(bool)Session["IsAuthenticated"])
            {
                Response.Redirect("~/Home.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int currentUserId;
                if (!int.TryParse(Session["UserId"]?.ToString(), out currentUserId))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                int requestedUserId;
                if (!int.TryParse(Request.QueryString["UserId"], out requestedUserId))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                if (currentUserId != requestedUserId)
                {
                    Response.Redirect("~/Home.aspx");
                    return;
                }

                if (masterPage != null)
                {
                    var loginButton = masterPage.FindControl("LoginButton") as Button;
                    if (loginButton != null)
                    {
                        loginButton.Enabled = false;
                        loginButton.Visible = false;
                        Session["IsQr"] = false;
                        Session["IsEmail"] = false;
                        
                    }
                }
                LoadEmployees();
            }
        }



        protected void btnGenerateEmploee_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text;
            string about = txtAbout.Text;

            int userId = Convert.ToInt32(Session["UserId"]);

            string imageUrl = "";
            if (fileUpload.HasFile)
            {
                imageUrl = UploadImage();
            }
            else
            {
                imageUrl = "/Images/profil.jpg"; 
            }

            int newEmployeeId = GetNextEmployeeId();

            SaveEmployee(userId, fullName, about, imageUrl, newEmployeeId);

            ClearEmployeeForm();

            LoadEmployees();
        }

        private int GetNextEmployeeId()
        {
            int nextEmployeeId = 1;

            string query = "SELECT MAX(EmployeeId) AS MaxEmployeeId FROM Employeers";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        nextEmployeeId = Convert.ToInt32(result) + 1;
                    }
                }
            }

            return nextEmployeeId;
        }

        private string UploadImage()
        {
            if (fileUpload.HasFile)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName);
                string imagePath = Server.MapPath("/Images/") + fileName;
                fileUpload.SaveAs(imagePath);
                return "/Images/" + fileName;
            }
            return null; 
        }

        private string UpdUploadImage()
        {
            if (fileUpdUpload.HasFile)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUpdUpload.FileName);
                string imagePath = Server.MapPath("/Images/") + fileName;
                fileUpdUpload.SaveAs(imagePath);
                return "/Images/" + fileName;
            }
            return null; 
        }

        protected void LoadEmployees()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            string query = "SELECT * FROM Employeers WHERE UserId = @UserId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Получение данных о сотруднике
                            string fullName = reader["FullName"].ToString();
                            string about = reader["About"].ToString();
                            int employeeId = Convert.ToInt32(reader["EmployeeId"]);
                            string imageUrl = reader["ImgUrl"].ToString(); // Получение ссылки на изображение

                            // Создание карточки с данными о сотруднике
                            CreateEmployeeCard(fullName, about, employeeId, imageUrl);
                        }
                    }
                }
            }
        }

        protected void CreateEmployeeCard(string fullName, string about, int employeeId, string imageUrl)
        {
            var cardContainer = new Panel();
            cardContainer.CssClass = "card card-content";

            var cardHeader = new Panel();
            cardHeader.CssClass = "card-header";
            var headerContent = $"<div class='d-flex' style='padding: 10px 0 0 14px; text-align: left;'>" +
                                 $"<div style='display:flex; align-items:center'>" +
                                 $"<div><img src='{imageUrl}' class='img2' style='width: 50px; height: 50px; border-radius: 100%;' /></div>" +
                                 $"<div style='margin-left: 10px;'><div style='font-size: 18px; font-weight: 600;'>{fullName}</div></div></div>" +
                                 $"<div style='margin-left: 10px;'><div style='font-size: 18px; font-weight: 600;'>ID сотрудника: {employeeId}</div></div>" +
                                 $"</div>";
            cardHeader.Controls.Add(new LiteralControl(headerContent));

            var cardBody = new Panel();
            cardBody.CssClass = "card-body";
            cardBody.Controls.Add(new LiteralControl(
                $"<div style='margin-left: 25px; margin-bottom: 20px; text-align: left;'>{about}" +
                $"<div style='margin-top:10px'>" +
                $"<button type='button' class='btn btn-warning' data-toggle='modal' data-target='#updateEmployeeModal' data-employee-id='{employeeId}'>Изменить</button></div>" +
                $"</div>"
                ));

            cardContainer.Controls.Add(cardHeader);
            cardContainer.Controls.Add(cardBody);

            Div.Controls.Add(cardContainer);
        }


        protected void SaveEmployee(int userId, string fullName, string about, string imageUrl, int employeeId)
        {
            string query = "INSERT INTO Employeers (UserId, FullName, About, ImgUrl, EmployeeId) " +
                "VALUES (@UserId, @FullName, @About, @ImageUrl, @EmployeeId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@About", about);
                    command.Parameters.AddWithValue("@ImageUrl", imageUrl);
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    command.ExecuteNonQuery();
                }
            }
        }

        protected void ClearEmployeeForm()
        {
            txtFullName.Text = "";
            txtAbout.Text = "";
        }

        protected void DeleteEmploee_Click(object sender, EventArgs e)
        {
            int employeeId = Convert.ToInt32(txtIDemployee.Text);
            DeleteEmployee(employeeId);
            txtIDemployee.Text = "";
            LoadEmployees();
        }

        protected void DeleteEmployee(int employeeId)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            string query = "DELETE FROM Employeers WHERE EmployeeId = @EmployeeId AND UserId = @UserId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void btnUpdateEmploee_Click(object sender, EventArgs e)
        {
            int employeeId = Convert.ToInt32(hdnEmployeeId.Value);
            string fullName = TextBoxUpd.Text;
            string about = TextBoxUpd2.Text;
            string imageUrl = "";
            if (fileUpdUpload.HasFile)
            {
                imageUrl = UpdUploadImage(); 
            }
            else
            {
                imageUrl = "/Images/profil.jpg";
            }

            UpdateEmployee(employeeId, fullName, about, imageUrl);

            LoadEmployees();
        }


        protected void UpdateEmployee(int employeeId, string fullName, string about, string imageUrl)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            string query = "UPDATE Employeers SET FullName = @FullName, About = @About, ImgUrl = @ImageUrl WHERE EmployeeId = @EmployeeId AND UserId = @UserId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@About", about);
                    command.Parameters.AddWithValue("@ImageUrl", imageUrl);
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
