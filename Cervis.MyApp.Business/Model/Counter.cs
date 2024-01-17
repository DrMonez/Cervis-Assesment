using System;
using System.Linq;
using System.Text;

namespace Cervis.MyApp.Business.Model
{
	public class Counter
	{
		public const int MaxValue = 10;

		public string Id { get; set; }
		public int Value { get; set; }
		public bool CanIncrement => Value < MaxValue;

		internal static string GetNewId()
		{
			Random random = new();
			return Encoding.ASCII.GetString(Enumerable.Range(0, 4).Select(i => (byte)(random.Next(26) + 'A')).ToArray());   // 4 random capital letters
		}
	}
}
