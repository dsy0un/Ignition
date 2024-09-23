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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("interact"))
        {
            // ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
            onCollisionEvent?.Invoke();
        }
    }
}
