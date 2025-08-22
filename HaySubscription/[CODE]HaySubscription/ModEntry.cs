using ContentPatcher;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Internal;
namespace HaySubscription
{
    public sealed partial class ModEntry : Mod
    {
		public static IModHelper ModHelper;
		public static bool SubscriptionOngoing;
		public static int LastCharged;
		public static bool HasExtraAnimalConfigFramework { get; private set; }
		public static Selph.StardewMods.ExtraAnimalConfig.IExtraAnimalConfigApi EACFApi { get; private set; }
		public override void Entry(IModHelper helper)
		{
			if(!SubscriptionManager.AnimalShopHasItem(StardewValley.Object.hayQID))
			{
				throw new System.Exception("[HaySubscription] CompatabilityError: A mod has removed hay from Marnie's Shop. Please disable this mod or the incompatable mod and try again");
			}
			ModHelper = helper;
			GameLocation.RegisterTileAction("TechnicalityCreations.HaySubscription_HaySubscriptionBook", HaySubscriptionBook.SubBookAction);
			helper.Events.GameLoop.Saving += (s, e) => helper.Data.WriteSaveData(ModManifest.UniqueID, ModData.Create());
			helper.Events.GameLoop.SaveCreating += (s, e) => helper.Data.WriteSaveData(ModManifest.UniqueID, new ModData());
			helper.Events.GameLoop.SaveLoaded += (s, e) => (helper.Data.ReadSaveData<ModData>(ModManifest.UniqueID) ?? new ModData()).Load();
			helper.Events.GameLoop.UpdateTicked += (s, e) => HaySubscriptionBook.Update();
			helper.Events.GameLoop.DayEnding += (s, e) => SubscriptionManager.DoHaySubscription();
			//Cross-mod compat and Content Patcher API
			helper.Events.GameLoop.GameLaunched += (s, e) =>
			{
				HasExtraAnimalConfigFramework = ModHelper.ModRegistry.IsLoaded("selph.ExtraAnimalConfig");
				if(HasExtraAnimalConfigFramework) EACFApi = Helper.ModRegistry.GetApi<Selph.StardewMods.ExtraAnimalConfig.IExtraAnimalConfigApi>("selph.ExtraAnimalConfig");
				var api = Helper.ModRegistry.GetApi<IContentPatcherAPI>("Pathoschild.ContentPatcher");
				api.RegisterToken(ModManifest, "LastChargedAmount", () =>  new[] { LastCharged.ToString() });
			};

		}
	}
}
