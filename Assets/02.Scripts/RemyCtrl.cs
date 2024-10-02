using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyCtrl : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    Animator ani;
    CapsuleCollider col;
    Vector3 initColCenter = new Vector3(0f, 1.9f, 0f);
    
    float moveY => Input.GetAxisRaw("Vertical");
    float initColHeight = 3.8f;
    float moveValue = 0.8f;
    float jumpForce = 5.0f;
    public bool isGround = true;
    public bool isSlide = false;
    public bool playerDie = false;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        // 좌우 이동 처리
        MoveHorizontal();

        if (moveY > 0 && isGround) 
            StartCoroutine(Jump());
        if (moveY < 0 && !isSlide) 
            StartCoroutine(Slide());
    }

    void MoveHorizontal()
    {
        Vector3 currentPos = tr.position;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentPos.x > -0.8f)
            currentPos.x -= moveValue;

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentPos.x < 0.8f)
            currentPos.x += moveValue;

        // x값을 제한 범위 내로 고정
        currentPos.x = Mathf.Clamp(currentPos.x, -0.8f, 0.8f);

        currentPos.z = 0f;
        tr.position = currentPos;
    }

    IEnumerator Jump()
    {
        ani.SetTrigger("Jump");

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isGround = false;
        col.center = new Vector3(0f, 2.3f, 0f);
        col.height = initColHeight / 2f;

        yield return new WaitForSeconds(0.8f);

        col.center = initColCenter;
        col.height = initColHeight;
    }

    IEnumerator Slide()
    {
        ani.SetTrigger("Slide");
        isSlide = true;
        col.center = new Vector3(0f, 0.7f, 0f);
        col.height = 1.4f;

        yield return new WaitForSeconds(1f);

        col.center = initColCenter;
        col.height = initColHeight;

        isSlide = false;
    }

    void OnCollisionEnter(Collision col)
    {
        // 플랫폼과 충돌하면 isGround를 true로 설정
        if (col.gameObject.CompareTag("PLATFORM"))
        {
            isGround = true;
            Debug.Log(isGround);
        }
    }

    void OnCollisionExit(Collision col)
    {
        // 플랫폼에서 나가면 isGround를 false로 설정
        if (col.gameObject.CompareTag("PLATFORM"))
        {
            isGround = false;
            Debug.Log(isGround);
        }
    }
}
