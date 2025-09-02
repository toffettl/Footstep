namespace Footstep.Communication.Requests.Users
{
    public class RequestUpdateUnlockedStylesUserJson
    {
        public string? UnlockedMapStyles { get; set; }
        public Guid? UnlockedPointOfInterestStyle { get; set; }
        public Guid? UnlockedHeadStyle { get; set; }
        public Guid? UnlockedBodyStyle { get; set; }
        public Guid? UnlockedLegStyle { get; set; }
        public Guid? UnlockedBagStyle { get; set; }
        public Guid? UnlockedAcessoryStyle { get; set; }
    }
}
