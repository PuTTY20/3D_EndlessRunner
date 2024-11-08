using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public enum COINPOS { LEFT, CENTER, RIGHT }

    LayerMask obstacleLayer = 1 << 6;
    LayerMask rightLayer = 1 << 7;
    LayerMask leftLayer = 1 << 8;
    public COINPOS coinPos = COINPOS.CENTER;

    void Update()
    {
        Debug.DrawRay(transform.position + (Vector3.left * 0.8f), Vector3.forward * 2f, Color.red);
        Debug.DrawRay(transform.position, Vector3.forward * 2f, Color.red);
        Debug.DrawRay(transform.position + (Vector3.right * 0.8f), Vector3.forward * 2f, Color.red);

        Debug.DrawRay(transform.position + (Vector3.left * 0.8f), Vector3.back * 2f, Color.blue);
        Debug.DrawRay(transform.position, Vector3.back * 2f, Color.blue);
        Debug.DrawRay(transform.position + (Vector3.right * 0.8f), Vector3.back * 2f, Color.blue);

        Debug.DrawRay(transform.position + Vector3.left * 0.3f, Vector3.down * 1f, Color.grey);
        Debug.DrawRay(transform.position + Vector3.right * 0.3f, Vector3.down * 1f, Color.gray);
    }

    void OffCoin()
        => gameObject.SetActive(false);

    public void CoinCtrl()
    {
        if (GameManager.instance.isInvincible) return;

        gameObject.SetActive(false);
        GameManager.UI.GaugeUP(1);
    }

    public void ObstacleCheck()
    {
        bool forwardHit = Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 4f, obstacleLayer);
        bool leftHit = Physics.Raycast(transform.position + (Vector3.left * 0.8f), Vector3.forward, out RaycastHit hitL, 4f, obstacleLayer);
        bool rightHit = Physics.Raycast(transform.position + (Vector3.right * 0.8f), Vector3.forward, out RaycastHit hitR, 4f, obstacleLayer);

        bool backHit = Physics.Raycast(transform.position, Vector3.back, out RaycastHit bhit, 4f, obstacleLayer);
        bool backleftHit = Physics.Raycast(transform.position + (Vector3.left * 0.8f), Vector3.back, out RaycastHit bhitL, 4f, obstacleLayer);
        bool backrightHit = Physics.Raycast(transform.position + (Vector3.right * 0.8f), Vector3.back, out RaycastHit bhitR, 4f, obstacleLayer);

        bool leftPlatform = Physics.Raycast(transform.position + Vector3.left * 0.3f, Vector3.down, out RaycastHit hitLD, 1f, leftLayer);
        bool rightPlatform = Physics.Raycast(transform.position + Vector3.right * 0.3f, Vector3.down, out RaycastHit hitRD, 1f, rightLayer);

        if (!forwardHit || !backHit)
        {
            coinPos = COINPOS.CENTER;
            Debug.Log("Coin CENTER");
        }
        else if ((forwardHit && !leftHit) || (backHit && !backleftHit))
        {
            coinPos = COINPOS.LEFT;
            Debug.Log("Coin LEFT");
        }
        else if ((forwardHit && !rightHit) || (backHit && !backrightHit))
        {
            coinPos = COINPOS.RIGHT;
            Debug.Log("Coin RIGHT");
        }
        else if ((forwardHit && leftHit && rightHit) || (backHit && backleftHit && backrightHit))
        {
            Debug.Log($"forward: {forwardHit}, left: {leftHit}, right: {rightHit}");

            Debug.Log("Coin OFF");
            OffCoin();
            return;
        }

        CheckPlatform();

        void CheckPlatform()
        {
            if (leftPlatform)
                coinPos = COINPOS.LEFT;
            else if (rightPlatform)
                coinPos = COINPOS.RIGHT;
        }
    }
}