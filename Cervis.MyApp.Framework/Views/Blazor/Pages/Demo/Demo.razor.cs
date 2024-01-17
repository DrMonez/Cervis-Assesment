using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Framework.Views.Blazor;
using Cervis.MyApp.Adapters.UI.Pages.Demo;
using Microsoft.AspNetCore.Components;

namespace Cervis.MyApp.Framework.Views.Blazor.Pages.Demo
{
	[DefaultViewForViewModel(typeof(DemoViewModel))]
	public partial class Demo : View<DemoViewModel>
	{
		[Inject]
		public ILocalizer<Demo> Localizer { get; set; }
		[Inject]
		public IApplicationLocalizer ApplicationLocalizer { get; set; }
	}
}
