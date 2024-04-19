using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Net.Mail;
using System.Web;

namespace TestASPProject
{
    public partial class Auth : Page
    {
        private const string connectionString = "Data Source=RADON;Initial Catalog=ASProject;Integrated Security=True;";
        public bool isQr = false;
        public bool isEmail = false;
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "*Введите имя пользователя и пароль.";
                return;
            }

            if (Regex.IsMatch(username, @"\s+"))
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "*Имя пользователя не должно содержать пробелов";
                return;
            }

            // Проверка пароля на стандарты качественного пароля
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "*Пароль должен содержать как минимум одну заглавную букву, одну строчную букву и одну цифру, и быть длиной не менее 8 символов.";
                return;
            }

            if (AuthenticateUser(username, password))
            {
                Session["UserId"] = GetUserId(username);
                errorTxt.ForeColor = Color.Green;
                errorTxt.Text = "Успешная авторизация";

                BtnEmaiLogin_Click();
                if (Session["IsQr"] != null && (bool)Session["IsQr"])
                {
                    BtnQrLogin_Click(); // Вызов метода для QR-авторизации
                }
                else if (Session["IsEmail"] != null && (bool)Session["IsEmail"])
                {
                    BtnEmaiLogin_Click(); // Вызов метода для Email-авторизации
                }
            }
            else
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "*Неверное имя пользователя или пароль.";
            }
        }

        public int GetUserId(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string getUserIdQuery = "SELECT Id FROM Users WHERE Username = @username";
                using (SqlCommand command = new SqlCommand(getUserIdQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
                using (SqlCommand command = new SqlCommand(selectUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    return userCount > 0;
                }
            }
        }

        protected void BtnGenerQR_Click(object sender, EventArgs e)
        {
            isQr = true;
            isEmail = false;
            // Сохранение состояния флагов в сессии
            Session["IsQr"] = isQr;
            Session["IsEmail"] = isEmail;
            Random rand = new Random();
            int i = rand.Next(100000, 999999);
            string qrimage = Convert.ToString(i);
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrcode = encoder.Encode(qrimage);

            using (MemoryStream ms = new MemoryStream())
            {
                qrcode.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                QRImage.ImageUrl = "data:image/png;base64," + base64String;
            }

            errorTxt.ForeColor = Color.Green;
            errorTxt.Text = $"QR код сгенерирован, откройте окно входа повторно.";
        }

        protected void BtnQrLogin_Click()
        {
            string base64Image = QRImage.ImageUrl.Replace("data:image/png;base64,", "");
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            string tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, imageBytes);

            try
            {
                QRCodeDecoder decoder = new QRCodeDecoder();
                Bitmap bitmap = new Bitmap(tempFilePath);
                string decodedText = decoder.Decode(new QRCodeBitmapImage(bitmap));
                string deCode = txtSecretCode.Text;

                if (deCode == decodedText)
                {
                    errorTxt.ForeColor = Color.Green;
                    errorTxt.Text = "*Аутентификация по QR-коду успешна.";
                    HttpContext.Current.Session["IsAuthenticated"] = true;
                    Response.Redirect("Profil.aspx?UserId=" + Session["UserId"]);
                    Session["IsQr"] = false;
                    Session["IsEmail"] = false;
                }
                else
                {
                    errorTxt.ForeColor = Color.Red;
                    errorTxt.Text = "*Вы ввели неверное число. Авторизируйтесь заново.";
                    GenerateAndDisplayQRCode();
                    txtSecretCode.Text = "";
                }
            }
            catch (Exception ex)
            {
                errorTxt.Text = "Ошибка при декодировании QR-кода: " + ex.Message;
            }
        }

        private void GenerateAndDisplayQRCode()
        {
            string deCode = GenerateSecretCode();
            string qrImage = Convert.ToString(deCode);
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrcode = encoder.Encode(qrImage);
            string imagePath = Server.MapPath("~/qrcodes/qrcode.png");
            qrcode.Save(imagePath);

            // Отображение изображения
            QRImage.ImageUrl = "~/qrcodes/qrcode.png";
        }


        protected void BtnSendCode_Click(object sender, EventArgs e)
        {
            isQr = false;
            isEmail = true;
            // Сохранение состояния флагов в сессии
            Session["IsQr"] = isQr;
            Session["IsEmail"] = isEmail;
            string emailTo = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(emailTo))
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "Пожалуйста, введите адрес электронной почты.";
                return;
            }

            // Проверка адреса электронной почты на валидность
            if (!Regex.IsMatch(emailTo, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "Пожалуйста, введите корректный адрес электронной почты.";
                return;
            }

            string secretCode = GenerateSecretCode();

            try
            {
                SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru");
                mySmtpClient.UseDefaultCredentials = true;
                mySmtpClient.EnableSsl = true;

                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("jf.com@mail.ru", "pkKjpAmnzeSLfVEt0Dmk");
                mySmtpClient.Credentials = basicAuthenticationInfo;

                MailAddress from = new MailAddress("jf.com@mail.ru", "3FactorAuth");
                MailAddress to = new MailAddress(emailTo, "TestToName");
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                myMail.Subject = $"Привет, вот код, который ты просил: {secretCode}";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                myMail.Body = $"Привет, вот код, который ты просил: <b>{secretCode}</b><br>using <b>HTML</b>.";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;

                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
                Session["SecretCode"] = secretCode;

                errorTxt.ForeColor = Color.Green;
                errorTxt.Text = $"Код отправлен на указанный адрес электронной почты.";
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException("SmtpException has occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnEmaiLogin_Click()
        {
            string enteredCode = TextBox1.Text;

            if (Session["SecretCode"] != null && enteredCode == Session["SecretCode"].ToString())
            {
                errorTxt.ForeColor = Color.Green;
                errorTxt.Text = "Код верен! Переход на другую форму.";
                HttpContext.Current.Session["IsAuthenticated"] = true;
                Response.Redirect("Profil.aspx?UserId=" + Session["UserId"]);

                Session["IsQr"] = false;
                Session["IsEmail"] = false;
            }
            else
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "Код неверен! Пожалуйста, введите правильный код.";
            }
        }

        private string GenerateSecretCode()
        {
            Random rand = new Random();
            int i = rand.Next(100000, 999999);
            return i.ToString();
        }
    }
}
