using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Transform tr;
    Vector3 targetPos;
    [SerializeField] Vector3 startPlatformInitPos;
    [SerializeField] GameObject startPlatform;
    public readonly string startName = "Start";
    public float zPos = 0f;
    float speed = 0f;
    float initSpeed = 7f;
    float midleSpeed = 10.5f;   //1.5배 증가
    float maxSpeed = 15.75f;    //2.25배 증가

    void Start()
    {
        tr = transform;
        speed = initSpeed;
        targetPos = new Vector3(tr.position.x, tr.position.y, -8f);
        if(startPlatform != null)
        startPlatform = GameObject.Find(startName).gameObject;

        if (startPlatform)
            startPlatformInitPos = transform.position;
    }

    void Update()
    {
        if (startPlatform)
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
        tr.position = Vector3.MoveTowards(tr.position, targetPos, speed * Time.deltaTime);

        zPos = tr.position.z;

        if (tr.position == targetPos)
            ObjectPooling.objpooling.RetunPlatformPool(gameObject);
    }

    public void ResetPlatform()
    {
        startPlatform.SetActive(true);
        startPlatform.transform.position = startPlatformInitPos;
    }
}
