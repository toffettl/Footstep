namespace Footstep.Communication.Requests.Comments
{
    public class RequestUpdateStatusCommentsJson
    {
        public RequestUpdateStatusLikeCommentsJson Likes { get; set; }
        public RequestUpdateStatusReplieCommentsJson Replies { get; set; }
    }

    public class RequestUpdateStatusLikeCommentsJson
    {
        public int Likes { get; set; }
    }

    public class RequestUpdateStatusReplieCommentsJson
    {
        public int Replies { get; set; }
    }
}
