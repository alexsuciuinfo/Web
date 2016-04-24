using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{

    static string username,search;
    static string EditOp, DeleteOp;

    protected void Page_Load(object sender, EventArgs e)
    {
        search = Request.Params["search"];

        if (!Page.IsPostBack && search != null)
        {
            search = Server.UrlDecode(search).ToString();
            SqlDataSource1.SelectCommand = "select n.Id as newsId, title,LEFT(heading,120) + '....' as heading,content,photo,date,name,c.id as Id from News n join Category c " +
            "on n.Category_id = c.Id where status = 'true' and (title like @search or heading like @search or content like @search) order by date desc";
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("search", "%" + search + "%");
        }

        if (!Page.IsPostBack)
        {
            if (Context.User.Identity.IsAuthenticated)
                isAuthenticated();
            else isNotAuthenticated();
        }


    }

    private void Options()
    {
        String query = "Select EditOp,DeleteOp from Options where UserId = (Select UserId from Users where UserName = @username)";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
        con.Open();
        SqlCommand qcom = new SqlCommand(query, con);
        qcom.Parameters.AddWithValue("username", username);

            SqlDataReader reader = qcom.ExecuteReader();
            while (reader.Read())
            {
                EditOp = reader["EditOp"].ToString();
                DeleteOp = reader["DeleteOp"].ToString();
            }
       
    }

    private void isAuthenticated()
    {
        username = Context.User.Identity.Name;
        if (User.IsInRole("editor"))
        {
            make_button_visible();
            Options();
        }
        else BindData();
    }

    private void make_button_visible()
    {
        BindData();

        Button b1, b2; 

        foreach (RepeaterItem e in Repeater1.Items)
        {
            b1 = e.FindControl("ButtonEdit") as Button;
            b2 = e.FindControl("ButtonDelete") as Button;
            b1.Visible = true;
            b2.Visible = true;
        }
        
    }

    private void isNotAuthenticated()
    {
        BindData();
    }

    protected void BindData()
    {
        Repeater1.DataSource = SqlDataSource1;
        Repeater1.DataBind();
    }
    protected void myfun(object sender, RepeaterItemEventArgs e)
    {
     
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            if (EditOp == "True")
            {
                string id = Convert.ToString(e.CommandArgument);
                Response.Redirect("Adauga.aspx?id=" + id);
            }

            else
            {
                Label l = e.Item.FindControl("OptionsEd") as Label;
                l.Text = "You are not allowed to edit news";
                l.Visible = true;
            }
        }
        
        else if(e.CommandName == "Delete")
        {
            string id = Convert.ToString(e.CommandArgument);
            if (DeleteOp == "True")
            {
                try
                {

                    String query = "delete from Comments where News_Id = @idNews " +
                        "delete from News where Id = @idNews";

                    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                    try
                    {
                        con.Open();
                        SqlCommand qcom = new SqlCommand(query, con);

                        qcom.Parameters.AddWithValue("idNews", id);

                        try
                        {
                            qcom.ExecuteNonQuery();
                            // LRaspuns1.Text = "New was deleted";

                        }
                        catch (System.Data.SqlClient.SqlException se)
                        {
                           //  LRaspuns1.Text = "Eroare la selectarea din baza de date " + se.Message;
                            con.Close();
                        }
                        con.Close();
                    }
                    catch (System.InvalidOperationException ioe)
                    {
                          // LRaspuns1.Text = "Eroare la deschiderea conexiunii la baza de date " + ioe.Message;
                    }
                    catch (System.Data.SqlClient.SqlException se)
                    {
                      //  LRaspuns1.Text = "Eroare la deschiderea conexiunii la baza de date " + se.Message;
                    }
                }
                catch (Exception ex)
                {
                   // LRaspuns1.Text = "Eroare la convertirea datelor." + ex.Message;
                }
                finally
                {
                   //  LRaspuns1.Visible = true;
                    isAuthenticated();
                }
            }

            else
            {
                Label l = e.Item.FindControl("OptionsEd") as Label;
                l.Text = "You are not allowed to delete news";
                l.Visible = true;
            }

        }
    }
}