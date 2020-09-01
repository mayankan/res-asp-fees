using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.ObjectModel;
using CommunicationLayer;
using BusinessLogicLayer;
using System.Web.Security;

namespace RainbowFeeSystem.school_login
{
    public partial class CreateFees : System.Web.UI.Page
    {
        PaymentDetailsBLL paymentBLL = new PaymentDetailsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnConfirm.Visible = false;
                txtMonth.Visible = false;
                if (!Request.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }

        public void BindDataGridPaging()
        {
            //Creating a datatable object
            DataTable tblcsv = new DataTable();
            //Adding the first row which is pre-defined.
            tblcsv.Columns.AddRange(new DataColumn[16] { new DataColumn("Admission Number", typeof(int)),
                    new DataColumn("Tution Fee", typeof(long)),
                    new DataColumn("Admission Fee",typeof(long)),
                    new DataColumn("Refreshment Accessories Fee",typeof(long)),
                    new DataColumn("Laboratory Fee",typeof(long)),
                    new DataColumn("Project Fee",typeof(long)),
                    new DataColumn("Annual Charges",typeof(long)),
                    new DataColumn("Administration Charges",typeof(long)),
                    new DataColumn("Smart Class Charges",typeof(long)),
                    new DataColumn("Computer Fee(Yearly)",typeof(long)),
                    new DataColumn("Computer Fee(Monthly)",typeof(long)),
                    new DataColumn("Development Fund(Yearly)",typeof(long)),
                    new DataColumn("Examination Fee", typeof(long)),
                    new DataColumn("Transport Fee", typeof(long)),
                    new DataColumn("Late Fee", typeof(long)),
                    new DataColumn("Total Fee",typeof(long)) });
            //Getting the CSV Location from session which is saved in temporary storage.
            string CSVLocation = Session["CSVLocation"].ToString();
            //Reading all the text from CSV File.
            string ReadCSV = File.ReadAllText(CSVLocation);
            //spliting row after new line  
            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                //Checking whether the data is empty.
                if (!string.IsNullOrEmpty(csvRow))
                {
                    //Adding each row into datatable  
                    tblcsv.Rows.Add();
                    //Creating count variable for splitting data in columns.
                    int count = 0;
                    foreach (string x in csvRow.Split(','))
                    {
                        tblcsv.Rows[tblcsv.Rows.Count - 1][count] = x;
                        count++;
                    }
                }
            }
            //Calling Bind Grid Functions  
            grdViewFees.DataSource = tblcsv;
            grdViewFees.DataBind();
        }

