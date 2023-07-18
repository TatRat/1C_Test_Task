using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameSettingsConfig", menuName = "ScriptableObjects/Configs/GameSettingsConfig")]
    public class GameSettingsConfig : ScriptableObject
    {
        [Header("Game areas settings")]
        [SerializeField] [Range(0, 1)] private float finishLineVerticalPositionOnScreenPercent;
        [SerializeField] [Range(0, 1)] private float enemySpawnersVerticalPositionOnScreenPercent;

        public float FinishLineVerticalPositionOnScreenPercent => finishLineVerticalPositionOnScreenPercent;
        public float EnemySpawnersVerticalPositionOnScreenPercent => enemySpawnersVerticalPositionOnScreenPercent;
    }
}