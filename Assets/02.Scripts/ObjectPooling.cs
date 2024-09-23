using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling poolingManager;
    
    public List<GameObject> platformList = new List<GameObject>();
    public int poolSize = 30;
    public GameObject[] platforms; // 랜덤으로 선택할 플랫폼 배열
    
    [Header("Platform")]
    public GameObject _default;
    public GameObject bridge;
    public GameObject wood;
    public GameObject gate;
    
    [Header("Obstacle")]
    public GameObject flagShort;
    public GameObject flagLong;
    public GameObject door;
    
    [Header("Condition Platform")]
    public GameObject one;
    public GameObject twoToOne;

    private Vector3 lastPlatformPosition = Vector3.zero; // 마지막 플랫폼 위치

    void Awake()
    {
        if (poolingManager == null)
            poolingManager = this;
        else if (poolingManager != this)
            Destroy(gameObject);

        StartCoroutine(CreatePlatformPool());
    }

    IEnumerator CreatePlatformPool()
    {
        GameObject platformGroup = new GameObject("PlatformGroup");

        for (int i = 0; i < poolSize; i++)
        {
            // 랜덤 플랫폼 선택
            GameObject randomPlatform = platforms[Random.Range(0, platforms.Length)];
            var platform = Instantiate(randomPlatform, platformGroup.transform);
            platform.SetActive(false);
            platformList.Add(platform);
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator Start()
    {
        // 모든 플랫폼 활성화
        for (int i = 0; i < platformList.Count; i++)
        {
            if (!platformList[i].activeInHierarchy)
            {
                platformList[i].SetActive(true);
                
                // 플랫폼의 Collider 크기 가져오기
                Collider platformCollider = platformList[i].GetComponent<Collider>();
                if (platformCollider != null)
                    // 플랫폼의 크기만큼 위치 조정
                    lastPlatformPosition += new Vector3(0, 0, platformCollider.bounds.size.z);
                else
                    // Collider가 없는 경우 기본 값으로 이동 (예: z축으로 5)
                    lastPlatformPosition += new Vector3(0, 0, 5.0f);
                
                // 플랫폼 위치 조정
                platformList[i].transform.position = lastPlatformPosition;

                // 비활성화 코루틴 호출
                StartCoroutine(DeactivatePlatform(platformList[i]));
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator DeactivatePlatform(GameObject platform)
    {
        yield return new WaitForSeconds(2f); // 2초 대기
        platform.SetActive(false); // 플랫폼 비활성화
    }
}
