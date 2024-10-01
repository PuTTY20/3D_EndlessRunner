using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyCtrl : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    Animator ani;
    CapsuleCollider col;

    float moveValue = 0.8f; // 좌우로 한 번에 이동하는 거리

    float moveY => Input.GetAxisRaw("Vertical");
    float jumpForce = 5.0f;
    [SerializeField] bool isGround = true;   // 캐릭터가 바닥에 있는지 여부
    [SerializeField] bool isSlide = false;   // 슬라이딩 여부
    Vector3 initColCenter = new Vector3(0f, 1.9f, 0f);
    float initColHeight = 3.8f;

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
        col.center = new Vector3(0f, 2.3f, 0f);
        col.height = initColHeight / 2f;

        yield return new WaitForSeconds(0.8f);

        col.center = initColCenter;
        col.height = initColHeight;
        isGround = true;
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
        if (col.gameObject.CompareTag("PLATFORM"))
            isGround = true;
    }
}
