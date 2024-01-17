using System.Threading.Tasks;
using Cervis.MyApp.Business.Model;
using Cervis.MyApp.Business.Stores;

namespace Cervis.MyApp.Business.Queries
{
	public interface ISingleParticipantQuery
	{
		Task<Participant> ExecuteAsync(string id);
	}

	class SingleParticipantQuery : ISingleParticipantQuery
	{
		private readonly IParticipantStore participantStore;

		public SingleParticipantQuery(IParticipantStore participantStore)
		{
			this.participantStore = participantStore;
		}

		public async Task<Participant> ExecuteAsync(string id)
		{
			return await participantStore.GetAsync(id);
		}
	}
}
