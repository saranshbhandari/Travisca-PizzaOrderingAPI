using PizzaOrderingSystem.Ordering.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PizzaOrderingSystem.Ordering.API.Services
{
    public interface IOrderService
    {
        #nullable enable
        public Task<Order?> GetAsync(string id);

        public Task<Order?> GetFromOrderIDAsync(string id);

        public Task<List<Order>> GetTodaysOrderAsync();

        public Task<List<Order>> GetTodaysInCompleteOrderAsync();

        #nullable disable
        public Task<List<Order>> GetAsync();

        public Task UpdateAsync(string id, Order updatedOrder);

        public Task RemoveAsync(string id);

        public Task CreateAsync(Order newOrder);
    }
}
