namespace Footstep.Domain.Entities
{
    public class Coin
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Total { get; set; } = 0;
        public int Earned { get; set; } = 0;
        public int Spent { get; set; }  = 0;

        public Guid UserId { get; set; }

        public User? User { get; set; }
    }
}
