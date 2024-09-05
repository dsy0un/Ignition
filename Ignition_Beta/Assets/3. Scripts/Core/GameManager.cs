using System.Collections;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEditorInternal;
using UnityEngine;
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
    }

    public void ClearEnemy()
    {

    }

    /// <summary>
    /// 방어 성공 이벤트 함수
    /// </summary>
    public void DefSuccessEvent()
    {
        lookOut.DefSuccessAnimation();  
        enemyGenerate.canSpawn = false;
    }

    /// <summary>
    /// 방어 실패 이벤트 함수
    /// </summary>
    public void DefFailureEvent()
    {

    }
}
