using RapidPayApi.Data;
using RapidPayApi.Data.Models;

namespace RapidPayApi.Services
{
    public interface ICreditCardService
    {
        Task<bool> AddCreditCardAsync(CreditCard card);
        Task<decimal> GetCreditCardBalanceAsync(string cardNumber);
        Task<PaymentResponse> PayAsync(string? cardNumber, decimal amount);
        Task<bool> IsValidCardNumber(string cardNumber);
    }

    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardsRepo _creditCardsRepo;
        private readonly IUniversalFeesExchangeService _ufeService;

        public CreditCardService(ICreditCardsRepo creditCardsRepo, IUniversalFeesExchangeService ufeService)
        {
            _creditCardsRepo = creditCardsRepo;
            _ufeService = ufeService;
        }

        public async Task<bool> IsValidCardNumber(string cardNumber)
        {
            return await Task.FromResult(cardNumber != null && cardNumber.Length == Constants.CREDITCARD_MAXLENGTH);
        }

        public async Task<decimal> GetCreditCardBalanceAsync(string cardNumber)
        {
            if (!await IsValidCardNumber(cardNumber))
                throw new ManagedExceptions(Constants.MESSAGE_CARD_WRONG_NUMBER);

            try
            {
                return await _creditCardsRepo.GetCreditCardBalanceAsync(cardNumber);
            }
            catch (CardDoesntExistException)
            {
                throw new ManagedExceptions(Constants.MESSAGE_CARD_WRONG_NUMBER);
            }
        }

        public async Task<bool> AddCreditCardAsync(CreditCard card)
        {
            if (string.IsNullOrEmpty(card.CardNumber))
                throw new ManagedExceptions(Constants.MESSAGE_CARD_WRONG_NUMBER);

            if (card.Balance < (decimal)0)
                throw new ManagedExceptions(Constants.MESSAGE_PAYMENT_INSUFICIENT_FUNDS);

            if(await _creditCardsRepo.DoesExistCreditCardAsync(card.CardNumber))
                throw new ManagedExceptions(Constants.MESSAGE_CARD_NUMBER_ALREADY_EXISTS);

            return await _creditCardsRepo.AddCreditCardAsync(card);
        }

        public async Task<PaymentResponse> PayAsync(string? cardNumber, decimal amount)
        {
            if (string.IsNullOrEmpty(cardNumber))
                throw new ManagedExceptions(Constants.MESSAGE_CARD_WRONG_NUMBER);

            if (amount <= 0)
                throw new ManagedExceptions(Constants.MESSAGE_CARD_WRONG_AMOUNT);

            decimal balance = await _creditCardsRepo.GetCreditCardBalanceAsync(cardNumber);

            decimal fee = await _ufeService.GetFee();
            decimal newBalance = balance - amount - fee;

            if (newBalance < (decimal)0)
                throw new ManagedExceptions(Constants.MESSAGE_PAYMENT_INSUFICIENT_FUNDS);

            await _creditCardsRepo.UpdateBalanceAsync(cardNumber, newBalance);

            return new PaymentResponse {
                OldBalance = balance, 
                NewBalance = newBalance, 
                FeeApplied = fee,
                Amount = amount
            };
        }
    }
}