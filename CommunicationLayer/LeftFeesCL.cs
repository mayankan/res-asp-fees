using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLayer
{
    public class LeftFeesCL
    {
        public int id { get; set; }
        public DateTime month { get; set; }
        public long tutionFee { get; set; }
        public long examinationFee { get; set; }
        public long admissionFee { get; set; }
        public long refreshmentAccFee { get; set; }
        public long labFee { get; set; }
        public long projectFee { get; set; }
        public long annualCharges { get; set; }
        public long adminCharges { get; set; }
        public long smartClassCharges { get; set; }
        public long computerFeeYearly { get; set; }
        public long computerFeeMonthly { get; set; }
        public long developmentChargesYearly { get; set; }
        public long lateFee { get; set; }
        public long transportFee { get; set; }
        public long totalFee { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }
        public bool isDeleted { get; set; }
    }
}
