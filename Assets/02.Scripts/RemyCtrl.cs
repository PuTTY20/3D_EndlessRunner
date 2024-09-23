using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyCtrl : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    Animator ani;

    float moveValue = 0.8f; // 좌우로 한 번에 이동하는 거리

    float moveY => Input.GetAxisRaw("Vertical");
    float jumpForce = 5.0f;   // 점프 시 힘
    [SerializeField] bool isGround = true;   // 캐릭터가 바닥에 있는지 여부
    [SerializeField] bool isSlide = false;   // 슬라이딩 여부

    public bool playerDie = false;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // 좌우 입력
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            tr.position += new Vector3(-moveValue, 0, 0);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            tr.position += new Vector3(moveValue, 0, 0);

        // 상하 입력
        if (moveY > 0 && isGround) StartCoroutine(Jump());
        if (moveY < 0 && !isSlide) StartCoroutine(Slide());
    }

    IEnumerator Jump()
    {
        ani.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGround = false;

        yield return new WaitForSeconds(0.8f);

        isGround = true;
    }

    IEnumerator Slide()
    {
        ani.SetTrigger("Slide");
        isSlide = true;

        yield return new WaitForSeconds(1f);
        
        isSlide = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("PLATFORM"))
            isGround = true;
    }
}
