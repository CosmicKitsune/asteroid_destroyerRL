using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public GameObject cargoShip;
    public Bullet bulletPrefab;

    [SerializeField] float playRespawnTime;

    public void Awake()
    {
        SetupPool();
    }

    private void SetupPool()
    {
        ObjectPooler.SetupPool(bulletPrefab, 10, "Bullet");
    }

    public void PlayerDied()
    {
        Invoke(nameof(Respawn), playRespawnTime);
    }

    private void Respawn()
    {
        player.transform.position = cargoShip.transform.position + new Vector3(10f,0,0);
        player.gameObject.SetActive(true);
        player.hp = 10f;
    }
}
