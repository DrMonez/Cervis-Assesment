using Cervis.MyApp.Business.Gateways;
using Cervis.MyApp.Business.Operations;
using Cervis.MyApp.Business.Queries;
using Cervis.MyApp.Business.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCervisMyAppBusiness(this IServiceCollection services)
		{
			services.AddSingleton<ICounterStore, CounterStore>();
			services.AddSingleton<IParticipantStore, ParticipantStore>();

			services.AddSingleton<IMutableModelObserver, ModelObserver>();
			services.AddSingleton<IModelObserver>(p => p.GetRequiredService<IMutableModelObserver>());

			services.AddSingleton<ICreateCounterOperation, CreateCounterOperation>();
			services.AddSingleton<IIncrementCounterOperation, IncrementCounterOperation>();
			services.AddSingleton<IResetCounterOperation, ResetCounterOperation>();
			
			services.AddSingleton<ICreateParticipantOperation, CreateParticipantOperation>();
			services.AddSingleton<IUpdateParticipantOperation, UpdateParticipantOperation>();
			services.AddSingleton<IDeleteParticipantOperation, DeleteParticipantOperation>();

			services.AddSingleton<ISingleCounterQuery, SingleCounterQuery>();
			services.AddSingleton<ISingleParticipantQuery, SingleParticipantQuery>();
			services.AddSingleton<IMultiParticipantQuery, MultiParticipantQuery>();

			return services;
		}
	}
}
