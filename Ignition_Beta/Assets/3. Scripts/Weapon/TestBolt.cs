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
            // 노리쇠의 로컬 위치 = x,y는 초기 위치, z는 본인의 로컬 위치
            //transform.localPosition = new Vector3(originPosition.x, originPosition.y, transform.localPosition.z);
            //joint.spring = jointValue; // 스프링 조인트 작동
            //if (interactable.attachedToHand != null || !gun.isGrab) // 노리쇠를 잡고 있거나 총을 잡고 있지 않을 때
            //{
            //    joint.spring = 0; // 스프링 조인트 끄기
            //    redyToShot = false;
            //}
            //if (mapping.value >= startPositionValue) // 노리쇠의 로컬 위치가 초기 위치 이상일 때
            //{
            //    //transform.localPosition = originPosition; // 노리쇠 로컬 위치 = 초기 위치
            //    if (boltRetraction)
            //    {
            //        redyToShot = true;
            //    }
            //}

            // 노리쇠의 로컬 회전 방향 = 초기 회전 방향
            //transform.localRotation = originRotation;

            if (mapping.value == 1)
            {
                //transform.localPosition = new Vector3
                //    (originPosition.x, originPosition.y, originPosition.z - endPositionValue);
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
        GetObject();
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
