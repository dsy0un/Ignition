using System.Collections;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEditorInternal;
using UnityEngine;

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
        barrier = FindObjectOfType<Barrier>();
    }

    public void ClearEnemy()
    {

    }
    public void DefSuccess()
    {

    }
}
