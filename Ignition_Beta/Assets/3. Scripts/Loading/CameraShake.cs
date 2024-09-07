using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CameraShake : MonoBehaviour
{
    public float shakeSpeed = 1.0f;
    public float shakeAmount = 0.2f;
    public float vibrate = 0.1f;
    

    public Transform port;

    public SteamVR_Action_Vibration hapticAction;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shake());
    }

    // Update is called once per frame
    void Update()
    {
        int ran = Random.Range(0, 2);
        Pulse(.01f, 150, ran, SteamVR_Input_Sources.LeftHand);
        Pulse(.01f, 150, ran, SteamVR_Input_Sources.RightHand);
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
    }


    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);
    }
}