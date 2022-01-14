namespace Sonderistic.UI
{
    using TMPro;
    using UnityEngine;

    public class OverlayDisplay : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private TMP_Text quickInfo;

        private static OverlayDisplay _instance;
        #endregion

        #region Properties
        public static OverlayDisplay Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<OverlayDisplay>();
                }

                return _instance;
            }
        }
        #endregion

        #region Methods
        public void SetQuickInfo(string description)
        {
            quickInfo.text = description;
            ShowQuickInfo(true);
        }

        public void ShowQuickInfo(bool isShow)
        {
            quickInfo.enabled = isShow;
        }
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            ShowQuickInfo(false);
        }
        #endregion
    }
}