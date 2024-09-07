using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class LookOut : MonoBehaviour
{
    [SerializeField]
    private Cooldown cooldown;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private EnemyGenerate Egen;

    private Animator animator;

    bool cool = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DefSuccessAnimation()
    {
        animator.SetTrigger("Success");

    }

    private void StartGen()
    {
        Egen.StartSpawn();
    }
    /// <summary>
    /// 쿨다운 보여주기 전 목표 시간까지 숫자가 차오르는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator CoolUpCO()
    {
        cool = !cool; //함수 끄고 켜기
        float time = 0;
        float count = 0;
        while (cool)
        {

            count += Time.deltaTime;
            time = Mathf.Lerp(0, cooldown.Cooltime, count / 1.5f);
            int sec = (int)time % 60;
            int min = (int)time / 60 % 60;
            int hour = (int)time / 3600 % 60;
            text.text = $"{hour:D2}:{min:D2}:{sec:D2}";
            yield return null;
        }
    }
    void Shake(float time)
    {
        StartCoroutine(GameManager.Instance.PlayerShake(time));
    }
}
