using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    LayerMask obstacleLayer = 1 << 6;
    float radius = 1.5f;

    void Update()
        => ObstacleCheck();

    void OffCoin()
        => gameObject.SetActive(false);

    public void CoinCtrl()
    {
        if(GameManager.instance.isInvincible) return;
        gameObject.SetActive(false);
        GameManager.UI.GaugeUP(1);
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
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}