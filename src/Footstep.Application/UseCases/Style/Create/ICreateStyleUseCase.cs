using Footstep.Communication.Requests.Styles;
using Footstep.Communication.Responses.Styles;

namespace Footstep.Application.UseCases.Style.Create
{
    public interface ICreateStyleUseCase
    {
        Task<ResponseStyleJson> Execute(RequestStyleJson request);
    }
}
