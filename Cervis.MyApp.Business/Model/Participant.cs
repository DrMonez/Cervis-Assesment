using System;

namespace Cervis.MyApp.Business.Model
{
	public class Participant
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool IsConfirmed { get; set; }

		internal static string GetNewId()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
