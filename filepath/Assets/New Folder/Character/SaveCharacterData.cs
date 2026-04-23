using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class SaveCharacterData
{
    public Guid instanceId { get; set; }

    [JsonConverter(typeof(CharacterDataConverter))]
    public CharacterData CharacterData { get; set; }
    public ItemData weapon {  get; set; }
    public ItemData equip { get; set; }
    public DateTime creationTime { get; set; }

    public static SaveCharacterData GetRandomCharacter()
    {
        SaveCharacterData newCharacter = new SaveCharacterData();
        newCharacter.CharacterData = DataTableManager.CharacterTable.GetRandom();
        newCharacter.weapon = randomWepon();
        newCharacter.equip = randomEquip();

        return newCharacter;
    }

    public static ItemData randomWepon()
    {
        var temp = DataTableManager.ItemTable.GetRandom();

        if (temp.Type == ItemTypes.Weapon)
        {
            return temp;
        }

        return null;
    }

    public static ItemData randomEquip()
    {
        var temp = DataTableManager.ItemTable.GetRandom();

        if (temp.Type == ItemTypes.Equip || temp.Type == ItemTypes.Consumable)
        {
            return temp;
        }

        return null;
    }


    public SaveCharacterData()
    {
        instanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{instanceId}\n{creationTime}\n{CharacterData.Id}\n{CharacterData.Name}";
    }
}
