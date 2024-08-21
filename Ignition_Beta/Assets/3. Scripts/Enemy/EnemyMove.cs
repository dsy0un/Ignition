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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();
        enemyState = GetComponent<EnemyState>();

        curStunTime = 0;
        curAtkTime = 0;
    }

    void Update()
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
            nma.ResetPath();
            nma.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePosition;
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
}
