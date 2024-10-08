using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemyCtrl : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    Animator ani;
    CapsuleCollider col;
    [SerializeField] Text score_txt;

    Vector3 initColCenter = new Vector3(0f, 1.9f, 0f);
    float moveY => Input.GetAxisRaw("Vertical");
    float initColHeight = 3.8f;
    float moveValue = 0.8f;
    float jumpForce = 5.0f;
    public int score = 0;
    public bool isGround = true;
    public bool isSlide = false;
    public bool isDie = false;
    public bool isPlatform = false;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        score_txt = GameObject.Find("Canvas").transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        // 좌우 이동 처리
        MoveHorizontal();

        if (!isDie)
        {
            score += Mathf.FloorToInt(Time.deltaTime * 10); // 초당 점수 증가량 설정
            score_txt.text = $"{score}M";
        }

        // 점프 및 슬라이드 입력 처리
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
            isGround = true;
    }
}
