using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Configure��o de Waves")]
    public WaveData[] waves; // lista de waves (definidas no inspector com ScriptableObjects)
    public AsteroidSpawnerWaves spawner; // referencia ao spawner
    public float timeBetweenWaves = 3f; // tempo de espera entre waves

    private int currentWaveIndex = 0;
    private int activeAsteroids = 0; // contador de asteroides ativos

    void Start()
    {
        StartCoroutine(StartWaveRoutine());
    }

    IEnumerator StartWaveRoutine()
    {
        while (currentWaveIndex < waves.Length)
        {
            WaveData wave = waves[currentWaveIndex];
            Debug.Log("Iniciando Wave " + (currentWaveIndex + 1));

            // Spawna todos os asteroides dessa wave
            for (int i = 0; i < wave.asteroidCount; i++)
            {
                spawner.SpawnAsteroid(this, wave);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            // Espera at� todos os asteroides morrerem
            while (activeAsteroids > 0)
                yield return null;

            // Contagem regressiva antes da pr�xima wave
            float countdown = timeBetweenWaves;
            while (countdown > 0)
            {
                Debug.Log($"Pr�xima wave em {Mathf.Ceil(countdown)}...");
                yield return new WaitForSeconds(1f);
                countdown -= 1f;
            }

            currentWaveIndex++;
        }

        Debug.Log("Todas as waves foram conclu�das!");
    }

    // =====================
    // Controle de asteroides
    // =====================
    public void RegisterAsteroid()
    {
        activeAsteroids++;
    }

    public void UnregisterAsteroid()
    {
        activeAsteroids--;
    }

    public int GetCurrentWave()
    {
        return currentWaveIndex + 1;
    }
}
