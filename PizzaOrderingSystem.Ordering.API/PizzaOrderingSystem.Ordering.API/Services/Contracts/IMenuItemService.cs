using PizzaOrderingSystem.Ordering.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PizzaOrderingSystem.Ordering.API.Services
{
    public interface IMenuItemService
    {
        #nullable enable
        public Task<MenuItem?> GetAsync(string id);
        public Task<MenuItem?> GetFromNameAsync(string name);
        #nullable disable
        
        public Task<List<MenuItem>> GetAsync();
        
        public Task<List<MenuItem>> GetFromCategoryAsync(string categoryname);
        
        public Task CreateAsync(MenuItem newMenuItem);

        public Task UpdateAsync(string id, MenuItem updatedMenuItem);

        public Task RemoveAsync(string id);
    }
}
