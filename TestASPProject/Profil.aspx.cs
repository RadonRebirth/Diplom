using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TestASPProject
{
    public partial class Profil : Page
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
                LoadData();
            }
               
        }

        protected void LoadData()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            string query = "SELECT * FROM UserProfile WHERE UserId = @UserId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fullName = reader["FullName"].ToString();
                            string about = reader["About"].ToString();
                            string profileImg = reader["ProfileImage"].ToString();

                            imgProfile.ImageUrl = profileImg; 
                            lblFullName.Text = fullName;
                            lblAbout.Text = about; 
                        }
                        else
                        {
                            string defaultFullName = "Здесь будет ваше ФИО";
                            string defaultAbout = "Здесь будет ваше описание";
                            string defaultProfileImg = "/Images/user.svg"; 

                            InsertDefaultUserData(userId, defaultFullName, defaultAbout, defaultProfileImg);

                            imgProfile.ImageUrl = defaultProfileImg;
                            lblFullName.Text = defaultFullName;
                            lblAbout.Text = defaultAbout;
                        }
                    }
                }
            }
        }

        protected void InsertDefaultUserData(int userId, string defaultFullName, string defaultAbout, string defaultProfileImg)
        {
            string insertQuery = "INSERT INTO UserProfile (UserId, FullName, About, ProfileImage) VALUES (@UserId, @FullName, @About, @ProfileImage)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@FullName", defaultFullName);
                    command.Parameters.AddWithValue("@About", defaultAbout);
                    command.Parameters.AddWithValue("@ProfileImage", defaultProfileImg);

                    command.ExecuteNonQuery();
                }
            }
        }

        protected void UpdateProfileImage(string profileImage)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string query = "UPDATE UserProfile SET ProfileImage = @ProfileImage WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProfileImage", profileImage);
                    command.Parameters.AddWithValue("@UserId", userId);

                    command.ExecuteNonQuery();
                }
            }
        }

        protected void UpdateFullName(string fullName)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string query = "UPDATE UserProfile SET FullName = @FullName WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@UserId", userId);

                    command.ExecuteNonQuery();
                }
            }
        }

        protected void UpdateAbout(string about)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string query = "UPDATE UserProfile SET About = @About WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@About", about);
                    command.Parameters.AddWithValue("@UserId", userId);

                    command.ExecuteNonQuery();
                }
            }
        }

        protected string UploadImage(FileUpload fileUpload)
        {
            if (fileUpload.HasFile)
            {
                string fileName = Path.GetFileName(fileUpload.FileName);
                string filePath = Server.MapPath("~/Images/") + fileName;
                fileUpload.SaveAs(filePath);
                return "~/Images/" + fileName;
            }
            return null;
        }

        protected void btnUpdateImage_Click(object sender, EventArgs e)
        {
            string profileImagePath = UploadImage(fileUpdUpload);
            if (profileImagePath == null)
            {
                profileImagePath = "/Images/user.svg";
            }
            UpdateProfileImage(profileImagePath);
            LoadData();
        }
        protected void btnUpdateName_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text;
            UpdateFullName(fullName);
            LoadData();
        }

        protected void btnUpdateAbout_Click(object sender, EventArgs e)
        {
            string about = txtAbout.Text;
            UpdateAbout(about);
            LoadData();
        }
    }
}
