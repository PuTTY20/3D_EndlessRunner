using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float radius = 0.7f;
    public LayerMask obstacleLayer = 1 << 6;

    void Update()
        => CheckForObstacles();

    public void CoinTrigger()
        => gameObject.SetActive(false);

    void CheckForObstacles()
    {
        Collider[] hitCol = Physics.OverlapSphere(transform.position, radius, obstacleLayer);
        foreach (var col in hitCol)
        {
            if(col.gameObject.CompareTag("OBSTACLE"))
            {
                
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}