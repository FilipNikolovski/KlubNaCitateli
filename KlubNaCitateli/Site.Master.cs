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
<<<<<<< HEAD
=======
            HyperLink3.PostBackUrl = "~/Sites/search.aspx";
            HyperLink4.PostBackUrl = "~/Sites/forum.aspx";
            HyperLink5.PostBackUrl = "~/Sites/index.aspx";
>>>>>>> 1bf304ac17f10fa20fa26ae57e61c449e2fdce23

            if (Session["Name"] != null)
            {
                HyperLink1.Text = "Log out";
                if (Session["Type"].ToString().Equals("administrator"))
                {
                    HyperLink2.Text = "Admin panel";
                }
                else
                { 
                    HyperLink2.Text = Session["Name"].ToString() + " " + Session["Surname"].ToString(); 
                }
               
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
            else if (HyperLink2.Text == "Admin panel")
            {
                Response.Redirect("adminpanel.aspx");
            }
            else
            {
                Response.Redirect("profile.aspx?id=" + Session["Id"].ToString());
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