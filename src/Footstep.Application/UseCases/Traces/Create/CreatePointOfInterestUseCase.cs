using AutoMapper;
using FluentValidation.Results;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Addresses;
using Footstep.Domain.Repositories.Traces;
using Footstep.Domain.Repositories.Users;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;

namespace Footstep.Application.UseCases.Traces.Create
{
    public class CreatePointOfInterestUseCase : ICreatePointOfInterestUseCase
    {
        private readonly IPointOfInterestWriteOnlyRepository _pointOfInterestWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IAddressWriteOnlyRepository _addressWriteOnlyRepository;
        private readonly IAddressReadOnlyRepository _addressReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePointOfInterestUseCase(
            IPointOfInterestWriteOnlyRepository pointOfInterestWriteRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IAddressWriteOnlyRepository addressWriteOnlyRepository,
            IAddressReadOnlyRepository addressReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _pointOfInterestWriteOnlyRepository = pointOfInterestWriteRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _addressWriteOnlyRepository = addressWriteOnlyRepository;
            _addressReadOnlyRepository = addressReadOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePointOfInterestJson> Execute(RequestPointOfInterestJson request)
        {
            await Validade(request);

            var pointOfInterest = _mapper.Map<PointOfInterest>(request);

            pointOfInterest.AddressId = await GetAddressId(request);

            await _pointOfInterestWriteOnlyRepository.Add(pointOfInterest);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponsePointOfInterestJson>(pointOfInterest);
        }

        private async Task Validade(RequestPointOfInterestJson request)
        {
            var validator = new RequestPointOfInterestJsonValidator();

            var result = validator.Validate(request);
            var existsId = await _userReadOnlyRepository.ExistActiveUserWithId(request.AuthorId);

            if (!existsId)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.USER_NOT_FOUND));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }

        private async Task<Guid> GetAddressId(RequestPointOfInterestJson request)
        {
            var address = await _addressReadOnlyRepository.GetByLatitudeAndLongitude(request.Coordinates!.Latitude, request.Coordinates!.Longitude);

            if (address == null)
            {
                address = new Address
                {
                    Latitude = request.Coordinates!.Latitude,
                    Longitude = request.Coordinates!.Longitude,
                    Cep = request.Address?.Cep,
                    City = request.Address?.City,
                    Country = request.Address?.Country,
                    District = request.Address?.District,
                    Number = request.Address!.Number,
                    State = request.Address?.State,
                    Street = request.Address?.Street
                };

                await _addressWriteOnlyRepository.Add(address);
            }

            return address.Id;
        }
    }
}
