namespace Sonderistic.GameInteraction
{
    using Sonderistic.Player;
    using Sonderistic.Player.Input;
    using Sonderistic.UI;
    using UnityEngine;

    public class Interactor : MonoBehaviour
    {
        #region Variables
        private InteractableNPC currentNPCInSight;
        private RaycastHit hitInfo;
        private LayerMask layerMask;
        private EntityState playerStateBeforeInteraction;
        private bool isInteracting;
        #endregion

        #region Methods
        public void EndInteraction()
        {
            Player.Instance.SetPlayerState(playerStateBeforeInteraction);
            isInteracting = false;
        }

        public void SetInteractorState(EntityState state)
        {
            Player.Instance.SetPlayerState(state);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1.5f);
        }

        private void HandlePlayerInteractionPerformed()
        {
            if (currentNPCInSight != null &&
                currentNPCInSight.CanInteract() == true &&
                isInteracting == false)
            {
                playerStateBeforeInteraction = Player.Instance.GetPlayerState(); 
                EntityState? playerStateToBeSet = currentNPCInSight.Interact(this);

                if (playerStateToBeSet != null)
                {
                    Player.Instance.SetPlayerState((EntityState)playerStateToBeSet);
                }

                isInteracting = true;
                OverlayDisplay.Instance.ShowQuickInfo(false);
            }
        }
        #endregion

        #region Unity Callbacks
        private void Update()
        {
            if (isInteracting == false)
            {
                if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 1.5f, layerMask) == true)
                {
                    OverlayDisplay.Instance.SetQuickInfo("Press 'E' to interact");
                    currentNPCInSight = hitInfo.collider.GetComponent<InteractableNPC>();
                }
                else
                {
                    OverlayDisplay.Instance.ShowQuickInfo(false);
                    currentNPCInSight = null;
                }
            }
        }

        private void Awake()
        {
            // mask everything but this layer.
            layerMask = 1 << LayerMask.NameToLayer("Interactable");
        }

        private void OnEnable()
        {
            PlayerInputActionService.onPlayerInteractionPerformed += HandlePlayerInteractionPerformed;
        }

        private void OnDisable()
        {
            PlayerInputActionService.onPlayerInteractionPerformed -= HandlePlayerInteractionPerformed;
        }
        #endregion
    }
}