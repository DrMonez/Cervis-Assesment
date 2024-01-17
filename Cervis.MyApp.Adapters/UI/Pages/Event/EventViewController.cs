using System.Threading.Tasks;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.MyApp.Adapters.UI.Components.Participant;

namespace Cervis.MyApp.Adapters.UI.Pages.Event
{
	public interface IEventViewController
	{
		Task AddParticipantAsync(ApplicationViewModel applicationViewModel);
	}

	public class EventViewController: IEventViewController
	{
		private readonly IParticipantPresenter participantPresenter;

		public EventViewController(IParticipantPresenter participantPresenter)
		{
			this.participantPresenter = participantPresenter;
		}

		public async Task AddParticipantAsync(ApplicationViewModel applicationViewModel)
		{
			await participantPresenter.ShowAsync(applicationViewModel);
		}
	}
}
