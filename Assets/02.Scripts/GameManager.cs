using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovePlatform _move;
    GameObject selectedPlatform = null;
    public float posZ = 0f;

    void Start()
    {
        StartCoroutine(ActivatePlatforms());
        _move = selectedPlatform.GetComponent<MovePlatform>();
    }

    void Update()
    {
        if (_move != null)
            posZ = _move.zPos;
    }

    IEnumerator ActivatePlatforms()
    {
        while (true)
        {
            #region ObjectPooling을 사용하지 않을 때
            // //랜덤하게 하나의 플랫폼을 선택하여 활성화
            // int randomIndex = Random.Range(0, PlatformList.Count);
            // GameObject selectedPlatform = Instantiate(PlatformList[randomIndex]);
            // // 위치 설정
            // selectedPlatform.transform.position = new Vector3(0, 0, 76f);
            // selectedPlatform.SetActive(true);
            #endregion

            selectedPlatform = ObjectPooling.poolingManager.GetPlatform();

            if (selectedPlatform != null)
            {
                selectedPlatform.SetActive(true);
                _move = selectedPlatform.GetComponent<MovePlatform>();
                selectedPlatform.transform.position += new Vector3(0, 0, 80f + posZ);
            }

            yield return new WaitForSeconds(5.0f);
        }
    }
}
