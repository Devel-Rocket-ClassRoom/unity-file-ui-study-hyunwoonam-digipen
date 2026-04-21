using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterUISlotList : MonoBehaviour
{
    public enum SortingOtions
    {
        CreationTimeAsscding,
        CreationTimeDeccending,
        AttackAsscding,
        AttackDeccending,
    }

    public enum FilteringOptions
    {
        None,
        Sword,
        Shield,
        Bow,
        Heart,
    }

    public readonly System.Comparison<SaveCharacterData>[] comparisons =
    {
        (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs, rhs) => rhs.creationTime.CompareTo(lhs.creationTime),
        (lhs, rhs) => lhs.CharacterData.Attack.CompareTo(rhs.CharacterData.Attack),
        (lhs, rhs) => rhs.CharacterData.Attack.CompareTo(lhs.CharacterData.Attack),
    };

    public readonly System.Func<SaveCharacterData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.CharacterData.Icon == "Icon_Sword01",
        (x) => x.CharacterData.Icon == "Icon_Shield01",
        (x) => x.CharacterData.Icon == "Icon_Bow01",
        (x) => x.CharacterData.Icon == "Icon_Heart01",
    };

    public UICharacterslot prefab;
    public ScrollRect scrollRect;

    private List<UICharacterslot> uiSlotList = new List<UICharacterslot>();

    private List<SaveCharacterData> SaveCharacterDataList = new List<SaveCharacterData>();

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
    public UnityEvent<SaveCharacterData> onSelectSlot;

    public UiCharacterInfo uiCharacterInfo;

    private void OnSelectSlot(SaveCharacterData savecharacterData)
    {
        Debug.Log(savecharacterData);

        uiCharacterInfo.SetSaveCharacterData(savecharacterData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
        uiCharacterInfo.SetEmpty();
    }

    private void OnEnable()
    {
        SetSaveCharacterDataList(SaveLoadManager.Data.CharacterList);
    }

    private void OnDisable()
    {
        SaveLoadManager.Data.CharacterList = SaveCharacterDataList;
        SaveLoadManager.Save();

        SaveCharacterDataList = null;
    }

    public void SetSaveCharacterDataList(List<SaveCharacterData> source)
    {
        SaveCharacterDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveCharacterData> GetSaveItemDataList()
    {
        return SaveCharacterDataList;
    }

    private void UpdateSlots()
    {
        var list = SaveCharacterDataList.Where(filterings[(int)filtering]).ToList();
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
                    onSelectSlot.Invoke(newSlot.SaveCharacterData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; ++i)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetCharacter(list[i]);
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

    public void AddRandomcharacter()
    {
        SaveCharacterDataList.Add(SaveCharacterData.GetRandomCharacter());
        UpdateSlots();
    }

    public void Removecharacter()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        SaveCharacterDataList.Remove(uiSlotList[selectedSlotIndex].SaveCharacterData);
        uiCharacterInfo.SetEmpty();
        UpdateSlots();
    }
}
