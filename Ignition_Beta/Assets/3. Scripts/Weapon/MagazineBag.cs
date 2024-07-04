using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MagazineBag : MonoBehaviour
{
    private Interactable interactable;
    public SteamVR_Action_Boolean grabMagazine;
    public GameObject magazinePrefab;
    private GameObject magazine;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            magazine = Instantiate(magazinePrefab, other.transform.position, other.transform.rotation);
            magazine.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hand")
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            magazine.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);

            if (grabMagazine[source].stateDown)
            {
                magazine.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand" && magazine.GetComponent<MeshRenderer>().enabled == false)
            Destroy(magazine);
    }
}
