using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Transform tr;
    string startPlatform = "START";
    public float zPos = 0f;
    Vector3 targetPos;

    void Start()
    {
        tr = transform;
        targetPos = new Vector3(tr.position.x, tr.position.y, -5f);
    }

    void Update()
    {
        if (gameObject.CompareTag(startPlatform))
        {
            tr.position = Vector3.MoveTowards(tr.position, targetPos, 10f * Time.deltaTime);

            if (tr.position == targetPos)
                this.gameObject.SetActive(false);
        }

        //PlatformMove
        else
        {
            tr.position = Vector3.MoveTowards(tr.position, targetPos, 10f * Time.deltaTime);

            zPos = tr.position.z;

            if (tr.position == targetPos)
                ObjectPooling.poolingManager.RetunPlatformPool(gameObject);
        }
    }
}
