using StardewModdingAPI;
using StardewValley;

namespace YearlyNightMarketPearl
{
	public class ModEntry : Mod
	{
		public override void Entry(IModHelper helper)
		{
			helper.Events.GameLoop.DayEnding += RemoveMermaidPearl;
		}

		private void RemoveMermaidPearl(object? sender, StardewModdingAPI.Events.DayEndingEventArgs e)
		{
			if(Game1.dayOfMonth != 13 || !Game1.IsWinter) return;
			if (Game1.player.mailReceived.Remove("gotPearl"))
			{
				Console.WriteLine($"[YearlyNightMarketPearl] Successfully cleared night market pearl from player {Game1.player.name}'s data");
			}
			else
			{
				Console.WriteLine($"[YearlyNightMarketPearl] Failed to clear night market pearl from player {Game1.player.name}'s data due to not being present");
			}

		}
	}
}
