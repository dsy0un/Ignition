using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GrenadeSafeDv : MonoBehaviour
{
    private Grenade grenade;
    private LinearMapping mapping;
    private Interactable interactable;
    public GameObject EndPos;
    public int topBottom;
    [HideInInspector]
    public bool isReady;

    private void Awake()
    {
        grenade = GetComponentInParent<Grenade>();
        mapping = GetComponent<LinearMapping>();
        interactable = GetComponent<Interactable>();
    }

    private void Start()
    {
        StartCoroutine("UnSetSafe");
    }

    IEnumerator UnSetSafe()
    {
        while (true)
        {
            if (mapping.value == 1)
            {
                interactable.attachedToHand.DetachObject(gameObject);
                grenade.safeDv[topBottom] = GetComponent<GrenadeSafeDv>();
                isReady = true;
                EndPos.SetActive(true);
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
