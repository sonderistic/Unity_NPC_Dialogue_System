namespace Sonderistic.Player.Input
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public static class PlayerInputActionService
    {
        #region Variables
        public static event Action<Vector2> onPlayerHorizontalMovementPerformed = null;
        public static event Action<bool> onPlayerSprintPerformed = null;
        public static event Action onPlayerInteractionPerformed = null;
        public static event Action onPlayerInputFieldInteractionPerformed = null;

        private static PlayerControls playerControls;
        #endregion

        #region Properties
        public static float mouseXDelta { get; private set; }
        public static float mouseYDelta { get; private set; }
        #endregion

        #region Callbacks
        private static void HandlePlayerHorizontalMovementPerformed(InputAction.CallbackContext callbackContext)
        {
            onPlayerHorizontalMovementPerformed?.Invoke(callbackContext.ReadValue<Vector2>());
        }

        private static void HandlePlayerSprintPerformed(InputAction.CallbackContext callbackContext)
        {
            onPlayerSprintPerformed?.Invoke(true);
        }

        private static void HandlePlayerSprintCancelled(InputAction.CallbackContext callbackContext)
        {
            onPlayerSprintPerformed?.Invoke(false);
        }

        private static void HandlePlayerNPCInteractionPerformed(InputAction.CallbackContext callbackContext)
        {
            onPlayerInteractionPerformed?.Invoke();
        }

        private static void HandlePlayerInputFieldInteractionPerformed(InputAction.CallbackContext callbackContext)
        {
            onPlayerInputFieldInteractionPerformed?.Invoke();
        }

        private static void HandleMouseRotationX(InputAction.CallbackContext callbackContext)
        {
            mouseXDelta = callbackContext.ReadValue<float>();
        }

        private static void HandleMouseRotationY(InputAction.CallbackContext callbackContext)
        {
            mouseYDelta = callbackContext.ReadValue<float>();
        }
        #endregion

        #region Constructor
        static PlayerInputActionService()
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
            playerControls.GroundMovement.HorizontalMovement.performed += HandlePlayerHorizontalMovementPerformed;
            playerControls.GroundMovement.Sprint.performed += HandlePlayerSprintPerformed;
            playerControls.GroundMovement.Sprint.canceled += HandlePlayerSprintCancelled;
            playerControls.NPCInteraction.Interact.performed += HandlePlayerNPCInteractionPerformed;
            playerControls.InputFieldInteraction.Interact.performed += HandlePlayerInputFieldInteractionPerformed;
            playerControls.GroundMovement.LookRotationX.performed += HandleMouseRotationX;
            playerControls.GroundMovement.LookRotationY.performed += HandleMouseRotationY;
        }
        #endregion
    }
}