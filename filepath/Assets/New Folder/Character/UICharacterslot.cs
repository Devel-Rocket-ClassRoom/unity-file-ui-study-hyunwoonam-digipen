using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterslot : MonoBehaviour
{
    public int slotIndex = -1;

    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveCharacterData SaveCharacterData { get; private set; }

    public Button button;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        SaveCharacterData = null;
    }

    public void SetCharacter(SaveCharacterData data)
    {
        SaveCharacterData = data;
        imageIcon.sprite = SaveCharacterData.CharacterData.SpriteIcon;
        textName.text = SaveCharacterData.CharacterData.Name;
    }
}
