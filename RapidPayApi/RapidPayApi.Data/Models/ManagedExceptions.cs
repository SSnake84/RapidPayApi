namespace RapidPayApi.Data.Models
{
    public class ManagedExceptions : Exception
    {
        public ManagedExceptions(string message) : base(message) { }
    }
    public class CardDoesntExistException : Exception
    {
    }
}
