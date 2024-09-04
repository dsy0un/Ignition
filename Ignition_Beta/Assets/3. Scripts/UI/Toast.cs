using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Toast : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI toastMsg;
    [SerializeField]
    float fadeTime = 0.3f;

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

    struct TOAST
    {
        public string msg;
        public float durationTime;
    }

    Queue<TOAST> queue = new();
    bool isPopUp;

    public void Show(string msg, float durationTime)
    {
        TOAST toast;

        toast.msg = msg;
        toast.durationTime = durationTime;

        queue.Enqueue(toast);
        if (!isPopUp) StartCoroutine(ShowToastQueue());
    }

    IEnumerator ShowToastQueue()
    {
        isPopUp = true;

        while (queue.Count != 0)
        {
            TOAST toast = queue.Dequeue();
            yield return StartCoroutine(ShowMessageCoroutine(toast.msg, toast.durationTime));
        }
    }

    IEnumerator ShowMessageCoroutine(string msg, float durationTime)
    {
        toastMsg.text = msg;
        toastMsg.enabled = true;

        yield return FadeInOut(toastMsg, fadeTime, true);

        float elapsedTime = 0.0f;
        while (elapsedTime < durationTime && !interrupt)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return FadeInOut(toastMsg, fadeTime, false);

        interrupt = false;
        toastMsg.enabled = false;
    }

    IEnumerator FadeInOut(TextMeshProUGUI target, float durationTime, bool inOut) 
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

        Color color = Color.clear;
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
