using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]
public class SpawnerData : ScriptableObject
{
    public GameObject itemToSpawn;
    public int minSpawn;
    public int maxSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
