using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using CommunicationLayer;
using System.Web.Security;

namespace RainbowFeeSystem.school_login
{
    public partial class CreateStudent : System.Web.UI.Page
    {
        //Instantiating the Business Logic Layer Class for Data creation method call.
        BackUserBLL backUserBLL = new BackUserBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Request.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                } 
            }
        }

        private void BindDataGridPaging()
        {
            //Creating a datatable object
            DataTable tblcsv = new DataTable();
            //Adding the first row which is pre-defined.
            tblcsv.Columns.AddRange(new DataColumn[8] { new DataColumn("Admission Number", typeof(int)),
                    new DataColumn("Name", typeof(string)),
                    new DataColumn("Father's Name",typeof(string)),
                    new DataColumn("Mother's Name",typeof(string)),
                    new DataColumn("Class Section",typeof(string)),
                    new DataColumn("Mobile Number",typeof(long)),
                    new DataColumn("Gender",typeof(string)),
                    new DataColumn("Address",typeof(string)) });
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
                    //Creating Address Count Varaible for splitting Address Column from other data.
                    int AddressCount = 0;
                    //Creating an array to split Address Column from Other Columns.
                    string[] def = csvRow.Split('"');
                    //Checking whether Address field has commas with "" or not.
                    if (def.Count() == 1)
                    {
                        foreach (string x in csvRow.Split(','))
                        {
                            tblcsv.Rows[tblcsv.Rows.Count - 1][count] = x;
                            count++;
                        }
                    }
                    //Address field doesn't have commas and "".
                    else
                    {
                        foreach (string FileRec in def.Take(def.Length - 1))
                        {
                            string[] abc = FileRec.Split(',');
                            foreach (string x in abc.Take(abc.Length - 1))
                            {
                                if (AddressCount != 0) break;
                                tblcsv.Rows[tblcsv.Rows.Count - 1][count] = x;
                                count++;
                            }
                            if (AddressCount == 0)
                            {
                                AddressCount++;
                                continue;
                            }
                            tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                            count++;
                        }
                    }
                }
            }
            //Calling Bind Grid Functions  
            grdViewStudent.DataSource = tblcsv;
            grdViewStudent.DataBind();
        }

        private void BindDataGrid()
        {
            try
            {
                //Creating object of datatable  
                DataTable tblcsv = new DataTable();
                //Adding the first row which is pre-defined.
                tblcsv.Columns.AddRange(new DataColumn[8] { new DataColumn("Admission Number", typeof(int)),
                    new DataColumn("Name", typeof(string)),
                    new DataColumn("Father's Name",typeof(string)),
                    new DataColumn("Mother's Name",typeof(string)),
                    new DataColumn("Class Section",typeof(string)),
                    new DataColumn("Mobile Number",typeof(long)),
                    new DataColumn("Gender",typeof(string)),
                    new DataColumn("Address",typeof(string)) });
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
                        //Creating Address Count Varaible for splitting Address Column from other data.
                        int AddressCount = 0;
                        //Creating an array to split Address Column from Other Columns.
                        string[] def = csvRow.Split('"');
                        //Checking whether Address field has commas with "" or not.
                        if (def.Count() == 1)
                        {
                            foreach (string x in csvRow.Split(','))
                            {
                                tblcsv.Rows[tblcsv.Rows.Count - 1][count] = x;
                                count++;
                            }
                        }
                        //Address field doesn't have commas and "".
                        else
                        {
                            foreach (string FileRec in def.Take(def.Length - 1))
                            {
                                string[] abc = FileRec.Split(',');
                                foreach (string x in abc.Take(abc.Length - 1))
                                {
                                    if (AddressCount != 0) break;
                                    tblcsv.Rows[tblcsv.Rows.Count - 1][count] = x;
                                    count++;
                                }
                                if (AddressCount == 0)
                                {
                                    AddressCount++;
                                    continue;
                                }
                                tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                                count++;
                            }
                        }
                    }
                }
                //Calling Bind Grid Functions
                grdViewStudent.DataSource = tblcsv;
                grdViewStudent.DataBind();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (flExcelUpload.HasFile)
            {
                try
                {
                    if (flExcelUpload.HasFile)
                    {
                        BindDataGrid(); 
                    }
                }
                catch (FieldAccessException)
                {
                    throw(new Exception("The file format is not correct or the data in file is not in correct format."));
                }
                catch(Exception ex)
                {
                    throw (new Exception(ex.Message));
                }
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //Creating Collection of StudentCL for storing multiple data entries provided by excel file.
                Collection<StudentCL> studentCol = new Collection<StudentCL>();
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
                        StudentCL studentCL = new StudentCL();
                        //Creating studentCount variable for counting student data entries.
                        int studentCount = 0;
                        //Creating count variable for splitting data in columns.
                        int count = 0;
                        //Creating Address Count Varaible for splitting Address Column from other data.
                        int AddressCount = 0;
                        //Creating an array to split Address Column from Other Columns.
                        string[] def = csvRow.Split('"');
                        //Checking whether Address field has commas with "" or not.
                        if (def.Count() == 1)
                        {
                            //Loop for splitting the data and inputting the data in StudentCL intance.
                            foreach (string FileRec in csvRow.Split(','))
                            {
                                if (count == 0)
                                {
                                    studentCL.admissionNo = Convert.ToInt32(FileRec);
                                }
                                if (count == 1)
                                {
                                    studentCL.name = FileRec;
                                }
                                if (count == 2)
                                {
                                    studentCL.fathersname = FileRec;
                                }
                                if (count == 3)
                                {
                                    studentCL.mothername = FileRec;
                                }
                                if (count == 4)
                                {
                                    studentCL.studentClass = FileRec.Remove(FileRec.Length - 1);
                                    studentCL.section = FileRec.Substring(FileRec.Length - 1);
                                }
                                if (count == 5)
                                {
                                    studentCL.mobileNumber = Convert.ToInt64(FileRec);
                                }
                                if (count == 6)
                                {
                                    bool gender = true;
                                    if (FileRec == "Male")
                                        gender = true;
                                    else
                                        gender = false;
                                    studentCL.gender = gender;
                                }
                                if (count == 7)
                                {
                                    studentCL.address = FileRec;
                                    break;
                                }
                                count++;
                            }
                        }
                        //Address field doesn't have commas and "".
                        else
                        {
                            //Loop for splitting the data and inputting the data in StudentCL intance.
                            foreach (string FileRec in def.Take(def.Length - 1))
                            {
                                string[] abc = FileRec.Split(',');
                                foreach (string x in abc.Take(abc.Length - 1))
                                {
                                    if (AddressCount != 0) break;
                                    if (count == 0)
                                    {
                                        studentCL.admissionNo = Convert.ToInt32(x);
                                    }
                                    if (count == 1)
                                    {
                                        studentCL.name = x;
                                    }
                                    if (count == 2)
                                    {
                                        studentCL.fathersname = x;
                                    }
                                    if (count == 3)
                                    {
                                        studentCL.mothername = x;
                                    }
                                    if (count == 4)
                                    {
                                        studentCL.studentClass = x.Remove(x.Length - 1);
                                        studentCL.section = x.Substring(x.Length - 1);
                                    }
                                    if (count == 5)
                                    {
                                        studentCL.mobileNumber = Convert.ToInt64(x);
                                    }
                                    if (count == 6)
                                    {
                                        bool gender = true;
                                        if (x == "Male")
                                            gender = true;
                                        else
                                            gender = false;
                                        studentCL.gender = gender;
                                    }
                                    count++;
                                }
                                if (AddressCount == 0)
                                {
                                    AddressCount++;
                                    continue;
                                }
                                studentCL.address = FileRec;
                            }
                        }
                        //Increasing the studentCount to input in Id Field to store multiple entries in Entity Framework.
                        studentCount++;
                        studentCL.dateCreated = DateTime.Now;
                        studentCL.dateModified = DateTime.Now;
                        studentCL.isDeleted = false;
                        studentCL.id = studentCount;
                        //Adding StudentCL instance in Collection instance.
                        studentCol.Add(studentCL);
                    }
                }
                //Sending the data to database from Collection data.
                int AddedStudents = backUserBLL.createStudentCollection(studentCol);
                //Redirecting to the same page on successful tranmission.
                Response.Redirect("CreateStudent.aspx");
            }
            catch (Exception ex)
            {
                
                throw(new Exception(ex.Message));
            }
        }

        protected void grdViewStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //Adding New Page Index in Grid View.
                grdViewStudent.PageIndex = e.NewPageIndex;
                //Calling Bind Grid Functions
                BindDataGridPaging();
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
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