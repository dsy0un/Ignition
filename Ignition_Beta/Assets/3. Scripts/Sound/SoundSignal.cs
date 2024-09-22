using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SoundSignal : MonoBehaviour
{
    [SerializeField, Tooltip("인식시키고 싶은 오브젝트")]
    private AudioSource audioSource;
    [SerializeField, Tooltip("Pivot Transform \nCollider가 존재해야 하며 그 오브젝트에 Ihitable이 존재해야함")]
    private Transform followObject;
    [SerializeField, Min(1), Tooltip("소리 크기 (적이 인식하는 우선순위)")]
    private int soundVolume = 1;

    [SerializeField]
    private LayerMask layer;

    private Collider[] colliders;
    private float audioMaxDistance;
    private List<GameObject> objectsWithinRange = new List<GameObject>(); // 이전 프레임 감지 객체 리스트
    private List<GameObject> currentObjects = new List<GameObject>(); // 현재 프레임 감지 객체 리스트

    private void Awake()
    {
    }

    void Start()
    {
        audioMaxDistance = audioSource.maxDistance;
        StartCoroutine(SoundSignalToTheEnemy());
    }

    IEnumerator SoundSignalToTheEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (!audioSource.isPlaying) yield break;

            DetectObjectsInRange();

            // 오디오 소리에 반응하는 적 감지
            if (colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    if (col.transform.root.TryGetComponent<EnemyController>(out var enemyController))
                    {
                        enemyController.ListenFollow(soundVolume, followObject); // 적이 소리를 감지했을 때 행동
                    }
                }
            }
        }
    }

    void DetectObjectsInRange()
    {
        // 현재 프레임에서 감지된 객체들을 갱신하기 전에 초기화
        currentObjects.Clear();

        // 현재 감지 범위 내의 객체들 감지
        colliders = Physics.OverlapSphere(followObject.position, audioMaxDistance, layer);

        // 현재 감지된 객체들을 리스트에 추가
        foreach (Collider collider in colliders)
        {
            currentObjects.Add(collider.transform.root.gameObject);
        }

        // 이전 프레임에 감지되었으나, 이번 프레임에 감지되지 않은 객체를 감지 (즉, 범위 밖으로 나간 객체)
        foreach (GameObject obj in objectsWithinRange)
        {
            if (!currentObjects.Contains(obj))
            {
                OnObjectExitRange(obj); // 범위 밖으로 나간 객체에 대한 이벤트
            }
        }

        // 이전 프레임 리스트 갱신: 이번 프레임에 감지된 객체들을 저장
        objectsWithinRange.Clear();
        objectsWithinRange.AddRange(currentObjects);
    }

    // 객체가 범위를 벗어났을 때 실행할 함수
    void OnObjectExitRange(GameObject obj)
    {
        if (obj.TryGetComponent<EnemyController>(out var enemyController))
        {
            Debug.Log(obj.name + "가 감지 범위를 벗어났습니다.");
            enemyController.ListenReset();
        }
        // 범위를 벗어났을 때 원하는 이벤트 처리

    }
}









//아 왜 T1짐?????????????????????????????????? 이걸 결승을 못가네;;
//아 왜 T1짐?????????????????????????????????? 이걸 결승을 못가네;;
//아 왜 T1짐?????????????????????????????????? 이걸 결승을 못가네;;
//아 왜 T1짐?????????????????????????????????? 이걸 결승을 못가네;;
//아 왜 T1짐?????????????????????????????????? 이걸 결승을 못가네;;
//아 왜 T1짐?????????????????????????????????? 이걸 결승을 못가네;;