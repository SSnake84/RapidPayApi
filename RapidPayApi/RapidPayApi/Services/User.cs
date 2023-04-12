using RapidPayApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPayApi.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly List<User> Users = new() {
            new User { Id = 1, Username = "Jose", Password = "Enser" },
            new User { Id = 2, Username = "Dario", Password = "Jose" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(
                () => Users.SingleOrDefault(
                    x => x.Username == username && x.Password == password
                )
            );
            
            if(user != null)
                user.Password = null;

            return user!;
        }
    }
}