using System.Threading.Tasks;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;

namespace Cervis.MyApp.Adapters.UI.Pages.Home
{
	[ViewModelWithAppLocation(AppLocations.Home)]
	public class HomeViewModel : BaseViewModel, IViewModelWithAppLocation
	{
		public Task InitializeAsync(ApplicationViewModel applicationViewModel, NavigationGoal navigationGoal)
		{
			return Task.CompletedTask;
		}
	}
}
