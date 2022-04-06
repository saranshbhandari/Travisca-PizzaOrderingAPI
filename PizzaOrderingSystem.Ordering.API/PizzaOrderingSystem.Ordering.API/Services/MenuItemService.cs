using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PizzaOrderingSystem.Ordering.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PizzaOrderingSystem.Ordering.API.Services
{
    public class MenuItemService: IMenuItemService
    {
        private readonly IMongoCollection<MenuItem> _menuItemCollection;

        public MenuItemService(
            IOptions<PizzaOrderingSystemDBSettings> DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DatabaseSettings.Value.DatabaseName);

            _menuItemCollection = mongoDatabase.GetCollection<MenuItem>(
                DatabaseSettings.Value.menuItemCollectionName);
        }

        #nullable enable
        public async Task<MenuItem?> GetFromNameAsync(string name) =>
            await _menuItemCollection.Find(x => x.ItemName == name).FirstOrDefaultAsync();

        public async Task<MenuItem?> GetAsync(string id) =>
            await _menuItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        #nullable disable 
        public async Task<List<MenuItem>> GetAsync() =>
            await _menuItemCollection.Find(_ => true).ToListAsync();

        public async Task<List<MenuItem>> GetFromCategoryAsync(string categoryname) =>
            await _menuItemCollection.Find(x => x.itemCategoryName== categoryname).ToListAsync();

        public async Task CreateAsync(MenuItem newMenuItem) =>
            await _menuItemCollection.InsertOneAsync(newMenuItem);

        public async Task UpdateAsync(string id, MenuItem updatedMenuItem) =>
            await _menuItemCollection.ReplaceOneAsync(x => x.Id == id, updatedMenuItem);

        public async Task RemoveAsync(string id) =>
            await _menuItemCollection.DeleteOneAsync(x => x.Id == id);
    }
}
