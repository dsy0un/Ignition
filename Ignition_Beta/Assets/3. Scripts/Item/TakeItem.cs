using EW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class TakeItem : MonoBehaviour
{
    [Tooltip("0: Pistol, 1: Rifle, 2: Shotgun")]
    [SerializeField]
    GameObject[] itemPrefab; // 0: Pistol, 1: Rifle, 2: Shotgun

    [SerializeField]
    Hand leftHand, rightHand;

    GameObject currentObject;

    GameObject spawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (leftHand.currentAttachedObject == null && rightHand.currentAttachedObject == null) return;
            if (leftHand.currentAttachedObject != null && rightHand.currentAttachedObject != null) return;

            if (other.CompareTag("Hand"))
            {
                if (leftHand.currentAttachedObject != null)
                    currentObject = leftHand.currentAttachedObject;
                else if (rightHand.currentAttachedObject != null)
                    currentObject = rightHand.currentAttachedObject;

                switch (currentObject.tag)
                {
                    case "Pistol":
                        break;
                    case "Rifle":
                        spawn = Instantiate(itemPrefab[1], other.transform.position, Quaternion.identity, other.transform);
                        break;
                    case "Shotgun":
                        break;
                    default: 
                        break;
                }
                spawn.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentObject != null && other.CompareTag("Hand"))
        {
            spawn.GetComponent<Rigidbody>().isKinematic = false;
            spawn.transform.SetParent(null);
        }
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grab = hand.GetGrabStarting();
        bool isgrab = hand.IsGrabEnding(spawn);
        if (grab == GrabTypes.Grip && !isgrab)
        {
            spawn.GetComponent<Rigidbody>().isKinematic = false;
            spawn.transform.SetParent(null);
        }
    }
}
