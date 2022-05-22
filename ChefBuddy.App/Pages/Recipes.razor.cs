using ChefBuddy.App.Services;
using ChefBuddy.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace ChefBuddy.App.Pages;

public partial class Recipes
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private DialogService  DialogService  { get; set; }
    [Inject] private NotificationService NotificationService { get; set; }
    [Inject] private RecipeService RecipeService { get; set; }

    private string userId;
    private List<Recipe> recipes;
    
    private RadzenGrid<Recipe> grid;

    private bool loadFailed = false;
    private Exception exception;
    
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        userId = Guid.Empty.ToString();
            
        try
        {
            recipes = await RecipeService.GetAllByOwnerId(userId);
        }
        catch(Exception e)
        {
            loadFailed = true;
            exception = e;
            NavigationManager.NavigateTo("/");
        }
    }
    
    private void Create()
    {
        NavigationManager.NavigateTo("/recipes/add");
    }
        
    private void Edit(string id)
    {
            
        NavigationManager.NavigateTo($"/recipes/{id}/edit");
    }
        
    private async Task Delete(string id)
    {
        var confirmed = await DialogService.Confirm("Are you sure?", "Delete Recipe",
            new ConfirmOptions {OkButtonText = "Yes", CancelButtonText = "No"});

        if (confirmed == true)
        {
            await RecipeService.Delete(id);
                
            recipes.RemoveAll(a => a.Id == id);
            await grid.Reload();
                
            NotificationService.Notify(new NotificationMessage
            {
                Summary = "Success!", Detail = "Recipe was deleted.", Duration = 5000f, Severity = NotificationSeverity.Success
            });
        }
    }
}