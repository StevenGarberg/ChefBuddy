using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;

namespace ChefBuddy.Models
{
    public class RecipeViewModel : Recipe
    {
        [BsonIgnore]
        public IFormFile Image { get; set; }
        
        [BsonIgnore]
        public List<StepViewModel> StepsViewModel { get; set; } = new();
    }

    public class StepViewModel : Step
    {
        [BsonIgnore]
        public IFormFile Image { get; set; }
    }
    
}