using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    protected Vector3 offPos;
    protected float speed = 0f;
    protected float initSpeed = 7f;
    protected float midleSpeed = 10.5f;   //1.5배 증가
    protected float maxSpeed = 15.75f;    //2.25배 증가

    protected virtual void Start()
    {
        speed = initSpeed;
        offPos = new Vector3(transform.position.x, transform.position.y, -8f);
    }

    protected virtual void Update()
    {
        if (!GameManager.instance.isDie)
            MoveObj();
    }

    protected virtual void MoveObj()
    {
        transform.position = Vector3.MoveTowards(transform.position, offPos, speed * Time.deltaTime);

        if(transform.position == offPos)
            GameManager.Pooling.RetunObjectPool(gameObject);
    }
}
