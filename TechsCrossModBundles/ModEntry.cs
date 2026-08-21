using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using System.Text;
using StardewValley.Extensions;
using StardewValley.GameData.Bundles;
using Microsoft.Xna.Framework;

namespace TechsCrossModBundles
{
    public sealed partial class ModEntry : Mod
    {
		static Dictionary<string, string> bundles;
		internal new static IModHelper Helper;
		const string SVE = "FlashShifter.StardewValleyExpandedCP",
			RSV = "Rafseazz.RSVCP",
			Vapius = "Lumisteria.MtVapius",
			Cornucopia = "Cornucopia",
			ES = "EastScarp",
			ASF = "ASF",
			Sunberry = "skellady.SBVCP";
		public override void Entry(IModHelper helper)
		{
			Helper = helper;
			var harmony = new Harmony(ModManifest.UniqueID);
			harmony.Patch(AccessTools.Method(typeof(DataLoader), nameof(DataLoader.Bundles)), prefix: new HarmonyMethod(AccessTools.Method(typeof(ModEntry), nameof(LoadBundles))));
			helper.Events.GameLoop.SaveCreated += GenerateBundles;
			helper.Events.GameLoop.Saving += SaveBundles;
			helper.Events.GameLoop.SaveLoaded += LoadBundlesVar;
		}

		private static void LoadBundlesVar(object? sender = null, StardewModdingAPI.Events.SaveLoadedEventArgs e = null)
		{
			bundles = Helper.Data.ReadSaveData<Dictionary<string, string>>("TCMB");
			if (bundles == null) GenerateBundles(null);
			else SetGame1Bundles();

		}
		static void SetGame1Bundles()
		{
			Game1.netWorldState.Value.SetBundleData(DataLoader.Bundles(Game1.content));
		}
		private void SaveBundles(object? sender, StardewModdingAPI.Events.SavingEventArgs e)
		{
			Helper.Data.WriteSaveData("TCMB", bundles);
		}

		private static void GenerateBundles(object? sender, StardewModdingAPI.Events.SaveCreatedEventArgs e = null)
		{
			Console.WriteLine("[TCMB] Generating bundles");
			bundles = new Dictionary<string, string>();
			foreach (var b in Bundles)
			{
				var g = b.Generate();
				bundles.Add(g.Key, g.Value);
			}
			var staticbundles = new Dictionary<string, string>
			{
				{"Fish Tank/11", "Crab Pot/O 710 3/715 1 0 716 1 0 717 1 0 718 1 0 719 1 0 720 1 0 721 1 0 722 1 0 723 1 0 372 1 0/1/5" },
				{"Vault/23", "2,500g/O 220 3/-1 2500 2500/4"},
				{"Vault/24", "5,000g/O 369 30/-1 5000 5000/2"},
				{"Vault/25", "10,000g/BO 9 1/-1 10000 10000/3"},
				{"Vault/26", "25,000g/BO 21 1/-1 25000 25000/1"},
				{"Boiler Room/20", "Blacksmith's/BO 13 1/334 1 0 335 1 0 336 1 0/2"},
				{"Boiler Room/21", "Geologist's/O 749 5/80 1 0 86 1 0 84 1 0 82 1 0/1"},
				{"Boiler Room/22", "Adventurer's/R 518 1/766 99 0 767 10 0 768 1 0 769 1 0/1/2"},
				{"Bulletin Board/33", "Enchanter's/O 336 5/725 1 0 348 1 0 446 1 0 637 1 0/1"},
				{"Crafts Room/17", "Construction/BO 114 1/388 99 0 388 99 0 390 99 0 709 10 0/4" },
			};
			bundles.TryAddMany(staticbundles);
			Console.WriteLine("[TCMB] Generated Bundles");
			foreach (var l in bundles) Console.WriteLine($"\"{l.Key}\": \"{l.Value}\"");
			SetGame1Bundles();
		}

		public static bool LoadBundles(LocalizedContentManager content, ref Dictionary<string, string> __result)
		{
			if (bundles == null) return true;
			__result = bundles != null ? new Dictionary<string, string>(bundles) : new Dictionary<string, string>(); 
			foreach (var l in __result) Console.WriteLine($"\"{l.Key}\": \"{l.Value}\"");

			return false;
		}

