using Michsky.UI.Shift;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Terminal : MonoBehaviour
{
    ModalWindowManager window;
    Interactable openWindow;
    Animator runMonitor;

    private void Awake()
    {
        runMonitor = transform.parent.GetComponent<Animator>();
        window = GetComponentInChildren<ModalWindowManager>();
    }

    void HandHoverUpdate(Hand hand)
    {
        runMonitor.SetBool("IsOn", true);
        if (runMonitor.GetBool("IsOn") && runMonitor.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            window.ModalWindowIn();
    }
}
