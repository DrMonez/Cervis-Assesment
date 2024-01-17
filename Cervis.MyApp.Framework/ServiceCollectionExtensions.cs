using Cervis.ClArc.Framework.Views.Blazor;
using Cervis.MyApp.Framework.Views.Blazor;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCervisMyAppApplication(this IServiceCollection services)
		{
			services.AddSingleton<INavigationGoalMapper, NavigationGoalMapper>();

			return services;
		}
	}
}
