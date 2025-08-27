using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.GameData.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NoSpoilersGiftIndicator
{
	internal static class IndicatorManager
	{
		static Dictionary<NPC, string[]> NpcGifts = new Dictionary<NPC, string[]>();
		static bool lastToggle = false;
		public static void Update()
		{
			if (Game1.gameMode != Game1.playingGameMode) return;
			if (ModEntry.Config.Toggle.IsDown())
			{
				if (!lastToggle) ModEntry.Config.IsToggled = !ModEntry.Config.IsToggled;
				lastToggle = true;
			}
			else
			{
				lastToggle = false;
			}
			NpcGifts.Clear();
			if (!ModEntry.Config.IsToggled) return;
			var player = Game1.player;
			if(player == null) return;
			foreach(NPC npc in player.currentLocation.characters)
			{
				if (ModEntry.Config.OnlyShowOnBirthday && !npc.isBirthday()) continue;
				if (npc.IsEmoting) continue;
				if(npc.IsInvisible) continue;
				if(npc.isSleeping.Value) continue;
				if(Game1.IsChatting) continue;
				if (player.friendshipData.TryGetValue(npc.Name, out Friendship value))
				{
					if(value == null) continue;
					if (((value.GiftsThisWeek >= 2 && !value.IsMarried() && !value.IsRoommate() && !npc.isBirthday()) || value.GiftsToday > 0) && !ModEntry.Config.ShowIfMaxedOutOnGifts) return;
					var MaxHearts = value.IsMarried() || value.IsRoommate() ? 14 : value.IsDating() ? 10 : npc.datable.Value ? 8 : 10;
					if (((int)MathF.Floor(value.Points / NPC.friendshipPointsPerHeartLevel)) >= MaxHearts && !ModEntry.Config.ShowOnMaxHearts) continue;

					List<string> loved = new List<string>();
					List<string> liked = new List<string>();

					// Find Gifts
					foreach (Item i in player.Items)
					{
						if(i==null) continue;
						if(!player.hasGiftTasteBeenRevealed(npc, i.ItemId)) continue;
						var taste = npc.getGiftTasteForThisItem(i);
						if (taste == NPC.gift_taste_love) loved.Add(i.QualifiedItemId);
						else if (taste == NPC.gift_taste_like) liked.Add(i.QualifiedItemId);
					}
					if (loved.Count > 0) NpcGifts.Add(npc, loved.ToArray());
					else if (liked.Count > 0 && ModEntry.Config.IncludeLikedGifts) NpcGifts.Add(npc, liked.ToArray());
				}
			}
		}
		public static void Draw()
		{
			foreach (NPC n in NpcGifts.Keys)
			{
				var itemIdx = Game1.currentGameTime.TotalGameTime.Seconds % NpcGifts[n].Length;
				var item = NpcGifts[n][itemIdx];
				var itemData = ItemRegistry.GetDataOrErrorItem(item);
				var itemTexture = itemData.GetTexture();
				var itemSourceRect = itemData.GetSourceRect();
				Game1.spriteBatch.Draw(Game1.mouseCursors, Game1.GlobalToLocal(Game1.viewport, n.position.Value - new Vector2(-12, 95)), new Rectangle?(new Rectangle(141, 465, 20, 24)), Color.White * (ModEntry.Config.IndicatorOpacityPercent / 100f), 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
				Game1.spriteBatch.Draw(itemTexture, Game1.GlobalToLocal(Game1.viewport, n.position.Value - new Vector2(-12, 95) + (new Vector2(20, 22))), itemSourceRect, Color.White * (ModEntry.Config.IndicatorOpacityPercent / 100f), 0f, itemSourceRect.Size.ToVector2() / 2f, 2f, SpriteEffects.None, 0f);
			}
		}
	}
}
