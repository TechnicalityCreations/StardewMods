using StardewValley;
using StardewValley.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace HaySubscription
{
	public static class SubscriptionManager
	{
		public static int HayPrice => GetAnimalShopStockForItem(StardewValley.Object.hayID)[0].Price;
		public static float HayMarkup => ModEntry.Config.HayMarkup;
		public static bool CanBeTriggeredByCustomFeeds => true;
		internal static List<Feed> FeedsToRefill = new List<Feed>();
		internal static void DoHaySubscription()
		{
			if (!ModEntry.SubscriptionOngoing) return;
			if (!Game1.player.IsMainPlayer) return;
			var animalCount = Game1.farmAnimalData.Count;
			if (Game1.getFarm().GetHayCapacity() < 1) ManageNoSilo();
			else if(Game1.getFarm().piecesOfHay.Value <= animalCount) TryDeliverHay();
			else
			{
				var flag = false;
				foreach(Feed feed in FeedsToRefill) if(feed.Amount <= animalCount) flag = true;
				if (flag && CanBeTriggeredByCustomFeeds) TryDeliverHay();
			}
		}
		static void ManageNoSilo()
		{
			Game1.player.mailbox.Add("MarnieHaySubscriptionNoSilo");
			ModEntry.SubscriptionOngoing = false;
		}
		static void TryDeliverHay()
		{
			var farm = Game1.getFarm();
			var price = 0;
			var feedString = "Feed Summary: ";
			foreach (Feed f in FeedsToRefill)
			{
				Console.WriteLine("[HaySubscription] Trying to get data for feed " + f.Name);

				//Debg Scripts
				var ASShopStock = ShopBuilder.GetShopStock("AnimalShop");
				if (ASShopStock == null) throw new Exception("[HaySubscription] ShopBuilder.GetShopStock(\"AnimalShop\") returned null");
				var shopHasItem = AnimalShopHasItem(f.ItemQID);
				if (!shopHasItem) throw new Exception($"[HaySubscription] AnimalShop Stock does not contain QID {f.ItemQID} for feed {f.Name}");
				var shopItem = GetAnimalShopStockForItem(f.ItemQID)[0];
				if (shopItem == null) throw new Exception($"[HaySubscription] GetAnimalShopStockForItem(f.ItemQID)[0] returned null");

				var needed = f.Capacity - f.Amount;
				int individualPrice = (int)Math.Floor(needed * f.Price * HayMarkup);
				price += individualPrice;
				feedString += $"\n {f.Name} TotalPrice {individualPrice}(PriceForOne {f.Price} * Needed {needed}(Capacity {f.Capacity} - Amount {f.Amount}) * Markup {HayMarkup})";
				Console.WriteLine("[HaySubscription] Successfully got feed data for feed " + f.Name + $" capac. {f.Capacity} amount {f.Amount} price {f.Price}");
			}
			if(Game1.player.Money >= price)
			{
				Game1.player.Money -= price;
				ModEntry.LastCharged = price;
				foreach (Feed f in FeedsToRefill) f.Refill();
				Game1.player.mailbox.Add("MarnieHaySubscriptionDelivered");
				Console.WriteLine($"[HaySubscription] Delivered for {price}g. " + feedString);
			}
			else
			{
				Console.WriteLine($"[HaySubsscription] Cancelled subscription due to lack of money. Player had {Game1.player.Money}g. Needed {price}g. "+ feedString );
				Game1.player.mailbox.Add("MarnieHaySubscriptionNoMoney");
				ModEntry.SubscriptionOngoing = false;
				return;
			}
		}
		public static List<ItemStockInformation> GetAnimalShopStockForItem(string qItemid) => (from KeyValuePair<ISalable, ItemStockInformation> x in ShopBuilder.GetShopStock("AnimalShop") where x.Key.QualifiedItemId == qItemid select x.Value).ToList();
		public static bool AnimalShopHasItem(string qItemid) => GetAnimalShopStockForItem(qItemid).Count > 0;
		public static void LoadFeeds()
		{
			FeedsToRefill.Clear();
			FeedsToRefill.Add(new Feed(StardewValley.Object.hayQID, () => Game1.getFarm().piecesOfHay.Set(Game1.getFarm().GetHayCapacity()), () => Game1.getFarm().GetHayCapacity(), () => Game1.getFarm().piecesOfHay.Value, "Hay"));
			if (ModEntry.HasExtraAnimalConfigFramework)
			{
				var eACFFeeds = ModEntry.EACFApi.GetModdedFeedInfo();
				foreach(var feed in eACFFeeds)
				{
					var feedForList = new Feed(feed.Key, ()=>feed.Value.count = feed.Value.capacity, ()=>feed.Value.capacity, ()=>feed.Value.count, "EACF Custom Feed");
					if(feedForList.IsValid) FeedsToRefill.Add(feedForList);
				}
			}
			Console.WriteLine($"[HaySubscription] Loaded Feeds: {string.Join(", ", from Feed f in FeedsToRefill select f.Name)}");
		}
	}
	internal struct Feed
	{
		public string ItemQID;
		public string Name;
		public int Price => SubscriptionManager.GetAnimalShopStockForItem(ItemQID)[0].Price;
		public bool IsValid => SubscriptionManager.AnimalShopHasItem(ItemQID);
		public delegate void RefillAction();
		public RefillAction Refill;
		private Func<int> GetCapacity;
		private Func<int> GetAmount;
		public int Capacity => GetCapacity();
		public int Amount => GetAmount();
		public Feed(string itemid, RefillAction refill, Func<int> capacity, Func<int> amount, string name)
		{
			ItemQID = itemid;
			Refill = refill;
			GetCapacity = capacity;
			GetAmount = amount;
			Name = name;
		}
	}
}
