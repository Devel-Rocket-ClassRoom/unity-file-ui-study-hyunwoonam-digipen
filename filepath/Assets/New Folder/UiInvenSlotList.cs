using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiInvenSlotList : MonoBehaviour
{
    public enum SortingOtions
    {
        CreationTimeAsscding,
        CreationTimeDeccending,
        NameAccending, 
        NameDeaccending,
        costAccending,
        costDeanneg
    }

    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable,
        NonConsumable,
    }

    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs, rhs) => rhs.creationTime.CompareTo(lhs.creationTime),
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
        (lhs, rhs) => lhs.ItemData.Cost.CompareTo(rhs.ItemData.Cost),
        (lhs, rhs) => rhs.ItemData.Cost.CompareTo(lhs.ItemData.Cost),
    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemTypes.Weapon,
        (x) => x.ItemData.Type == ItemTypes.Equip,
        (x) => x.ItemData.Type == ItemTypes.Consumable,
        (x) => x.ItemData.Type != ItemTypes.Consumable,
    };

    public UIInvenslot prefab;
    public ScrollRect scrollRect;

    private List<UIInvenslot> uiSlotList = new List<UIInvenslot>();

    private List<SaveItemData> saveItemDataList = new List<SaveItemData>();

    private SortingOtions sorting = SortingOtions.CreationTimeAsscding;
    private FilteringOptions filtering = FilteringOptions.None;

    public SortingOtions Sorting
    {
        get => sorting;
        set
        {
            sorting = value;
            UpdateSlots();
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;

    public UiItemInfo uiItemInfo;

    private void OnSelectSlot(SaveItemData saveItemData)
    {
        Debug.Log(saveItemData);

        uiItemInfo.SetSaveItemData(saveItemData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
        uiItemInfo.SetEmpty();
    }

    private void OnEnable()
    {
        SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }

    private void OnDisable()
    {
        SaveLoadManager.Data.ItemList = saveItemDataList;
        SaveLoadManager.Save();

        saveItemDataList = null;
    }

    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }

    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.SaveItemData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; ++i)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if(selectedSlotIndex == -1)
        {
            return;
        }

        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].SaveItemData);
        uiItemInfo.SetEmpty();
        UpdateSlots();
    }

    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        //for(int i = 0; i < 10; ++i)
    //        //{
    //        //    var saveItemData = SaveItemData.GetRandomItem();
    //        //    var newInven = Instantiate(prefab, scrollRect.content);
    //        //    newInven.SetItem(saveItemData);
    //        //}
    //
    //        //for (int i = 0; i < 10; ++i)
    //        //{
    //        //    saveItemDataList.Add(SaveItemData.GetRandomItem());
    //        //}
    //        //
    //        //UpdateSlots(saveItemDataList);
    //
    //        AddRandomItem();
    //    }
    //
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        //Filtering = (FilteringOptions)(((int)Filtering + 1) % 4);
    //        RemoveItem();
    //        Debug.Log("2번동작");
    //    }
    //
    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        //Sorting = (SortingOtions)(((int)Sorting + 1) % 4);
    //
    //    }
    //}


}
