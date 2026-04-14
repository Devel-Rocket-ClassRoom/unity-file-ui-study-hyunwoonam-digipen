using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUIController : MonoBehaviour
{
    public Image centerIcon;
    public TextMeshProUGUI centerNameText;
    public TextMeshProUGUI centerFullInfoText;

    public ItemButton[] itemButtons; 

    void Start()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            string itemId = $"Item{i + 1}"; 
            ItemData data = DataTableManager.ItemTable.Get(itemId);

            if (data != null)
            {
                itemButtons[i].SetItem(data, ShowItemDetail);
            }
        }

        Variables.OnLanguageChanged += RefreshUI;
    }

    private void OnDestroy()
    {
        Variables.OnLanguageChanged -= RefreshUI;
    }

    private void ShowItemDetail(ItemData data)
    {
        if (data == null) return;

        centerIcon.sprite = data.SpriteIcon;
        centerNameText.text = data.StringName;
        centerFullInfoText.text = data.ToString();
    }

    private void RefreshUI()
    {

        foreach (var btn in itemButtons)
        {

            btn.nameText.text = DataTableManager.StringTable.Get(DataTableManager.ItemTable.Get(btn.nameText.transform.parent.name).Name);
        }
    }
}