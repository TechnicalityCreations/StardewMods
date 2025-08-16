
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Dimensions;

namespace HaySubscription
{
	internal static class HaySubscriptionBook
	{
		static ITranslationHelper Translate => ModEntry.ModHelper.Translation;
		static Translation yesTranslation => Translate.Get("sub-book-dialogue.yes");
		static Translation noTranslation => Translate.Get("sub-book-dialogue.no");
		public static bool SubBookAction(GameLocation location, string[] actionArgs, Farmer who, Point Location)
		{
			if(!who.IsMainPlayer) Game1.activeClickableMenu = new DialogueBox(Translate.Get("sub-book-dialogue.not-main-player"));
			else if (Game1.getFarm().GetHayCapacity() < 1) Game1.activeClickableMenu = new DialogueBox(Translate.Get("sub-book-dialogue.no-silo"));
			else if (ModEntry.SubscriptionOngoing) location.createQuestionDialogue(Translate.Get("sub-book-dialogue.cancel"), new Response[] { new Response("y", yesTranslation), new Response("n", noTranslation) }, ConfirmSubscriptionCancel);
			else location.createQuestionDialogue(Translate.Get("sub-book-dialogue.question"), new Response[] { new Response("y", yesTranslation), new Response("n", noTranslation) }, ConfirmSubscriptionStart);
			return true;
		}
		public static void ConfirmSubscriptionStart(Farmer who, string whichAnswer)
		{
			if(whichAnswer == "n") return;
			NextTickSubStart = true;
			NextTickSubPlayer = who;
		}
		public static void ConfirmSubscriptionCancel(Farmer who, string whichAnswer)
		{
			if (whichAnswer == "n") return;
			NextTickSubCancel = true;
			NextTickSubPlayer = who;
		}
		public static void SubscriptionStart(Farmer who, string whichAnswer)
		{
			Console.WriteLine($"Start Subscription? {whichAnswer}");
			if (whichAnswer == "n") return;
			Console.WriteLine("Started Subscription");
			ModEntry.SubscriptionOngoing = true;
			Game1.activeClickableMenu = new DialogueBox(Translate.Get("sub-book-dialogue.success"));
		}
		public static void SubscriptionCancel(Farmer who, string whichAnswer)
		{
			Console.WriteLine($"End Subscription? {whichAnswer}");
			if (whichAnswer == "n") return;
			Console.WriteLine("Ended Subscription");
			ModEntry.SubscriptionOngoing = false;
			Game1.activeClickableMenu = new DialogueBox(Translate.Get("sub-book-dialogue.cancel-success"));
		}
		public static void Update()
		{
			if (NextTickSubStart)
			{
				Console.WriteLine("Subscription Confirm?");
				NextTickSubPlayer.currentLocation.createQuestionDialogue(Translate.Get("sub-book-dialogue.confirm"), new Response[] { new Response("y", yesTranslation), new Response("n", noTranslation) }, SubscriptionStart);
				NextTickSubStart = false;
			}
			if (NextTickSubCancel)
			{
				Console.WriteLine("Cancel Confirm?");
				NextTickSubPlayer.currentLocation.createQuestionDialogue(Translate.Get("sub-book-dialogue.cancel-confirm"), new Response[] { new Response("y", yesTranslation), new Response("n", noTranslation) }, SubscriptionCancel);
				NextTickSubCancel = false;
			}
		}
		static bool NextTickSubStart = false;
		static Farmer NextTickSubPlayer;
		static bool NextTickSubCancel = false;
	}
}
