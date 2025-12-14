using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;

public class BurstTurret : Turret
{
    [SerializeField] public float maxBurst;
    [SerializeField] public float burstCooldown;

    private float burstCount;
    private List<Vector2> directions = new List<Vector2>();

    private void Start()
    {
        burstCount = maxBurst;
        CalculateDirections();
    }

    // Update is called once per frame
    protected override void DetectTarget()    
    {
        target = GetClosestTarget();

        if (target != null)
        {
            detected = true;

            if (canShoot && burstCount > 0)
            {
                Fire();
                StartCoroutine(FireCooldown());
            }
        }
        else
        {
            detected = false;
        }
    }

    protected override void Fire()
    {
        foreach (Vector2 d in directions)
        {
            Bullet firedBullet = Instantiate(bullet, transform.position + new Vector3(d.x, d.y, 0), transform.rotation);
            firedBullet.Project(d, 0.5f);
        }
    }

    private void CalculateDirections()
    {
        float angleStep = (2f * Mathf.PI) / 12;

        for (int i = 0; i < 12; i++)
        {
            // get current angle
            float currentAngle = i * angleStep;

            // get position of circle relative to the centre
            float x = Mathf.Cos(currentAngle) * detectRadius;
            float y = Mathf.Sin(currentAngle) * detectRadius;

            // assign the direciton vector and normalise it
            Vector2 pointPosition = new Vector2(x, y);
            Vector2 direction = pointPosition.normalized;

            directions.Add(direction);

            //Debug.Log($"Point {i}: Direction = {direction} at position = {pointPosition}");
        }
    }

    private GameObject GetClosestTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, detectRadius, Vector2.zero); //cast a raycast in all directions

        foreach (RaycastHit2D hit in hits) //check each gameobject with the "Player" tag (cargoship and playership) and determine which object is closer
        {
            if (hit && hit.collider.gameObject.CompareTag("Player"))
            {
                target = hit.collider.gameObject;

                return target;
            } 
        }

        return null;
    }

    private void OnDrawGizmosSelected() // visualise 2D raycast
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    private IEnumerator FireCooldown()
    {
        canShoot = false;
        burstCount--;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;

        if (burstCount <= 0)
        {
            StartCoroutine(BurstCooldown());
        }
    }

    private IEnumerator BurstCooldown()
    {
        yield return new WaitForSeconds(burstCooldown);
        burstCount = maxBurst;
    }
}
