using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Bolt : MonoBehaviour
{
    public Interactable interactable;
    public Rigidbody rb;
    private MagazineSystem magazineSystem;
    public Gun gun;
    public LinearMapping mapping;

    public GameObject round;
    public GameObject cartridge;
    public GameObject ejectBullet;

    private float recoilPower;
    public float maxRecoil;
    public float minRecoil;

    public bool redyToShot;
    private bool boltRetraction;

    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();
    public GameObject ejectPoint;
    public int spawnPrefabAmount;


    private void Awake()
    {
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
            if (interactable.attachedToHand != null)
                recoilPower = minRecoil;
            else
                recoilPower = maxRecoil;

            if (gun.socket.IsMagazine)
                magazineSystem = gun.magazineSystem;
            else
                magazineSystem = null;

            if (mapping.value == 1)
            {
                if (cartridge.activeInHierarchy == true)
                    GetObject();
                cartridge.SetActive(false);

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
            yield return null;
        }
    }
    public void Shot()
    {
        rb.AddRelativeForce(Vector3.back * recoilPower, ForceMode.Impulse);
        rb.AddForce(Vector3.up * recoilPower, ForceMode.Impulse);
        round.SetActive(false);
        cartridge.SetActive(true);
        boltRetraction = false;
        redyToShot = false;
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
