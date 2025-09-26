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
using System.Net;

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
            Validade(request);

            var pointOfInterest = _mapper.Map<PointOfInterest>(request);

            Address address = await GetAddressId(request);

            pointOfInterest.AddressId = address.Id;

            if (request.PointOfInterestType == PointOfInterestType.Mark)
            {
                pointOfInterest.ExpireAt = null;
            }

            ResponsePointOfInterestJson response = await CreateResponse(pointOfInterest);

            response.Coordinates = _mapper.Map<ResponseCoordinates>(address);
            response.Address = _mapper.Map<ResponseAddress>(address);

            await _pointOfInterestWriteOnlyRepository.Add(pointOfInterest);

            await _unitOfWork.Commit();

            return response;
        }

        private void Validade(RequestPointOfInterestJson request)
        {
            var validator = new RequestPointOfInterestJsonValidator();

            var result = validator.Validate(request);

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
                    Number = request.Address!.Number,
                    State = request.Address!.State,
                    Street = request.Address!.Street
                };

                await _addressWriteOnlyRepository.Add(address);
            }

            return address;
        }

        private async Task<ResponsePointOfInterestJson> CreateResponse(PointOfInterest pointOfInterest)
        {
            var response = _mapper.Map<ResponsePointOfInterestJson>(pointOfInterest);
            var user = await _userReadOnlyRepository.GetById(pointOfInterest.UserId);

            if (user == null)
            {
                throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }

            response.Author = _mapper.Map<ResponseAuthor>(user);

            response.Status = new ResponseStatus
            {
                Likes = 0,
                Comments = 0,
            };

            return response;
        }
    }
}
