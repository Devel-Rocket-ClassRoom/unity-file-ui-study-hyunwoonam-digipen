using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
public class CSVData
{
    public string Id {  get; set; }
    public string String { get; set; }
}

public class CSVTest : MonoBehaviour
{
    //public TextAsset textAsset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            TextAsset textAsset = Resources.Load<TextAsset>("DataTables/StringTableKr");
            string csv = textAsset.text;

            using (var reader = new StringReader(csv))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<CSVData>();
                foreach (var record in records) 
                {
                    Debug.Log($"{record.Id} : {record.String}");
                }
            }
        }
    }
}
