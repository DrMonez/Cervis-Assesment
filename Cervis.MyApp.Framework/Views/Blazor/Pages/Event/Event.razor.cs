using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Framework.Views.Blazor;
using Cervis.MyApp.Adapters.UI.Pages.Event;
using Microsoft.AspNetCore.Components;

namespace Cervis.MyApp.Framework.Views.Blazor.Pages.Event
{
	[DefaultViewForViewModel(typeof(EventViewModel))]
	public partial class Event : View<EventViewModel>
	{
		[Inject]
		public ILocalizer<Event> Localizer { get; set; }
		[Inject]
		public IApplicationLocalizer ApplicationLocalizer { get; set; }
		[Inject]
		public INavigationGoalMapper NavigationGoalMapper { get; set; }
	}
}
