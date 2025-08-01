using AutoMapper;
using Footstep.Communication.Requests.Comments;
using Footstep.Communication.Requests.Traces;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.UpdateStatus
{
    public class UpdateStatusPointOfInterestUseCase : IUpdateStatusPointOfInterestUseCase
    {
        private readonly IPointsOfInterestUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStatusPointOfInterestUseCase(IPointsOfInterestUpdateOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(Guid id, RequestUpdateStatusPointOfInterestJson request)
        {
            var pointOfInterest = await _repository.GetById(id);

            if (pointOfInterest == null)
            {
                throw new NotFoundException(ResourceErrorMessages.POINT_OF_INTEREST_NOT_FOUND);
            }

            _mapper.Map(request, pointOfInterest);

            pointOfInterest.UpdatedAt = DateTime.UtcNow;
            _repository.Update(pointOfInterest);

            await _unitOfWork.Commit();
        }

        public async Task Execute(Guid id, RequestUpdateStatusPointOfInterestJson.RequestUpdateLikesPointOfInterestJson requestLike)
        {
           var pointOfInterest = await _repository.GetById(id);

            if (pointOfInterest == null)
            {
                throw new NotFoundException(ResourceErrorMessages.POINT_OF_INTEREST_NOT_FOUND);
            }

            _mapper.Map(requestLike, pointOfInterest);

            pointOfInterest.UpdatedAt = DateTime.UtcNow;
            _repository.Update(pointOfInterest);

            await _unitOfWork.Commit();
        }

        public async Task Execute(Guid id, RequestUpdateStatusPointOfInterestJson.RequestUpdateViewsPointOfInterestJson requestView)
        {
            var pointOfInterest = await _repository.GetById(id);

            if (pointOfInterest == null)
            {
                throw new NotFoundException(ResourceErrorMessages.POINT_OF_INTEREST_NOT_FOUND);
            }

            _mapper.Map(requestView, pointOfInterest);

            pointOfInterest.UpdatedAt= DateTime.UtcNow;
            _repository.Update(pointOfInterest);

            await _unitOfWork.Commit();
                
        }
    }
}
