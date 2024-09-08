using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Bolt : MonoBehaviour
{
    [HideInInspector]
    public MagazineSystem magazineSystem;

    private Interactable interactable;
    public Interactable secondInteractable;
    public Rigidbody gunRb;
    public Gun gun;
    public LinearMapping mapping;

    public GameObject round;
    public GameObject cartridge;

    [Header("Bolt")]
    public bool autoBolt;
    public float moveTime;
    private LinearDrive linearDrive;
    private bool boltMoving = false;
    private float time = 0f;

    [Header("Recoil")]
    public float maxRecoil;
    public float minRecoil;
    private float recoilPower;

    [HideInInspector]
    public bool redyToShot;
    private bool boltRetraction;

    [Header("Eject Cartridge")]
    public int spawnPrefabAmount;
    public GameObject ejectPoint;
    public GameObject ejectBullet;
    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();

    private void Awake()
    {
        linearDrive = GetComponent<LinearDrive>();
        interactable = GetComponent<Interactable>();
        Initialize(spawnPrefabAmount);
    }

    private void Start()
    {
        StartCoroutine("BoltAction");
    }

    IEnumerator BoltAction()
    {
        while (true)
        {
            time += Time.deltaTime;
            if (secondInteractable.attachedToHand != null)
                recoilPower = minRecoil;
            else
                recoilPower = maxRecoil;

            if (mapping.value == 1)
            {
                if (cartridge.activeInHierarchy == true)
                    GetObject();
                cartridge.SetActive(false);
                boltMoving = false;

                if (magazineSystem != null && magazineSystem.BulletCount > 0)
                    boltRetraction = true;
                else
                {
                    round.SetActive(false);
                    cartridge.SetActive(false);
                }
            }

            if (boltRetraction)
            {
                if (mapping.value < 1)
                    round.SetActive(true);
                if (mapping.value == 0)
                    redyToShot = true;
                else
                    redyToShot = false;
            }

            if (!boltMoving && interactable.attachedToHand == null && autoBolt)
            {
                transform.position = Vector3.Lerp
                (linearDrive.endPosition.position, linearDrive.startPosition.position, time / moveTime);
                mapping.value = Mathf.Lerp(1, 0, time / moveTime);
            }
            yield return null;
        }
    }
    public void Shot()
    {
        gunRb.AddRelativeForce(Vector3.back * recoilPower, ForceMode.Impulse);
        gunRb.AddForce(Vector3.up * recoilPower, ForceMode.Impulse);
        round.SetActive(false);
        cartridge.SetActive(true);
        boltRetraction = false;
        redyToShot = false;
        if (autoBolt && interactable.attachedToHand == null)
        {
            StartCoroutine("AutoMove");
            boltMoving = true;
        }
    }

    IEnumerator AutoMove()
    {
        while (true)
        {
            transform.position = Vector3.Lerp
            (linearDrive.startPosition.position, linearDrive.endPosition.position, time / moveTime);
            mapping.value = Mathf.Lerp(0, 1, time / moveTime);
            yield return null;
        }
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
        var newObj = Instantiate(ejectBullet);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public void GetObject()
    {
        if (poolingObjectQueue.Count > 0)
        {
            var obj = poolingObjectQueue.Dequeue();
            obj.transform.position = ejectPoint.transform.position;
            obj.SetActive(true);
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.transform.position = ejectPoint.transform.position;
            newObj.SetActive(true);
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        poolingObjectQueue.Enqueue(obj);
    }
}
