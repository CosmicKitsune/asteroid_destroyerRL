using System.Collections;
using UnityEngine;

public class SmartTurret : Turret
{
    private Vector2 direction;
    private Vector2 lastDirection;

    // Update is called once per frame
    protected override void DetectTarget()    
    {
        target = GetClosestTarget();

        if (target != null)
        {
            detected = true;

            Vector2 targetPos = target.transform.position;

            direction = targetPos - (Vector2)transform.position;

            if (canShoot)
            {
                Fire();
                StartCoroutine(FireCooldown());
            }
        }
        else
        {
            detected = false;
        }

        if (detected)
        {
            transform.up = direction;
        } else
        {
            transform.up = lastDirection;
        }

        lastDirection = direction;
    }

    protected override void Fire()
    {
        Bullet firedBullet = Instantiate(bullet, transform.position, transform.rotation);
        firedBullet.Project(direction, 0.8f);
    }

    private GameObject GetClosestTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, detectRadius, Vector2.zero); //cast a raycast in all directions

        float shortestDistance = Mathf.Infinity; //default shortest distance

        GameObject closest = null;

        foreach (RaycastHit2D hit in hits) //check each gameobject with the "Player" tag (cargoship and playership) and determine which object is closer
        {
            if (hit && hit.collider.gameObject.CompareTag("Player"))
            {
                target = hit.collider.gameObject;
                float dist = Vector2.Distance(transform.position, target.transform.position);

                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    closest = hit.collider.gameObject;
                }
            } 
        }

        return closest;
    }

    private void OnDrawGizmosSelected() // visualise 2D raycast
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
    private IEnumerator FireCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}
