using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Rigidbody rb;
    EnemyState enemyState;
    public NavMeshSurface nms;
    public GameObject target;

    private Vector3 speedtovelo;
    private NavMeshAgent nma;

    private float stunTime = 0.5f;
    private float atkTime = 1f;
    private float curStunTime;
    private float curAtkTime;
    private float hideBody = 4;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();
        enemyState = GetComponent<EnemyState>();

    }
    void Start()
    {
        curStunTime = 0;
        curAtkTime = 0;
        StartCoroutine(CoroutineUpdate());
    }

    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            if (!enemyState.IsDead)
            {
                if (enemyState.IsStuned || enemyState.IsAttack || enemyState.InDistance)
                    StopMove();
                if (curStunTime >= stunTime || curAtkTime >= atkTime && !enemyState.InDistance)
                    StartMove();
                nma.SetDestination(target.transform.position);
                speedtovelo = new Vector3(nma.velocity.x, 0, nma.velocity.z);
                rb.velocity = speedtovelo;
            }
            else
            {
                StartCoroutine(Die());
            }
            yield return null;
        }
    }

    private void StopMove()
    {
        nma.isStopped = true;
        nma.velocity = Vector3.zero;
        if (enemyState.IsStuned)
            curStunTime += Time.deltaTime;
        if (enemyState.IsAttack)
            curAtkTime += Time.deltaTime;
    }
    private void StartMove()
    {
        nma.isStopped = false;
        if (enemyState.IsStuned)
        {
            enemyState.IsStuned = false;
            curStunTime = 0;
        }
        if (enemyState.IsAttack)
        {
            enemyState.IsAttack = false;
            curAtkTime = 0;
        }
    }

    IEnumerator Die()
    {
        nma.ResetPath();
        nma.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(hideBody);
        gameObject.SetActive(false);
        rb.constraints = RigidbodyConstraints.None;
        enemyState.Respawn();
    }
    public IEnumerator BackMove(Vector3 dir)
    {
        Debug.Log(1);
        StopMove();
        yield return new WaitForSeconds(0.2f);
        rb.AddRelativeForce(dir * 100f, ForceMode.Acceleration);
        yield return new WaitForSeconds(1f);
        StartMove();
    }
}
