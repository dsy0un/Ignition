using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Bolt : MonoBehaviour
{
    private Interactable interactable;
    private SpringJoint joint;
    private Rigidbody rb;
    private MagazineSystem magazineSystem;
    public Gun gun;
    public Socket socket;

    public GameObject round;
    public GameObject cartridge;
    public GameObject ejectBullet;
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float endPositionValue;
    public int jointValue;
    public bool redyToShot;
    private bool boltRetraction;
    public float impulsePower;

    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<Interactable>();
        joint = GetComponent<SpringJoint>();
        originPosition = transform.localPosition;
        originRotation = transform.localRotation;
        Initialize(30);
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
            // 노리쇠의 로컬 회전 방향 = 초기 회전 방향
            transform.localRotation = originRotation;
            // 노리쇠의 로컬 위치 = x,y는 초기 위치, z는 본인의 로컬 위치
            transform.localPosition = new Vector3(originPosition.x, originPosition.y, transform.localPosition.z);
            joint.spring = jointValue; // 스프링 조인트 작동
            if (interactable.attachedToHand != null) // 노리쇠를 잡고 있을 때
                joint.spring = 0; // 스프링 조인트 끄기
            if (transform.localPosition.z >= originPosition.z - 0.01f) // 노리쇠의 로컬 위치가 초기 위치 - 0.01 위치일 때
            {
                joint.spring = 0; // 스프링 조인트 끄기
                transform.localPosition = originPosition; // 노리쇠 로컬 위치 = 초기 위치
                if (boltRetraction)
                {
                    redyToShot = true;
                }
            }
            else
                redyToShot = false;
            if (transform.localPosition.z <= originPosition.z - endPositionValue)
            {
                transform.localPosition = new Vector3
                    (originPosition.x, originPosition.y, originPosition.z - endPositionValue);
                cartridge.SetActive(false);
                if (magazineSystem != null && magazineSystem.BulletCount >= 0)
                    boltRetraction = true;
            }
            if (boltRetraction)
            {
                round.SetActive(true);
            }
            yield return null;
        }
    }
    public void Shot()
    {
        rb.AddForce(Vector3.back * impulsePower, ForceMode.Impulse);
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
            obj.transform.position = transform.position;
            obj.SetActive(true);
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.transform.position = transform.position;
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
