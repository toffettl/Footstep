using Footstep.Communication.Enums;

namespace Footstep.Communication.Requests.Comments
{
    public class RequestCommentJson
    {
        public Guid AuthorId { get; set; }
        public Guid ParentId { get; set; }

        public ParentType ParentType { get; set; }
        public string? Content { get; set; }
    }
}
