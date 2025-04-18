using UnityEngine;

[CreateAssetMenu(fileName = "Familar.asset", menuName = "ScriptableObjects/Familiar")]
public class FamiliarData : ScriptableObject
{
    public string familiarType;

    public float speed;
    public float fireDelay;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
