using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCtrl : MonoBehaviour
{
    Transform tr;

    void Start()
    {
        tr = transform;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(tr.position + Vector3.up * 0.3f, Vector3.down, out hit, 2f))
        {
            if (hit.collider.CompareTag("PLATFORM"))
            {
                // PLATFORM이 감지되지 않았을 때 gameObject.SetActive(false)가 된 후 다시 불러왔을 때 계속 비활성화 되어 있는 상태를 방지하기 위한 로직
                if (!gameObject.activeSelf) // 비활성화 상태라면
                {
                    Debug.Log($"{gameObject.name}이 PLATFORM 감지하여 활성화");
                    gameObject.SetActive(true);
                }
            }
            else
            {
                // PLATFORM이 감지되지 않으면 비활성화
                if (gameObject.activeSelf) // 활성화 상태라면
                {
                    Debug.Log($"{gameObject.name}이 PLATFORM 없음, 비활성화");
                    gameObject.SetActive(false);
                }
            }
        }
        
        else
        {
            // 플랫폼이 없을 때 비활성화
            if (gameObject.activeSelf) // 활성화 상태라면
            {
                Debug.Log($"{gameObject.name}이 PLATFORM 없음, 비활성화");
                gameObject.SetActive(false);
            }
        }
    }
}
