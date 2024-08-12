using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SubGunGrab : MonoBehaviour
{
    public Interactable mainInter; // 부모 Interactable
    private Interactable interactable; // 자식 Interactable (본인)
    private Quaternion secondRotationOffset; // 부모를 기준으로 회전값을 조절하기 위한 값

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // 부모의 조건이 맞지 않으면 강제로(자식을 잡고 있는 손) 떼내기 위한 함수
    //public void ForceDetach()
    //{
    //    if (interactable.attachedToHand)
    //    {
    //        interactable.attachedToHand.HoverUnlock(interactable);
    //        interactable.attachedToHand.DetachObject(gameObject);
    //    }
    //}

    // 부모의 장착 지점과 자식의 장착 지점을 계산하여 Quaternion값을 반환
    private Quaternion GetTargetRotation()
    {
        Vector3 mainHandUp = mainInter.attachedToHand.objectAttachmentPoint.up;
        Vector3 secondHandUp = interactable.attachedToHand.objectAttachmentPoint.up;

        return Quaternion.LookRotation(interactable.attachedToHand.transform.position - mainInter.attachedToHand.transform.position, mainHandUp);
    }

    // 부모와 자식 포인트에 잡고있는 상태 (둘 다)
    private void HandAttachedUpdate(Hand hand)
    {
        if (mainInter.attachedToHand)
        {
            if (mainInter.skeletonPoser)
            {
                Quaternion customHandPoserRotation = mainInter.skeletonPoser.GetBlendedPose(mainInter.attachedToHand.skeleton).rotation;
                mainInter.transform.rotation = GetTargetRotation() * secondRotationOffset * customHandPoserRotation;
            }
            else
            {
                mainInter.attachedToHand.objectAttachmentPoint.rotation = GetTargetRotation() * secondRotationOffset;
            }
        }
    }

    // 자식 포인트에 닿고 있는 상태
    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        // Grab
        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType, 0);
            hand.HoverLock(interactable);
            hand.HideGrabHint();
            secondRotationOffset = Quaternion.Inverse(GetTargetRotation()) 
                * mainInter.attachedToHand.currentAttachedObjectInfo.Value.handAttachmentPointTransform.rotation;
        }

        // Release
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }
    }
}
