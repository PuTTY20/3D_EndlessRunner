using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    float radius = 1.0f;
    LayerMask obstacleLayer = 1 << 6;

    void Update()
        => CheckForObstacles();

    public void OffCoin()
        => gameObject.SetActive(false);

    void CheckForObstacles()
    {
        Collider[] hitCol = Physics.OverlapSphere(transform.position, radius, obstacleLayer);
        foreach (var col in hitCol)
        {
            if(col.transform.parent.TryGetComponent(out MoveObstacle _obstacle))
                OffCoin();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}