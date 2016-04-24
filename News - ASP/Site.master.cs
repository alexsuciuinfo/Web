using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class SiteMaster : MasterPage
{
   

  

    protected void Search_Click(object sender, EventArgs e)
    {

        string id_categorie = Server.UrlEncode(ListBox1.SelectedValue.ToString());
        Response.Redirect("~/Category.aspx?category=" + id_categorie);
    }

    protected void Button_search_click(object sender, EventArgs e)
    {
        String search = Server.UrlEncode(search_input.Text);
        Response.Redirect("~/Default.aspx?search=" + search);
    }

  
    protected void Page_Load(object sender, EventArgs e)
    {

    }

}