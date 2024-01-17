using System.Threading.Tasks;
using Cervis.MyApp.Business.Gateways;
using Cervis.MyApp.Business.Model;
using Cervis.MyApp.Business.Stores;

namespace Cervis.MyApp.Business.Operations
{
	public enum CreateParticipantResultCode
	{
		OK,
	}

	public interface ICreateParticipantOperation
	{
		Task<OperationResult<CreateParticipantResultCode, Participant>> ExecuteAsync();
	}

	class CreateParticipantOperation : ICreateParticipantOperation
	{
		private readonly IParticipantStore participantStore;
		private readonly IMutableModelObserver mutableModelObserver;

		public CreateParticipantOperation(IParticipantStore participantStore, IMutableModelObserver mutableModelObserver)
		{
			this.participantStore = participantStore;
			this.mutableModelObserver = mutableModelObserver;
		}

		public async Task<OperationResult<CreateParticipantResultCode, Participant>> ExecuteAsync()
		{
			Participant participant = new() { Id = Participant.GetNewId() };

			await participantStore.StoreAsync(participant);

			await mutableModelObserver.RaiseParticipantCreated(participant.Id);

			return new OperationResult<CreateParticipantResultCode, Participant>(CreateParticipantResultCode.OK, participant);
		}
	}
}
