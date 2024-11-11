using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoin : MoveObject
{
    protected override void Start()
        => speed = initSpeed;

    public void SetOffPos(float offsetX)
        => offPos = new Vector3(transform.position.x + offsetX, transform.position.y, -8f);

    protected override void MoveObj()
    {
        if (GameManager.instance.isInvincible)
        {
            speed = invincibleSpeed;
        }

        else
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

            else if (GameManager.Score.score > 0)
            {
                speed = initSpeed;
                Debug.Log(speed);
            }
        }

        transform.position = Vector3.MoveTowards(new Vector3(offPos.x, transform.position.y, transform.position.z), offPos, speed * Time.deltaTime);

        if (transform.position == offPos)
            GameManager.Pooling.RetunObjectPool(gameObject);
    }
}
