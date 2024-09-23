using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TakeItem : MonoBehaviour
{
    [Tooltip("0: Pistol, 1: Rifle, 2: Shotgun")]
    [SerializeField]
    GameObject[] itemPrefab; // 0: Pistol, 1: Rifle, 2: Shotgun

    [SerializeField]
    Hand leftHand, rightHand; // 플레이어 왼손, 오른손 

    GameObject currentObject; // 현재 들고 있는 오브젝트

    GameObject spawn; // 소환된 아이템

    [SerializeField]
    int maxItemCount = 15;
    int currentItemCount;

    bool isUpgrade = false;

    private void Start()
    {
        currentItemCount = maxItemCount;
    }

    private void Update()
    {
        if (!isUpgrade && GameManager.Instance.isMagUpgrade)
        {
            isUpgrade = true;
            currentItemCount = maxItemCount * 2;
        }
    }

    /// <summary>
    /// 들고 있는 총에 알맞은 아이템 소환
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (currentItemCount >= 1 && other.CompareTag("Hand"))
        {
            if (leftHand.currentAttachedObject == null && rightHand.currentAttachedObject == null) return; // 양손 다 들고 있지 않을 때
            if (leftHand.currentAttachedObject != null && rightHand.currentAttachedObject != null) return; // 양손 다 들고 있을 때

            if (leftHand.currentAttachedObject != null) // 왼손에 들고 있는 오브젝트가 있을 때
                currentObject = leftHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가
                
            else if (rightHand.currentAttachedObject != null) // 오른손에 들고 있는 오브젝트가 있을 때
                currentObject = rightHand.currentAttachedObject; // currentObject에 들고 있는 오브젝트 추가

            switch (currentObject.tag) // 들고 있는 오브젝트의 태그를 switch 문으로 풀기
            {
                case "Pistol": // 권총일 때
                    spawn = Instantiate(itemPrefab[0], other.transform.position, Quaternion.identity, other.transform);
                    break;
                case "Rifle": // 소총일 때
                    // 탄창 소환 후 spawn에 추가
                    spawn = Instantiate(itemPrefab[1], other.transform.position, Quaternion.identity, other.transform);
                    break;
                case "Shotgun": // 샷건일 때
                    spawn = Instantiate(itemPrefab[2], other.transform.position, Quaternion.identity, other.transform);
                    break;
                default: 
                    break;
            }
            spawn.GetComponent<Collider>().isTrigger = true;
            foreach (var mesh in spawn.GetComponentsInChildren<MeshRenderer>())
                mesh.enabled = false;
            foreach (var canvas in spawn.GetComponentsInChildren<Canvas>())
                canvas.enabled = false;
            spawn.GetComponent<Rigidbody>().isKinematic = true; // spawn의 Rigidbody를 감지 후 isKenematic 켜기
        }
    }

    /// <summary>
    /// 상자에서 손을 뺐을 때 실행
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (currentObject != null && other.CompareTag("Hand"))
        {
            if (leftHand.currentAttachedObject != null && rightHand.currentAttachedObject != null) // 양손 다 들고 있을 때
            {
                foreach (var mesh in spawn.GetComponentsInChildren<MeshRenderer>())
                    mesh.enabled = true;
                foreach (var canvas in spawn.GetComponentsInChildren<Canvas>())
                    canvas.enabled = true;
                spawn.GetComponent<Rigidbody>().isKinematic = false; // isKenematic 끄기
                spawn.GetComponent<Collider>().isTrigger = false;
                spawn.transform.SetParent(null); // spawn의 부모 제거
                currentItemCount--;
            }
            else Destroy(spawn); // 아니면 Destroy
        }
    }

    //private void HandHoverUpdate(Hand hand) 이거 대체했음
    //{
    //    GrabTypes grab = hand.GetGrabStarting();
    //    bool isgrab = hand.IsGrabEnding(spawn);
    //    if (grab == GrabTypes.Grip && !isgrab)
    //    {

    //    }
    //}
}
