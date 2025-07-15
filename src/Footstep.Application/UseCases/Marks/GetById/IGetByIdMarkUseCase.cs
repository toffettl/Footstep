using Footstep.Communication.Responses.Marks;

namespace Footstep.Application.UseCases.Marks.GetById
{
    public interface IGetByIdMarkUseCase
    {
        Task<ResponseMarkJson> Execute(Guid id);
    }
}
