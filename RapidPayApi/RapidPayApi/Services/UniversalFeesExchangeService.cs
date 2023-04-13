using RapidPayApi.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Concurrent;

namespace RapidPayApi.Services
{
    public interface IUniversalFeesExchangeService
    {
        decimal GetFee();
    }

    public class UniversalFeesExchangeService : IUniversalFeesExchangeService
    {
        private static readonly UniversalFeesExchangeService instance = new UniversalFeesExchangeService();

        private static decimal _fee = 1;
        private const decimal UPDATE_TIME_MINUTES = 5;
        private static readonly Random _random;

        static UniversalFeesExchangeService()
        {
            _random = new();

            _ = new Timer(e => UpdateFee(), null, TimeSpan.Zero, TimeSpan.FromSeconds((double)UPDATE_TIME_MINUTES));
            UpdateFee(); // to start with a different value than 1 during the first hour.
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

        public decimal GetFee()
        {
            return UniversalFeesExchangeService._fee;
        }
    }
}