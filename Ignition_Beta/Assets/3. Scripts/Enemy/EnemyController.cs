using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IHitAble
{
    [SerializeField]
    private float maxHP = 100;
    [SerializeField]
    private float dmg = 10;

    private float currentHP;

    private EnemyAnimation enemyAnim;

    public void Hit(float dmg, string coliName)
    {

    }
    public void Die()
    {

    }
    public void Respawn()
    {

    }
}
