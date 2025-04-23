using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public float spawnInterval = 2f;
    public Rect spawnArea = new Rect(-9f, -4f, 18f, 8f);

    void Start()
    {
        InvokeRepeating("SpawnFood", 0f, spawnInterval);
    }

    private void SpawnFood()
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
        if (foods.Length > 30) return;
        
        Vector2 randomPosition = new Vector2(Random.Range(spawnArea.xMin, spawnArea.xMax), Random.Range(spawnArea.yMin, spawnArea.yMax));
        Instantiate(foodPrefab, randomPosition, Quaternion.identity);
    }
}
