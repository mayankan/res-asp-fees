using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLayer
{
    public class StudentCL
    {
        public int id { get; set; }
        public int admissionNo { get; set; }
        public string name { get; set; }
        public string fathersname { get; set; }
        public string mothername { get; set; }
        public string studentClass { get; set; }
        public string section { get; set; }
        public long mobileNumber { get; set; }
        public bool gender { get; set; }
        public string address { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }
        public bool isDeleted { get; set; }
    }
}
