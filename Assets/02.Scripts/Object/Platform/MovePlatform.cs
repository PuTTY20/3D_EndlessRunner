using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MoveObject
{
    public float zPos = 0f;

    protected override void MoveObj()
    {
        if (GameManager.Score.score > 200)
        {
            speed = maxSpeed;
            Debug.Log(speed);
        }
        else if (GameManager.Score.score > 100)
        {
            speed = midleSpeed;
            Debug.Log(speed);
        }
        transform.position = Vector3.MoveTowards(transform.position, offPos, speed * Time.deltaTime);

        if (transform.position == offPos)
            GameManager.Pooling.RetunObjectPool(gameObject);
        zPos = transform.position.z;

    }
}
