namespace RapidPayApi.Data.Models
{
    public class PaymentResponse
    {
        public decimal OldBalance { get; set; }
        public decimal NewBalance { get; set; }
        public decimal FeeApplied { get; set; }
        public decimal Amount { get; set; }
    }
}
