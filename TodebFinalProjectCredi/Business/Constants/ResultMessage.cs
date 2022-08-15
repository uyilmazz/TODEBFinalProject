using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class ResultMessage
    {

        // Credit Card
        public static string CreditCardAlreayExists = "Credit card already exists!";
        public static string CreditCardNotFound = "Credit card not found!";
        public static string CreditCardAdded = "Credit card added successfully";
        public static string CreditCardDeleted = "Credit card deleted successfully";
        public static string CreditCardUpdated = "Credit card updated successfully";

        // Payment
        public static string AlreadyPaid = "Already paid";
        public static string PaymentCompleted = "Payment Completed.";
        public static string PaymentNotFound = "Payment not found!";
        public static string PaymentUpdated = "Payment updated.";
        public static string PaymentDeleted = "Payment deleted.";
        public static string ErrorDuringPayment = "Error occurred during payment";
        public static string  InsfufficientBalance="insufficient balance!";

        // Paid Type
        public static string PaidTypeAlreayExists = "Paid Type already exists!";
        public static string PaidTypeNotFound = "Paid type not found!";
        public static string PaidTypeAdded = "Paid type added successfully";
        public static string PaidTypeDeleted = "Paid type deleted successfully";
        public static string PaidTypeUpdated = "Paid type updated successfully";

    }
}