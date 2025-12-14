using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 500.0f;
    public float life_time = 10f;

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
        if (this.gameObject.CompareTag("Bullet") && collision.gameObject.CompareTag("EnemyBullet") || this.gameObject.CompareTag("EnemyBullet") && collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag(this.gameObject.tag))
        {
            return;
        } else
        {
            Destroy(this.gameObject);
        }
            
    }

    private IEnumerator DeleteBullet() // deletes the bullet
    {
        yield return new WaitForSeconds(life_time);
        Destroy(this.gameObject);
    }
}
