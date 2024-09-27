using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    Transform tr;

    void Start()
    {
        tr = transform;
    }

    void Update()
    {
        tr.position = Vector3.MoveTowards(tr.position, new Vector3(tr.position.x, tr.position.y, 0f), 10f * Time.deltaTime);

        /* if (tr.position == Vector3.zero)
            gameObject.SetActive(false); */
    }
}
