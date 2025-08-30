using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;

    [Range(0f, 1f)] public float normalVolume = 1f;
    [Range(0f, 1f)] public float lowVolume = 0.3f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.volume = normalVolume;
            audioSource.Play();
        }
    }

    public void SetLowVolume()
    {
        audioSource.volume = lowVolume;
    }

    public void SetNormalVolume()
    {
        audioSource.volume = normalVolume;
    }

    public void RestartMusic()
    {
        audioSource.Stop();
        audioSource.volume = normalVolume;
        audioSource.Play();
    }
}
