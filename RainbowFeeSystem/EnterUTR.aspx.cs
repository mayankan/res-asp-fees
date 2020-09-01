using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RainbowFeeSystem
{
    public partial class EnterURN : System.Web.UI.Page
    {
        PaymentDetailsBLL paymentBLL = new PaymentDetailsBLL();
        FrontUserBLL studentBLL = new FrontUserBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblAdmissionNo.Text = Session["admNo"].ToString();
                    lblStudentName.Text = Session["studentName"].ToString();
                    lblMobileNo.Text = Session["fatherNo"].ToString();
                    lblTotalFees.Text = Session["amt"].ToString();
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string UTRNo = txtUTR.Text;
            string AdmissionNo = lblAdmissionNo.Text;
            string MobileNo = lblMobileNo.Text;
            string StudentName = lblStudentName.Text;
            string TotalFee = lblTotalFees.Text;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtpout.asia.secureserver.net";
            smtpClient.Port = 3535;
            smtpClient.Credentials = new NetworkCredential("mail@rainbowonlinefees.com", "rainbowschool1234");
            smtpClient.EnableSsl = false;

            MailMessage mail = new MailMessage();
            //Setting From , To and CC
            mail.From = new MailAddress("mail@rainbowschooljp.com", "Rainbow English Sr. Sec. School");
            mail.To.Add(new MailAddress("feepayment@rainbowschooljp.com"));
            //mail.CC.Add(new MailAddress("rainbow_school@rediffmail.com"));
            mail.Subject = "Mail Regarding Fee Payment";
            mail.IsBodyHtml = true;
            mail.Body = "UTR Number - " + UTRNo + "<br/>" + "Student Admission Number - " + AdmissionNo + "<br/>" + "Student Name - " + StudentName + "<br/>" + " Total Fee Amount - " + TotalFee + "<br/>" + "Rainbow English Sr. Sec. School";
            smtpClient.Send(mail);
            lblUpdate.Text = "Reference Number has been recieved by us. You will be redirected to homepage in 5 seconds.";
            Response.AppendHeader("Refresh", "5;url=index.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("FeeDetail.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("index.aspx");
        }
    }
}