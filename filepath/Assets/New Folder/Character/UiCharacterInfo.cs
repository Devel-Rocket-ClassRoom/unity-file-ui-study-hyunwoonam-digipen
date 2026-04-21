using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";

    public Image imageIcon;
    public TextMeshProUGUI textId;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textAttack;
    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textId.text = string.Empty;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textAttack.text = string.Empty;
    }

    public void SetSaveCharacterData(SaveCharacterData saveCharacterData)
    {
        CharacterData data = saveCharacterData.CharacterData;

        imageIcon.sprite = data.SpriteIcon;
        textId.text =
            string.Format(FormatCommon, DataTableManager.StringTable.Get("ID"), data.Id);
        string id = data.Id.ToString().ToUpper();
        textName.text =
    string.Format(FormatCommon, DataTableManager.StringTable.Get("NAME"), data.StringName);

        textDesc.text =
            string.Format(FormatCommon,
                DataTableManager.StringTable.Get("DESC"), data.StringDesc);
        textAttack.text =
            string.Format(FormatCommon, DataTableManager.StringTable.Get("ATTACK"), data.Attack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
