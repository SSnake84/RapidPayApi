namespace RapidPayApi
{
    public class Messages
    {
        public const string CARD_CREATED = "Credit Card has been created succesfully";

        // Errors
        public const string AUTH_BLANK_CREDENTIALS = "Username or password cannot be blank";
        public const string AUTH_WRONG_CREDENTIALS = "The combination of Username and password is incorrect";
        public const string CARD_WRONG_NUMBER = "Card number is invalid or doesn't exist";
        public const string CARD_WRONG_AMOUNT = "Wrong Amount";
        public const string CARD_NUMBER_ALREADY_EXISTS = "Credit Card number already exists";
        public const string PAYMENT_FAILED = "There was an error trying to pay. Please try again later";
        public const string PAYMENT_INSUFICIENT_FUNDS = "The selected amount exceeds the balance of the credit card";
        public const string SYSTEM_EXCEPTION = "Something went wrong. please try again later. If the issue persists, contact your IT Administrtor";
    }
}
