using AutoMapper;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.CommentLikes;
using Footstep.Exception;

namespace Footstep.Application.UseCases.Comments.Update
{
    public class UpdateCommentLikeCommentUseCase : IUpdateCommentLikeCommentUseCase
    {
        private readonly ICommentLikeWriteOnlyRepository _commentLikeWriteOnlyRepository;
        private readonly ICommentLikeReadOnlyRepository _commentLikeReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCommentLikeCommentUseCase(
            ICommentLikeWriteOnlyRepository commentLikeWriteOnlyRepository,
            ICommentLikeReadOnlyRepository commentLikeReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _commentLikeWriteOnlyRepository = commentLikeWriteOnlyRepository;
            _commentLikeReadOnlyRepository = commentLikeReadOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(Guid id, Guid userId)
        {
            if (await _commentLikeReadOnlyRepository.ExistCommentWithUserIdAndCommentId(userId, id) != false)
            { 
                throw new ArgumentException(ResourceErrorMessages.COMMENTLIKE_ALREADY_EXISTS);
            }

            CommentLike commentLike = new CommentLike
            {
                UserId = userId,
                CommentId = id
            };

            await _commentLikeWriteOnlyRepository.Add(commentLike);

            await _unitOfWork.Commit();
        }
    }
}