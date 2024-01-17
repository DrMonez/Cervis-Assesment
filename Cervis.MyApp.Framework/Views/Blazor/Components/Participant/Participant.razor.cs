using Cervis.ClArc.Adapters.UI;
using Cervis.ClArc.Framework.Views.Blazor;
using Cervis.MyApp.Adapters.UI.Components.Participant;
using Microsoft.AspNetCore.Components;

namespace Cervis.MyApp.Framework.Views.Blazor.Components.Participant
{
	[DefaultViewForViewModel(typeof(ParticipantViewModel))]
	public partial class Participant : View<ParticipantViewModel>
	{
		[Inject]
		public ILocalizer<Participant> Localizer { get; set; }
	}
}
