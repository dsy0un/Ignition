using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SimpleAttach : MonoBehaviour
{
    //private Interactable interactable;
    private Vector3 originPos;
    private Quaternion originRot;

    private void Awake()
    {
        originPos = transform.localPosition;
        originRot = transform.localRotation;
        //interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        transform.localPosition = originPos;
        transform.localRotation = Quaternion.Euler(new Vector3(originRot.x, transform.localRotation.y, originRot.z));
    }

    //private void HandHoverUpdate(Hand hand)
    //{
    //    GrabTypes grabTypes = hand.GetGrabStarting();
    //    bool isGrabEnding = hand.IsGrabEnding(gameObject);

    //    if (interactable.attachedToHand == null && grabTypes != GrabTypes.None)
    //    {
    //        hand.AttachObject(gameObject, grabTypes);
    //        hand.HoverLock(interactable);
    //    }
    //    else if (isGrabEnding)
    //    {
    //        hand.DetachObject(gameObject);
    //        hand.HoverUnlock(interactable);
    //    }
    //}
}
