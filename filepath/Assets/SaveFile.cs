using System.IO;
using TMPro;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI logTextUI;

    void Start()
    {
        string saveDir = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
            PrintLog($"폴더 생성: {saveDir}");
        }
        else
        {
            PrintLog("폴더가 이미 존재합니다.");
        }

        File.WriteAllText(Path.Combine(saveDir, "save1.txt"), "Save 1");
        File.WriteAllText(Path.Combine(saveDir, "save2.txt"), "Save 2");
        File.WriteAllText(Path.Combine(saveDir, "save3.txt"), "Save 3");

        PrintLog("=== 세이브 파일 목록 ===");

        ShowFileList(saveDir);

        FileCopy(saveDir, "save1.txt");
        DeleteFile(saveDir, "save3.txt");

        PrintLog("=== 작업 후 파일 목록 ===");

        ShowFileList(saveDir);
    }
    private void PrintLog(string message)
    {
        Debug.Log(message);

        if (logTextUI != null)
        {
            logTextUI.text += message + "\n";
        }
    }
    public void FileCopy(string path, string file)
    {
        string Rename = Path.GetFileNameWithoutExtension(file);

        Rename = Rename + "_backup.txt";

        string sourceFile = Path.Combine(path, file);
        string backupFile = Path.Combine(path, Rename);

        if (File.Exists(sourceFile))
        {
            File.Copy(sourceFile, backupFile, true);
            PrintLog($"{file} → {Rename} 복사 완료");
        }
    }

    public void DeleteFile(string path, string file)
    {
        string fileToDelete = Path.Combine(path, file);

        if (File.Exists(fileToDelete))
        {
            File.Delete(fileToDelete);
            PrintLog($"{file} 삭제 완료");
        }
    }

    public void ShowFileList(string dir)
    {
        string[] files = Directory.GetFiles(dir);

        foreach (string file in files)
        {
            PrintLog($"파일: {Path.GetFileName(file)}");
        }
    }
}
