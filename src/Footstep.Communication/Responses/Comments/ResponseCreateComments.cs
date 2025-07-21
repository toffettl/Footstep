using Footstep.Communication.Enums;
using System.ComponentModel.DataAnnotations;

namespace Footstep.Communication.Responses.Comments
{
    public class ResponseCreateComments
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }

        public Guid ParentId { get; set; }
        public ParentType ParentType { get; set; }
        public string? Content { get; set; }

        public StatusResponse? Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }

    public class StatusResponse
    {
        public int Likes { get; set; }
        public int Replies { get; set; }
    }
}
