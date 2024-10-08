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
        //draw raycast
        if (Physics.Raycast(tr.position, Vector3.down, out hit, 0.5f))
            gameObject.SetActive(true);

        else gameObject.SetActive(false);
    }
}
