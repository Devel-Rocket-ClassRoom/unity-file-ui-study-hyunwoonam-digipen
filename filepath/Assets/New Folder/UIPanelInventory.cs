using UnityEngine;
using TMPro;

public class UIPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInvenSlotList uiInvenSlotList;

    private void OnEnable()
    {
        OnLoad();
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOtions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.ItemSorting = (UiInvenSlotList.SortingOtions)sorting.value;
        SaveLoadManager.Data.ItemFiltering = (UiInvenSlotList.FilteringOptions)filtering.value;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();

        OnChangeFiltering((int)SaveLoadManager.Data.ItemFiltering);
        OnChangeSorting((int)SaveLoadManager.Data.ItemSorting);

        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);

        
    }

    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }

}
