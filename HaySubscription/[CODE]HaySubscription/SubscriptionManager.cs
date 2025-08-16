using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaySubscription
{
	public static class SubscriptionManager
	{
		const int HayPrice = 50;
		internal static void DoHaySubscription()
		{
			if (!ModEntry.SubscriptionOngoing) return;
			if (!Game1.player.IsMainPlayer) return;
			var animalCount = Game1.farmAnimalData.Count;
			if (Game1.getFarm().GetHayCapacity() < 1) ManageNoSilo();
			else if(Game1.getFarm().piecesOfHay.Value <= animalCount) TryDeliverHay();

		}
		static void ManageNoSilo()
		{
			Game1.player.mailbox.Add("MarnieHaySubscriptionNoSilo");
			ModEntry.SubscriptionOngoing = false;
		}
		static void TryDeliverHay()
		{
			var farm = Game1.getFarm();
			var HayNeeded = farm.GetHayCapacity() - farm.piecesOfHay.Value;
			var price = (int)Math.Floor(HayPrice * HayNeeded * 1.1f);
			if(Game1.player.Money >= price)
			{
				Game1.player.Money -= price;
				farm.piecesOfHay.Set(farm.GetHayCapacity());
				ModEntry.LastCharged = price;
				Game1.player.mailbox.Add("MarnieHaySubscriptionDelivered");
				Console.WriteLine($"[HaySubscription] Delivered {HayNeeded} hay for {price}g");
			}
			else
			{
				Game1.player.mailbox.Add("MarnieHaySubscriptionNoMoney");
				ModEntry.SubscriptionOngoing = false;
				return;
			}
		}
	}
}
