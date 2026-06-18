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
		public readonly static Bundle[] Bundles = new Bundle[]
		{
			new Bundle("Crafts Room", 13, "Spring Foraging", "O 495 30", 0, -1,
				//Anywhere Pool
				new Pool(2, new BundleItem("16"), new BundleItem("18"), new BundleItem("20"), new BundleItem("22"), new BundleItem("Bluestar", "skellady.SBVCP"), new BundleItem("Peppercorn", "Cornucopia")),
				// Location/Time Specific
				new Pool(2, new BundleItem("399"), new BundleItem("296"), new BundleItem("Lucky_Four_Leaf_Clover", "FlashShifter.StardewValleyExpandedCP"), new BundleItem("Ridge_Cherry", "Rafseazz.RSVCP"), new BundleItem("Ridge_Azorean_Flower", "Rafseazz.RSVCP")))

		};
		public override void Entry(IModHelper helper)
		{
			Helper = helper;
			var harmony = new Harmony(ModManifest.UniqueID);
			harmony.Patch(AccessTools.Method(typeof(DataLoader), nameof(DataLoader.Bundles)), new HarmonyMethod(AccessTools.Method(typeof(ModEntry), nameof(LoadBundles))));
			helper.Events.GameLoop.SaveCreated += GenerateBundles;
			helper.Events.GameLoop.Saving += SaveBundles;
			helper.Events.GameLoop.SaveLoaded += LoadBundlesVar;
		}

		private static void LoadBundlesVar(object? sender = null, StardewModdingAPI.Events.SaveLoadedEventArgs e = null)
		{
			bundles = Helper.Data.ReadSaveData<Dictionary<string, string>>("TCB");
			if (bundles == null) GenerateBundles(null);
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

		}

		public static bool LoadBundles(LocalizedContentManager content, ref Dictionary<string, string> __result)
		{
			Console.WriteLine("Loading Bundles");
			LoadBundlesVar();
			__result = new Dictionary<string, string>(bundles);
			foreach (var l in __result) Console.WriteLine($"\"{l.Key}\": \"{l.Value}\"");

			return false;//__result != null;
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
				ID = mod + "_"+id;
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
