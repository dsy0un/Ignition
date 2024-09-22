using Michsky.UI.Shift;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Terminal : MonoBehaviour
{
    [SerializeField]
    bool sharpAnimations = false;
    [SerializeField]
    float arriveTime;
    float currentTime;

    Interactable openWindow;
    Animator runMonitor;
    Animator terminal;
    bool isOn = false;
    bool isEscape = false;

    enum IsAnimState
    {
        None,
        First,
        Reinforce,
        CallDrone
    }
    IsAnimState isAnimState;

    private void Awake()
    {
        // runMonitor = transform.parent.GetComponent<Animator>();
        terminal = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isEscape = false;
        currentTime = arriveTime;
    }

    private void Start()
    {
        ModalAnim("Terminal In");
        isAnimState = IsAnimState.First;
    }

    private void Update()
    {
        if (isEscape)
        {
            currentTime -= Time.deltaTime;
            int min = (int)currentTime / 60 % 60;
            switch (currentTime)
            {
                case float n when (n <= 30f && n >= 10f):
                    GameManager.Instance.window.windowTimer.text = $"도착까지 남은 시간 : <color=orange>{min:D2}:{currentTime:00.00}</color>";
                    break;
                case float n when (n <= 10f):
                    GameManager.Instance.window.windowTimer.text = $"도착까지 남은 시간 : <color=red>{min:D2}:{currentTime:00.00}</color>";
                    break;
                default:
                    GameManager.Instance.window.windowTimer.text = $"도착까지 남은 시간 : <color=white>{min:D2}:{currentTime:00.00}</color>";
                    break;
            }
        }
    }

    //void OnHandHoverBegin(Hand hand)
    //{
    //    if (runMonitor.GetBool("IsOn")) // 나중에 멀리 가면 꺼지게하기
    //    {
    //        ModalAnim("Terminal Out");
    //        isAnimState = IsAnimState.None;
    //        if (terminal.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
    //            runMonitor.SetBool("IsOn", false);
    //    }
    //    else
    //    {
    //        runMonitor.SetBool("IsOn", true);
    //        if (runMonitor.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
    //        {
    //            ModalAnim("Terminal In");
    //            isAnimState = IsAnimState.First;
    //        }
    //    }
    //}

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

    /// <summary>
    /// 강화하기 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void ReinforceButtonClick()
    {
        ModalAnim("Terminal FirstToReinforce");
        isAnimState = IsAnimState.Reinforce;
    }

    /// <summary>
    /// 드론 호출하기 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void CallDroneButtonClick()
    {
        ModalAnim("Terminal FirstToCallDrone");
        isAnimState = IsAnimState.CallDrone;
    }

    /// <summary>
    /// 뒤로가기(오른쪽 위 "X") 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void GoBackButtonClick()
    {
        switch (isAnimState)
        {
            case IsAnimState.First:
                ModalAnim("Terminal Out");
                isAnimState = IsAnimState.None;
                if (terminal.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    runMonitor.SetBool("IsOn", false);
                break;
            case IsAnimState.Reinforce:
                ModalAnim("Terminal ReinforceToFirst");
                isAnimState = IsAnimState.First;
                break;
            case IsAnimState.CallDrone:
                ModalAnim("Terminal CallDroneToFirst");
                isAnimState = IsAnimState.First;
                break;
            default:
                break;
        }
    }

    // -----------------------------------------------------

    /// <summary>
    /// 총기 피해량 업그레이드 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void WeaponDamageButtonClick() // 5분 쿨타임
    {

    }

    /// <summary>
    /// 총기 탄창 업그레이드 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void WeaponMagazineButtonClick() // 5분 쿨타임
    {

    }

    /// <summary>
    /// 드론 호출 확인 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void AreYouSureButtonClick() // 한 번 실행하면 취소 불가능
    {
        isEscape = true;
        
    }
}
