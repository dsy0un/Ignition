using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GrenadeSafeDv : MonoBehaviour
{
    private Grenade grenade;
    private LinearMapping mapping;
    private LinearDrive drive;
    private Interactable interactable;
    public int topBottom;
    [HideInInspector]
    public bool isReady;

    private void Awake()
    {
        grenade = GetComponentInParent<Grenade>();
        mapping = GetComponent<LinearMapping>();
        drive = GetComponent<LinearDrive>();
        interactable = GetComponent<Interactable>();
    }

    private void OnEnable()
    {
        transform.position = drive.startPosition.position;
        mapping.value = 0;
        drive.endPosition.gameObject.SetActive(false);
        StartCoroutine("UnSetSafe");
    }

    IEnumerator UnSetSafe()
    {
        while (true)
        {
            if (mapping.value == 1)
            {
                if (interactable.attachedToHand)
                    interactable.attachedToHand.DetachObject(gameObject);
                grenade.safeDv[topBottom] = GetComponent<GrenadeSafeDv>();
                isReady = true;
                drive.endPosition.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
