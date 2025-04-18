using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static float health = 10;
    private static int maxHealth = 10;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    private bool speedItemCollected = false;
    private bool fireRateItemCollected = false;
    
    public List<string> collectedNames = new List<string>();

    public static float Health
    {
        get => health;
        set => health = value;
    }
    public static int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    public static float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    public static float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }
    public static float BulletSize
    {
        get => bulletSize;
        set => bulletSize = value;
    }

    public TextMeshProUGUI healthText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
    }

    public static void DamagePlayer(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, Health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }
    
    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    public void UpdateCollectedItems(CollectionController item)
    {
        collectedNames.Add(item.item.name);
        foreach (string i in collectedNames)
        {
            switch (i)
            {
                case "Speed":
                    speedItemCollected = true;
                    break;
                case "FireRate":
                    fireRateItemCollected = true;
                    break;
            }
        }

        if (speedItemCollected && fireRateItemCollected)
        {
            FireRateChange(0.25f);
        }
    }
    private static void KillPlayer()
    {
        
    }
}
