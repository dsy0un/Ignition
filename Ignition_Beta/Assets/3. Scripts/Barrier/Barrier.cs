using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrier : MonoBehaviour, IHitAble
{
    [SerializeField]
    Image image;

    float maxHP = 100;
    float currentHP;

    void Awake()
    {
        
    }

    private void Start()
    {
        GameManager.Instance.barrier = gameObject.GetComponent<Barrier>();

        currentHP = maxHP;
    }

    void Update()
    {
        image.fillAmount = currentHP / maxHP;
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
