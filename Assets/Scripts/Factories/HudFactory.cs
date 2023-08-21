using HUD;
using UnityEngine;
using EventProvider = EventProvider.EventProvider;

namespace Factories
{
    public class HudFactory
    {
        private Transform _hudTransform;

        private const string RoundHeadsUpDisplayPath = "Prefabs/HUD/RoundHeadsUpDisplay";
        private const string WinHeadsUpDisplayPath = "Prefabs/HUD/WinHeadsUpDisplay";
        private const string DefeatHeadsUpDisplayPath = "Prefabs/HUD/DefeatHeadsUpDisplay";
        
        public HudFactory(Transform hudTransform) => 
            _hudTransform = hudTransform;

        public EndGameHeadsUpDisplay GetWinHeadsUpDisplay() => 
            Object.Instantiate(Resources.Load<EndGameHeadsUpDisplay>(WinHeadsUpDisplayPath), _hudTransform);

        public EndGameHeadsUpDisplay GetDefeatHeadsUpDisplay() => 
            Object.Instantiate(Resources.Load<EndGameHeadsUpDisplay>(DefeatHeadsUpDisplayPath), _hudTransform);

        public RoundHeadsUpDisplay GetRoundHeadsUpDisplay(global::EventProvider.EventProvider eventProvider, int health)
        {
            RoundHeadsUpDisplay roundHeadsUpDisplay = Object.Instantiate(Resources.Load<RoundHeadsUpDisplay>(RoundHeadsUpDisplayPath), _hudTransform);
            roundHeadsUpDisplay.Initialize(eventProvider, health);
            
            return roundHeadsUpDisplay;
        }
    }
}