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
            if(target != null )
                nma.SetDestination(target.position);

        }
    }

    public void ListenFollow(int sVolume, Transform sTarget) // 소리가 들리면 쫒아가게 설정하는 함수
    {
        target = sTarget;
        volume = sVolume;
    }
    public void ListenReset() // 소리 범위 밖에 나갔을때 초기화
    {
        nma.ResetPath();
        volume = 0;
        target = null;
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
