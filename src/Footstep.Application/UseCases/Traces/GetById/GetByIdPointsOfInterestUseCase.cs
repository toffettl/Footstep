using AutoMapper;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Domain.Repositories.UserPointOfInterestRelations;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using System.Threading.Tasks;

namespace Footstep.Application.UseCases.Traces.GetById
{
    public class GetByIdPointsOfInterestUseCase : IGetByIdPointOfInterestUseCase
    {
        private readonly IPointOfInterestReadOnlyRepository _pointOfInterestReadOnlyRepository;
        private readonly IPointOfInterestUpdateOnlyRepository _pointOfInterestUpdateOnlyRepository; 
        private readonly IUserPointOfInterestRelationReadOnlyRepository _userPointOfInterestRelationReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdPointsOfInterestUseCase(
            IPointOfInterestReadOnlyRepository pointOfInterestReadOnlyRepository,
            IPointOfInterestUpdateOnlyRepository pointOfInterestUpdateOnlyRepository,
            IUserPointOfInterestRelationReadOnlyRepository userPointOfInterestRelationReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _pointOfInterestReadOnlyRepository = pointOfInterestReadOnlyRepository;
            _pointOfInterestUpdateOnlyRepository = pointOfInterestUpdateOnlyRepository;
            _userPointOfInterestRelationReadOnlyRepository = userPointOfInterestRelationReadOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePointOfInterestJson> Execute(Guid id, Guid userId)
        {
            var pointOfInterest = await _pointOfInterestReadOnlyRepository.GetById(id);

            if (pointOfInterest == null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRACE_NOT_FOUND);
            }

            var userPointOfInterestRelation = await GetUserPointOfInterestRelation(id, userId);

            pointOfInterest.Views++;
            pointOfInterest.UserPointOfInterestRelations.Add(userPointOfInterestRelation);

            _pointOfInterestUpdateOnlyRepository.Update(pointOfInterest);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponsePointOfInterestJson>(pointOfInterest);
        }

        private async Task<UserPointOfInterestRelation> GetUserPointOfInterestRelation(Guid pointOfInterestId, Guid userId)
        {
            var userPointOfInterestRelation = await _userPointOfInterestRelationReadOnlyRepository.GetByUserIdAndPointOfInterestId(userId, pointOfInterestId);

            if (userPointOfInterestRelation == null)
            {
                userPointOfInterestRelation = new UserPointOfInterestRelation
                {
                    UserId = userId,
                };
            }

            userPointOfInterestRelation.UpdatedAt = DateTime.UtcNow;

            return userPointOfInterestRelation;
        }
    }
}
