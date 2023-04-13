using RapidPayApi.Models;
using System.Collections.Concurrent;

namespace RapidPayApi.Services
{
    public interface ICreditCardService
    {
        Task<bool> AddCreditCard(CreditCard card);
        Task<decimal> GetCreditCardBalance(string cardNumber);
        Task<PaymentResponse> Pay(string cardNumber, decimal amount);
        Task<bool> IsValidCardNumber(string cardNumber);
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

        public async Task<bool> IsValidCardNumber(string cardNumber)
        {
            return await Task.FromResult(cardNumber != null && cardNumber.Length == Constants.CREDITCARD_MAXLENGTH);
        }

        public async Task<decimal> GetCreditCardBalance(string cardNumber)
        {
            if (!await IsValidCardNumber(cardNumber))
                throw new ManagedException(Constants.MESSAGE_CARD_WRONG_NUMBER);

            if(!CreditCards.TryGetValue(cardNumber, out CreditCard? card))
                throw new ManagedException(Constants.MESSAGE_CARD_WRONG_NUMBER);

            return await Task.FromResult(card.Balance);
        }

        public async Task<bool> AddCreditCard(CreditCard card)
        {
            if (string.IsNullOrEmpty(card.CardNumber))
                throw new ManagedException(Constants.MESSAGE_CARD_WRONG_NUMBER);

            if (card.Balance < (decimal)0)
                throw new ManagedException(Constants.MESSAGE_PAYMENT_INSUFICIENT_FUNDS);

            return await Task.FromResult(CreditCards.TryAdd(card.CardNumber, card));
        }

        public async Task<PaymentResponse> Pay(string cardNumber, decimal amount)
        {
            if(amount <= 0)
                throw new ManagedException(Constants.MESSAGE_CARD_WRONG_AMOUNT);

            if (!CreditCards.TryGetValue(cardNumber, out CreditCard? currentRecord))
                throw new ManagedException(Constants.MESSAGE_CARD_WRONG_NUMBER);

            decimal fee = await UfeService.GetFee();
            decimal newBalance = currentRecord.Balance - amount - fee;

            if (newBalance < (decimal)0)
                throw new ManagedException(Constants.MESSAGE_PAYMENT_INSUFICIENT_FUNDS);

            var updatedCreditCard = new CreditCard(currentRecord)
            {
                Balance = newBalance
            };

            if (!CreditCards.TryUpdate(cardNumber, updatedCreditCard, currentRecord))
                throw new ManagedException(Constants.MESSAGE_PAYMENT_FAILED);

            return new PaymentResponse { 
                OldBalance = currentRecord.Balance, 
                NewBalance = newBalance, 
                FeeApplied = fee,
                Amount = amount
            };
        }
    }
}