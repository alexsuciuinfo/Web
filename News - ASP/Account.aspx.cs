using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class Account : System.Web.UI.Page
{
    static int status;
    static string username;
    static string AddOp;

    protected void Page_Load(object sender, EventArgs e)
    {   
            if (Context.User.Identity.IsAuthenticated && !Page.IsPostBack)
                isAuthenticated();
            else if(!Context.User.Identity.IsAuthenticated) isNotAuthenticated();
             
    }

    protected void isAuthenticated()
    {
        BindData();

        if (User.IsInRole("editor"))
        {
            status = 1;
            divTextNew.InnerHtml = "Add a new";
            isEditor();
        }

        else if (User.IsInRole("registered_user"))
        {
            status = 0;
            divTextNew.InnerHtml = "Suggest a new";
            isUser();
        }

        else if (User.IsInRole("admin"))
            isAdmin();


        username = Context.User.Identity.Name;
        string query = " where p.UserId = (Select UserId from Users where UserName = @username)";
        SqlDataSource2.SelectCommand = SqlDataSource2.SelectCommand + query;
        SqlDataSource2.SelectParameters.Clear();
        SqlDataSource2.SelectParameters.Add("username", username);
        SqlDataSource2.DataBind();

        Options();
        
    }

    private void Options()
    {
        String query = "Select AddOp from Options where UserId = (Select UserId from Users where UserName = @username)";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
        con.Open();
        SqlCommand qcom = new SqlCommand(query, con);
        qcom.Parameters.AddWithValue("username", username);

        SqlDataReader reader = qcom.ExecuteReader();
        while (reader.Read())
        {
            AddOp = reader["AddOp"].ToString();
        }

    }

    private void isUser()
    {
        LabelIsLoggedIn.Visible = true;
       LabelSuggestNew.Visible = true;
        LabelOptions.Visible = false;
        LabelAdmin.Visible = false;
    }

    private void isEditor()
    {

        LabelIsLoggedIn.Visible = true;
        LabelSuggestNew.Visible = true;
        LabelOptions.Visible = true;
        LabelAdmin.Visible = false;
       // LabelOptionEditor.Visible = true;
    }

    private void isAdmin()
    {
        LabelIsLoggedIn.Visible = true;
       LabelSuggestNew.Visible = false;
        LabelOptions.Visible = true;
        LabelAdmin.Visible = true;
       // LabelOptionsManager.Visible = true;
    }

    private void BindData()
    {
        DropDownList1.DataSource = SqlDataSource1;
       DropDownList2.DataSource = SqlDataSource1;
       DropDownList3.DataSource = SqlDataSource1;

        DropDownList1.DataBind();
        DropDownList2.DataBind();
        DropDownList3.DataBind();

    }

    protected void isNotAuthenticated()
    {
        LabelIsNotLoggedIn.Text = "You are not logged in ";
        LabelIsNotLoggedIn.Visible = true;
        LabelIsLoggedIn.Visible = false;
        LabelSuggestNew.Visible = false;
        LabelOptions.Visible = false;
        LabelAdmin.Visible = false;

    }

    protected void Button_SuggestNew(object sender, EventArgs e)
    {
        if (AddOp == "True")
        {
            try
            {
                int typeNew = 1;
                String titlu = TBTitle.Text;
                String heading = TBHead.Text;
                String continut = TBContinut.Text;
                int id_categorie = Convert.ToInt32(DropDownList1.SelectedValue);
                DateTime data = DateTime.Now;
                string poza, path = "";

                if (selectBox_Suggested.Value.ToString() == "original")
                    typeNew = 1;
                else typeNew = 0;

                string type = FileUploadMI.PostedFile.ContentType;

                if (FileUploadMI.HasFile && (type.Contains("jpg") || type.Contains("jpeg") || type.Contains("png")))
                {
                    poza = FileUploadMI.FileName;
                    string poza1 = Server.MapPath("~") + "/Images/" + poza.ToString();
                    while(File.Exists(poza1))
                    {
                        Random r = new Random();
                        string x = r.Next(0, 999).ToString();
                        poza = x + poza;
                        poza1 = Server.MapPath("~") + "/Images/" + poza;
                    }
                    FileUploadMI.SaveAs(Server.MapPath("~") + "/Images/" + poza);
                    LRaspuns.Text = "Received" + FileUploadMI.FileName + " Content Type " + FileUploadMI.PostedFile.ContentType;
                    path = "/Images/" + poza.ToString();

                }

                String query = "insert into News(Title,Heading,Content,Photo,category_id,date,Original,Status,User_Id) values (@pt,@phead,@pcont,@ppoz, @pcateg, @pdata,@porig,@pstat,(Select UserId from Users where UserName = @pusername))";

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                try
                {
                    con.Open();
                    SqlCommand qcom = new SqlCommand(query, con);

                    qcom.Parameters.AddWithValue("pt", titlu);
                    qcom.Parameters.AddWithValue("phead", heading);
                    qcom.Parameters.AddWithValue("pcont", continut);
                    qcom.Parameters.AddWithValue("ppoz", path);
                    qcom.Parameters.AddWithValue("pcateg", id_categorie);
                    qcom.Parameters.AddWithValue("pdata", data);
                    qcom.Parameters.AddWithValue("porig", typeNew);
                    qcom.Parameters.AddWithValue("pstat", status);
                    qcom.Parameters.AddWithValue("pusername", username);

                    try
                    {
                        qcom.ExecuteNonQuery();
                        LRaspuns.Text = "New was added ";

                    }
                    catch (System.Data.SqlClient.SqlException se)
                    {
                        LRaspuns.Text = "Eroare la selectarea din baza de date " + se.Message + path;
                        con.Close();
                    }
                    con.Close();
                }
                catch (System.InvalidOperationException ioe)
                {
                    LRaspuns.Text = "Eroare la deschiderea conexiunii la baza de date " + ioe.Message;
                }
                catch (System.Data.SqlClient.SqlException se)
                {
                    LRaspuns.Text = "Eroare la deschiderea conexiunii la baza de date " + se.Message;
                }
            }
            catch (Exception ex)
            {
                LRaspuns.Text = "Eroare la convertirea datelor." + ex.Message;
            }
            finally
            {
                LRaspuns.Visible = true;
                BindData();

            }
        }
        else LRaspuns.Text = "You are not allowed to add news";

    }

    protected void Button_AddCategory(object sender, EventArgs e)
    {
       
            Boolean exist = false;
            String category = TBAddCategory.Text;

            foreach (ListItem l in DropDownList1.Items)
            {
                if (l.Text == category)
                {
                    exist = true;
                }
            }
            if (exist == true) LRaspuns.Text = "There is already a category with this name";

            else try
                {

                    String query = "insert into Category(Name) values (@pnume)";

                    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                    try
                    {
                        con.Open();
                        SqlCommand qcom = new SqlCommand(query, con);

                        qcom.Parameters.AddWithValue("pnume", category);

                        try
                        {
                            qcom.ExecuteNonQuery();
                            LRaspuns1.Text = "Category was added";

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
                    BindData();
                }
       
        }
    

    protected void Button_DeleteCategory(object sender, EventArgs e)
    {
        
            try
            {
                int id_categorie = Convert.ToInt32(DropDownList2.SelectedValue);

                String query = "delete from News where Category_Id = @categ_id " + " delete from Category where Id = @categ_id ";

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                try
                {
                    con.Open();
                    SqlCommand qcom = new SqlCommand(query, con);

                    qcom.Parameters.AddWithValue("categ_id", id_categorie);

                    try
                    {
                        qcom.ExecuteNonQuery();
                        LRaspuns1.Text = "Categoria was deleted";

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
                    LRaspuns.Text = "Eroare la deschiderea conexiunii la baza de date " + se.Message;
                }
            }
            catch (Exception ex)
            {
                LRaspuns1.Text = "Eroare la convertirea datelor." + ex.Message;
            }
            finally
            {
                LRaspuns1.Visible = true;
                BindData();

            }
        
    }

    protected void Button_RenameCategory(object sender, EventArgs e)
    {

       
            Boolean exist = false;
            String category = TBCategoryRename.Text;
            int id_categorie = Convert.ToInt32(DropDownList2.SelectedValue);

            foreach (ListItem l in DropDownList1.Items)
            {
                if (l.Text == category)
                {
                    exist = true;
                }
            }
            if (exist == true) LRaspuns1.Text = "There is already a category with this name";

            else try
                {

                    String query = "update Category set Name = @pnume where Id = @idcateg";

                    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
                    try
                    {
                        con.Open();
                        SqlCommand qcom = new SqlCommand(query, con);

                        qcom.Parameters.AddWithValue("pnume", category);
                        qcom.Parameters.AddWithValue("idcateg", id_categorie);

                        try
                        {
                            qcom.ExecuteNonQuery();
                            LRaspuns1.Text = "Category was renamed";

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
                    BindData();

                }
        
    }



    protected void ButtonAddEditor_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditor.aspx");
    }
    protected void ButtonManageUsers_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageUsers.aspx");
    }
    protected void ButtonManageEditors_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageEditors.aspx");
    }
    protected void ButtonViewNews_Click(object sender, EventArgs e)
    {
            Response.Redirect("SuggestedNews.aspx");
    }
}