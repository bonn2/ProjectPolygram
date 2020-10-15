using UnityEngine;

public class TurretAI : EntityClass
{

    // Turret object information
    private Rigidbody2D turret;

    // Default weapon variables
    protected float thrust = 1.0f;
    protected float fireRate = 0.5f;
    protected float nextFire = -1f;
    protected float bulletForce = 20f;
    protected int offset = 0;

    // Base weapon objects
    public Transform firePoint;
    public GameObject bulletPrefab;
    public ParticleSystem hitEffect;
    
    void Start()
    {
        turret = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Grab target variable
        TriangleAI target = TriangleAI.FindNearest(transform.position);

        // Rotate turret towards target
        Vector2 lookDirection = target.position - turret.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        turret.rotation = angle;

        // Shoot bullet
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
            return;
        }

        // Call shoot function, update cooldown
        Shoot();
        Cooldown();
    }

    // Creates bullet object
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.Rotate(0, 0, offset);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(rotate(firePoint.up, Random.Range(-0.1f, 0.1f)) * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 1f);
    }

    // Bullet cooldown when holding
    void Cooldown()
    {
        nextFire = fireRate;
    }

    // Rotation function
    private static Vector2 rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

}