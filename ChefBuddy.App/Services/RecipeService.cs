using ChefBuddy.App.Repositories;
using ChefBuddy.Models;

namespace ChefBuddy.App.Services;

public class RecipeService
{
    private readonly RecipeRepository _RecipeRepository;
        
    public RecipeService(RecipeRepository RecipeRepository)
    {
        _RecipeRepository = RecipeRepository;
    }
        
    public async Task<Recipe> CreateAsync(string ownerId, Recipe Recipe)
    {
        if (Recipe == null)
            throw new ArgumentNullException(nameof(Recipe));
            
        Recipe.OwnerId = ownerId;
            
        return await _RecipeRepository.CreateAsync(Recipe);
    }

    public async Task<Recipe> Update(string id, Recipe Recipe)
    {
        return await _RecipeRepository.Update(id, Recipe);
    }
        
    public async Task Delete(string id)
    {
        await _RecipeRepository.Delete(id);
    }

    public async Task<Recipe> GetById(string id)
    {
        return await _RecipeRepository.GetByIdAsync(id);
    }

    public async Task<List<Recipe>> GetAllByOwnerId(string ownerId)
    {
        return await _RecipeRepository.GetAllByOwnerId(ownerId);
    }

    public async Task<List<Recipe>> GetAll()
    {
        return await _RecipeRepository.GetAll();
    }
}