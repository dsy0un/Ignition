using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

namespace EW
{

    public class Document : MonoBehaviour
    {
        public Open open;
        public M_Position position;
        public Animator animator;
        public FadeInOut fadeInOut;

        public GameObject file;

        private Animator thisAnimator;
        private bool documentOpen = false;
        private string callSign;

        // Start is called before the first frame update
        void Start()
        {
            open = open.gameObject.GetComponent<Open>();
            position = position.gameObject.GetComponent<M_Position>();
            thisAnimator = this.gameObject.GetComponent<Animator>();
            fadeInOut = fadeInOut.gameObject.GetComponent<FadeInOut>();

        }

        public void File()
        {
            file.SetActive(true);
        }

        private void HandHoverUpdate(Hand hand)
        {
            GrabTypes grab = hand.GetGrabStarting();
            bool isgrab = hand.IsGrabEnding(gameObject);
            if (grab == GrabTypes.Pinch && !isgrab && !documentOpen)
            {
                thisAnimator.SetTrigger("Open");
                documentOpen = true;
            }
            else if (grab == GrabTypes.Pinch && !isgrab && documentOpen && position.checkBox && open.textInput)
            {
                SceneManager.LoadScene("Logo");
            }
        }

    }

}