using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toast : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI toastMsg;
    [SerializeField]
    float fadeTime = 0.3f;

    private static Toast instance = null;
    public static Toast Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Toast>();
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;

        StartCoroutine(ShowMessageCoroutine());
    }

    struct TOAST
    {
        public string msg;
        public float durationTime;
    }

    Queue<TOAST> queue = new();
    bool isPopUp;

    public void ShowMessage(string msg, float durationTime)
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
            yield return StartCoroutine(ShowMessageCoroutine());
        }
    }

    IEnumerator ShowMessageCoroutine()
    {
        Debug.Log(1);
        yield return StartCoroutine(ShowMessageCoroutine());
    }
}
