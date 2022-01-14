namespace Sonderistic.UI
{
    using Sonderistic.Player.Input;
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class InputFieldDisplay : MonoBehaviour
    {
        #region Constants
        private const string DEFAULT_PLACEHOLDER_TEXT = "Enter Answer...";
        #endregion

        #region Variables
        [SerializeField]
        private GameObject displayGameObject;
        [SerializeField]
        private TMP_InputField inputField;
        [SerializeField]
        private TMP_Text promptText;
        [SerializeField]
        private TMP_Text placeholderText;
        [SerializeField]
        private Button confirmButton;

        private bool isDisplaying = false;
        private Action<string> currentInputCapturedCallback = null;
        private static InputFieldDisplay _instance;
        #endregion

        #region Properties
        public static InputFieldDisplay Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<InputFieldDisplay>();
                }

                return _instance;
            }
        }
        #endregion

        #region Methods
        public void DisplayInputField(string promptText, string placeholderText = null, Action<string> inputCapturedCallback = null)
        {
            if (isDisplaying == false)
            {
                inputField.text = string.Empty;
                PlayerInputActionService.onPlayerInputFieldInteractionPerformed += HandlePlayerInteractionPerformed;
                currentInputCapturedCallback = inputCapturedCallback;
                displayGameObject.SetActive(true);
                
                if (string.IsNullOrEmpty(placeholderText) == true)
                {
                    this.placeholderText.text = DEFAULT_PLACEHOLDER_TEXT;
                }
                else
                {
                    this.placeholderText.text = placeholderText;
                }

                this.promptText.text = promptText;
                isDisplaying = true;
            }
        }

        private void HandleConfirmButtonClicked()
        {
            if (IsInputValid() == true)
            {
                currentInputCapturedCallback?.Invoke(inputField.text);
                currentInputCapturedCallback = null;
                HideInputField();
            }
        }

        private void HideInputField()
        {
            displayGameObject.SetActive(false);
            isDisplaying = false;
            PlayerInputActionService.onPlayerInputFieldInteractionPerformed -= HandlePlayerInteractionPerformed;
        }

        private bool IsInputValid()
        {
            return !string.IsNullOrEmpty(inputField.text);
        }
        #endregion

        #region Callbacks
        private void HandlePlayerInteractionPerformed()
        {
            if (isDisplaying == true)
            {
                HandleConfirmButtonClicked();
            }
        }
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            confirmButton.onClick.AddListener(HandleConfirmButtonClicked);
            HideInputField();
        }
        #endregion
    }
}