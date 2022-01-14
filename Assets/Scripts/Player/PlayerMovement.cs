namespace Sonderistic.Player.Movement
{
    using Sonderistic.GameInteraction;
    using Sonderistic.Player.Input;
    using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {
        #region Constants
        private const string GROUND_LAYER_MASK_NAME = "Ground";
        #endregion

        #region Variables
        [SerializeField]
        private CharacterController characterController;
        [SerializeField]
        private float playerWalkSpeed = 5f;
        [SerializeField]
        private float playerSprintSpeed = 10f;
        [SerializeField]
        private float gravityAccel = -200f;
        [SerializeField]
        private Transform groundCheck;

        private Vector2 playerHorizontalInputAxisRaw;
        private bool isPlayerSprinting;
        private int groundLayerMask;
        private EntityState playerCurrentState;
        #endregion

        #region Callbacks
        private void HandlePlayerHorizontalMovementPerformed(Vector2 horizontalInputAxisRaw)
        {
            playerHorizontalInputAxisRaw = horizontalInputAxisRaw;
        }

        private void HandlePlayerSprintPerformed(bool isSprint)
        {
            isPlayerSprinting = isSprint;
        }
        #endregion

        #region Methods
        private void SetPlayerMovement()
        {
            float movementSpeed = (isPlayerSprinting == true) ? playerSprintSpeed : playerWalkSpeed;
            Vector3 horizontalVelocity = (playerHorizontalInputAxisRaw.x * transform.right + playerHorizontalInputAxisRaw.y * transform.forward) * movementSpeed;
            characterController.Move(horizontalVelocity * Time.deltaTime);

            bool playerGrounded = Physics.Raycast(groundCheck.position, -transform.up, 0.1f, groundLayerMask);
            Vector3 verticalVelocity = new Vector3(0, -2f, 0);

            if (playerGrounded == false)
            {
                verticalVelocity.y += gravityAccel * Time.deltaTime;
            }

            characterController.Move(verticalVelocity * Time.deltaTime);
        }
        #endregion

        #region MonoBehaviour Callbacks
        private void Update()
        {
            playerCurrentState = Player.Instance.GetPlayerState();
            if (playerCurrentState == EntityState.Free ||
                playerCurrentState == EntityState.LookFrozen)
            {
                SetPlayerMovement();
            }
        }

        private void Awake()
        {
            groundLayerMask = ~LayerMask.NameToLayer(GROUND_LAYER_MASK_NAME);
        }

        private void OnEnable()
        {
            PlayerInputActionService.onPlayerHorizontalMovementPerformed += HandlePlayerHorizontalMovementPerformed;
            PlayerInputActionService.onPlayerSprintPerformed += HandlePlayerSprintPerformed;
        }

        private void OnDisable()
        {
            PlayerInputActionService.onPlayerHorizontalMovementPerformed -= HandlePlayerHorizontalMovementPerformed;
            PlayerInputActionService.onPlayerSprintPerformed -= HandlePlayerSprintPerformed;
        }
        #endregion
    }
}