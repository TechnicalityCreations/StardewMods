using StardewModdingAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSpoilersGiftIndicator
{
	public sealed class Config
	{
		public int IndicatorOpacityPercent = 60;
		public bool ShowIfMaxedOutOnGifts { get; set; } = false;
		public bool OnlyShowOnBirthday { get; set; } = false;
		public bool IncludeLikedGifts { get; set; } = true;
		public bool ShowOnMaxHearts { get; set; } = false;
		public KeybindList Toggle { get; set; } = KeybindList.Parse("RightControl");
		public bool IsToggled = true;
	}
}
