using Michsky.UI.Shift;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
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

    public GameObject gameManagerPrefab; // 게임 매니저 프리팹

    public Volume volume;
    public Bloom bloom;
    public Barrier barrier;
    public Player player;
    public AudioListener playerHead;
    public EnemyGenerate enemyGenerate;
    public LookOut lookOut;
    public Drone drone;
    public ModalWindowManager window;
    public EnemyMove enemyMove;
    public Camera mainCamera;
    public FadeInOut fadeInOut;

    public SteamVR_Action_Vibration hapticAction;
    [SerializeField]
    private float shakeAmount = 0.2f;
    public float ShakeAmount
    {
        get { return shakeAmount; }
    }
    [SerializeField]
    private float shakeSpeed = 1.0f;
    [SerializeField]
    private float vibrate = 0.1f;

    [SerializeField]
    int level = 0;

    public bool isBulletUpgrade = false;
    public bool isMagUpgrade = false;
    public bool isBarrierUpgrade = false;

    public int Level
    {
        get { return level; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ReGetCom();
        // gameClearObject = GameObject.Find("Turret").GetComponent<GameObject>();
    }

    //private void Update()
    //{
    //    level = Mathf.Clamp(level, 0, 4);
    //    if (drone.Animator.GetCurrentAnimatorStateInfo(0).IsName("DefenceEscape"))
    //    {
    //        if (drone.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) LoadingSceneManager.Load("Base");
    //    }
    //}

    public void ReGetCom()
    {
        player = FindObjectOfType<Player>();
        playerHead = FindObjectOfType<AudioListener>();
        barrier = FindObjectOfType<Barrier>();
        lookOut = FindObjectOfType<LookOut>();
        enemyGenerate = FindObjectOfType<EnemyGenerate>();
        drone = FindObjectOfType<Drone>();
        window = FindObjectOfType<ModalWindowManager>(true);
        enemyMove = FindObjectOfType<EnemyMove>();
        volume = FindObjectOfType<Volume>();
        volume.profile.TryGet(out bloom);
        mainCamera = Camera.main;

        if (player != null)
        {
            fadeInOut = player.GetComponentInChildren<FadeInOut>();
        }
    }

    public void ClearEnemy()
    {

    }

    /// <summary>
    /// 방어 실패 이벤트 함수
    /// </summary>
    public void DefFailureEvent()
    {
        fadeInOut._image.color = Color.white;
        fadeInOut.StartFadeOut();
        StartCoroutine("ChangeIntensityValue");
        Invoke("ChangeScene", fadeInOut._fadeTime);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Base");
    }

    public void LowHPEvent()
    {
        window.ModalWindowIn();
    }

    /// <summary>
    /// 방어 실패 후 돌아가는 이벤트 함수
    /// </summary>
    public void DefEscapeEvent()
    {
        window.ModalWindowOut();
        drone.Animator.Play("DefenceEscape");
        StartCoroutine(PlayerShake(10, 0));
        if (!enemyGenerate.canSpawn) level++;
        else level--;
    }

    /// <summary>
    /// 디펜스 성공 이벤트
    /// </summary>
    public void DefSuccessEvent()
    {
        lookOut.DefSuccessAnimation();
        Toast.Instance.Show("<size=20>적을 전부 소탕했습니다.\n터미널에서 드론을 호출하여 기지로 이동하십시오</size>", 30f, new Color(0, 1, 0));
        enemyGenerate.canSpawn = false;
    }

    public IEnumerator ChangeIntensityValue()
    {
        float time = 0;
        fadeInOut.StartFadeOut();
        while (true)
        {
            time += Time.deltaTime;
            bloom.intensity.value = Mathf.Lerp(1, 100, time/2f);
            if (bloom.intensity.value >= 100)
            {
                
                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// 플레이어 진동주기 (컨트롤러 포함)
    /// </summary>
    /// <returns>Null</returns>
    public IEnumerator PlayerShake(float time, float amount)
    {
        Vector3 originPosition = player.transform.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime <= time)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * amount;
            player.transform.localPosition = Vector3.Lerp(player.transform.localPosition, randomPoint, Time.deltaTime * shakeSpeed);
            yield return null;

            elapsedTime += Time.deltaTime;
            int ran = Random.Range(0, 2);
            Pulse(.01f, 150, ran, SteamVR_Input_Sources.LeftHand);
            Pulse(.01f, 150, ran, SteamVR_Input_Sources.RightHand);
        }
        // player.transform.localPosition = originPosition;
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
