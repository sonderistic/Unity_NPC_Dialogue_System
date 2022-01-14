namespace Sonderistic.GameInteraction
{
    using Sonderistic.GameInteraction.Dialogue;
    using Sonderistic.UI;
    using UnityEngine;

    public abstract class InteractableNPC : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        protected DialogueSequence dialogueSequence;
        private EntityState interactorStateWhenInteracting = EntityState.MovementFrozen; 

        protected int currentDialogueSlideIndex = -1;
        protected bool isInteracting;
        protected Interactor currentInteractor;
        #endregion

        #region Methods
        public virtual bool CanInteract()
        {
            return (isInteracting == false);
        }

        public virtual void EndInteraction()
        {
            isInteracting = false;
            currentInteractor?.EndInteraction();
        }

        public EntityState? Interact(Interactor interactor)
        {
            EntityState? interactorStateToBeSet = null;
            if (CanInteract() == true)
            {
                currentInteractor = interactor;
                interactorStateToBeSet = interactorStateWhenInteracting;
                isInteracting = true;
                InteractInternal(interactor);
                StartDialogueSequence();
                SetCursorVisibility(false);
            }
            return interactorStateToBeSet;
        }

        protected abstract void InteractInternal(Interactor otherInteractable);

        private void StartDialogueSequence()
        {
            currentDialogueSlideIndex = 0;
            DisplayCurrentDialogueSlide();
        }

        private void EndDialogueSequence()
        {
            EndInteraction();
            DialogueSlideDisplay.Instance.EndDisplay();
            SetCursorVisibility(false);
        }

        private void DisplayCurrentDialogueSlide()
        {
            if (currentDialogueSlideIndex < dialogueSequence.dialogueSlideCount)
            {
                DialogueSlideArgs currentSlideArgs = dialogueSequence.GetDialogueSlideArgsAtIndex(currentDialogueSlideIndex);

                if (currentSlideArgs.takesInput == true)
                {
                    if (string.IsNullOrEmpty(currentSlideArgs.dialogueSlideText) == false)
                    {
                        DialogueSlideDisplay.Instance.DisplayDialogueSlide(currentSlideArgs);
                    }
                    DialogueSlideDisplay.Instance.ReceivePlayerInput(false);
                    InputFieldDisplay.Instance.DisplayInputField(currentSlideArgs.inputFieldPromptText, null, HandleInputFieldSubmit);
                    currentInteractor.SetInteractorState(EntityState.MovementAndLookFrozen);
                    SetCursorVisibility(true);
                }
                else
                {
                    DialogueSlideDisplay.Instance.DisplayDialogueSlide(currentSlideArgs, HandleDialogueSlideComplete);
                    currentInteractor.SetInteractorState(EntityState.MovementFrozen);
                    SetCursorVisibility(false);
                }
            }
            else
            {
                EndDialogueSequence();
            }
        }

        protected void SetCursorVisibility(bool isVisible)
        {
            Cursor.visible = isVisible;
        }
        #endregion

        #region Callbacks
        protected virtual void HandleInputFieldSubmit(string input)
        {
            DialogueSlideArgs currentSlideArgs = dialogueSequence.GetDialogueSlideArgsAtIndex(currentDialogueSlideIndex);
            string displayText = string.Format(currentSlideArgs.responseToInputStringTemplate, input);
            DialogueSlideDisplay.Instance.DisplayDialogueSlide(displayText, HandleDialogueSlideComplete);
        }

        protected void HandleDialogueSlideComplete()
        {
            if (isInteracting == true)
            {
                currentDialogueSlideIndex++;
                DisplayCurrentDialogueSlide();
            }
        }
        #endregion
    }
}