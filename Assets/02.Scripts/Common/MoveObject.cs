using UnityEngine;

public class MoveObject : MonoBehaviour
{
    internal Vector3 offPos;
    protected float speed = 0f;
    protected float initSpeed = 7f;
    protected float midleSpeed = 10.5f;
    protected float maxSpeed = 13f;
    protected float invincibleSpeed = 15f;
    protected float preSpeed = 0f;

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
        if (GameManager.instance.isInvincible)
        {
            if (speed != invincibleSpeed)
            {
                preSpeed = speed;
                speed = invincibleSpeed;
            }
        }

        else
        {
            if (speed == invincibleSpeed)
                speed = preSpeed;


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
        }

        transform.position = Vector3.MoveTowards(transform.position, offPos, speed * Time.deltaTime);

        if (transform.position == offPos)
            GameManager.Pooling.RetunObjectPool(gameObject);
    }
}
