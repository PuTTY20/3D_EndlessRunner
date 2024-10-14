using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Transform tr;
    string startPlatform = "START";
    public float zPos = 0f;
    Vector3 targetPos;
    float speed = 0f;
    float initSpeed = 7f;
    float midleSpeed = 10.5f;      //1.5배 증가
    float maxSpeed = 15.75f;    //2.25배 증가


    void Start()
    {
        tr = transform;
        speed = initSpeed;
        targetPos = new Vector3(tr.position.x, tr.position.y, -5f);
    }

    void Update()
    {
        if (gameObject.CompareTag(startPlatform))
        {
            tr.position = Vector3.MoveTowards(tr.position, targetPos, 7f * Time.deltaTime);

            if (tr.position == targetPos)
                this.gameObject.SetActive(false);
        }

        //PlatformMove
        else
            MovePlatform();
    }

    public void MovePlatform()
    {
        if(GameManager.instance.score > 200)
        {
            speed = maxSpeed;
            Debug.Log(speed);
        }
        else if(GameManager.instance.score > 100)
        {
            speed = midleSpeed;
            Debug.Log(speed);
        }
        tr.position = Vector3.MoveTowards(tr.position, targetPos, speed * Time.deltaTime);

        zPos = tr.position.z;

        if (tr.position == targetPos)
            ObjectPooling.poolingManager.RetunPlatformPool(gameObject);
    }
}
