using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    Transform tr;
    Vector3 offPos;
    GameObject start;
    public float zPos = 0f;
    float speed = 0f;
    float initSpeed = 7f;

    void Start()
    {
        tr = transform;
        speed = initSpeed;
        start = tr.GetChild(0).gameObject;
        offPos = new Vector3(tr.position.x, tr.position.y, -8f);
    }

    void Update()
    {
        tr.position = Vector3.MoveTowards(tr.position, offPos, speed * Time.deltaTime);

        if (tr.position == offPos)
        {
            start.SetActive(false);
            start.transform.position = Vector3.zero;
        }
    }

    public void ResetStartPlatform() => tr.position = Vector3.zero;
}
