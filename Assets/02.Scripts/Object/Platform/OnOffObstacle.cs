using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffObstacle : MonoBehaviour
{
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.3f, Vector3.down, out hit, 2f))
        {
            if (hit.collider != null)
                gameObject.SetActive(true);
        }

        else gameObject.SetActive(false);
    }
}