		public class Bundle
		{
			public string Room;
			public int SpriteIdx;
			public string Name;
			public string Reward;
			public Pool[] Pools;
			public int Colour;
			int RequiredCount;
			public Bundle(string room, int sIdx, string name, string reward, int colour, int requiredCount, params Pool[] pools)
			{
				Room = room;
				SpriteIdx = sIdx;
				Name = name;
				Reward = reward;
				Colour = colour;
				RequiredCount = requiredCount;
				Pools = pools;
			}
			public KeyValuePair<string, string> Generate()
			{
				Console.WriteLine($"[TCMB] Generating {Name} Bundle");
				var requirements = new List<BundleItem>();
				foreach(var p in Pools)
				{
					requirements.AddRange(p.Select());
				}
				if (RequiredCount == -1) RequiredCount = requirements.Count;
				var key = $"{Room}/{SpriteIdx}";
				var value = $"{Name}/{Reward}/{string.Join(' ', requirements)}/{Colour}/{RequiredCount}";
				return new KeyValuePair<string, string>(key, value);
				Console.WriteLine($"[TCMB] Generated {Name} Bundle");

			}
		}
		public class BundleItem
		{
			public string Mod;
			public string ID;
			public int Count;
			public int MinQuality;
			public BundleItem(string id, string mod = "", int count = 1, int quality = 0)
			{
				Mod = mod;
				ID = (mod==""? "":mod + "_") +id;
				Count = count;
				MinQuality = quality;
			}
			public override string ToString()
			{
				return $"{ID} {Count} {MinQuality}";
			}
		}
		public class VanillaPool : Pool
		{
			public VanillaPool(int chooseCount, params BundleItem[] items) : base(chooseCount, items)
			{
			}
			public override BundleItem[] Select()
			{
				return AllItems;
			}
		}
		public class Pool
		{
			List<BundleItem> Items;
			protected BundleItem[] AllItems;
			int ChooseCount;
			public Pool(int chooseCount, params BundleItem[] items)
			{
				ChooseCount = chooseCount;
				AllItems = items;
				Items = new List<BundleItem>();
			}
			public Pool(int chooseCount, int quality, int amount, params BundleItem[] items) : this(chooseCount, items)
			{ 
				foreach(BundleItem i in items)
				{
					i.MinQuality = quality;
					i.Count = amount;
				}
			}
			public virtual BundleItem[] Select()
			{
				Items = new List<BundleItem>();
				foreach (var i in AllItems)
				{
					AddToList(Items, i);
				}
				var lastMod = "* But nobody came.";
				var onlyOneModLeft = false;
				var returnMe = new List<BundleItem>();
				for (int i = 0; i < ChooseCount; i++)
				{
				Choose:
					var item = Game1.random.ChooseFrom(Items);
					if(Items.Count == 0)
					{
						throw new Exception("Items was empty");
					}
					if(item == null)
					{
						Console.WriteLine("Item was null");
						goto Choose;
					}
					Console.WriteLine("[TCMB] Attempting to select " + item.ID);
					if (!ItemRegistry.Exists(item.ID)) continue;
					if (item.Mod == lastMod && !onlyOneModLeft)
					{
						onlyOneModLeft = true;
						foreach (var item2 in Items)
						{
							if (item.Mod != lastMod)
							{
								onlyOneModLeft = false;
								break;
							}
						}
						if (!onlyOneModLeft) goto Choose;
					}
					Console.WriteLine("[TCMB] Chose " + item.ID);

					returnMe.Add(item);
					Items.Remove(item);
					lastMod = item.Mod;
				}
				return returnMe.ToArray();
			}
		}
		public static void AddToList(List<BundleItem> list, BundleItem b)
		{
			if (ItemRegistry.Exists(b.ID))
			{
				list.Add(b);
				Console.WriteLine("[TCMB] Adding " + b.ID + " to pool");
			}
			else
			{
				Console.WriteLine("[TCMB] Skipping " + b.ID);
			}
		}

	}
}
