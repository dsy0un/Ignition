using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class Toast : MonoBehaviour
{
    Vector3 offset = new Vector3(0, 0, 10);
    Camera mainCamera = Camera.main;

    [SerializeField]
    TextMeshProUGUI toastMsg;
    [SerializeField]
    float fadeTime = 0.3f;

    Color color;

    bool interrupt;

    private static Toast instance;
    public static Toast Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FollowCamera());
        Show("안녕하세요!", 10.0f, new Color(0.56f, 1, 0.43f));
    }

    IEnumerator FollowCamera()
    {
        while (true)
        {
            Debug.Log(1);
            transform.position = mainCamera.transform.position 
                + mainCamera.transform.forward * offset.z
                + mainCamera.transform.up * offset.y
                + mainCamera.transform.right * offset.x;

            transform.LookAt(mainCamera.transform);

            yield return null;
        }
    }

    struct TOAST
    {
        public string msg;
        public float durationTime;
        public Color color;
    }

    Queue<TOAST> queue = new();
    bool isPopUp;

    public void Show(string msg, float durationTime, Color? color = null)
    {
        TOAST toast;

        toast.msg = msg;
        toast.durationTime = durationTime;
        toast.color = color ?? new Color(1, 1, 1, 0);

        queue.Enqueue(toast);
        if (!isPopUp) StartCoroutine(ShowToastQueue());
    }

    IEnumerator ShowToastQueue()
    {
        isPopUp = true;

        while (queue.Count != 0)
        {
            TOAST toast = queue.Dequeue();
            yield return StartCoroutine(ShowMessageCoroutine(toast.msg, toast.durationTime, toast.color));
        }
    }

    IEnumerator ShowMessageCoroutine(string msg, float durationTime, Color color)
    {
        toastMsg.text = msg;
        toastMsg.color = color;
        toastMsg.enabled = true;

        yield return FadeInOut(toastMsg, fadeTime, color, true);

        float elapsedTime = 0.0f;
        while (elapsedTime < durationTime && !interrupt)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return FadeInOut(toastMsg, fadeTime, color, false);

        interrupt = false;
        toastMsg.enabled = false;
    }

    IEnumerator FadeInOut(TextMeshProUGUI target, float durationTime, Color color, bool inOut) 
    {
        float start, end;
        if (inOut)
        {
            start = 0.0f;
            end = 1.0f;
        }
        else
        {
            start = 1.0f;
            end = 0.0f;
        }

        float elapsedTime = 0.0f;

        while (elapsedTime < durationTime)
        {
            float alpha = Mathf.Lerp(start, end, elapsedTime / durationTime);

            target.color = new Color(color.r, color.g, color.b, alpha);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void InterruptMessage() /* 필요한 곳에서 호출 */
    {
        interrupt = true;
    }
}
