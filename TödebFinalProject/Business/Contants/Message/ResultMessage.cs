using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contants.Message
{
    public static class ResultMessage
    {

        // Person 
        public static string UserAlreadyExits = "User Already Exists!";
        public static string UserAdded = "User Added successfully";
        public static string UserNotFound = "User not found!";
        public static string UserDeleted = "User deleted successfully";
        public static string UserUpdated = "User updated successfully";

        // Person Type
        public static string UserTypeAlreadyExits = "User type Already Exists!";
        public static string UserTypeAdded = "User type Added successfully";
        public static string UserTypeNotFound = "User type not found!";
        public static string UserTypeDeleted = "User type deleted successfully";
        public static string UserTypeUpdated = "User type updated successfully";

        // Apartment 
        public static string ApartmentAlreadyExists = "Apartment Already Exists!";
        public static string ApartmentAdded = "Apartment added successfully";
        public static string ApartmentNotFound = "Apartment not found!";
        public static string ApartmentDeleted = "Apartment deleted successfully";
        public static string ApartmentUpdated = "Apartment updated successfully";

        // Apartment Type
        public static string ApartmentTypeAlreadyExists = "Apartment type Already Exists!";
        public static string ApartmentTypeAdded = "Apartment type added successfully";
        public static string ApartmentTypeNotFound = "Apartment type not found!";
        public static string ApartmentTypeDeleted = "Apartment type deleted successfully";
        public static string ApartmentTypeUpdated = "Apartment type updated successfully";

        // Apartment Bloc
        public static string ApartmentBlocAlreadyExists = "Apartment bloc Already Exists!";
        public static string ApartmentBlocAdded = "Apartment bloc added successfully";
        public static string ApartmentBlocNotFound = "Apartment bloc not found!";
        public static string ApartmentBlocDeleted = "Apartment bloc deleted successfully";
        public static string ApartmentBlocUpdated = "Apartment bloc updated successfully";

        // Bill 
        public static string BillCreated = "Bill created.";
        public static string BulkBillCreated = "Bulk bills created.";
        public static string BillNotFound = "Bill not found!";
        public static string BillDeleted = "Bill deleted successfully";
        public static string BillUpdated = "Bill updated successfully";
        public static string UnPaidBillNotFound = "You do not have such unpaid bill.";

        // Fee 
        public static string FeeCreated = "Fee created.";
        public static string BulkFeeCreated = "Bulk fees created.";
        public static string FeeNotFound = "Fee not found!";
        public static string FeeDeleted = "Fee deleted successfully";
        public static string FeeUpdated = "Fee updated successfully";
        public static string UnPaidFeeNotFound = "You do not have such unpaid fee.";
        public static string InvoiceCannot ="An invoice cannot be issued for an empty flat.";

        // Message
        public static string MessageCreated = "Message created.";
        public static string MessageNotFound = "Message not found!";
        public static string MessageDeleted = "Message deleted successfully";
        public static string MessageUpdated = "Message updated successfully";

        // Login
        public static string LoginError = "Email or password not correct!";
        public static string SuccessLogin = "You have successfully logged in.";
        public static string AccessTokenCreated = "Access Token created.";
        public static string AuthorizationDenied = "You are not authorized!";

        public static string PasswordMailContent = "Your password for login: ";
        
    }
}
