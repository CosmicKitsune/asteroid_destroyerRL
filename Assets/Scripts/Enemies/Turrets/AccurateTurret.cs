using UnityEngine;

public class AccurateTurret : Turret
{
    Vector2 direction;

    // Update is called once per frame
    protected override void DetectTarget()
    {
        //Vector2 targetPos = target.position;

        //direction = targetPos - (Vector2)transform.position;

        target = GetClosestTarget();
        if (target != null)
        {
            Debug.Log($"Object hit {target.name} at {target.transform.position}");
        }
        else
        {
            Debug.Log($"No current closest target");
        }

        //RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, , detectRadius); //direction
    }

    protected override void Fire()
    {

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
}
