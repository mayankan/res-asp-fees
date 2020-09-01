using BusinessLogicLayer;
using CommunicationLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RainbowFeeSystem
{
    public partial class index : System.Web.UI.Page
    {
        MaintainenceBLL maintainBLL = new MaintainenceBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            try
            {
                MaintainenceCL maintainStatus = maintainBLL.getStatus();
                if (!IsPostBack)
                {
                    BackendText.Text = maintainStatus.homeNote;
                }
                if(!maintainStatus.isOffline)
                {
                    IsOffline.Visible = true;
                    NotOffline.Visible = false;
                }
                else
                {
                    IsOffline.Visible = false;
                    NotOffline.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAdmissionNo_Click(object sender, EventArgs e)
        {
            int AdmissionNumber = Convert.ToInt32(txtAdmissionNo.Text);
            Session["AdmNo"] = AdmissionNumber;
            Response.Redirect("VerifyNumber.aspx");
        }

        protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                if (e.Item.Text == SiteMap.CurrentNode.Title)
                {
                    if (e.Item.Parent != null)
                    {
                        e.Item.Parent.Selected = true;
                    }
                    else
                    {
                        e.Item.Selected = true;
                    }
                }
            }
        }
    }
}