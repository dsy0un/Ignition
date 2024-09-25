using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Michsky.UI.Shift
{
    public class ModalWindowManager : MonoBehaviour
    {
        [Header("Resources")]
        public TextMeshProUGUI windowTitle;
        public TextMeshProUGUI windowDescription;
        public TextMeshProUGUI windowTimer;

        [Header("Settings")]
        public bool sharpAnimations = false;
        public bool useCustomTexts = false;
        public string titleText = "Title";
        [TextArea] public string descriptionText = "Description here";
        public string timerText = "00:00.00";

        Animator mWindowAnimator;
        AudioSource audioSource;
        bool isOn = false;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            mWindowAnimator = gameObject.GetComponent<Animator>();

            if (useCustomTexts == false)
            {
                windowTitle.text = titleText;
                windowDescription.text = descriptionText;
                if (windowTimer)
                    windowTimer.text = timerText;
            }

            gameObject.SetActive(false);

            // ModalWindowIn();
        }

        public void ModalWindowIn()
        {
            StopCoroutine(nameof(DisableWindow));
            gameObject.SetActive(true);

            if (isOn == false)
            {
                if (sharpAnimations == false)
                    mWindowAnimator.CrossFade("Window In", 0.1f);
                else
                    mWindowAnimator.Play("Window In");

                isOn = true;
                audioSource.Play();
            }
        }

        public void ModalWindowOut()
        {
            if (isOn == true)
            {
                if (sharpAnimations == false)
                    mWindowAnimator.CrossFade("Window Out", 0.1f);
                else
                    mWindowAnimator.Play("Window Out");

                isOn = false;
                audioSource.Stop();
            }

            //StartCoroutine("DisableWindow");
        }

        IEnumerator DisableWindow()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }
}