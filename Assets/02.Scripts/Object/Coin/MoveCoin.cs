using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoin : MoveObject
{
    protected override void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, offPos, speed * Time.deltaTime);

        if(transform.position == offPos)
            GameManager.Pooling.RetunObjectPool(gameObject);
    }
}
