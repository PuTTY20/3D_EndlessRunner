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

        StartCoroutine(CreateDefaultPool());
        StartCoroutine(CreatebridgePool());
        StartCoroutine(CreateOneLeftPool());
        StartCoroutine(CreateOneRightPool());
    }

    IEnumerator CreateDefaultPool()
    {
        GameObject defaultGroup = new GameObject("DefaultGroup");

        for (int i = 0; i < poolSize; i++)
        {
            var defaultPlatform = Instantiate(_default, defaultGroup.transform);
            defaultPlatform.SetActive(false);
            PlatformList.Add(defaultPlatform);
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator CreatebridgePool()
    {
        GameObject bridgeGroup = new GameObject("BridgeGroup");

        for (int i = 0; i < poolSize; i++)
        {
            var bridgePlatform = Instantiate(bridge, bridgeGroup.transform);
            bridgePlatform.SetActive(false);
            PlatformList.Add(bridgePlatform);
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator CreateOneLeftPool()
    {
        GameObject oneLeftGroup = new GameObject("OneLeftGroup");

        for (int i = 0; i < poolSize; i++)
        {
            var oneLeftPlatform = Instantiate(oneLeft, oneLeftGroup.transform);
            oneLeftPlatform.SetActive(false);
            PlatformList.Add(oneLeftPlatform);
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator CreateOneRightPool()
    {
        GameObject oneRightGroup = new GameObject("OneRightGroup");

        for (int i = 0; i < poolSize; i++)
        {
            var oneRightPlatform = Instantiate(oneRight, oneRightGroup.transform);
            oneRightPlatform.SetActive(false);
            PlatformList.Add(oneRightPlatform);
        }

        yield return new WaitForSeconds(0.1f);
    }

    public GameObject GetPlatform()
    {
        foreach (GameObject platform in PlatformList)
        {
            if (!platform.activeSelf)
                return platform;
        }

        return null;
    }

    public void RetunPlatformPool(GameObject platform)
    {
        platform.SetActive(false);
    }

}
