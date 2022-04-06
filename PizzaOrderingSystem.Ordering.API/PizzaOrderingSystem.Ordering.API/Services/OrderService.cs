using PizzaOrderingSystem.Ordering.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
namespace PizzaOrderingSystem.Ordering.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderService(
            IOptions<PizzaOrderingSystemDBSettings> DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DatabaseSettings.Value.DatabaseName);

            _orderCollection = mongoDatabase.GetCollection<Order>(
                DatabaseSettings.Value.OrderCollectionName);
        }

        #nullable enable
        public async Task<Order?> GetAsync(string id) =>
        await _orderCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Order?> GetFromOrderIDAsync(string id) =>
        await _orderCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
         #nullable disable


        public async Task<List<Order>> GetTodaysInCompleteOrderAsync() =>
            await _orderCollection.Find(x => x.OrderedPlacedTime > DateTime.Today && x.OrderStatus.ToLower() != "complete").ToListAsync();

        public async Task<List<Order>> GetTodaysOrderAsync() =>
        await _orderCollection.Find(x => x.OrderedPlacedTime > DateTime.Today).ToListAsync();
        
        public async Task<List<Order>> GetAsync() =>
            await _orderCollection.Find(_ => true).ToListAsync();


       

        public async Task CreateAsync(Order newMenuItem) =>
            await _orderCollection.InsertOneAsync(newMenuItem);


        public async Task UpdateAsync(string id, Order updatedOrder) =>
            await _orderCollection.ReplaceOneAsync(x => x.Id == id, updatedOrder);

        public async Task RemoveAsync(string id) =>
            await _orderCollection.DeleteOneAsync(x => x.Id == id);

    }
}

