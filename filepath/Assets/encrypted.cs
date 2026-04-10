using TMPro;
using UnityEngine;

public class encrypted : MonoBehaviour
{
    private TextMeshProUGUI logTextUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PrintLog(string message)
    {
        Debug.Log(message);

        if (logTextUI != null)
        {
            logTextUI.text += message + "\n";
        }
    }
}
