namespace GameLogic.Enemies
{
    public class EnemyModel
    {
        private int _maxHealth;
        private float _speed;

        public int MaxHealth => _maxHealth;
        public float Speed => _speed;

        public EnemyModel(int maxHealth, float speed)
        {
            _maxHealth = maxHealth;
            _speed = speed;
        }
    }
}