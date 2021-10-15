using System;
using System.Collections.Generic;
using ChefBuddy.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChefBuddy.API.Controllers
{
    [ApiController]
    [Route("recipe")]
    public class RecipeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Recipe> GetAll()
        {
            return new List<Recipe>
            {
                new Recipe
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    OwnerId = "Big Daddy 420",
                    Steps = new List<Step>
                    {
                        new Step
                        {
                            Description = "Heat your oven to 400 degrees.",
                            ImageUrl = "https://example.com/my-picture.jpg"
                        },
                        new Step
                        {
                            Description = "Sprinkle the spices",
                            Ingredients = new List<Ingredient>
                            {
                                new Ingredient { Name = "Salt", Amount = "a pinch", Description = "Preferably sea salt" },
                                new Ingredient { Name = "Ground black pepper", Amount = "a smidge" }
                            }
                        }
                    }
                },
                new Recipe { Id = Guid.NewGuid().ToString() },
                new Recipe { Id = Guid.NewGuid().ToString() }
            };
        }
        
        [HttpGet("id")]
        public Recipe GetById(string id)
        {
            return new Recipe
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                OwnerId = "Big Daddy 420",
                Steps = new List<Step>
                {
                    new Step
                    {
                        Description = "Heat your oven to 400 degrees.",
                        ImageUrl = "https://example.com/my-picture.jpg"
                    },
                    new Step
                    {
                        IsOptional = true,
                        Description = "Sprinkle the spices",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Name = "Salt", Amount = "a pinch", Description = "Preferably sea salt" },
                            new Ingredient { Name = "Ground black pepper", Amount = "a smidge" }
                        }
                    }
                }
            };
        }
    }
}