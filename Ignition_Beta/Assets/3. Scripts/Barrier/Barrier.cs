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
    float currentHP;
    [SerializeField]
    float breakTime;
    float currentTime;

    bool isEscape;

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
            image.fillAmount = currentHP / maxHP;   
            Debug.Log(2);
            if (isEscape)
            {
                Debug.Log(1);
                currentTime -= Time.deltaTime;
                int min = (int)currentTime / 60 % 60;
                GameManager.Instance.window.windowTimer.text = $"\nºØ±«±îÁö ³²Àº ½Ã°£ {min:D2}:{currentTime:00.00}";
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
        StartCoroutine(Escape());
        isEscape = true;
        Debug.Log(isEscape);
    }

    IEnumerator Escape()
    {
        yield return new WaitForSeconds(breakTime);
        GameManager.Instance.DefEscapeEvent();
        gameObject.SetActive(false);
    }

    public void Respawn()
    {

    }
}
