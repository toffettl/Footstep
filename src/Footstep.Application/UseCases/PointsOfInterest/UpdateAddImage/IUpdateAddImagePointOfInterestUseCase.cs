namespace Footstep.Application.UseCases.PointsOfInterest.UpdateImages
{
    public interface IUpdateAddImagePointOfInterestUseCase
    {
        Task Execute(Guid Id, Stream stream, string fileName, string contentType);
    }
}
