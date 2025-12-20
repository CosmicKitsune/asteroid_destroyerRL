using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public GameObject cargoShip;
    public Bullet bulletPrefab;
    public Bullet enemyPrefab;

    [SerializeField] float playRespawnTime;

    protected override void Awake()
    {
        base.Awake();
        SetupPool();
    }

    private void SetupPool()
    {
        ObjectPooler.SetupPool(bulletPrefab, 10, "Bullet");
        //ObjectPooler.SetupPool(bulletPrefab, 10, "EnemyBullet");
    }

    public void PlayerDied()
    {
        Debug.Log($"Respawning Player");
        Invoke(nameof(Respawn), playRespawnTime);
    }

    private void Respawn()
    {
        player.transform.position = cargoShip.transform.position + new Vector3(10f,0,0);
        player.gameObject.SetActive(true);
        player.hp = 10f;
    }
}
