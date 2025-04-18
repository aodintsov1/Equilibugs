using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    public GridController grid;
    public RandomSpawner[] spawnerData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //grid = GetComponentInChildren<GridController>();
    }

    public void InitializeObjectSpawning()
    {
        foreach (RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }
    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);
        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.availablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availablePoints.RemoveAt(randomPos);
            Debug.Log("Spawned Object: " + go.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
