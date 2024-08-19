using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour, IHitAble
{
    EnemyAnimation enemyAnim;
    private float maxHP = 100;
    private float currentHP;
    private float atk = 10;

    private int playerLayer;
    private int lookOutLayer;

    public enum State{Idle, Attack, AttackWait}
    public State state = State.Idle;

    private EnemyMove enemyMove;
    private float attackDelay = 2.5f;
    private float currentTime;
    private float attackDistance = 4.3f;
    private bool isDead;

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

            float distance = Vector3.Distance(enemyMove.target.transform.position, this.transform.position);

            print(enemyMove.target.layer == lookOutLayer);

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
                    if (currentTime >= attackDelay)
                    {
                        if (enemyMove.target.layer == playerLayer)
                        {
                            enemyAnim.AttackAnim("Bite");
                            print(1);
                        }
                        else if (enemyMove.target.layer == lookOutLayer)
                        {
                            enemyAnim.AttackAnim("stinger");
                            print(2);
                        }
                        currentTime = 0;
                    }
                    break;
            }
            yield return null;
        }
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
        isDead = true;
        enemyAnim.isDie(true);
    }
}
