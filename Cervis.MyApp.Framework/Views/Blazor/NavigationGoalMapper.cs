using System;
using System.Collections.Generic;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Framework.Views.Blazor;
using Cervis.MyApp.Adapters.UI.Pages;

namespace Cervis.MyApp.Framework.Views.Blazor
{
	class NavigationGoalMapper : INavigationGoalMapper
	{
		public NavigationGoal? MapToNavigationGoal(string localPath)
		{
			string[] parts = localPath.Split('/');

			return parts[0] switch
			{
				"" => new NavigationGoal { Location = AppLocations.Home },
				"demo" => new NavigationGoal { Location = AppLocations.Demo },
				"counter" => parts.Length == 1 ? new NavigationGoal { Location = AppLocations.Counter, Parameters = new Dictionary<string, object>() }
					: new NavigationGoal { Location = AppLocations.Counter, Parameters = new Dictionary<string, object> { { "Id", parts[1] } } },
				"event" => new NavigationGoal { Location = AppLocations.Event },
				_ => null,
			};
		}

		public string MapToPath(NavigationGoal navigationGoal)
		{
			return navigationGoal.Location switch
			{
				AppLocations.Home => string.Empty,
				AppLocations.Demo => "demo",
				AppLocations.Counter => navigationGoal.Parameters?.ContainsKey("Id") == true ? $"counter/{navigationGoal.Parameters["Id"]}" : "counter",
				AppLocations.Event => navigationGoal.Parameters?.ContainsKey("Id") == true ? $"event/{navigationGoal.Parameters["Id"]}" : "event",
				_ => throw new InvalidOperationException("Unexpected case: " + navigationGoal.Location),
			};
		}
	}
}
