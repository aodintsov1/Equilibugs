using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite itemImage;
}
public class CollectionController : MonoBehaviour
{
    public Item item;

    public float healthChange;

    public float moveSpeedChange;

    public float attackSpeedChange;

    public float bulletSizeChange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.collectedAmount++;
            GameController.HealPlayer(healthChange);
            GameController.FireRateChange(attackSpeedChange);
            GameController.BulletSizeChange(bulletSizeChange);
            GameController.MoveSpeedChange(moveSpeedChange);
            GameController.instance.UpdateCollectedItems(this);
            Destroy(gameObject);
        }
    }
}
