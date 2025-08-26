using StardewModdingAPI;

namespace Selph.StardewMods.ExtraAnimalConfig
{
	public interface IExtraAnimalConfigApi
	{
		// Get a list of every modded feed that is/can be stored;
		// the result is a dictionary of *qualified* item IDs
		// to an IFeedInfo object that can be used to get the capacity and modify count.
		// The IFeedInfo object is stateless so you can save it if you want.
		public Dictionary<string, IFeedInfo> GetModdedFeedInfo();
	}
	public interface IFeedInfo
	{
		// The total capacity
		public int capacity { get; }
		// The current count
		public int count { get; set; }
	}
}
namespace GenericModConfigMenu
{
	/// <summary>The API which lets other mods add a config UI through Generic Mod Config Menu.</summary>
	public interface IGenericModConfigMenuApi
	{
		/*********
        ** Methods
        *********/
		/****
        ** Must be called first
        ****/
		/// <summary>Register a mod whose config can be edited through the UI.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="reset">Reset the mod's config to its default values.</param>
		/// <param name="save">Save the mod's current config to the <c>config.json</c> file.</param>
		/// <param name="titleScreenOnly">Whether the options can only be edited from the title screen.</param>
		/// <remarks>Each mod can only be registered once, unless it's deleted via <see cref="Unregister"/> before calling this again.</remarks>
		void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);

		/// <summary>Add an integer option at the current position in the form.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="getValue">Get the current value from the mod config.</param>
		/// <param name="setValue">Set a new value in the mod config.</param>
		/// <param name="name">The label text to show in the form.</param>
		/// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
		/// <param name="min">The minimum allowed value, or <c>null</c> to allow any.</param>
		/// <param name="max">The maximum allowed value, or <c>null</c> to allow any.</param>
		/// <param name="interval">The interval of values that can be selected.</param>
		/// <param name="formatValue">Get the display text to show for a value, or <c>null</c> to show the number as-is.</param>
		/// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
		void AddNumberOption(IManifest mod, Func<int> getValue, Action<int> setValue, Func<string> name, Func<string> tooltip = null, int? min = null, int? max = null, int? interval = null, Func<int, string> formatValue = null, string fieldId = null);
	}
}
namespace ContentPatcher
{
	/// <summary>The Content Patcher API which other mods can access.</summary>
	public interface IContentPatcherAPI
	{
		/// <summary>Register a simple token.</summary>
		/// <param name="mod">The manifest of the mod defining the token (see <see cref="Mod.ModManifest"/> in your entry class).</param>
		/// <param name="name">The token name. This only needs to be unique for your mod; Content Patcher will prefix it with your mod ID automatically, like <c>YourName.ExampleMod/SomeTokenName</c>.</param>
		/// <param name="getValue">A function which returns the current token value. If this returns a null or empty list, the token is considered unavailable in the current context and any patches or dynamic tokens using it are disabled.</param>
		void RegisterToken(IManifest mod, string name, Func<IEnumerable<string>?> getValue);
	}
}