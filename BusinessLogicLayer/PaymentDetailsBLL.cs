using CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Link_Layer;
using System.Collections.ObjectModel;
using System.Globalization;

namespace BusinessLogicLayer
{
    public class PaymentDetailsBLL
    {
        //Instantiating the Rainbow SQL server Entities.
        RAINBOWEntities dbcontext = new RAINBOWEntities();
        /// <summary>
        /// This method gets the Payment Detail Table Data from querying the studentid in the Payment Detail Table.  
        /// </summary>
        /// <param name="StudentId">Table Primary Key Id in Default Student Table.</param>
        /// <returns>Payment Status of a Student for all the months for transaction.</returns>
        public PaymentDetailCL getFeeDetailbyStudentId(int StudentId)
        {
            Payment_Detail query = (from x in dbcontext.Payment_Details where x.StudentId == StudentId select x).FirstOrDefault();
            PaymentDetailCL getFee = new PaymentDetailCL()
            {
                datecreated = query.DateCreated,
                dateModified = query.DateModified,
                id = query.Id,
                isDeleted = query.IsDeleted,
                leftFeesId = query.LeftfeesId,
                modeOfPayment = query.ModeOfPayment,
                studentId = query.StudentId,
                leftFeesAmount = query.LeftFeesAmount,
            };
            return getFee;
        }
        /// <summary>
        /// This method gets the Paid Fee Detail Table Data of Student Required Payment from querying the paidfeeid.
        /// </summary>
        /// <param name="paidFeeId">Paid Fee Id provided by Payment Detail Table to query Paid Fee Table.</param>
        /// <returns>Left Fee Details for a particular month</returns>
        public LeftFeesCL getLeftFeeById(int paidFeeId)
        {

            Left_Fee query = (from x in dbcontext.Left_Fees where x.Id == paidFeeId select x).FirstOrDefault();
            LeftFeesCL getLeftFee = new LeftFeesCL()
            {
                id = query.Id,
                adminCharges = query.AdmissionFee,
                admissionFee = query.AdmissionFee,
                examinationFee = query.ExaminationFee,
                annualCharges = query.AnnualCharges,
                computerFeeMonthly = query.ComputerFeeMonthly,
                computerFeeYearly = query.ComputerFeeYearly,
                developmentChargesYearly = query.DevelopmentChargesYearly,
                labFee = query.LabFee,
                projectFee = query.ProjectFee,
                refreshmentAccFee = query.RefreshmentAccFee,
                smartClassCharges = query.SmartClassCharges,
                totalFee = query.TotalFee,
                tutionFee = query.TutionFee,
                transportFee = query.TransportFee,
                isDeleted = query.IsDeleted,
                dateModified = query.DateModified,
                dateCreated = query.DateCreated,
                month = query.Month,
            };
            return getLeftFee;
        }
        /// <summary>
        /// This method gets the Left Fees data collection for a specefic student for all the months.
        /// </summary>
        /// <param name="studentId">StudentId returned from Student Table at the point of changing page.</param>
        /// <returns>Collection of Fees Left of all the months due for student</returns>
        public Collection<LeftFeesCL> getLeftFeeCollection(int studentId)
        {
            DateTime dateHosting = DateTime.UtcNow;
            TimeZoneInfo indianZoneId = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime dateNow = TimeZoneInfo.ConvertTimeFromUtc(dateHosting, indianZoneId);
            //Gets Payment Detail Table of current student Id for current month.
            IQueryable<Payment_Detail> paymentCurrentMonth = from x in dbcontext.Payment_Details where x.StudentId == studentId && x.Left_Fees.IsDeleted == false && x.Left_Fees.Month.Month == dateNow.Month && x.isPaid == false && x.IsDeleted == false select x;
            Collection<LeftFeesCL> leftFees = new Collection<LeftFeesCL>();
            foreach (Payment_Detail item in paymentCurrentMonth)
            {
                long LateFee = 0;
                string date = dateNow.Date.Day.ToString();
                if (Convert.ToInt64(date) <= 10)
                {
                    LateFee = 0;
                }
                if (Convert.ToInt64(date) > 10 && Convert.ToInt32(date) <= 20)
                {
                    LateFee = 20;
                }
                if (Convert.ToInt64(date) > 20 && Convert.ToInt32(date) <= 31)
                {
                    LateFee = 30;
                }
                leftFees.Add(new LeftFeesCL()
                {
                    tutionFee = item.Left_Fees.TutionFee,
                    adminCharges = item.Left_Fees.AdminCharges,
                    admissionFee = item.Left_Fees.AdmissionFee,
                    examinationFee = item.Left_Fees.ExaminationFee,
                    annualCharges = item.Left_Fees.AnnualCharges,
                    computerFeeMonthly = item.Left_Fees.ComputerFeeMonthly,
                    computerFeeYearly = item.Left_Fees.ComputerFeeYearly,
                    dateCreated = item.Left_Fees.DateCreated,
                    dateModified = item.Left_Fees.DateModified,
                    developmentChargesYearly = item.Left_Fees.DevelopmentChargesYearly,
                    id = item.Left_Fees.Id,
                    isDeleted = item.Left_Fees.IsDeleted,
                    labFee = item.Left_Fees.LabFee,
                    month = item.Left_Fees.Month,
                    projectFee = item.Left_Fees.ProjectFee,
                    refreshmentAccFee = item.Left_Fees.RefreshmentAccFee,
                    smartClassCharges = item.Left_Fees.SmartClassCharges,
                    transportFee = item.Left_Fees.TransportFee,
                    lateFee = LateFee,
                    totalFee = item.Left_Fees.TotalFee + LateFee,
                });
            }
            return leftFees;
        }
        /// <summary>
        /// This method gets the Left Fees data collection for a specefic student for all the months.
        /// </summary>
        /// <param name="studentId">StudentId returned from Student Table at the point of changing page.</param>
        /// <returns>Collection of Fees Left of all the months due for student</returns>
        public Collection<LeftFeesCL> getLeftFeeDueCollection(int studentId)
        {
            DateTime dateHosting = DateTime.UtcNow;
            TimeZoneInfo indianZoneId = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime dateNow = TimeZoneInfo.ConvertTimeFromUtc(dateHosting, indianZoneId);
            IQueryable<Payment_Detail> query = from x in dbcontext.Payment_Details where x.StudentId == studentId && x.Left_Fees.IsDeleted == false && x.isPaid == false && x.Left_Fees.Month.Month != dateNow.Month && x.IsDeleted == false select x;
            Collection<LeftFeesCL> leftFees = new Collection<LeftFeesCL>();
            foreach (Payment_Detail item in query)
            {
                leftFees.Add(new LeftFeesCL()
                {
                    adminCharges = item.Left_Fees.AdminCharges,
                    admissionFee = item.Left_Fees.AdmissionFee,
                    examinationFee = item.Left_Fees.ExaminationFee,
                    annualCharges = item.Left_Fees.AnnualCharges,
                    computerFeeMonthly = item.Left_Fees.ComputerFeeMonthly,
                    computerFeeYearly = item.Left_Fees.ComputerFeeYearly,
                    dateCreated = item.Left_Fees.DateCreated,
                    dateModified = item.Left_Fees.DateModified,
                    developmentChargesYearly = item.Left_Fees.DevelopmentChargesYearly,
                    id = item.Left_Fees.Id,
                    isDeleted = item.Left_Fees.IsDeleted,
                    labFee = item.Left_Fees.LabFee,
                    month = item.Left_Fees.Month,
                    projectFee = item.Left_Fees.ProjectFee,
                    refreshmentAccFee = item.Left_Fees.RefreshmentAccFee,
                    smartClassCharges = item.Left_Fees.SmartClassCharges,
                    tutionFee = item.Left_Fees.TutionFee,
                    transportFee = item.Left_Fees.TransportFee,
                    lateFee = item.Left_Fees.LateFee,
                    totalFee = item.Left_Fees.TotalFee,
                });
            }
            return leftFees;
        }
        /// <summary>
        /// This method creates the Fee Data Collection provided by User inputted excel and checks whether fee is paid or already there.
        /// </summary>
        /// <param name="feeCollection">Fee Data Collection provided by user for Creation.</param>
        /// <returns>The total number of fee data enties created or updated.</returns>
        public int createFeeCollection(Collection<FeeExcelCL> feeCollection)
        {
            int count = 0;//Counts the total number of Fee Entries sent to Database.
            foreach (FeeExcelCL item in feeCollection)
            {
                Student studentQuery = (from x in dbcontext.Students where x.AdmissionNo == item.admissionNo select x).FirstOrDefault();//Query gets the student data from admission No.
                Payment_Detail paymentQuery = (from x in dbcontext.Payment_Details where x.StudentId == studentQuery.Id && x.Left_Fees.Month == item.month select x).FirstOrDefault();
                if (paymentQuery == null)//Checks whether Payment Detail Table Data is present.
                {
                    Left_Fee leftFee = dbcontext.Left_Fees.Add(new Left_Fee//Adds Left Fee Table in DB.
                    {
                        TutionFee = item.tutionFee,
                        AdminCharges = item.adminCharges,
                        AdmissionFee = item.admissionFee,
                        ExaminationFee = item.examinationFee,
                        AnnualCharges = item.annualCharges,
                        ComputerFeeMonthly = item.computerFeeMonthly,
                        ComputerFeeYearly = item.computerFeeYearly,
                        DateCreated = item.dateCreated,
                        DateModified = item.dateModified,
                        DevelopmentChargesYearly = item.developmentChargesYearly,
                        IsDeleted = false,
                        LabFee = item.labFee,
                        Month = item.month,
                        ProjectFee = item.projectFee,
                        RefreshmentAccFee = item.refreshmentAccFee,
                        SmartClassCharges = item.smartClassCharges,
                        TransportFee = item.transportFee,
                        LateFee = item.lateFee,
                        TotalFee = item.totalFee,
                    });
                    dbcontext.SaveChanges();
                    dbcontext.Payment_Details.Add(new Payment_Detail()//Add Payment Detail Table after Left Fee Table in Database.
                    {
                        DateCreated = item.dateCreated,
                        DateModified = item.dateModified,
                        IsDeleted = false,
                        LeftFeesAmount = item.totalFee,
                        LeftfeesId = leftFee.Id,
                        isPaid = false,
                        ModeOfPayment = "None",
                        StudentId = studentQuery.Id,
                    });
                    count++;
                    dbcontext.SaveChanges();
                }
                else
                {
                    if (paymentQuery.isPaid == true)
                    {

                    }
                    else
                    {
                        paymentQuery.Left_Fees.TutionFee = item.tutionFee;
                        paymentQuery.Left_Fees.AdminCharges = item.adminCharges;
                        paymentQuery.Left_Fees.AdmissionFee = item.admissionFee;
                        paymentQuery.Left_Fees.ExaminationFee = item.examinationFee;
                        paymentQuery.Left_Fees.AnnualCharges = item.annualCharges;
                        paymentQuery.Left_Fees.ComputerFeeMonthly = item.computerFeeMonthly;
                        paymentQuery.Left_Fees.ComputerFeeYearly = item.computerFeeYearly;
                        paymentQuery.Left_Fees.DateCreated = item.dateCreated;
                        paymentQuery.Left_Fees.DateModified = item.dateModified;
                        paymentQuery.Left_Fees.DevelopmentChargesYearly = item.developmentChargesYearly;
                        paymentQuery.Left_Fees.IsDeleted = false;
                        paymentQuery.Left_Fees.LabFee = item.labFee;
                        paymentQuery.Left_Fees.Month = item.month;
                        paymentQuery.Left_Fees.ProjectFee = item.projectFee;
                        paymentQuery.Left_Fees.RefreshmentAccFee = item.refreshmentAccFee;
                        paymentQuery.Left_Fees.SmartClassCharges = item.smartClassCharges;
                        paymentQuery.Left_Fees.TransportFee = item.transportFee;
                        paymentQuery.Left_Fees.LateFee = item.lateFee;
                        paymentQuery.Left_Fees.TotalFee = item.totalFee;
                        count++;
                        dbcontext.SaveChanges();
                    }
                }
            }
            return count;
        }
        /// <summary>
        /// This method checks whether the specific Admission No in Fee Data Collection provided by Excel is available in Student Table or not.
        /// </summary>
        /// <param name="feeCollection">The Fee Collection with Admission No to be checked.</param>
        /// <returns>The Admission No of the last row not available in Student Table.</returns>
        public Collection<int> getNoAdmissionNo(Collection<FeeExcelCL> feeCollection)
        {
            Collection<int> count = new Collection<int>();
            foreach (FeeExcelCL item in feeCollection)
            {
                Student query = (from x in dbcontext.Students where x.AdmissionNo == item.admissionNo select x).FirstOrDefault();
                Student query2 = (from x in dbcontext.Students where x.AdmissionNo == item.admissionNo && x.IsDeleted == true select x).FirstOrDefault();
                if (query == null || query2 != null)
                {
                    count.Add(item.admissionNo);
                }
            }
            return count;
        }
        /// <summary>
        /// This method gets all the data from Fee Database for backend user to view Fee Details.
        /// </summary>
        /// <returns>Collection of Fee Grid Shown to Backend User.</returns>
        public Collection<FeeGridCL> viewFeeDB()
        {
            DateTime dateHosting = DateTime.UtcNow;
            TimeZoneInfo indianZoneId = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime dateNow = TimeZoneInfo.ConvertTimeFromUtc(dateHosting, indianZoneId);
            Collection<FeeGridCL> viewFees = new Collection<FeeGridCL>();
            //Gets Payment Detail Table from Database for current month which is not paid.
            IQueryable<Payment_Detail> paymentCurrentMonth = from x in dbcontext.Payment_Details where x.IsDeleted == false && x.Left_Fees.IsDeleted == false && x.Left_Fees.Month.Month == dateNow.Month && x.isPaid == false select x;
            foreach (Payment_Detail item in paymentCurrentMonth)
            {
                long LateFee = 0;
                string date = dateNow.Date.Day.ToString();
                if (Convert.ToInt32(date) <= 10)
                {
                    LateFee = 0;
                }
                if (Convert.ToInt32(date) > 10 && Convert.ToInt32(date) <= 20)
                {
                    LateFee = 20;
                }
                if (Convert.ToInt32(date) > 20 && Convert.ToInt32(date) <= 31)
                {
                    LateFee = 30;
                }

                viewFees.Add(new FeeGridCL()
                {
                    adminCharges = item.Left_Fees.AdminCharges.ToString(),
                    admissionFee = item.Left_Fees.AdmissionFee.ToString(),
                    examinationFee = item.Left_Fees.ExaminationFee.ToString(),
                    admissionNo = item.Student.AdmissionNo.ToString(),
                    annualCharges = item.Left_Fees.AnnualCharges.ToString(),
                    computerFeeMonthly = item.Left_Fees.ComputerFeeMonthly.ToString(),
                    computerFeeYearly = item.Left_Fees.ComputerFeeYearly.ToString(),
                    developmentChargesYearly = item.Left_Fees.DevelopmentChargesYearly.ToString(),
                    leftFeeId = item.Left_Fees.Id,
                    isPaid = item.isPaid ? "Yes" : "No",
                    labFee = item.Left_Fees.LabFee.ToString(),
                    month = item.Left_Fees.Month.ToString("MMMM-yyyy"),
                    projectFee = item.Left_Fees.ProjectFee.ToString(),
                    refreshmentAccFee = item.Left_Fees.RefreshmentAccFee.ToString(),
                    smartClassCharges = item.Left_Fees.SmartClassCharges.ToString(),
                    tutionFee = item.Left_Fees.TutionFee.ToString(),
                    classsection = item.Student.Class + "-" + item.Student.Section,
                    name = item.Student.Name,
                    paymentDetailId = item.Id,
                    transportFee = item.Left_Fees.TransportFee.ToString(),
                    studentId = item.Student.Id,
                    lateFee = LateFee.ToString(),
                    dateCreated = item.DateCreated.ToString(),
                    dateModified = item.DateModified.ToString(),
                    totalFee = (item.Left_Fees.TotalFee + LateFee).ToString(),
                });
            }
            //Gets Payment Detail Table from Database for current month which is paid.
            IQueryable<Payment_Detail> paymentCurrentMonthPaid = from x in dbcontext.Payment_Details where x.IsDeleted == false && x.Left_Fees.IsDeleted == false && x.Left_Fees.Month.Month == dateNow.Month && x.isPaid == true select x;
            foreach (Payment_Detail item in paymentCurrentMonthPaid)
            {
                string date = dateNow.Date.Day.ToString();
                viewFees.Add(new FeeGridCL()
                {
                    adminCharges = item.Left_Fees.AdminCharges.ToString(),
                    admissionFee = item.Left_Fees.AdmissionFee.ToString(),
                    examinationFee = item.Left_Fees.ExaminationFee.ToString(),
                    admissionNo = item.Student.AdmissionNo.ToString(),
                    annualCharges = item.Left_Fees.AnnualCharges.ToString(),
                    computerFeeMonthly = item.Left_Fees.ComputerFeeMonthly.ToString(),
                    computerFeeYearly = item.Left_Fees.ComputerFeeYearly.ToString(),
                    developmentChargesYearly = item.Left_Fees.DevelopmentChargesYearly.ToString(),
                    leftFeeId = item.Left_Fees.Id,
                    isPaid = item.isPaid ? "Yes" : "No",
                    labFee = item.Left_Fees.LabFee.ToString(),
                    month = item.Left_Fees.Month.ToString("MMMM-yyyy"),
                    projectFee = item.Left_Fees.ProjectFee.ToString(),
                    refreshmentAccFee = item.Left_Fees.RefreshmentAccFee.ToString(),
                    smartClassCharges = item.Left_Fees.SmartClassCharges.ToString(),
                    tutionFee = item.Left_Fees.TutionFee.ToString(),
                    classsection = item.Student.Class + "-" + item.Student.Section,
                    name = item.Student.Name,
                    paymentDetailId = item.Id,
                    transportFee = item.Left_Fees.TransportFee.ToString(),
                    studentId = item.Student.Id,
                    lateFee = item.Left_Fees.LateFee.ToString(),
                    dateCreated = item.DateCreated.ToString(),
                    dateModified = item.DateModified.ToString(),
                    totalFee = item.Left_Fees.TotalFee.ToString(),
                });
            }
            //Gets Payment Detail Table from Database for Ealier Months.
            IQueryable<Payment_Detail> paymentDues = from x in dbcontext.Payment_Details where x.IsDeleted == false && x.Left_Fees.IsDeleted == false && x.Left_Fees.Month.Month != dateNow.Month select x;
            foreach (Payment_Detail item in paymentDues)
            {
                viewFees.Add(new FeeGridCL()
                {
                    adminCharges = item.Left_Fees.AdminCharges.ToString(),
                    admissionFee = item.Left_Fees.AdmissionFee.ToString(),
                    examinationFee = item.Left_Fees.ExaminationFee.ToString(),
                    admissionNo = item.Student.AdmissionNo.ToString(),
                    annualCharges = item.Left_Fees.AnnualCharges.ToString(),
                    computerFeeMonthly = item.Left_Fees.ComputerFeeMonthly.ToString(),
                    computerFeeYearly = item.Left_Fees.ComputerFeeYearly.ToString(),
                    developmentChargesYearly = item.Left_Fees.DevelopmentChargesYearly.ToString(),
                    leftFeeId = item.Left_Fees.Id,
                    isPaid = item.isPaid ? "Yes" : "No",
                    labFee = item.Left_Fees.LabFee.ToString(),
                    month = item.Left_Fees.Month.ToString("MMMM-yyyy"),
                    projectFee = item.Left_Fees.ProjectFee.ToString(),
                    refreshmentAccFee = item.Left_Fees.RefreshmentAccFee.ToString(),
                    smartClassCharges = item.Left_Fees.SmartClassCharges.ToString(),
                    tutionFee = item.Left_Fees.TutionFee.ToString(),
                    classsection = item.Student.Class + "-" + item.Student.Section,
                    name = item.Student.Name,
                    paymentDetailId = item.Id,
                    transportFee = item.Left_Fees.TransportFee.ToString(),
                    studentId = item.Student.Id,
                    lateFee = item.Left_Fees.LateFee.ToString(),
                    dateCreated = item.DateCreated.ToString(),
                    dateModified = item.DateModified.ToString(),
                    totalFee = item.Left_Fees.TotalFee.ToString(),
                });
            }
            return viewFees;
        }
        /// <summary>
        /// This method gets the Payment Fees data collection for a specefic student for all the months.
        /// </summary>
        /// <param name="studentId">StudentId returned from Student Table at the point of changing page.</param>
        /// <returns>Collection of Fees Payment Detail of all the months due for student</returns>
        public Collection<PaymentDetailCL> getPaymentFeeCollection(int studentId)
        {
            IQueryable<Payment_Detail> query = from x in dbcontext.Payment_Details where x.StudentId == studentId && x.Left_Fees.IsDeleted == false select x;
            Collection<PaymentDetailCL> leftFees = new Collection<PaymentDetailCL>();
            foreach (Payment_Detail item in query)
            {
                leftFees.Add(new PaymentDetailCL()
                {
                    datecreated = item.DateCreated,
                    dateModified = item.DateModified,
                    id = item.Id,
                    isDeleted = item.IsDeleted,
                    isPaid = item.isPaid,
                    leftFeesAmount = item.LeftfeesId,
                    leftFeesId = item.LeftfeesId,
                    modeOfPayment = item.ModeOfPayment,
                    studentId = item.StudentId,
                });
            }
            return leftFees;
        }
        public int deleteFeeDatabyMonth(DateTime month)
        {
            int i = 0;
            IQueryable<Left_Fee> query = from x in dbcontext.Left_Fees where x.Month == month && x.IsDeleted == false select x;
            foreach (Left_Fee item in query)
            {
                Left_Fee query2 = (from x in dbcontext.Left_Fees where x.Id == item.Id select x).FirstOrDefault();
                item.IsDeleted = true;
                i++;
            }
            dbcontext.SaveChanges();
            return i;
        }
    }
}
