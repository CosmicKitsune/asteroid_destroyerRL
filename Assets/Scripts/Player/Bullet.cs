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

    public void Project(Vector2 direction)
    {
        rb.AddForce(direction * speed);
        StartCoroutine(DeleteBullet());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"Colliding with {collision.gameObject}");
        Destroy(this.gameObject);
    }

    private IEnumerator DeleteBullet() // deletes the bullet
    {
        yield return new WaitForSeconds(life_time);
        Destroy(this.gameObject);
    }
}
