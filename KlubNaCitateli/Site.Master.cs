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
            if (Session["Name"] != null)
            {
                HyperLink2.Visible = false;
                HyperLink1.Text = "Log out";
                nameSurname.Text = Session["Name"].ToString() + " " + Session["Surname"].ToString();
                
            }
            else
            {
                HyperLink1.Text = "Log in";
                HyperLink2.Visible = true;
                nameSurname.Visible = false;
                

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
                Response.Redirect("index.aspx");
            }
        }
        public void signUp_click(object sender, EventArgs e)
        {
            
                Response.Redirect("signup.aspx");
            
        }
    }
}