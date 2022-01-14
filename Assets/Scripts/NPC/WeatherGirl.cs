namespace Sonderistic.GameInteraction
{
    using Sonderistic.Data.Geocode;
    using Sonderistic.Data.Weather;
    using Sonderistic.GameInteraction.Dialogue;
    using Sonderistic.UI;
    using UnityEngine;

    public class WeatherGirl : InteractableNPC
    {
        #region Variables
        // TODO: abstract out animator functinonality
        [SerializeField]
        private Animator weatherGirlAnimator;
        #endregion

        #region InteractableNPC Methods
        public override void EndInteraction()
        {
            base.EndInteraction();
            weatherGirlAnimator.SetTrigger("Goodbye");
        }

        protected override void InteractInternal(Interactor otherInteractable)
        {
            weatherGirlAnimator.SetTrigger("Talk");
        }


        protected override async void HandleInputFieldSubmit(string input)
        {
            SetCursorVisibility(false);
            currentInteractor.SetInteractorState(EntityState.MovementFrozen);
            DialogueSlideDisplay.Instance.DisplayDialogueSlide("Please wait while I check...");
            DialogueSlideDisplay.Instance.ReceivePlayerInput(false);

            LocationInformation locInfo = await GeocodeDataService.GetLocationInformation(input);
            WeatherData weatherData = null;

            if (locInfo != null)
            {
                weatherData = await LocationWeatherDataService.GetCurrentWeatherData(locInfo);
            }

            DialogueSlideDisplay.Instance.EndDisplay();

            if (weatherData != null)
            {
                DialogueSlideArgs currentSlideArgs = dialogueSequence.GetDialogueSlideArgsAtIndex(currentDialogueSlideIndex);
                string displayText = string.Format(currentSlideArgs.responseToInputStringTemplate, input);
                displayText += $" {weatherData.detailedForecast}";
                DialogueSlideDisplay.Instance.DisplayDialogueSlide(displayText, HandleDialogueSlideComplete);
            }
            else
            {
                DialogueSlideDisplay.Instance.DisplayDialogueSlide("I wasn't able to find that location, sorry!", HandleDialogueSlideComplete);
            }
        }
        #endregion
    }
}