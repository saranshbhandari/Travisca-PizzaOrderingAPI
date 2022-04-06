using System.Collections.Generic;

namespace PizzaOrderingSystem.Ordering.API.Models
{
    public class ItemCustomization
    {
        public string Name { get; set; }

        public int maxSelection { get; set; }

        public List<customizationkeyvalue> values { get; set; }

        public List<string> selectedValues;
    }
}
