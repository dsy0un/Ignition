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

    private Barrier barrier;
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
        barrier = FindAnyObjectByType<Barrier>();
        cooldown = FindAnyObjectByType<Cooldown>();
        // gameClearObject = GameObject.Find("Turret").GetComponent<GameObject>();
    }

    public void ClearEnemy()
    {

    }
}
