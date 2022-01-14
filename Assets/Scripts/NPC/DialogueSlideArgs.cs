namespace Sonderistic.GameInteraction.Dialogue
{
    using System;
    using UnityEngine;

    [Serializable]
    public class DialogueSlideArgs
    {
        #region Variables
        [SerializeField]
        [TextArea]
        private string _dialogue;
        [SerializeField]
        private bool _takesInput;
        [SerializeField]
        [TextArea]
        private string _inputFieldPromptText;
        [SerializeField]
        [TextArea]
        private string _responseToInputStringTemplate;
        #endregion

        #region Properties
        public string dialogueSlideText
        {
            get
            {
                return _dialogue; 
            }
        }

        public bool takesInput
        {
            get
            {
                return _takesInput;
            }
        }

        public string inputFieldPromptText
        {
            get
            {
                return _inputFieldPromptText;
            }
        }

        public string responseToInputStringTemplate
        {
            get
            {
                return _responseToInputStringTemplate;
            }
        }
        #endregion
    }
}