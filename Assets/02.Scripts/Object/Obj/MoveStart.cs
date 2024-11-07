using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStart : MoveObject
{
    GameObject start;
    Vector3 initPos;

    override protected void Start()
    {
        base.Start();

        start = transform.GetChild(0).gameObject;
        initPos = transform.position;
    }

    protected override void MoveObj()
    {
        transform.position = Vector3.MoveTowards(transform.position, offPos, speed * Time.deltaTime);

        if (transform.position == offPos)
            start.SetActive(false);
    }

    public void ResetStartPlatform()
    {
        transform.position = initPos;
        start.SetActive(true);
    }
}
