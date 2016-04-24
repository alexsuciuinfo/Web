using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _SuggestedNew : System.Web.UI.Page
{
     static string id_new,username;

    protected void Page_Load(object sender, EventArgs e)
    {

        id_new = Request.Params["id"];
        if (id_new != null && !Page.IsPostBack)
        {
            id_new = Server.UrlDecode(id_new);
            if (Context.User.Identity.IsAuthenticated)
                isAuthentified();
            else isNotAuthentified();
        }

        else if(id_new == null)
        {
            Response.Redirect("~/Default.aspx");
        }

    }

    protected void BindData()
    {
        SqlDataSource1.SelectCommand = "select n.Id as newsId, c.Id, title,heading as heading,content,photo,date,name,c.id as Id from News n join Category c on n.Category_id = c.Id where n.Id = @id";
        SqlDataSource1.SelectParameters.Clear();
        SqlDataSource1.SelectParameters.Add("id", id_new);
        Repeater1.DataSource = SqlDataSource1;
        Repeater1.DataBind();
    }

    protected void isAuthentified()
    {
        BindData();
        username = Context.User.Identity.Name;
    }

    protected void isNotAuthentified()
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Accept")
        {
            string id = Convert.ToString(e.CommandArgument);

            try
            {
                DateTime date = DateTime.Now;
                String query = "update News set status = 1, date = @date  where Id = @idNews";

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                try
                {
                    con.Open();
                    SqlCommand qcom = new SqlCommand(query, con);

                    qcom.Parameters.AddWithValue("idNews", id);
                    qcom.Parameters.AddWithValue("date", date);

                    try
                    {
                        qcom.ExecuteNonQuery();
                        LRaspuns1.Text = "New was accepted";

                    }
                    catch (System.Data.SqlClient.SqlException se)
                    {
                        // LRaspuns1.Text = "Eroare la selectarea din baza de date " + se.Message;
                        con.Close();
                    }
                    con.Close();
                }
                catch (System.InvalidOperationException ioe)
                {
                    //   LRaspuns1.Text = "Eroare la deschiderea conexiunii la baza de date " + ioe.Message;
                }
                catch (System.Data.SqlClient.SqlException se)
                {
                    // LRaspuns1.Text = "Eroare la deschiderea conexiunii la baza de date " + se.Message;
                }
            }
            catch (Exception ex)
            {
                // LRaspuns1.Text = "Eroare la convertirea datelor." + ex.Message;
            }
            finally
            {
                LRaspuns1.Visible = true;
                BindData();
            }
        }

        else if (e.CommandName == "Decline")
        {

            try
            {

                String query = "delete from News where Id = @idNews " +
                                " delete from Comments where News_ID = @idNews ";

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                try
                {
                    con.Open();
                    SqlCommand qcom = new SqlCommand(query, con);

                    qcom.Parameters.AddWithValue("idNews", id_new);

                    try
                    {
                        qcom.ExecuteNonQuery();
                        LRaspuns1.Text = "New was deleted";

                    }
                    catch (System.Data.SqlClient.SqlException se)
                    {
                        LRaspuns1.Text = "Eroare la selectarea din baza de date " + se.Message;
                        con.Close();
                    }
                    con.Close();
                }
                catch (System.InvalidOperationException ioe)
                {
                    LRaspuns1.Text = "Eroare la deschiderea conexiunii la baza de date " + ioe.Message;
                }
                catch (System.Data.SqlClient.SqlException se)
                {
                    LRaspuns1.Text = "Eroare la deschiderea conexiunii la baza de date " + se.Message;
                }
            }
            catch (Exception ex)
            {
                LRaspuns1.Text = "Eroare la convertirea datelor." + ex.Message;
            }
            finally
            {
                LRaspuns1.Visible = true;
                Response.Redirect("~/SuggestedNews.aspx");

            }

        }
    }
}