namespace RapidPayApi.Models
{
    public class PayRequest
    {
        public string? CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}