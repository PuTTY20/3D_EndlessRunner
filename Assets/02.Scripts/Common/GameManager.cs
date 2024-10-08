using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public MoveObject _move;
    GameObject selectedPlatform = null;
    GameObject selectedObstacle = null;
    public float posZ = 0f;

    void Start()
    {
        StartCoroutine(ActivatePlatforms());
        StartCoroutine(ActiveObstacle());
    }

    void Update()
    {
        if (_move != null)
            posZ = _move.zPos;
        //Debug.Log(posZ);
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
            selectedObstacle = ObjectPooling.poolingManager.GetObstacle();
            if (selectedObstacle != null)
            {
                selectedObstacle.SetActive(true);
                
                int idx = selectedObstacle.transform.childCount;
                for (int i = 0; i < idx; i++)
                    selectedObstacle.transform.GetChild(i).gameObject.SetActive(true);

                selectedObstacle.transform.position = new Vector3(0f, 0, 30f);
                //Debug.Log(selectedObstacle.transform.position);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}

//