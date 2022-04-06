namespace PizzaOrderingSystem.Ordering.API.Models
{
    public class PizzaOrderingSystemDBSettings
    {
            public string ConnectionString { get; set; } = null!;

            public string DatabaseName { get; set; } = null!;

            public string menuItemCollectionName { get; set; } = null!;
            
            public string ItemCategoriesCollectionName { get; set; } = null!;
            
            public string OrderCollectionName { get; set; } = null!;

            public string UserCollectionName { get; set; } = null!;


    }
}
