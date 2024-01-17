using System.Threading.Tasks;
using Cervis.MyApp.Business.Gateways;
using Cervis.MyApp.Business.Model;
using Cervis.MyApp.Business.Stores;

namespace Cervis.MyApp.Business.Operations
{
	public enum ResetCounterResultCode
	{
		OK,
		NotFoundError,
	}

	public interface IResetCounterOperation
	{
		Task<OperationResult<ResetCounterResultCode>> Execute(string counterId);
	}

	class ResetCounterOperation : IResetCounterOperation
	{
		private readonly ICounterStore counterStore;
		private readonly IMutableModelObserver mutableModelObserver;

		public ResetCounterOperation(ICounterStore counterStore, IMutableModelObserver mutableModelObserver)
		{
			this.counterStore = counterStore;
			this.mutableModelObserver = mutableModelObserver;
		}

		public async Task<OperationResult<ResetCounterResultCode>> Execute(string counterId)
		{
			Counter counter = await counterStore.Get(counterId);
			if (counter == null)
				return new OperationResult<ResetCounterResultCode>(ResetCounterResultCode.NotFoundError);

			counter.Value = 0;

			await counterStore.Update(counter);

			await mutableModelObserver.RaiseCounterChanged(counter.Id);

			return new OperationResult<ResetCounterResultCode>(ResetCounterResultCode.OK);
		}
	}
}
