using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour, IHitAble
{
    EnemyAnimation enemyAnim;
    private float maxHP = 100;
    private float currentHP;
    private float atk = 10;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<EnemyAnimation>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Hit(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        enemyAnim.isDie(true);
    }
}
