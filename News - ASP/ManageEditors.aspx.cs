using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;

public partial class ManagerPages_ManageEditors : System.Web.UI.Page
{
    static string username;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("admin"))
            Response.Redirect("~/Default.aspx");
    }

    private void updateAdmin()
    {
        string query = "update Options set EditOp = 1, CommentOp = 1,DeleteOp = 1,AcceptOp = 1,AddOp=1 where UserId = (Select UserId from Users where UserName = @username)";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
        con.Open();
        SqlCommand qcom = new SqlCommand(query, con);
        qcom.Parameters.AddWithValue("username", username);
        qcom.ExecuteNonQuery();
        con.Close();
    }

    private void updateUser()
    {
        string query = "update Options set EditOp = 0, CommentOp = 1,DeleteOp = 0,AcceptOp = 1,AddOp=1 where UserId = (Select UserId from Users where UserName = @username)";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
        con.Open();
        SqlCommand qcom = new SqlCommand(query, con);
        qcom.Parameters.AddWithValue("username", username);
        qcom.ExecuteNonQuery();
        con.Close();
    }

    private void update(string query, string user)
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alexandru\Documents\BazaDeStiri.mdf;Integrated Security=True;Connect Timeout=30");
        con.Open();
        SqlCommand qcom = new SqlCommand(query, con);
        qcom.Parameters.AddWithValue("user", user);
        qcom.ExecuteNonQuery();
        con.Close();
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
                Response.Redirect("ManageEditors.aspx");
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
                Response.Redirect("ManageEditors.aspx");
            }
        }

        else if (e.CommandName == "Make")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string role, role_ru = "registered_user";
            username = GridView1.Rows[index].Cells[1].Text;
            role = GridView1.Rows[index].Cells[3].Text;

            if (username != null && role != null)
            {
                int length = Roles.GetAllRoles().Length;
                string[] roles = Roles.GetAllRoles();
                if (!roles.Contains(role_ru)) Roles.CreateRole("registered_user");
                Roles.RemoveUserFromRole(username, role);
                Roles.AddUserToRole(username, "registered_user");
                updateUser();
                Response.Redirect("ManageEditors.aspx");
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
                updateAdmin();
                Response.Redirect("ManageUsers.aspx");
            }
        }

        else if (e.CommandName == "AllowAdd")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set AddOp = 1 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageEditors.aspx");
        }

        else if (e.CommandName == "BlockAdd")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set AddOp = 0 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageEditors.aspx");
        }

        else if (e.CommandName == "AllowEdit")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set EditOp = 1 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageEditors.aspx");
        }

        else if (e.CommandName == "BlockEdit")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set EditOp = 0 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageEditors.aspx");
        }

        else if (e.CommandName == "AllowDelete")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set DeleteOp = 1 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageEditors.aspx");
        }

        else if (e.CommandName == "BlockDelete")
        {
            string user = e.CommandArgument.ToString();
            string query = "update Options set DeleteOp = 0 where UserId = @user";
            update(query, user);
            Response.Redirect("ManageEditors.aspx");
        }

    }
}