using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;

    string[] leftStats;
    string[] rightStats;
    private int totalScore = 0;

    public Button nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);

        MakeScore();
    }

    private void Reset()
    {
        leftStatLabel.text = string.Empty;
        leftStatValue.text = string.Empty;
        rightStatLabel.text = string.Empty;
        rightStatValue.text = string.Empty;
        scoreValue.text = 0.ToString("D9");
    }

    private void Start()
    {
        //StartCoroutine(ShowStats());
    }


    public override void Open()
    {
        base.Open();

        StartCoroutine(ShowStats());
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnNext()
    {
        windowManager.open(0);
    }

    private void MakeScore()
    {
        totalScore = 0;

        leftStats = new string[3];

        for (int i = 0; i < leftStats.Length; i++)
        {
            int temp = Random.Range(0, 1001);
            totalScore += temp;
            leftStats[i] = temp.ToString("D4"); 
        }

        rightStats = new string[3];

        for (int i = 0; i < leftStats.Length; i++)
        {
            int temp = Random.Range(0, 1001);
            totalScore += temp;
            rightStats[i] = temp.ToString("D4");
        }
    }

    IEnumerator ShowStats()
    {
        Reset();

        for (int i = 0; i < 3; i++)
        {
            leftStatLabel.text += $"STAT{i}\n";
            leftStatValue.text += leftStats[i] + "\n";

            rightStatLabel.text += $"STAT{i}\n";
            rightStatValue.text += rightStats[i] + "\n"; 

            yield return new WaitForSeconds(1f);

            if (i == 2)
            {
                StartCoroutine(AnimateScore(totalScore));
            }
        }
    }

    IEnumerator AnimateScore(int totalScore)
    {
        float duration = 5f;
        float elapsed = 0f;

        int startScore = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            int currentScore = (int)Mathf.Lerp(startScore, totalScore, t);

            scoreValue.text = currentScore.ToString("D9");

            yield return null;
        }

        scoreValue.text = totalScore.ToString("D9");
    }
}
