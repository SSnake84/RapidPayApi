using RapidPayApi.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Concurrent;

namespace RapidPayApi.Services
{
    public interface ICreditCardService
    {
        void  AddCreditCard(CreditCard card);
        decimal GetCreditCardBalance(string cardNumber);
        decimal Pay(string cardNumber, decimal amount);
        bool IsValidCardNumber(string cardNumber);
    }

    public class CreditCardService : ICreditCardService
    {

        private ConcurrentBag<CreditCard> CreditCards { get; set; }

        public CreditCardService()
        {
            CreditCards = new ConcurrentBag<CreditCard>();
        }

        public bool IsValidCardNumber(string cardNumber)
        {
            return cardNumber != null && cardNumber.Length == 15;
        }

        public decimal GetCreditCardBalance(string cardNumber)
        {
            if (!IsValidCardNumber(cardNumber))
                throw new Exception(Messages.WRONG_CARD_NUMBER);

            CreditCard? card = CreditCards.FirstOrDefault(c => c.CardNumber == cardNumber);

            if(card == null)
                throw new Exception(Messages.WRONG_CARD_NUMBER);

            return card.Balance;
        }

        public void AddCreditCard(CreditCard card)
        {
            // Run some Validations
            CreditCards.Add(card);
        }

        public decimal Pay(string cardNumber, decimal amount)
        {
            CreditCard? card = CreditCards.FirstOrDefault(c => c.CardNumber == cardNumber);

            if (card == null)
                throw new Exception(Messages.WRONG_CARD_NUMBER);

            card.Balance -= amount;

            return card.Balance;
        }
    }
}