using System.Threading.Tasks;
using Cervis.MyApp.Business.Gateways;
using Cervis.MyApp.Business.Model;
using Cervis.MyApp.Business.Stores;

namespace Cervis.MyApp.Business.Operations
{
	public enum IncrementCounterResultCode
	{
		OK,
		NotFoundError,
		LimitExceededError,
	}

	public interface IIncrementCounterOperation
	{
		Task<OperationResult<IncrementCounterResultCode>> Execute(string counterId);
	}

	class IncrementCounterOperation : IIncrementCounterOperation
	{
		private readonly ICounterStore counterStore;
		private readonly IMutableModelObserver mutableModelObserver;

		public IncrementCounterOperation(ICounterStore counterStore, IMutableModelObserver mutableModelObserver)
		{
			this.counterStore = counterStore;
			this.mutableModelObserver = mutableModelObserver;
		}

		public async Task<OperationResult<IncrementCounterResultCode>> Execute(string counterId)
		{
			Counter counter = await counterStore.Get(counterId);
			if (counter == null)
				return new OperationResult<IncrementCounterResultCode>(IncrementCounterResultCode.NotFoundError);

			if (!counter.CanIncrement)
				return new OperationResult<IncrementCounterResultCode>(IncrementCounterResultCode.LimitExceededError);

			++counter.Value;

			await counterStore.Update(counter);

			await mutableModelObserver.RaiseCounterChanged(counter.Id);

			return new OperationResult<IncrementCounterResultCode>(IncrementCounterResultCode.OK);
		}
	}
}
