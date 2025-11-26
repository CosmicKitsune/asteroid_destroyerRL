using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;

    [SerializeField] float playRespawnTime;

    public void PlayerDied()
    {
        Invoke(nameof(Respawn), playRespawnTime);
    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }
}
