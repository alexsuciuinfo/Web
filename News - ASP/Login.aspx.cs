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

public partial class Account_Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RegisterHyperLink.NavigateUrl = "Register.aspx";
       

        var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        if (!String.IsNullOrEmpty(returnUrl))
        {
           // RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
        }
    }

    protected void myLogin_LoginError(object sender, EventArgs e)
    {
        // Determine why the user could not login...        
        myLogin.FailureText = "Your login attempt was not successful. Please try again.";

        // Does there exist a User account for this user?
        MembershipUser usrInfo = Membership.GetUser(myLogin.UserName);
        if (usrInfo != null)
        {
            // Is this user locked out?
            if (usrInfo.IsLockedOut)
            {
                myLogin.FailureText = "Your account has been locked out because of too many invalid login attempts. Please contact the administrator to have your account unlocked.";
            }
            else if (!usrInfo.IsApproved)
            {
                myLogin.FailureText = "Your account has been blocked or is not yet approved ! You cannot login until an administrator has approved your account.";
            }
        }
    }
}