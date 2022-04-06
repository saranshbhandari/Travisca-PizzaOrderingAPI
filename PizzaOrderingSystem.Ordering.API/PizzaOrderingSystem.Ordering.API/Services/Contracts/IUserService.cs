using PizzaOrderingSystem.Ordering.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PizzaOrderingSystem.Ordering.API.Services
{
    public interface IUserService
    {
        #nullable enable
        public Task<User?> GetAsync(string id);
        public Task<User?> GetFromNameAsync(string name);
        
        #nullable disable
        
        public Task<List<User>> GetAsync();

        public Task CreateAsync(User newUser);

        public Task UpdateAsync(string id, User updatedUser);

        public Task RemoveAsync(string id);
    }
}
