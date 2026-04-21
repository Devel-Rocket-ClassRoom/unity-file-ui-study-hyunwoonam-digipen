using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
//1.CSV파일 (ID / 이름 / 설명 / 공격력..../ 초상화 or 아이콘...)
//2. DataTable 상속
//3. DataTableManager 등록
//4. 테스트 패널

public class CharacterData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Attack { get; set; }
    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString()
    {
        return $"{Id} / {Name} / {Desc} / {Attack} / {Icon}";
    }
}

public class CharacterTable : DataTable
{
    private readonly Dictionary<string, CharacterData> table = new Dictionary<string, CharacterData>();

    private List<string> keyList;

    public override void Load(string filename)
    {
        table.Clear();

        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCSV<CharacterData>(textAsset.text);

        foreach (var character in list)
        {
            if (!table.ContainsKey(character.Id))
            {
                table.Add(character.Id, character);
            }
            else
            {
                Debug.LogError("캐릭터 아이디 중복");
            }
        }

        keyList = table.Keys.ToList();
    }

    public CharacterData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError("캐릭터 아이디 없음");
            return null;
        }

        return table[id];
    }

    public CharacterData GetRandom()
    {
        return Get(keyList[Random.Range(0, keyList.Count)]);
    }
}
