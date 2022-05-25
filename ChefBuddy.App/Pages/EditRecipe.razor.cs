using AutoMapper;
using ChefBuddy.App.Services;
using ChefBuddy.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace ChefBuddy.App.Pages;

public partial class EditRecipe
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private DialogService  DialogService  { get; set; }
    [Inject] private NotificationService NotificationService { get; set; }
    [Inject] private RecipeService RecipeService { get; set; }
    [Inject] private IMapper Mapper { get; set; }

    [Parameter] public string Id { get; set; }

    private string userId;
    private RecipeViewModel recipe;

    private bool loadFailed = false;
    private Exception exception;
    
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        userId = Guid.Empty.ToString();
            
        try
        {
            if (Id != null)
            {
                var recipeEntity = await RecipeService.GetById(Id);
                if (recipeEntity == null)
                {
                    NavigationManager.NavigateTo("/workouts");
                }
                recipe = Mapper.Map<RecipeViewModel>(recipeEntity);
            }
            else
            {
                recipe = new RecipeViewModel
                {
                    OwnerId = userId
                };
            }
        }
        catch(Exception e)
        {
            loadFailed = true;
            exception = e;
            NavigationManager.NavigateTo("/");
        }
    }
    
    private async Task Submit()
    {
        if (Id != null)
        {
            recipe = Mapper.Map<RecipeViewModel>(await RecipeService.Update(Id, recipe));
        }
        else
        {
            recipe = Mapper.Map<RecipeViewModel>(await RecipeService.CreateAsync(userId, recipe));   
        }

        NavigationManager.NavigateTo("/recipes");
    }
    
    private void Cancel()
    {
        NavigationManager.NavigateTo("/recipes");
    }
    
    private void AddStep()
    {
        var step = new StepViewModel();
        recipe.StepsViewModel.Add(step);
    }
    
    private async Task RemoveStep(StepViewModel step)
    {
        var confirmed = await DialogService.Confirm("Are you sure?", "Delete Step",
            new ConfirmOptions {OkButtonText = "Yes", CancelButtonText = "No"});
            
        recipe.StepsViewModel.Remove(step);
            
        NotificationService.Notify(new NotificationMessage
        {
            Summary = "Success!", Detail = $"\"{(!string.IsNullOrWhiteSpace(step.Name) ? step.Name : "Step")}\" was deleted.", Duration = 5000f, Severity = NotificationSeverity.Success
        });
    }
    
    private void MoveDown<TStep>(int index, List<TStep> steps) where TStep : Step
    {
        var exercise = steps[index];
        var tempExercise = steps[index + 1];
        steps[index + 1] = exercise;
        steps[index] = tempExercise;

    }

    private void MoveUp<TStep>(int index, List<TStep> steps) where TStep : Step
    {
        var exercise = steps[index];
        var tempExercise = steps[index - 1];
        steps[index - 1] = exercise;
        steps[index] = tempExercise;
    }
    
    private void UpdateName(object value, Step step)
    {
        step.Name = value.ToString();
    }
}