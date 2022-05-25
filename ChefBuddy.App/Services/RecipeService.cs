using AutoMapper;
using ChefBuddy.App.Repositories;
using ChefBuddy.Models;

namespace ChefBuddy.App.Services;

public class RecipeService
{
    private readonly RecipeRepository _recipeRepository;
    private readonly IMapper _mapper;
        
    public RecipeService(RecipeRepository RecipeRepository, IMapper mapper)
    {
        _recipeRepository = RecipeRepository;
        _mapper = mapper;
    }
        
    public async Task<Recipe> CreateAsync(string ownerId, RecipeViewModel recipe)
    {
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe));
            
        recipe.OwnerId = ownerId;

        if (recipe.Image != null)
        {
            // TODO: Upload image
            // TODO: Place S3 URL in Image URL
        }
        foreach (var step in recipe.StepsViewModel)
        {
            if (step.Image != null)
            {
                // TODO: Upload image
                // TODO: Place S3 URL in Image URL
            }
        }

        return await _recipeRepository.CreateAsync(_mapper.Map<Recipe>(recipe));
    }

    public async Task<Recipe> Update(string id, RecipeViewModel recipe)
    {
        return await _recipeRepository.Update(id, recipe);
    }
        
    public async Task Delete(string id)
    {
        await _recipeRepository.Delete(id);
    }

    public async Task<Recipe> GetById(string id)
    {
        return await _recipeRepository.GetByIdAsync(id);
    }

    public async Task<List<Recipe>> GetAllByOwnerId(string ownerId)
    {
        return await _recipeRepository.GetAllByOwnerId(ownerId);
    }

    public async Task<List<Recipe>> GetAll()
    {
        return await _recipeRepository.GetAll();
    }
}