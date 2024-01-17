using System;
using System.Threading.Tasks;
using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Adapters.UI.Application;

namespace Cervis.MyApp.Adapters.UI.Components.Clock
{
	public interface IClockPresenter
	{
		Task Show(ApplicationViewModel applicationViewModel);
		Task Update(ApplicationViewModel applicationViewModel, ClockViewModel clockViewModel);
	}

	class ClockPresenter : IClockPresenter
	{
		private readonly IApplicationPresenter applicationPresenter;
		private readonly ILocalizer<ClockPresenter> localizer;

		public ClockPresenter(IApplicationPresenter applicationPresenter, ILocalizer<ClockPresenter> localizer)
		{
			this.applicationPresenter = applicationPresenter;
			this.localizer = localizer;
		}

		public async Task Show(ApplicationViewModel applicationViewModel)
		{
			ClockViewModel clockViewModel = applicationViewModel.CreateViewModel<ClockViewModel>();
			DateTime now = DateTime.Now;
			clockViewModel.Time = TimeOnly.FromDateTime(now);
			clockViewModel.Date = DateOnly.FromDateTime(now);
			clockViewModel.Initialize(applicationViewModel);

			await applicationPresenter.ShowModalAsync(applicationViewModel, (builder, resultDelegate) =>
			{
				builder.WindowSize = ModalViewModel.Size.Medium;
				builder.Title = localizer["Title"];
				builder.UserCanClose = true;
				builder.Content = clockViewModel;

				// optional: invoke resultDelegate to automatically close the modal in certain situations
				// clockViewModel.SomeEventOccurred += resultDelegate(ModalViewModel.Result.Cancel);
			});
		}

		public Task Update(ApplicationViewModel applicationViewModel, ClockViewModel clockViewModel)
		{
			using (clockViewModel.ChangeObservationContext)
			{
				DateTime now = DateTime.Now;
				clockViewModel.Time = TimeOnly.FromDateTime(now);
				clockViewModel.Date = DateOnly.FromDateTime(now);
			}

			return Task.CompletedTask;
		}
	}
}
