using EW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class TakeItem : MonoBehaviour
{
    [SerializeField]
    GameObject itemPrefab;

    GameObject spawn;

    bool isGrab = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            spawn = Instantiate(itemPrefab, other.transform.position, Quaternion.identity, other.transform);
            spawn.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
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
