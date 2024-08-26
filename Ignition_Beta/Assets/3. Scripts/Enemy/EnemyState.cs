using System.Collections;
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
    private float currentTime;
    private float attackDelay = 2.5f;
    private float attackDistance = 5f;
    private float distance;
    private bool isDead;
    private bool inDistance;

    public bool IsDead {  get { return isDead; } }
    public bool IsStuned { get; set; }
    public bool IsAttack { get; set; }
    public bool InDistance { get { return inDistance; } }

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<EnemyAnimation>();
        enemyMove = GetComponent<EnemyMove>();
        currentHP = maxHP;
        currentTime = attackDelay;
        isDead = false;
        IsStuned = false;

        playerLayer = LayerMask.NameToLayer("Player");
        lookOutLayer = LayerMask.NameToLayer("Barrier");

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
                inDistance = true;
            }
            else
            {
                state = State.Idle;
                inDistance = false;
                currentTime = attackDelay;
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
            IsAttack = true;
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
        IsStuned = true;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        enemyAnim.isDie(true);
        transform.GetChild(0).gameObject.SetActive(false);
        currentHP = maxHP;
    }

    public void Respawn()
    {
        isDead = false;
        enemyAnim.isDie(false);
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.position = GetComponentInParent<EnemyGenerate>().GenEnemy();
        StartCoroutine(this.CheckState());
        StartCoroutine(this.StateForAction());
    }
}
