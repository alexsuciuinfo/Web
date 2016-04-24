using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;

public partial class ManagerPages_AddEditor : System.Web.UI.Page
{
    static string  username;


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void CreateUserWizard1_ContinueButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        string[] roles = Roles.GetAllRoles();
        if (!roles.Contains("editor")) Roles.CreateRole("editor");

        username = CreateUserWizard1.UserName;
        Roles.AddUserToRole(username, "editor");

    }

    protected void CreateUserWizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {



        try
        {

            TextBox t;
            string FName = "", LName = "", Phone = "", Website = "";
            DateTime BirthDate = DateTime.Now;
            DateTime date = DateTime.Now;
            t = CreateUserWizard1.FindControl("FName") as TextBox;
            if (t != null) FName = t.Text;
            t = CreateUserWizard1.FindControl("LName") as TextBox;
            if (t != null) LName = t.Text;
            t = CreateUserWizard1.FindControl("BirthDate") as TextBox;
            if (t != null) BirthDate = Convert.ToDateTime(t.Text);
            t = CreateUserWizard1.FindControl("Phone") as TextBox;
            if (t != null) Phone = t.Text;
            t = CreateUserWizard1.FindControl("WebSite") as TextBox;
            if (t != null) Website = t.Text;

            String query = "insert into UserProfileData(FName,LName,BirthDate,JoinDate,Phone,Website,UserId) values (@fn,@ln,@bd,@jd,@ph,@web,(select UserId from Users where UserName = @username))  " +
                " insert into Options(UserId,EditOp,DeleteOp,AddOp,AcceptOp,CommentOp) values((select UserId from Users where UserName = @username),1,1,1,1,1)";

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
            try
            {
                con.Open();
                SqlCommand qcom = new SqlCommand(query, con);

                qcom.Parameters.AddWithValue("fn", FName);
                qcom.Parameters.AddWithValue("ln", LName);
                qcom.Parameters.AddWithValue("bd", BirthDate);
                qcom.Parameters.AddWithValue("jd", date);
                qcom.Parameters.AddWithValue("ph", Phone);
                qcom.Parameters.AddWithValue("web", Website);
                qcom.Parameters.AddWithValue("username", username);

                try
                {
                    qcom.ExecuteNonQuery();
                    LRaspuns.Text = "OK";

                }
                catch (System.Data.SqlClient.SqlException se)
                {
                    LRaspuns.Text = "Eroare selectearea din baza de date " + se.Message;
                    con.Close();
                }
                con.Close();
            }
            catch (System.InvalidOperationException ioe)
            {
                LRaspuns.Text = "Eroare la deschidere BD." + ioe.Message + " ";
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                LRaspuns.Text = "Eroare la deschidere BD." + se.Message + " ";
            }
            catch (Exception ex)
            {
                LRaspuns.Text = "Eroare la convertirea datelor." + ex.Message + " ";
            }
            finally
            {
                LRaspuns.Text = "Editor was added";
            }
        }
        finally
        {
        }

    }
}