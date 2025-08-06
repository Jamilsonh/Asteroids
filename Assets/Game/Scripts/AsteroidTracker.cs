using UnityEngine;

public class AsteroidTracker : MonoBehaviour
{
    private MainMenuAsteroidSpawner spawner;

    public void Setup(MainMenuAsteroidSpawner s)
    {
        spawner = s;
    }

    void OnDestroy()
    {
        if (spawner != null)
            spawner.AsteroidDestroyed();
    }
}
