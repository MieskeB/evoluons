using UnityEngine;
using Random = UnityEngine.Random;

public class Agent : MonoBehaviour
{
    public float score = 0f;

    [Header("stats")] 
    public float energy = 100f;
    public float health = 100f;
    public float happiness = 100f;
    public Brain brain;

    [Header("debug")] public float[] outputDisplay;
    
    [Header("components")]
    private Eyes eyes;

    private void Start()
    {
        brain = new Brain();
        eyes = GetComponent<Eyes>();
    }

    private void Update()
    {
        float[] vision = eyes.GetNormalizedDistances();
        float[] inputs = new float[]
        {
            health / 100f,
            energy / 100f,
            happiness / 100f,
            vision[0],
            vision[1],
            vision[2],
            0.1f * Random.value
        };

        float[] outputs = brain.Forward(inputs);
        this.outputDisplay = outputs;
        int action = ArgMax(outputs);
        switch (action)
        {
            case 0:
                Move(outputs[4], outputs[5]);
                break;
            case 1:
                Eat();
                break;
            case 2:
                Rest();
                break;
            case 3:
                Reproduce();
                break;
        }

        CapStats();

        energy -= Time.deltaTime * 5f;
        health -= Time.deltaTime * 1f;
        happiness -= Time.deltaTime * 2f;
        score += Time.deltaTime * (energy + health + happiness) / 300f;

        if (energy <= 0f || health <= 0f || happiness <= 0f)
        {
            score -= 25f;
            Destroy(gameObject);
        }
    }

    private int ArgMax(float[] array)
    {
        int bestIndex = 0;
        float bestValue = array[0];

        for (int i = 1; i < 4; i++)
        {
            if (array[i] > bestValue)
            {
                bestValue = array[i];
                bestIndex = i;
            }
        }

        return bestIndex;
    }

    private void CapStats()
    {
        if (energy > 100f) energy = 100f;
        if (health > 100f) health = 100f;
        if (happiness > 100f) happiness = 100f;
    }

    private void Move(float velocity, float rotation)
    {
        float speedMultiplier = Mathf.Clamp01((velocity + 1f) / 2f);

        float moveSpeed = 2f;
        float currentSpeed = moveSpeed * speedMultiplier;

        Vector3 forward = transform.up;
        transform.position += forward * (currentSpeed * Time.deltaTime);

        float rotationSpeed = 180f;
        float rotationAmount = rotation * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, rotationAmount);
        
        float baseEnergyCost = 1f;
        float energyCost = baseEnergyCost * (0.5f + speedMultiplier); // e.g., 0.5x at standstill, 1.5x at sprint
        energy -= Time.deltaTime * energyCost;
    }

    private void Eat()
    {
        Collider2D touchingFood = Physics2D.OverlapCircle(transform.position, 2f);
        if (touchingFood != null && touchingFood.CompareTag("Food"))
        {
            if (energy >= 50)
            {
                happiness -= 20f;
                score -= 25;
            }

            energy += 50f;
            health += 10f;
            score += 50f;
            Destroy(touchingFood.gameObject);
        }
    }

    private void Rest()
    {
        energy += Time.deltaTime * 10f;
        if (energy >= 95f) happiness -= 5f * Time.deltaTime;
    }

    private float SenseFood()
    {
        GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
        float minDistance = float.MaxValue;

        foreach (GameObject food in foodObjects)
        {
            float distance = Vector3.Distance(transform.position, food.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }

        float normalized = 1f - Mathf.Clamp01(minDistance / 10f);
        return normalized;
    }

    private void Reproduce()
    {
        if (energy <= 50f) return;
        energy -= 50f;
        health -= 20f;
        happiness += 100f;
        score += 100f;
        GameObject child = Instantiate(gameObject, transform.position, Quaternion.identity);
        Agent childAgent = child.GetComponent<Agent>();
        childAgent.health = 100f;
        childAgent.energy = 100f;
        childAgent.happiness = 100f;
        childAgent.score = 0f;
        childAgent.brain = brain.CloneWithMutation();
    }
}