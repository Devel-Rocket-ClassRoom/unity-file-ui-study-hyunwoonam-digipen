using UnityEngine;
using TMPro;

[ExecuteInEditMode] 
public class LocalizationText : MonoBehaviour
{
#if UNITY_EDITOR
    public Languages editorLang;
    int num = 0;
#endif

    public string id;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        if(Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnChangeLanguage;

            OnChangedId();
        }
#if UNITY_EDITOR
        else
        {
            OnChangeLanguage(editorLang);
            //OnChangedId();
        }
#endif
    }

    [ContextMenu("ChangeLanguage")]
    private void ChangeLanguage()
    {
#if UNITY_EDITOR

       
        
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
        //OnChangedId();
#else
        OnChangeLanguage();
        //OnChangedId();
#endif
    }

    private void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(id);
        Debug.Log(text.text);
    }
    private void OnChangeLanguage()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Languages lang)
    {
        var stringTable = DataTableManager.GetStringTable(lang);

        text.text = stringTable.Get(id);
    }
#endif
}
