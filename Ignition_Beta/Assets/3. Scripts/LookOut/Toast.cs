using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toast : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;

    [SerializeField]
    TextMeshProUGUI toastMsg;
    [SerializeField]
    float fadeTime = 0.3f;

    bool interrupt;

    Camera mainCamera;

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
        mainCamera = GameManager.Instance.mainCamera;
        // Show("안녕하세요!", 10.0f, new Color(0.56f, 1, 0.43f));
    }

    private void Update()
    {
        FollowCamera();
    }

    /// <summary>
    /// UI가 카메라를 따라가기 위한 함수
    /// </summary>
    /// <returns>Null</returns>
    void FollowCamera()
    {
        transform.position = Vector3.Lerp(transform.position, mainCamera.transform.position 
            + mainCamera.transform.forward * offset.z 
            + mainCamera.transform.up * offset.y 
            + mainCamera.transform.right * offset.x, 
            3 * Time.deltaTime);

        Vector3 l_vector = mainCamera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-l_vector).normalized;
    }

    struct TOAST
    {
        public string msg;
        public float durationTime;
        public Color color;
    }

    Queue<TOAST> queue = new();
    bool isPopUp;

    /// <summary>
    /// Toast 메시지를 보여주기 위한 함수 (이걸로 메시지 실행)
    /// </summary>
    /// <param name="msg">메시지 내용</param>
    /// <param name="durationTime">보여주는 시간</param>
    /// <param name="color">메시지 색깔</param>
    public void Show(string msg, float durationTime, Color? color = null)
    {
        TOAST toast;

        toast.msg = msg;
        toast.durationTime = durationTime;
        toast.color = color ?? new Color(1, 1, 1, 0);

        queue.Enqueue(toast);
        if (!isPopUp) StartCoroutine(ShowToastQueue());
    }

    /// <summary>
    /// Toast 메시지를 보여주기 위한 큐 코루틴 함수
    /// </summary>
    /// <returns>Coroutine</returns>
    IEnumerator ShowToastQueue()
    {
        isPopUp = true;

        while (queue.Count != 0)
        {
            TOAST toast = queue.Dequeue();
            yield return StartCoroutine(ShowMessageCoroutine(toast.msg, toast.durationTime, toast.color));
        }
    }

    /// <summary>
    /// Toast 메시지를 보여주기 위함 코루틴 함수
    /// </summary>
    /// <param name="msg">메시지 내용</param>
    /// <param name="durationTime">보여주는 시간</param>
    /// <param name="color">메시지 색깔</param>
    /// <returns></returns>
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

    /// <summary>
    /// 글씨 천천히 사라지고 생기게 하기
    /// </summary>
    /// <param name="target">글씨</param>
    /// <param name="durationTime">사라지고 생기는 시간(초)</param>
    /// <param name="color">글씨 색깔</param>
    /// <param name="inOut">true = in -> out</param>
    /// <returns>Null</returns>
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

    /// <summary>
    /// Toast 메시지 바로 멈추고 싶을 때 쓰는 함수
    /// </summary>
    public void InterruptMessage()
    {
        interrupt = true;
    }
}
