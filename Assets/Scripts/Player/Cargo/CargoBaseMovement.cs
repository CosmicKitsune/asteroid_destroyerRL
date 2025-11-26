using NavMeshPlus.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CargoBaseMovement : MonoBehaviour
{
    // main issue, getting negative velocity somehow :/ probably just cap it MinVel and MaxVel

    [SerializeField] float rotSpeed = 5f;
    [SerializeField] public Transform target;
    [SerializeField] public NavMeshSurface surface;

    private Rigidbody2D rb;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void FixedUpdate()
    {
        Vector2 toTarget = ((Vector2)agent.steeringTarget - rb.position).normalized; // get the direction angle based on the steering point

        float angle = Vector2.SignedAngle(transform.right, toTarget); // compare both DIRECTIONS, based on the forward direction of the square and the direction of the target
        float desiredAngle = Mathf.MoveTowardsAngle(rb.rotation, rb.rotation + angle - 90f, rotSpeed * Time.deltaTime); //angle that moves towards the target position

        rb.MoveRotation(desiredAngle);

        float dist = Vector2.Distance(rb.position, target.position);

        if (dist >= 10f) //refresh the navmesh automatically if the agent is too far from the player, alongside updating it every 30 seconds
        {                //this should prevent the cargo ship from just veering away from the player upon collisions
            RefreshNavmesh.Refresh(surface);
        }
    }
}
