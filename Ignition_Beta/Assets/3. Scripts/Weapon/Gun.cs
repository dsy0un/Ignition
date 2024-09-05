using System.Collections;
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

    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean ejectMagazine;
    public SteamVR_Action_Boolean changeFireMode;
    public GameObject bulletPref;

    public float shootingSpeed = 1f;
    public float recoil = 5;
    private float currentTime;
    private int fireMode = 1;

    public Interactable interactable;
    public MagazineSystem magazineSystem;
    public Socket socket;
    public Bolt bolt;

    public bool isGrab;

    private void Start()
    {
        StartCoroutine("GunWork");
        currentTime = fireTime; // 발사 지연시간 초기화
    }
    void Fire()
    {
        // 발사 지연시간
        if (currentTime <= fireTime) return;
        if (bolt.redyToShot) // 노리쇠가 준비 되었을 때
        {
            // 총알 생성
            Rigidbody bulletrb = Instantiate
                (bulletPref, muzzelFlash.transform.position, muzzelFlash.transform.rotation).GetComponent<Rigidbody>();
            bulletrb.velocity = muzzelFlash.transform.forward * shootingSpeed; // 총알의 발사 방향 및 속도
            muzzelFlash.Play(); // 총구 화염 이펙트 재생
            audioSource.PlayOneShot(shotSound); // 발사 사운드 재생
            magazineSystem.BulletCount -= 1; // 총 발사시 탄창의 총 총알 개수 -1
            bolt.Shot();
            muzzleLight.SetActive(true); // 총구 화염 라이트 켜기
            Invoke("HideLight", 0.1f); // 0.1초 후 총구 화염 라이트 끄기
            currentTime = 0; // 발사 지연시간 초기화
        }
        else // 준비되지 않았다면 소리 재생
            audioSource.PlayOneShot(emptyShotSound);
    }

    void HideLight()
    {
        muzzleLight.SetActive(false);
    }

    IEnumerator GunWork()
    {
        while (true)
        {
            if (socket.IsMagazine) // 탄창이 있을 경우 스크립트 가져오기
                magazineSystem = GetComponentInChildren<MagazineSystem>();
            else // 아닌경우 스크립트 NULL
                magazineSystem = null;
            isGrab = true;
            if (currentTime <= fireTime && fireMode == 3) // 연사 모드일때 발사 지연시간 작동
                currentTime += Time.deltaTime;
            // 총을 잡고 있을 때 실행
            if (interactable.attachedToHand != null)
            {
                SteamVR_Input_Sources source = interactable.attachedToHand.handType;

                // 발사 모드 변경
                if (changeFireMode[source].stateDown && ableAutomaticFire) // 연사,단발 변경
                    fireMode = 4 - fireMode;
                if (ejectMagazine[source].stateDown) // 탄창 분리
                    magazineSystem.ChangeMagazine();

                if (fireAction[source].lastState != fireAction[source].stateDown) // 트리거를 눌렀을 때 작동
                {
                    Fire();
                }
                else // 트리거가 눌려있지 않을 경우 발사 지연시간 초기화
                    currentTime = fireTime;                    
            }
            else
            {
                isGrab = false;
            }
            yield return null;
        }
    }
}