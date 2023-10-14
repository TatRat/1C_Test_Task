using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using HUD;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class HudFactory
    {
        private Transform _hudTransform;
        private AssetProvider _assetProvider;
        private DiContainer _diContainer;

        private const string RoundHeadsUpDisplayAddress = "RoundHeadsUpDisplay";
        private const string WinHeadsUpDisplayAddress = "WinHeadsUpDisplay";
        private const string DefeatHeadsUpDisplayAddress = "DefeatHeadsUpDisplay";

        public HudFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }

        public void Initialize(Transform hudTransform) => 
            _hudTransform = hudTransform;

        public async UniTask<EndGameHeadsUpDisplay> GetWinHeadsUpDisplay() => 
            _diContainer.InstantiatePrefabForComponent<EndGameHeadsUpDisplay>(await _assetProvider.Load<GameObject>(WinHeadsUpDisplayAddress), _hudTransform);

        public async UniTask<EndGameHeadsUpDisplay> GetDefeatHeadsUpDisplay() => 
            _diContainer.InstantiatePrefabForComponent<EndGameHeadsUpDisplay>(await _assetProvider.Load<GameObject>(DefeatHeadsUpDisplayAddress), _hudTransform);

        public async UniTask<RoundHeadsUpDisplay> GetRoundHeadsUpDisplay(int health)
        {
            RoundHeadsUpDisplay roundHeadsUpDisplay = _diContainer.InstantiatePrefabForComponent<RoundHeadsUpDisplay>(await _assetProvider.Load<GameObject>(RoundHeadsUpDisplayAddress), _hudTransform);
            roundHeadsUpDisplay.Initialize(health);
            
            return roundHeadsUpDisplay;
        }
    }
}