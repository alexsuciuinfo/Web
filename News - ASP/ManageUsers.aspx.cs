using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;

public partial class ManagerPages_ManageUsers : System.Web.UI.Page
{
    static string username;

   protected void Page_Load(object sender, EventArgs e)
    {
       //if(!Page.IsPostBack)
           // BindGridview();
       if(!User.IsInRole("admin"))
           Response.Redirect("~/Default.aspx");
    }

   private void updateAll()
   {
       string query = "update Options set EditOp = 1, CommentOp = 1,DeleteOp = 1,AcceptOp = 1,AddOp=1 where UserId = (Select UserId from Users where UserName = @username)";
       SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
       con.Open();
       SqlCommand qcom = new SqlCommand(query, con);
       qcom.Parameters.AddWithValue("username", username);
       qcom.ExecuteNonQuery();
   }

    private void update(string query,string user)
    {

       try
       {
    
           SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
           try
           {
               con.Open();
               SqlCommand qcom = new SqlCommand(query, con);
               qcom.Parameters.AddWithValue("user", user);

               try
               {
                   qcom.ExecuteNonQuery();
                  // LRaspuns1.Text = "New was accepted";

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
          
       }
    }


    protected void GridView1_RowCommand(object sender,
   GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Block")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            username = GridView1.Rows[index].Cells[1].Text;
            MembershipUser user = Membership.GetUser(username);
            if (user != null)
            {
                user.IsApproved = false;
                Membership.UpdateUser(user);
                Response.Redirect("ManageUsers.aspx");
            }
        }

        else if (e.CommandName == "Unblock")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            username = GridView1.Rows[index].Cells[1].Text;
            MembershipUser user = Membership.GetUser(username);
            if (user != null)
            {
                user.IsApproved = true;
                Membership.UpdateUser(user);
                Response.Redirect("ManageUsers.aspx");
            }
        }

        else if (e.CommandName == "Make")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string role,role_ed = "editor";
            username = GridView1.Rows[index].Cells[1].Text;
            role = GridView1.Rows[index].Cells[3].Text;

            if (username != null && role != null)
            {
                int length = Roles.GetAllRoles().Length;
                string[] roles = Roles.GetAllRoles();
                if (!roles.Contains(role_ed)) Roles.CreateRole("editor");
                Roles.RemoveUserFromRole(username, role);
                Roles.AddUserToRole(username, "editor");
                updateAll();
                Response.Redirect("ManageUsers.aspx");
            }
        }

        else if (e.CommandName == "MakeA")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string role, role_ad = "admin";
            username = GridView1.Rows[index].Cells[1].Text;
            role = GridView1.Rows[index].Cells[3].Text;

            if (username != null && role != null)
            {
                int length = Roles.GetAllRoles().Length;
                string[] roles = Roles.GetAllRoles();
                if (!roles.Contains(role_ad)) Roles.CreateRole("admin");
                Roles.RemoveUserFromRole(username, role);
                Roles.AddUserToRole(username, "admin");
                updateAll();
                Response.Redirect("ManageUsers.aspx");
            }
        }

        else if (e.CommandName == "AllowSuggest")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set AddOp = 1 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageUsers.aspx");
        }

        else if (e.CommandName == "BlockSuggest")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set AddOp = 0 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageUsers.aspx");
        }

        else if (e.CommandName == "AllowComments")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set CommentOp = 1 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageUsers.aspx");
        }

        else if (e.CommandName == "BlockComments")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set CommentOp = 0 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageUsers.aspx");
        }

    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}