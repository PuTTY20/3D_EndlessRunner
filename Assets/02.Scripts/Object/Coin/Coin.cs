using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float radius = 0.7f;
    public LayerMask obstacleLayer = 1 << 6;

    void Update()
    {
        CheckForObstacles();
    }

    public void CoinOnTrigger()
    {
        gameObject.SetActive(false);
    }

    void CheckForObstacles()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, obstacleLayer);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Obstacle detected: " + hitCollider.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}