using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling poolingManager;

    public List<GameObject> PlatformList = new List<GameObject>();
    public GameObject _default;
    public GameObject bridge;
    public GameObject oneLeft;
    public GameObject oneRight;

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
    }

    // 플랫폼 풀을 생성하는 함수
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

    // 비활성화된 플랫폼을 반환하는 함수
    public GameObject GetPlatform()
    {
        foreach (GameObject platform in PlatformList)
            if (!platform.activeSelf)
                return platform;

        return null;  // 비활성화된 오브젝트가 없으면 null 반환
    }

    public void RetunPlatformPool(GameObject platform) => platform.SetActive(false);
}
