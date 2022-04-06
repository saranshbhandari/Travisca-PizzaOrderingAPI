using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PizzaOrderingSystem.Ordering.API.Models;
using PizzaOrderingSystem.Ordering.API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PizzaOrderingSystem.Ordering.API.Services
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly IMongoCollection<ItemCategory> _itemCategoryCollection;

        public ItemCategoryService(
            IOptions<PizzaOrderingSystemDBSettings> DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DatabaseSettings.Value.DatabaseName);

            _itemCategoryCollection = mongoDatabase.GetCollection<ItemCategory>(
                DatabaseSettings.Value.ItemCategoriesCollectionName);
        }

        #nullable enable
        public async Task<ItemCategory?> GetAsync(string id) =>
            await _itemCategoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<ItemCategory?> GetFromNameAsync(string name) =>
            await _itemCategoryCollection.Find(x => x.itemCategoryName == name).FirstOrDefaultAsync();
        
        #nullable disable
        public async Task<List<ItemCategory>> GetAsync() =>
            await _itemCategoryCollection.Find(_ => true).ToListAsync();


        public async Task CreateAsync(ItemCategory newMenuItem) =>
            await _itemCategoryCollection.InsertOneAsync(newMenuItem);

        public async Task UpdateAsync(string id, ItemCategory updatedMenuItem) =>
            await _itemCategoryCollection.ReplaceOneAsync(x => x.Id == id, updatedMenuItem);

        public async Task RemoveAsync(string id) =>
            await _itemCategoryCollection.DeleteOneAsync(x => x.Id == id);
    }
}
