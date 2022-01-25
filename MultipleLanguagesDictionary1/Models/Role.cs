using Microsoft.AspNetCore.Identity;

namespace MultipleLanguagesDictionary1.Models
{
    public class RoleModel 
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
    }
}