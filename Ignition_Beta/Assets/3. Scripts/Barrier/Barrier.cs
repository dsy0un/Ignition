using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrier : MonoBehaviour, IHitAble
{
    [SerializeField]
    Image image;
    [SerializeField]
    float maxHP = 100;
    [SerializeField]
    float currentHP;
    [SerializeField]
    float breakTime;
    float currentTime;
    [SerializeField]
    Vector3 offset;
    Canvas canvas;
    Camera mainCamera;
    Animator animator;

    bool isDie;
    bool isWarning;

    bool isUpgrade = false;

    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        isDie = false;
        isWarning = false;
        currentHP = maxHP;
        currentTime = breakTime;
        StartCoroutine(CoroutineUpdate());
    }

    private void Update()
    {
        FollowCamera();
    }

    /// <summary>
    /// UI가 카메라를 따라가기 위한 함수
    /// </summary>
    /// <returns>Null</returns>
    void FollowCamera()
    {
        canvas.transform.position = Vector3.Lerp(canvas.transform.position, mainCamera.transform.position
            + mainCamera.transform.forward * offset.z
            + mainCamera.transform.up * offset.y
            + mainCamera.transform.right * offset.x,
            3 * Time.deltaTime);

        Vector3 l_vector = mainCamera.transform.position - canvas.transform.position;
        canvas.transform.rotation = Quaternion.LookRotation(-l_vector).normalized;
    }

    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            if (!isUpgrade && GameManager.Instance.isBarrierUpgrade)
            {
                isUpgrade = true;
                currentHP *= 1.5f;
                maxHP *= 1.5f;
                //animator.Play("Barrier");
            }
            image.fillAmount = currentHP / maxHP;

            yield return null;
        }
    }

    public void Hit(float dmg, string coliName)
    {
        currentHP -= dmg;
        if (currentHP <= 0 && !isDie)
        {
            Die();
        }
        else if (currentHP <= maxHP * 0.3f && !isWarning)
        {
            GameManager.Instance.LowHPEvent();
            isWarning = true;
        }
    }

    public void Die()
    {
        GameManager.Instance.DefFailureEvent();
        gameObject.SetActive(false);
        //StartCoroutine(Escape(breakTime));
        isDie = true;
    }

    public IEnumerator Escape(float time = 0)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
        GameManager.Instance.DefEscapeEvent();
        gameObject.SetActive(false);
    }

    public void Respawn()
    {

    }
}
