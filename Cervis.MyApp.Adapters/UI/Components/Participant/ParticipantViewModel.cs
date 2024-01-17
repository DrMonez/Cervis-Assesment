using System.Threading.Tasks;
using System.Windows.Input;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;

namespace Cervis.MyApp.Adapters.UI.Components.Participant
{
	public class ParticipantViewModel : BaseViewModel
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool IsConfirmed { get; set; }

		public ApplicationViewModel Application { get; private set; }
		public ICommand SaveParticipantCommand { get; }
		public ICommand EditParticipantCommand { get; }
		public ICommand DeleteParticipantCommand { get; }


		private readonly IParticipantPresenter participantPresenter;

		public ParticipantViewModel(IParticipantPresenter participantPresenter)
		{
			SaveParticipantCommand = new Command(SaveParticipantAsync);
			EditParticipantCommand = new Command(EditParticipantAsync);
			DeleteParticipantCommand = new Command(DeleteParticipantAsync);

			this.participantPresenter = participantPresenter;
		}

		public void Initialize(ApplicationViewModel applicationViewModel)
		{
			Application = applicationViewModel;
		}

		public Business.Model.Participant ToParticipant()
		{
			return new Business.Model.Participant
			{
				Id = this.Id,
				FirstName = this.FirstName,
				LastName = this.LastName,
				IsConfirmed = this.IsConfirmed,
			};
		}

		private async Task SaveParticipantAsync()
		{
			await participantPresenter.UpdateAsync(this);
		}

		private async Task EditParticipantAsync()
		{
			await participantPresenter.ShowAsync(Application, Id);
		}

		private async Task DeleteParticipantAsync()
		{
			await participantPresenter.DeleteAsync(this);
		}

		public static ParticipantViewModel CreateFromParticipant(ApplicationViewModel applicationViewModel, Business.Model.Participant participant)
		{
			var participantViewModel = applicationViewModel.CreateViewModel<ParticipantViewModel>();

			participantViewModel.Id = participant.Id;
			participantViewModel.FirstName = participant.FirstName;
			participantViewModel.LastName = participant.LastName;
			participantViewModel.IsConfirmed = participant.IsConfirmed;

			participantViewModel.Initialize(applicationViewModel);

			return participantViewModel;
		}
	}
}
