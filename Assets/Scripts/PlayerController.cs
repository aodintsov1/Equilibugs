using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);
        if (movement.sqrMagnitude > 0.01f)
        {
            facingDirection = movement.normalized;
        }
        
        if (Input.GetButton("Fire1") && Time.time - lastFire > fireDelay)
        {
            Shoot(facingDirection);
            lastFire = Time.time;
        }
        rb.linearVelocity = movement * speed;
        collectedText.text = "Items collected: " + collectedAmount;
    }

    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.AddComponent<Rigidbody2D>();
        bulletRb.gravityScale = 0;
        bulletRb.linearVelocity = direction * bulletSpeed;
    }
}
