using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text finalWaveText;
    public TMP_Text scoreTextUI; // referência ao texto de score que aparece durante o jogo

    private void Awake()
    {
        Instance = this;
    }

    public void TriggerGameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetLowVolume();
        }

        // Desativa o score ativo da tela de jogo
        if (scoreTextUI != null)
            scoreTextUI.gameObject.SetActive(false);

        if (finalScoreText != null)
            finalScoreText.text = "SCORE: " + ScoreManager.Instance.score;

        WaveManager waveManager = Object.FindFirstObjectByType<WaveManager>();
        if (finalWaveText != null && waveManager != null)
            finalWaveText.text = "WAVE: " + waveManager.GetCurrentWave();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.RestartMusic();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
