using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling poolingManager;

    public List<GameObject> PlatformList = new List<GameObject>();
    public List<GameObject> inactivePlatforms = new List<GameObject>();

    [Header("Platforms")]
    public GameObject _default;
    public GameObject bridge;
    public GameObject oneLeft;
    public GameObject oneRight;

    [Header("Obstacle Platforms")]
    public GameObject JumpDoor;
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject LeftLongFlag;
    public GameObject LeftShortFlag;
    public GameObject MiddleLongFlag;
    public GameObject MiddleShortFlag;
    public GameObject RightLongFlag;
    public GameObject RightShortFlag;

    public int poolSize = 3;

    void Awake()
    {
        if (poolingManager == null)
            poolingManager = this;
        else if (poolingManager != this)
            Destroy(gameObject);

        StartCoroutine(CreatePool(_default));
        StartCoroutine(CreatePool(bridge));
        StartCoroutine(CreatePool(oneLeft));
        StartCoroutine(CreatePool(oneRight));

        StartCoroutine(CreateObstaclePool(JumpDoor));
        StartCoroutine(CreateObstaclePool(LeftDoor));
        StartCoroutine(CreateObstaclePool(RightDoor));
        StartCoroutine(CreateObstaclePool(LeftLongFlag));
        StartCoroutine(CreateObstaclePool(LeftShortFlag));
        StartCoroutine(CreateObstaclePool(MiddleLongFlag));
        StartCoroutine(CreateObstaclePool(MiddleShortFlag));
        StartCoroutine(CreateObstaclePool(RightLongFlag));
        StartCoroutine(CreateObstaclePool(RightShortFlag));
    }

    // 플랫폼 Pool을 생성하는 함수
    IEnumerator CreatePool(GameObject platformPrefab)
    {
        for (int i = 0; i < poolSize; i++)
        {
            var platform = Instantiate(platformPrefab);
            platform.SetActive(false);
            PlatformList.Add(platform);
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator CreateObstaclePool(GameObject obstaclePrefab)
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obstacle = Instantiate(obstaclePrefab);
            obstacle.SetActive(false);
            inactivePlatforms.Add(obstacle);
        }
        yield return new WaitForSeconds(0.1f);
    }

    public GameObject GetPlatform()
    {
        #region 전체 리스트에서 랜덤한 플랫폼을 선택하는 코드로, 이미 활성화된 플랫폼을 반환해 플랫폼이 자리를 이동해버리는 문제 발생으로 코드 삭제
        // foreach (GameObject platform in PlatformList)
        //     if (!platform.activeSelf && !platform.activeInHierarchy)
        //         return PlatformList[Random.Range(0, PlatformList.Count)];
        #endregion

        // inactivePlatforms 리스트 초기화
        inactivePlatforms.Clear();

        // 비활성화된 플랫폼만 리스트에 추가
        foreach (GameObject platform in PlatformList)
        {
            if (!platform.activeSelf)
                inactivePlatforms.Add(platform);
        }

        if (inactivePlatforms.Count > 0)
        {
            // 비활성화된 플랫폼 중 하나를 랜덤으로 선택
            int randomIdx = Random.Range(0, inactivePlatforms.Count);
            return inactivePlatforms[randomIdx];
        }

        return null;  // 사용 가능한 비활성화된 플랫폼이 없으면 null 반환
    }

    public GameObject GetObstacle()
    {
    }

    public void RetunPlatformPool(GameObject platform) => platform.SetActive(false);
}
