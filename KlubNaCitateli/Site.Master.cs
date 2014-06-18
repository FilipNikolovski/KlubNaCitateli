using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace KlubNaCitateli
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink3.PostBackUrl = "~/Sites/search.aspx";
            HyperLink4.PostBackUrl = "~/Sites/forum.aspx";
            HyperLink5.PostBackUrl = "~/Sites/index.aspx";

            if (Session["Name"] != null)
            {
                HyperLink1.Text = "Log out";
                HyperLink2.Text =Session["Name"].ToString() + " " + Session["Surname"].ToString();
               
            }
            else
            {
                HyperLink1.Text = "Log in";
            }

        }
        public void logInOut_click(object sender, EventArgs e)
        {
            if (HyperLink1.Text == "Log in")
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                Session["Name"] = null;
                Session["Surname"] = null;
                Session["Id"] = null;
                Session["Type"] = null;
                Session["Banned"] = null;
                Response.Redirect("index.aspx");
            }
        }
        public void signUp_click(object sender, EventArgs e)
        {
            if (HyperLink2.Text == "Sign Up")
            { 
                Response.Redirect("signup.aspx"); 
            }
            else
            {
                Response.Redirect("profile.aspx?id="+Session["Id"].ToString());
            }
            
        }

        public void myProfile_click(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                Response.Redirect("profile.aspx?id=" + Session["Id"].ToString());
            }
            else
                Response.Redirect("login.aspx"); 
        }
    }
}