using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

[System.Serializable]
public abstract class SaveDatas
{
    public int Version { get; protected set; }
    public abstract SaveDatas VersionUp();
}

[System.Serializable]
public class SaveDataV1 : SaveDatas
{
    public string PlayerName { get; set; } = string.Empty;



    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveDatas VersionUp()
    {
        var saveData = new SaveDataV2();

        saveData.Name = PlayerName;

        return saveData;
    }
}

[System.Serializable]
public class SaveDataV2 : SaveDatas
{
    public string Name { get; set; } = string.Empty;

    public int Gold = 0;

    public SaveDataV2()
    {
        Version = 2;
    }

    public override SaveDatas VersionUp()
    {
        var saveData = new SaveDataV3();
        saveData.Name = Name;
        saveData.Gold = Gold;

        return saveData;
    }
}

[System.Serializable]
public class SaveDataV3 : SaveDatas
{
    public string Name { get; set; } = string.Empty;

    public int Gold = 0;

    public string ItemId {  get; set; } = string.Empty;

    public List<string> ItemList = new List<string>();

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveDatas VersionUp()
    {
        SaveDataV4 data = new SaveDataV4();
        data.Name = Name;
        data.Gold = Gold;

        foreach (string id in ItemList)
        {
            SaveItemData itemData = new SaveItemData();
            itemData.ItemData = DataTableManager.ItemTable.Get(id);
            data.ItemList.Add(itemData);
        }

        return data;
    }
}

[System.Serializable]
public class SaveDataV4 : SaveDataV2
{
    public List<SaveItemData> ItemList = new List<SaveItemData>();
    public UiInvenSlotList.SortingOtions ItemSorting = UiInvenSlotList.SortingOtions.NameAccending;
    public UiInvenSlotList.FilteringOptions ItemFiltering = UiInvenSlotList.FilteringOptions.None;

    public List<SaveCharacterData> CharacterList = new List<SaveCharacterData>();
    public CharacterUISlotList.SortingOtions CharacterSorting = CharacterUISlotList.SortingOtions.CreationTimeAsscding;
    public CharacterUISlotList.FilteringOptions CharacterFiltering = CharacterUISlotList.FilteringOptions.None;

    public SaveDataV4()
    {
        Version = 4;
    }

    public override SaveDatas VersionUp()
    {
        throw new System.NotImplementedException();
    }
}