using StardewValley;
using StardewValley.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaySubscription
{
	public static class SubscriptionManager
	{
		public static int HayPrice => GetAnimalShopStockForItem(StardewValley.Object.hayID)[0].Price;
		public static float HayMarkup => 1.1f;
		internal static List<Feed> FeedsToRefill = new List<Feed>();
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
			var price = (int)Math.Floor(HayPrice * HayNeeded * HayMarkup);
			if(Game1.player.Money >= price)
			{
				Game1.player.Money -= price;
				ModEntry.LastCharged = price;
				Game1.player.mailbox.Add("MarnieHaySubscriptionDelivered");
				Console.WriteLine($"[HaySubscription] Delivered {HayNeeded} hay for {price}g");
			}
			else
			{
				Console.WriteLine($"[HaySubsscription] Cancelled subscription due to lack of money. Player had {Game1.player.Money}g. Needed {price}g (Feed Price {HayPrice}g * Markup {HayMarkup}f * Hay needed {HayNeeded} (Hay Capacity {farm.GetHayCapacity()} - current hay {farm.piecesOfHay.Value}))");
				Game1.player.mailbox.Add("MarnieHaySubscriptionNoMoney");
				ModEntry.SubscriptionOngoing = false;
				return;
			}
		}
		public static ItemStockInformation[] GetAnimalShopStockForItem(string qItemid) => (from KeyValuePair<ISalable, ItemStockInformation> x in ShopBuilder.GetShopStock("AnimalShop") where x.Key.QualifiedItemId == qItemid select x.Value).ToArray();
		public static bool AnimalShopHasItem(string qItemid) => GetAnimalShopStockForItem(qItemid).Length > 0;
		public static void LoadFeeds()
		{
			FeedsToRefill.Clear();
			FeedsToRefill.Add(new Feed(StardewValley.Object.hayID, () => Game1.getFarm().piecesOfHay.Set(Game1.getFarm().GetHayCapacity()), () => Game1.getFarm().GetHayCapacity(), () => Game1.getFarm().piecesOfHay.Value, "Hay"));
			if (ModEntry.HasExtraAnimalConfigFramework)
			{
				var eACFFeeds = ModEntry.EACFApi.GetModdedFeedInfo();
				foreach(var feed in eACFFeeds)
				{
					var feedForList = new Feed(feed.Key, ()=>feed.Value.count = feed.Value.capacity, ()=>feed.Value.capacity, ()=>feed.Value.count, "EACF Custom Feed");
					if(feedForList.IsValid) FeedsToRefill.Add(feedForList);
				}
			}
		}
	}
	internal struct Feed
	{
		public string ItemID;
		public string Name;
		public int Price => SubscriptionManager.GetAnimalShopStockForItem(ItemID)[0].Price;
		public bool IsValid => SubscriptionManager.AnimalShopHasItem(ItemID);
		public delegate void RefillAction();
		public RefillAction Refill;
		private Func<int> GetCapacity;
		private Func<int> GetAmount;
		public int Capacity => GetCapacity();
		public int Amount => GetAmount();
		public Feed(string itemid, RefillAction refill, Func<int> capacity, Func<int> amount, string name)
		{
			ItemID = itemid;
			Refill = refill;
			GetCapacity = capacity;
			GetAmount = amount;
			Name = name;
		}
	}
}
