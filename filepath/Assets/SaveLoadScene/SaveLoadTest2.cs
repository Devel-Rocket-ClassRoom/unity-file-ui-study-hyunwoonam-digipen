using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class SaveLoadTest2 : MonoBehaviour
{
    List<string> ItemIds;

    void Start()
    {
        ItemTable itemTable = DataTableManager.Get<ItemTable>("ItemTable");
        ItemIds = new List<string>(itemTable.table.Keys);
        Debug.Log($"총 로드된 아이템 수: {ItemIds.Count}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataV3();
            SaveLoadManager.Data.Name = "TEST1234";
            SaveLoadManager.Data.Gold = 4321;
            SaveLoadManager.Data.ItemId = ItemIds[UnityEngine.Random.Range(0, ItemIds.Count)];
            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                Debug.Log(SaveLoadManager.Data.ItemId);
                Debug.Log(DataTableManager.ItemTable.Get(SaveLoadManager.Data.ItemId));
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }

        }
    }
}
