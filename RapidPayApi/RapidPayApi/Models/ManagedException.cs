namespace RapidPayApi.Models
{
    public class ManagedException : Exception
    {
        public ManagedException(string message) : base(message)
        {
        }
    }
}
