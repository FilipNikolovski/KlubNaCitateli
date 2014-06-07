using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Script.Serialization;
using System.Collections;
using System.Text;
using KlubNaCitateli.Classes;

namespace KlubNaCitateli.Sites
{
    public partial class adminpanel : System.Web.UI.Page
    {
        private static string api_key = "AIzaSyDQNdTLOCVjieDzeY9IZoyaMvpDy4ApRec&maxResults";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbSearchBooks.Text.Trim() != "")
            {
                using (WebClient webClient = new WebClient())
                {
                    string url = "";
                    string urlContents = "";
                    url = "https://www.googleapis.com/books/v1/volumes?key=AIzaSyDQNdTLOCVjieDzeY9IZoyaMvpDy4ApRec&maxResults=40&q=";
                    url += tbSearchBooks.Text;

                    try
                    {
                        urlContents = webClient.DownloadString(url);
                    }
                    catch(Exception exc)
                    {
                        lblError.Text = exc.Message;
                        lblError.Visible = true;
                    }

                    if (urlContents.ToString().Trim() != "")
                    {
                        Dictionary<string, object> jsonData = new Dictionary<string, object>();

                        jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(urlContents);

                        ArrayList items = jsonData["items"] as ArrayList;

                        List<string> idList = new List<string>();
                        for (int i = 0; i < items.Count; i++)
                        {
                            Dictionary<string, object> item = items[i] as Dictionary<string, object>;
                            idList.Add(item["id"].ToString());
                        }

                        //Prevzemanje lista na knigi od google.books
                        List<Book> bookList = new List<Book>();

                        foreach (string id in idList)
                        {
                            url = "https://www.googleapis.com/books/v1/volumes/";
                            url += (id + "?key=" + api_key);

                            try
                            {
                                urlContents = webClient.DownloadString(url);
                                jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(urlContents);

                                Dictionary<string, object> volumeInfo = jsonData["volumeInfo"] as Dictionary<string, object>;

                                //Kreiranje i dodavanje na kniga vo lista----------------------------------------
                                Book book = new Book();
                                book.Name = volumeInfo["title"].ToString();
                                if (volumeInfo.ContainsKey("description"))
                                    book.Description = volumeInfo["description"].ToString();
                                else
                                    book.Description = "No description available.";
                                if (volumeInfo.ContainsKey("publishedDate"))
                                    book.YearPublished = volumeInfo["publishedDate"].ToString();
                                else
                                    book.YearPublished = "-";
                                book.Language = volumeInfo["language"].ToString();
                                book.DateAdded = DateTime.Now.Year.ToString();
                                book.SumRating = 0;
                                book.NumVotes = 0;

                                //Avtori
                                if (volumeInfo.ContainsKey("authors"))
                                {
                                    ArrayList authors = volumeInfo["authors"] as ArrayList;
                                    List<string> authorList = new List<string>();
                                    for (int i = 0; i < authors.Count; i++)
                                    {
                                        authorList.Add(authors[i].ToString());
                                    }
                                    book.Authors = authorList;
                                }
                                else
                                {
                                    book.Authors = new List<string>();
                                    book.Authors.Add("-");
                                }


                                //Link kon sliki
                                Dictionary<string, object> imageLinks = volumeInfo["imageLinks"] as Dictionary<string, object>;
                                if (imageLinks.ContainsKey("small"))
                                    book.ImageSrc = imageLinks["small"].ToString();
                                else if (imageLinks.ContainsKey("medium"))
                                    book.ImageSrc = imageLinks["medium"].ToString();
                                else if (imageLinks.ContainsKey("large"))
                                    book.ImageSrc = imageLinks["large"].ToString();
                                else
                                    book.ImageSrc = "defaultImage.png";
                                if (imageLinks.ContainsKey("thumbnail"))
                                    book.ThumbnailSrc = imageLinks["thumbnail"].ToString();
                                else if (imageLinks.ContainsKey("smallThumbnail"))
                                    book.ThumbnailSrc = imageLinks["smallThumbnail"].ToString();
                                else
                                    book.ThumbnailSrc = "defaultThumb.png";

                                //ISBN id na kniga
                                if (volumeInfo.ContainsKey("industryIdentifiers"))
                                {
                                    ArrayList isbn = volumeInfo["industryIdentifiers"] as ArrayList;
                                    Dictionary<string, object> isbn10 = isbn[0] as Dictionary<string, object>;
                                    book.ISBN = isbn10["identifier"].ToString();
                                }
                                else
                                    book.ISBN = "-";

                                //-------------------------------------------------------------------------------

                                bookList.Add(book);
                            }
                            catch (Exception ex)
                            {
                                lblError.Text = ex.Message;
                                lblError.Visible = true;
                            }
                        }

                        if (bookList.Count > 0)
                        {
                            gvBooks.DataSource = bookList;
                            gvBooks.DataBind();
                        }
                        
                    }
                   
                }
            }
        }

    }
}