using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using CommunicationLayer;

namespace RainbowFeeSystem
{
    public partial class VerifyDetails : System.Web.UI.Page
    {
        FrontUserBLL frontUserBLL = new FrontUserBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int admissionNo = Convert.ToInt32(Session["AdmNo"]);
                    StudentCL getUser = frontUserBLL.getStudentByAdmissionNo(admissionNo);
                    Session["MobNo"] = getUser.mobileNumber;
                    lblMobNo.Text = Session["MobNo"].ToString().Substring(0, 6) + "XXXX";
                }
            }
            catch (Exception ex)
            {
                MsgBox("404 Not Found", this.Page, this);
                Response.Redirect("index.aspx");
            }
        }
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        protected void btnMobileNo_Click(object sender, EventArgs e)
        {
            if (txtMobileNo.Text == Convert.ToString(Session["MobNo"]).Substring(6, 4))
            {
                Response.Redirect("VerifyDetails.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Mobile Number Not Correct. Please retry.')", true);
            }
        }
    }
}