using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Instamojo.NET;
using Instamojo.NET.Models;
using BusinessLogicLayer;
using CommunicationLayer;
using System.Collections.ObjectModel;

namespace RainbowFeeSystem
{
    public partial class SuccessfulPayment : System.Web.UI.Page
    {
        FrontUserBLL userBLL = new FrontUserBLL();
        PaymentDetailsBLL paymentBLL = new PaymentDetailsBLL();
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["payment_id"].Count() == 0)
            {
                throw (new Exception("404 Page Not Found"));
            }
            else
            {
                Instamojo.NET.Instamojo im = new Instamojo.NET.Instamojo("74daa5061b049d6cdc8540a79cfd7a1a", "a1ff98eeb01b5358e479494464b62849");
                string paymentId = Request.QueryString["payment_id"];
                string paymentRequestId = Request.QueryString["payment_request_id"];
                PaymentRequest npr = await im.GetPaymentRequest(paymentRequestId);
                StudentCL getStudent = userBLL.getStudentByMobileNo(Convert.ToInt64(npr.phone.Substring(3)), npr.buyer_name);
                Collection<PaymentDetailCL> getFeeCollection = paymentBLL.getPaymentFeeCollection(getStudent.id);
                Collection<LeftFeesCL> leftFeeDetailbyStudentId = paymentBLL.getLeftFeeCollection(getStudent.id);
                Collection<LeftFeesCL> leftDuesByStudentId = paymentBLL.getLeftFeeDueCollection(getStudent.id);
                long TotalAmountWithoutTransCost = (leftFeeDetailbyStudentId.Sum(x => x.totalFee) + leftDuesByStudentId.Sum(x => x.totalFee));
                double TotalAmountWithTransCost1 = (TotalAmountWithoutTransCost + 1.18 * 0);
                double TotalAmountWithTransCost2 = (TotalAmountWithTransCost1 / (1 - 1.18 * 1.9 / 100));
                TotalAmountWithTransCost2 = Math.Round(TotalAmountWithTransCost2, 2);
                if (npr.status == "Completed" && npr.amount == TotalAmountWithTransCost2.ToString())
                {
                    if (getFeeCollection.FirstOrDefault().isPaid==false)
                    {
                        int FeeMonthCount = userBLL.deleteLeftFeePayment(getFeeCollection);
                        lblTransactionStatus.Text = "Thankyou for your Transaction. Your Transaction has been successful.Reference Id :" + paymentId; 
                    }
                    else
                    {
                        lblTransactionStatus.Text = "You have already paid for this Month Fees & Dues. Your Transaction Reference Id is " + paymentId;
                    }
                }
                else
                {
                    lblTransactionStatus.Text = "Sorry for the inconvenience but the Transaction had failed due to some technical error. Please retry logging in.";
                } 
            }
        }
    }
}