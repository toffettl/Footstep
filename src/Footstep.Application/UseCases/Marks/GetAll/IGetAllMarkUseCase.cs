using Footstep.Communication.Responses.Marks;

namespace Footstep.Application.UseCases.Marks.GetAll
{
    public interface IGetAllMarkUseCase
    {
        Task<ResponseMarksJson> Execute();
    }
}
