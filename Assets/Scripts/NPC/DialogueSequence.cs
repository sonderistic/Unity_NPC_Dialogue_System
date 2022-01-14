namespace Sonderistic.GameInteraction.Dialogue
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "DialogueSequencer", menuName = "DialogueSequencer")]
    public class DialogueSequence : ScriptableObject
    {
        #region Variables
        [SerializeField]
        private DialogueSlideArgs[] dialogues;
        #endregion

        #region Properties
        public int dialogueSlideCount
        {
            get
            {
                return dialogues.Length;
            }
        }
        #endregion

        #region Methods
        public DialogueSlideArgs GetDialogueSlideArgsAtIndex(int index)
        {
            DialogueSlideArgs args = null;

            if (index < dialogueSlideCount)
            {
                args = dialogues[index];
            }

            return args;
        }
        #endregion
    }
}