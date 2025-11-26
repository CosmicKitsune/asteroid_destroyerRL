using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public GameObject cargoShip;


    [SerializeField] float playRespawnTime;

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
