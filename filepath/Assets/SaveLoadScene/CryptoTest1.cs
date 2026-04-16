using UnityEngine;

public class CryptoTest1 : MonoBehaviour
{
    private byte[] encrypted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            string plainText = "Hello~ AES!";
            encrypted = CryptoUtil.Encrypt(plainText);
            Debug.Log(System.BitConverter.ToString(encrypted));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(encrypted != null)
            {
                string plainText = CryptoUtil.Decrypt(encrypted);
                Debug.Log(plainText); 
            }

        }
    }
}
