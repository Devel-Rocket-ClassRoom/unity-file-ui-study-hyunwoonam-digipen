using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[ExecuteInEditMode]
public class LocallizationDropDown : MonoBehaviour
{
#if UNITY_EDITOR
    public Languages editorLang;
#endif

    public string[] ids;
    public TMP_Dropdown dropdown;

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnChangeLanguage;

            OnChangeLanguage();
        }
#if UNITY_EDITOR
        else
        {
            OnChangeLanguage(editorLang);
        }
#endif
    }

    [ContextMenu("ChangeLanguage")]
    private void ChangeLanguage()
    {
#if UNITY_EDITOR
        LocallizationDropDown[] allTexts = FindObjectsByType<LocallizationDropDown>(FindObjectsSortMode.None);

        foreach (LocallizationDropDown loc in allTexts)
        {
            loc.editorLang = this.editorLang;

            loc.OnChangeLanguage(this.editorLang);
        }

#endif
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged -= OnChangeLanguage;
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(editorLang);
#endif
    }

    private void OnChangeLanguage()
    {
        Apply(DataTableManager.StringTable);
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Languages lang)
    {
        Apply(DataTableManager.GetStringTable(lang));
    }
#endif

    private void Apply(StringTable table)
    {
        if (dropdown == null || ids == null)
            return;

        int prevValue = dropdown.value;
        dropdown.ClearOptions();

        var options = new List<TMP_Dropdown.OptionData>(ids.Length);
        for(int i = 0; i < ids.Length; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(table.Get(ids[i])));
        }
        dropdown.AddOptions(options);

        if(ids.Length > 0)
        {
            dropdown.value = Mathf.Clamp(prevValue, 0, ids.Length - 1);
        }
        dropdown.RefreshShownValue();
    }
}
