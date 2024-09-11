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
    public Gun gun;
    public LinearMapping mapping;

    public GameObject round;
    public GameObject cartridge;

    [Header("Bolt")]
    public bool autoBolt;
    public float moveTime;
    private LinearDrive linearDrive;
    private bool boltMoving = false;

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
            if (mapping.value == 1)
            {
                if (cartridge.activeInHierarchy == true)
                    GetObject();
                cartridge.SetActive(false);

                if (magazineSystem != null && magazineSystem.bulletCount > 0)
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

            if (boltMoving == false && interactable.attachedToHand == null && autoBolt)
            {
                StartCoroutine("AutoMoveFront");
            }
            yield return null;
        }
    }
    public void Shot()
    {
        round.SetActive(false);
        cartridge.SetActive(true);
        boltRetraction = false;
        redyToShot = false;
        if (autoBolt && interactable.attachedToHand == null)
        {
            StartCoroutine("AutoMoveBack");
            boltMoving = true;
        }
    }

    IEnumerator AutoMoveBack()
    {
        float time = 0f;
        while (true)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp
            (linearDrive.startPosition.position, linearDrive.endPosition.position, time / moveTime);
            mapping.value = Mathf.Lerp(0, 1, time / moveTime);
            if (mapping.value == 1)
            {
                boltMoving = false;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator AutoMoveFront()
    {
        float time = 0f;
        while (true)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp
                (transform.position, linearDrive.startPosition.position, time / moveTime);
            mapping.value = Mathf.Lerp(mapping.value, 0, time / moveTime);
            if (mapping.value == 0)
            {
                yield break;
            }
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
