using System.Threading.Tasks;
using Cervis.MyApp.Business.Gateways;
using Cervis.MyApp.Business.Model;
using Cervis.MyApp.Business.Stores;

namespace Cervis.MyApp.Business.Operations
{
	public enum DeleteParticipantResultCode
	{
		OK,
		NotFoundError,
	}

	public interface IDeleteParticipantOperation
	{
		Task<OperationResult<DeleteParticipantResultCode, Participant>> ExecuteAsync(Participant participant);
	}

	class DeleteParticipantOperation : IDeleteParticipantOperation
	{
		private readonly IParticipantStore participantStore;
		private readonly IMutableModelObserver mutableModelObserver;

		public DeleteParticipantOperation(IParticipantStore participantStore, IMutableModelObserver mutableModelObserver)
		{
			this.participantStore = participantStore;
			this.mutableModelObserver = mutableModelObserver;
		}

		public async Task<OperationResult<DeleteParticipantResultCode, Participant>> ExecuteAsync(Participant participant)
		{
			Participant oldParticipant = await participantStore.GetAsync(participant.Id);
			if (oldParticipant == null)
			{
				return new OperationResult<DeleteParticipantResultCode, Participant>(DeleteParticipantResultCode.NotFoundError, null);
			}

			await participantStore.DeleteAsync(participant.Id);

			await mutableModelObserver.RaiseParticipantChanged(participant.Id);
			await mutableModelObserver.RaiseEventChanged();

			return new OperationResult<DeleteParticipantResultCode, Participant>(DeleteParticipantResultCode.OK, participant);
		}
	}
}
