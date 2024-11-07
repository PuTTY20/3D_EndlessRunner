using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    LayerMask obstacleLayer = 1 << 6;
    public int coin = 0;
    float radius = 1.5f;

    void Update()
        => ObstacleCheck();

    void OffCoin()
        => gameObject.SetActive(false);

    public void CoinCtrl()
    {
        gameObject.SetActive(false);
        coin++;
        GameManager.UI.GaugeUP(coin);
    }

    void ObstacleCheck()
    {
        Collider[] hitCol = Physics.OverlapSphere(transform.position, radius, obstacleLayer);
        foreach (var col in hitCol)
        {
            if (col.transform.parent.TryGetComponent(out MoveObstacle _obstacle))
                OffCoin();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}