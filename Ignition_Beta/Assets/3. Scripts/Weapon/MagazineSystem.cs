using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Valve.VR.InteractionSystem;

public class MagazineSystem : MonoBehaviour
{
    [SerializeField] private int bulletCount = 20;
    public int BulletCount
    {
        get => bulletCount;
        set => bulletCount = value;
    }
    private Interactable interactable;
    private Rigidbody rb;
    private Transform magazinePoint;
    private bool isLoad;
    private GameObject gunObject;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isLoad)
            rb.useGravity = false;
        else if(!isLoad)
            rb.useGravity = true;
        gunObject = transform.root.gameObject;
        if (gunObject.GetComponent<Gun>() != null && gunObject.GetComponent<Gun>().changeMagazine)
        {
            gunObject.GetComponent<Gun>().changeMagazine = false;
            ChangeMagazine();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactable.attachedToHand != null)
        {
            if (other.tag == "Socket" && !isLoad && other.GetComponent<Socket>().IsMagazine == false)
            {
                magazinePoint = other.transform.GetChild(0);
                isLoad = true;
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    Collider childCollider = child.GetComponent<Collider>();
                    childCollider.isTrigger = true;
                }
                other.GetComponent<Socket>().IsMagazine = true;
                transform.parent = magazinePoint;
                transform.localPosition = new Vector3(magazinePoint.localPosition.x, magazinePoint.localPosition.y, magazinePoint.localPosition.z);
                if (interactable.attachedToHand != null)
                    interactable.attachedToHand.DetachObject(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Socket" && isLoad)
        {
            transform.localPosition = new Vector3(magazinePoint.localPosition.x, magazinePoint.localPosition.y, magazinePoint.localPosition.z);
            transform.localEulerAngles = new Vector3(magazinePoint.localEulerAngles.x, magazinePoint.localEulerAngles.y, magazinePoint.localEulerAngles.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactable.attachedToHand != null)
        {
            if (other.tag == "Socket")
            {
                rb.constraints = RigidbodyConstraints.None;
                transform.GetComponentInParent<Socket>().IsMagazine = false;
                transform.parent = null;
                isLoad = false;
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    Collider childCollider = child.GetComponent<Collider>();
                    childCollider.isTrigger = false;
                }
            }
        }
    }

    private void ChangeMagazine()
    {
        if (isLoad)
        {
            rb.AddForce(-transform.forward * 100);
            rb.constraints = RigidbodyConstraints.None;
            transform.GetComponentInParent<Socket>().IsMagazine = false;
            transform.parent = null;
            isLoad = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                Collider childCollider = child.GetComponent<Collider>();
                childCollider.isTrigger = false;
            }
        }
    }
}
