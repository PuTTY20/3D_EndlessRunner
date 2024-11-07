using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public List<GameObject> PlatformList = new List<GameObject>();
    List<GameObject> offPlatformList = new List<GameObject>();
    public List<GameObject> obstaclePlatformList = new List<GameObject>();
    List<GameObject> obstacleOffPlatformList = new List<GameObject>();
    public List<GameObject> coinList = new List<GameObject>();
    List<GameObject> coinOffList = new List<GameObject>();


    [Header("Platforms")]
    GameObject _default;
    GameObject bridge;
    GameObject oneLeft;
    GameObject oneRight;

    [Header("Obstacle Platforms")]
    GameObject JumpDoor;
    GameObject LeftDoor;
    GameObject RightDoor;
    GameObject LeftLongFlag;
    GameObject LeftShortFlag;
    GameObject MiddleLongFlag;
    GameObject MiddleShortFlag;
    GameObject RightLongFlag;
    GameObject RightShortFlag;

    GameObject Coin;

    GameObject platformGroup;
    GameObject obstacleGroup;

    GameObject coinGroup;

    int poolSize = 3;
    int coinPoolSize = 40;

    void Awake()
    {
        _default = Resources.Load<GameObject>("ObjectPlatform/DefaultPlatform");
        bridge = Resources.Load<GameObject>("ObjectPlatform/BridgePlatform");
        oneLeft = Resources.Load<GameObject>("ObjectPlatform/OneLeftPlatform");
        oneRight = Resources.Load<GameObject>("ObjectPlatform/OneRightPlatform");

        JumpDoor = Resources.Load<GameObject>("ObstaclePlatform/JumpDoor");
        LeftDoor = Resources.Load<GameObject>("ObstaclePlatform/LeftDoor");
        RightDoor = Resources.Load<GameObject>("ObstaclePlatform/RightDoor");
        LeftLongFlag = Resources.Load<GameObject>("ObstaclePlatform/LeftLongFlag");
        LeftShortFlag = Resources.Load<GameObject>("ObstaclePlatform/LeftShortFlag");
        MiddleLongFlag = Resources.Load<GameObject>("ObstaclePlatform/MiddleLongFlag");
        MiddleShortFlag = Resources.Load<GameObject>("ObstaclePlatform/MiddleShortFlag");
        RightLongFlag = Resources.Load<GameObject>("ObstaclePlatform/RightLongFlag");
        RightShortFlag = Resources.Load<GameObject>("ObstaclePlatform/RightShortFlag");

        Coin = Resources.Load<GameObject>("Coin/Coin");

        platformGroup = new GameObject("Platform Group");
        obstacleGroup = new GameObject("Obstacle Group");
        coinGroup = new GameObject("Coin Group");

        StartCoroutine(CreatePlatformPool(_default));
        StartCoroutine(CreatePlatformPool(bridge));
        StartCoroutine(CreatePlatformPool(oneLeft));
        StartCoroutine(CreatePlatformPool(oneRight));

        StartCoroutine(CreateObstaclePool(JumpDoor));
        StartCoroutine(CreateObstaclePool(LeftDoor));
        StartCoroutine(CreateObstaclePool(RightDoor));
        StartCoroutine(CreateObstaclePool(LeftLongFlag));
        StartCoroutine(CreateObstaclePool(LeftShortFlag));
        StartCoroutine(CreateObstaclePool(MiddleLongFlag));
        StartCoroutine(CreateObstaclePool(MiddleShortFlag));
        StartCoroutine(CreateObstaclePool(RightLongFlag));
        StartCoroutine(CreateObstaclePool(RightShortFlag));

        StartCoroutine(CreateCoinPool(Coin));
    }

    // 플랫폼 Pool을 생성하는 함수
    IEnumerator CreatePlatformPool(GameObject platformPrefab)
    {
        for (int i = 0; i < poolSize; i++)
        {
            var platform = Instantiate(platformPrefab, platformGroup.transform);
            platform.SetActive(false);
            PlatformList.Add(platform);
        }
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator CreateObstaclePool(GameObject obstaclePrefab)
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obstacle = Instantiate(obstaclePrefab, obstacleGroup.transform);
            obstacle.SetActive(false);
            obstaclePlatformList.Add(obstacle);
        }
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator CreateCoinPool(GameObject coinPrefab)
    {
        for (int i = 0; i < coinPoolSize; i++)
        {
            var coin = Instantiate(coinPrefab, coinGroup.transform);
            coin.SetActive(false);
            coinList.Add(coin);
        }
        yield return new WaitForSeconds(0.1f);
    }

    public GameObject GetPlatform()
    {
        // inactivePlatforms 리스트 초기화
        offPlatformList.Clear();

        // 비활성화된 플랫폼만 리스트에 추가
        foreach (GameObject platform in PlatformList)
        {
            if (!platform.activeSelf)
                offPlatformList.Add(platform);
        }

        if (offPlatformList.Count > 0)
        {
            // 비활성화된 플랫폼 중 하나를 랜덤으로 선택
            int randomIdx = Random.Range(0, offPlatformList.Count);
            return offPlatformList[randomIdx];
        }

        return null;  // 사용 가능한 비활성화된 플랫폼이 없으면 null 반환
    }

    public GameObject GetObstacle()
    {
        obstacleOffPlatformList.Clear();
        foreach (GameObject obstacle in obstaclePlatformList)
        {
            if (!obstacle.activeSelf)
                obstacleOffPlatformList.Add(obstacle);
        }

        if (obstacleOffPlatformList.Count > 0)
        {
            int randomIdx = Random.Range(0, obstacleOffPlatformList.Count);
            return obstacleOffPlatformList[randomIdx];
        }

        return null;
    }

    public GameObject GetCoin()
    {
        coinOffList.Clear();
        foreach (GameObject coin in coinList)
        {
            if (!coin.activeSelf)
                coinOffList.Add(coin);
        }

        if (coinOffList.Count > 0)
        {
            int randomIdx = Random.Range(0, coinOffList.Count);
            return coinOffList[randomIdx];
        }

        return null;
    }

    public void RetunObjectPool(GameObject platform)
        => platform.SetActive(false);

    public void OffAllPlatform()
    {
        foreach (GameObject platform in PlatformList)
            if (platform.activeSelf)
                platform.SetActive(false);

        foreach (GameObject obstacle in obstaclePlatformList)
            if (obstacle.activeSelf)
                obstacle.SetActive(false);
    }
}
