using System.Threading.Tasks;
using Cervis.MyApp.Business.Gateways;
using Cervis.MyApp.Business.Model;
using Cervis.MyApp.Business.Stores;

namespace Cervis.MyApp.Business.Operations
{
	public enum CreateCounterResultCode
	{
		OK,
	}

	public interface ICreateCounterOperation
	{
		Task<OperationResult<CreateCounterResultCode, Counter>> Execute();
	}

	class CreateCounterOperation : ICreateCounterOperation
	{
		private readonly ICounterStore counterStore;
		private readonly IMutableModelObserver mutableModelObserver;

		public CreateCounterOperation(ICounterStore counterStore, IMutableModelObserver mutableModelObserver)
		{
			this.counterStore = counterStore;
			this.mutableModelObserver = mutableModelObserver;
		}

		public async Task<OperationResult<CreateCounterResultCode, Counter>> Execute()
		{
			Counter counter = new() { Id = Counter.GetNewId() };

			await counterStore.Store(counter);

			await mutableModelObserver.RaiseCounterCreated(counter.Id);

			return new OperationResult<CreateCounterResultCode, Counter>(CreateCounterResultCode.OK, counter);
		}
	}
}
