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
    public Socket socket;
    public Gun gun;

    public GameObject round;
    public GameObject cartridge;
    public GameObject ejectPoint;
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float endPositionValue;
    public int jointValue;
    public bool redyToShot;
    public bool boltRetraction;

    public float impulsePower;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<Interactable>();
        joint = GetComponent<SpringJoint>();
        originPosition = transform.localPosition;
        originRotation = transform.localRotation;
    }

    private void Start()
    {
        StartCoroutine("BoltAction");
    }

    IEnumerator BoltAction()
    {
        while (true)
        {
            //magazineSystem = gun.magazineSystem;
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
            //else
            //    redyToShot = false;
            if (transform.localPosition.z <= originPosition.z - endPositionValue)
            {
                transform.localPosition = new Vector3
                    (originPosition.x, originPosition.y, originPosition.z - endPositionValue);
                boltRetraction = true;
            }
            //if (magazineSystem.BulletCount <= 0 || magazineSystem == null) return;
            if (boltRetraction)
            {
                //magazineSystem.BulletCount -= 1; // 총 발사시 탄창의 총 총알 개수 -1
                round.SetActive(true);
                cartridge.SetActive(false);
            }
            yield return null;
        }
    }
    public void Shot()
    {
        rb.AddForce(Vector3.back * impulsePower, ForceMode.Impulse);
        round.SetActive(false);
        cartridge.SetActive(true);
        boltRetraction = false;
        redyToShot = false;
    }
}
