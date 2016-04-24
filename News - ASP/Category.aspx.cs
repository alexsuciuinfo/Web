using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Category : System.Web.UI.Page
{
    static string search,sort;

    protected void Page_Load(object sender, EventArgs e)
    {
        search = Request.Params["category"];

        if (search != null && !Page.IsPostBack)
        {
            sort = DropDownList1.SelectedValue.ToString();
            search = Server.UrlDecode(search);
            categ();
                if (Context.User.Identity.IsAuthenticated)
                    isAuthenticated();
                else isNotAuthenticated();
            
        }

        else if(search==null) Response.Redirect("~/Default.aspx");

    }

    private void categ()
    {
        string numecateg ="";
         String query = "Select Name from Category where id = @idcateg";
         SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand qcom = new SqlCommand(query, con);
                qcom.Parameters.AddWithValue("idcateg", search);
                    SqlDataReader reader = qcom.ExecuteReader();
                    while (reader.Read())
                    {
                        numecateg = reader["Name"].ToString();
                    }
       category.InnerText = numecateg;
    }

    private void isAuthenticated()
    {
        if (Context.User.IsInRole("editor"))
        {
            BindData(sort);
            make_button_visible();
        }
        else BindData(sort);
    }

    private void make_button_visible()
    {
        
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
        BindData(sort);
    }



    protected void BindData(string sort)
    {
        
        SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand = "select Id as newsId, title,LEFT(heading,120) + '....' as heading,content,photo,date from News " +
            "where Category_id = @id and status = 'true' order by " + sort;
        SqlDataSource1.SelectParameters.Clear();
        SqlDataSource1.SelectParameters.Add("id", search);
        Repeater1.DataSource = SqlDataSource1;
        Repeater1.DataBind();
    }

    protected void Sort_select(object sender, EventArgs e)
    {
        
        string sort = DropDownList1.SelectedValue.ToString();
        if(Context.User.IsInRole("editor"))
        {
            BindData(sort);
            make_button_visible();
        }
        else BindData(sort);
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string id = Convert.ToString(e.CommandArgument);
            Response.Redirect("Adauga.aspx?id=" + id);
        }

        else if (e.CommandName == "Delete")
        {
            string id = Convert.ToString(e.CommandArgument);
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
                // LRaspuns1.Visible = true;
                isAuthenticated();

            }

        }
    }
}