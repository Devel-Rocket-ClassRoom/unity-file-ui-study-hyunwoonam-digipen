using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterTableTest1 : MonoBehaviour
{
    //public string CharacterId;

    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;
    public LocalizationText info;

    private int num = 1;

    public void OnEnable()
    {
        OnChangeCharacterId();
    }

    public void OnValidate()
    {
        OnChangeCharacterId();
    }

    public void OnChangeCharacterId()
    {
        string key = "Character" + num.ToString();
        
        CharacterData data = DataTableManager.CharacterTable.Get(key);
        if (data != null)
        {
            icon.sprite = data.SpriteIcon;
            textName.id = data.Name;
            textDesc.id = data.Desc;
            info.id = data.Attack.ToString();

            Debug.Log(key);
            textName.OnChangedId();
            textDesc.OnChangedId();
            info.OnChangedId();
        }
    }

    public void OnClick()
    {
        num++;
        if (num >= 5)
        {
            num = 1;
        }
        OnChangeCharacterId();
    }
}
