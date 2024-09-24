using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class ButtonClickEvent : MonoBehaviour
{
    [SerializeField]
    UnityEvent onCollisionEvent;

    [SerializeField]
    UnityEvent onTriggerEvent;
    [SerializeField]
    UnityEvent onTriggerExitEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("interact"))
        {
            // ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
            onCollisionEvent?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        onTriggerEvent?.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        onTriggerExitEvent?.Invoke();
    }
}
