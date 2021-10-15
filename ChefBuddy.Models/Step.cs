using System.Collections.Generic;

namespace ChefBuddy.Models
{
    public class Step
    {
        public bool IsOptional { get; set; }
        
        public List<Ingredient> Ingredients { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
    }
}