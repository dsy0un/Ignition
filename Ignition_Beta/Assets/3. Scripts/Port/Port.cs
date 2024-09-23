using Michsky.UI.Shift;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class Port : MonoBehaviour
{
    [SerializeField]
    private GameObject lever;

    private ModalWindowManager mwm;
    private LinearMapping linear;
    private CircularDrive circular;
    private Interactable interactable;
    private string mapName;

    float time;
    float mapping;
    float zF;
    Vector3 zV;
    AsyncOperation op;

    private void Awake()
    {
        mwm = GetComponent<ModalWindowManager>();
        linear = lever.GetComponent<LinearMapping>();
        circular = lever.GetComponent<CircularDrive>();
        interactable = lever.GetComponent<Interactable>();
    }
    private void Start()
    {
        op = SceneManager.LoadSceneAsync("Loading");
        op.allowSceneActivation = false;
    }
    private void Update()
    {
        if (linear.value == 0)
        {
            time = 0f;
            zV = Vector3.zero;
        }

        else if (linear.value != 1)
        {
            if (interactable.attachedToHand != null)
            {
                //SceneManager.LoadSceneAsync("Loading");
                zF = lever.transform.rotation.z;
                mapping = linear.value;
            }
            else if (interactable.attachedToHand == null)
            {
                time += Time.deltaTime;
                zV = new Vector3(0f, 0f, Mathf.Lerp(zF, circular.minAngle, time / 0.3f));
                linear.value = Mathf.Lerp(mapping, 0, time / 0.3f);
                circular.outAngle = circular.minAngle;
                lever.transform.rotation = Quaternion.Euler(zV);
            }
            return;
        }
        else if (linear.value == 1 && mapName != null)
        {
            if (op.progress < 0.9f)
            {
            }
            else
            {
                LoadingSceneManager.LoadScene(mapName);
                op.allowSceneActivation = true;
            }
        }
    }
    public void OnTriggerEnters()
    {
        mwm.ModalWindowOut();
    }
    public void OnTriggerExits()
    {
        if (interactable.attachedToHand == null)
        {
            mwm.ModalWindowIn();
        }
    }
    public void stageSelect(string name)
    {
        mapName = name;
    }
}
