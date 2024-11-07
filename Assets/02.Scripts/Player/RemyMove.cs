using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyMove : MonoBehaviour
{
    RemyCtrl remy;

    Animator ani;
    CapsuleCollider col;

    internal Vector3 curPos;
    Vector3 initColCenter;

    float initColHeight = 3.8f;
    float moveSize = 0.8f;
    float jumpForce = 5.0f;
    float damping = 5f;
    int hashJump = Animator.StringToHash("Jump");
    int hashSlide = Animator.StringToHash("Slide");

    void Start()
    {
        remy = GetComponent<RemyCtrl>();
        ani = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        initColCenter = new Vector3(0f, 1.9f, 0f);
    }

    void Update()
    {
        // 좌우 이동 처리
        MoveHorizontal();

        // 점프
        if (Input.GetKeyDown(KeyCode.UpArrow) && remy.isGround && !remy.isSlide && remy.isPlatform)
            StartCoroutine(Jump());
        // 슬라이드
        if (Input.GetKeyDown(KeyCode.DownArrow) && !remy.isSlide && remy.isGround)
            StartCoroutine(Slide());
    }

    void MoveHorizontal()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && curPos.x >= -0.8f)
            curPos.x -= moveSize;

        if (Input.GetKeyDown(KeyCode.RightArrow) && curPos.x <= 0.8f)
            curPos.x += moveSize;

        float posX = Mathf.Clamp(curPos.x, -0.8f, 0.8f);
        curPos = new Vector3(posX, transform.position.y, 0f);

        transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * damping);
    }

    IEnumerator Jump()
    {
        ani.SetTrigger(hashJump);
        remy.rb.AddForce(jumpForce * remy.rb.mass * Vector3.up, ForceMode.Impulse);

        //yield return null;
        col.center = new Vector3(0f, 2.3f, 0f);
        col.height = initColHeight / 2f;

        yield return new WaitForSeconds(0.8f);

        col.center = initColCenter;
        col.height = initColHeight;
    }

    IEnumerator Slide()
    {
        ani.SetTrigger(hashSlide);
        remy.isSlide = true;
        col.center = new Vector3(0f, 0.7f, 0f);
        col.height = 1.4f;

        yield return new WaitForSeconds(1f);

        col.center = initColCenter;
        col.height = initColHeight;

        remy.isSlide = false;
    }
}
