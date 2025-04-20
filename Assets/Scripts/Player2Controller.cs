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
    public Sprite forwardSprite;    
    public Sprite backSprite; 
    public Sprite sideSprite;       
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Collider2D myCol = GetComponent<Collider2D>();  
        foreach (var other in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (other == gameObject) continue;
            Collider2D otherCol = other.GetComponent<Collider2D>();
            if (otherCol != null)
                Physics2D.IgnoreCollision(myCol, otherCol, true);
        }
    }

    void Update()
    {
        rb.linearVelocity = moveInput * speed;
        UpdateSprite(moveInput);
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
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, angle - 90f);
        var bullet = Instantiate(bulletPrefab, transform.position, rot);
        var brb = bullet.AddComponent<Rigidbody2D>();
        brb.gravityScale = 0;
        brb.linearVelocity = facingDirection * bulletSpeed;
    }
    
    private void UpdateSprite(Vector2 input)
    {
        if (input.sqrMagnitude < 0.01f) return;
        if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
        {
            if (input.y > 0)
                spriteRenderer.sprite = backSprite;
            else
                spriteRenderer.sprite = forwardSprite;

            spriteRenderer.flipX = false; 
        }
        else
        {
            spriteRenderer.sprite = sideSprite;
            spriteRenderer.flipX = (input.x > 0);
        }
    }
}
