using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Rigidbody rb;
    public NavMeshSurface nms;
    public GameObject target;

    private NavMeshAgent nma;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();
        nms.BuildNavMesh();
    }

    void Update()
    {
        nma.SetDestination(target.transform.position);
        Vector3 speedX = new Vector3(nma.speed, 0, 0);

    }
}
