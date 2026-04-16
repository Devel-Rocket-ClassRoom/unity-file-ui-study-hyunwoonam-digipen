using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}

[Serializable]
public class ObjectSaveData
{
    public string prefabName;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}

[Serializable]
public class SaveData
{
    public List<SomeClass> spawnedCubesData = new List<SomeClass>(); 
}


public class JsonTest2 : MonoBehaviour
{
    public string fileName = "test.json";
    public string FullFilePath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);

    public string[] prefabNames =
    {
        "Cube",
        "Sphere",
        "Capsule",
        "Cylinder"
    };

    public JsonSerializerSettings jsonSettings;

    private List<GameObject> spawnedCubes = new List<GameObject>();

    private void Awake()
    {
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
    }

    public void Save()
    {
        //SomeClass obj = new SomeClass();
        //
        //obj.pos = targetCube.transform.position;
        //obj.rot = targetCube.transform.rotation;
        //obj.scale = targetCube.transform.localScale;
        //obj.color = targetCube.GetComponent<MeshRenderer>().material.color;
        //var json = JsonConvert.SerializeObject(obj, jsonSettings);
        //
        //File.WriteAllText(FullFilepath, json);
    }

    private void CreateRandomObject()
    {
        var prefabName = prefabNames[UnityEngine.Random.Range(0, prefabNames.Length)];
        var prefab = Resources.Load<JsonTestObject>(prefabName);
        var obj = Instantiate(prefab);

        obj.transform.position = UnityEngine.Random.insideUnitSphere * 10f;
        obj.transform.rotation = UnityEngine.Random.rotation;
        obj.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.5f, 3f);
        obj.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
    }    

    public void OnCreate()
    {
       for ( int i = 0; i < 10; i++)
        {
            CreateRandomObject();
        }
    }

    public void OnClear()
    {
        var objs = GameObject.FindGameObjectsWithTag("TestObject");

        foreach(var obj in objs)
        {
            Destroy(obj);
        }

    }

    public void OnSave()
    {
        var saveList = new List<ObjectSaveData>();

        var objs = GameObject.FindGameObjectsWithTag("TestObject");
        foreach(var obj in objs)
        {
            var jsonTestObj = obj.GetComponent<JsonTestObject>();
            saveList.Add(jsonTestObj.GetSaveData());
        }

        var json = JsonConvert.SerializeObject(saveList, jsonSettings);
        File.WriteAllText(FullFilePath, json);
    }

    public void OnLoad()
    {
        OnClear();

        var json = File.ReadAllText(FullFilePath);
        var saveList = JsonConvert.DeserializeObject<List<ObjectSaveData>>(json, jsonSettings);

        foreach (var saveData in saveList)
        {
            var prefab = Resources.Load<JsonTestObject>(saveData.prefabName);
            var jsonTestObj = Instantiate(prefab);
            jsonTestObj.Set(saveData);
        }
    }

    public void Load()
    {
        //var json = File.ReadAllText(FullFilepath);
        //var obj = JsonConvert.DeserializeObject<SomeClass>(json, jsonSettings);
        //
        //targetCube.transform.position = obj.pos;
        //targetCube.transform.rotation = obj.rot;
        //targetCube.transform.localScale = obj.scale;
        //targetCube.GetComponent<MeshRenderer>().material.color = obj.color;
        //
        //Debug.Log(obj);
    }
}
