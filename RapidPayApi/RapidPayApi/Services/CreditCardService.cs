using RapidPayApi.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Concurrent;

namespace RapidPayApi.Services
{
    public interface ICreditCardService
    {
        bool AddCreditCard(CreditCard card);
        decimal GetCreditCardBalance(string cardNumber);
        PaymentResponse Pay(string cardNumber, decimal amount);
        bool IsValidCardNumber(string cardNumber);
    }

    public class CreditCardService : ICreditCardService
    {

        private ConcurrentDictionary<string, CreditCard> CreditCards { get; set; }
        private IUniversalFeesExchangeService UfeService { get; set; }

        public CreditCardService()
        {
            CreditCards = new();
            UfeService = UniversalFeesExchangeService.GetInstance();
        }

        public bool IsValidCardNumber(string cardNumber)
        {
            return cardNumber != null && cardNumber.Length == 15;
        }

        public decimal GetCreditCardBalance(string cardNumber)
        {
            if (!IsValidCardNumber(cardNumber))
                throw new ManagedException(Messages.CARD_WRONG_NUMBER);

            if(!CreditCards.TryGetValue(cardNumber, out CreditCard? card))
                throw new ManagedException(Messages.CARD_WRONG_NUMBER);

            return card.Balance;
        }

        public bool AddCreditCard(CreditCard card)
        {
            if (string.IsNullOrEmpty(card.CardNumber))
                throw new ManagedException(Messages.CARD_WRONG_NUMBER);

            if (card.Balance < (decimal)0)
                throw new ManagedException(Messages.PAYMENT_INSUFICIENT_FUNDS);

            return CreditCards.TryAdd(card.CardNumber, card);
        }

        public PaymentResponse Pay(string cardNumber, decimal amount)
        {
            if(amount <= 0)
                throw new ManagedException(Messages.CARD_WRONG_AMOUNT);

            if (!CreditCards.TryGetValue(cardNumber, out CreditCard? currentRecord))
                throw new ManagedException(Messages.CARD_WRONG_NUMBER);

            decimal fee = UfeService.GetFee();
            decimal newBalance = currentRecord.Balance - amount - fee;

            if (newBalance < (decimal)0)
                throw new ManagedException(Messages.PAYMENT_INSUFICIENT_FUNDS);

            var updatedCreditCard = new CreditCard(currentRecord)
            {
                Balance = newBalance
            };

            if (!CreditCards.TryUpdate(cardNumber, updatedCreditCard, currentRecord))
                throw new ManagedException(Messages.PAYMENT_FAILED);

            return new PaymentResponse { 
                OldBalance = currentRecord.Balance, 
                NewBalance = newBalance, 
                FeeApplied = fee,
                Amount = amount
            };
        }
    }
}