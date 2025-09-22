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
                    Title = "Administration",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "Setup", Icon = "ph-plus", RouteName = "ProductCreate" },
                        new MenuItemViewModel { Title = "Settings", Icon = "ph-plus-circle", RouteName = "ProductCategoriesIndex" }
                    }
                },
                new MenuItemViewModel
                {
                    Title = "Financials",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "Chart of Accounts", Icon = "ph-plus", RouteName = "ProductCreate" }
                    }
                },
                new MenuItemViewModel
                {
                    Title = "Sales Management",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "Chart of Accounts", Icon = "ph-plus", RouteName = "ProductCreate" }
                    }
                },

                 new MenuItemViewModel
                {
                    Title = "Purchase Management",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "Chart of Accounts", Icon = "ph-plus", RouteName = "ProductCreate" }
                    }
                },
                   new MenuItemViewModel
                {
                    Title = "Business Partner",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "Chart of Accounts", Icon = "ph-plus", RouteName = "ProductCreate" }
                    }
                },
                    new MenuItemViewModel
                {
                    Title = "Banking",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "Incoming Payments", Icon = "ph-plus", RouteName = "ProductCreate" },
                        new MenuItemViewModel { Title = "Outgoing Payments", Icon = "ph-plus", RouteName = "ProductCreate" }
                    }
                },
                     new MenuItemViewModel
                {
                    Title = "Inventory Management",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "Items", Icon = "ph-plus", RouteName = "ProductCreate" },
                        new MenuItemViewModel { Title = "Goods Receipt", Icon = "ph-plus", RouteName = "ProductCreate" },
                          new MenuItemViewModel { Title = "Goods Issue", Icon = "ph-plus", RouteName = "ProductCreate" },
                            new MenuItemViewModel { Title = "Inventory Transfer Request", Icon = "ph-plus", RouteName = "ProductCreate" },
                              new MenuItemViewModel { Title = "Inventory Transfer", Icon = "ph-plus", RouteName = "ProductCreate" }
                    }
                },
                       new MenuItemViewModel
                {
                    Title = "Human Resource",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "Employees", Icon = "ph-plus", RouteName = "ProductCreate" },
                       
                    }
                },
                               new MenuItemViewModel
                {
                    Title = "Reports",
                    Icon = "ph-light ph-gift",
                    SubItems = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel { Title = "Financials Reports", Icon = "ph-plus", RouteName = "ProductCreate" },

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
