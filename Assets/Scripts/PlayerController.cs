using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public TextMeshProUGUI collectedText;
    public static int collectedAmount = 0;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    public Vector2 facingDirection = Vector2.right;
    private Vector2 moveInput;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction fireAction;
    public Sprite forwardSprite;    
    public Sprite backSprite; 
    public Sprite sideSprite;       
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        fireAction = playerInput.actions["Fire"];
    }

    void OnEnable()
    {
        moveAction.Enable();
        fireAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        fireAction.Disable();
    }

    void Update()
    {
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.sqrMagnitude > 0.01f)
        {
            facingDirection = moveInput.normalized;
        }

        rb.linearVelocity = moveInput * speed;
        UpdateSprite(moveInput);
        collectedText.text = "Items collected: " + collectedAmount;

        if (fireAction.triggered && Time.time - lastFire > fireDelay)
        {
            Shoot(facingDirection);
            lastFire = Time.time;
        }
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
    
    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.AddComponent<Rigidbody2D>();
        bulletRb.gravityScale = 0;
        bulletRb.linearVelocity = direction * bulletSpeed;
    }
}