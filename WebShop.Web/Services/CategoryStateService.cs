namespace WebShop.Web.Services;

public class CategoryStateService
{
    public event Action? OnCategoryChanged;

    public void NotifyCategoryStateChanged() => OnCategoryChanged?.Invoke();
}
