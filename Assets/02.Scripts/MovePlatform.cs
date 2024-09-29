using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    Transform tr;
    string startPlatform = "START";

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
        else
        {
            tr.position = Vector3.MoveTowards(tr.position, new Vector3(tr.position.x, tr.position.y, 0f), 10f * Time.deltaTime);

            if (tr.position == Vector3.zero)
                ObjectPooling.poolingManager.RetunPlatformPool(gameObject);
        }
    }
}
