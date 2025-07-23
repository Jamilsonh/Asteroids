using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Shoot
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 50f;

    // Audio
    public AudioClip[] shootSounds; // array com os sons de tiro
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        // Toca som aleatório
        if (shootSounds.Length > 0)
        {
            int index = Random.Range(0, shootSounds.Length);
            audioSource.PlayOneShot(shootSounds[index]);
        }
    }
}
