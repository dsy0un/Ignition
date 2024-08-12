//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

namespace Valve.VR.Extras
{
    public class SteamVR_LaserPointer : MonoBehaviour
    {
        public SteamVR_Behaviour_Pose pose;

        public SteamVR_Action_Boolean grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
        public SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");

        //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");

        public bool active = true;
        public Color color;
        public float thickness = 0.002f;
        public Color clickColor = Color.green;
        public GameObject holder;
        public GameObject pointer;
        bool isActive = false;
        public bool addRigidBody = false;
        public Transform reference;
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;

        Transform previousContact = null;
        public RaycastHit hit;

        public Hand hand;

        Hand.AttachmentFlags defaultAttachmentFlags = Hand.AttachmentFlags.ParentToHand |
                                                              Hand.AttachmentFlags.DetachOthers |
                                                              Hand.AttachmentFlags.DetachFromOtherHand |
                                                                Hand.AttachmentFlags.TurnOnKinematic;
        public SteamVR_LaserPointer otherHandLaser;


        private void Start()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();
            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.", this);

            otherHandLaser = hand.otherHand.GetComponent<SteamVR_LaserPointer>();

            grabPinchAction.AddOnChangeListener(OnPlantActionChange, hand.handType);

            grabGripAction.AddOnChangeListener(OnActivateActionChange, hand.handType);

            holder = new GameObject();
            holder.transform.parent = hand.trackedObject.transform;
            holder.transform.localPosition = Vector3.zero;
            holder.transform.localRotation = Quaternion.identity;

            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.parent = holder.transform;
            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            pointer.transform.localRotation = Quaternion.identity;
            BoxCollider collider = pointer.GetComponent<BoxCollider>();
            if (addRigidBody)
            {
                if (collider)
                {
                    collider.isTrigger = true;
                }
                Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
                rigidBody.isKinematic = true;
            }
            else
            {
                if (collider)
                {
                    GameObject.Destroy(collider);
                }
            }
            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
        }

        private void OnActivateActionChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
        {
            if (newState)
            {
                isActive = !isActive;

                holder.SetActive(isActive);
                if (!isActive && previousContact != null)
                {
                    previousContact.gameObject.SendMessage("OnHandHoverEnd", hand, SendMessageOptions.DontRequireReceiver);
                    if (hand.currentAttachedObject != null && hand.currentAttachedObject == previousContact.gameObject)
                    {
                        hand.DetachObject(previousContact.gameObject);
                    }
                    if (otherHandLaser.previousContact != null && otherHandLaser.previousContact == previousContact)
                    {
                        otherHandLaser.previousContact.gameObject.SendMessage("OnHandHoverBegin", hand.otherHand, SendMessageOptions.DontRequireReceiver);
                    }
                    previousContact = null;
                }
            }
        }

        private void OnDisable()
        {
            if (grabPinchAction != null)
                grabPinchAction.RemoveOnChangeListener(OnPlantActionChange, hand.handType);

            if (grabGripAction != null)
                grabGripAction.RemoveOnChangeListener(OnPlantActionChange, hand.handType);
        }

        private void OnPlantActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                if (previousContact)
                {
                    if (previousContact.gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        previousContact.gameObject.SendMessage("HandHoverUpdate", hand, SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        Interactable interactable = previousContact.gameObject.GetComponent<Interactable>();

                        hand.AttachObject(previousContact.gameObject, hand.GetBestGrabbingType(GrabTypes.None), defaultAttachmentFlags);
                        hand.objectAttachmentPoint = hit.transform;

                        interactable.onDetachedFromHand += OnDetachedFromHand;
                    }
                    if (previousContact == otherHandLaser.previousContact)
                    {
                        previousContact.gameObject.SendMessage("OnHandHoverEnd", hand, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }

        private void OnDetachedFromHand(Hand hand)
        {

            if (previousContact == otherHandLaser.previousContact)
            {
                otherHandLaser.previousContact = null;
            }

            previousContact = null;
        }

        public virtual void OnPointerIn(PointerEventArgs e)
        {
            if (PointerIn != null)
                PointerIn(this, e);
        }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnPointerOut(PointerEventArgs e)
        {
            if (PointerOut != null)
                PointerOut(this, e);
        }


        private void Update()
        {
            if (!isActive)
            {
                return;
                //isActive = true;
                //this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            Ray raycast = new Ray(hand.trackedObject.transform.position, hand.trackedObject.transform.forward);

            bool bHit = Physics.Raycast(raycast, out hit);

            if (previousContact && previousContact != hit.transform)
            {
                //PointerEventArgs args = new PointerEventArgs();
                //args.fromInputSource = pose.inputSource;
                //args.distance = 0f;
                //args.flags = 0;
                //args.target = previousContact;
                //OnPointerOut(args);
                previousContact.gameObject.SendMessage("OnHandHoverEnd", hand, SendMessageOptions.DontRequireReceiver);
                if (otherHandLaser.previousContact != null && otherHandLaser.previousContact == previousContact)
                {
                    otherHandLaser.previousContact.gameObject.SendMessage("OnHandHoverBegin", hand.otherHand, SendMessageOptions.DontRequireReceiver);
                }
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                //PointerEventArgs argsIn = new PointerEventArgs();
                //argsIn.fromInputSource = pose.inputSource;
                //argsIn.distance = hit.distance;
                //argsIn.flags = 0;
                //argsIn.target = hit.transform;
                //OnPointerIn(argsIn);
                previousContact = hit.transform;
                if (otherHandLaser.previousContact != previousContact)
                {
                    previousContact.gameObject.SendMessage("OnHandHoverBegin", hand, SendMessageOptions.DontRequireReceiver);
                }

            }
            if (!bHit)
            {
                previousContact = null;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;
            }

            if (grabPinchAction.GetState(pose.inputSource))
            {
                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                pointer.GetComponent<MeshRenderer>().material.color = clickColor;
            }
            else
            {
                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                pointer.GetComponent<MeshRenderer>().material.color = color;
            }

            //if (bHit && interactWithUI.GetStateUp(pose.inputSource))
            //{
            //    PointerEventArgs argsClick = new PointerEventArgs();
            //    argsClick.fromInputSource = pose.inputSource;
            //    argsClick.distance = hit.distance;
            //    argsClick.flags = 0;
            //    argsClick.target = hit.transform;
            //    OnPointerClick(argsClick);
            //}

            //if (interactWithUI != null && interactWithUI.GetState(pose.inputSource))
            //{
            //    pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
            //    pointer.GetComponent<MeshRenderer>().material.color = clickColor;
            //}
            //else
            //{
            //    pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            //    pointer.GetComponent<MeshRenderer>().material.color = color;
            //}
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
        }
    }

    public struct PointerEventArgs
    {
        public SteamVR_Input_Sources fromInputSource;
        public uint flags;
        public float distance;
        public Transform target;
    }

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);
}