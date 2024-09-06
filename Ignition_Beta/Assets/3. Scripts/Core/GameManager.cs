using Michsky.UI.Shift;
using System.Collections;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEditorInternal;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) 
                return null;
            return instance;
        }
    }

    public Barrier barrier;
    public Player player;
    public EnemyGenerate enemyGenerate;
    private LookOut lookOut;
    private Drone drone;
    private ModalWindowManager window;

    public SteamVR_Action_Vibration hapticAction;
    [SerializeField]
    private float shakeAmount = 0.2f;
    [SerializeField]
    private float shakeSpeed = 1.0f;
    [SerializeField]
    private float vibrate = 0.1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ReGetCom();
        // gameClearObject = GameObject.Find("Turret").GetComponent<GameObject>();
    }

    public void ReGetCom()
    {
        player = FindObjectOfType<Player>();
        barrier = FindObjectOfType<Barrier>();
        lookOut = FindObjectOfType<LookOut>();
        enemyGenerate = FindObjectOfType<EnemyGenerate>();
        drone = FindObjectOfType<Drone>(true);
        window = FindObjectOfType<ModalWindowManager>();
    }

    public void ClearEnemy()
    {

    }

    /// <summary>
    /// 방어 실패 이벤트 함수
    /// </summary>
    public void DefFailureEvent()
    {
        window.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// 방어 실패 후 돌아가는 이벤트 함수
    /// </summary>
    public void DefEscapeEvent()
    {
        drone.Animator.SetBool("TimeOut", true);
    }

    /// <summary>
    /// 디펜스 성공 이벤트
    /// </summary>
    public void DefSuccessEvent()
    {
        lookOut.DefSuccessAnimation();
        enemyGenerate.canSpawn = false;
    }

    /// <summary>
    /// 플레이어 진동주기 (컨트롤러 포함)
    /// </summary>
    /// <returns>Null</returns>
    public IEnumerator PlayerShake(float time)
    {
        Vector3 originPosition = player.transform.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime <= time)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
            player.transform.localPosition = Vector3.Lerp(player.transform.localPosition, randomPoint, Time.deltaTime * shakeSpeed);
            yield return null;

            elapsedTime += Time.deltaTime;
            int ran = Random.Range(0, 2);
            Pulse(.01f, 150, ran, SteamVR_Input_Sources.LeftHand);
            Pulse(.01f, 150, ran, SteamVR_Input_Sources.RightHand);
        }
        //player.transform.localPosition = originPosition;
    }
    /// <summary>
    /// 컨트롤러 진동
    /// </summary>
    /// <param name="duration">지속시간</param>
    /// <param name="frequency">Hz (값 바꿔도 큰 변화 없음)</param>
    /// <param name="amplitude">진동 강도</param>
    /// <param name="source">컨트롤러 종류</param>
    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);
    }
}
