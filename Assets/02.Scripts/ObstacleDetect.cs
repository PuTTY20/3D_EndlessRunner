using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetect : MonoBehaviour
{
    Transform tr;
    string obstacleTag = "OBSTACLE";

    void Start()
    {
        tr = transform;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(obstacleTag))
        {
            Debug.Log("obstacle");
        }
    }
}
