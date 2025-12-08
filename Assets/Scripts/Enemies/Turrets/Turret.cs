using Unity.Cinemachine;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [SerializeField] protected float detectRadius;
    [SerializeField] protected float shootCooldown = 2f;
    [SerializeField] protected Bullet bullet;
    protected GameObject target;
    protected bool detected = false;
    protected bool canShoot = true;

    protected virtual void Update()
    {
        DetectTarget();
    }

    protected abstract void DetectTarget();
    protected abstract void Fire();
}
