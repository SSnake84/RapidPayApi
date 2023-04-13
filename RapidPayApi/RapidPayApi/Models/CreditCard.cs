namespace RapidPayApi.Models
{
    public class CreditCard
    {
        public string? CardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public short ExpirationMonth { get; set; }
        public short ExpirationYear { get; set; }
        public short VerificationCode { get; set; }
        public decimal Balance { get; set; }
    }
}