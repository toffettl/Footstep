using Footstep.Communication.Enums;

namespace Footstep.Communication.Responses.Styles
{
    public class ResponseStyleJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public StyleType StyleType { get; set; }
    }
}
