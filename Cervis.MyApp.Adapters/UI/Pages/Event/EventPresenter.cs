using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.MyApp.Adapters.UI.Components.Participant;
using Cervis.MyApp.Business.Queries;

namespace Cervis.MyApp.Adapters.UI.Pages.Event
{
	public interface IEventPresenter
	{
		Task InitializeAsync(ApplicationViewModel applicationViewModel, EventViewModel eventViewModel);
		Task UpdateAsync(ApplicationViewModel applicationViewModel, EventViewModel eventViewModel);
	}

	class EventPresenter : IEventPresenter
	{
		private readonly IMultiParticipantQuery multiParticipantQuery;

		public EventPresenter(IMultiParticipantQuery multiParticipantQuery)
		{
			this.multiParticipantQuery = multiParticipantQuery;
		}

		public async Task InitializeAsync(ApplicationViewModel applicationViewModel, EventViewModel eventViewModel)
		{
			using (eventViewModel.ChangeObservationContext)
			{
				eventViewModel.Participants =
					await MapParticipantViewModelsAsync(applicationViewModel);
			}
		}

		public async Task UpdateAsync(ApplicationViewModel applicationViewModel, EventViewModel eventViewModel)
		{
			using (eventViewModel.ChangeObservationContext)
			{
				eventViewModel.Participants =
					await MapParticipantViewModelsAsync(applicationViewModel);
			}
		}

		private async Task<List<ParticipantViewModel>> MapParticipantViewModelsAsync(ApplicationViewModel applicationViewModel)
		{
			var participants = await multiParticipantQuery.ExecuteAsync();
			return participants.Select((participant) => ParticipantViewModel.CreateFromParticipant(applicationViewModel, participant)).ToList();
		}
	}
}
