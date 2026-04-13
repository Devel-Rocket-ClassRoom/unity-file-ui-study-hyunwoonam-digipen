using System.Collections.Generic;
using UnityEngine;
using static Variables;

public static class DataTableManager 
{
    private static readonly Dictionary<string, DataTable> tables =
        new Dictionary<string, DataTable>();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);
#if UNITY_EDITOR
    public static StringTable GetStringTable(Languages lang)
    {
        return Get<StringTable>(DataTableIds.StringTableIds[(int)lang]);
    }
#endif
    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
#if !UNITY_EDITOR
        var stringTable = new StringTable();
        stringTable.Load(DatableIds.String);
        tables.Add(DatableIds.String, stringTable);
#else
        foreach ( var id in DataTableIds.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }
#endif
    }

    public static void ChangeLanguage(Languages lang)
    {
        string tableId = DataTableIds.StringTableIds[(int)lang];

        if (tables.ContainsKey(tableId))
            return; 

        string oldId = string.Empty;
        foreach (var id in DataTableIds.StringTableIds)
        {
            if (tables.ContainsKey(id))
            {
                oldId = id;
                break;
            }
        }

        var stringTable = tables[oldId];
        stringTable.Load(DataTableIds.String);
        tables.Remove(oldId);
        tables.Add(DataTableIds.String, stringTable);
    }

    //public static void ChangeLanguage(Languages lang)
    //{
    //    var stringTable = StringTable;
    //    stringTable.Load(DataTableIds.StringTableIds[(int)lang]);
    //}

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("테이블 없음");
            return null;
        }

        return tables[id] as T;
    }
}