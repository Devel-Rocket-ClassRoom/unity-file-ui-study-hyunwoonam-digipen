using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemTableTest2 : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;

    private string id;

    public void OnEnable()
    {
        SetEmpty();
    }
    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.id = string.Empty;
        textName.OnChangedId();
        textDesc.OnChangedId();

    }
    public void SetItemData(string itemId)
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        SetItemData(data);
    }

    public void SetItemData(ItemData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;
        id = data.Id;

        textName.OnChangedId();
        textDesc.OnChangedId();
    }
    public void OnClick()
    {
        Debug.Log(DataTableManager.ItemTable.Get(id));
    }
}
