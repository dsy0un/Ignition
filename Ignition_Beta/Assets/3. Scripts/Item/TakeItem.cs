using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TakeItem : MonoBehaviour
{
    [Tooltip("0: Pistol, 1: Rifle, 2: Shotgun")]
    [SerializeField]
    GameObject[] itemPrefab; // 0: Pistol, 1: Rifle, 2: Shotgun

    [SerializeField]
    Hand leftHand, rightHand;

    GameObject currentObject;

    GameObject spawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (leftHand.currentAttachedObject == null && rightHand.currentAttachedObject == null) return;
            if (leftHand.currentAttachedObject != null && rightHand.currentAttachedObject != null) return;

            if (leftHand.currentAttachedObject != null)
                currentObject = leftHand.currentAttachedObject;
                
            else if (rightHand.currentAttachedObject != null)
                currentObject = rightHand.currentAttachedObject;

            switch (currentObject.tag)
            {
                case "Pistol":
                    break;
                case "Rifle":
                    spawn = Instantiate(itemPrefab[1], other.transform.position, Quaternion.identity, other.transform);
                    break;
                case "Shotgun":
                    break;
                default: 
                    break;
            }
            foreach (var mesh in spawn.GetComponentsInChildren<MeshRenderer>())
                mesh.enabled = false;
            foreach (var canvas in spawn.GetComponentsInChildren<Canvas>())
                canvas.enabled = false;
            spawn.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentObject == null && other.CompareTag("Hand"))
        {
            foreach (var mesh in spawn.GetComponentsInChildren<MeshRenderer>())
                mesh.enabled = true;
            foreach (var canvas in spawn.GetComponentsInChildren<Canvas>())
                canvas.enabled = true;
            spawn.GetComponent<Rigidbody>().isKinematic = false;
            spawn.transform.SetParent(null);
        }
        //if (currentObject == null && other.CompareTag("Hand"))
        //{
        //    Destroy(spawn);
        //}
    }

    private void HandHoverUpdate(Hand hand)
    {
        Debug.Log(1);
        GrabTypes grab = hand.GetGrabStarting();
        bool isgrab = hand.IsGrabEnding(spawn);
        if (grab == GrabTypes.Grip && !isgrab)
        {
            foreach (var mesh in spawn.GetComponentsInChildren<MeshRenderer>())
                mesh.enabled = true;
            foreach (var canvas in spawn.GetComponentsInChildren<Canvas>())
                canvas.enabled = true;
            spawn.GetComponent<Rigidbody>().isKinematic = false;
            spawn.transform.SetParent(null);
        }
    }
}
