using GameLogic.Player;
using Input;
using ObjectsPool;
using UnityEngine;

namespace Factories
{
    public class PlayerFactory
    {
        private const string BulletPath = "Prefabs/Bullets/Bullet";
        private const string PlayerUnitPath = "Prefabs/Units/PlayerUnit";
        private const int MinCount = 5;
        private const int MaxCount = 20;

        private readonly Pool<Bullet> _bulletsPool;
        
        public PlayerFactory(Transform bulletsContainer)
        {
            _bulletsPool = new Pool<Bullet>(Resources.Load<Bullet>(BulletPath), bulletsContainer, MinCount, MaxCount, true);
            _bulletsPool.Initialize();
        }

        public Bullet GetBullet(Vector3 position) => 
            _bulletsPool.GetFreeElement(position);

        public PlayerUnit GetPlayerUnit(IInputService inputService, PlayerModel playerModel, PlayerFactory playerFactory, Vector3 position)
        {
            PlayerUnit playerUnit = Object.Instantiate(Resources.Load<PlayerUnit>(PlayerUnitPath), position, Quaternion.identity);
            playerUnit.Initialize(inputService, playerModel, playerFactory);
            
            return playerUnit;
        }
    }
}