namespace Sonderistic.Player
{
    using Sonderistic.GameInteraction;
    using System;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        #region Variables
        public static Player _player;
        public static Action onPlayerStateUpdated = null;

        [SerializeField]
        private EntityState playerState = EntityState.Free;
        #endregion

        #region Properties
        public static Player Instance
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.FindObjectOfType<Player>();
                }

                return _player;
            }
        }
        #endregion

        #region Methods
        public void SetPlayerState(EntityState playerState)
        {
            this.playerState = playerState;
            onPlayerStateUpdated?.Invoke();
        }

        public EntityState GetPlayerState()
        {
            return playerState;
        }
        #endregion
    }
}
