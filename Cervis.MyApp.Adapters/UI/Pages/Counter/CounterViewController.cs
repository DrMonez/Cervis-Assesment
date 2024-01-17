using System.Threading.Tasks;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;
using Cervis.MyApp.Business.Operations;

namespace Cervis.MyApp.Adapters.UI.Pages.Counter
{
	public interface ICounterViewController
	{
		Task<string> CreateCounter(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel);
		Task IncrementCounter(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel);
		Task ResetCounter(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel);
	}

	class CounterViewController : ICounterViewController
	{
		private readonly ICreateCounterOperation createCounterOperation;
		private readonly IIncrementCounterOperation incrementCounterOperation;
		private readonly IResetCounterOperation resetCounterOperation;
		private readonly ILocalizer<CounterViewController> localizer;
		private readonly IApplicationPresenter applicationPresenter;

		public CounterViewController(ICreateCounterOperation createCounterOperation, IIncrementCounterOperation incrementCounterOperation, IResetCounterOperation resetCounterOperation, ILocalizer<CounterViewController> localizer, IApplicationPresenter applicationPresenter)
		{
			this.createCounterOperation = createCounterOperation;
			this.incrementCounterOperation = incrementCounterOperation;
			this.resetCounterOperation = resetCounterOperation;
			this.localizer = localizer;
			this.applicationPresenter = applicationPresenter;
		}

		public async Task<string> CreateCounter(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel)
		{
			OperationResult<CreateCounterResultCode, Business.Model.Counter> result = await createCounterOperation.Execute();

			return result.Data.Id;
		}

		public async Task IncrementCounter(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel)
		{
			OperationResult<IncrementCounterResultCode> result = await incrementCounterOperation.Execute(counterViewModel.Id);

			if (result.Code == IncrementCounterResultCode.OK)
				return;

			await applicationPresenter.ShowErrorAsync(applicationViewModel, localizer[result.Code.ToString()]);
		}

		public async Task ResetCounter(ApplicationViewModel applicationViewModel, CounterViewModel counterViewModel)
		{
			OperationResult<ResetCounterResultCode> result = await resetCounterOperation.Execute(counterViewModel.Id);

			if (result.Code == ResetCounterResultCode.OK)
			{
				await applicationPresenter.AddQuickNotification(applicationViewModel, localizer["ResetNotificationText"], QuickNotificationSeverity.Info);
				return;
			}

			await applicationPresenter.ShowErrorAsync(applicationViewModel, localizer[result.Code.ToString()]);
		}
	}
}
