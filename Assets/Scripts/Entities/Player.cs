namespace ashlight.james_strike_again.Entities
{
    public class Player : Entity
    {
        public override void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}
