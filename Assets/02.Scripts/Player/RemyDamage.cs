using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyDamage : MonoBehaviour
{
    CamShake _camShake;

    readonly string obstacleTag = "OBSTACLE";
    readonly string coinTag = "COIN";

    [SerializeField] int HP = 0;
    int maxHP = 10;

    void Start()
    {
        _camShake = GetComponent<CamShake>();

        HP = maxHP;
    }

    void Update()
    {
        if (HP <= 0)
            GameManager.instance.isDie = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(obstacleTag) && HP > 0)
        {
            --HP;
            StartCoroutine(_camShake.ShakeCamera());
            GameManager.UI.UpdateHP(HP);
        }
    }

    public void ResetHP()
    {
        HP = maxHP;
        GameManager.UI.UpdateHP(HP);
    }
}
