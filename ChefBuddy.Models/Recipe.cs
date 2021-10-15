using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace ChefBuddy.Models
{
    public class Recipe : BaseResource
    {
        public List<Step> Steps { get; set; } = new List<Step>();
        
        [JsonIgnore]
        public List<Ingredient> Ingredients => Steps?.SelectMany(s => s.Ingredients).Distinct().ToList();
    }
}