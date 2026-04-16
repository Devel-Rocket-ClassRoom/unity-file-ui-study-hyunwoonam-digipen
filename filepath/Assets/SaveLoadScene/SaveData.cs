using UnityEngine;
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

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveDatas VersionUp()
    {
        throw new System.NotImplementedException();
    }
}