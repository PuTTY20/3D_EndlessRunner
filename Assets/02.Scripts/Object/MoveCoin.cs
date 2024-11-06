using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoin : MoveObject
{
    protected override void Start()
        => base.Start();

    protected override void Update()
        => base.Update();

    protected override void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, offPos, speed * Time.deltaTime);
        zPos = transform.position.z;

        if(transform.position == offPos)
            GameManager.Pooling.RetunObjectPool(gameObject);
    }
}
