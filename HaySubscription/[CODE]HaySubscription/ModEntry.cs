using ContentPatcher;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Internal;
using Selph.StardewMods.ExtraAnimalConfig;
using GenericModConfigMenu;
namespace HaySubscription
{
    public sealed partial class ModEntry : Mod
    {
		public static ITranslationHelper Translation;
		public static bool SubscriptionOngoing;
		public static int LastCharged;
		internal static Config Config;
		public static bool HasExtraAnimalConfigFramework { get; private set; }
		public static IExtraAnimalConfigApi EACFApi { get; private set; }
		public override void Entry(IModHelper helper)
		{
			Translation = helper.Translation;
			Config = helper.ReadConfig<Config>();
			if(Config.HayMarkup < 1)
			{
				Config.HayMarkup = 1;
				helper.WriteConfig(Config);
			}
			GameLocation.RegisterTileAction("TechnicalityCreations.HaySubscription_HaySubscriptionBook", HaySubscriptionBook.SubBookAction);
			helper.Events.GameLoop.Saving += (s, e) => helper.Data.WriteSaveData(ModManifest.UniqueID, ModData.Create());

			helper.Events.GameLoop.DayStarted += (s, e) => SubscriptionManager.LoadFeeds();

			helper.Events.GameLoop.SaveCreating += (s, e) => helper.Data.WriteSaveData(ModManifest.UniqueID, new ModData());
			helper.Events.GameLoop.SaveLoaded += (s, e) => (helper.Data.ReadSaveData<ModData>(ModManifest.UniqueID) ?? new ModData()).Load();
			helper.Events.GameLoop.UpdateTicked += (s, e) => HaySubscriptionBook.Update();
			helper.Events.GameLoop.DayEnding += (s, e) => SubscriptionManager.DoHaySubscription();
			//Cross-mod compat and Content Patcher API
			helper.Events.GameLoop.GameLaunched += (s, e) =>
			{
				//EACF Compat
				HasExtraAnimalConfigFramework = helper.ModRegistry.IsLoaded("selph.ExtraAnimalConfig");
				if(HasExtraAnimalConfigFramework) EACFApi = Helper.ModRegistry.GetApi<IExtraAnimalConfigApi>("selph.ExtraAnimalConfig");
				
				//Content Patcher Token
				var cpApi = Helper.ModRegistry.GetApi<IContentPatcherAPI>("Pathoschild.ContentPatcher");
				cpApi.RegisterToken(ModManifest, "LastChargedAmount", () =>  new[] { LastCharged.ToString() });

				//GMCM Config Fields
				var gmcmApi = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
				if(gmcmApi != null)
				{
					gmcmApi.Register(ModManifest, () => Config = new Config(), () => helper.WriteConfig(Config));
					gmcmApi.AddNumberOption(ModManifest, () => (int)((Config.HayMarkup - 1) * 100), (int v) => Config.HayMarkup = ((((float)v) / 100f) + 1), ()=>Translation.Get("config.markup-name"), ()=>Translation.Get("config.markup-tooltip"), 0, interval: 5, formatValue: (int val)=>$"{val}%");
				}
			};
		}
	}
}
