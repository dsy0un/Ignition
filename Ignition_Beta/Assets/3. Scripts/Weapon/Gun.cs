using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
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

    public float bulletLifeTime = 30f;
    public float shootingSpeed = 1f;
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
}