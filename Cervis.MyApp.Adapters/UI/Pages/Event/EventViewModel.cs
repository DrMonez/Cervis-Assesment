using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.MyApp.Adapters.UI.Components.Participant;
using Cervis.MyApp.Business.Gateways;

namespace Cervis.MyApp.Adapters.UI.Pages.Event
{
	[ViewModelWithAppLocation(AppLocations.Event)]
	public class EventViewModel : BaseViewModel, IViewModelWithAppLocation
	{
		public List<ParticipantViewModel> Participants { get; internal set; } = new();
		public ApplicationViewModel Application { get; private set; }
		public ICommand AddParticipantCommand { get; }

		private readonly IEventViewController eventViewController;
		private readonly IModelObserver modelObserver;
		private readonly IEventPresenter eventPresenter;


		public EventViewModel(
			IEventViewController eventViewController, 
			IModelObserver modelObserver,
			IEventPresenter eventPresenter)
		{
			this.eventViewController = eventViewController;
			this.eventPresenter = eventPresenter;

			AddParticipantCommand = new Command(AddParticipantAsync);

			this.modelObserver = modelObserver;
			this.modelObserver.EventChanged.AddHandler(ModelObserver_EventChangedAsync);
		}

		public async Task InitializeAsync(ApplicationViewModel applicationViewModel, NavigationGoal navigationGoal)
		{
			Application = applicationViewModel;
			await eventPresenter.InitializeAsync(Application, this);
		}

		protected override void Dispose(bool dispose)
		{
			modelObserver.EventChanged.RemoveHandler(ModelObserver_EventChangedAsync);

			base.Dispose(dispose);
		}

		private async Task AddParticipantAsync()
		{
			await eventViewController.AddParticipantAsync(Application);
		}

		private async Task ModelObserver_EventChangedAsync()
		{
			await eventPresenter.UpdateAsync(Application, this);
		}
	}
}
