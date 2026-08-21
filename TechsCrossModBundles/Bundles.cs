using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechsCrossModBundles
{
	partial class ModEntry
	{
		public readonly static Bundle[] Bundles = new Bundle[] 
		{
			new Bundle("Crafts Room", 13, "Spring Foraging", "O 495 30", 0, -1,
				new Pool(2,
					new BundleItem("16"), // Horseradish
					new BundleItem("18"), // Daff
					new BundleItem("20"), // Leek
					new BundleItem("22"), // Dandelion
					new BundleItem("Bluestar", Sunberry),
					new BundleItem("Peppercorn", Cornucopia)
				),
				new Pool(2,
					new BundleItem("399"), // Spring Onion
					new BundleItem("296"), // Salmonberry
					new BundleItem("Lucky_Four_Leaf_Clover", SVE),
					new BundleItem("Ridge_Cherry", RSV),
					new BundleItem("Ridge_Azorean_Flower", RSV),
					new BundleItem("BalmcreekCarillon", Vapius)
				)
			),
			new Bundle("Crafts Room", 14, "Summer Foraging", "O 496 30", 3, -1,
				new Pool(3,
					new BundleItem("398"), // Grape
					new BundleItem("396"), // Spice Berry
					new BundleItem("402"), // Sweet Pea
					new BundleItem("Ridge_Wild_Apple", RSV),
					new BundleItem("Marigold", Sunberry),
					new BundleItem("Raspberry", Cornucopia)
				)
			),
			new Bundle("Crafts Room", 15, "Fall Foraging", "O 497 30", 2, -1,
				new Pool(1,
					new BundleItem("408"), // Hazelnut
					new BundleItem("Mushroom_Colony", SVE),
					new BundleItem("Lava_Lily", RSV)
				),
				new Pool(2,
					new BundleItem("406"), // Wild Plum
					new BundleItem("410"), // Blackberry
					new BundleItem("Autumn_Drop_Berry", RSV)
				),
				new Pool(1,
					new BundleItem("404") // Common Mushroom
				)
			),
			new Bundle("Crafts Room", 16, "Winter Foraging", "O 498 30", 6, -1,
				new Pool(2,
					new BundleItem("418"), // Crocus
					new BundleItem("414"), // Crystal Fruit
					new BundleItem("283"), // Holly
					new BundleItem("Sierra_Wintergreen", RSV),
					new BundleItem("CarmineBlossom", Sunberry),
					new BundleItem("JuniperBerries", Cornucopia)
				),
				new Pool(2,
					new BundleItem("412"), // Winter Root
					new BundleItem("416"), // Snow Yam
					new BundleItem("Bearberrys", SVE)
				)
			),
			new Bundle("Crafts Room", 19, "Exotic Foraging", "O 235 5", 1, 5,
				new Pool(2,
					new BundleItem("88"), // Coconut
					new BundleItem("90") // Cactus Fruit
				),
				new Pool(2,
					new BundleItem("724"), // Maple Syrup
					new BundleItem("725"), // Oak Resin
					new BundleItem("726"), // Pine Tar
					new BundleItem("Fir_Wax", SVE),
					new BundleItem("Birch_Water", SVE),
					new BundleItem("CinderLeaf", Sunberry),
					new BundleItem("ChicleRubber", Cornucopia),
					new BundleItem("BirchSap", Vapius)
				),
				new Pool(2,
					new BundleItem("257"), // Morel
					new BundleItem("259"), // Fiddlehead Fern
					new BundleItem("Poison_Mushroom", SVE),
					new BundleItem("Smelly_Rafflesia", SVE)
				),
				new Pool(2,
					new BundleItem("78"), // Cave Carrot
					new BundleItem("394"), // Rainbow Shell
					new BundleItem("Thistle", SVE),
					new BundleItem("Mountain_Mistbloom", RSV)
				)
			),

			new Bundle("Pantry", 0, "Spring Crops", "O 465 20", 0, -1,
				new Pool(4,
					new BundleItem("24"), // Parsnip
					new BundleItem("188"), // Green Bean
					new BundleItem("190"), // Cauliflower
					new BundleItem("192"), // Potato
					new BundleItem("250"), // Kale
					new BundleItem("Carrot"),
					new BundleItem("Cucumber", SVE),
					new BundleItem("SugarBeet", Cornucopia),
					new BundleItem("Asparagus", Cornucopia),
					new BundleItem("Buckwheat", Cornucopia),
					new BundleItem("Cabbage", Cornucopia),
					new BundleItem("Onion", Cornucopia),
					new BundleItem("Spinach", Cornucopia)
				)
			),
			new Bundle("Pantry", 1, "Summer Crops", "O 621 1", 3, -1,
				new Pool(4,
					new BundleItem("256"), // Tomato
					new BundleItem("260"), // Hot Pepper
					new BundleItem("258"), // Blueberry
					new BundleItem("254"), // Melon
					new BundleItem("SummerSquash"),
					new BundleItem("Butternut_Squash", SVE),
					new BundleItem("Sunberries", Sunberry),
					new BundleItem("BellPepper", Cornucopia),
					new BundleItem("Honeydew", Cornucopia),
					new BundleItem("GreenPeas", Cornucopia),
					new BundleItem("Watermelon", Cornucopia),
					new BundleItem("Chickpea", Cornucopia)
				)
			),
			new Bundle("Pantry", 2, "Fall Crops", "BO 10 1", 2, -1,
				new Pool(4,
					new BundleItem("270"), // Corn
					new BundleItem("272"), // Eggplant
					new BundleItem("276"), // Pumpkin
					new BundleItem("280"), // Yam
					new BundleItem("Broccoli"),
					new BundleItem("282"), // Cranberry
					new BundleItem("Sweet_Potato", SVE),
					new BundleItem("Barley", Cornucopia),
					new BundleItem("Zucchini", Cornucopia),
					new BundleItem("Celery", Cornucopia),
					new BundleItem("Lentils", Cornucopia),
					new BundleItem("Oats", Cornucopia),
					new BundleItem("Turnip", Cornucopia)
				)
			),
			new Bundle("Pantry", 3, "Quality Crops", "BO 15 1", 6, 3,
				// Spring
				new Pool(1, 2, 5,
					new BundleItem("24"), // Parsnip
					new BundleItem("188"), // Green Bean
					new BundleItem("190"), // Cauliflower
					new BundleItem("192"), // Potato
					new BundleItem("250"), // Kale
					new BundleItem("Carrot"),
					new BundleItem("Cucumber", SVE),
					new BundleItem("SugarBeet", Cornucopia),
					new BundleItem("Asparagus", Cornucopia),
					new BundleItem("Buckwheat", Cornucopia),
					new BundleItem("Cabbage", Cornucopia),
					new BundleItem("Onion", Cornucopia),
					new BundleItem("Spinach", Cornucopia)
				),
				//Summer
				new Pool(1, 2, 5,
					new BundleItem("256"), // Tomato
					new BundleItem("260"), // Hot Pepper
					new BundleItem("258"), // Blueberry
					new BundleItem("254"), // Melon
					new BundleItem("SummerSquash"),
					new BundleItem("Butternut_Squash", SVE),
					new BundleItem("Sunberries", Sunberry),
					new BundleItem("BellPepper", Cornucopia),
					new BundleItem("Honeydew", Cornucopia),
					new BundleItem("GreenPeas", Cornucopia),
					new BundleItem("Watermelon", Cornucopia),
					new BundleItem("Chickpea", Cornucopia)
				),
				//Fall
				new Pool(1, 2, 5,
					new BundleItem("270"), // Corn
					new BundleItem("272"), // Eggplant
					new BundleItem("276"), // Pumpkin
					new BundleItem("280"), // Yam
					new BundleItem("Broccoli"),
					new BundleItem("282"), // Cranberry
					new BundleItem("Sweet_Potato", SVE),
					new BundleItem("Barley", Cornucopia),
					new BundleItem("Zucchini", Cornucopia),
					new BundleItem("Celery", Cornucopia),
					new BundleItem("Lentils", Cornucopia),
					new BundleItem("Oats", Cornucopia),
					new BundleItem("Turnip", Cornucopia)
				)
			),
			new Bundle("Pantry", 4, "Animal", "BO 12 1", 4, 5,
				new Pool(3,
					new BundleItem("174"), // Large White Egg
					new BundleItem("182"), // Large Brown Egg
					new BundleItem("442"), // Duck Egg
					new BundleItem("LargeQuailEgg", "Mizu.Quail"),
					new BundleItem("GooseEgg", "Mizu.Goose")
				),
				new Pool(2, 
					new BundleItem("186"), // Large Milk
					new BundleItem("438") // Large Goat Milk
				),
				new Pool(1,
					new BundleItem("440"), // Wool
					new BundleItem("SpeckledFowlEgg", Vapius)
				)
			),
			new Bundle("Pantry", 5, "Artisan", "BO 10 1", 1, 6,
				new Pool(1,
					new BundleItem("432"), // Truffle Oil
					new BundleItem("428"), // Cloth
					new BundleItem("TruffleMayonnaise", Cornucopia),
					new BundleItem("BlueVapiusCheese", Vapius),
					new BundleItem("EweCheese", Vapius)
				),
				new Pool(1,
					new BundleItem("307") // Duck Mayo
				),
				new Pool(1, 
					new BundleItem("426"), // Goat Cheese
					new BundleItem("SkyshardSharpCheese")
				),
				new Pool(3,
					new BundleItem("613"), // Apple
					new BundleItem("634"), // Apricot
					new BundleItem("635"), // Orange
					new BundleItem("636"), // Peach
					new BundleItem("637"), // Pom
					new BundleItem("638"), // Cherry
					new BundleItem("Nectarine", SVE),
					new BundleItem("Pear", SVE),
					new BundleItem("Cherry_Pluot", RSV),
					new BundleItem("Highland_Jostaberry", RSV),
					new BundleItem("Mountain_Plumcot", RSV),
					new BundleItem("Northern_Limequat", RSV),
					new BundleItem("Paradise_Rangpur", RSV),
					new BundleItem("Tropi_Ugli_Fruit", RSV),
					new BundleItem("Lemon", Cornucopia),
					new BundleItem("Lime", Cornucopia),
					new BundleItem("Grapefruit", Cornucopia),
					new BundleItem("Pomelo", Cornucopia),
					new BundleItem("Fig", Cornucopia),
					new BundleItem("Yuzu", Cornucopia)
					),
				new Pool(3,
					new BundleItem("344"), // Jelly
					new BundleItem("342"), // Pickles
					new BundleItem("348"), // Wine
					new BundleItem("350"), // Juice
					new BundleItem("DriedFlower", Cornucopia),
					new BundleItem("DriedHerb", Cornucopia),
					new BundleItem("Candy_CandiedFruit", Vapius),
					new BundleItem("Candy_CandiedFlower", Vapius)
					),
				new Pool(2,
					new BundleItem("340"), // Honey
					new BundleItem("DriedMushrooms"),
					new BundleItem("RoyalJelly", Cornucopia)
				)
			),

			new Bundle("Fish Tank", 6, "River Fish", "O 685 30", 6, -1,
				new Pool(3,
					new BundleItem("145"), // Sunfish
					new BundleItem("706"), // Shad
					new BundleItem("699"), // Tiger Trout
					new BundleItem("Minnow", SVE)
				),
				new Pool(1,
					new BundleItem("143") // Catfish
				)
			),
			new Bundle("Fish Tank", 7, "Lake Fish", "O 687 1", 0, -1,
				new Pool(3,
					new BundleItem("136"), // Largemouth Bass
					new BundleItem("142"), // Carp
					new BundleItem("700"), // Bullhead
					new BundleItem("Tadpole", SVE),
					new BundleItem("Bull_Trout", SVE)
				),
				new Pool(1,
					new BundleItem("698") // Sturgeon
				)
			),
			new Bundle("Fish Tank", 8, "Ocean Fish", "O 690 5", 5, -1,
				new Pool(4,
					new BundleItem("131"), // Sardine
					new BundleItem("130"), // Tuna
					new BundleItem("150"), // Red Snapper
					new BundleItem("701"), // Tilapia
					new BundleItem("Starfish", SVE),
					new BundleItem("SeaTurtle", ASF),
					new BundleItem("Marlin", ASF)
				)
			),
			new Bundle("Fish Tank", 9, "Night Fishing", "R 516 1", 1, -1,
				new Pool(3,
					new BundleItem("132"), // Bream
					new BundleItem("140"), // Walleye
					new BundleItem("148"), // Eel
					new BundleItem("269"), // Midnight Carp
					new BundleItem("Slime_Barbel", ES),
					new BundleItem("FiretailGuppy", Sunberry)
				)
			),
			new Bundle("Fish Tank", 10, "Specialty Fishing", "O 242 5", 4, -1,
				new Pool(4,
					new BundleItem("128"), // Pufferfish
					new BundleItem("156"), // Ghostfish
					new BundleItem("164"), // Sandfish
					new BundleItem("734"), // Woodskip
					new BundleItem("Radioactive_Bass", SVE),
					new BundleItem("Ridge_Bluegill", RSV),
					new BundleItem("LeafyTrout", Vapius),
					new BundleItem("MarineGold", "DTZ.DowntownZuzuCP"),
					new BundleItem("MossyEel", ASF),
					new BundleItem("JackOFish", ASF)
				)
			),

			new Bundle("Bulletin Board", 31, "Chef's", "O 221 3", 4, -1,
				new Pool(2,
					new BundleItem("724"), // M Syrup
					new BundleItem("259"), // F Fern
					new BundleItem("Birch_Syrup", SVE),
					new BundleItem("JuniperBerries", Cornucopia),
					new BundleItem("Sugarcane", Cornucopia)
				),
				new Pool(2,
					new BundleItem("430"), // Truffle
					new BundleItem("376"), // Poppy
					new BundleItem("Gold_Carrot", SVE),
					new BundleItem("Chives", Cornucopia),
					new BundleItem("Parsley", Cornucopia),
					new BundleItem("Olive", Vapius)
				),
				new Pool(2,
					new BundleItem("194"), // Fried Egg
					new BundleItem("228"), // Maki
					new BundleItem("Frog_Legs", SVE),
					new BundleItem("Glazed_Butterfish", SVE),
					new BundleItem("Arugula_Roll", RSV),
					new BundleItem("ChocolatePudding", Cornucopia),
					new BundleItem("Cooking_Quiche", Vapius)
				)
			),
			new Bundle("Bulletin Board", 34, "Dye", "BO 25 1", 6, -1,
				new Pool(1,
					new BundleItem("420"), // Red Mushroom
					new BundleItem("Sweet_Potato", SVE),
					new BundleItem("Paneeki", ES),
					new BundleItem("Geranium", Cornucopia)
					),
				new Pool(1,
					new BundleItem("396"), // Spice Berry
					new BundleItem("Red_Baneberry", SVE),
					new BundleItem("Lava_Lily", RSV)
					),
				new Pool(1,
					new BundleItem("421"), // Sunflower
					new BundleItem("Forest_Amancay", RSV),
					new BundleItem("Marigold", Sunberry)
					),
				new Pool(1,
					new BundleItem("444"), // Duck Feather
					new BundleItem("Aloe", Cornucopia)
					),
				new Pool(1,
					new BundleItem("62"), // Aquamarine
					new BundleItem("Mountain_Hokkaido", RSV),
					new BundleItem("Azure_Chrysanthemum", ES),
					new BundleItem("BlueMist", Cornucopia)
					),
				new Pool(1,
					new BundleItem("397"), // Sea Urchin
					new BundleItem("ZinfazuFruit", ES),
					new BundleItem("Sunberries", Sunberry),
					new BundleItem("Clematis", Cornucopia),
					new BundleItem("PinkMorningGlory", Vapius)
					)
			),
			new Bundle("Bulletin Board", 32, "Field Research", "BO 20 1", 5, -1,
				new Pool(2,
					new BundleItem("422"), // Purple Mushroom
					new BundleItem("392"), // Nautilus Shell
					new BundleItem("Lucky_Four_Leaf_Clover", SVE),
					new BundleItem("Amber", SVE),
					new BundleItem("Violet_Devil_s_Claw", RSV),
					new BundleItem("ChickenoftheWoods", Cornucopia),
					new BundleItem("WoodSilkFlower", Vapius)
				),
				new Pool(1,
					new BundleItem("702"), // Chub
					new BundleItem("Puppyfish", SVE),
					new BundleItem("Caped_Tree_Frog", RSV),
					new BundleItem("Obsidian_Maw", ES),
					new BundleItem("BubblyBetta", Sunberry),
					new BundleItem("CrystalFish", Vapius),
					new BundleItem("HermitCrab", ASF)
				),
				new Pool(1,
					new BundleItem("536"), // F Geode
					new BundleItem("SunberryGeode", Sunberry),
					new BundleItem("BlackGeode", Vapius)
				)
			),
			new Bundle("Bulletin Board", 35, "Fodder", "BO 104 1", 3, -1,
				new Pool(1,
					new BundleItem("262", count: 10), // Wheat
					new BundleItem("Buckwheat", Cornucopia, count: 10)
				),
				new Pool(1,
					new BundleItem("178", count: 10), // Hay
					new BundleItem("selph.CoopFeed.ChickenFeed", count: 20)
				),
				new Pool(1,
					new BundleItem("613", count:3)
				)
			),

			new Bundle("Abandoned Joja Mart", 36, "The Missing", "", 1, 5,
				new Pool(6,
					new BundleItem("348", quality: 1), // Wine
					new BundleItem("807"), // Dino Mayo
					new BundleItem("74", quality: 2), // Prismatic Shard
					new BundleItem("454", quality: 2, count: 5), // Ancient Fruit
					new BundleItem("795", quality: 2), // Void Salmon
					new BundleItem("445"), // Caviar
					new BundleItem("162"), // Lava Eel
					new BundleItem("Swirl_Stone", SVE),
					new BundleItem("Aged_Blue_Moon_Wine", SVE),
					new BundleItem("Hundred_Flavor_Doughnut", RSV),
					new BundleItem("LunarBean", Sunberry)
				)
			)
		};
	}
}
