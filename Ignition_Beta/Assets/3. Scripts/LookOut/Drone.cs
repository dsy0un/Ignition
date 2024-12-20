using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField]
    Transform[] rotors;
    
    Animator animator;
    public Animator Animator
    {
        get { return animator; }
    }

    [SerializeField]
    float speed = 5000f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(RotateRotors());
    }

    IEnumerator RotateRotors()
    {
        while (true)
        {
            foreach (Transform t in rotors) 
            {
                t.Rotate(0, 0, speed * Time.deltaTime);
            }
            yield return null;
        }
    }
}
