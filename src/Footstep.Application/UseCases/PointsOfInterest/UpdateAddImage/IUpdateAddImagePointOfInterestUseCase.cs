namespace Footstep.Application.UseCases.PointsOfInterest.UpdateImages
{
    public interface IUpdateAddImagePointOfInterestUseCase
    {
        Task Execute(Guid pointOfInterestId, Stream stream, string fileName, string contentType);
    }
}
