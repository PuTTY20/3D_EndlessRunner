using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetect : MonoBehaviour
{
    Transform tr;
    Camera cam;
    Vector3 initPos;

    string obstacleTag = "OBSTACLE";
    float duration = 0.2f;
    float shakeMag = 0.1f;

    void Start()
    {
        tr = transform;
        cam = Camera.main;

        initPos = cam.transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(obstacleTag))
            StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMag;
            cam.transform.position = initPos + randomOffset;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        cam.transform.position = initPos;
    }
}
