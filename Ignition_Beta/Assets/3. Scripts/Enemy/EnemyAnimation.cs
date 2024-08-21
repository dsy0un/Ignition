using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    public float zVel = 0.7f; // 개체의 앞,뒤 속도 측정시 최소 속도
    public float xVel = 0.7f; // 개체의 왼쪽,오른쪽 속도 측정시 최소 속도
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 객체의 리지드바디 속도(글로벌 Vector3 형태)를 구한 후 로컬 형태로 변환
        Vector3 velocity = transform.InverseTransformDirection(rb.velocity);

        if (isDead)
        {
            anim.SetBool("isDead", true);
        }
        else
        {
            // 속도 측정 값이 최솟값보다 작으면 애니메이션 멈춤
            if (velocity.z > zVel || velocity.z < -zVel || velocity.x > xVel || velocity.x < -xVel)
            {
                anim.SetBool("isMove", true);
                anim.SetFloat("xDir", velocity.z);
                anim.SetFloat("yDir", velocity.x * 0.5f);
            }
            else
            {
                anim.SetBool("isMove", false);
                anim.SetFloat("xDir", 0);
                anim.SetFloat("yDir", 0);
            }
        }
    }

    public void SetTrigger(string trgName)
    {
        anim.SetTrigger(trgName);
    }

    public void isDie(bool die)
    {
        isDead = die;
    }
}
