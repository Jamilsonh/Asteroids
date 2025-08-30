using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public TMP_Text scoreLabelText;
    public TMP_Text scoreValueText;

    void Awake()
    {
        // Garante que s� exista uma inst�ncia
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreLabelText != null)
        {
            scoreLabelText.text = "SCORE";
        }

        if (scoreValueText != null)
        {
            scoreValueText.text = score.ToString();
        }
    }
}
