using Cervis.ClArc.Adapters.UI.Application;
using Microsoft.AspNetCore.Components;

namespace Cervis.MyApp.Framework.Views.Blazor
{
	public partial class MainLayout : LayoutComponentBase
	{
		[CascadingParameter]
		public ApplicationViewModel ViewModel { get; set; }
	}
}
