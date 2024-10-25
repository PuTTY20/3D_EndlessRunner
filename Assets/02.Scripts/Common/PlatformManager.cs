using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    MoveObject _move;
    GameObject selectedPlatform = null;
    GameObject selectedObstacle = null;

    float posZ = 0f;

    void Start()
    {
        StartCoroutine(ActivatePlatforms());
        StartCoroutine(ActiveObstacle());
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

            selectedPlatform = GameManager.Pooling.GetPlatform();

            if (selectedPlatform != null)
            {
                selectedPlatform.SetActive(true);
                _move = selectedPlatform.GetComponent<MoveObject>();
                selectedPlatform.transform.position = new Vector3(0, 0, 84f + posZ);
            }

            yield return new WaitForSeconds(4.0f);
        }
    }

    IEnumerator ActiveObstacle()
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

}
