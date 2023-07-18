namespace EventProvider.Events
{
    public struct EnemyReachedFinishLineEvent : IEvent
    {
        public readonly int Health;

        public EnemyReachedFinishLineEvent(int health) => 
            this.Health = health;
    }
}