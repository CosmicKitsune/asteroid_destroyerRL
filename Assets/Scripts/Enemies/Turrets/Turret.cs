using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] protected float detectRadius;
    [SerializeField] protected float shieldHp;
    [SerializeField] protected float hp;
    [SerializeField] protected float shootCooldown = 2f;
    [SerializeField] protected Bullet bullet;
    protected GameObject target;
    protected bool detected = false;
    protected bool hasShield = true;
    protected bool canShoot = true;


    protected virtual void Update()
    {
        DetectTarget();
    }

    public virtual void TakeDamage(float dmg)
    {
        if (hasShield)
        {
            shieldHp -= dmg;
        } else
        {
            hp -= dmg;
        }

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    protected abstract void DetectTarget();
    protected abstract void Fire();
}
