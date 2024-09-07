using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TestBolt : MonoBehaviour
{
    private Interactable interactable;
    //private SpringJoint joint;
    public Rigidbody rb;
    private MagazineSystem magazineSystem;
    public Gun gun;
    public Socket socket;
    public LinearMapping mapping;

    public GameObject round;
    public GameObject cartridge;
    public GameObject ejectBullet;
    //private Quaternion originRotation;
    //private Vector3 originPosition;
    //public float startPositionValue;
    public float impulsePower;
    //public int jointValue;
    public bool redyToShot;
    private bool boltRetraction;

    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();
    public GameObject ejectPoint;
    public int spawnPrefabAmount;


    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        //originPosition = transform.localPosition;
        //joint = GetComponent<SpringJoint>();
        //originRotation = transform.localRotation;
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
            if (socket.IsMagazine)
                magazineSystem = gun.magazineSystem;
            else
                magazineSystem = null;
            if (mapping.value == 1)
            {
                if (cartridge.activeInHierarchy == true)
                {
                    GetObject();
                }
                cartridge.SetActive(false);
                if (magazineSystem != null && magazineSystem.BulletCount > 0)
                {
                    boltRetraction = true;
                }
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
        rb.AddRelativeForce(Vector3.back * impulsePower, ForceMode.Impulse);
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
