using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IHitAble
{
    [SerializeField]
    private float maxHP = 100;
    [SerializeField]
    private float dmg = 10; 
    [SerializeField]
    private float attackCoolTime = 1.7f;
    [SerializeField]
    private float hitDelay = 1.2f;
    public NavMeshSurface nms;


    private float currentHP;
    private int volume; // 적이 쫒아 가는 우선순위
    private Vector3 target; // 쫒아갈 목표
    private Transform targetTrans;
    bool isMove = false; // 움직일 수 있는가
    bool isAttack = true; // 공격할 수 있는가
    bool isStiffen = false; // 경직 되었는가
    private int playerLayer; // 플레이어 레이어
    private int barrierLayer; // 적 레이어

    private EnemyAnimation enemyAnim;
    private NavMeshAgent nma;

    private void Awake()
    {
        enemyAnim = GetComponent<EnemyAnimation>();
        nma = GetComponent<NavMeshAgent>();

        playerLayer = LayerMask.NameToLayer("Player");
        barrierLayer = LayerMask.NameToLayer("Barrier");
    }
    private void Start()
    {
        StartCoroutine(CoroutineUpdate());
        currentHP = maxHP;
    }


    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            yield return null;
            if (!isStiffen)
            {
                if (isMove)
                {
                    nma.SetDestination(target);
                }
                //목표와 가까우면 실행
                if (targetTrans != null && nma.pathStatus == NavMeshPathStatus.PathComplete && nma.remainingDistance - nma.stoppingDistance < 0.1f)
                {
                    isMove = false;
                    if (targetTrans.TryGetComponent<IHitAble>(out var h) && isAttack && playerLayer == targetTrans.gameObject.layer)
                    {
                        enemyAnim.SetTrigger("Bite");
                        Debug.Log(12);
                        h.Hit(dmg, "");
                        StartCoroutine(CoolTime(attackCoolTime, isAttack, (result) => { isAttack = result; }));
                    }
                    else if (targetTrans.TryGetComponent<IHitAble>(out var h2) && isAttack && barrierLayer == targetTrans.gameObject.layer)
                    {
                        Debug.Log(2);
                        enemyAnim.SetTrigger("stinger");
                        h2.Hit(dmg, "");
                        StartCoroutine(CoolTime(attackCoolTime, isAttack, (result) => { isAttack = result; }));
                    }
                    else if (isAttack)
                    {
                        enemyAnim.SetTrigger("Bite");
                        StartCoroutine(CoolTime(attackCoolTime, isAttack, (result) => { isAttack = result; }));
                    }
                }
            }
            else 
                nma.ResetPath();
        }
    }

    public void ListenFollow(int sVolume, Transform sTarget) // 소리가 들리면 쫒아가게 설정하는 함수
    {
        if (volume <= sVolume && sTarget.gameObject.TryGetComponent<Collider>(out var col))
        {
            target = col.ClosestPoint(transform.position);
            targetTrans = sTarget;
            volume = sVolume;
            isMove = true;
        }
    }
    public void ListenReset() // 소리 범위 밖에 나갔을때 초기화 함수
    {
        isMove = false;
        nma.ResetPath();
        volume = 0;
        targetTrans = null;
    }
    /// <summary>
    /// 쿨 타임 함수
    /// </summary>
    /// <param name="time">쿨 타임</param>
    /// <param name="iseffectiveness">쿨 타임 걸고 싶은 is~ 변수 넣기</param>
    /// <returns>result에 bool값 반환</returns>
    IEnumerator CoolTime(float time, bool iseffectiveness, System.Action<bool> callback)
    {
        iseffectiveness = !iseffectiveness;
        callback?.Invoke(iseffectiveness);
        yield return new WaitForSeconds(time);
        iseffectiveness = !iseffectiveness;
        callback?.Invoke(iseffectiveness);
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
        StartCoroutine(CoolTime(hitDelay, isStiffen, (result) => { isStiffen = result; }));
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        isStiffen = true;
        enemyAnim.isDie(true);
        transform.GetChild(0).gameObject.SetActive(false);
        StopCoroutine(CoolTime(0,true, (re) => { }));

        //gameObject.SetActive(false);
    }
    public void Respawn()
    {
        if (!GameManager.Instance.enemyGenerate.canSpawn) return;
        isStiffen = false;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
