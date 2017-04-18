using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace DashboardManaging
{
    public class AdminMenu : INavigationProvider
    {
        public string MenuName
        {
            get { return "admin"; }
        }

        public AdminMenu() {
            T = NullLocalizer.Instance;
        }

        private Localizer T { get; set; }

        public void GetNavigation(NavigationBuilder builder) {
                builder.Add(T("Test menu Item"), "2",
                    menu => menu.Action("Index", "DashboardAdmin", new { area = "DashboardManaging" })
                        .Add(T("Test menu Item"), "1.0", item => item.Action("Index", "DashboardAdmin", new { area = "DashboardManaging" })
                            .LocalNav().Permission(Permissions.TestPermission)));
                builder.Add(T("Second Test menu Item"), "3",
                    menu => menu.Action("SecondItemIndex", "DashboardAdmin", new {area = "DashboardManaging"})
                        .Add(T("Second Test menu Item"), "1.0", item => item.Action("SecondItemIndex", "DashboardAdmin", new { area = "DashboardManaging" })
                            .LocalNav().Permission(Permissions.SecondTestPermission)));
        }
    }
}