using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    Camera cam;
    Vector3 initPos;

    float duration = 0.2f;
    float shakeMag = 0.1f;

    void Start()
        => cam = Camera.main;

    public IEnumerator ShakeCamera()
    {
        initPos = cam.transform.position;
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