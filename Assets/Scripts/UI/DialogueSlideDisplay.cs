namespace Sonderistic.UI
{
    using Sonderistic.GameInteraction.Dialogue;
    using Sonderistic.Player.Input;
    using System;
    using TMPro;
    using UnityEngine;

    public class DialogueSlideDisplay : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private GameObject displayGameObject;
        [SerializeField]
        private TMP_Text dialogueText;
        [SerializeField]
        private TMP_Text continueDialoguePromptText;

        private static DialogueSlideDisplay _instance;
        private Action dialogueSlideEndCallback;
        private bool isDisplaying;
        #endregion

        #region Properties
        public static DialogueSlideDisplay Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<DialogueSlideDisplay>();
                }

                return _instance;
            }
        }
        #endregion

        #region Methods
        public void ReceivePlayerInput(bool shouldReceive)
        {
            continueDialoguePromptText.enabled = shouldReceive;
            PlayerInputActionService.onPlayerInteractionPerformed -= EndDisplay;
            
            if (shouldReceive == true)
            {
                PlayerInputActionService.onPlayerInteractionPerformed += EndDisplay;
            }
        }

        public void DisplayDialogueSlide(string dialogueSlideText, Action dialogueSlideEndCallback = null)
        {
            if (isDisplaying == false)
            {
                isDisplaying = true;
                dialogueText.enabled = true;
                displayGameObject.SetActive(true);
                this.dialogueSlideEndCallback = dialogueSlideEndCallback;
                dialogueText.text = dialogueSlideText;

                ReceivePlayerInput(true);
            }
        }

        public void DisplayDialogueSlide(DialogueSlideArgs dialogueSlideArgs, Action dialogueSlideEndCallback = null)
        {
            DisplayDialogueSlide(dialogueSlideArgs.dialogueSlideText, dialogueSlideEndCallback);
        }

        public void EndDisplay()
        {
            if (isDisplaying == true)
            {
                isDisplaying = false;
                HideDisplay();
                ReceivePlayerInput(false);
                dialogueSlideEndCallback?.Invoke();
            }
        }

        private void HideDisplay()
        {
            displayGameObject.SetActive(false);
        }
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            HideDisplay();
        }
        #endregion
    }
}