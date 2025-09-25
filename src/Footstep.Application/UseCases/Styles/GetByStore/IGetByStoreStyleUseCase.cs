using Footstep.Communication.Responses.Styles;

namespace Footstep.Application.UseCases.Styles.GetByStore
{
    public interface IGetByStoreStyleUseCase
    {
        Task<List<ResponseStyleJson>> Execute();
    }
}
