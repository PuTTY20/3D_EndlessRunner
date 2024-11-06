using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDetect : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out RemyCtrl _remy))
            transform.parent.GetComponent<Coin>().OffCoin();
    }
}
