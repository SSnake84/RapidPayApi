using RapidPayApi.Data.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Concurrent;

namespace RapidPayApi.Services
{
    public interface IUniversalFeesExchangeService
    {
        Task<decimal> GetFee();
    }

    public class UniversalFeesExchangeService : IUniversalFeesExchangeService
    {
        private decimal _fee = Constants.FEE_INITIALVALUE;
        private readonly Random _random;

        public UniversalFeesExchangeService()
        {
            _random = new();
            UpdateFee(); // to start with a different value than 1 during the first hour.

            _ = new Timer(e => UpdateFee(), null, TimeSpan.Zero, TimeSpan.FromSeconds(Constants.UFE_INTERVAL_SECONDS));
        }

        private void UpdateFee()
        {
            // we should change a little bit this logic but it's out of scope now.  Reason for a change?
            // a value like 0.0001 greatly reduces the result and then we would need many higher random numbers
            // to increase it, but since 2 is the maximum, it is doomed to always end up next to zero.
            decimal newRandom = 0;
            while (newRandom == 0)
                newRandom = (decimal)_random.NextDouble();

            _fee *= newRandom * 2;
        }

        public async Task<decimal> GetFee()
        {
            return await Task.FromResult(_fee);
        }
    }
}