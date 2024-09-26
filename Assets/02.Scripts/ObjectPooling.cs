using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling poolingManager;

    public List<GameObject> defaultPlatformList = new List<GameObject>();
    public List<GameObject> bridgePlatformList = new List<GameObject>();
    public List<GameObject> woodPlatformList = new List<GameObject>();
    public List<GameObject> gatePlatformList = new List<GameObject>();
    public List<GameObject> flagShortPlatformList = new List<GameObject>();
    public List<GameObject> flagLongPlatformList = new List<GameObject>();
    public List<GameObject> doorPlatformList = new List<GameObject>();
    public List<GameObject> onePlatformList = new List<GameObject>();
    public List<GameObject> twoToOnePlatformList = new List<GameObject>();

    public int poolSize = 60;
    public int platformSize = 9;

    // List of Lists of GameObjects to hold multiple platform lists
    public List<List<GameObject>> PlatformList = new List<List<GameObject>>();

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

        StartCoroutine(CreateDefaultPool());
        StartCoroutine(CreatebridgePool());
    }

    IEnumerator CreateDefaultPool()
    {
        GameObject defaultGroup = new GameObject("DefaultGroup");

        for (int i = 0; i < poolSize; i++)
        {
            var defaultPlatform = Instantiate(_default, defaultGroup.transform);
            defaultPlatform.SetActive(false);
            defaultPlatformList.Add(defaultPlatform);
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
            bridgePlatformList.Add(bridgePlatform);
        }

        yield return new WaitForSeconds(0.1f);
    }

    void Start()
    {
        PlatformList.Add(defaultPlatformList);
        PlatformList.Add(bridgePlatformList);

        StartCoroutine(GetColSize(PlatformList[0]));
    }

    IEnumerator GetColSize(List<GameObject> selectedPlatform)
    {
        for (int i = 0; i < defaultPlatformList.Count; i++)
        {
            if (!selectedPlatform[i].activeInHierarchy)
            {
                selectedPlatform[i].SetActive(true);

                Collider platformCol = selectedPlatform[i].GetComponent<Collider>();
                if (platformCol != null)
                    // 플랫폼의 크기만큼 위치 조정
                    lastPlatformPosition += new Vector3(0, 0, platformCol.bounds.size.z);

                // 플랫폼 위치 조정
                selectedPlatform[i].transform.position = lastPlatformPosition;

                // 비활성화 코루틴 호출
                StartCoroutine(PlatformOff(selectedPlatform[i]));
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator PlatformOff(GameObject platform)
    {
        yield return new WaitForSeconds(2f); // 2초 대기
        platform.SetActive(false); // 플랫폼 비활성화
    }
}
