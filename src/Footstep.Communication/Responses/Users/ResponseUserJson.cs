using Footstep.Communication.Responses.Traces;

namespace Footstep.Communication.Responses.Users;
public class ResponseUserJson
{
    public string? Name { get; set; }
    public string? Token { get; set; }

    public List<ResponseUserJson> Users { get; set; } = [];
}
