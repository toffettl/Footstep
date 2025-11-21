namespace Footstep.Communication.Responses.Shop
{
    public class ResponseUserCoinsJson
    {
        public Guid UserId { get; set; }
        public int Total { get; set; }
        public int Earned { get; set; }
        public int Spent { get; set; }

    }
}
