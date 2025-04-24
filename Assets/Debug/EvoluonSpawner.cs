using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvoluonSpawner : MonoBehaviour
{
    public GameObject evoluonPrefab;
    public Rect spawnArea = new Rect(-9f, -4f, 18f, 8f);

    public List<Brain> bestBrains = new List<Brain>();
    public List<float> bestScores = new List<float>();
    public int generation = 0;
    
    void Update()
        {
            GameObject[] evoluons = GameObject.FindGameObjectsWithTag("Evoluon");
    
            // While agents exist, track the top 10 best brains
            foreach (GameObject evoluon in evoluons)
            {
                Agent agent = evoluon.GetComponent<Agent>();
                if (bestBrains.Count < 10 || agent.score > bestScores.Min())
                {
                    // Remove worst from list if already full
                    if (bestBrains.Count == 10)
                    {
                        int worstIndex = bestScores.IndexOf(bestScores.Min());
                        bestBrains.RemoveAt(worstIndex);
                        bestScores.RemoveAt(worstIndex);
                    }
    
                    bestBrains.Add(agent.brain.CloneWithMutation(0f)); // Clone without mutation
                    bestScores.Add(agent.score);
                }
            }
    
            // Don't spawn if any are alive
            if (evoluons.Length > 0) return;
    
            float averageScore = bestScores.Average();
            Debug.Log($"Generation {generation} - Avg top score: {averageScore:F2}, Max: {bestScores.Max():F2}");
            generation++;
            Debug.Log($"Spawning generation {generation}...");
    
            // If no good brains yet, just spawn random ones
            if (bestBrains.Count == 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    SpawnNewRandom();
                }
                return;
            }
    
            // Spawn new generation based on top brains
            for (int i = 0; i < 100; i++)
            {
                Vector2 randomPosition = new Vector2(
                    Random.Range(spawnArea.xMin, spawnArea.xMax),
                    Random.Range(spawnArea.yMin, spawnArea.yMax)
                );
            
                GameObject baby = Instantiate(evoluonPrefab, randomPosition, Quaternion.identity);
            
                if (Random.value < 0.2f)
                {
                    // 20% are random brand-new brains
                    baby.GetComponent<Agent>().brain = new Brain();
                }
                else
                {
                    // 80% come from top 10 brains
                    Brain parent = bestBrains[Random.Range(0, bestBrains.Count)];
                    baby.GetComponent<Agent>().brain = parent.CloneWithMutation(0.2f);
                }
            }
    
            // Optional: clear old brains if you want fresh top-10 every generation
            // bestBrains.Clear();
            // bestScores.Clear();
        }
    
        private void SpawnNewRandom()
        {
            Vector2 pos = new Vector2(
                Random.Range(spawnArea.xMin, spawnArea.xMax),
                Random.Range(spawnArea.yMin, spawnArea.yMax)
            );
            GameObject agent = Instantiate(evoluonPrefab, pos, Quaternion.identity);
            agent.GetComponent<Agent>().brain = new Brain();
        }
    }