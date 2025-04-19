using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player2Controller : MonoBehaviour
{
    private PlayerInputActions inputActions;

    [Header("Stats")]
    public float speed;
    public float fireDelay;
    public float bulletSpeed;
    private float lastFire;

    [Header("References")]
    public Rigidbody2D rb;
    public TextMeshProUGUI collectedText;
    public GameObject bulletPrefab;
    
    private Vector2 moveInput = Vector2.zero;
    public Vector2 facingDirection = Vector2.right;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player2.Move.performed   += OnMovePerformed;
        inputActions.Player2.Move.canceled    += OnMoveCanceled;
        inputActions.Player2.Fire.performed   += OnFirePerformed;
    }

    void OnEnable()
    {
        inputActions.Player2.Enable();
    }

    void OnDisable()
    {
        inputActions.Player2.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = moveInput * speed;
        collectedText.text = "Items collected: " + PlayerController.collectedAmount;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        if (moveInput.sqrMagnitude > 0.01f)
            facingDirection = moveInput.normalized;
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    private void OnFirePerformed(InputAction.CallbackContext ctx)
    {
        if (Time.time - lastFire > fireDelay)
        {
            Shoot();
            lastFire = Time.time;
        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var brb = bullet.AddComponent<Rigidbody2D>();
        brb.gravityScale = 0;
        brb.linearVelocity = facingDirection * bulletSpeed;
    }
}
