using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;

    public string defultScene = "Base";

    public bool test; // 테스트 용 true일 경우 무한 로딩

    private void Start()
    {
        StartCoroutine(LoadScene());
        if (nextScene == null)
        {
            nextScene = defultScene;
        }
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadScene()
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
                if (timer > 13f && !test)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
