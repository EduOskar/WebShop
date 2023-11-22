using Microsoft.AspNetCore.Components;
using WebShop.Models.DTOs;

namespace WebShop.Web.Components.Pages;

public class DisplayProductsBase : ComponentBase
{
    [Parameter]
    public IEnumerable<ProductDto>? Products { get; set; }
}
