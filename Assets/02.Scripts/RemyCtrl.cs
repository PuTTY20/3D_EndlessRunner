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
    float initColHeight = 3.8f;
    float moveValue = 0.8f;
    float jumpForce = 5.0f;
    public bool isGround = true;
    public bool isSlide = false;
    float moveY => Input.GetAxisRaw("Vertical");

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
        // 현재 위치 값 가져오기
        Vector3 currentPosition = tr.position;

        // 좌우 입력
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentPosition.x > -0.8f)
            currentPosition.x -= moveValue; // 좌측 이동

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentPosition.x < 0.8f)
            currentPosition.x += moveValue; // 우측 이동

        currentPosition.x = Mathf.Clamp(currentPosition.x, -0.8f, 0.8f);
        currentPosition.z = 0f;
        tr.position = currentPosition;

        // 상하 입력
        if (moveY > 0 && isGround) StartCoroutine(Jump());
        if (moveY < 0 && !isSlide) StartCoroutine(Slide());
    }

    IEnumerator Jump()
    {
        ani.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGround = false; // 점프 시 isGround를 false로 설정
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
        if (col.gameObject.CompareTag("PLATFORM"))
            isGround = true;
    }

    void OnCollisionExit(Collision col)
    {
        // 플랫폼에서 나갈 때 isGround를 false로 설정
        if (col.gameObject.CompareTag("PLATFORM"))
            isGround = false;
    }
}
