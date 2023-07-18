using HUD;
using UnityEngine;

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

        public RoundHeadsUpDisplay GetRoundHeadsUpDisplay() => 
            Object.Instantiate(Resources.Load<RoundHeadsUpDisplay>(RoundHeadsUpDisplayPath), _hudTransform);

        public EndGameHeadsUpDisplay GetWinHeadsUpDisplay() => 
            Object.Instantiate(Resources.Load<EndGameHeadsUpDisplay>(WinHeadsUpDisplayPath), _hudTransform);

        public EndGameHeadsUpDisplay GetDefeatHeadsUpDisplay() => 
            Object.Instantiate(Resources.Load<EndGameHeadsUpDisplay>(DefeatHeadsUpDisplayPath), _hudTransform);
    }
}