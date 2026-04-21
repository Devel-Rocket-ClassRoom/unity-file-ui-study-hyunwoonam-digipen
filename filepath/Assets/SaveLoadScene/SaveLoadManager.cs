using UnityEngine;
using SaveDataVC = SaveDataV4;
using Newtonsoft.Json;
using System.IO;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text,
        Encrypted,
    }

    public static SaveMode Mode { get; set; } = SaveMode.Text;

    public static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";

    private static readonly string[] SaveFileName =
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3",
    };

    private static string GetSaveFilePath(int slot)
    {
        return GetSaveFilePath(slot, Mode);
    }

    public static string GetSaveFilePath(int slot, SaveMode mode)
    {
        string ext = mode == SaveMode.Text ? ".json" : ".dat";
        
        return Path.Combine(SaveDirectory, $"{SaveFileName[slot]}{ext}");
    }
    public static int SaveDataVersion { get; } = 4;
    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    static SaveLoadManager()
    {
        if (!Load())
        {
            
            Debug.LogError("세이브 파일 로드 실패");
        }
    }

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    public static bool Save(int slot = 0)
    {
        return Save(Mode, slot);
    }


    public static bool Save(SaveMode mode, int slot = 0)
    {
        if(Data == null || slot < 0 || slot >= SaveFileName.Length)
        {
            return false;
        }

        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            var json = JsonConvert.SerializeObject(Data, settings);
            string path = GetSaveFilePath(0, mode);
               
            switch(mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    break;
            }

            return true;
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }

    public static bool Load(int slot = 0)
    {
        return Load(Mode, slot);
    }

    public static bool Load(SaveMode mode, int slot = 0)
    {
        if (slot < 0 || slot >= SaveFileName.Length)
        {
            return false;
        }
        string path = GetSaveFilePath(0, mode);

        if (!File.Exists(path))
        {
            return Save();
        }

        try
        {
            string json = string.Empty;

            switch(mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    break;
            }

            var saveData = JsonConvert.DeserializeObject<SaveDatas>(json, settings);

            while(saveData.Version < SaveDataVersion)
            {
                Debug.Log(saveData.Version);
                saveData = saveData.VersionUp();
                Debug.Log(saveData.Version);
            }

            Data = saveData as SaveDataVC;

            return true;
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }
    }
}
