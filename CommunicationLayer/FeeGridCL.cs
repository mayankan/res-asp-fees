using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLayer
{
    public class FeeGridCL
    {
        public int paymentDetailId { get; set; }
        public int studentId { get; set; }
        public int leftFeeId { get; set; }
        public string admissionNo { get; set; }
        public string name { get; set; }
        public string classsection { get; set; }
        public string month { get; set; }
        public string tutionFee { get; set; }
        public string admissionFee { get; set; }
        public string examinationFee { get; set; }
        public string refreshmentAccFee { get; set; }
        public string labFee { get; set; }
        public string projectFee { get; set; }
        public string annualCharges { get; set; }
        public string adminCharges { get; set; }
        public string smartClassCharges { get; set; }
        public string computerFeeYearly { get; set; }
        public string computerFeeMonthly { get; set; }
        public string developmentChargesYearly { get; set; }
        public string transportFee { get; set; }
        public string lateFee { get; set; }
        public string totalFee { get; set; }
        public string isPaid { get; set; }
        public string dateCreated { get; set; }
        public string dateModified { get; set; }
    }
}
