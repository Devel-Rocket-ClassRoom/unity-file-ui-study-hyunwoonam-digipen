using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI logTextUI;

    private string configFilePath;

    void Start()
    {
        configFilePath = Path.Combine(Application.persistentDataPath, "settings.cfg");

        CreateInitialConfigFile();

        Dictionary<string, string> settings = LoadConfig();

        if (settings != null)
        {
            PrintLog($"설정 로드 완료 (항목 {settings.Count}개)");

            PrintLog("--- 변경 전 ---");
            PrintLog($"bgm_volume = {settings["bgm_volume"]}");
            PrintLog($"language = {settings["language"]}");

            settings["bgm_volume"] = "50";
            settings["language"] = "en";

            PrintLog("--- 변경 후 저장 ---");
            PrintLog($"bgm_volume = {settings["bgm_volume"]}");
            PrintLog($"language = {settings["language"]}");

            SaveConfig(settings);

            PrintLog("--- 최종 파일 내용 ---");
            PrintLog(File.ReadAllText(configFilePath));
        }
    }

    private void PrintLog(string message)
    {
        Debug.Log(message);

        if (logTextUI != null)
        {
            logTextUI.text += message + "\n";
        }
    }

    private void CreateInitialConfigFile()
    {
        string[] initialData = new string[]
        {
            "master_volume=80",
            "bgm_volume=70",
            "sfx_volume=90",
            "language=kr",
            "show_damage=true"
        };

        File.WriteAllLines(configFilePath, initialData);
    }

    private Dictionary<string, string> LoadConfig()
    {
        if (!File.Exists(configFilePath))
        {
            return null;
        }

        Dictionary<string, string> configDict = new Dictionary<string, string>();

        using (StreamReader reader = new StreamReader(configFilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split('=');

                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    configDict[key] = value;
                }
            }
        }

        return configDict;
    }

    private void SaveConfig(Dictionary<string, string> configDict)
    {
        using (StreamWriter writer = new StreamWriter(configFilePath, false))
        {
            foreach (KeyValuePair<string, string> kvp in configDict)
            {
                writer.WriteLine($"{kvp.Key}={kvp.Value}");
            }
        }
    }
}
