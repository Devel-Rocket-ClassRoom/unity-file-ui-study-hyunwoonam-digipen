using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class DifficultyInfo
{
    public int difficulty;
}

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;

    public int selected;

    public int cancel;

    private void Awake()
    {
        string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "Difficulty"
            );

        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }

        string path = Path.Combine(
            pathFolder,
            "Difficulty.json"
        );


        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            DifficultyInfo loadedObj = JsonUtility.FromJson<DifficultyInfo>(json);

            if (loadedObj != null)
            {
                Debug.Log($"불러오기 성공! 난이도: {loadedObj.difficulty}");
                selected = loadedObj.difficulty;
            }
            else
            {
                Debug.LogWarning("JSON 데이터를 파싱할 수 없습니다. 기본값을 반환합니다.");
                selected = 0;
            }
        }
        else
        {
            Debug.Log("저장된 난이도 파일이 없습니다. 기본 난이도를 적용합니다.");
            selected = 0;
        }       

        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
    }
    public override void Open()
    {
        base.Open();
        toggles[selected].isOn = true;

        cancel = selected;
    }

    public override void Close()
    {
        base.Close(); 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEasy(bool active)
    {
        if (active)
        {
            selected = 0;

            Debug.Log("OnEasy");
        }
    }

    public void OnNormal(bool active)
    {
        if (active)
        {
            selected = 1;

            Debug.Log("OnNormal");
        }
    }

    public void OnHard(bool active)
    {
        if (active)
        {
            selected = 2;

            Debug.Log("OnHard");
        }
    }

    public void OnCancel()
    {
        base.Close();

        selected = cancel;

        windowManager.open(0);
    }

    public void OnApplay()
    {
        base.Close();

        DifficultyInfo obj = new DifficultyInfo()
        {
            difficulty = selected,
        };

        string pathFolder = Path.Combine(
            Application.persistentDataPath,
            "Difficulty"
        );

        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }

        string path = Path.Combine(
            pathFolder,
            "Difficulty.json"
        );

        string json = JsonUtility.ToJson(obj, prettyPrint: true);
        File.WriteAllText(path, json);

        windowManager.open(0);
    }
}
