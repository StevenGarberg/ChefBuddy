using ChefBuddy.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ChefBuddy.App.Repositories;

public class RecipeRepository
{
    private readonly IMongoClient _mongoClient;

    public RecipeRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task<Recipe> CreateAsync(Recipe Recipe)
    {
        Recipe.Id = Guid.NewGuid().ToString();
        Recipe.CreatedAt = DateTime.UtcNow;
        Recipe.UpdatedAt = Recipe.CreatedAt;
        await GetCollection().InsertOneAsync(Recipe);
        return Recipe;
    }
        
    public async Task<Recipe> Update(string id, Recipe data)
    {
        data.UpdatedAt = DateTime.UtcNow;
        await GetCollection().ReplaceOneAsync(x => x.Id == id, data);
        return data;
    }
        
    public async Task Delete(string id)
    {
        await GetCollection().UpdateOneAsync(x => x.Id == id,
            Builders<Recipe>.Update.Set(x => x.Deleted, true));
    }

    public async Task<Recipe> GetByIdAsync(string id)
    {
        return await GetCollection().AsQueryable()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Recipe>> GetAll()
    {
        return await GetCollection().AsQueryable()
            .ToListAsync();
    }
        
    public async Task<List<Recipe>> GetAllByOwnerId(string ownerId)
    {
        return await GetCollection().AsQueryable()
            .Where(w => w.OwnerId == ownerId && !w.Deleted)
            .ToListAsync();
    }
        
    private IMongoCollection<Recipe> GetCollection()
    {
        IMongoDatabase database = _mongoClient.GetDatabase("ChefBuddy");
        IMongoCollection<Recipe> collection = database.GetCollection<Recipe>("Recipes");
        return collection;
    }
}