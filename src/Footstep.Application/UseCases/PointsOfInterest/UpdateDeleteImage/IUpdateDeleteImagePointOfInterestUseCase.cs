namespace Footstep.Application.UseCases.PointsOfInterest.UpdateDeleteImage
{
    public interface IUpdateDeleteImagePointOfInterestUseCase
    {
        Task Execute(Guid imageId);
    }
}
