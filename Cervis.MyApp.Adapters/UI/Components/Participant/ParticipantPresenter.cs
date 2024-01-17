using System.Threading.Tasks;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.ClArc.Adapters.UI;
using Cervis.MyApp.Business.Queries;
using Cervis.MyApp.Business.Operations;

namespace Cervis.MyApp.Adapters.UI.Components.Participant
{
	public interface IParticipantPresenter
	{
		Task ShowAsync(ApplicationViewModel applicationViewModel, string id = null);
		Task UpdateAsync(ParticipantViewModel participantViewModel);
		Task DeleteAsync(ParticipantViewModel participantViewModel);
	}

	class ParticipantPresenter : IParticipantPresenter
	{
		private readonly IApplicationPresenter applicationPresenter;
		private readonly ILocalizer<ParticipantPresenter> localizer;
		private readonly ISingleParticipantQuery singleParticipantQuery;
		private readonly IUpdateParticipantOperation updateParticipantOperation;
		private readonly ICreateParticipantOperation createParticipantOperation;
		private readonly IDeleteParticipantOperation deleteParticipantOperation;

		public ParticipantPresenter(
			IApplicationPresenter applicationPresenter, 
			ILocalizer<ParticipantPresenter> localizer, 
			ISingleParticipantQuery singleParticipantQuery,
			IUpdateParticipantOperation updateParticipantOperation,
			ICreateParticipantOperation createParticipantOperation,
			IDeleteParticipantOperation deleteParticipantOperation)
		{
			this.applicationPresenter = applicationPresenter;
			this.localizer = localizer;
			this.singleParticipantQuery = singleParticipantQuery;
			this.updateParticipantOperation = updateParticipantOperation;
			this.createParticipantOperation = createParticipantOperation;
			this.deleteParticipantOperation = deleteParticipantOperation;
		}

		public async Task ShowAsync(ApplicationViewModel applicationViewModel, string id = null)
		{
			ParticipantViewModel participantViewModel = string.IsNullOrEmpty(id)
				? applicationViewModel.CreateViewModel<ParticipantViewModel>()
				: ParticipantViewModel.CreateFromParticipant(applicationViewModel,
					await singleParticipantQuery.ExecuteAsync(id));
			participantViewModel.Initialize(applicationViewModel);


			await applicationPresenter.ShowModalAsync(applicationViewModel, (builder, resultDelegate) =>
			{
				builder.WindowSize = ModalViewModel.Size.Medium;
				builder.Title = localizer["Title"];
				builder.UserCanClose = true;
				builder.Content = participantViewModel;
				builder.AddButton(localizer["Close"], ModalViewModel.ButtonType.Confirm, ModalViewModel.Result.OK);
			});
		}

		public async Task UpdateAsync(ParticipantViewModel participantViewModel)
		{
			if (string.IsNullOrEmpty(participantViewModel.Id))
			{
				var participant = (await createParticipantOperation.ExecuteAsync()).Data;
				participantViewModel.Id = participant.Id;
			}

			using (participantViewModel.ChangeObservationContext)
			{
				await updateParticipantOperation.ExecuteAsync(participantViewModel.ToParticipant());
			}
		}

		public async Task DeleteAsync(ParticipantViewModel participantViewModel)
		{
			await deleteParticipantOperation.ExecuteAsync(participantViewModel.ToParticipant());
		}
	}
}