        public void BindDataGrid()
        {
            //Creating object of datatable  
            DataTable tblcsv = new DataTable();
            //Adding the first row which is pre-defined.
            tblcsv.Columns.AddRange(new DataColumn[16] { new DataColumn("Admission Number", typeof(int)),
                    new DataColumn("Tution Fee", typeof(long)),
                    new DataColumn("Admission Fee",typeof(long)),
                    new DataColumn("Refreshment Accessories Fee",typeof(long)),
                    new DataColumn("Laboratory Fee",typeof(long)),
                    new DataColumn("Project Fee",typeof(long)),
                    new DataColumn("Annual Charges",typeof(long)),
                    new DataColumn("Administration Charges",typeof(long)),
                    new DataColumn("Smart Class Charges",typeof(long)),
                    new DataColumn("Computer Fee(Yearly)",typeof(long)),
                    new DataColumn("Computer Fee(Monthly)",typeof(long)),
                    new DataColumn("Development Fund(Yearly)",typeof(long)),
                    new DataColumn("Examination Fee", typeof(long)),
                    new DataColumn("Transport Fee", typeof(long)),
                    new DataColumn("Late Fee", typeof(long)),
                    new DataColumn("Total Fee",typeof(long)) });
            //Creating the CSV Location which is saved in temporary storage.
            string CSVFilePath = Path.GetTempFileName() + flExcelUpload.FileName;
            flExcelUpload.SaveAs(CSVFilePath);
            //Storing the CSV Location in Session for further usage.
            Session["CSVLocation"] = CSVFilePath;
            //Reading All text  
            string ReadCSV = File.ReadAllText(CSVFilePath);
            //spliting row after new line  
            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                //Checking whether the data is empty.
                if (!string.IsNullOrEmpty(csvRow))
                {
                    //Adding each row into datatable  
                    tblcsv.Rows.Add();
                    //Creating count variable for splitting data in columns.
                    int count = 0;
                    foreach (string x in csvRow.Split(','))
                    {
                        tblcsv.Rows[tblcsv.Rows.Count - 1][count] = x;
                        count++;
                    }
                }
            }
            //Calling Bind Grid Functions
            grdViewFees.DataSource = tblcsv;
            grdViewFees.DataBind();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //Creating Collection of FeeExcelCL for storing multiple data entries provided by excel file.
                Collection<FeeExcelCL> feeCol = new Collection<FeeExcelCL>();
                //Getting the CSV Location from session which is saved in temporary storage.
                string CSVLocation = Session["CSVLocation"].ToString();
                //Reading All text 
                string ReadCSV = File.ReadAllText(CSVLocation);
                //spliting row after new line  
                foreach (string csvRow in ReadCSV.Split('\n'))
                {
                    //Checking whether the data is empty.
                    if (!string.IsNullOrEmpty(csvRow))
                    {
                        //Creating StudentCL instance for storing single data entry in loop.
                        FeeExcelCL feeCL = new FeeExcelCL();
                        //Creating studentCount variable for counting student data entries.
                        int feeCount = 0;
                        //Creating count variable for splitting data in columns.
                        int count = 0;
                        //Loop for splitting the data and inputting the data in StudentCL intance.
                        foreach (string FileRec in csvRow.Split(','))
                        {
                            if (count == 0)
                            {
                                feeCL.admissionNo = Convert.ToInt32(FileRec);
                            }
                            if (count == 1)
                            {
                                feeCL.tutionFee = Convert.ToInt64(FileRec);
                            }
                            if (count == 2)
                            {
                                feeCL.admissionFee = Convert.ToInt64(FileRec);
                            }
                            if (count == 3)
                            {
                                feeCL.refreshmentAccFee = Convert.ToInt64(FileRec);
                            }
                            if (count == 4)
                            {
                                feeCL.labFee = Convert.ToInt64(FileRec);
                            }
                            if (count == 5)
                            {
                                feeCL.projectFee = Convert.ToInt64(FileRec);
                            }
                            if (count == 6)
                            {
                                feeCL.annualCharges = Convert.ToInt64(FileRec);
                            }
                            if (count == 7)
                            {
                                feeCL.adminCharges = Convert.ToInt64(FileRec);
                            }
                            if (count == 8)
                            {
                                feeCL.smartClassCharges = Convert.ToInt64(FileRec);
                            }
                            if (count == 9)
                            {
                                feeCL.computerFeeYearly = Convert.ToInt64(FileRec);
                            }
                            if (count == 10)
                            {
                                feeCL.computerFeeMonthly = Convert.ToInt64(FileRec);
                            }
                            if (count == 11)
                            {
                                feeCL.developmentChargesYearly = Convert.ToInt64(FileRec);
                            }
                            if (count == 12)
                            {
                                feeCL.examinationFee = Convert.ToInt64(FileRec);
                            }
                            if (count == 13)
                            {
                                feeCL.transportFee = Convert.ToInt64(FileRec);
                            }
                            if (count == 14)
                            {
                                feeCL.lateFee = Convert.ToInt64(FileRec);
                            }
                            if (count == 15)
                            {
                                feeCL.totalFee = Convert.ToInt64(FileRec);
                                continue;
                            }
                            count++;
                        }
                        //Increasing the studentCount to input in Id Field to store multiple entries in Entity Framework.
                        feeCount++;
                        feeCL.dateCreated = DateTime.Now;
                        feeCL.dateModified = DateTime.Now;
                        feeCL.isDeleted = false;
                        feeCL.id = feeCount;
                        feeCL.isPaid = false;
                        string month = Request.Form[txtMonth.UniqueID];
                        feeCL.month = Convert.ToDateTime(Request.Form[txtMonth.UniqueID]);
                        //Adding StudentCL instance in Collection instance.
                        feeCol.Add(feeCL);
                    }
                }
                //Creating a variable AdmissionCount fetching the Admission Number from database which exists.
                Collection<int> AdmissionNos = paymentBLL.getNoAdmissionNo(feeCol);
                //Condition checking Admission Number is present or not.
                if (AdmissionNos.Count() == 0)//If All Admission No Student Data is present in Application.
                {
                    paymentBLL.createFeeCollection(feeCol);
                    lblError.Text = "Fee Data entered successfully. The page will refresh in 3 seconds";
                    Response.AppendHeader("Refresh", "5;url=CreateFees.aspx");
                }
                else//This condition includes Admission No. who's student data is not present in Application.
                {
                    lblError.Text = "Admission Number Entered Incorrect/ Admission Number Not in Database.";
                    foreach (int item in AdmissionNos)
                    {
                        lblError.Text += item + ", ";
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (flExcelUpload.HasFile)
                {
                    BindDataGrid();
                    btnConfirm.Visible = true;
                    txtMonth.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        protected void grdViewFees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //Adding New Page Index in Grid View.
                grdViewFees.PageIndex = e.NewPageIndex;
                //Calling Bind Grid Functions
                BindDataGridPaging();
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
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