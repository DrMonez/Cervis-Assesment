using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Framework.Views.Blazor;
using Cervis.MyApp.Adapters.UI.Components.Clock;
using Microsoft.AspNetCore.Components;

namespace Cervis.MyApp.Framework.Views.Blazor.Components.Clock
{
	[DefaultViewForViewModel(typeof(ClockViewModel))]
	public partial class Clock : View<ClockViewModel>
	{
		[Inject]
		public ILocalizer<Clock> Localizer { get; set; }
	}
}
