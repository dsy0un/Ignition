using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TakeItem : MonoBehaviour
{
    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    GameObject grabItem;

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grab = hand.GetGrabStarting();
        bool isgrab = hand.IsGrabEnding(gameObject);
        if (grab == GrabTypes.Grip && isgrab)
        {
            Instantiate(itemPrefab, hand.gameObject.transform.position, hand.gameObject.transform.rotation, hand.gameObject.transform);
        }
    }
}
