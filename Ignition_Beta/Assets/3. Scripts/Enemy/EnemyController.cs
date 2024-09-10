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
    private Vector3 target; // 쫒아갈 목표
    bool isMove = false; // 움직일 수 있는가

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
            if (isMove)
            {
                nma.SetDestination(target);
            }
        }
    }

    public void ListenFollow(int sVolume, Transform sTarget) // 소리가 들리면 쫒아가게 설정하는 함수
    {
        if (volume <= sVolume && sTarget.gameObject.TryGetComponent<Collider>(out var col))
        {
            Debug.Log(col.ClosestPoint(transform.position));
            target = col.ClosestPoint(transform.position);
            volume = sVolume;
            isMove = true;
        }
    }
    public void ListenReset() // 소리 범위 밖에 나갔을때 초기화 함수
    {
        isMove = false;
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
