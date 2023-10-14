using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameLogic.Player;
using Infrastructure.Services;
using ObjectsPool;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class PlayerFactory
    {
        private const string BulletAddress = "Bullet";
        private const string PlayerUnitAddress = "PlayerUnit";
        private const int MinCount = 5;
        private const int MaxCount = 20;

        private Pool<Bullet> _bulletsPool;
        private AssetProvider _assetProvider;
        private DiContainer _diContainer;

        public PlayerFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
        }

        public async UniTask Initialize(Transform bulletsContainer)
        {
            _bulletsPool = new Pool<Bullet>(_diContainer, (await _assetProvider.Load<GameObject>(BulletAddress)).GetComponent<Bullet>(), bulletsContainer, MinCount, MaxCount, true);
            _bulletsPool.Initialize();
        }

        public Bullet GetBullet(Vector3 position) =>
            _bulletsPool.GetFreeElement(position);

        public async UniTask<PlayerUnit> GetPlayerUnit(PlayerModel playerModel, Vector3 position)
        {
            PlayerUnit playerUnit =
                _diContainer.InstantiatePrefabForComponent<PlayerUnit>(await _assetProvider.Load<GameObject>(PlayerUnitAddress), position, Quaternion.identity, null);
            playerUnit.Initialize(playerModel);

            return playerUnit;
        }
    }
}