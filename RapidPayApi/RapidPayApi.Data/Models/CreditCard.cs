namespace RapidPayApi.Data.Models
{
    public class CreditCard
    {
        public string? CardNumber { get; set; }
        public decimal Balance { get; set; }
        public CreditCard() { }
        
        public CreditCard(CreditCard card)
        {
            CardNumber = card.CardNumber;
            Balance = card.Balance;
        }
    }
}