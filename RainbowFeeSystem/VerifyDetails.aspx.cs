using BusinessLogicLayer;
using CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RainbowFeeSystem
{
    public partial class VerifyDetails1 : System.Web.UI.Page
    {
        FrontUserBLL userBLL = new FrontUserBLL();
        PaymentDetailsBLL paymentBLL = new PaymentDetailsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int admissionNo = Convert.ToInt32(Session["AdmNo"]);
                    long MobileNo = Convert.ToInt64(Session["MobNo"]);
                    StudentCL getUserbyMobNo = userBLL.getStudentByMobileNo(MobileNo,admissionNo);
                    GlobalVariables.SetGlobalLong(getUserbyMobNo.id);
                    lblAddress.Text = getUserbyMobNo.address;
                    lblAdmissionNo.Text = getUserbyMobNo.admissionNo.ToString();
                    lblClass.Text = getUserbyMobNo.studentClass + " - " + getUserbyMobNo.section;
                    lblFatherName.Text = getUserbyMobNo.fathersname;
                    lblGender.Text = (getUserbyMobNo.gender) ? "Male" : "Female";
                    lblMotherName.Text = getUserbyMobNo.mothername;
                    lblStudentName.Text = getUserbyMobNo.name;
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
        protected void btnProceed_Click(object sender, EventArgs e)
        {
            if(chckConfirm.Checked==true)
            {
                Session["StudentId"] = GlobalVariables.GlobalLong;
                Response.Redirect("FeeDetail.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check the box.')", true);
            }
        }
    }
}