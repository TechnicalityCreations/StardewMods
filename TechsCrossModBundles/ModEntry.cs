using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using System.Text;
using StardewValley.Extensions;
using StardewValley.GameData.Bundles;
using Microsoft.Xna.Framework;

namespace TechsCrossModBundles
{
    public sealed class ModEntry : Mod
    {
		static Dictionary<string, string> bundles;
		internal new static IModHelper Helper;
		const string SVE = "FlashShifter.StardewValleyExpandedCP", RSV = "Rafseazz.RSVCP", Vapius = "Lumisteria.MtVapius", CornucopiaCrops = "Cornucopia", Sunberry = "skellady.SBVCP";
		public readonly static Bundle[] Bundles = new Bundle[]
		{
			new Bundle("Crafts Room", 13, "Spring Foraging", "O 495 30", 0, -1,
				//Anywhere Pool
				new Pool(2, new BundleItem("16"), new BundleItem("18"), new BundleItem("20"), new BundleItem("22"), new BundleItem("Bluestar", Sunberry), new BundleItem("Peppercorn", CornucopiaCrops)),
				// Location/Time Specific
				new Pool(2, new BundleItem("399"), new BundleItem("296"), new BundleItem("Lucky_Four_Leaf_Clover", SVE), new BundleItem("Ridge_Cherry", RSV), new BundleItem("Ridge_Azorean_Flower", RSV), new BundleItem("BalmcreekCarillon", Vapius))),
			new Bundle("Crafts Room", 14, "Summer Foraging", "O 496 30", 3, -1,
				new Pool(3, new BundleItem("398"), new BundleItem("396"), new BundleItem("402"), new BundleItem("Ridge_Wild_Apple", RSV), new BundleItem("Marigold", Sunberry), new BundleItem("Raspberry", CornucopiaCrops))
				),
			new Bundle("Crafts Room", 15, "Fall Foraging", "O 497 30", 2, -1,
				//Main
				new Pool(1, new BundleItem("408"), new BundleItem("Mushroom_Colony", SVE), new BundleItem("Lava_Lily", RSV)),
				// Fruit
				new Pool(2, new BundleItem("406"), new BundleItem("410"), new BundleItem("Autumn_Drop_Berry", RSV)),
				// Mushroom
				new Pool(1, new BundleItem("404"))
				),
			new Bundle("Crafts Room", 16, "Winter Foraging", "O 498 30", 6, -1,
				// Main
				new Pool(2, new BundleItem("418"), new BundleItem("414"), new BundleItem("283"), new BundleItem("Sierra_Wintergreen", RSV), new BundleItem("CarmineBlossom", Sunberry), new BundleItem("JuniperBerries", CornucopiaCrops)),
				// Tilling
				new Pool(2, new BundleItem("412"), new BundleItem("416"), new BundleItem("Bearberrys", SVE))
				),
			new Bundle("Crafts Room", 17, "Construction", "BO 114 1", 4, -1,
				new Pool(4, new BundleItem("388", count:99), new BundleItem("388", count:99), new BundleItem("390", count:99), new BundleItem("709", count:10))),
			new Bundle("Crafts Room", 19, "Exotic Foraging", "O 235 5", 1, 5, 
				//Desert
				new Pool(2, new BundleItem("88"), new BundleItem("90")),
				//Trees
				new Pool(2, new BundleItem("724"), new BundleItem("725"), new BundleItem("726"), new BundleItem("Fir_Wax", SVE), new BundleItem("Birch_Water", SVE), new BundleItem("CinderLeaf", Sunberry), new BundleItem("ChicleRubber", CornucopiaCrops), new BundleItem("BirchSap", Vapius)),
				//Secret Woods
				new Pool(2, new BundleItem("257"), new BundleItem("259"), new BundleItem("Poison_Mushroom", SVE), new BundleItem("Smelly_Rafflesia")),
				//Other
				new Pool(2, new BundleItem("78"), new BundleItem("394"), new BundleItem("Thistle", SVE), new BundleItem("Mountain_Mistbloom", RSV))
				)


		};
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
			bundles = Helper.Data.ReadSaveData<Dictionary<string, string>>("TCB");
			if (bundles == null) GenerateBundles(null);
			else SetGame1Bundles();

		}
		static void SetGame1Bundles()
		{
			Game1.netWorldState.Value.SetBundleData(DataLoader.Bundles(Game1.content));
		}
		private void SaveBundles(object? sender, StardewModdingAPI.Events.SavingEventArgs e)
		{
			Helper.Data.WriteSaveData("TCB", bundles);
		}

		private static void GenerateBundles(object? sender, StardewModdingAPI.Events.SaveCreatedEventArgs e = null)
		{
			Console.WriteLine("[Tech's Cross Mod Bundles] Generating bundles");
			bundles = new Dictionary<string, string>();
			foreach(var b in Bundles)
			{
				var g = b.Generate();
				bundles.Add(g.Key, g.Value);
			}
			Console.WriteLine("Generated Bundles");
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
				var requirements = new List<BundleItem>();
				foreach(var p in Pools)
				{
					requirements.AddRange(p.Select());
				}
				if (RequiredCount == -1) RequiredCount = requirements.Count;
				var key = $"{Room}/{SpriteIdx}";
				var value = $"{Name}/{Reward}/{string.Join(' ', requirements)}/{Colour}/{RequiredCount}";
				return new KeyValuePair<string, string>(key, value);
			}
		}
		public class BundleItem
		{
			public string Mod;
			public string ID;
			public int Count;
			public int MinQuality;
			public BundleItem(string id, string mod = "", int count = 1, int minQuality = 0)
			{
				Mod = mod;
				ID = (mod==""? "":mod + "_") +id;
				Count = count;
				MinQuality = minQuality;
			}
			public override string ToString()
			{
				return $"{ID} {Count} {MinQuality}";
			}
		}
		public class Pool
		{
			List<BundleItem> Items;
			BundleItem[] AllItems;
			int ChooseCount;
			public Pool(int chooseCount, params BundleItem[] items)
			{
				ChooseCount = chooseCount;
				AllItems = items;
				Items = new List<BundleItem>();
			}
			public BundleItem[] Select()
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
					returnMe.Add(item);
					Items.Remove(item);
					lastMod = item.Mod;
				}
				return returnMe.ToArray();
			}
		}
		public static void AddToList(List<BundleItem> list, BundleItem b)
		{
			if (b.Mod == "" || Helper.ModRegistry.IsLoaded(b.Mod))
			{
				list.Add(b);
			}
		}

	}
}
