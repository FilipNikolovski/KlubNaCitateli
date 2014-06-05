using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KlubNaCitateli.Classes;
using System.Text;

namespace KlubNaCitateli.Sites
{
    public partial class search : System.Web.UI.Page
    {
        private Database db;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            db = new Database();
            List<Book> books = new List<Book>();
            books = db.SelectListBooks(tbSearch.Text, "Any", "Any");

            if (books.Count > 0)
            {
                StringBuilder innerHTML = new StringBuilder();
                foreach (Book book in books)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<div class='searchItem'>");
                    sb.Append("<div class='span1'></div>");
                    sb.Append("<div class='span2'><span>");
                    foreach (Author author in book.Authors)
                    {
                        sb.Append(author.Name + " " + author.Surname + " ");
                    }
                    sb.Append("</span></div>");
                    sb.Append("<div class='span3'><span>" + book.Description + "</span></div>");
                    sb.Append("<div class='span4'><span>" + book.YearPublished + "</span></div>");

                    sb.Append("</div>");
                    innerHTML.Append(sb.ToString());
                }

                searchList.InnerHtml = innerHTML.ToString();
            }

            else
            {
                searchList.InnerHtml = "<div class='searchItem'><span>Search result is empty.</span></div>";
            }
        }


    }
}