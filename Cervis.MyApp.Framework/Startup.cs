using System.Globalization;
using System.Reflection;
using Cervis.ClArc.Adapters;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.ClArc.Framework.Views.Blazor;
using Cervis.MyApp.Adapters.UI.Application;
using Cervis.MyApp.Adapters.UI.Pages.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen

namespace Cervis.ClArc.Host
{
	public static class StartupExtensions
	{
		public static AuthorizationOptions ApplyMyAppAuthorizationOptions(this AuthorizationOptions options, IWebHostEnvironment environment, IConfiguration configuration)
		{
			return options;
		}

		public static RazorPagesOptions ApplyMyAppRazorPagesOptions(this RazorPagesOptions options, IWebHostEnvironment environment, IConfiguration configuration)
		{
			return options;
		}

		public static IEndpointRouteBuilder BuildMyAppEndpointRoutes(this IEndpointRouteBuilder builder, IWebHostEnvironment environment, IConfiguration configuration)
		{
			return builder;
		}

		public static IServiceCollection AddMyAppServices(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
		{
			services.AddCervisMyAppApplication();
			services.AddCervisMyAppAdaptersUI();
			services.AddCervisMyAppBusiness();

			return services;
		}

		public static CultureSupportOptions ApplyCultureSupportOptions(this CultureSupportOptions options, IWebHostEnvironment environment, IConfiguration configuration)
		{
			options.WithDefaultCulture(new CultureInfo("de-DE"))
				.AddCulture(new CultureInfo("de-DE"))
				.AddCulture(new CultureInfo("en-US"));

			return options;
		}

		public static IApplicationBuilder UseMyAppServices(this IApplicationBuilder builder, IWebHostEnvironment environment, IConfiguration configuration)
		{

			return builder;
		}

		public static IApplicationBuilder RegisterViewComponentTypes(this IApplicationBuilder builder, IViewComponentTypeRegistry viewComponentTypeRegistry)
		{
			viewComponentTypeRegistry.AddViewComponentTypeAssembly(typeof(MyApp.Framework.Views.Blazor.Pages.Home.Home).Assembly);

			return builder;
		}

		public static IApplicationBuilder RegisterViewModelTypes(this IApplicationBuilder builder, IViewModelTypeRegistry viewModelTypeRegistry)
		{
			viewModelTypeRegistry.SetApplicationViewModelType(typeof(MyAppApplicationViewModel));
			viewModelTypeRegistry.AddViewModelTypeAssembly(typeof(HomeViewModel).Assembly);

			return builder;
		}

		public static IApplicationBuilder ConfigureStaticPage(this IApplicationBuilder builder, IMutableStaticPageSettings staticPageSettings)
		{
			staticPageSettings.Title = "Cervis.MyApp";

			return builder;
		}

		public static IMvcBuilder AddApplicationParts(this IMvcBuilder builder)
		{
			builder.AddApplicationPart(Assembly.GetExecutingAssembly());

			return builder;
		}
	}
}
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen
