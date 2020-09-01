using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLayer
{
    public class PaymentDetailCL
    {
        public int id { get; set; }
        public int studentId { get; set; }
        public string modeOfPayment { get; set; }
        public long leftFeesAmount { get; set; }
        public long leftFeesId { get; set; }
        public DateTime datecreated { get; set; }
        public DateTime dateModified { get; set; }
        public bool isDeleted { get; set; }
        public bool isPaid { get; set; }
    }
}
