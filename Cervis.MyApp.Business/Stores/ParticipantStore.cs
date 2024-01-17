using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cervis.MyApp.Business.Model;

namespace Cervis.MyApp.Business.Stores
{
	interface IParticipantStore
	{
		Task<Participant> GetAsync(string id);
		Task<List<Participant>> GetAllAsync();
		Task StoreAsync(Participant participant);
		Task UpdateAsync(Participant participant);
		Task DeleteAsync(string id);
	}

	class ParticipantStore : IParticipantStore
	{
		private readonly Dictionary<string, Participant> store = new();

		public Task<Participant> GetAsync(string id)
		{
			store.TryGetValue(id, out Participant participant);

			return Task.FromResult(participant);
		}

		public Task<List<Participant>> GetAllAsync()
		{
			var participants = store.Select((x) => x.Value).ToList();

			return Task.FromResult(participants);
		}

		public Task StoreAsync(Participant participant)
		{
			store.Add(participant.Id, participant);

			return Task.CompletedTask;
		}

		public Task UpdateAsync(Participant participant)
		{
			store[participant.Id] = participant;

			return Task.CompletedTask;
		}

		public Task DeleteAsync(string id)
		{
			store.Remove(id);

			return Task.CompletedTask;
		}
	}
}
