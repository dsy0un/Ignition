using Michsky.UI.Shift;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Terminal : MonoBehaviour
{
    [SerializeField]
    bool sharpAnimations = false;

    Interactable openWindow;
    Animator runMonitor;
    Animator terminal;
    bool isOn = false;

    private void Awake()
    {
        runMonitor = transform.parent.GetComponent<Animator>();
        terminal = transform.GetChild(0).GetComponent<Animator>();
    }

    void OnHandHoverBegin(Hand hand)
    {
        if (runMonitor.GetBool("IsOn"))
        {
            ModalAnim("Terminal Out");
            if (terminal.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                runMonitor.SetBool("IsOn", false);
        }
        else
        {
            runMonitor.SetBool("IsOn", true);
            if (runMonitor.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                ModalAnim("Terminal In");
        }
    }

    /// <summary>
    /// 터미널 관련 애니메이션 실행 함수
    /// </summary>
    /// <param name="animName"></param>
    public void ModalAnim(string animName)
    {
        if (sharpAnimations == false)
            terminal.CrossFade(animName, 0.1f);
        else
            terminal.Play(animName);
    }
}
