//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data_Link_Layer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public Student()
        {
            this.Payment_Details = new HashSet<Payment_Detail>();
        }
    
        public int Id { get; set; }
        public int AdmissionNo { get; set; }
        public string Name { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public long MobileNumber { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<Payment_Detail> Payment_Details { get; set; }
    }
}