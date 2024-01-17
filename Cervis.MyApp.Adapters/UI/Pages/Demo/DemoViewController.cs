using System.Threading.Tasks;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.MyApp.Adapters.UI.Components.Clock;

namespace Cervis.MyApp.Adapters.UI.Pages.Demo
{
	public interface IDemoViewController
	{
		Task ShowClock(ApplicationViewModel applicationViewModel);
	}

	class DemoViewController : IDemoViewController
	{
		private readonly IClockPresenter clockPresenter;

		public DemoViewController(IClockPresenter clockPresenter)
		{
			this.clockPresenter = clockPresenter;
		}

		public async Task ShowClock(ApplicationViewModel applicationViewModel)
		{
			await clockPresenter.Show(applicationViewModel);
		}
	}
}
