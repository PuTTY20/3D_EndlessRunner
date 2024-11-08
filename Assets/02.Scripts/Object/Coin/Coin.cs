using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public enum COINPOS { LEFT, CENTER, RIGHT }

    LayerMask obstacleLayer = 1 << 6;
    public COINPOS coinPos = COINPOS.CENTER;
    
    void Update()
        => ObstacleCheck();

    void OffCoin()
        => gameObject.SetActive(false);

    public void CoinCtrl()
    {
        if (GameManager.instance.isInvincible) return;

        gameObject.SetActive(false);
        GameManager.UI.GaugeUP(1);
    }

    public void SetCoinPosition(COINPOS position)
        => coinPos = position;

    void ObstacleCheck()
    {
        RaycastHit hit;
        Vector3 leftDir = (3 * Vector3.forward + Vector3.left).normalized;
        Vector3 rightDir = (3 * Vector3.forward + Vector3.right).normalized;
        bool leftHit = Physics.Raycast(transform.position, leftDir, out hit, 2f, obstacleLayer);
        bool forwardHit = Physics.Raycast(transform.position, Vector3.forward, out hit, 3f, obstacleLayer);
        bool rightHit = Physics.Raycast(transform.position, rightDir, out hit, 2f, obstacleLayer);

        if (leftHit && forwardHit)
        {
            coinPos = COINPOS.RIGHT;
            Debug.Log("Coin position set to RIGHT");
        }

        if (rightHit && forwardHit)
        {
            coinPos = COINPOS.LEFT;
            Debug.Log("Coin position set to LEFT");
        }

        if (leftHit && forwardHit && rightHit)
        {
            OffCoin();
        }
        else if (!leftHit && !forwardHit && !rightHit)
        {
            coinPos = COINPOS.CENTER;
            Debug.Log("Coin position set to CENTER");
        }

        Debug.DrawRay(transform.position, leftDir * 2f, Color.red);
        Debug.DrawRay(transform.position, Vector3.forward * 3f, Color.red);
        Debug.DrawRay(transform.position, rightDir * 2f, Color.red);
    }
}