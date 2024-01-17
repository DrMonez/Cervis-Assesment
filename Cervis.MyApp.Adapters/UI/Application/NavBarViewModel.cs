using Cervis.ClArc.Adapters.UI;
using Cervis.MyApp.Adapters.UI.Pages;

namespace Cervis.MyApp.Adapters.UI.Application
{
	public class NavBarViewModel : BaseViewModel
	{
		public NavigationGoal HomeNavigationGoal { get; } = new NavigationGoal { Location = AppLocations.Home };
		public NavigationGoal DemoNavigationGoal { get; } = new NavigationGoal { Location = AppLocations.Demo };
		public NavigationGoal CounterNavigationGoal { get; } = new NavigationGoal { Location = AppLocations.Counter };
		public NavigationGoal EventNavigationGoal { get; } = new NavigationGoal { Location = AppLocations.Event };
	}
}
