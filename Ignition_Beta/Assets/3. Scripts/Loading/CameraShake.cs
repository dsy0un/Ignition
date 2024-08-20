using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //public float shakeTime = 1.0f;
    public float shakeSpeed = 1.0f;
    public float shakeAmount = 0.2f;

    public Transform port;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shake());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        Vector3 originPosition = port.localPosition;
        float elapsedTime = 0.0f;

        while (true)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
            port.localPosition = Vector3.Lerp(port.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        //port.localPosition = originPosition;
    }
}
