using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Bolt : MonoBehaviour
{
    private Interactable interactable;
    private SpringJoint joint;
    public Socket socket;

    public GameObject round;
    public GameObject cartridge;
    public GameObject ejectPoint;
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float endPositionValue;
    public int jointValue;
    public bool redyToShot;
    public bool boltRetraction;

    public SteamVR_Action_Boolean grab;


    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        joint = GetComponent<SpringJoint>();
        originPosition = transform.localPosition;
        originRotation = transform.localRotation;
    }

    private void Start()
    {
        StartCoroutine("BoltAction");
    }

    IEnumerator BoltAction()
    {
        while (true)
        {
            transform.localRotation = originRotation;
            transform.localPosition = new Vector3(originPosition.x, originPosition.y, transform.localPosition.z);
            joint.spring = jointValue;
            if (interactable.attachedToHand != null)
            {
               joint.spring = 0;
            }
            if (transform.localPosition.z >= originPosition.z - 0.01f)
            {
                joint.spring = 0;
                transform.localPosition = originPosition;
                if (boltRetraction)
                {
                    redyToShot = true;
                }
            }
            if (transform.localPosition.z <= originPosition.z - endPositionValue)
            {
                transform.localPosition = new Vector3
                    (originPosition.x, originPosition.y, originPosition.z - endPositionValue);
                boltRetraction = true;
            }
            else if (boltRetraction && socket.IsMagazine)
            {
                round.SetActive(true);
            }
            yield return null;
        }
    }

    public void Shot()
    {
        round.SetActive(false);
        cartridge.SetActive(true);
        boltRetraction = false;
        redyToShot = false;
    }
}
