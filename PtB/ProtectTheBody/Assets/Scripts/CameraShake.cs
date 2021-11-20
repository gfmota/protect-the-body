using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeAmount;

    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("StartShake", 0, .02f);
        Invoke("StopShake", length);
    }

    private void StartShake()
    {
        float xValue = Random.Range(-1, 1) * shakeAmount;
        float yValue = Random.Range(-1, 1) * shakeAmount;
        transform.position = new Vector3(xValue, yValue, -1);
    }

    private void StopShake()
    {
        CancelInvoke("StartShake");
        transform.position = new Vector3(0, 0, -1);
    }
}
