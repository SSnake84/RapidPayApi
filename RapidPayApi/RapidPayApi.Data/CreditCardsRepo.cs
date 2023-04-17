using Microsoft.EntityFrameworkCore;
using RapidPayApi.Data.Models;

namespace RapidPayApi.Data
{
    public interface ICreditCardsRepo
    {
        Task<bool> AddCreditCardAsync(CreditCard card);
        Task<decimal> GetCreditCardBalanceAsync(string cardNumber);
        Task<bool> UpdateBalanceAsync(string cardNumber, decimal amount);

        Task<bool> DoesExistCreditCardAsync(string cardNumber);
    }

    public class CreditCardsRepo : ICreditCardsRepo
    {
        public async Task<bool> AddCreditCardAsync(CreditCard card)
        {
            using (var context = new RapidPayContext())
            {
                await context.AddAsync(card);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<decimal> GetCreditCardBalanceAsync(string cardNumber)
        {
            using (var context = new RapidPayContext())
            {
                // not using SingleOrDefaultAsync method to avoid returning 0 as Blance for a non-existing Card
                var query = context.CreditCards.Where(c => c.CardNumber == cardNumber).Select(c => c.Balance);                
                if(!query.Any())
                    throw new CardDoesntExistException();

                return await query.SingleAsync();
            }
        }

        public async Task<bool> UpdateBalanceAsync(string cardNumber, decimal amount)
        {
            using (var context = new RapidPayContext())
            {
                var card = await context.CreditCards.Where(c => c.CardNumber == cardNumber).SingleAsync();
                card.Balance = amount;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DoesExistCreditCardAsync(string cardNumber)
        {
            using (var context = new RapidPayContext())
            {
                return await context.CreditCards.AnyAsync(c => c.CardNumber == cardNumber);
            }
        }
    }
}