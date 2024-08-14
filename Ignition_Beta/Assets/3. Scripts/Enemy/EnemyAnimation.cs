using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    private float xVel = 0.7f;
    private float zVel = 0.7f;
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
        if (isDead)
        {
            anim.SetBool("isDead", true);
        }
        else
        {
            if (rb.velocity.x < xVel && rb.velocity.x > -xVel && rb.velocity.z < zVel && rb.velocity.z > -zVel)
            {
                anim.SetBool("isMove", false);
                anim.SetFloat("xDir", 0);
                anim.SetFloat("yDir", 0);
            }
            else
            {
                anim.SetBool("isMove", true);
                anim.SetFloat("xDir", rb.velocity.x);
                anim.SetFloat("yDir", rb.velocity.z);
            }
        }
    }
    public void isDie(bool die)
    {
        isDead = die;
    }
}
