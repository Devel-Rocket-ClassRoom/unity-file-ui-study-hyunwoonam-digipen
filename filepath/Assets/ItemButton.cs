using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{
    public Image iconImage;          
    public TextMeshProUGUI nameText; 

    private ItemData itemData;
    private System.Action<ItemData> onClickAction;
    public void SetItem(ItemData data, System.Action<ItemData> callback)
    {
        itemData = data;
        onClickAction = callback;

        if (itemData != null)
        {
            nameText.text = itemData.StringName;

            iconImage.sprite = itemData.SpriteIcon;
        }
    }

    public void OnClick()
    {
        if (itemData != null)
        {
            onClickAction?.Invoke(itemData);
        }
    }
}