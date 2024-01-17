using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Framework.Views.Blazor;
using Cervis.MyApp.Adapters.UI.Pages.Home;
using Microsoft.AspNetCore.Components;

namespace Cervis.MyApp.Framework.Views.Blazor.Pages.Home
{
	[DefaultViewForViewModel(typeof(HomeViewModel))]
	public partial class Home : View<HomeViewModel>
	{
		[Inject]
		public ILocalizer<Home> Localizer { get; set; }
		[Inject]
		public IApplicationLocalizer ApplicationLocalizer { get; set; }
	}
}
