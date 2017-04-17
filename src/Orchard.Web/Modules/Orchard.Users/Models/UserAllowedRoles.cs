using System.Collections;
using System.Collections.Generic;

namespace Orchard.Users.Models
{
    public static class UserAllowedRoles {
        public static string _rolesAllowed = "MyRole,HisRole,YourRole";
        public static Dictionary<string, IList<string>> _roleAllowed = new Dictionary<string, IList<string>>() {
            {"MyRole", new List<string> {"MyRole", "HisRole", "YourRole", "Editor"}},
            {"HisRole", new List<string> {"HisRole"}},
            {"Administrator", new List<string> {"MyRole", "HisRole", "YourRole", "HiddenRole"}}
        };
    }
}