using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public float size = 1.0f;
    [SerializeField] public float minSize = 0.5f;
    [SerializeField] public float maxSize = 1.5f;
    [SerializeField] public float speed = 50.0f;
    [SerializeField] public float maxLifetme = 30.0f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        sr.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = new Vector3(1, 1, 0) * size;

        rb.mass = size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * speed);

        Destroy(gameObject, maxLifetme);
    }
}
