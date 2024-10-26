using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Transform tr;
    Vector3 offPos;
    public float zPos = 0f;
    float speed = 0f;
    float initSpeed = 7f;
    float midleSpeed = 10.5f;   //1.5배 증가
    float maxSpeed = 15.75f;    //2.25배 증가

    void Start()
    {
        tr = transform;
        speed = initSpeed;
        offPos = new Vector3(tr.position.x, tr.position.y, -8f);
    }

    void Update()
    {
        if (!GameManager.instance.isDie)
            MovePlatform();

        if (GameManager.instance.isDie)
        {
            zPos = 0f;
        }
    }

    public void MovePlatform()
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
        tr.position = Vector3.MoveTowards(tr.position, offPos, speed * Time.deltaTime);

        zPos = tr.position.z;

        if (tr.position == offPos)
            GameManager.Pooling.RetunPlatformPool(gameObject);
    }
}
