using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CargoBaseMovement : MonoBehaviour
{
    [SerializeField] float rotSpeed = 5f;
    [SerializeField] float hoverDistance = 10f;
    [SerializeField] public Transform target;

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
    }
}
