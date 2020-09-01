using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using CommunicationLayer;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.Security;

namespace RainbowFeeSystem.school_login
{
    public partial class ManageFees : System.Web.UI.Page
    {
        FeeGridCL feeGridClass = new FeeGridCL();
        PaymentDetailsBLL paymentBLL = new PaymentDetailsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DateTime dateHosting = DateTime.UtcNow;
                    TimeZoneInfo indianZoneId = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime dateNow = TimeZoneInfo.ConvertTimeFromUtc(dateHosting, indianZoneId);
                    lblDate.Text = dateNow.ToString();
                    if (!Request.IsAuthenticated)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    else
                    {
                        getGridData();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        private void getGridData()
        {
            if (Session["FeeData"] == null)
            {
                Session["FeeData"] = paymentBLL.viewFeeDB();
                Session.Timeout = 5;
                Collection<FeeGridCL> getGrid = paymentBLL.viewFeeDB();//Gets fee data from Database.
                var getGridQuery = getGrid.Where(x => x.paymentDetailId >= 0);//Converts Grid Collection to Variable to filter it.
                if (txtAdmissionNo.Text == string.Empty && txtClass.Text == string.Empty && txtSection.Text == string.Empty && ddlPaid.SelectedValue != "Yes" && ddlPaid.SelectedValue != "No" && txtDateFrom.Text == string.Empty && txtDateTo.Text == string.Empty)
                {
                    grdViewFees.DataSource = getGrid;
                    grdViewFees.DataBind();
                }
                else//If filters are not empty.
                {
                    if (txtAdmissionNo.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where x.admissionNo.ToString().Contains(txtAdmissionNo.Text) select x;
                    }
                    if (txtClass.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where x.classsection.Remove(x.classsection.Length - 1).Contains(txtClass.Text) select x;
                    }
                    if (txtSection.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where x.classsection.Substring(x.classsection.Length - 1).Contains(txtSection.Text) select x;
                    }
                    if (ddlPaid.SelectedValue == "Yes" || ddlPaid.SelectedValue == "No")
                    {
                        getGridQuery = from x in getGridQuery where x.isPaid == ddlPaid.SelectedValue select x;
                    }
                    if (txtDateFrom.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where Convert.ToDateTime(x.dateModified) >= Convert.ToDateTime(txtDateFrom.Text) select x;
                    }
                    if (txtDateTo.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where Convert.ToDateTime(x.dateModified) <= Convert.ToDateTime(txtDateTo.Text) select x;
                    }
                    Collection<FeeGridCL> newGetGrid = new Collection<FeeGridCL>();
                    foreach (var x in getGridQuery)//Converts variable to Collection of Fee Griid CL.
                    {
                        newGetGrid.Add(new FeeGridCL()
                        {
                            adminCharges = x.adminCharges,
                            examinationFee = x.examinationFee,
                            admissionFee = x.admissionFee,
                            admissionNo = x.admissionNo,
                            annualCharges = x.annualCharges,
                            classsection = x.classsection,
                            computerFeeMonthly = x.computerFeeMonthly,
                            computerFeeYearly = x.computerFeeYearly,
                            developmentChargesYearly = x.developmentChargesYearly,
                            isPaid = x.isPaid,
                            labFee = x.labFee,
                            leftFeeId = x.leftFeeId,
                            month = x.month,
                            name = x.name,
                            paymentDetailId = x.paymentDetailId,
                            projectFee = x.projectFee,
                            refreshmentAccFee = x.refreshmentAccFee,
                            smartClassCharges = x.smartClassCharges,
                            studentId = x.studentId,
                            totalFee = x.totalFee,
                            tutionFee = x.tutionFee,
                            dateCreated = x.dateCreated,
                            dateModified = x.dateModified,
                            lateFee = x.lateFee,
                            transportFee = x.transportFee,
                        });
                    }

                    grdViewFees.DataSource = newGetGrid;
                    grdViewFees.DataBind();
                }
            }
            else
            {
                Collection<FeeGridCL> getGrid = (Collection<FeeGridCL>)Session["FeeData"];//Gets Data from Session.
                var getGridQuery = getGrid.Where(x => x.paymentDetailId >= 0);//Converts Grid Collection to Variable to filter it.
                if (txtAdmissionNo.Text == string.Empty && txtClass.Text == string.Empty && txtSection.Text == string.Empty && ddlPaid.SelectedValue != "Yes" && ddlPaid.SelectedValue != "No" && txtDateFrom.Text == string.Empty && txtDateTo.Text == string.Empty)
                {
                    grdViewFees.DataSource = getGrid;
                    grdViewFees.DataBind();
                }
                else//If filters are not empty.
                {
                    if (txtAdmissionNo.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where x.admissionNo.ToString().Contains(txtAdmissionNo.Text) select x;
                    }
                    if (txtClass.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where x.classsection.Remove(x.classsection.Length - 1).Contains(txtClass.Text) select x;
                    }
                    if (txtSection.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where x.classsection.Substring(x.classsection.Length - 1).Contains(txtSection.Text) select x;
                    }
                    if (ddlPaid.SelectedValue == "Yes" || ddlPaid.SelectedValue == "No")
                    {
                        getGridQuery = from x in getGridQuery where x.isPaid == ddlPaid.SelectedValue select x;
                    }
                    if (txtDateFrom.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where Convert.ToDateTime(x.dateModified) >= Convert.ToDateTime(txtDateFrom.Text) select x;
                    }
                    if (txtDateTo.Text != string.Empty)
                    {
                        getGridQuery = from x in getGridQuery where Convert.ToDateTime(x.dateModified) <= Convert.ToDateTime(txtDateTo.Text) select x;
                    }
                    Collection<FeeGridCL> newGetGrid = new Collection<FeeGridCL>();
                    foreach (var x in getGridQuery)//Converts variable to Collection of Fee Griid CL.
                    {
                        newGetGrid.Add(new FeeGridCL()
                        {
                            adminCharges = x.adminCharges,
                            examinationFee = x.examinationFee,
                            admissionFee = x.admissionFee,
                            admissionNo = x.admissionNo,
                            annualCharges = x.annualCharges,
                            classsection = x.classsection,
                            computerFeeMonthly = x.computerFeeMonthly,
                            computerFeeYearly = x.computerFeeYearly,
                            developmentChargesYearly = x.developmentChargesYearly,
                            isPaid = x.isPaid,
                            labFee = x.labFee,
                            leftFeeId = x.leftFeeId,
                            month = x.month,
                            name = x.name,
                            paymentDetailId = x.paymentDetailId,
                            projectFee = x.projectFee,
                            refreshmentAccFee = x.refreshmentAccFee,
                            smartClassCharges = x.smartClassCharges,
                            studentId = x.studentId,
                            totalFee = x.totalFee,
                            tutionFee = x.tutionFee,
                            dateCreated = x.dateCreated,
                            dateModified = x.dateModified,
                            lateFee = x.lateFee,
                            transportFee = x.transportFee,
                        });
                    }

                    grdViewFees.DataSource = newGetGrid;
                    grdViewFees.DataBind();
                }
            }
        }

        protected void grdViewFees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdViewFees.PageIndex = e.NewPageIndex;
                getGridData();
            }
            catch (Exception ex)
            {

                throw (new Exception(ex.Message));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                getGridData();
            }
            catch (Exception ex)
            {

                throw (new Exception(ex.Message));
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                 "attachment;filename=ExportExcel.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grdViewFees.AllowPaging = false;
                getGridData();
                for (int i = 0; i < grdViewFees.Rows.Count; i++)
                {
                    GridViewRow row = grdViewFees.Rows[i];
                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                grdViewFees.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        //To Export all pages
                        grdViewFees.AllowPaging = false;
                        getGridData();

                        grdViewFees.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=ExportPDF.pdf");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Write(pdfDoc);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        protected void btnCsv_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                 "attachment;filename=ExportCSV.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";

                grdViewFees.AllowPaging = false;
                getGridData();

                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < grdViewFees.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(grdViewFees.Columns[k].HeaderText + ',');
                }
                //append new line
                sb.Append("\r\n");
                for (int i = 0; i < grdViewFees.Rows.Count; i++)
                {
                    for (int k = 0; k < grdViewFees.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(grdViewFees.Rows[i].Cells[k].Text + ',');
                    }
                    //append new line
                    sb.Append("\r\n");
                }
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }

            catch (Exception ex)
            {

                throw (new Exception(ex.Message));
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
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

        protected void btnDeleteFees_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeleteFees.aspx");
        }
    }
}