/*
 * Copyright (c) 2023 Cervis GmbH
 *
 * This file is part of the Cervis.ClArc framework.
 *
 * All rights reserved. Unauthorized copying of this file, via any medium, 
 * is strictly prohibited. Proprietary and confidential.
 */

using System;
using System.Linq;
using System.Reflection;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Cervis.ClArc.Adapters;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.ClArc.Framework;
using Cervis.ClArc.Framework.Views.Blazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Cervis.ClArc.Host
{
	public class Startup
	{
		public IConfiguration Configuration { get; private set; }
		public IWebHostEnvironment Environment { get; }

		public Startup(IWebHostEnvironment environment)
		{
			Environment = environment;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// Visual Studio expects config files be in the project folder. This does not work with our approach of injecting them into the host application.
			// So we override this behavior and take the files from the output folder, next to the executable. This should work in both dev and runtime environment.
			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddJsonFile("appsettings.json", false);
			if (!string.IsNullOrEmpty(Environment.EnvironmentName))
				configurationBuilder.AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", true);
			configurationBuilder.AddEnvironmentVariables();
			configurationBuilder.AddEnvironmentVariables("ASPNETCORE_");
			Configuration = configurationBuilder.Build();

			services.AddSingleton(Configuration);

			services.AddLocalization();

			services.AddControllers()
				.AddApplicationPart(typeof(View<>).Assembly)
				.AddApplicationParts();

			services.AddAuthorization(options =>
			{
				options.ApplyMyAppAuthorizationOptions(Environment, Configuration);
			});

			services.AddBlazorise(options =>
			{
				options.ValidationMessageLocalizer = ValidationMessageLocalizer.Localize;
			});
			services.AddBootstrapProviders();
			services.AddFontAwesomeIcons();

			services.AddRazorPages(options =>
			{
				options.RootDirectory = "/Views/Blazor/Pages";
				options.ApplyMyAppRazorPagesOptions(Environment, Configuration);
			});

			services.AddServerSideBlazor();

			services.AddCervisClArcBlazor();
			services.AddCervisClArcAdapters();

			services.AddMyAppServices(Environment, Configuration);
		}

		public void Configure(IApplicationBuilder app)
		{
			ForwardedHeadersOptions forwardedHeadersOptions = new()
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
			};
			forwardedHeadersOptions.KnownProxies.Clear();   // remove IP restrictions
			forwardedHeadersOptions.KnownNetworks.Clear();  // remove IP range restrictions
			app.UseForwardedHeaders(forwardedHeadersOptions);

			if (Environment.IsCervisDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseExceptionHandler("/Error");

			CultureSupportOptions cultureSupportOptions = new();
			cultureSupportOptions.ApplyCultureSupportOptions(Environment, Configuration);

			RequestLocalizationOptions requestLocalizationOptions = new();
			requestLocalizationOptions.AddSupportedCultures(cultureSupportOptions.Cultures.Select(c => c.Name).ToArray());
			requestLocalizationOptions.AddSupportedUICultures(cultureSupportOptions.Cultures.Select(c => c.Name).ToArray());
			requestLocalizationOptions.SetDefaultCulture(cultureSupportOptions.DefaultCulture.Name);

			app.ApplicationServices.GetRequiredService<ILocalizationSetup>().Setup(cultureSupportOptions);
			app.UseRequestLocalization(requestLocalizationOptions);

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(routeBuilder =>
			{
				routeBuilder.BuildMyAppEndpointRoutes(Environment, Configuration);

				routeBuilder.MapBlazorHub();
				routeBuilder.MapFallbackToPage("/_Host");
				routeBuilder.MapDefaultControllerRoute();
			});

			IViewModelTypeRegistry viewModelTypeRegistry = app.ApplicationServices.GetRequiredService<IViewModelTypeRegistry>();
			viewModelTypeRegistry.AddViewModelTypeAssembly(typeof(BaseViewModel).Assembly);
			app.RegisterViewModelTypes(viewModelTypeRegistry);

			IViewComponentTypeRegistry viewComponentTypeRegistry = app.ApplicationServices.GetRequiredService<IViewComponentTypeRegistry>();
			viewComponentTypeRegistry.AddViewComponentTypeAssembly(Assembly.GetExecutingAssembly());
			app.RegisterViewComponentTypes(viewComponentTypeRegistry);

			IMutableStaticPageSettings staticPageSettings = app.ApplicationServices.GetRequiredService<IMutableStaticPageSettings>();
			staticPageSettings.Icon = new Uri("~/icons/cervis_c.svg", UriKind.Relative);
			staticPageSettings.Stylesheets.Add(new Uri("https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css"));
			staticPageSettings.Stylesheets.Add(new Uri("https://use.fontawesome.com/releases/v5.15.4/css/all.css"));
			staticPageSettings.Stylesheets.Add(new Uri("_content/Blazorise/blazorise.css", UriKind.Relative));
			staticPageSettings.Stylesheets.Add(new Uri("_content/Blazorise.Bootstrap/blazorise.bootstrap.css", UriKind.Relative));
			staticPageSettings.Stylesheets.Add(new Uri("_content/Blazorise.SpinKit/blazorise.spinkit.css", UriKind.Relative));
			staticPageSettings.Stylesheets.Add(new Uri("_content/Blazorise.TreeView/blazorise.treeview.css", UriKind.Relative));
			staticPageSettings.Stylesheets.Add(new Uri("_content/Blazorise.Snackbar/blazorise.snackbar.css", UriKind.Relative));
			staticPageSettings.Stylesheets.Add(new Uri("css/site.css", UriKind.Relative));
			staticPageSettings.Scripts.Add(new Uri("https://cdn.jsdelivr.net/npm/jquery@3.5.1/dist/jquery.slim.min.js"));
			staticPageSettings.Scripts.Add(new Uri("https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"));
			staticPageSettings.Scripts.Add(new Uri("https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.min.js"));
			staticPageSettings.Scripts.Add(new Uri("_framework/blazor.server.js", UriKind.Relative));
			app.ConfigureStaticPage(staticPageSettings);

			app.UseMyAppServices(Environment, Configuration);
		}
	}
}
