using RapidPayApi.Models;
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
        private static readonly UniversalFeesExchangeService instance = new UniversalFeesExchangeService();

        private static decimal _fee = Constants.FEE_INITIALVALUE;
        private static readonly Random _random;

        static UniversalFeesExchangeService()
        {
            _random = new();
            UpdateFee(); // to start with a different value than 1 during the first hour.

            _ = new Timer(e => UpdateFee(), null, TimeSpan.Zero, TimeSpan.FromSeconds(Constants.UFE_INTERVAL_SECONDS));
        }

        private UniversalFeesExchangeService() { }

        public static UniversalFeesExchangeService GetInstance()
        {
            return instance;
        }

        private static void UpdateFee()
        {
            decimal newRandom = 0;
            while (newRandom == 0)
                newRandom = (decimal)_random.NextDouble();

            _fee *= newRandom * 2;
        }

        public async Task<decimal> GetFee()
        {
            return await Task.FromResult(UniversalFeesExchangeService._fee);
        }
    }
}