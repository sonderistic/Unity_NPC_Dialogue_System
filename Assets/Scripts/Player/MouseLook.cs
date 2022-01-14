namespace Sonderistic.Player.Movement
{
    using Sonderistic.Player.Input;
    using UnityEngine;

    public class MouseLook : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float sensitivityX = 8f;
        [SerializeField]
        private float sensitivityY = 0.5f;
        [SerializeField]
        private float xClampAngle = 85.0f;
        [SerializeField]
        private Transform playerCamera;
        public float mouseX;
        public float mouseY;

        private float xRotation;
        #endregion

        #region Unity Callbacks
        private void Update()
        {
            if (Player.Instance.GetPlayerState() == GameInteraction.EntityState.Free ||
                Player.Instance.GetPlayerState() == GameInteraction.EntityState.MovementFrozen)
            {
                mouseX = PlayerInputActionService.mouseXDelta * sensitivityX;
                mouseY = PlayerInputActionService.mouseYDelta * sensitivityY;
                transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -xClampAngle, xClampAngle);
                Vector3 targetRotation = transform.eulerAngles;
                targetRotation.x = xRotation;
                playerCamera.eulerAngles = targetRotation;
            }
        }
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            Cursor.visible = false;
        }
        #endregion
    }
}