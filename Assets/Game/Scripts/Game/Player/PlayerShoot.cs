using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;       // Ponto de onde sai a bala
    public float bulletForce = 50f;   // Força do tiro

    public AudioClip[] shootSounds;   // Sons do tiro
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Pega uma bala da pool
        GameObject bullet = BulletPool.Instance.GetBullet(firePoint.position, firePoint.rotation);

        // Adiciona força à bala
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.linearVelocity = Vector2.zero; // Garante que começa parado
        rbBullet.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        // Som do tiro
        if (shootSounds.Length > 0)
        {
            int index = Random.Range(0, shootSounds.Length);
            audioSource.PlayOneShot(shootSounds[index]);
        }
    }
}
