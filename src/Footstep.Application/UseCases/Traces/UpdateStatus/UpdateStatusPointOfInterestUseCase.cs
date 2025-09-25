using AutoMapper;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.UserPointOfInterestRelations;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.UpdateStatus
{
    public class UpdateStatusPointOfInterestUseCase : 
        IUpdateStatusPointOfInterestUseCase
    {
        private readonly IUserPointOfInterestRelationReadOnlyRepository _userPointOfInterestRelationReadOnlyRepository;
        private readonly IUserPointOfInterestRelationUpdateOnlyRepository _userPointOfInterestRelationUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStatusPointOfInterestUseCase(
            IUserPointOfInterestRelationReadOnlyRepository userPointOfInterestRelationReadOnlyRepository,
            IUserPointOfInterestRelationUpdateOnlyRepository userPointOfInterestRelationUpdateOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userPointOfInterestRelationReadOnlyRepository = userPointOfInterestRelationReadOnlyRepository;
            _userPointOfInterestRelationUpdateOnlyRepository = userPointOfInterestRelationUpdateOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid id, Guid userId, bool like)
        {
            var userPointOfInterestRelation = await _userPointOfInterestRelationReadOnlyRepository.GetByUserIdAndPointOfInterestId(id, userId);

            if (userPointOfInterestRelation  == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_AND_POINT_OF_INTEREST_RELATION_NOT_FOUND);
            }

            userPointOfInterestRelation.Like = like;

            _userPointOfInterestRelationUpdateOnlyRepository.Update(userPointOfInterestRelation);

            await _unitOfWork.Commit();
        }
    }
}
