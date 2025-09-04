using Footstep.Communication.Responses.Styles;

namespace Footstep.Application.UseCases.Styles.GetByName
{
    public interface IGetByNameStyleUseCase
    {
        Task<ResponseStyleJson> Execute(string name);
    }
}
