using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    GameObject start;
    Transform tr;
    Vector3 initPos;
    Vector3 offPos;
    float speed = 7f;

    void Start()
    {
        start = transform.GetChild(0).gameObject;
        tr = transform;
        initPos = tr.position;
        offPos = new Vector3(tr.position.x, tr.position.y, -8f);
    }

    void Update()
    {
        tr.position = Vector3.MoveTowards(tr.position, offPos, speed * Time.deltaTime);

        if (tr.position == offPos)
            start.SetActive(false);
    }

    public void ResetStartPlatform()
    {
        tr.position = initPos;
        start.SetActive(true);
    }
}
