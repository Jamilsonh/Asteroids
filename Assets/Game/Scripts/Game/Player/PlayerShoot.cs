using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint; // Ponto de onde sai a bala
    public float bulletForce = 50f; // For�a do tiro

    public AudioClip[] shootSounds; // Sons do tiro
    private AudioSource audioSource;

    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    // Arroz

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Pega uma bala da pool
        GameObject bullet = BulletPool.Instance.GetBullet(firePoint.position, firePoint.rotation);

        // Adiciona for�a � bala
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.linearVelocity = Vector2.zero; // Garante que come�a parado
        rbBullet.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        // Som do tiro
        if (shootSounds.Length > 0)
        {
            int index = Random.Range(0, shootSounds.Length);
            audioSource.PlayOneShot(shootSounds[index]);
        }
    }
}