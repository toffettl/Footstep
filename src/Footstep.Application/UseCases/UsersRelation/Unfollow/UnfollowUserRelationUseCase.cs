using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.RelationUser;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.UserRelation.Unfollow
{
    public class UnfollowUserRelationUseCase : IUnfollowUserRelationUseCase
    {
        private readonly IUserRelationWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UnfollowUserRelationUseCase(IUserRelationWriteOnlyRepository repostiory,
            IUnitOfWork unitOfWork)
        {
            _repository = repostiory;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid followerId, Guid followingId)
        {
            var result = await _repository.DeleteRelation(followerId, followingId);
            if (result == false)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_RELATION_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
