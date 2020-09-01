using BusinessLogicLayer;
using CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.Security;

namespace RainbowFeeSystem.school_login
{
    public partial class ManageStudent : System.Web.UI.Page
    {
        BackUserBLL userBLL = new BackUserBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
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

        private class StudentExcelGrid
        {
            public int id { get; set; }
            public string admissionNo { get; set; }
            public string name { get; set; }
            public string fathersname { get; set; }
            public string mothername { get; set; }
            public string classsection { get; set; }
            public string mobileNumber { get; set; }
            public string gender { get; set; }
            public string address { get; set; }
        }

        private void getGridData()
        {
            Collection<StudentCL> getGrid = userBLL.viewStudentDB();
            var getGridQuery = getGrid.Where(x => x.id >= 0);
            Collection<StudentExcelGrid> studentData = new Collection<StudentExcelGrid>();
            foreach (StudentCL item in getGrid)
            {
                studentData.Add(new StudentExcelGrid()
                {
                    address = item.address,
                    admissionNo = item.admissionNo.ToString(),
                    classsection = item.studentClass + item.section,
                    fathersname = item.fathersname,
                    gender = item.gender ? "Male" : "Female",
                    id = item.id,
                    mobileNumber = item.mobileNumber.ToString(),
                    mothername = item.mothername,
                    name = item.name,
                });
            }

            if (txtAdmissionNo.Text == string.Empty && txtClass.Text == string.Empty && txtSection.Text == string.Empty)
            {
                grdViewStudent.DataSource = studentData;
                grdViewStudent.DataBind();
            }
            else
            {
                if (txtAdmissionNo.Text != string.Empty)
                {
                    getGridQuery = from x in getGridQuery where x.admissionNo.ToString().Contains(txtAdmissionNo.Text) select x;
                }
                if (txtClass.Text != string.Empty)
                {
                    getGridQuery = from x in getGridQuery where x.studentClass.Contains(txtClass.Text) select x;
                }
                if (txtSection.Text != string.Empty)
                {
                    getGridQuery = from x in getGridQuery where x.section.Contains(txtSection.Text) select x;
                }
                Collection<StudentExcelGrid> newGetGrid = new Collection<StudentExcelGrid>();
                foreach (var x in getGridQuery)
                {
                    newGetGrid.Add(new StudentExcelGrid()
                    {
                        address = x.address,
                        admissionNo = x.admissionNo.ToString(),
                        classsection = x.studentClass + x.section,
                        fathersname = x.fathersname,
                        gender = x.gender ? "Male" : "Female",
                        id = x.id,
                        mobileNumber = x.mobileNumber.ToString(),
                        mothername = x.mothername,
                        name = x.name
                    });
                }
                grdViewStudent.DataSource = newGetGrid;
                grdViewStudent.DataBind();
            }
        }

        protected void grdViewStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdViewStudent.PageIndex = e.NewPageIndex;
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
                grdViewStudent.AllowPaging = false;
                getGridData();
                for (int i = 0; i < grdViewStudent.Rows.Count; i++)
                {
                    GridViewRow row = grdViewStudent.Rows[i];
                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                }
                grdViewStudent.RenderControl(hw);

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

        protected void btnCsv_Click(object sender, EventArgs e)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        //To Export all pages
                        grdViewStudent.AllowPaging = false;
                        getGridData();

                        grdViewStudent.RenderControl(hw);
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

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                 "attachment;filename=ExportCSV.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";

                grdViewStudent.AllowPaging = false;
                getGridData();

                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < grdViewStudent.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(grdViewStudent.Columns[k].HeaderText + ',');
                }
                //append new line
                sb.Append("\r\n");
                for (int i = 0; i < grdViewStudent.Rows.Count; i++)
                {
                    for (int k = 0; k < grdViewStudent.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(grdViewStudent.Rows[i].Cells[k].Text + ',');
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