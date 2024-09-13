using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private VisualEffect muzzelFlash;
    [SerializeField] private GameObject muzzleLight;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip emptyShotSound;
    [SerializeField] private float fireTime;
    [SerializeField] private bool ableAutomaticFire;
    public bool isShotgun;
    public int spawnPrefabAmount;
    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();
    private bool redyToFire;

    public float shootingSpeed = 1f;
    private int fireMode = 1;
    public GameObject bulletPref;

    public Interactable interactable;
    public Socket socket;
    public Bolt bolt;
    private MagazineSystem magazineSystem;
    private Hand currentHand = null;

    [Header("Recoil")]
    public float maxRecoil;
    public float minRecoil;
    public Interactable secondInteractable;
    private Rigidbody gunRb;
    private float recoilPower;

    [Header("Action Button")]
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean ejectMagazine;
    public SteamVR_Action_Boolean changeFireMode;

    [HideInInspector]
    public bool isGrab;

    private void Awake()
    {
        gunRb = GetComponent<Rigidbody>();
        Initialize(spawnPrefabAmount);
    }

    private void Start()
    {
        StartCoroutine("GunWork");
    }

    IEnumerator GunWork()
    {
        while (true)
        {
            if (socket.isMagazine) // 탄창이 있을 경우 스크립트 가져오기
                magazineSystem = GetComponentInChildren<MagazineSystem>();
            else // 아닌경우 스크립트 NULL
                magazineSystem = null;
            bolt.magazineSystem = magazineSystem;

            if (secondInteractable.attachedToHand != null) // 다른 손도 총을 잡고 있다면 반동 줄이기
                recoilPower = minRecoil;
            else // 아니라면 최대 반동
                recoilPower = maxRecoil;

            if (fireMode == 3) // 연사 모드일 때 항상 발사 가능
                redyToFire = true;
            
            if (interactable.attachedToHand != null) // 총을 잡고 있을 때 실행
                CanFire();

            else // 잡고 있지 않을 경우
                isGrab = false;

            yield return null;
        }
    }

    void Fire()
    {
        // 발사 지연시간
        if (redyToFire)
        {
            if (!isShotgun)
            {
                // 총알 생성
                //Rigidbody bulletrb = Instantiate
                //    (bulletPref, muzzelFlash.transform.position, muzzelFlash.transform.rotation).GetComponent<Rigidbody>();
                //bulletrb.velocity = muzzelFlash.transform.forward * shootingSpeed; // 총알의 발사 방향 및 속도
                GameObject bullet = GetObject();
                Rigidbody buletRb = bullet.GetComponent<Rigidbody>();
                bullet.transform.position = muzzelFlash.transform.position;
                buletRb.velocity = muzzelFlash.transform.forward * shootingSpeed;
            }
            else
            { 
                for (int i = 0; i < 21; i++)
                {
                    GameObject bullet = GetObject();
                    Rigidbody buletRb = bullet.GetComponent<Rigidbody>();
                    bullet.transform.position = muzzelFlash.transform.position;
                    bullet.transform.rotation = Quaternion.Euler
                        (new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0));
                    buletRb.velocity = muzzelFlash.transform.forward * shootingSpeed;
                    print(i);
                }
            }
            muzzelFlash.Play(); // 총구 화염 이펙트 재생
            audioSource.PlayOneShot(shotSound); // 발사 사운드 재생
            gunRb.AddRelativeForce(Vector3.back * recoilPower, ForceMode.Force);
            gunRb.AddRelativeTorque(Vector3.left * recoilPower, ForceMode.Force);
            magazineSystem.bulletCount -= 1; // 총 발사시 탄창의 총 총알 개수 -1
            bolt.Shot();
            muzzleLight.SetActive(true); // 총구 화염 라이트 켜기
            Invoke("HideLight", 0.1f); // 0.1초 후 총구 화염 라이트 끄기
            redyToFire = false; // 발사 불가능
        }
    }

    private void CanFire()
    {
        SteamVR_Input_Sources source = interactable.attachedToHand.handType;

        isGrab = true;
        // 발사 모드 변경
        if (changeFireMode[source].stateDown && ableAutomaticFire) // 연사,단발 변경
            fireMode = 4 - fireMode;
        if (ejectMagazine[source].stateDown && magazineSystem != null) // 탄창 분리
            magazineSystem.ChangeMagazine();

        if (bolt.redyToShot) // 노리쇠가 준비 되었을 때
        {
            if (fireAction[source].lastState != fireAction[source].stateDown) // 트리거를 눌렀을 때 작동
                Fire();
            else // 트리거가 눌려있지 않을 경우 발사 가능
                redyToFire = true;
        }
        else if (fireAction[source].stateDown) // 준비되지 않았다면 소리 재생
            audioSource.PlayOneShot(emptyShotSound);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private GameObject CreateNewObject()
    {
        var newObj = Instantiate(bulletPref);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public GameObject GetObject()
    {
        if (poolingObjectQueue.Count > 0)
        {
            var obj = poolingObjectQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.SetActive(true);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        poolingObjectQueue.Enqueue(obj);
    }

    void HideLight()
    {
        muzzleLight.SetActive(false);
    }

    private void HandHoverUpdate(Hand hand)
    {
        // 현재 손이 물체를 잡고 있는지 확인
        if (interactable.attachedToHand != null)
        {
            // 다른 손이 잡으려 하면, 잡지 못하게 막음
            if (currentHand != null && currentHand != hand)
            {
                hand.DetachObject(gameObject); // 다른 손으로 잡지 못하게 물체 분리
            }
        }
    }

    private void OnAttachedToHand(Hand hand)
    {
        // 물체가 손에 잡히면, 해당 손을 기록
        currentHand = hand;
    }

    private void OnDetachedFromHand(Hand hand)
    {
        // 물체가 손에서 떨어지면, 현재 손 기록을 초기화
        if (currentHand == hand)
        {
            currentHand = null;
        }
    }
}