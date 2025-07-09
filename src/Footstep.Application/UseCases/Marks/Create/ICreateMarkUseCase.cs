using Footstep.Communication.Requests.Marks;
using Footstep.Communication.Responses.Marks;

namespace Footstep.Application.UseCases.Marks.Create
{
    public interface ICreateMarkUseCase
    {
        Task<ResponseMarkJson> Execute(RequestMarkJson request);
    }
}
