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
                Debug.Log($"{gameObject.name}이 PLATFORM 감지하여 활성화");
                gameObject.SetActive(true);
            }
            else
            {
                Debug.Log($"{gameObject.name}이 PLATFORM 없음, 비활성화");
                gameObject.SetActive(false);
            }
        }

        else
        {
            Debug.Log($"{gameObject.name}이 PLATFORM 없음, 비활성화");
            gameObject.SetActive(false);
        }
    }
}
