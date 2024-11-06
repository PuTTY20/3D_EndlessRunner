using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    MovePlatform _movePlatform;
    GameObject selectedPlatform = null;
    GameObject selectedObstacle = null;
    GameObject selectedCoin = null;

    float posZ = 0f;

    void Start()
    {
        StartCoroutine(ActivatePlatforms());
        StartCoroutine(ActiveObstacle());
        StartCoroutine(ActiveCoin());
    }

    void Update()
    {
        if (_movePlatform != null)
            posZ = _movePlatform.zPos;
    }

    public IEnumerator ActivatePlatforms()
    {
        posZ = 0f;  //Reset 했을 때 바로 눈 앞에 플랫폼 나타나게 하려면 필수...
        while (true)
        {
            selectedPlatform = GameManager.Pooling.GetPlatform();

            if (selectedPlatform != null)
            {
                selectedPlatform.SetActive(true);
                _movePlatform = selectedPlatform.GetComponent<MovePlatform>();
                selectedPlatform.transform.position = new Vector3(0, 0, 84f + posZ);
            }

            yield return new WaitForSeconds(4.0f);
        }
    }

    public IEnumerator ActiveObstacle()
    {
        while (true)
        {
            selectedObstacle = GameManager.Pooling.GetObstacle();
            if (selectedObstacle != null)
            {
                selectedObstacle.SetActive(true);

                #region ObstacleCtrl에서 setActive(false)된 자식 Obj를 true로 바꾸기 위한 코드
                int idx = selectedObstacle.transform.childCount;
                for (int i = 0; i < idx; i++)
                    selectedObstacle.transform.GetChild(i).gameObject.SetActive(true);
                #endregion

                selectedObstacle.transform.position = new Vector3(0f, 0, 40f);
            }

            yield return new WaitForSeconds(3f);
        }
    }

    public IEnumerator ActiveCoin()
    {
        float[] xPos = new float[] { -0.8f, 0f, 0.8f };

        while (true)
        {
            selectedCoin = GameManager.Pooling.GetCoin();
            if (selectedCoin != null)
            {
                selectedCoin.SetActive(true);
                float randomX = xPos[Random.Range(0, xPos.Length)];
                selectedCoin.transform.position = new Vector3(0f, 4.85f, 20f);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OffAllObject()
    {
        foreach (GameObject platform in GameManager.Pooling.PlatformList)
            if (platform.activeSelf)
                GameManager.Pooling.RetunObjectPool(platform);

        foreach (GameObject obstacle in GameManager.Pooling.obstaclePlatformList)
            if (obstacle.activeSelf)
                GameManager.Pooling.RetunObjectPool(obstacle);

        foreach (GameObject coin in GameManager.Pooling.coinList)
            if (coin.activeSelf)
                GameManager.Pooling.RetunObjectPool(coin);
    }
}
