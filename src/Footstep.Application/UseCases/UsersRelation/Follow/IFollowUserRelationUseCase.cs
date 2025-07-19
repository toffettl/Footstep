using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Requests.UserRelation;
using Footstep.Communication.Responses.Traces;
using Footstep.Communication.Responses.UserRelation;

namespace Footstep.Application.UseCases.RelationUser.Follow;
public interface IFollowUserRelationUseCase
{
    Task<ResponseUserRelationJson> Execute(RequestUserRelationJson request);
}
