using System;
using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataV4();
            //SaveLoadManager.Data.Name = "TEST1234";
            //SaveLoadManager.Data.Gold = 4321;
            ItemData item = new ItemData();

            item.Id = "Item1";

            SaveItemData saveItem = new SaveItemData();
            saveItem.ItemData = item;

            SaveLoadManager.Data.ItemList.Add(saveItem);

            SaveLoadManager.Save();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(SaveLoadManager.Load())
            {
                //Debug.Log(SaveLoadManager.Data.Name);
                //Debug.Log(SaveLoadManager.Data.Gold);

                //foreach (var saveItemData in SaveLoadManager.Data.ItemList)
                //{
                //    Debug.Log(saveItemData.instanceId);
                //    Debug.Log(saveItemData.ItemData.Name);
                //    Debug.Log(saveItemData.creationTime);
                //}

            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
            
        }
    }
}
