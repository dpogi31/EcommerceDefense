using System.Collections.Generic;

namespace EcommerceDefense.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<string> AvailableRoles { get; set; } = new();
        public List<string> CurrentRoles { get; set; } = new();
        public List<string> SelectedRoles { get; set; } = new();
    }
}
