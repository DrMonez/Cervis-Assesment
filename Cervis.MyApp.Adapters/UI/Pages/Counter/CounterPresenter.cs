using System.Threading.Tasks;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.MyApp.Business.Queries;

namespace Cervis.MyApp.Adapters.UI.Pages.Counter
{
	public interface ICounterPresenter
	{
		Task Initialize(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel);
		Task Update(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel);
	}

	class CounterPresenter : ICounterPresenter
	{
		private readonly ISingleCounterQuery singleCounterQuery;
		private readonly ILocalizer<CounterPresenter> localizer;
		private readonly IApplicationPresenter applicationPresenter;

		public CounterPresenter(ISingleCounterQuery singleCounterQuery, ILocalizer<CounterPresenter> localizer, IApplicationPresenter applicationPresenter)
		{
			this.singleCounterQuery = singleCounterQuery;
			this.localizer = localizer;
			this.applicationPresenter = applicationPresenter;
		}

		public async Task Initialize(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel)
		{
			Business.Model.Counter counter = await singleCounterQuery.Execute(counterViewModel.Id);

			if (counter == null)
			{
				// Initalize is called during navigation. We can't navigate when already navigating, so we're putting this in the Task queue
				_ = Task.Run(async () =>
				{
					await applicationPresenter.NavigateAsync(applicationViewModel, new NavigationGoal { Location = AppLocations.Home });
					await applicationPresenter.ShowErrorAsync(applicationViewModel, localizer["NotFoundError"]);
				});
			}
			else
			{
				using (counterViewModel.ChangeObservationContext)   // ChangeObservationContext checks the model for changes when disposed and raises them as INotifyPropertyChanged.PropertyChanged events
				{
					counterViewModel.Value = counter.Value;
				}
			}
		}

		public async Task Update(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel)
		{
			Business.Model.Counter counter = await singleCounterQuery.Execute(counterViewModel.Id);

			using (counterViewModel.ChangeObservationContext)
			{
				counterViewModel.Value = counter.Value;
			}
		}
	}
}
