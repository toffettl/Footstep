namespace Footstep.Domain.Entities
{
    public class Coin
    {
        public Guid Id { get; set; }

        public int Total { get; set; }
        public int Earned { get; set; }
        public int Spent { get; set; }  

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
