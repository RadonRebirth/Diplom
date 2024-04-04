using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestASPProject
{
    public partial class Tasks : System.Web.UI.Page
    {
        private const string connectionString = "Data Source=RADON;Initial Catalog=ASProject;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAuthenticated"] == null || !(bool)Session["IsAuthenticated"])
            {
                Response.Redirect("~/Home.aspx");
                return;
            }
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

            if (!IsPostBack)
            {
                LoadTasks();
            }
        }

        protected void LoadTasks()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            string query = "SELECT t.*, e.FullName, e.ImgUrl FROM Tasks t LEFT JOIN Employeers e ON t.EmployeeId = e.EmployeeId WHERE t.UserId = @UserId";
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
                            string taskName = reader["TaskName"].ToString();
                            string taskDescription = reader["TaskDescription"].ToString();
                            int taskId = Convert.ToInt32(reader["TaskId"]);
                            string employeeFullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : null;
                            string employeeImageUrl = reader["ImgUrl"] != DBNull.Value ? reader["ImgUrl"].ToString() : null;

                            CreateTaskCard(taskName, taskDescription, taskId, employeeFullName, employeeImageUrl);
                        }
                    }
                }
            }
        }

        protected void CreateTaskCard(string taskName, string taskDescription, int taskId, string employeeFullName = null, string employeeImageUrl = null)
        {
            var cardContainer = new Panel();
            cardContainer.CssClass = "card card-content";

            var cardHeader = new Panel();
            cardHeader.CssClass = "card-header";
            var headerContent = 
                $"<div style='padding: 10px 14px 0 14px; text-align: left;'>" +
                $"<div style='display:flex; align-items:center'>" +
                $"<div style='margin-right:10px'><img src='/Images/todo.svg' class='img2' style='width: 35px;border-radius: 100%;' /></div>" +
                $"<div style='font-size: 18px; font-weight: 600;'>{taskName}</div></div>" +
                $"<div>Описание: {taskDescription}</div>" +
                $"<div style='font-weight: 600;'>ID задачи: {taskId}</div>" +
                $"</div>";
            cardHeader.Controls.Add(new LiteralControl(headerContent));

            var cardBody = new Panel();
            cardBody.CssClass = "card-body";
            var bodyConent = $"<div style='display:flex; margin-left: 10px; text-align: left;'>";

            if(employeeFullName != null && employeeImageUrl != null)
            {
                bodyConent +=
                $"<div style='margin-left:2px;margin-top:10px;'>" +
                $"<div style='font-weight: 600;'>Назначенный сотрудник:</div>" +
                $"<div style='background-color: #C0C0C0;display: flex;flex-wrap:wrap;border-radius: 10px;padding: 10px 10px 10px 10px;align-items: center;'>" +
                $"<div style='margin-right:10px'><img src='{employeeImageUrl}' class='img2' style='width: 40px;height:40px;border-radius: 100%;' /></div>" +
                $"<div style='margin-right:70px;font-weight: 600;'>{employeeFullName}</div>" +
                $"<button type='button' data-toggle='modal' data-target='#delEmplTask' data-task-id='{taskId}' class='btn btn-danger'>X</button>" +
                $"</div>" +
                $"</div>"; 
            }
            bodyConent += "</div>";

            cardBody.Controls.Add(new LiteralControl(bodyConent));
            var cardFooter = new Panel();
            cardFooter.CssClass = "card-footer";
            cardFooter.Controls.Add(new LiteralControl(
                 $"<div style='display:flex; margin-left: 10px; margin-bottom: 15px; text-align: left;'>" +
                $"<div style='margin-right:15px;margin-top:10px'>" +
                $"<button type='button' class='btn btn-warning' data-toggle='modal' data-target='#updateTaskModal' data-task-id='{taskId}'>Изменить</button></div>" +
                $"<div style='margin-top:10px'>" +
                $"<button type='button' class='btn btn-info' data-toggle='modal' data-target='#selectEmplTodoById' data-task-id='{taskId}' >Назначить сотрудника</button></div>" +
                $"</div>"
                ));

            cardContainer.Controls.Add(cardHeader);
            cardContainer.Controls.Add(cardBody);
            cardContainer.Controls.Add(cardFooter);

            Div.Controls.Add(cardContainer);
        }

        protected void btnSelEmplTodoTask_Click(object sender, EventArgs e)
        {
            int taskId = Convert.ToInt32(hdnTaskTodoId.Value);
            int employeeId = Convert.ToInt32(txtIdEmpl.Text);
            AssignEmployeeToTask(taskId, employeeId);
            LoadTasks();
        }


        protected void AssignEmployeeToTask(int taskId, int employeeId)
        {

            int userId = Convert.ToInt32(Session["UserId"]);
            string query = "UPDATE Tasks SET EmployeeId = @EmployeeId WHERE TaskId = @TaskId AND UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    command.Parameters.AddWithValue("@UserId", userId);

                    command.ExecuteNonQuery();
                }
            }
        }

        protected void DeleteAssignedEmplId_Click(object sender, EventArgs e)
        {
            int taskId = Convert.ToInt32(hdnDelEmplTaskId.Value);

            DelAssignedEmplId(taskId);

            LoadTasks();
        }

        protected void DelAssignedEmplId(int taskId)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            string query = "UPDATE Tasks SET EmployeeId = NULL WHERE TaskId = @TaskId AND UserId = @UserId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void SaveTask(int userId, string taskName, string taskDescription)
        {
            string query = "INSERT INTO Tasks (UserId, TaskName, TaskDescription, TaskId) VALUES (@UserId, @TaskName, @TaskDescription, @TaskId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Генерируем новый TaskId
                    int taskId = GenerateNextTaskId();

                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@TaskName", taskName);
                    command.Parameters.AddWithValue("@TaskDescription", taskDescription);
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    command.ExecuteNonQuery();
                }
            }
        }


        protected int GenerateNextTaskId()
        {
            // Получаем максимальный TaskId из базы данных и увеличиваем его на 1
            int nextTaskId;
            string query = "SELECT ISNULL(MAX(TaskId), 0) + 1 FROM Tasks";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    nextTaskId = (int)command.ExecuteScalar();
                }
            }
            return nextTaskId;
        }


        protected void DeleteTask_Click(object sender, EventArgs e)
        {
            int taskId = Convert.ToInt32(txtIdTask.Text);

            DelTask(taskId);

            txtIdTask.Text = "";

            LoadTasks();
        }

        protected void DelTask(int taskId)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            string query = "DELETE FROM Tasks WHERE TaskId = @TaskId AND UserId = @UserId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
        protected void UpdateTask(int taskId, string taskName, string taskDescription)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            string query = "UPDATE Tasks SET TaskName = @TaskName, TaskDescription = @TaskDescription WHERE TaskId = @TaskId AND UserId = @UserId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskName", taskName);
                    command.Parameters.AddWithValue("@TaskDescription", taskDescription);
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void ClearTaskForm()
        {
            txtTaskName.Text = "";
            txtTaskAbout.Text = "";
        }

        protected void btnGenerateTask_Click(object sender, EventArgs e)
        {
            string taskName = txtTaskName.Text;
            string taskDescription = txtTaskAbout.Text;

            int userId = Convert.ToInt32(Session["UserId"]);
            SaveTask(userId, taskName, taskDescription);
            ClearTaskForm();
            LoadTasks();
        }

        protected void btnUpdateTask_Click(object sender, EventArgs e)
        {
            int taskId = Convert.ToInt32(hdnTaskId.Value);
            string taskName = txtNameTaskUpd.Text;
            string taskDescription = txtAboutTaskUpd.Text;

            UpdateTask(taskId, taskName, taskDescription);

            LoadTasks();
        }
    }
}
