using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Vector2 moveInput, turnInput, moveDirection;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotSpeed = 5f;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash;

    [Header("Bullet Settings")]
    [SerializeField] Transform bulletSpawn; // fix this later, maybe place into a separate script, might mess up when it comes to respawning player
    [SerializeField] Bullet bullet;
    [SerializeField] float shootCooldown = 1f;
    private bool canShoot;

    private void Start()
    {
        canDash = true;
        canShoot = true;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        {
            _playerInput.Player.Disable();
        }
    }

    private void FixedUpdate()
    {
        moveInput = _playerInput.Player.Move.ReadValue<Vector2>();
        turnInput = _playerInput.Player.Turn.ReadValue<Vector2>();

        if (_playerInput.Player.Move.IsInProgress() && !isDashing)
        {
            PlayerMove();
        }
        if (_playerInput.Player.Turn.IsInProgress())
        {
            PlayerTurn();
        }
        if (_playerInput.Player.Dash.IsPressed() && canDash)
        {
            StartCoroutine(Dash());
        }
        if (_playerInput.Player.Attack.IsPressed() && canShoot)
        {
            Fire();
            StartCoroutine(FireCooldown());
        }

        moveDirection = new Vector2(turnInput.x, moveInput.y).normalized;

    }

    private void PlayerMove()
    {
        rb.AddForce((transform.up * moveDirection.y) * (moveSpeed * Time.deltaTime)); //move forwards
    }

    private void PlayerTurn()
    {
        rb.AddTorque(-moveDirection.x * rotSpeed * Time.deltaTime);
    }

    private void Fire()
    {
        Bullet firedBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        firedBullet.Project(transform.up);
    }

    private IEnumerator Dash() // kudos to BMo for code
    {
        canDash = false;
        isDashing = true;

        rb.linearVelocity = transform.up * moveDirection.y * dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private IEnumerator FireCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DrivableSpace"))
        {
            Debug.Log("Driving on tile");
        }
    }
}
