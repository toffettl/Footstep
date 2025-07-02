using Footstep.Domain.Entities;

namespace Footstep.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    string Generate(User user);
}
