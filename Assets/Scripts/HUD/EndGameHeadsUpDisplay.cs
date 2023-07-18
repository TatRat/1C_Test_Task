using EventProvider.Events;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class EndGameHeadsUpDisplay : MonoBehaviour, IHeadsUpDisplay
    {
        [SerializeField] private Button restartButton;
        
        private EventProvider.EventProvider _eventProvider;

        public void Initialize(EventProvider.EventProvider eventProvider) => 
            _eventProvider = eventProvider;

        public void Enable() => 
            restartButton.onClick.AddListener(OnRestartButtonPressed);

        public void Disable() => 
            restartButton.onClick.RemoveListener(OnRestartButtonPressed);

        private void OnRestartButtonPressed() => 
            _eventProvider.Invoke(new RestartButtonPressedEvent());
    }
}