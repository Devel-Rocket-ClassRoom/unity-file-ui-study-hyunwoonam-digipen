using TMPro;
using UnityEngine;

public class UICharacterPanel : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public CharacterUISlotList uiCharacterSlotList;

    private void OnEnable()
    {
        OnLoad();
    }

    public void OnChangeSorting(int index)
    {
        uiCharacterSlotList.Sorting = (CharacterUISlotList.SortingOtions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiCharacterSlotList.Filtering = (CharacterUISlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.CharacterList = uiCharacterSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.CharacterSorting = (CharacterUISlotList.SortingOtions)sorting.value;
        SaveLoadManager.Data.CharacterFiltering = (CharacterUISlotList.FilteringOptions)filtering.value;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();

        OnChangeFiltering((int)SaveLoadManager.Data.CharacterSorting);
        OnChangeSorting((int)SaveLoadManager.Data.CharacterFiltering);

        uiCharacterSlotList.SetSaveCharacterDataList(SaveLoadManager.Data.CharacterList);


    }

    public void OnCreateCharacter()
    {
        uiCharacterSlotList.AddRandomcharacter();
    }

    public void OnRemoveCharacter()
    {
        uiCharacterSlotList.Removecharacter();
    }
}
