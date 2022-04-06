using PizzaOrderingSystem.Ordering.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Ordering.API.Services
{
    #nullable enable
    public interface IItemCategoryService
    {
        public Task<List<ItemCategory>> GetAsync();

        public Task<ItemCategory?> GetAsync(string id);

        public Task<ItemCategory?> GetFromNameAsync(string name);

        public Task CreateAsync(ItemCategory newItemCategory);

        public Task UpdateAsync(string id, ItemCategory updatedItemCategory);

        public Task RemoveAsync(string id);
    }
}
