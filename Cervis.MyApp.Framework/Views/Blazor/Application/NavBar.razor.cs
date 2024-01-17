using Cervis.ClArc.Adapters.UI;
using Cervis.MyApp.Adapters.UI.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cervis.MyApp.Framework.Views.Blazor.Application
{
	public partial class NavBar
	{
		[Parameter]
		public NavBarViewModel ViewModel { get; set; }

		[Inject]
		public IApplicationLocalizer ApplicationLocalizer { get; set; }
		[Inject]
		public ILocalizer<NavBar> Localizer { get; set; }
		[Inject]
		public IWebHostEnvironment WebHostEnviroment { get; set; }
		[Inject]
		public IConfiguration Configuration { get; set; }
	}
}
