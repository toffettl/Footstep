using AutoMapper;
using Footstep.Communication.Enums;
using Footstep.Communication.Requests.Traces;
using Footstep.Communication.Responses.Traces;
using Footstep.Domain.Entities;
using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Addresses;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception.ExceptionsBase;
using System.Threading.Tasks;

namespace Footstep.Application.UseCases.Traces.Create
{
    public class CreatePointOfInterestUseCase : ICreatePointOfInterestUseCase
    {
        private readonly IPointsOfInterestWriteOnlyRepository _pointOfInterestWriteOnlyRepository;
        private readonly IAddressWriteOnlyRepository _addressWriteOnlyRepository;
        private readonly IAddressReadOnlyRepository _addressReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePointOfInterestUseCase(
            IPointsOfInterestWriteOnlyRepository repository,
            IAddressWriteOnlyRepository addressWriteOnlyRepository,
            IAddressReadOnlyRepository addressReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _pointOfInterestWriteOnlyRepository = repository;
            _addressWriteOnlyRepository = addressWriteOnlyRepository;
            _addressReadOnlyRepository = addressReadOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePointOfInterestJson> Execute(RequestPointOfInterestJson request)
        {
            Validade(request);

            var entity = _mapper.Map<PointOfInterest>(request);

            entity.AddressId = await GetAddressId(request);
            entity.UserPointOfInterestRelations.Add(CreateUserPointOfInterestRelation(request.AuthorId));

            await _pointOfInterestWriteOnlyRepository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponsePointOfInterestJson>(entity);
        }

        private void Validade(RequestPointOfInterestJson request)
        {
            var validator = new RequestPointOfInterestJsonValidator();

            var result = validator.Validate(request);

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
                    Cep = request.Adress?.Cep,
                    City = request.Adress?.City,
                    Country = request.Adress?.Coutry,
                    District = request.Adress?.District,
                    Number = request.Adress!.Number,
                    State = request.Adress?.State,
                    Street = request.Adress?.Street
                };

                await _addressWriteOnlyRepository.Add(address);
            }

            return address.Id;
        }

        private UserPointOfInterestRelation CreateUserPointOfInterestRelation(Guid userId)
        {
            return new UserPointOfInterestRelation
            {
                UserId = userId,
                Type = (Domain.Enums.UserPointOfInterestRelationType)(int)UserPointOfInterestRelationType.Creator
            };
        }
    }
}
