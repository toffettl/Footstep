using AutoMapper;
using FluentValidation.Results;
using Footstep.Communication.Requests.UserRelation;
using Footstep.Communication.Responses.Traces;
using Footstep.Communication.Responses.UserRelation;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.RelationUser;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.RelationUser.Follow;
public class FollowUserRelationUseCase : IFollowUserRelationUseCase
{
    private readonly IUserRelationWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserRelationReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public FollowUserRelationUseCase(
        IUserRelationWriteOnlyRepository writeOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRelationReadOnlyRepository readOnlyRepository)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<ResponseUserRelationJson> Execute(RequestUserRelationJson request)
    {
        await Validate(request);

        var relation = _mapper.Map<UserRelation>(request);

        await _writeOnlyRepository.AddRelation(relation);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseUserRelationJson>(relation);
    }

    private async Task Validate(RequestUserRelationJson request)
    {
        var result = new UserRelationValidator().Validate(request);

        var isFollowing = await _readOnlyRepository.IsFollowingAsync(request.FollowerId, request.FollowingId);
        if (isFollowing)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.IS_FOLLOWING));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
