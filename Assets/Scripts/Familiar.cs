using System;
using UnityEngine;

public class Familiar : MonoBehaviour
{
    private float lastFire;
    private GameObject player;
    public FamiliarData familiar;
    private float lastOffsetX;
    private float lastOffsetY;
    private PlayerController playerCtrl;  
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCtrl = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = playerCtrl.facingDirection;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");
        if (Input.GetButton("Fire1") && Time.time - lastFire > familiar.fireDelay)
        {
            Shoot(dir.x, dir.y);   
            lastFire = Time.time;
        }

        if (horizontal != 0 || vertical != 0)
        {
            float offsetX = (horizontal < 0) ? Mathf.Floor(horizontal) : Mathf.Ceil(horizontal);
            float offsetY = (vertical < 0) ? Mathf.Floor(vertical) : Mathf.Ceil(vertical);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, familiar.speed * Time.deltaTime);
            lastOffsetX = offsetX;
            lastOffsetY = offsetY;
        }
        else
        {
            if (!(transform.position.x < lastOffsetX + 0.5f) || !(transform.position.y < lastOffsetY + 0.5f))
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x - lastOffsetX, player.transform.position.y - lastOffsetY), familiar.speed * Time.deltaTime);
            }
        }
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(familiar.bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        float posX = (x < 0) ? Mathf.Floor(x) * familiar.speed : Mathf.Ceil(x) * familiar.speed;
        float posY = (y < 0) ? Mathf.Floor(y) * familiar.speed : Mathf.Ceil(y) * familiar.speed;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(posX, posY);
    }
}
