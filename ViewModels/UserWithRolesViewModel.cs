namespace EcommerceDefense.ViewModels
{
    public class UserWithRolesViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = "/images/default-avatar.png";
        public List<string> Roles { get; set; } = new List<string>();
    }
}
