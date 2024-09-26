using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.ReGetCom();
    }
}
