using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaySubscription
{
	public class ModData
	{
		public bool HasSubscription = false;
		public int LastCharged = 0;
		public static ModData Create()
		{
			return new ModData() { HasSubscription = ModEntry.SubscriptionOngoing, LastCharged = ModEntry.LastCharged };
		}
		public void Load()
		{
			ModEntry.SubscriptionOngoing = HasSubscription;
			ModEntry.LastCharged = LastCharged;
		}
	}
}
