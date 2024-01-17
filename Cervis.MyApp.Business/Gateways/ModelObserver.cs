using System.Threading.Tasks;
using Cervis.MyApp.Business.Utilities;

namespace Cervis.MyApp.Business.Gateways
{
	public interface IModelObserver
	{
		AsyncHandler<string> CounterCreated { get; }
		AsyncHandler<string> CounterChanged { get; }
		AsyncHandler EventChanged { get; }
		AsyncHandler<string> ParticipantCreated { get; }
		AsyncHandler<string> ParticipantChanged { get; }
	}

	interface IMutableModelObserver : IModelObserver
	{
		Task RaiseCounterCreated(string id);
		Task RaiseCounterChanged(string id);
		Task RaiseEventChanged();
		Task RaiseParticipantCreated(string id);
		Task RaiseParticipantChanged(string id);
	}

	class ModelObserver : IModelObserver, IMutableModelObserver
	{
		private readonly InvokableAsyncHandler<string> counterCreated = new();
		private readonly InvokableAsyncHandler<string> counterChanged = new();
		private readonly InvokableAsyncHandler eventChanged = new();
		private readonly InvokableAsyncHandler<string> participantCreated = new();
		private readonly InvokableAsyncHandler<string> participantChanged = new();

		public AsyncHandler<string> CounterCreated => counterCreated;
		public AsyncHandler<string> CounterChanged => counterChanged;
		public AsyncHandler EventChanged => eventChanged;
		public AsyncHandler<string> ParticipantCreated => participantCreated;
		public AsyncHandler<string> ParticipantChanged => participantChanged;

		public async Task RaiseCounterChanged(string id)
		{
			await counterChanged.InvokeAsync(id);
		}

		public async Task RaiseCounterCreated(string id)
		{
			await counterCreated.InvokeAsync(id);
		}

		public async Task RaiseEventChanged()
		{
			await eventChanged.InvokeAsync();
		}

		public async Task RaiseParticipantChanged(string id)
		{
			await participantChanged.InvokeAsync(id);
		}

		public async Task RaiseParticipantCreated(string id)
		{
			await participantCreated.InvokeAsync(id);
		}
	}
}
