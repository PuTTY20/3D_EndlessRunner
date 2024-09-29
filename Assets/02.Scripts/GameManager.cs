using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> PlatformList = new List<GameObject>();
    Transform tr;

    void Start()
    {
        tr = transform;
    }

    
}