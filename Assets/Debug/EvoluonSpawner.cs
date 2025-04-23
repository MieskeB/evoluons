using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoluonSpawner : MonoBehaviour
{
    public GameObject evoluonPrefab;
    public Rect spawnArea = new Rect(-9f, -4f, 18f, 8f);

    public float bestScore = -Mathf.Infinity;
    public Brain bestBrain;
    
    void Update()
    {
        GameObject[] evoluons = GameObject.FindGameObjectsWithTag("Evoluon");
        foreach (GameObject evoluon in evoluons)
        {
            Agent evoluonAgent = evoluon.GetComponent<Agent>();
            if (evoluonAgent.score > bestScore)
            {
                bestBrain = evoluonAgent.brain.CloneWithMutation(0f);
                bestScore = evoluonAgent.score;
            }
        }
        if (evoluons.Length > 0) return;

        for (int i = 0; i < 100; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(spawnArea.xMin, spawnArea.xMax), Random.Range(spawnArea.yMin, spawnArea.yMax));
            GameObject initializedGuy = Instantiate(evoluonPrefab, randomPosition, Quaternion.identity);
            initializedGuy.GetComponent<Agent>().brain = bestBrain.CloneWithMutation();
        }
    }
}
