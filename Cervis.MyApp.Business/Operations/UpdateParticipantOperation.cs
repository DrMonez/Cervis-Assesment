using System.Threading.Tasks;
using Cervis.MyApp.Business.Gateways;
using Cervis.MyApp.Business.Model;
using Cervis.MyApp.Business.Stores;

namespace Cervis.MyApp.Business.Operations
{
	public enum UpdateParticipantResultCode
	{
		OK,
		NotFoundError,
	}

	public interface IUpdateParticipantOperation
	{
		Task<OperationResult<UpdateParticipantResultCode, Participant>> ExecuteAsync(Participant participant);
	}

	class UpdateParticipantOperation : IUpdateParticipantOperation
	{
		private readonly IParticipantStore participantStore;
		private readonly IMutableModelObserver mutableModelObserver;

		public UpdateParticipantOperation(IParticipantStore participantStore, IMutableModelObserver mutableModelObserver)
		{
			this.participantStore = participantStore;
			this.mutableModelObserver = mutableModelObserver;
		}

		public async Task<OperationResult<UpdateParticipantResultCode, Participant>> ExecuteAsync(Participant participant)
		{
			Participant oldParticipant = await participantStore.GetAsync(participant.Id);
			if (oldParticipant == null)
			{
				return new OperationResult<UpdateParticipantResultCode, Participant>(UpdateParticipantResultCode.NotFoundError, null);
			}

			await participantStore.UpdateAsync(participant);

			await mutableModelObserver.RaiseParticipantChanged(participant.Id);
			await mutableModelObserver.RaiseEventChanged();

			return new OperationResult<UpdateParticipantResultCode, Participant>(UpdateParticipantResultCode.OK, participant);
		}
	}
}
