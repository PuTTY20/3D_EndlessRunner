using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MoveObject
{
    public float zPos = 0f;

    protected override void MoveObj()
    {
        base.MoveObj();
        zPos = transform.position.z;
    }
}
