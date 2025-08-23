using UnityEngine;

public class AsteroidTrackerWave : MonoBehaviour
{
    private WaveManager manager;

    public void Setup(WaveManager wm)
    {
        manager = wm;
    }

    void OnDestroy()
    {
        if (manager != null)
            manager.UnregisterAsteroid();
    }
}
