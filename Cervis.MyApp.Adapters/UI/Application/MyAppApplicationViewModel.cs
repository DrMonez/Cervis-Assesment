using System;
using Cervis.ClArc.Adapters.UI.Application;

namespace Cervis.MyApp.Adapters.UI.Application
{
	public class MyAppApplicationViewModel : ApplicationViewModel
	{
		public NavBarViewModel NavMenu { get; }

		public MyAppApplicationViewModel(IServiceProvider provider) : base(provider)
		{
			NavMenu = CreateViewModel<NavBarViewModel>();
		}
	}
}
