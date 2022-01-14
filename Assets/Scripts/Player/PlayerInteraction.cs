namespace Sonderistic.Player
{
    using Sonderistic.Player.Input;
    using UnityEngine;

    public class PlayerInteraction : MonoBehaviour
    {
        #region Methods

        #endregion

        #region Callbacks
        private void HandlePlayerNPCInteractionPerformed()
        {
            
        }
        #endregion

        #region MonoBehaviour Callbacks
        private void OnEnable()
        {
            PlayerInputActionService.onPlayerInteractionPerformed += HandlePlayerNPCInteractionPerformed;
        }

        private void OnDisable()
        {
            PlayerInputActionService.onPlayerInteractionPerformed -= HandlePlayerNPCInteractionPerformed;
        }
        #endregion
    }
}