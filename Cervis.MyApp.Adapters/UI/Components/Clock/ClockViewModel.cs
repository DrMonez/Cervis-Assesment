using System;
using System.Threading.Tasks;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;

namespace Cervis.MyApp.Adapters.UI.Components.Clock
{
	public class ClockViewModel : BaseViewModel
	{
		public DateOnly Date { get; internal set; }
		public TimeOnly Time { get; internal set; }

		public ApplicationViewModel Application { get; private set; }

		private readonly IClockPresenter clockPresenter;

		public ClockViewModel(IClockPresenter clockPresenter)
		{
			this.clockPresenter = clockPresenter;
		}

		public void Initialize(ApplicationViewModel applicationViewModel)
		{
			Application = applicationViewModel;

			Task.Run(Refresher);
		}

		private async Task Refresher()
		{
			while (!LifetimeToken.IsCancellationRequested)
			{
				try
				{
					await Task.Delay(100, LifetimeToken);
				}
				catch (OperationCanceledException)
				{
					return;
				}

				await clockPresenter.Update(Application, this);
			}
		}
	}
}
