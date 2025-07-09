using Footstep.Communication.Requests.Marks;

namespace Footstep.Application.UseCases.Marks.Update
{
    public interface IUpdateMarkUseCase
    {
        Task Execute(Guid id, RequestMarkJson request);
    }
}
