using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Link_Layer;
using CommunicationLayer;
using System.Collections.ObjectModel;

namespace BusinessLogicLayer
{
    public class BackUserBLL
    {
        //Instantiating the Rainbow SQL server Entities.
        RAINBOWEntities dbcontext = new RAINBOWEntities();
        /// <summary>
        /// This method creates new student data in the database.
        /// </summary>
        /// <param name="studentCL">Student Data entered by Backend User</param>
        /// <returns>Student Id created after adding Student.</returns>
        public int createStudent(StudentCL studentCL)
        {
            Student student = dbcontext.Students.Add(new Student
                {
                    Address = studentCL.address,
                    AdmissionNo = studentCL.admissionNo,
                    Class = studentCL.studentClass,
                    DateCreated = studentCL.dateCreated,
                    DateModified = studentCL.dateModified,
                    FathersName = studentCL.fathersname,
                    Gender = studentCL.gender,
                    IsDeleted = studentCL.isDeleted,
                    MobileNumber = studentCL.mobileNumber,
                    MothersName = studentCL.mothername,
                    Name = studentCL.name,
                    Section = studentCL.section,
                });
            dbcontext.SaveChanges();
            Left_Fee leftFee = dbcontext.Left_Fees.Add(new Left_Fee
                {
                    AdminCharges = 0,
                    AdmissionFee = 0,
                    AnnualCharges = 0,
                    ComputerFeeMonthly = 0,
                    ComputerFeeYearly = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    DevelopmentChargesYearly = 0,
                    IsDeleted = false,
                    LabFee = 0,
                    Month = DateTime.Now,
                    ProjectFee = 0,
                    RefreshmentAccFee = 0,
                    SmartClassCharges = 0,
                    TotalFee = 0,
                    TutionFee = 0,
                });
            dbcontext.SaveChanges();
            Payment_Detail paymentDetail = dbcontext.Payment_Details.Add(new Payment_Detail
                {
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    IsDeleted = false,
                    LeftFeesAmount = leftFee.TotalFee,
                    LeftfeesId = leftFee.Id,
                    ModeOfPayment = "Cash",
                    StudentId = student.Id,
                });
            dbcontext.SaveChanges();
            return student.Id;
        }
        /// <summary>
        /// This method creates new student data collection in the database.
        /// </summary>
        /// <param name="studentCL">Student Data entered by Backend User</param>
        /// <returns>Number of Students Created.</returns>
        public int createStudentCollection(Collection<StudentCL> studentCL)
        {
            int count = 0;
            foreach (StudentCL item in studentCL)
            {
                Student query = (from x in dbcontext.Students where x.AdmissionNo == item.admissionNo select x).FirstOrDefault();
                if (query == null)
                {
                    Student student = dbcontext.Students.Add(new Student
                    {
                        Address = item.address,
                        AdmissionNo = item.admissionNo,
                        Class = item.studentClass,
                        DateCreated = item.dateCreated,
                        DateModified = item.dateModified,
                        FathersName = item.fathersname,
                        Gender = item.gender,
                        IsDeleted = item.isDeleted,
                        MobileNumber = item.mobileNumber,
                        MothersName = item.mothername,
                        Name = item.name,
                        Section = item.section,
                    });
                    count++;
                }
                else
                {
                    query.Address = item.address;
                    query.Class = item.studentClass;
                    query.DateModified = item.dateModified;
                    query.FathersName = item.fathersname;
                    query.Gender = item.gender;
                    query.IsDeleted = item.isDeleted;
                    query.MobileNumber = item.mobileNumber;
                    query.MothersName = item.mothername;
                    query.Name = item.name;
                    query.Section = item.section;
                }
            }
            dbcontext.SaveChanges();
            return count;
        }
        /// <summary>
        /// This method gets all the student data from the database.
        /// </summary>
        /// <returns>Collection of Student CL instance required</returns>
        public Collection<StudentCL> viewStudentDB()
        {
            Collection<StudentCL> viewStudent = new Collection<StudentCL>();
            IQueryable<Student> query = from x in dbcontext.Students where x.IsDeleted == false select x;
            foreach (Student item in query)
            {
                viewStudent.Add(new StudentCL()
                    {
                        address = item.Address,
                        admissionNo = item.AdmissionNo,
                        dateCreated = item.DateCreated,
                        dateModified = item.DateModified,
                        fathersname = item.FathersName,
                        gender = item.Gender,
                        id = item.Id,
                        isDeleted = item.IsDeleted,
                        mobileNumber = item.MobileNumber,
                        mothername = item.MothersName,
                        name = item.Name,
                        section = item.Section,
                        studentClass = item.Class,
                    });
            }
            return viewStudent;
        }
    }
}