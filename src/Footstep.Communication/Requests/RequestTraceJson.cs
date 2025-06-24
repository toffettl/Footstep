namespace Footstep.Communication.Requests
{
    public class RequestTraceJson
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ExpireAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
