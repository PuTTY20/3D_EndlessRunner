using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Transform tr;
    string startPlatform = "START";
    public float zPos = 0f;

    void Start()
    {
        tr = transform;
    }

    void Update()
    {
        if (gameObject.CompareTag(startPlatform))
        {
            tr.position = Vector3.MoveTowards(tr.position, new Vector3(tr.position.x, tr.position.y, -6f), 10f * Time.deltaTime);

            if (tr.position == new Vector3(tr.position.x, tr.position.y, -6f))
                this.gameObject.SetActive(false);
        }

        //PlatformMove
        else
        {
            tr.position = Vector3.MoveTowards(tr.position, new Vector3(tr.position.x, tr.position.y, -5f), 10f * Time.deltaTime);

            zPos = tr.position.z;

            if (tr.position == new Vector3(0f, 0f, -5f))
                ObjectPooling.poolingManager.RetunPlatformPool(gameObject);
        }
    }
}
