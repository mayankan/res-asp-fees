using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using CommunicationLayer;
using System.Collections.ObjectModel;
using Instamojo.NET;
using Instamojo.NET.Models;

namespace RainbowFeeSystem
{
    public partial class FeeDetail : System.Web.UI.Page
    {
        PaymentDetailsBLL paymentBLL = new PaymentDetailsBLL();
        FrontUserBLL studentBLL = new FrontUserBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int admissionNo = Convert.ToInt32(Session["AdmNo"]);
                    long MobileNo = Convert.ToInt64(Session["MobNo"]);
                    StudentCL getUserbyMobNo = studentBLL.getStudentByMobileNo(MobileNo, admissionNo);
                    lblAdmissionNo.Text = getUserbyMobNo.admissionNo.ToString();
                    lblStudentName.Text = getUserbyMobNo.name.ToString();
                    lblMobileNo.Text = getUserbyMobNo.mobileNumber.ToString();
                    Collection<LeftFeesCL> leftFeeDetailbyStudentId = paymentBLL.getLeftFeeCollection(getUserbyMobNo.id);
                    Collection<LeftFeesCL> leftDuesByStudentId = paymentBLL.getLeftFeeDueCollection(getUserbyMobNo.id);
                    grdFeeDetails.DataSource = leftFeeDetailbyStudentId;
                    grdFeeDetails.DataBind();
                    grdLateFees.DataSource = leftDuesByStudentId;
                    grdLateFees.DataBind();
                    long totalFee = (leftFeeDetailbyStudentId.Sum(x => x.totalFee) + leftDuesByStudentId.Sum(x => x.totalFee));
                    if (totalFee == 0)
                    {
                        lblTransFees.Visible = false;
                        lblTotalLeftAmt.Text = "No Fees Available.";
                        lblTransFeeNote.Visible = false;
                        btnPayInsta.Visible = false;
                        btnRtgs.Visible = false;
                    }
                    else
                    {
                        lblTransFees.Visible = true;
                        lblTotalLeftAmt.Text = (leftFeeDetailbyStudentId.Sum(x => x.totalFee) + leftDuesByStudentId.Sum(x => x.totalFee)).ToString();
                    }
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

        protected void grdFeeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LeftFeesCL item = e.Row.DataItem as LeftFeesCL;
            if (item != null)
            {
                if (item.tutionFee == 0)
                {
                    grdFeeDetails.Columns[0].Visible = false;
                }
                if (item.admissionFee == 0)
                {
                    grdFeeDetails.Columns[1].Visible = false;
                }
                if (item.examinationFee == 0)
                {
                    grdFeeDetails.Columns[2].Visible = false;
                }
                if (item.refreshmentAccFee == 0)
                {
                    grdFeeDetails.Columns[3].Visible = false;
                }
                if (item.labFee == 0)
                {
                    grdFeeDetails.Columns[4].Visible = false;
                }
                if (item.projectFee == 0)
                {
                    grdFeeDetails.Columns[5].Visible = false;
                }
                if (item.annualCharges == 0)
                {
                    grdFeeDetails.Columns[6].Visible = false;
                }
                if (item.adminCharges == 0)
                {
                    grdFeeDetails.Columns[7].Visible = false;
                }
                if (item.smartClassCharges == 0)
                {
                    grdFeeDetails.Columns[8].Visible = false;
                }
                if (item.computerFeeYearly == 0)
                {
                    grdFeeDetails.Columns[9].Visible = false;
                }
                if (item.computerFeeMonthly == 0)
                {
                    grdFeeDetails.Columns[10].Visible = false;
                }
                if (item.developmentChargesYearly == 0)
                {
                    grdFeeDetails.Columns[11].Visible = false;
                }
                if (item.transportFee == 0)
                {
                    grdFeeDetails.Columns[12].Visible = false;
                }
                if (item.lateFee == 0)
                {
                    grdFeeDetails.Columns[13].Visible = false;
                }
                if (item.totalFee == 0)
                {
                    grdFeeDetails.Columns[14].Visible = false;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected async void btnPayInsta_Click(object sender, EventArgs e)
        {
            long TotalAmountWithoutTransCost = Convert.ToInt64(lblTotalLeftAmt.Text);
            double TotalAmountWithTransCost1 = (TotalAmountWithoutTransCost + 1.18 * 0);
            double TotalAmountWithTransCost2 = (TotalAmountWithTransCost1 / (1 - 1.18 * 1.9 / 100));
            TotalAmountWithTransCost2 = Math.Round(TotalAmountWithTransCost2, 2);
            Instamojo.NET.Instamojo im = new Instamojo.NET.Instamojo("74daa5061b049d6cdc8540a79cfd7a1a", "a1ff98eeb01b5358e479494464b62849");
            PaymentRequest pr = new PaymentRequest();
            pr.allow_repeated_payments = false;
            pr.amount = TotalAmountWithTransCost2.ToString();
            pr.buyer_name = lblStudentName.Text;
            pr.email = "mail@rainbowschooljp.com";
            pr.phone = lblMobileNo.Text;
            pr.send_email = true;
            pr.send_sms = true;
            pr.redirect_url = "http://rainbowonlinefees.com/SuccessfulPayment.aspx";
            pr.webhook = "";
            pr.purpose = "Online Fee Payment";
            PaymentRequest npr = await im.CreatePaymentRequest(pr);
            String PaymentURL = npr.longurl;
            String PaymentRequestId = npr.id;
            Session.Clear();
            Response.Redirect(PaymentURL);
        }

        protected void btnRtgs_Click(object sender, EventArgs e)
        {
            Session["admNo"] = lblAdmissionNo.Text;
            Session["fatherNo"] = lblMobileNo.Text;
            Session["studentName"] = lblStudentName.Text;
            Session["amt"] = lblTotalLeftAmt.Text;
            Response.Redirect("EnterUTR.aspx");
        }

    }
}