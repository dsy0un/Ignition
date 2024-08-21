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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();
        enemyState = GetComponent<EnemyState>();
    }

    void Update()
    {
        if (!enemyState.IsDead)
        {
            nma.SetDestination(target.transform.position);
            speedtovelo = new Vector3(nma.velocity.x, 0, nma.velocity.z);
            rb.velocity = speedtovelo;
        }
        else
        {
            nma.ResetPath();
            rb.velocity = Vector3.zero;
        }
    }
}
