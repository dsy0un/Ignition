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
    private Cooldown cooldown;
    

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
        cooldown = FindObjectOfType<Cooldown>();
        player = FindObjectOfType<Player>();
        barrier = FindObjectOfType<Barrier>();
    }

    public void ClearEnemy()
    {

    }
    public void DefSuccessEvent()
    {
        
    }
}
