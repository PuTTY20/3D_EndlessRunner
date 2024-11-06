using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    void Update()
        => transform.Rotate(Vector3.up * 360 * Time.deltaTime);
}
