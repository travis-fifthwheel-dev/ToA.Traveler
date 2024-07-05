using Fluxor.Blazor.Web.Components;

namespace ToA.Traveler.Layout;

public class MainLayoutBase : FluxorLayout
{
    protected bool IsDrawerOpen = false;

    protected void DrawerToggle()
    {
        IsDrawerOpen = !IsDrawerOpen;
    }
}
