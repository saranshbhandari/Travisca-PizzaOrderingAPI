using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Ordering.API.Models
{
    public class ItemCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  

    
        public string itemCategoryName { get; set; }

        public bool BuildYourItemEnabled { get; set; }

        public List<customizationkeyvalue> SizeAvailable { get; set; }

        public string ItemDescription { get; set; }

        public List<ItemCustomization> Customization { get; set; }
    }

    public class customizationkeyvalue
    {
        public string name { get; set; }

        public float price { get; set; }
    }
}
