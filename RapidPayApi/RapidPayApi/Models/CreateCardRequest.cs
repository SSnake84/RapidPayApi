using RapidPayApi.Data.Models;

namespace RapidPayApi.Models
{
    public class CreateCardRequest
    {
        public string? CardNumber { get; set; }
        public decimal Balance { get; set; }

        public CreditCard ToCreditCard()
        {
            return new CreditCard()
            {
                Balance = Balance, 
                CardNumber = CardNumber
            };
        }
    }
}