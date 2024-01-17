using System.Threading.Tasks;
using System.Windows.Input;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;

namespace Cervis.MyApp.Adapters.UI.Pages.Demo
{
	[ViewModelWithAppLocation(AppLocations.Demo)]
	public class DemoViewModel : BaseViewModel, IViewModelWithAppLocation
	{
		public ApplicationViewModel Application { get; private set; }
		public ICommand ShowClockCommand { get; }

		private readonly IDemoViewController demoViewController;

		public DemoViewModel(IDemoViewController demoViewController)
		{
			this.demoViewController = demoViewController;

			ShowClockCommand = new Command(ShowClock);
		}

		public Task InitializeAsync(ApplicationViewModel applicationViewModel, NavigationGoal navigationGoal)
		{
			Application = applicationViewModel;

			return Task.CompletedTask;
		}

		private async Task ShowClock()
		{
			await demoViewController.ShowClock(Application);
		}
	}
}
