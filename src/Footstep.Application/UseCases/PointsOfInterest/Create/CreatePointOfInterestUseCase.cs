using AutoMapper;
using FluentValidation.Results;
using Footstep.Communication.Enums;
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
        private readonly IPointOfInterestReadOnlyRepository _pointOfInterestReadOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IAddressWriteOnlyRepository _addressWriteOnlyRepository;
        private readonly IAddressReadOnlyRepository _addressReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePointOfInterestUseCase(
            IPointOfInterestWriteOnlyRepository pointOfInterestWriteRepository,
            IPointOfInterestReadOnlyRepository pointOfInterestReadOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IAddressWriteOnlyRepository addressWriteOnlyRepository,
            IAddressReadOnlyRepository addressReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _pointOfInterestWriteOnlyRepository = pointOfInterestWriteRepository;
            _pointOfInterestReadOnlyRepository = pointOfInterestReadOnlyRepository;
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

            Address address = await GetAddressId(request);

            pointOfInterest.AddressId = address.Id;

            if (request.PointOfInterestType == PointOfInterestType.Mark)
            {
                pointOfInterest.ExpireAt = null;
            }

            await _pointOfInterestWriteOnlyRepository.Add(pointOfInterest);

            await _unitOfWork.Commit();

            pointOfInterest = await _pointOfInterestReadOnlyRepository.GetById(pointOfInterest.Id);   

            return _mapper.Map<ResponsePointOfInterestJson>(pointOfInterest);
        }

        private async Task Validade(RequestPointOfInterestJson request)
        {
            var validator = new RequestPointOfInterestJsonValidator();

            var result = validator.Validate(request);

            var existsId = await _userReadOnlyRepository.ExistActiveUserWithId(request.Author!.Id);

            if (!existsId)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.USER_NOT_FOUND));
            }

            if (request.PointOfInterestType == PointOfInterestType.Step && request.ExpireAt < DateTime.UtcNow)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.THE_EXPIRATION_DATE_CANNOT_BE_IN_THE_PAST));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }

        private async Task<Address> GetAddressId(RequestPointOfInterestJson request)
        {
            var address = await _addressReadOnlyRepository.GetByLatitudeAndLongitude(request.Coordinates!.Latitude, request.Coordinates!.Longitude);

            if (address == null)
            {
                address = new Address
                {
                    Latitude = request.Coordinates!.Latitude,
                    Longitude = request.Coordinates!.Longitude,
                    Cep = request.Address!.Cep,
                    City = request.Address!.City,
                    Country = request.Address!.Country,
                    District = request.Address!.District,
                    Number = request.Address!.Number.ToString(),
                    State = request.Address!.State,
                    Street = request.Address!.Street
                };

                await _addressWriteOnlyRepository.Add(address);
            }

            return address;
        }
    }
}
