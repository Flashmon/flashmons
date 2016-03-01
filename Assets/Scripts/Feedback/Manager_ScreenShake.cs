using UnityEngine;
using System.Collections;

public class Manager_ScreenShake : MonoBehaviour {

    public Transform cameraShakeTransform;

    Vector3 originalCameraPosition;

    float shakeAmount = 0;

    public void ScreenShake(float var)
    {
        originalCameraPosition = cameraShakeTransform.localPosition;
        shakeAmount = var * .0025f;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);

    }

    void CameraShake()
    {
        if (shakeAmount > 0)
        {
            float quakeAmount = Random.value * shakeAmount * 2 - shakeAmount;
            Vector3 camPos = cameraShakeTransform.transform.position;
            camPos.y += quakeAmount;
            camPos.x += quakeAmount;
            cameraShakeTransform.transform.position = camPos;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        cameraShakeTransform.transform.localPosition = originalCameraPosition;
    }
}
