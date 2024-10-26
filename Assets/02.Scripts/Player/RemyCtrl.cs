using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyCtrl : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    Animator ani;
    CapsuleCollider col;

    Vector3 initPos;
    Vector3 initColCenter = new Vector3(0f, 1.9f, 0f);
    Vector3 curPos;

    float initColHeight = 3.8f;
    float moveSize = 0.8f;
    float jumpForce = 5.0f;
    float damping = 5f;
    float timer = 0f;
    float coolDown = 3f;
    public bool isGround = true;
    public bool isSlide = false;
    public bool isPlatform = true;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        initPos = tr.position;
    }

    void Update()
    {
        // 좌우 이동 처리
        MoveHorizontal();

        // 점프
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround && !isSlide && isPlatform)
            StartCoroutine(Jump());
        // 슬라이드
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSlide && isGround)
            StartCoroutine(Slide());

        // 플랫폼 또는 장애물 체크
        CheckPlatform();

        // PlayerDie 처리
        DieCheck();
    }

    void MoveHorizontal()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && curPos.x >= -0.8f)
            curPos.x -= moveSize;

        if (Input.GetKeyDown(KeyCode.RightArrow) && curPos.x <= 0.8f)
            curPos.x += moveSize;

        float posX = Mathf.Clamp(curPos.x, -0.8f, 0.8f);
        curPos = new Vector3(posX, tr.position.y, 0f);

        tr.position = Vector3.Lerp(tr.position, curPos, Time.deltaTime * damping);
    }

    IEnumerator Jump()
    {
        ani.SetTrigger("Jump");
        rb.AddForce(jumpForce * rb.mass * Vector3.up, ForceMode.Impulse);

        //yield return null;
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

    RaycastHit GetPlatform(Vector3 dir, float distance)
    {
        Physics.Raycast(tr.position + Vector3.up * 0.3f, dir, out RaycastHit hit, distance);
        return hit;
    }

    void CheckPlatform()
    {
        RaycastHit hit = GetPlatform(Vector3.down, 50f);
        if (hit.collider == null)
        {
            isPlatform = false;
            isGround = false;
        }
        else
        {
            isPlatform = true;

            if (hit.distance < 0.31f)
                isGround = true;
            else
                isGround = false;
        }
    }

    void DieCheck()
    {
        if (!isPlatform)
        {
            timer += Time.deltaTime;
            if (timer > coolDown)
            {
                GameManager.instance.isDie = true;
                timer = 0f;
            }
        }
        else
            timer = 0f;
    }

    public void ResetRemy()
    {
        tr.position = initPos;
        rb.velocity = Vector3.zero;
        isGround = true;
        isSlide = false;
        GameManager.instance.isDie = false;
        isPlatform = true;
    }
}
