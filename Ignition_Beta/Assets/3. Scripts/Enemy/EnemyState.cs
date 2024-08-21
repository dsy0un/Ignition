using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour, IHitAble
{
    EnemyAnimation enemyAnim;
    EnemyMove enemyMove;

    private float maxHP = 100;
    private float currentHP;
    private float dmg = 10;

    private int playerLayer;
    private int lookOutLayer;

    public enum State{Idle, Attack, AttackWait}
    public State state = State.Idle;

    private float attackDelay = 2.5f;
    private float currentTime;
    private float attackDistance = 4.5f;
    private float distance;
    private bool isDead;
    private bool isAttacking;
    public bool IsDead {  get { return isDead; } }

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<EnemyAnimation>();
        enemyMove = GetComponent<EnemyMove>();
        currentHP = maxHP;
        currentTime = attackDelay;
        isDead = false;

        playerLayer = LayerMask.NameToLayer("Player");
        lookOutLayer = LayerMask.NameToLayer("LookOut");

        StartCoroutine(this.CheckState());
        StartCoroutine(this.StateForAction());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentTime <= attackDelay)
        {
            currentTime += Time.deltaTime;
        }
    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            distance = Vector3.Distance(enemyMove.target.transform.position, this.transform.position);

            if (distance <= attackDistance)
            {
                state = State.Attack;
            }
            else
            {
                state = State.Idle;
            }
        }
    }
    IEnumerator StateForAction()
    {
        while (!isDead)
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Attack:
                    Attack();
                    break;
            }
            yield return null;
        }
    }

    private void Attack()
    {
        if (currentTime >= attackDelay)
        {
            isAttacking = true;
            if (enemyMove.target.layer == playerLayer)
            {
                enemyAnim.SetTrigger("Bite");
                if (distance <= attackDistance)
                {
                    if (enemyMove.target.transform.TryGetComponent<IHitAble>(out var h))
                        h.Hit(dmg,"");
                }
            }
            else if (enemyMove.target.layer == lookOutLayer)
            {
                enemyAnim.SetTrigger("stinger");
            }
            currentTime = 0;
        }
    }

    public void Hit(float dmg, string coliName)
    {
        if (coliName == "WeakPoint")
        {
            dmg = 100;
            enemyAnim.SetTrigger("getHitHead");
        }
        else
        {
            enemyAnim.SetTrigger("getHit");
        }
        currentHP -= dmg;
        print(currentHP);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        enemyAnim.isDie(true);
    }
}
