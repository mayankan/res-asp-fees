using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunicationLayer;
using Data_Link_Layer;
using System.Collections.ObjectModel;

namespace BusinessLogicLayer
{
    public class FrontUserBLL
    {
        //Instantiating the Rainbow SQL server Entities.
        RAINBOWEntities dbcontext = new RAINBOWEntities();
        /// <summary>
        /// This method gets the Student Data by querying the Admission Number
        /// </summary>
        /// <param name="admissionNo">Admission No entered by user</param>
        /// <returns>Student Table data found in Database.</returns>
        public StudentCL getStudentByAdmissionNo(int admissionNo)
        {
            Student query = (from xyz in dbcontext.Students where xyz.AdmissionNo == admissionNo && xyz.IsDeleted == false select xyz).FirstOrDefault();
            StudentCL getUserbyAdmissionNo = new StudentCL()
            {
                id = query.Id,
                address = query.Address,
                dateCreated = query.DateCreated,
                dateModified = query.DateModified,
                fathersname = query.FathersName,
                gender = query.Gender,
                isDeleted = query.IsDeleted,
                studentClass = query.Class,
                mothername = query.MothersName,
                section = query.Section,
                name = query.Name,
                admissionNo = query.AdmissionNo,
                mobileNumber = query.MobileNumber
            };
            return getUserbyAdmissionNo;
        }
        /// <summary>
        /// This method gets the Student Data by querying the Mobile Number
        /// </summary>
        /// <param name="mobileNo">Mobile Number entered by User</param>
        /// <returns>Student Table data found in Database.</returns>
        public StudentCL getStudentByMobileNo(long mobileNo, int admissionNo)
        {
            Student query = (from xyz in dbcontext.Students where xyz.AdmissionNo == admissionNo && xyz.MobileNumber == mobileNo && xyz.IsDeleted == false select xyz).FirstOrDefault();
            StudentCL getUserbyMobileNo = new StudentCL()
            {
                id = query.Id,
                address = query.Address,
                dateCreated = query.DateCreated,
                dateModified = query.DateModified,
                fathersname = query.FathersName,
                gender = query.Gender,
                isDeleted = query.IsDeleted,
                studentClass = query.Class,
                mothername = query.MothersName,
                section = query.Section,
                name = query.Name,
                admissionNo = query.AdmissionNo,
                mobileNumber = query.MobileNumber
            };
            return getUserbyMobileNo;
        }
        /// <summary>
        /// This method gets the Student Data by querying the Mobile Number
        /// </summary>
        /// <param name="mobileNo">Mobile Number entered by User</param>
        /// <returns>Student Table data found in Database.</returns>
        public StudentCL getStudentByMobileNo(long mobileNo, string studentName)
        {
            Student query = (from xyz in dbcontext.Students where xyz.Name == studentName && xyz.MobileNumber == mobileNo && xyz.IsDeleted == false select xyz).FirstOrDefault();
            StudentCL getUserbyMobileNo = new StudentCL()
            {
                id = query.Id,
                address = query.Address,
                dateCreated = query.DateCreated,
                dateModified = query.DateModified,
                fathersname = query.FathersName,
                gender = query.Gender,
                isDeleted = query.IsDeleted,
                studentClass = query.Class,
                mothername = query.MothersName,
                section = query.Section,
                name = query.Name,
                admissionNo = query.AdmissionNo,
                mobileNumber = query.MobileNumber
            };
            return getUserbyMobileNo;
        }
        /// <summary>
        /// This method gets the Student Data by querying the Student Primary Key Id
        /// </summary>
        /// <param name="admissionNo">Student Primary Key Id entered by page</param>
        /// <returns>Student Table data found in Database.</returns>
        public StudentCL getStudentByStudentId(int studentId)
        {
            Student query = (from xyz in dbcontext.Students where xyz.Id == studentId && xyz.IsDeleted == false select xyz).FirstOrDefault();
            StudentCL getUserbyStudentId = new StudentCL()
            {
                id = query.Id,
                address = query.Address,
                dateCreated = query.DateCreated,
                dateModified = query.DateModified,
                fathersname = query.FathersName,
                gender = query.Gender,
                isDeleted = query.IsDeleted,
                studentClass = query.Class,
                mothername = query.MothersName,
                section = query.Section,
                name = query.Name,
                admissionNo = query.AdmissionNo,
                mobileNumber = query.MobileNumber
            };
            return getUserbyStudentId;
        }
        /// <summary>
        /// This method deletes the Left Fee Table Collection which has been paid by the user.
        /// </summary>
        /// <param name="leftFeeCollection">Collection Input of fee Paid by the User</param>
        /// <returns>Number Count of Left Fee Collection Deleted</returns>
        public int deleteLeftFeePayment(Collection<PaymentDetailCL> paymentFeeCollection)
        {
            DateTime dateHosting = DateTime.UtcNow;
            TimeZoneInfo indianZoneId = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime dateNow = TimeZoneInfo.ConvertTimeFromUtc(dateHosting, indianZoneId);
            int i = 0;
            foreach (PaymentDetailCL item in paymentFeeCollection)
            {
                Payment_Detail query = (from x in dbcontext.Payment_Details where x.Id == item.id select x).FirstOrDefault();
                query.isPaid = true;
                query.DateModified = dateNow;
                if (query.Left_Fees.Month.Month == dateNow.Month)
                {
                    long LateFeeOnDate = 0;
                    string date = dateNow.Date.Day.ToString();
                    if (Convert.ToInt32(date) <= 10)
                    {
                        LateFeeOnDate = 0;
                    }
                    if (Convert.ToInt32(date) > 10 && Convert.ToInt32(date) <= 20)
                    {
                        LateFeeOnDate = 20;
                    }
                    if (Convert.ToInt32(date) > 20 && Convert.ToInt32(date) <= 31)
                    {
                        LateFeeOnDate = 30;
                    }
                    query.Left_Fees.LateFee = LateFeeOnDate;
                    query.Left_Fees.TotalFee = query.Left_Fees.TotalFee + LateFeeOnDate;
                }
                query.LeftFeesAmount = query.Left_Fees.TotalFee;
                dbcontext.SaveChanges();
                i++;
            }
            return i;
        }
    }
}