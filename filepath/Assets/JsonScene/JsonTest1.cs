using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;
    //[JsonConverter(typeof(Vector3Converter))]
    public Vector3 position;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health} / {position}";
    }
}

public class JsonTest1 : MonoBehaviour
{
    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            //save
            PlayerState obj = new PlayerState()
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)
            };

            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTest2"
            );

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(
                pathFolder,
                "player2.json"
            );

            //string json = JsonConvert.SerializeObject(obj, Formatting.Indented, new Vector3Converter());
            //string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            string json = JsonConvert.SerializeObject(obj, jsonSetting);

            File.WriteAllText(path, json);

            Debug.Log(path);
            Debug.Log(json);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            //load

            string path = Path.Combine(
                Application.persistentDataPath,
                "JsonTest2",
                "player2.json"
            );

            string json = File.ReadAllText(path);
            //PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(json, new Vector3Converter());
            //PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(json);
            PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(json, jsonSetting);

            Debug.Log(json);
            Debug.Log($"{obj.playerName} / {obj.health}");
        }
    }
}
