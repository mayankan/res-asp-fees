using BusinessLogicLayer;
using CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RainbowFeeSystem.school_login
{
    public partial class DeleteFee : System.Web.UI.Page
    {
        PaymentDetailsBLL paymentBLL = new PaymentDetailsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
            }
        }
        protected void btnMgmtStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageStudent.aspx");
        }

        protected void btnMgmtFees_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void btnCrtStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateStudent.aspx");
        }

        protected void btnCrtFees_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateFees.aspx");
        }

        protected void btnMaintainPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Maintainence.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void btnDeleteFees_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeleteFees.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DateTime month = Convert.ToDateTime(Request.Form[txtMonth.UniqueID]);
            int deletedData = paymentBLL.deleteFeeDatabyMonth(month);
            lblUpdate.Text = deletedData + " entries deleted from the data. This page will refresh in 5 seconds.";
            Response.AppendHeader("Refresh", "5;url=DeleteFees.aspx");
        }
    }
}