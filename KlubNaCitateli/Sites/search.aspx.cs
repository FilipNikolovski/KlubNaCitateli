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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            List<Book> books = new List<Book>();
            books = Book.SelectListBooks(tbSearch.Text, "Any", "Any");

            if (books.Count > 0)
            {
                StringBuilder innerHTML = new StringBuilder();
                foreach (Book book in books)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<div class='searchItem'>");
                    sb.Append("<div class='span1'></div>");
                    sb.Append("<div class='span2'><span>");
                    foreach (string author in book.Authors)
                    {
                        sb.Append(author.ToString() + " ");
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