using UnityEngine;
using Valve.VR.InteractionSystem;

public class Terminal : MonoBehaviour
{
    [SerializeField]
    Animator monitorAnim;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void HandHoverUpdate(Hand hand)
    {
        monitorAnim.SetBool("IsOn", true);
    }
}
