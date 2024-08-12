using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DSY
{
    public class ClickButton : MonoBehaviour
    {
        [Header("Main Select")]
        [SerializeField]
        GameObject select;
        //[SerializeField]
        //Button generalBtn;
        //[SerializeField]
        //Button audioBtn, graphicBtn, controlBtn, gameplayBtn;

        [Header("Next Select")]
        [SerializeField]
        GameObject general;
        [SerializeField]
        new GameObject audio;
        [SerializeField]
        GameObject graphic, control, gameplay, back;
        [SerializeField]
        RectTransform nextSel;

        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            
        }

        public void ClickGeneralButton()
        {
            general.SetActive(true);
            audio.SetActive(false);
            graphic.SetActive(false);
            control.SetActive(false);
            gameplay.SetActive(false);
            back.SetActive(true);

            animator.SetBool("Click", true);
            new WaitForSeconds(0.25f);
            nextSel.transform.SetAsLastSibling();

            Interactable(false);
        }

        public void ClickAudioButton()
        {
            general.SetActive(false);
            audio.SetActive(true);
            graphic.SetActive(false);
            control.SetActive(false);
            gameplay.SetActive(false);
            back.SetActive(true);

            animator.SetBool("Click", true);
            new WaitForSeconds(0.25f);
            nextSel.transform.SetAsLastSibling();

            Interactable(false);
        }

        public void ClickGraphicButton()
        {
            general.SetActive(false);
            audio.SetActive(false);
            graphic.SetActive(true);
            control.SetActive(false);
            gameplay.SetActive(false);
            back.SetActive(true);

            animator.SetBool("Click", true);
            new WaitForSeconds(0.25f);
            nextSel.transform.SetAsLastSibling();

            Interactable(false);
        }

        public void ClickControlButton()
        {
            general.SetActive(false);
            audio.SetActive(false);
            graphic.SetActive(false);
            control.SetActive(true);
            gameplay.SetActive(false);
            back.SetActive(true);

            animator.SetBool("Click", true);
            new WaitForSeconds(0.25f);
            nextSel.transform.SetAsLastSibling();

            Interactable(false);
        }

        public void ClickGamePlayButton()
        {
            general.SetActive(false);
            audio.SetActive(false);
            graphic.SetActive(false);
            control.SetActive(false);
            gameplay.SetActive(true);
            back.SetActive(true);

            animator.SetBool("Click", true);
            new WaitForSeconds(0.25f);
            nextSel.transform.SetAsLastSibling();

            Interactable(false);
        }

        public void ClickNextSelBackButton()
        {
            animator.SetBool("Click", false);
            new WaitForSeconds(0.25f);
            nextSel.transform.SetAsFirstSibling();

            general.SetActive(false);
            audio.SetActive(false);
            graphic.SetActive(false);
            control.SetActive(false);
            gameplay.SetActive(false);
            back.SetActive(false);

            Interactable(true);
        }

        /// <summary>
        /// Button Interactable on/off
        /// </summary>
        /// <param name="isInteract">turn on/off</param>
        void Interactable(bool isInteract)
        {
            Button[] btns = select.GetComponentsInChildren<Button>();
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].interactable = isInteract;
            }
        }
    }
}
