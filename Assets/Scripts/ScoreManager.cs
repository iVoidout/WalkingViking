using UnityEngine;
using UnityEngine.UI;
using System.Collections; // This is needed for coroutines!
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int currentScore = 0;
    private int killCount = 0;
    public Color flashColor = Color.yellow;
    public float flashDuration = 0.2f;
    private Color originalColor;
    public TMP_Text scoreText;
    public TMP_Text killText;

    void Awake()
    {
        currentScore = 0;
        killCount = 0;

        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    void Start()
    {
        

        if (scoreText != null)
        {
            originalColor = scoreText.color;
        }
        UpdateUI();
        // REMOVED the flash from Start - we only want it when adding score
    }

    public void AddScore(int points)
    {
        currentScore += points;
        killCount++;
        UpdateUI();

        // Flash effect when score is added
        if (scoreText != null)
        {
            StartCoroutine(FlashText());
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
        if (killText != null)
        {
            killText.text = "Kills: " + killCount.ToString();
        }
    }

    public int GetScore() { return currentScore; }
    public int GetKills() { return killCount; }

    public void ResetScore()
    {
        currentScore = 0;
        killCount = 0;
        UpdateUI();
    }

    System.Collections.IEnumerator FlashText()
    {
        scoreText.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        scoreText.color = originalColor;
    }
}