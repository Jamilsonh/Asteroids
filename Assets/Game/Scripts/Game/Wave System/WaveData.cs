using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    [Header("Quantidade de Asteroides")]
    public int asteroidCount = 5; // Quantos asteroides essa wave terá 

    [Header("Tempo entre spawns")]
    public float spawnInterval = 1f; // Intervalo entre cada asteroide dentro da wave

    [Header("Escala dos Asteroides")]
    public float minScale = 0.8f;
    public float maxScale = 1.5f;

    [Header("Velocidade dos Asteroides Grandes")]
    public float minSpeed = 2f;
    public float maxSpeed = 4f;

    [Header("Velocidade dos Fragmentos (Asteroides Pequenos)")]
    public float minFragmentSpeed = 0.5f;
    public float maxFragmentSpeed = 1.5f;
}
