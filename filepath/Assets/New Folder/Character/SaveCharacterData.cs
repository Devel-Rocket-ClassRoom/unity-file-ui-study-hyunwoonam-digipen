using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class SaveCharacterData
{
    public Guid instanceId { get; set; }

    [JsonConverter(typeof(CharacterDataConverter))]
    public CharacterData CharacterData { get; set; }
    public DateTime creationTime { get; set; }

    public static SaveCharacterData GetRandomCharacter()
    {
        SaveCharacterData newCharacter = new SaveCharacterData();
        newCharacter.CharacterData = DataTableManager.CharacterTable.GetRandom();
        return newCharacter;
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
