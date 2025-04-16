namespace ECommerce.Application.Common.Utilities
{
    public static class Constants
    {
        #region SEED ACCOUNT ADMIN
        public const string ADMIN_EMAIL = "admin@gmail.com";
        public const string ADMIN_PASSWORD = "123456";
        public const string ADMIN_USERNAME = "admin";
        #endregion

        #region PAGED LIST
        public const int PAGESIZE = 10;
        public const int PAGENUMBER = 1;
        #endregion    

        #region VALIDATION MODEL MESSAGE
        public const string VALIDATION_FAILED = "Validation Failed!";
        #endregion

        #region RESPONSE API MESSAGE
        public const string ADD_SUCCESS = "Created {0} Successfully!";
        public const string UPDATE_SUCCESS = "Updated {0} Successfully!";
        public const string DELETE_SUCCESS = "Deleted {0} Successfully!";
        public const string GET_SUCCESS = "Get Data Successfully!";
        public const string GET_NOTFOUND = "Not Found!";
        public const string SERVER_ERROR = "Internal Server Error!";
        public const string EMAIL_EXIST = "This email address is already being used";
        public const string USERNAME_EXIST = "This username address is already being used";
        #endregion

        #region RESPONSE API MESSAGE CHANGE PASSWORD
        public const string CHANGEPASSSWORD_SUCCCESS = "Password Changed Successfully";
        public const string CONFIRMPASSWORD_ERROR = "The New Password And Confirm Password Do Not Match";
        public const string OLDPASSSWORD_ERROR = "Current Password Incorrect";
        #endregion

        #region  RESPONSE API MESSAGE AUTHENTICATION
        public const string REGISTER_ERROR = "Registration failed. Please check again!";
        public const string REGISTER_SUCCESS = "Registration successfully!";
        public const string LOGIN_ERROR = "Invalid email or password.";
        public const string LOGIN_ERROR_USERNAME = "Username does not exist. Please check again!";
        public const string LOGIN_ERROR_PASSWORD = "Incorrect password. Please check again!";
        public const string LOGIN_SUCCESS = "Login to the system successfully!";
        public const string LOGIN_ACOUNT_LOCKED = "Account temporarily locked!";
        public const string LOGIN_Email_NOT_CONFIRM = "Email Not Confirm. Please check again!";
        public const string REFRESH_TOKEN_INVALID = "Invalid refresh token.";   
        #endregion

        #region  FOLDER PATH
        public static string ROOT_PATH = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        public static string ROOT_PATH_DATA = "Domain";
        public static string ROOT_PATH_MAIL_TEMPLATE = "Common/MailTemplates";
        public static string ROOT_PATH_SEED_DATA = "Common/SeedData";
        #endregion

        #region Restaurant Info
        public static TimeSpan OpenTime = new TimeSpan(8, 0, 0);
        public static TimeSpan CloseTime = new TimeSpan(22, 0, 0);
        public static TimeSpan TimeStepBooking = new TimeSpan(2, 0, 0);
        #endregion

        #region Request Limit 
        public static int RequestLimit = 20; 
        public static TimeSpan RequestLimitTime = new TimeSpan(1, 0, 0);
        #endregion
    }
}
