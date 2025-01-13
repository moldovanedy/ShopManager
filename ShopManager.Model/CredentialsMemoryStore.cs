using ShopManager.Controller.DataModels;

namespace ShopManager.Controller
{
    public static class CredentialsMemoryStore
    {
        public static User CurrentUser { get; set; } = null;
    }
}
