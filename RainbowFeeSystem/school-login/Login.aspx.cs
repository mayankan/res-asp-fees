using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RainbowFeeSystem.school_login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // Three valid username/password pairs: Scott/password, Jisun/password, and Sam/password.
            string[] users = { "Mayank", "FeeCounter", "Admin" };
            string[] passwords = { "mayankanan20", "adminfee@12345", "rainbow@12345" };
            for (int i = 0; i < users.Length; i++)
            {
                bool validUsername = (string.Compare(UserName.Text, users[i], true) == 0);
                bool validPassword = (string.Compare(Password.Text, passwords[i], false) == 0);
                if (validUsername && validPassword)
                {
                    Session["User"] = UserName.Text;
                    Session["Login"] = true;
                    FormsAuthentication.RedirectFromLoginPage(UserName.Text, true);
                    // TODO: Log in the user...
                }
            }
            // If we reach here, the user's credentials were invalid
            InvalidCredentialsMessage.Visible = true;
        }
    }
}