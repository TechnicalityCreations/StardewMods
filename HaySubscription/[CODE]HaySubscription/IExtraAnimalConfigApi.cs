using System;
using System.Collections.Generic;
using StardewValley;
using StardewValley.GameData;

using SObject = StardewValley.Object;

namespace Selph.StardewMods.ExtraAnimalConfig;

public interface IExtraAnimalConfigApi {
  // Get a list of item queries, in order, that can potentially replace the specified output of this animal. Returns an empty list if there are no overrides.
  // animalType: the animal type (ie. the key in Data/FarmAnimals)
  // produceId: the qualified or unqualified ID of the base produce (ie. the value in (Deluxe)ProduceItemIds)
  public List<GenericSpawnItemDataWithCondition> GetItemQueryOverrides(string animalType, string produceId);
  // Events that will fire when a produce is created. You can register an event here to listen for animal produce events, and potentially even change the output item.
  // WARNING: Because of a quirk in vanilla code, this will fire if an autograbber was unable to grab the produce from a tool harvested animal because it is full and doing nothing afterwards!
  // Drop overnight animals are safe from this event double firing.
  public event Action<IAnimalProduceCreatedEvent>? AnimalProduceCreated;
  // Get a list of extra custom drops associated with this animal using EAC's feature (ie not in Data/FarmAnimals).
  // This is a dictionary of strings to lists of unqualified item IDs, with each dictionary corresponding to one slot.
  // Each slot will be filled by at most one produce from one item in the list.
  // NOTE: EAC may also override the drop with an item query. Use the API function GetItemQueryOverrides to check if this is the case.
  public Dictionary<string, List<string>> GetExtraDrops(string animalType);
  // Get a list of every modded feed that is/can be stored;
  // the result is a dictionary of *qualified* item IDs
  // to an IFeedInfo object that can be used to get the capacity and modify count.
  // The IFeedInfo object is stateless so you can save it if you want.
  public Dictionary<string, IFeedInfo> GetModdedFeedInfo();
}

// The harvest method associated with this animal. Note that (aside from method Tool and null tool) this is not an indicator of whether the produce was autograbbed.
public enum ProduceMethod {
  DropOvernight,
  Tool,
  DigUp,
  Debris
}

public interface IAnimalProduceCreatedEvent {
  public FarmAnimal animal { get; }
  // You can modify this field to change what gets returned.
  // Note that changing the stack count does not work - the stack will get reset to 1 (or 2 with Golden Animal Cracker).
  // To add extra stacks, add a copy to player inventory/world/autograbber/etc.
  public SObject produce { get; set; }
  public ProduceMethod produceMethod { get; }
  // If harvested with tool, the tool that harvested this animal. Will be null if autograbbed or not harvested with tool.
  public Tool? tool { get; }
}

public interface IFeedInfo {
  // The total capacity
  public int capacity { get; }
  // The current count
  public int count { get; set; }
}
