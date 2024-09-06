using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Rope : MonoBehaviour
{
    void OnAttachedToHand(Hand hand)
    {
        Debug.Log(1);
        hand.gameObject.transform.root.SetParent(transform, false);
    }
}
