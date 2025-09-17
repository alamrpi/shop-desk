namespace ShopDesk.UI.Models
{
    public class MenuItemViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Svg { get; set; }
        public string? RouteName { get; set; } // We'll use route name for generating URLs
        public List<MenuItemViewModel> SubItems { get; set; } = new List<MenuItemViewModel>();

        // A helper property to check if the menu item has children.
        public bool HasSubItems => SubItems.Any();
    }
}
