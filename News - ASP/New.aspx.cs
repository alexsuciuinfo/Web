using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class New : System.Web.UI.Page
{
    static string id_new,username;
    static Boolean isAuth = false;
    static string EditOp, DeleteOp,CommentOp;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        id_new = Request.Params["id"];
        if (!Page.IsPostBack && id_new != null)
        {
            if (Context.User.Identity.IsAuthenticated)
                isAuthenticated();
            else isNotAuthenticated();
        }


        else if (id_new == null)
            Response.Redirect("~/Default.aspx");

    }

    private void Options()
    {
        String query = "Select EditOp,DeleteOp,CommentOp from Options where UserId = (Select UserId from Users where UserName = @username)";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
        con.Open();
        SqlCommand qcom = new SqlCommand(query, con);
        qcom.Parameters.AddWithValue("username", username);

        SqlDataReader reader = qcom.ExecuteReader();
        while (reader.Read())
        {
            EditOp = reader["EditOp"].ToString();
            DeleteOp = reader["DeleteOp"].ToString();
            CommentOp = reader["CommentOp"].ToString();
        }

    }

    protected void BindData()
    {

        SqlDataSource1.SelectCommand = "select n.Id as newsId, c.Id, title,heading as heading,content,photo,date,name,c.id as Id from News n join Category c on n.Category_id = c.Id where n.Id = @id";
        SqlDataSource1.SelectParameters.Clear();
        SqlDataSource1.SelectParameters.Add("id", id_new);
        Repeater1.DataSource = SqlDataSource1;
        Repeater1.DataBind();

            SqlDataSource2.SelectCommand = "select n.Id as newsID, c.Id as com_Id,c.date as date, c.comment as comm,u.UserName as user_name from Comments c join News n on c.News_Id = n.Id join Users u on u.UserId = c.User_Id where n.Id = @id";
            SqlDataSource2.SelectParameters.Clear();
            SqlDataSource2.SelectParameters.Add("id", id_new);
        

        Repeater2.DataSource = SqlDataSource2;
        Repeater2.DataBind();
    }

    protected void isAuthenticated()
    {
        BindData();
        isAuth = true;
        username = Context.User.Identity.Name;
        Options();
        if (Context.User.IsInRole("editor"))
        {
            make_button_visible();
        }
        
    }

    protected void isNotAuthenticated()
    {
        BindData();
        isAuth = false;
    }

    private void make_button_visible()
    {

        Button b1, b2; int nr = 0;

        foreach (RepeaterItem e in Repeater1.Items)
        {
            b1 = e.FindControl("ButtonEdit") as Button;
            b2 = e.FindControl("ButtonDelete") as Button;
            b1.Visible = true;
            b2.Visible = true;
            nr++;
        }

        foreach (RepeaterItem e in Repeater2.Items)
        {
            b1 = e.FindControl("ButtonDelComm") as Button;
            b1.Visible = true;
        }


    }

    protected void Button_Add_Click(object sender, EventArgs e)
    {

        if (Context.User.Identity.IsAuthenticated)
        {
            if (CommentOp == "True")
            {
                try
                {
                    string message = TBContinut.Text;
                    DateTime date = DateTime.Now;
                    String query = "insert into Comments(User_Id,News_Id,Comment,Date) values ((Select userID from Users where username = @username),@newsID,@pcomm,@pdate)";

                    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                    try
                    {
                        con.Open();
                        SqlCommand qcom = new SqlCommand(query, con);

                        qcom.Parameters.AddWithValue("pcomm", message);
                        qcom.Parameters.AddWithValue("pdate", date);
                        qcom.Parameters.AddWithValue("username", username);
                        qcom.Parameters.AddWithValue("newsID", id_new);

                        try
                        {
                            qcom.ExecuteNonQuery();
                            LRaspuns1.Text = "Comment was added";

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
                    isAuthenticated();

                }
            }
            else
            {
                LRaspuns1.Text = "You are not allowed to post comments";
                LRaspuns1.Visible = true;
            }
        }

        else
        {
            LRaspuns1.Text = "You are not logged in";
            LRaspuns1.Visible = true;
        }

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

        else if (e.CommandName == "Delete")
        {
            if (DeleteOp == "True")
            {

                try
                {

                    String query = " delete from Comments where News_ID = @idNews " +
                        " delete from News where Id = @idNews ";
                                    

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
                    Response.Redirect("~/Default.aspx");

                }

            }
        }
        else
        {
            Label l = e.Item.FindControl("OptionsEd") as Label;
            l.Text = "You are not allowed to delete news";
            l.Visible = true;
        }
    }
         protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
         {
             if (e.CommandName == "DeleteComm")
             {
                 string comm_id = e.CommandArgument.ToString();

                 try
                 {

                     String query = " delete from Comments where Id = @idComm ";

                     SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                     try
                     {
                         con.Open();
                         SqlCommand qcom = new SqlCommand(query, con);

                         qcom.Parameters.AddWithValue("idComm", comm_id);

                         try
                         {
                             qcom.ExecuteNonQuery();
                             LRaspuns1.Text = "Comment was deleted";

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
                     isAuthenticated();

                 }

             }
         }
}
