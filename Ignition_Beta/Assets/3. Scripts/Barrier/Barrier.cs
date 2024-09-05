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

    void Awake()
    {
        
    }

    private void Start()
    {
        currentHP = maxHP;
        StartCoroutine(CoroutineUpdate());
    }

    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            image.fillAmount = currentHP / maxHP;
            yield return null;
        }
    }

    public void Hit(float dmg, string coliName)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {

    }
}
