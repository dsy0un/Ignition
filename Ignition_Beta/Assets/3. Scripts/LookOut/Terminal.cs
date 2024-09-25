using Michsky.UI.Shift;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR.InteractionSystem;
using TMPro;

public class Terminal : MonoBehaviour
{
    [SerializeField]
    Animator drone;
    [SerializeField]
    bool sharpAnimations = false;
    [SerializeField]
    TextMeshProUGUI leftTime;
    [SerializeField]
    float arriveTime;
    float currentArriveTime;
    //[SerializeField]
    //float bulletUpgradeCooldown;
    //float currentBulletUpgradeCooldown;
    //[SerializeField]
    //float magazineUpgradeCooldown;
    //float currentMagazineUpgradeCooldown;
    [SerializeField]
    float coolTime;
    float currentCoolTime;
    int min;
    int sec;

    Animator runMonitor;
    Animator terminal;
    bool isOn = false;
    bool isDrone = false;

    enum IsAnimState
    {
        None,
        First,
        Reinforce,
        CallDrone,
        Sure
    }
    IsAnimState isAnimState;

    private void Awake()
    {
        runMonitor = transform.parent.GetComponent<Animator>();
        terminal = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isDrone = false;
        currentArriveTime = arriveTime;
        //currentBulletUpgradeCooldown = bulletUpgradeCooldown;
        //currentMagazineUpgradeCooldown = magazineUpgradeCooldown;
        currentCoolTime = coolTime;
    }

    private void Start()
    {
        //ModalAnim("Terminal In");
        //isAnimState = IsAnimState.First;
    }

    private void Update()
    {
        if (currentCoolTime <= coolTime)
        {
            currentCoolTime -= Time.deltaTime;
            min = (int)currentCoolTime / 60 % 60;
            sec = (int)currentCoolTime % 60;
        }
        if (isAnimState == IsAnimState.Sure)
        {
            if (currentArriveTime == arriveTime) isDrone = true;
            if (isDrone)
            {
                currentArriveTime -= Time.deltaTime;
                int min = (int)currentArriveTime / 60 % 60;
                switch (currentArriveTime)
                {
                    case float n when (n <= 30f && n >= 10f):
                        leftTime.text = $"도착까지 남은 시간 : <color=orange>{min:D2}:{currentArriveTime:00.00}</color>";
                        break;
                    case float n when (n <= 10f):
                        leftTime.text = $"도착까지 남은 시간 : <color=red>{min:D2}:{currentArriveTime:00.00}</color>";
                        break;
                    default:
                        leftTime.text = $"도착까지 남은 시간 : <color=white>{min:D2}:{currentArriveTime:00.00}</color>";
                        break;
                }
                if (currentArriveTime <= 0 && terminal.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    isDrone = false;
                    ModalAnim("Terminal SureToOut");
                    if (terminal.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    {
                        if (!sharpAnimations)
                            drone.CrossFade("DefenceFailure", 0.1f);
                        else
                            drone.Play("DefenceFailure");
                    }
                }
            }
            else
            {
                currentArriveTime = arriveTime;
                isAnimState = IsAnimState.None;
            }
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

    /// <summary>
    /// 터미널 실행 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void StartTerminalButtonClick()
    {
        if (isAnimState == IsAnimState.None)
        {
            runMonitor.SetBool("IsOn", true);
            if (runMonitor.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                ModalAnim("Terminal In");
                isAnimState = IsAnimState.First;
            }
        }
    }

    // -----------------------------------------------------

    /// <summary>
    /// 총기 피해량 업그레이드 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void WeaponDamageButtonClick() // 5분 쿨타임, 1.5배 증가
    {
        if (currentCoolTime <= 0)
        {
            if (!GameManager.Instance.isBulletUpgrade)
            {
                GameManager.Instance.isBulletUpgrade = true;
                Toast.Instance.Show("[System] 총알 피해량이 증가했습니다.", 5.0f, new Color(1, 1, 0));
            }
        }
        else
        {
            if (min >= 1) Toast.Instance.Show($"[System] 다음 시간 후 업그레이드가 가능합니다. {min:00}:{sec:00}", 5.0f, new Color(1, 1, 0));
            else Toast.Instance.Show($"[System] 다음 시간 후 업그레이드가 가능합니다. {min:00}:{currentCoolTime:00.00}", 5.0f, new Color(1, 1, 0));
        }
    }

    /// <summary>
    /// 총기 탄창 업그레이드 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void WeaponMagazineButtonClick() // 5분 쿨타임, 1.5배 증가 
    {
        if (currentCoolTime <= 0)
        {
            if (!GameManager.Instance.isMagUpgrade)
            {
                GameManager.Instance.isMagUpgrade = true;
                Toast.Instance.Show("[System] 탄창의 총알 수와 꺼낼 수 있는 탄창의 수가 증가했습니다.", 5.0f, new Color(1, 1, 0));
            }
        }
        else
        {
            if (min >= 1) Toast.Instance.Show($"[System] 다음 시간 후 업그레이드가 가능합니다. {min}:{sec}", 5.0f, new Color(1, 1, 0));
            else Toast.Instance.Show($"[System] 다음 시간 후 업그레이드가 가능합니다. {min}:{currentCoolTime:00.00}", 5.0f, new Color(1, 1, 0));
        }
    }

    /// <summary>
    /// 베리어 체력 업그레이드 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void BarrierHealthButtonClick() // 5분 쿨타임, 1.5배 증가
    {
        if (currentCoolTime <= 0)
        {
            if (!GameManager.Instance.isBarrierUpgrade)
            {
                GameManager.Instance.isBarrierUpgrade = true;
                Toast.Instance.Show("[System] 베리어의 체력이 증가했습니다.", 5.0f, new Color(1, 1, 0));
            }
        }
        else
        {
            if (min >= 1) Toast.Instance.Show($"[System] 다음 시간 후 업그레이드가 가능합니다. {min}:{sec}", 5.0f, new Color(1, 1, 0));
            else Toast.Instance.Show($"[System] 다음 시간 후 업그레이드가 가능합니다. {min}:{currentCoolTime:00.00}", 5.0f, new Color(1, 1, 0));
        }
    }

    /// <summary>
    /// 드론 호출 확인 버튼 클릭 이벤트 실행 함수
    /// </summary>
    public void AreYouSureButtonClick() // 한 번 실행하면 취소 불가능
    {
        ModalAnim("Terminal CallDroneToSure");
        isAnimState = IsAnimState.Sure;
    }
}
