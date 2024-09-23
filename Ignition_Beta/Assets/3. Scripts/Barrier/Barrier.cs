using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrier : MonoBehaviour, IHitAble
{
    [SerializeField]
    Image image;
    [SerializeField]
    float maxHP = 100;
    [SerializeField]
    float currentHP;
    [SerializeField]
    float breakTime;
    float currentTime;

    bool isEscape;

    bool isUpgrade = false;

    void Awake()
    {

    }

    private void Start()
    {
        isEscape = false;
        currentHP = maxHP;
        currentTime = breakTime;
        StartCoroutine(CoroutineUpdate());
    }

    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            if (!isUpgrade && GameManager.Instance.isBarrierUpgrade)
            {
                isUpgrade = true;
                currentHP *= 1.5f;
                maxHP *= 1.5f;
            }
            image.fillAmount = currentHP / maxHP;

            if (isEscape)
            {
                currentTime -= Time.deltaTime;
                int min = (int)currentTime / 60 % 60;
                switch (currentTime)
                {
                    case float n when (n <= 30f && n >= 10f):
                        GameManager.Instance.window.windowTimer.text = $"붕괴까지 남은 시간 : <color=orange>{min:D2}:{currentTime:00.00}</color>";
                        break;
                    case float n when (n <= 10f):
                        GameManager.Instance.window.windowTimer.text = $"붕괴까지 남은 시간 : <color=red>{min:D2}:{currentTime:00.00}</color>";
                        break;
                    default:
                        GameManager.Instance.window.windowTimer.text = $"붕괴까지 남은 시간 : <color=white>{min:D2}:{currentTime:00.00}</color>";
                        break;
                }
            }

            yield return null;
        }
    }

    public void Hit(float dmg, string coliName)
    {
        currentHP -= dmg;
        if (currentHP <= 0 && !isEscape)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.DefFailureEvent();
        StartCoroutine(Escape(breakTime));
        isEscape = true;
    }

    public IEnumerator Escape(float time = 0)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
        GameManager.Instance.DefEscapeEvent();
        gameObject.SetActive(false);
    }

    public void Respawn()
    {

    }
}
