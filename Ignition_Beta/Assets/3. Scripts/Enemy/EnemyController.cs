using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Unity.Burst.Intrinsics.X86;

public class EnemyController : MonoBehaviour, IHitAble
{
    [SerializeField]
    private float maxHP = 100;
    [SerializeField]
    private float dmg = 10; 
    public NavMeshSurface nms;


    private float currentHP;
    private int volume; // 적이 쫒아 가는 우선순위
    private Transform target; // 쫒아갈 목표

    private EnemyAnimation enemyAnim;
    private NavMeshAgent nma;

    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        StartCoroutine(CoroutineUpdate());
    }


    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            yield return null;
            Debug.Log(32);
            nma.SetDestination(target.position);

        }
    }

    public void ListenFollow(int sVolume, Transform sTarget)
    {
        target = sTarget;
        volume = sVolume;
    }
    public void ListenReset()
    {
        nma.ResetPath();
        volume = 0;
    }

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
