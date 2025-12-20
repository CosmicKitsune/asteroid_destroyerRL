using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 500.0f;
    public float life_time = 10f;
    public float dmg = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction, float modifier)
    {
        rb.AddForce(direction * (speed * modifier));
        StartCoroutine(DeleteBullet());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(1);
            ObjectPooler.EnqueueObject(this, "Bullet");
        } else if (collision.CompareTag("Asteroid"))
        {
            ObjectPooler.EnqueueObject(this, "Bullet");
        }
            
    }

    private IEnumerator DeleteBullet() // deletes the bullet
    {
        yield return new WaitForSeconds(life_time);
        ObjectPooler.EnqueueObject(this, "Bullet");
    }
}
