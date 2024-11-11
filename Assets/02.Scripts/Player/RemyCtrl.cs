using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyCtrl : MonoBehaviour
{
    RemyMove _move;

    Transform tr;
    SkinnedMeshRenderer[] rend;
    internal Rigidbody rb;

    Vector3 initPos;

    float DieTimer = 0f;
    float timeToDie = 2f;
    float blinkInterval = 0.1f;
    float minAlpha = 0.3f;
    float maxAlpha = 1f;
    public bool isGround = true;
    public bool isSlide = false;
    public bool isPlatform = true;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        _move = GetComponent<RemyMove>();
        rend = GetComponentsInChildren<SkinnedMeshRenderer>();
        initPos = new Vector3(0f, tr.position.y, tr.position.z);
    }

    void Update()
    {
        // 플랫폼 또는 장애물 체크
        CheckPlatform();

        // PlayerDie 처리
        DieCheck();

        if (GameManager.instance.isInvincible)
            StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        bool isTransparent = false;

        while (GameManager.instance.isInvincible)
        {
            // 모든 SkinnedMeshRenderer의 Material에 대해 투명도 조절
            foreach (var renderer in rend)
            {
                foreach (var mat in renderer.materials)
                {
                    Color color = mat.color;
                    color.a = isTransparent ? minAlpha : maxAlpha;
                    mat.color = color;
                }
            }

            // 투명도를 번갈아가며 적용
            isTransparent = !isTransparent;
            yield return new WaitForSeconds(blinkInterval);
        }

        // 깜빡임 종료 후 모든 Material의 투명도를 최댓값으로 설정
        foreach (var renderer in rend)
        {
            foreach (var mat in renderer.materials)
            {
                Color color = mat.color;
                color.a = maxAlpha;
                mat.color = color;
            }
        }
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
        if (!isPlatform && !GameManager.instance.isInvincible)
        {
            DieTimer += Time.deltaTime;
            if (DieTimer > timeToDie)
            {
                GameManager.instance.isDie = true;
                DieTimer = 0f;
            }
        }
        else DieTimer = 0f;
    }

    public void ResetRemy()
    {
        _move.curPos = Vector3.zero;
        tr.position = initPos;
        rb.velocity = Vector3.zero;
        isGround = true;
        isSlide = false;
        GameManager.instance.isDie = false;
        isPlatform = true;
    }
}
