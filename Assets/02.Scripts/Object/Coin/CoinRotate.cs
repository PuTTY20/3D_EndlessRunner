using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    Transform tr;

    void Start()
    {
        tr = transform;
    }

    void Update()
    {
        tr.Rotate(Vector3.up * 360 * Time.deltaTime);
    }
}
