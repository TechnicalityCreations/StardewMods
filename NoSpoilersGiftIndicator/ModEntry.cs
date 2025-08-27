using GenericModConfigMenu;
using StardewModdingAPI;

namespace NoSpoilersGiftIndicator
{
	public partial class ModEntry : Mod
	{
		internal static Config Config { get; set; }
		internal static IModHelper Helper;
		public override void Entry(IModHelper helper)
		{
			Helper = helper;
			Config = helper.ReadConfig<Config>();
			helper.Events.Display.RenderedWorld += (s, e) => IndicatorManager.Draw();
			helper.Events.GameLoop.UpdateTicked += (s, e) => IndicatorManager.Update();
			helper.Events.GameLoop.GameLaunched += (s, e) =>
			{
				var gmcm = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
				if (gmcm == null) return;
				gmcm.Register(ModManifest, () => Config = new Config(), () => helper.WriteConfig(Config));
				gmcm.AddNumberOption(ModManifest, () => Config.IndicatorOpacityPercent, (v) => Config.IndicatorOpacityPercent = v, () => helper.Translation.Get("config.opacity"), () => helper.Translation.Get("config.opacity-tooltip"), 5, 100, 5, (x) => $"{x}%");
				gmcm.AddBoolOption(ModManifest, () => Config.OnlyShowOnBirthday, (v) => Config.OnlyShowOnBirthday = v, () => helper.Translation.Get("config.birthday"), () => helper.Translation.Get("config.birthday-tooltip"));
				gmcm.AddBoolOption(ModManifest, () => Config.ShowOnMaxHearts, (v) => Config.ShowOnMaxHearts = v, () => helper.Translation.Get("config.max-hearts"), () => helper.Translation.Get("config.max-hearts-tooltip"));
				gmcm.AddBoolOption(ModManifest, () => Config.ShowIfMaxedOutOnGifts, (v) => Config.ShowIfMaxedOutOnGifts = v, () => helper.Translation.Get("config.max-gifts"), () => helper.Translation.Get("config.max-gifts-tooltip"));
				gmcm.AddBoolOption(ModManifest, () => Config.IncludeLikedGifts, (v) => Config.IncludeLikedGifts = v, () => helper.Translation.Get("config.liked"), () => helper.Translation.Get("config.liked-tooltip"));
				gmcm.AddKeybindList(ModManifest, () => Config.Toggle, (v) => Config.Toggle = v, () => helper.Translation.Get("config.toggle"));
				gmcm.AddBoolOption(ModManifest, () => Config.AdvancedLogging, (v) => Config.AdvancedLogging = v, () => helper.Translation.Get("config.dev"), () => helper.Translation.Get("config.dev-tooltip"));
			};
		}
	}
}
