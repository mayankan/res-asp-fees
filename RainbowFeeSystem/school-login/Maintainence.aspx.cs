using BusinessLogicLayer;
using CommunicationLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RainbowFeeSystem.school_login
{
    public partial class Maintainence : System.Web.UI.Page
    {
        MaintainenceBLL maintainBLL = new MaintainenceBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MaintainenceCL maintainStatus = maintainBLL.getStatus();
                if (!IsPostBack)
                {
                    if (!Request.IsAuthenticated)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    txtNote.Text = maintainStatus.homeNote.ToString().Replace("<br />","\n");
                    btnMaintainence.Text=(maintainStatus.isOffline)?"On":"Off";
                }
            }
            catch (Exception ex)
            {

                throw (new Exception(ex.Message));
            }
        }

        protected void btnMaintainence_Click(object sender, EventArgs e)
        {
            MaintainenceCL updateOffline = new MaintainenceCL();
            if(btnMaintainence.Text=="On")
            {
                updateOffline.isOffline = false;
            }
            else
            {
                updateOffline.isOffline = true;
            }
            maintainBLL.updateOffline(updateOffline);
            Response.Redirect("Maintainence.aspx");
        }

        protected void btnMaintainPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Maintainence.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MaintainenceCL note = new MaintainenceCL();
            note.homeNote = txtNote.Text.ToString().Replace("\n", "<br />");
            note.isOffline = (btnMaintainence.Text == "On") ? true : false;
            maintainBLL.updateNote(note);
            lblSuccessful.Text = "Note has been successfully updated.";
            Response.AppendHeader("Refresh", "5;url=Maintainence.aspx");
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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void btnDeleteFees_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeleteFees.aspx");
        }

    }
}