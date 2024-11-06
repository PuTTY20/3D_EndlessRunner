using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    protected Vector3 offPos;
    public float zPos = 0f;
    protected float speed = 0f;
    protected float initSpeed = 7f;
    protected float midleSpeed = 10.5f;   //1.5배 증가 =>
    protected float maxSpeed = 15.75f;    //2.25배 증가

    protected virtual void Start()
    {
        speed = initSpeed;
        offPos = new Vector3(transform.position.x, transform.position.y, -8f);
    }

    protected virtual void Update()
    {
        if (!GameManager.instance.isDie)
            MovePlatform();
    }

    protected virtual void MovePlatform()
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

        zPos = transform.position.z;

        if (transform.position == offPos)
            GameManager.Pooling.RetunObjectPool(gameObject);
    }
}
