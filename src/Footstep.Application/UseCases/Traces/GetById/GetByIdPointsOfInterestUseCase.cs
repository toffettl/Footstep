using AutoMapper;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Domain.Repositories.UserPointOfInterestRelations;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.GetById
{
    public class GetByIdPointsOfInterestUseCase : IGetByIdPointOfInterestUseCase
    {
        private readonly IPointOfInterestReadOnlyRepository _pointOfInterestReadOnlyRepository;
        private readonly IPointOfInterestUpdateOnlyRepository _pointOfInterestUpdateOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserPointOfInterestRelationWriteOnlyRepository _userPointOfInterestRelationWriteOnlyRepository;
        private readonly IUserPointOfInterestRelationReadOnlyRepository _userPointOfInterestRelationReadOnlyRepository;
        private readonly IUserPointOfInterestRelationUpdateOnlyRepository _userPointOfInterestRelationUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdPointsOfInterestUseCase(
            IPointOfInterestReadOnlyRepository pointOfInterestReadOnlyRepository,
            IPointOfInterestUpdateOnlyRepository pointOfInterestUpdateOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUserPointOfInterestRelationWriteOnlyRepository userPointOfInterestRelationWriteOnlyRepository,
            IUserPointOfInterestRelationReadOnlyRepository userPointOfInterestRelationReadOnlyRepository,
            IUserPointOfInterestRelationUpdateOnlyRepository userPointOfInterestRelationUpdateOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _pointOfInterestReadOnlyRepository = pointOfInterestReadOnlyRepository;
            _pointOfInterestUpdateOnlyRepository = pointOfInterestUpdateOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _userPointOfInterestRelationWriteOnlyRepository = userPointOfInterestRelationWriteOnlyRepository;
            _userPointOfInterestRelationReadOnlyRepository = userPointOfInterestRelationReadOnlyRepository;
            _userPointOfInterestRelationUpdateOnlyRepository = userPointOfInterestRelationUpdateOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePointOfInterestJson> Execute(Guid id, Guid userId)
        {
            var pointOfInterest = await _pointOfInterestReadOnlyRepository.GetById(id);

            if (pointOfInterest == null)
            {
                throw new NotFoundException(ResourceErrorMessages.POINT_OF_INTEREST_NOT_FOUND);
            }

            await UpdateUserPointOfInterestRelation(id, userId);

            pointOfInterest.Views++;

            _pointOfInterestUpdateOnlyRepository.Update(pointOfInterest);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponsePointOfInterestJson>(pointOfInterest);
        }

        private async Task UpdateUserPointOfInterestRelation(Guid pointOfInterestId, Guid userId)
        {
            var userPointOfInterestRelation = await _userPointOfInterestRelationReadOnlyRepository.GetByUserIdAndPointOfInterestId(pointOfInterestId, userId);

            if (userPointOfInterestRelation == null)
            {
                if (!await _userReadOnlyRepository.ExistActiveUserWithId(userId))
                {
                    throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
                }

                userPointOfInterestRelation = new UserPointOfInterestRelation
                {
                    UserId = userId,
                    PointOfInterestId = pointOfInterestId
                };

                await _userPointOfInterestRelationWriteOnlyRepository.Add(userPointOfInterestRelation);
            } 
            else
            {
                userPointOfInterestRelation.UpdatedAt = DateTime.UtcNow;

                _userPointOfInterestRelationUpdateOnlyRepository.Update(userPointOfInterestRelation);
            }
        }
    }
}
