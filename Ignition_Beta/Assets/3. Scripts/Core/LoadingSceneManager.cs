using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;

    public string defultScene = "Base";

    public bool test; // 테스트 용 true일 경우 무한 로딩

    public GameObject LoadingText;
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;
    public Image progressBar;
    [SerializeField, Tooltip("로딩되는 시간(실제 로딩과는 별개의 시간임 최소 10초)"), Min(10f)]
    private float loadingDuration;
    [SerializeField]
    private bool dron;

    private void Start()
    {
        if (nextScene == null)
        {
            nextScene = defultScene;
        }
        if (dron) StartCoroutine(LoadSceneDron());
        else StartCoroutine(LoadScenePort());
        switch (nextScene) // 동적으로 바꾸기
        {
            case "Stage1":
                text2.text = "목적지 : 협곡";
                break;
            case "Stage2":
                text2.text = "목적지 : 돌 산";
                break;
            case "Stage3":
                text2.text = "목적지 : 사막";
                break;
            case "Stage4":
                text2.text = "목적지 : 오염된 숲";
                break;

        }
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        //SceneManager.LoadScene("Loading");
    }

    public static void Load(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading2");
    }

    IEnumerator LoadScenePort()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        float timer2 = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            switch (timer)
            {
                case float n when (n >= 1.5f && n <= 9f):
                    timer2 += Time.deltaTime;
                    progressBar.fillAmount = Mathf.Lerp(0 , 1, timer2 / 7.5f);
                    text.text = $"목표까지 남은거리";
                    break;
                case float n when (n >= 9.5f):
                    progressBar.gameObject.SetActive(false);
                    if (op.progress < 0.9f || timer < loadingDuration) // 실제 로딩 체크
                    {
                        text.gameObject.SetActive(false);
                        LoadingText.gameObject.SetActive(true);
                        if (op.progress > 0.9)
                            Debug.Log("실제 로딩");
                    }
                    else if (timer > loadingDuration + 3f && !test) // 로딩 완료시 씬 전환
                    {
                        text.text = $"<color=red>착륙!</color>";
                        op.allowSceneActivation = true;
                        yield break;
                    }
                    else if (timer > loadingDuration + 2f) // 자연스러운 로딩 텍스트 
                    {
                        text.text = $"착륙 중...";
                    }
                    else if (timer > loadingDuration + 1.5f)
                    {
                        text.text = $"착륙 중..";
                    }
                    else if (timer > loadingDuration + 1f)
                    {
                        text.text = $"착륙 중.";
                    }
                    else if (timer > loadingDuration) 
                    {
                        text.text = $"착륙 지점 확보!";
                        text.gameObject.SetActive(true);
                        LoadingText.gameObject.SetActive(false);
                    }
                    break;
            }
            //if (op.progress < 0.9f)
            //{
                
            //}
            //else
            //{
            //    if (timer > 13f && !test)
            //    {
            //        op.allowSceneActivation = true;
            //        yield break;
            //    }
            //}
        }
    }
    IEnumerator LoadSceneDron()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {

            }
            else
            {
                if (timer > loadingDuration && !test)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
