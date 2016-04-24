using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _SuggestedNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Context.User.IsInRole("editor"))
                BindData();
            else Response.Redirect("~/Default.aspx");
        }

    }

    protected void BindData()
    {
        Repeater1.DataSource = SqlDataSource1;
        Repeater1.DataBind();
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Accept")
        {
            string id = Convert.ToString(e.CommandArgument);

             try
            {

                String query = "update News set status = 1 where Id = @idNews";

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
                BindData();

            }
            
        }

        else if (e.CommandName == "Decline")
        {
            string id = Convert.ToString(e.CommandArgument);
            try
            {

                String query = "delete from News where Id = @idNews";

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
                BindData();

            }

        }
    }
}