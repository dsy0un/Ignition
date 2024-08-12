using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private VisualEffect muzzelFlash;
    [SerializeField] private GameObject muzzleLight;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip emptyShotSound;
    [SerializeField] private float fireTime;

    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean ejectMagazine;
    public SteamVR_Action_Boolean changeFireMode;
    public GameObject bulletPref;
    private Rigidbody rb;
    public Transform gunTransform; // 총기의 Transform
    public float recoilAmount = 2f; // 반동의 세기
    public float recoilSpeed = 5f; // 반동이 원위치로 돌아오는 속도

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 recoilOffset;
    private Quaternion recoilRotation;

    public float bulletLifeTime = 30f;
    public float shootingSpeed = 1f;
    public float recoil = 5;
    private int fireMode = 1;
    private float currentTime;
    public bool changeMagazine;

    private Interactable interactable;
    private GameObject socket;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        socket = transform.Find("Body").Find("Socket").gameObject;
        currentTime = fireTime;
        changeMagazine = false;
        originalPosition = gunTransform.localPosition;
        originalRotation = gunTransform.localRotation;
    }
    private void Update()
    {
        if (currentTime <= fireTime)
        {
            currentTime += Time.deltaTime;
        }
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (this.gameObject.tag == "Pistol")
                fireMode = 1;
            else if (this.gameObject.tag != "Pistol" && changeFireMode[source].stateDown)
                fireMode = 4 - fireMode;

            if (GetComponentInChildren<MagazineSystem>() != null && GetComponentInChildren<MagazineSystem>().BulletCount > 0)
            {
                if (fireMode == 1)
                {
                    if (fireAction[source].stateDown)
                    {
                        Fire();
                        ApplyRecoil();
                    }
                }
                else
                {
                    if (fireAction[source].lastState != fireAction[source].stateDown)
                    {
                        if (currentTime >= fireTime)
                        {
                            Fire();
                            currentTime = 0;
                            ApplyRecoil();
                        }
                    }
                    else
                        currentTime = fireTime;
                }
            }
            else if (fireAction[source].stateDown)
            {
                audioSource.PlayOneShot(emptyShotSound);
            }

            if (ejectMagazine[source].stateDown)
            {
                changeMagazine = true;
            }
        }
        gunTransform.localPosition = 
            Vector3.Lerp(gunTransform.localPosition, originalPosition + recoilOffset, Time.deltaTime * recoilSpeed);
        gunTransform.localRotation = 
            Quaternion.Slerp(gunTransform.localRotation, originalRotation * recoilRotation, Time.deltaTime * recoilSpeed);
    }
    void Fire()
    {   
        Rigidbody bulletrb = Instantiate(bulletPref, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
        bulletrb.velocity = firePoint.forward * shootingSpeed;
        muzzelFlash.Play();
        audioSource.PlayOneShot(shotSound);
        GetComponentInChildren<MagazineSystem>().BulletCount -= 1;
        muzzleLight.SetActive(true);
        Invoke("HideLight", 0.1f);
    }

    void HideLight()
    {
        muzzleLight.SetActive(false);
    }

    void ApplyRecoil()
    {
        // 랜덤한 반동 적용 (위쪽과 좌우로)
        recoilOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.1f, 0.2f), 0) * recoilAmount;
        recoilRotation = Quaternion.Euler(new Vector3(-Random.Range(2f, 5f), Random.Range(-1f, 1f), 0) * recoilAmount);
    }
}