using UnityEngine;

public class DataTbleTest : MonoBehaviour
{
    public string NameStringTablekr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTablejp = "StringTablejp";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.Language = Languages.Korean;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.Language = Languages.English;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.Language = Languages.Japanese;
        }
    }


    public void OnClickStringTableKr()
    {
        Debug.Log(DataTableManager.StringTable.Get("HELLO"));

        //var table = new StringTable();
        //table.Load(NameStringTablekr);
        //Debug.Log(table.Get("HELLO"));
    }

    public void OnClickStringTableEn()
    {
        var table = new StringTable();
        table.Load(NameStringTableEn);
        Debug.Log(table.Get("YOU DIE"));
    }

    public void OnClickStringTableJp()
    {
        var table = new StringTable();
        table.Load(NameStringTablejp);
        Debug.Log(table.Get("BYE"));
    }
}
