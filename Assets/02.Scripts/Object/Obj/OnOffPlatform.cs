using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffPlatform : MonoBehaviour
{
    GameObject invinciblePlatform;
    
    void Start()
        => invinciblePlatform = transform.GetChild(0).gameObject;

    void Update()
        => invinciblePlatform.SetActive(GameManager.instance.isInvincible);

    

}
