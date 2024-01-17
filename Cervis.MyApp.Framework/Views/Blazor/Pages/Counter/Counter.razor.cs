using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Framework.Views.Blazor;
using Cervis.MyApp.Adapters.UI.Pages.Counter;
using Microsoft.AspNetCore.Components;

namespace Cervis.MyApp.Framework.Views.Blazor.Pages.Counter
{
	[DefaultViewForViewModel(typeof(CounterViewModel))]
	public partial class Counter : View<CounterViewModel>
	{
		[Inject]
		public ILocalizer<Counter> Localizer { get; set; }
		[Inject]
		public IApplicationLocalizer ApplicationLocalizer { get; set; }
		[Inject]
		public INavigationGoalMapper NavigationGoalMapper { get; set; }
	}
}
