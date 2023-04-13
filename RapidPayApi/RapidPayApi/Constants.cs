namespace RapidPayApi
{
    public class Constants
    {
        public const int CREDITCARD_MAXLENGTH = 15;
        public const decimal FEE_INITIALVALUE = 1;
        public const double UFE_INTERVAL_SECONDS = 3600;
        public const string AUTH_USERNAME = "Jose";
        public const string AUTH_PASSWORD = "Enser";

        public const string MESSAGE_CARD_CREATED = "Credit Card has been created succesfully";

        // Errors
        public const string MESSAGE_AUTH_BLANK_CREDENTIALS = "Username or password cannot be blank";
        public const string MESSAGE_AUTH_WRONG_CREDENTIALS = "The combination of Username and password is incorrect";
        public const string MESSAGE_CARD_WRONG_NUMBER = "Card number is invalid or doesn't exist";
        public const string MESSAGE_CARD_WRONG_AMOUNT = "Wrong Amount";
        public const string MESSAGE_CARD_NUMBER_ALREADY_EXISTS = "Credit Card number already exists";
        public const string MESSAGE_PAYMENT_FAILED = "There was an error trying to pay. Please try again later";
        public const string MESSAGE_PAYMENT_INSUFICIENT_FUNDS = "The selected amount exceeds the balance of the credit card";
        public const string MESSAGE_SYSTEM_EXCEPTION = "Something went wrong. please try again later. If the issue persists, contact your IT Administrtor";
    }
}
