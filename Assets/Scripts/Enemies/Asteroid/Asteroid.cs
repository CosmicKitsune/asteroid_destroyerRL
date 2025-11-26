using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public float size = 1.0f;
    [SerializeField] public float minSize = 0.5f;
    [SerializeField] public float maxSize = 1.5f;
    [SerializeField] public float speed = 50.0f;
    [SerializeField] public float orbitSpeed = 1.0f;
    [SerializeField] public float maxLifetme = 30.0f;
    [SerializeField] public int splitCount = 2;

    public bool isBelt = false;
    public GameObject target;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private void Awake()
    {
        orbitSpeed = Random.Range(orbitSpeed, orbitSpeed * 2f);
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

    public void RotateTrajectory(GameObject target)
    {
        transform.RotateAround(target.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if(isBelt)
        {
            RotateTrajectory(target);
        } else
        {
            Debug.Log("Not orbiting");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            if ((size * 0.8f) >= minSize / 2)
            {
                CreateSplit(splitCount);
            }

            Destroy(gameObject);
        }
    }

    private void CreateSplit(int splitCount)
    {
        for (int i = 0; i < splitCount; i++)
        {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle * 0.5f;

            Asteroid half = Instantiate(this, position, Quaternion.identity);
            half.size = size / 2;
            half.SetTrajectory(Random.insideUnitCircle.normalized);
        }
    }
}
