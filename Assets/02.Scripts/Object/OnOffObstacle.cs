using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffObstacle : MonoBehaviour
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
                //Debug.Log($"{gameObject.name}이 PLATFORM 감지하여 활성화");
                gameObject.SetActive(true);

            //floag에서 Player가 슬라이딩 할 때 false되지 않도록 코드 추가
            else if (hit.collider.CompareTag("Player"))
                gameObject.SetActive(true);
        }

        else
            //Debug.Log($"{gameObject.name} 아래 PLATFORM 없음, 비활성화");
            gameObject.SetActive(false);
    }
}
