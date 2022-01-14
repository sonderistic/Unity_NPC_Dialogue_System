namespace Sonderistic.Player.Animation
{
    using UnityEngine;

    public class TalkingAnimationRandomizer : StateMachineBehaviour
    {
        #region Constants
        private const string TALKING_ANIM_ID_PARAM_NAME = "TalkingAnimId";
        #endregion

        #region StateMachineBehaviour Callbacks
        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            animator.SetInteger(TALKING_ANIM_ID_PARAM_NAME, Random.Range(0, 2));
        }
        #endregion
    }
}