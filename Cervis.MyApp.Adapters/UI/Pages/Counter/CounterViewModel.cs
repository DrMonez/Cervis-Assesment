using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.MyApp.Business.Gateways;

namespace Cervis.MyApp.Adapters.UI.Pages.Counter
{
	[ViewModelWithAppLocation(AppLocations.Counter)]
	public class CounterViewModel : BaseViewModel, IViewModelWithAppLocation
	{
		public string Id { get; internal set; }
		public int Value { get; internal set; }

#pragma warning disable CA1822
		//private bool CanIncrement => Value < Business.Model.Counter.MaxValue;		disabled for demonstration
		private bool CanIncrement => true;
#pragma warning restore CA1822

		public NavigationGoal ShareNavigationGoal => new() { Location = AppLocations.Counter, Parameters = new Dictionary<string, object> { { "Id", Id } } };
		public ApplicationViewModel Application { get; private set; }

		public ICommand IncrementCommand { get; }
		public ICommand ResetCommand { get; }

		private readonly ICounterViewController counterViewController;
		private readonly ICounterPresenter counterPresenter;
		private readonly IApplicationPresenter applicationPresenter;
		private readonly IModelObserver modelObserver;

		public CounterViewModel(ICounterViewController counterViewController, ICounterPresenter counterPresenter, IApplicationPresenter applicationPresenter, IModelObserver modelObserver)
		{
			this.counterViewController = counterViewController;
			this.counterPresenter = counterPresenter;
			this.applicationPresenter = applicationPresenter;
			this.modelObserver = modelObserver;

			modelObserver.CounterChanged.AddHandler(ModelObserver_CounterChanged);

			IncrementCommand = new Command(Increment, () => CanIncrement);
			ResetCommand = new Command(Reset);
		}

		protected override void Dispose(bool dispose)
		{
			modelObserver.CounterChanged.RemoveHandler(ModelObserver_CounterChanged);

			base.Dispose(dispose);
		}

		public async Task InitializeAsync(ApplicationViewModel applicationViewModel, NavigationGoal navigationGoal)
		{
			Application = applicationViewModel;

			Id = navigationGoal.Parameters.TryGetValue("Id", out object id) && id is string idAsString ? idAsString : null;

			if (Id == null)
			{
				Id = await counterViewController.CreateCounter(Application, this);
				await applicationPresenter.NavigateAsync(Application, new NavigationGoal
				{
					Location = AppLocations.Counter,
					Parameters = new Dictionary<string, object>
					{
						{ "Id", Id }
					}
				});
				return;
			}

			await counterPresenter.Initialize(Application, this);
		}

		private async Task Increment()
		{
			await counterViewController.IncrementCounter(Application, this);
		}

		private async Task Reset()
		{
			await counterViewController.ResetCounter(Application, this);
		}

		private async Task ModelObserver_CounterChanged(string id)
		{
			if (id != Id)
				return;

			Application.ApplyUserCulture(); // the model observer will raise the CounterChanged event for all users, who may all have different culture settings.
											// So we need to apply the users culture to the current context

			await counterPresenter.Update(Application, this);
		}
	}
}
