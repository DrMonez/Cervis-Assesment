using System.Collections.Generic;
using System.Threading.Tasks;
using Cervis.MyApp.Business.Model;
using Cervis.MyApp.Business.Stores;

namespace Cervis.MyApp.Business.Queries
{
	public interface IMultiParticipantQuery
	{
		Task<List<Participant>> ExecuteAsync();
	}

	class MultiParticipantQuery : IMultiParticipantQuery
	{
		private readonly IParticipantStore participantStore;

		public MultiParticipantQuery(IParticipantStore participantStore)
		{
			this.participantStore = participantStore;
		}

		public async Task<List<Participant>> ExecuteAsync()
		{	
			return await participantStore.GetAllAsync();
		}
	}
}
