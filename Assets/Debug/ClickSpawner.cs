using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 = left click
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(prefabToSpawn, clickPosition, Quaternion.identity);
        }
    }
}
