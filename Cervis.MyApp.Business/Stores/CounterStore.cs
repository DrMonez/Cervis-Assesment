using System.Collections.Generic;
using System.Threading.Tasks;
using Cervis.MyApp.Business.Model;

namespace Cervis.MyApp.Business.Stores
{
	interface ICounterStore
	{
		Task<Counter> Get(string id);
		Task Store(Counter counter);
		Task Update(Counter counter);
	}

	class CounterStore : ICounterStore
	{
		private readonly Dictionary<string, Counter> store = new();

		public Task<Counter> Get(string id)
		{
			store.TryGetValue(id, out Counter counter);

			return Task.FromResult(counter);
		}

		public Task Store(Counter counter)
		{
			store.Add(counter.Id, counter);

			return Task.CompletedTask;
		}

		public Task Update(Counter counter)
		{
			// no-op for in-memory store

			return Task.CompletedTask;
		}
	}
}
