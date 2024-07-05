using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class LaserInput : MonoBehaviour
{
    GameObject currentObject;
    int currentID;

    public SteamVR_Action_Boolean fireAction;
    Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            int id = hit.collider.gameObject.GetInstanceID();
            if (currentID != id)
            {
                currentID = id;
                currentObject = hit.collider.gameObject;

                SteamVR_Input_Sources source = interactable.attachedToHand.handType;

                string name = currentObject.name;
                if (currentObject.GetComponent<Button>() != null)
                {
                    if (fireAction[source].stateDown)
                    {
                        if (name == "SetGeneral")
                        {

                        }
                        if (name == "SetAudio")
                        {

                        }
                        if (name == "SetGraphic")
                        {

                        }
                        if (name == "SetControl")
                        {

                        }
                        if (name == "SetGamePlay")
                        {

                        }
                        if (name == "SetBack")
                        {

                        }
                    }
                }
            }
        }
    }
}
