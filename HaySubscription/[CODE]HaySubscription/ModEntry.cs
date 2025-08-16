using ContentPatcher;
using StardewModdingAPI;
using StardewValley;
namespace HaySubscription
{
    public sealed partial class ModEntry : Mod
    {
		public static IModHelper ModHelper;
		public static bool SubscriptionOngoing;
		public static int LastCharged;
		public static bool HasExtraAnimalConfigFramework { get; private set; }
		public override void Entry(IModHelper helper)
		{
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
				var api = this.Helper.ModRegistry.GetApi<IContentPatcherAPI>("Pathoschild.ContentPatcher");
				api.RegisterToken(ModManifest, "LastChargedAmount", () =>  new[] { LastCharged.ToString() });
			};

		}
	}
}
