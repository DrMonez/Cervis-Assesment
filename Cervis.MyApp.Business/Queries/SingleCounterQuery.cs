using System.Threading.Tasks;
using Cervis.MyApp.Business.Model;
using Cervis.MyApp.Business.Stores;

namespace Cervis.MyApp.Business.Queries
{
	public interface ISingleCounterQuery
	{
		Task<Counter> Execute(string id);
	}

	class SingleCounterQuery : ISingleCounterQuery
	{
		private readonly ICounterStore counterStore;

		public SingleCounterQuery(ICounterStore counterStore)
		{
			this.counterStore = counterStore;
		}

		public async Task<Counter> Execute(string id)
		{
			return await counterStore.Get(id);
		}
	}
}
