using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float weight;
    }
    public List<Spawnable> items = new List<Spawnable>();
    float totalWeight;

    private void Awake()
    {
        totalWeight = 0;
        foreach (var spawnable in items)
        {
            totalWeight += spawnable.weight;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = items[0].weight;
        while (pick > cumulativeWeight && chosenIndex < items.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += items[chosenIndex].weight;
        }
        GameObject item = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
