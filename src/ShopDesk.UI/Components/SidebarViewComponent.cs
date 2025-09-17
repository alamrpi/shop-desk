using Microsoft.AspNetCore.Mvc;
using ShopDesk.UI.Models;

namespace ShopDesk.UI.Components
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // In a real application, this data would come from a database or a service.
            // For this conversion, we are hard-coding it as in the original PHP code.
            var menuItems = new List<MenuItemViewModel>
            {
                new MenuItemViewModel { Title = "Dashboard", Icon = "ph-light ph-house", RouteName = "Dashboard" },
                new MenuItemViewModel
                {
                    Title = "Product Management",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "New Product", Icon = "ph-plus", RouteName = "ProductCreate" },
                        new MenuItemViewModel { Title = "Agro Products", Icon = "ph-plus-circle", RouteName = "" },
                        new MenuItemViewModel { Title = "Farming Tools", Icon = "ph-plus-circle", RouteName = "" },
                        new MenuItemViewModel { Title = "Sub-category", Icon = "ph-plus-circle", RouteName = "ProductSubCategoriesIndex" },
                        new MenuItemViewModel { Title = "Category", Icon = "ph-plus-circle", RouteName = "ProductCategoriesIndex" }
                    }
                },
                new MenuItemViewModel { Title = "Buyers", Icon = "ph-light ph-users", RouteName = "BuyersIndex" },
                new MenuItemViewModel { Title = "Sellers", Icon = "ph-light ph-user-switch", RouteName = "SellersIndex" },
                new MenuItemViewModel { Title = "Traders", Icon = "ph-light ph-handshake", RouteName = "TradersIndex" },
                // Add other menu items similarly...
                 new MenuItemViewModel
                {
                    Title = "Website",
                    Icon = "ph-light ph-globe",
                    RouteName = "",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "FAQ", Icon = "ph-plus", RouteName = ""},
                        new MenuItemViewModel { Title = "Blog", Icon = "ph-plus-circle", RouteName = ""},
                    }
                },
            };

            // Fetch user data. This is a placeholder.
            // In a real app with authentication, you'd get this from HttpContext.User.
            var currentUser = new UserViewModel
            {
                Name = User.Identity?.Name ?? "Admin User",
                Email = "admin@krisheye.com" // Example email
            };

            var model = new SidebarViewModel
            {
                CurrentUser = currentUser,
                MenuItems = menuItems
            };

            return View(model); // This will look for Views/Shared/Components/Sidebar/Default.cshtml
        }
    }
}
