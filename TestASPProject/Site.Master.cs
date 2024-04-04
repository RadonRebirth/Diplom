using System;
using System.Web.UI;

namespace TestASPProject
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAuthenticated"] != null && (bool)Session["IsAuthenticated"])
            {
                LogoutButton.Enabled = true;
                LogoutButton.Visible = true;
                LoginButton.Enabled = false;
                LoginButton.Visible = false;
                Emploeers.Enabled = true;
                Emploeers.Visible = true;
                Profil.Enabled = true;
                Profil.Visible = true;
                Tasks.Enabled = true;
                Tasks.Visible = true;
            }
            else
            {
                LogoutButton.Enabled = false; 
                LogoutButton.Visible = false;
                LoginButton.Enabled = true;
                LoginButton.Visible = true;
                Emploeers.Enabled = false;
                Emploeers.Visible = false;
                Profil.Enabled = false;
                Profil.Visible = false;
                Tasks.Enabled = false;
                Tasks.Visible = false;
            }
        }

        public void LoginButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");

        }
        
        public void LogoutButton_Click(object sender, EventArgs e)
        {
            Session["IsAuthenticated"] = false;
            Response.Redirect("~/Home.aspx");
        }
        protected void EmploeersButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Baidge.aspx?UserId=" + Session["UserId"]);
        }

        protected void ProfilButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Profil.aspx?UserId=" + Session["UserId"]);
        }

        protected void TaskButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Tasks.aspx?UserId=" + Session["UserId"]);
        }

    }

}