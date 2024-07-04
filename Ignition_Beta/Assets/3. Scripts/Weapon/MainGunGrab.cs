using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using Unity.VisualScripting;

public class MainGunGrab : MonoBehaviour
{
    private Interactable interactable; // 부모 Interactable (본인)
    public Interactable subInter; // 자식 Interactable

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // 매프레임 마지막의 검사
    private void LateUpdate()
    {
        // 서브 손잡이를 잡고 있는 상태에서 메인 손잡이를 놨을 때
        if (!interactable.attachedToHand && subInter.attachedToHand)
        {
            subInter.attachedToHand.HoverUnlock(subInter);
            subInter.attachedToHand.DetachObject(subInter.gameObject);
            subInter.DetachFromHand();
        }

        // check if grabbed
        if (interactable.attachedToHand != null)
        {
            // Get the hand source
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
        }
    }

    // 부모 객체에 닿고 있는 상태
    private void HandHoverUpdate(Hand hand)
    {
        if (!interactable.attachedToHand)
        {
            hand.DetachObject(subInter.gameObject);
            hand.HoverUnlock(subInter);
        }
    }
}
