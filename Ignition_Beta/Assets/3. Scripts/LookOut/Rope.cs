using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Rope : MonoBehaviour
{
    Interactable interactable;

    bool isGrab = false;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabTypes = hand.GetGrabStarting();

        if (interactable.attachedToHand == null && grabTypes != GrabTypes.None && !isGrab)
        {
            // hand.AttachObject(gameObject, grabTypes);
            hand.HoverLock(interactable);
            hand.gameObject.transform.root.SetParent(transform, true);
            StopCoroutine(GameManager.Instance.barrier.Escape());
            StartCoroutine(GameManager.Instance.barrier.Escape(0.1f));
            isGrab = true;
        }
    }
}
