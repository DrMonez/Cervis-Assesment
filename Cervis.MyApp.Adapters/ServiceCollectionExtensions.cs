using Cervis.MyApp.Adapters.UI.Components.Clock;
using Cervis.MyApp.Adapters.UI.Components.Participant;
using Cervis.MyApp.Adapters.UI.Pages.Counter;
using Cervis.MyApp.Adapters.UI.Pages.Demo;
using Cervis.MyApp.Adapters.UI.Pages.Event;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCervisMyAppAdaptersUI(this IServiceCollection services)
		{
			services.AddSingleton<ICounterPresenter, CounterPresenter>();
			services.AddSingleton<ICounterViewController, CounterViewController>();

			services.AddSingleton<IDemoViewController, DemoViewController>();

			services.AddSingleton<IClockPresenter, ClockPresenter>();

			services.AddSingleton<IEventViewController, EventViewController>();
			services.AddSingleton<IEventPresenter, EventPresenter>();
			services.AddSingleton<IParticipantPresenter, ParticipantPresenter>();

			return services;
		}
	}
}
