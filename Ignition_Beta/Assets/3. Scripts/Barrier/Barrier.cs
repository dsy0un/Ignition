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

    float timer;
    bool isBreakBarrier;

    void Awake()
    {

    }

    private void Start()
    {
        currentHP = maxHP;
        timer = 0;
        StartCoroutine(CoroutineUpdate());
    }

    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            image.fillAmount = currentHP / maxHP;

            if (isBreakBarrier) timer += 

            yield return null;
        }
    }

    public void Hit(float dmg, string coliName)
    {
        Debug.Log(currentHP);
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        
        GameManager.Instance.DefFailureEvent();
        // gameObject.SetActive(false);
    }

    public void Respawn()
    {

    }
}
