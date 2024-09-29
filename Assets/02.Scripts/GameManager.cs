using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Transform tr;

    void Start()
    {
        tr = transform;
        StartCoroutine(ActivatePlatforms());
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

            // ObjectPooling에서 랜덤하게 하나의 플랫폼을 가져옴
            GameObject selectedPlatform = ObjectPooling.poolingManager.GetPlatform();
            selectedPlatform.SetActive(true);

            if (selectedPlatform != null)
            {
                // 위치 설정
                selectedPlatform.transform.position = new Vector3(0, 0, 76f);

                if (selectedPlatform.transform.position == Vector3.zero)
                    ObjectPooling.poolingManager.RetunPlatformPool(selectedPlatform);
            }

            yield return new WaitForSeconds(5.0f);
        }
    }
}
