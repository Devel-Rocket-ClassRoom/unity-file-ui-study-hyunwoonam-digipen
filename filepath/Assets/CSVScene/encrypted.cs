using System.IO;
using TMPro;
using UnityEngine;

public class encrypted : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI logTextUI;
    private const byte XOR_KEY = 0xAB;

    void Start()
    {
        string saveDir = Application.persistentDataPath;

        string originalFile = Path.Combine(saveDir, "secret.txt");
        string encryptedFile = Path.Combine(saveDir, "encrypted.dat");
        string decryptedFile = Path.Combine(saveDir, "decrypted.txt");

        string originalMessage = "Hello Unity World";

        File.WriteAllText(originalFile, originalMessage);

        PrintLog($"원본: {originalMessage}");

        EncryptFile(originalFile, encryptedFile, XOR_KEY);

        DecryptFile(encryptedFile, decryptedFile, XOR_KEY);

        string decryptedMessage = File.ReadAllText(decryptedFile);

        PrintLog($"복호화 결과: {decryptedMessage}");
        PrintLog($"원본과 일치: {originalMessage == decryptedMessage}");
    }

    private void EncryptFile(string inputPath, string outputPath, byte key)
    {
        using (FileStream fsIn = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream fsOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            int data;

            while ((data = fsIn.ReadByte()) != -1)
            {
                byte encryptedByte = (byte)(data ^ key);

                fsOut.WriteByte(encryptedByte);
            }

            PrintLog($"암호화 완료 (파일 크기: {fsOut.Length} bytes)");
        }
    }

    private void DecryptFile(string inputPath, string outputPath, byte key)
    {
        using (FileStream fsIn = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream fsOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            int data;

            while ((data = fsIn.ReadByte()) != -1)
            {
                byte decryptedByte = (byte)(data ^ key);

                fsOut.WriteByte(decryptedByte);
            }

            PrintLog("복호화 완료");
        }
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
