using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

public partial class EditorPages_Adauga : System.Web.UI.Page
{
    static int status;
    static string username, id_new;

    protected void Page_Load(object sender, EventArgs e)
    {

        id_new = Request.Params["id"];
        if (!Page.IsPostBack && id_new != null)
        {
            if (Context.User.Identity.IsAuthenticated)
                isAuthenticated();
            else isNotAuthenticated();
        }


        else if(id_new == null) 
            Response.Redirect("~/Default.aspx");



    }

    protected void isAuthenticated()
    {
        if (User.IsInRole("editor"))
        {
            status = 1;
        }

        else 
        {
            Response.Redirect("~/Default.aspx");
        }

        id_new = Server.UrlDecode(id_new);
        username = Context.User.Identity.Name;
        string query = " where n.Id = @newsID";
        SqlDataSource2.SelectCommand = SqlDataSource2.SelectCommand + query;
        SqlDataSource2.SelectParameters.Clear();
        SqlDataSource2.SelectParameters.Add("newsID", id_new);
        SqlDataSource2.DataBind();

    }

    protected void isNotAuthenticated()
    {
        Response.Redirect("~/Default.aspx");
        
    }

    protected void Button_EditNew(object sender, EventArgs e)
    {

        try
        {
            TextBox t;
            int typeNew = 1;
            t = Repeater1.Items[0].FindControl("TBTitle") as TextBox;
            String titlu = t.Text;
            t = Repeater1.Items[0].FindControl("TBHead") as TextBox;
            String heading = t.Text;
            t = Repeater1.Items[0].FindControl("TBContinut") as TextBox;
            String continut = t.Text;
            DropDownList drop1 = Repeater1.Items[0].FindControl("DropDownList1") as DropDownList;
            int id_categorie = Convert.ToInt32(drop1.SelectedValue);
            FileUpload FileUploadMI = Repeater1.Items[0].FindControl("FileUploadMI") as FileUpload;
            DropDownList drop2 = Repeater1.Items[0].FindControl("DropDownList2") as DropDownList;
            DateTime data = DateTime.Now;
            string poza, path = "",query;
            Boolean isPhoto = true;

            if (drop2.SelectedValue.ToString() == "original")
                typeNew = 1;
            else typeNew = 0;

            string type = FileUploadMI.PostedFile.ContentType;

            if (FileUploadMI.HasFile && (type.Contains("jpg") || type.Contains("jpeg") || type.Contains("png")))
            {
                poza = FileUploadMI.FileName;
                string poza1 = Server.MapPath("~") + "/Images/" + poza.ToString();
                while (File.Exists(poza1))
                {
                    Random r = new Random();
                    string x = r.Next(0, 999).ToString();
                    poza = x + poza;
                    poza1 = Server.MapPath("~") + "/Images/" + poza;
                }
                FileUploadMI.SaveAs(Server.MapPath("~") + "/Images/" + poza);
                LRaspuns.Text = "Received" + FileUploadMI.FileName + " Content Type " + FileUploadMI.PostedFile.ContentType;
                path = "/Images/" + poza.ToString();
                isPhoto = true;
            }

            else isPhoto = false;
                
                if(isPhoto == true)
             query = "update News set Title = @pt,Heading=@phead,Content=@pcont,"+
            "Photo = @ppoz,category_id = @pcateg,date=@pdata,Original=@porig,Status=@pstat,User_Id = (Select UserId from Users where UserName = @pusername) "+
            "where Id = @newsId";

                else query = "update News set Title = @pt,Heading=@phead,Content=@pcont," +
            "category_id = @pcateg,date=@pdata,Original=@porig,Status=@pstat,User_Id = (Select UserId from Users where UserName = @pusername)"+
            "where Id = @newsId";
            

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
                qcom.Parameters.AddWithValue("newsId", id_new);

                try
                {
                    qcom.ExecuteNonQuery();
                    LRaspuns.Text = "New has been edited " + id_new;

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
        }
    }
       
    
    }
