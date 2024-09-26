using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Tutorial : MonoBehaviour
{
    SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    SteamVR_Action_Boolean gripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Grip");
    SteamVR_Action_Boolean triggerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Trigger");
    SteamVR_Action_Vector2 axis2dAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("Axis2D");

    [SerializeField] Player player;
    Hand hand;
    int messageOrder;

    void Start()
    {
        messageOrder = 0;
        StartCoroutine("ToastMessage");
    }
    //ControllerButtonHints.ShowButtonHint(hand, teleportAction);

    private void Update()
    {
        foreach (Hand hands in player.hands)
        {
            hand = hands;
        }
    }

    IEnumerator ToastMessage()
    {
        switch (messageOrder)
        {
            case 0:
                Toast.Instance.Show("사격장에 오신걸 환영합니다.", 3);
                Toast.Instance.Show("사격장에서는 기본적인 게임플레이와 무기운용을 익힐수 있습니다", 3);
                messageOrder = 1;
                StartCoroutine("ToastMessage");
                break;
            case 1:
                Toast.Instance.Show("먼저 당신의 목표입니다.", 2);
                Toast.Instance.Show("당신은 각 임무 위치마다 포탑을 건설하여야 합니다.", 3);
                Toast.Instance.Show("건설이 완료될때까지 적들을 막아내며 방벽을 지켜야 하며", 4);
                Toast.Instance.Show("건설이 완료되기까지 남은시간은 뒤쪽을 보면 확인이 가능합니다", 5);
                Toast.Instance.Show("방벽은 버틸수 있는 에너지 양이 정해져 있으며", 3);
                Toast.Instance.Show("누적된 에너지양은 앞에있는 흰색 바에 나타납니다.", 3);
                Toast.Instance.Show("만약 방벽이 버틸수 있는 에너지의 양이 한계점에 가까워지면 경고가 뜨며", 6);
                yield return new WaitForSeconds(32f);
                GameManager.Instance.window.ModalWindowIn();
                yield return new WaitForSeconds(5f);
                GameManager.Instance.window.ModalWindowOut();
                yield return new WaitForSeconds(1f);
                Toast.Instance.Show("한계점을 넘어가게 되면 폭발하게 되어 사망할 수 있습니다.", 3);
                Toast.Instance.Show("포탑 건설이 완료되면 포탑이 적들을 모두 소탕하는것으로 임무는 끝입니다.", 6);
                yield return new WaitForSeconds(12f);
                messageOrder = 2;
                StartCoroutine("ToastMessage");
                break;
            case 2:
                Toast.Instance.Show("다음은 움직임 입니다.", 2);
                Toast.Instance.Show("왼쪽 컨트롤러의 조이스틱을 상하좌우로 움직여 걸어다닐수 있고", 6);
                Toast.Instance.Show("오른쪽 컨트롤러의 조이스틱을 좌우로 움직여 시야를 움직일 수 있으며", 7);
                Toast.Instance.Show("오른쪽 컨트롤러의 조이스틱을 앞으로 밀었다 놓으면 순간이동 할 수 있습니다", 8);
                yield return new WaitForSeconds(23f);
                Toast.Instance.Show("무기 사용 방법은 앞쪽 사격로에서 알 수 있습니다.", 3);
                break;
            default:
                break;
        }
        yield return null;
    }
}
