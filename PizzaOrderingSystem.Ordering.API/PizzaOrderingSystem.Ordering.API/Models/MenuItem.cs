using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PizzaOrderingSystem.Ordering.API.Models
{
    public class MenuItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

      
        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public string itemCategoryName { get; set; }

        public Dictionary<string, List<string>> Customization { get; set; }


        public List<customizationkeyvalue> SizeAvailable { get; set; }

    }
}
