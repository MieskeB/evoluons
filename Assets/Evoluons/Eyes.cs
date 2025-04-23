using UnityEngine;

public class Eyes : MonoBehaviour
{
    public float maxDistance = 10f;
    public float angleOffset = 10f;
    public string detectableTag = "Food";

    public struct EyeRayResult
    {
        public string hitTag;
        public float distance;
    }
    
    public float[] GetNormalizedDistances()
    {
        EyeRayResult[] results = Sense();
        float[] normalized = new float[3];

        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].hitTag == "None")
                normalized[i] = -1f; // Special case: nothing seen
            else
                normalized[i] = Mathf.Clamp01(results[i].distance / maxDistance); // 0 = close, 1 = far
        }

        return normalized;
    }

    public EyeRayResult[] Sense()
    {
        EyeRayResult[] results = new EyeRayResult[3];

        results[0] = CastAndAnalyze(transform.up); // Center
        results[1] = CastAndAnalyze(Quaternion.Euler(0, 0, angleOffset) * transform.up);
        results[2] = CastAndAnalyze(Quaternion.Euler(0, 0, -angleOffset) * transform.up);

        return results;
    }

    private EyeRayResult CastAndAnalyze(Vector2 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, maxDistance);
        Debug.DrawRay(transform.position, direction * maxDistance, Color.yellow, 0.1f);

        EyeRayResult result;
        result.hitTag = "None";
        result.distance = maxDistance;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag(detectableTag))
            {
                result.hitTag = hit.collider.tag;
                result.distance = hit.distance;
                break;
            }
        }

        return result;
    }
}