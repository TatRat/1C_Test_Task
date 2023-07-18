using EventProvider.Events;
using TMPro;
using UnityEngine;

namespace HUD
{
    public class RoundHeadsUpDisplay : MonoBehaviour, IHeadsUpDisplay
    {
        [SerializeField] private TMP_Text _healthText;
        
        private EventProvider.EventProvider _eventProvider;
        private const string HealthTextString = "Health: ";
        
        public void Initialize(EventProvider.EventProvider eventProvider, int startHealth)
        {
            _eventProvider = eventProvider;
            _healthText.text = $"{HealthTextString}{startHealth.ToString()}";
        }

        public void Enable() => 
            _eventProvider.Subscribe<EnemyReachedFinishLineEvent>(OnHealthChanged);

        public void Disable() => 
            _eventProvider.UnSubscribe<EnemyReachedFinishLineEvent>(OnHealthChanged);

        private void OnHealthChanged(EnemyReachedFinishLineEvent enemyReachedFinishLineEvent) => 
            _healthText.text = $"{HealthTextString}{enemyReachedFinishLineEvent.Health.ToString()}";
    }
}