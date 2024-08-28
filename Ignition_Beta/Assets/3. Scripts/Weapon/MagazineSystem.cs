using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class MagazineSystem : MonoBehaviour
{
    [SerializeField] private float maxBullet = 20;
    private float bulletCount;
    public float BulletCount
    {
        get => bulletCount;
        set => bulletCount = value;
    }
    private Interactable interactable;
    private Rigidbody rb;
    private Collider col;
    private Transform magazinePoint;
    private bool isLoad;
    public Image[] bulletImage;

    private void Start()
    {
        bulletCount = maxBullet;
        interactable = GetComponent<Interactable>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isLoad)
        {
            rb.useGravity = false;
            transform.localPosition = 
                new Vector3(magazinePoint.localPosition.x, magazinePoint.localPosition.y, magazinePoint.localPosition.z);
            transform.localEulerAngles = 
                new Vector3(magazinePoint.localEulerAngles.x, magazinePoint.localEulerAngles.y, magazinePoint.localEulerAngles.z);
            if (interactable.attachedToHand != null)
                interactable.attachedToHand.DetachObject(gameObject);
        }
        else if (!isLoad)
            rb.useGravity = true;
        foreach (Image i in bulletImage)
        {
            i.fillAmount = bulletCount / maxBullet;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactable.attachedToHand != null)
        {
            if (other.tag == "Socket" && !isLoad && other.GetComponent<Socket>().IsMagazine == false)
            {
                rb.useGravity = false;
                magazinePoint = other.transform.GetChild(0);
                isLoad = true;
                col.isTrigger = true;
                other.GetComponent<Socket>().IsMagazine = true;
                transform.parent = magazinePoint;
                transform.localPosition = 
                    new Vector3(magazinePoint.localPosition.x, magazinePoint.localPosition.y, magazinePoint.localPosition.z);
                if (interactable.attachedToHand != null)
                    interactable.attachedToHand.DetachObject(gameObject);
            }
        }
    }

    public void ChangeMagazine()
    {
        if (isLoad)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            transform.GetComponentInParent<Socket>().IsMagazine = false;
            transform.parent = null;
            isLoad = false;
            col.isTrigger = false;
            rb.AddForce(-transform.up * 100);
            transform.localScale = Vector3.one;
        }
    }
}
