using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 3;
    private int currentHearts;

    public Image[] heartImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public float invulnerabilityDuration = 2f;
    public float blinkInterval = 0.1f;
    public float extraSafeTime = 0.25f; // Tempo adicional sem piscar

    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    public AudioClip[] hitSounds;
    private AudioSource audioSource;
    public AudioClip deathSound;

    //public GameObject gameOverPanel;

    void Start()
    {
        currentHearts = maxHearts;
        UpdateHeartsUI();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        currentHearts -= damage;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
        UpdateHeartsUI();

        if (currentHearts <= 0)
        {
            if (deathSound != null)
                audioSource.PlayOneShot(deathSound);

            // Notifica que o player morreu
            GameOverManager.Instance.TriggerGameOver();
            return;
        }

        if (hitSounds.Length > 0)
        {
            int index = Random.Range(0, hitSounds.Length);
            audioSource.PlayOneShot(hitSounds[index]);
        }

        StartCoroutine(InvulnerabilityCoroutine());
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHearts)
                heartImages[i].sprite = fullHeart;
            else
                heartImages[i].sprite = emptyHeart;
        }
    }

    //void GameOver()
    //{
    //    if (deathSound != null)
    //    {
    //        audioSource.PlayOneShot(deathSound);
    //    }

    //    if (gameOverPanel != null)
    //    {
    //        gameOverPanel.SetActive(true);
    //    }

    //    Time.timeScale = 0f;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        float elapsed = 0f;

        while (elapsed < invulnerabilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Pisca
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        spriteRenderer.enabled = true;

        // tempo adicional de invulnerabilidade, sem piscar
        yield return new WaitForSeconds(extraSafeTime);

        isInvulnerable = false;
    }

    //public void RestartGame()
    //{
    //    Time.timeScale = 1f; // volta o tempo ao normal
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
}
