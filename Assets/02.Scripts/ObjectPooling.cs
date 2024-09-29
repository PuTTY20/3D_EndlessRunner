using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling poolingManager;

    public List<GameObject> PlatformList = new List<GameObject>();
    public List<GameObject> inactivePlatforms = new List<GameObject>();
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

    public GameObject GetPlatform()
    {
        #region 전체 리스트에서 랜덤한 플랫폼을 선택해, 이미 활성화된 플랫폼을 반환하는 문제 발생으로 코드 삭제
        // foreach (GameObject platform in PlatformList)
        //     if (!platform.activeSelf && !platform.activeInHierarchy)
        //         return PlatformList[Random.Range(0, PlatformList.Count)];
        #endregion

        // 비활성화된 플랫폼만 리스트에 추가
        foreach (GameObject platform in PlatformList)
            if (!platform.activeSelf && !platform.activeInHierarchy)
                inactivePlatforms.Add(platform);

        if (inactivePlatforms.Count > 0)
        {
            int randomIdx = Random.Range(0, inactivePlatforms.Count);
            return inactivePlatforms[randomIdx];
        }

        return null;
    }

    public void RetunPlatformPool(GameObject platform) => platform.SetActive(false);
}
